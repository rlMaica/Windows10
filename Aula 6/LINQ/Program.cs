using LINQ.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class Program
    {
        public static ObservableCollection<Funcionario> funcionarios = new ObservableCollection<Funcionario>();

        //delegate int Somar(int a, int b);

        static void Main(string[] args)
        {
            //Somar soma = MinhaSoma;
            //int resultado = soma(10, 5);

            //Action<string> imprime = ImprimirNome;
            //imprime("Nome");

            //Func<int, int, int> soma = MinhaSoma;

            //Func<int, int, double> soma2 = (a, b) => a + b;
            //double resultado2 = soma(10, 50);

            LoadData();

            //LINQ
            var funcionariosMaiorQueQuatro1 = from f in funcionarios
                                              orderby f.Salario ascending
                                              where f.Salario > 4000
                                              select f;

            //LINQ - Expressão
            //var funcionariosMaiorQueQuatro2 = funcionarios.Where( (f) => f.Salario > 4000 );
            //var funcionariosMaiorQueQuatro2 = funcionarios.Where((f) => f.Salario > 4000).OrderBy((f) => f.Salario);

            var funcionariosMaiorQueQuatro2 = funcionarios.Where((f) =>
            {
                return f.Salario > 4000;
            });

            Console.WriteLine("");
            Console.WriteLine($"Funcionários maior que 4k - ({ funcionariosMaiorQueQuatro1.Count() })");
            foreach (var f in funcionariosMaiorQueQuatro1)
            {
                Console.WriteLine($"{f.NomeCompleto} - {f.Salario}");
            }

            //Funcionarios que são gerentes
            var funcionariosGerentes = from f in funcionarios
                                       where f is Gerente
                                       select f;

            var funcionariosGerentes2 = funcionarios.OfType<Gerente>().OrderBy((f) => f.Nome);

            Console.WriteLine("");
            Console.WriteLine($"Gerentes 1 - ({ funcionariosGerentes.Count() })");
            foreach (var f in funcionariosGerentes)
            {
                Console.WriteLine($"{f.NomeCompleto} - {f.Salario}");
            }

            Console.WriteLine("");
            Console.WriteLine($"Gerentes 2 - ({ funcionariosGerentes2.Count() })");
            foreach (var f in funcionariosGerentes2)
            {
                Console.WriteLine($"{f.NomeCompleto} - {f.Salario}");
            }

            //Funcionarios do Marketing
            var funcionariosDoMarketing = funcionarios.Where((f) => f.Departamento.Nome.Equals("Marketing")).OrderBy((f) => f.Nome);

            Console.WriteLine("");
            Console.WriteLine($"funcionários do Marketing - ({ funcionariosDoMarketing.Count() })");
            foreach (var f in funcionariosDoMarketing)
            {
                Console.WriteLine($"{f.NomeCompleto} - {f.Salario}");
            }

            //Média de Salário dos funcionários do Marketing
            var funcionariosDoMarketingMedia = funcionarios.Where((f) => f.Departamento.Nome.Equals("Marketing"))
                                                           .Select((f) => f.Salario)
                                                           .Average();

            Console.WriteLine("");
            Console.WriteLine($"Média Salárial dos Funcionários do Marketing - ({ funcionariosDoMarketingMedia })");


            //Agrupados
            var deptosAgrupados1 = from f in funcionarios
                                   group f by f.Departamento.Nome into g
                                   select new
                                   {
                                       Depto = g.Key,
                                       Qtde = g.Count()
                                   };

            var deptosAgrupados2 = funcionarios
                                    .GroupBy(f => f.Departamento.Nome)
                                    .Select((grp) => new
                                    {
                                        Depto = grp.Key,
                                        Qtde = grp.Count()
                                    });

            foreach (var item in deptosAgrupados1)
            {
                Console.WriteLine($"{item.Depto} - {item.Qtde}");
            }

            Console.WriteLine("");

            foreach (var item in deptosAgrupados2)
            {
                Console.WriteLine($"{item.Depto} - {item.Qtde}");
            }


            //Agrupado por faixa salárial A > 4000 , B > 2000 < 4000, C < 2000
            var deptosAgrupados3 = funcionarios
                                    .GroupBy(f =>
                                    {
                                        return f.Salario >= 4000 ? "Faixa A" : f.Salario < 2000 ? "Faixa C" : "Faixa B";
                                    })
                                    .Select(g => new
                                    {
                                        Grupo = g.Key,
                                        Qtde = g.Count()
                                    });

            Console.WriteLine("");
            Console.WriteLine("Faixa Salárial");

            foreach (var item in deptosAgrupados3)
            {
                Console.WriteLine($"{item.Grupo} - {item.Qtde}");
            }


            //DISTINCT do Array
            int[] inteiros = new int[] { 1, 2, 2, 2, 2, 3, 3, 5, 8, 9, 9 };

            var distinct = inteiros.Distinct();
            Console.WriteLine("");
            Console.WriteLine("DISTINCT");
            foreach(var item in distinct)
            {
                Console.WriteLine($"item - {item}");
            }

            Console.ReadLine();
        }

        //public static void ImprimirNome(string f)
        //{
            
        //}

        //public static int MinhaSoma(int a, int b)
        //{
            //return a + b;
        //}

        #region Load Data
        private static void LoadData()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.DateFormatString = "dd/MM/yyyy";

            string pasta = Path.GetDirectoryName(
                Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            var arquivo = Path.Combine(pasta, "Data", "Funcionario.json");
            using (StreamReader sr = new StreamReader(arquivo))
            {
                string json = sr.ReadToEnd();
                var registries = JsonConvert.DeserializeObject<ObservableCollection<Funcionario>>(json, settings);

                funcionarios = new ObservableCollection<Funcionario>(registries);
            }

            arquivo = Path.Combine(pasta, "Data", "Gerente.json");
            using (StreamReader sr = new StreamReader(arquivo))
            {
                string json = sr.ReadToEnd();
                var registries = JsonConvert.DeserializeObject<ObservableCollection<Gerente>>(json, settings);

                var list = funcionarios.Concat(registries);
                funcionarios = new ObservableCollection<Funcionario>(list);
            }
        }
        #endregion
    }
}
