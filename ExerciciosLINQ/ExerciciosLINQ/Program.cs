using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace LINQ
{
    class Program
    {
        public static ObservableCollection<Funcionario> funcionarios => LoadData();

        static void Main(string[] args)
        {
            //Exercicio01A();
            //Exercicio01B();
            //Exercicio02A();
            //Exercicio02B();
            Exercicio03A();
            //Exercicio03B();
            Console.ReadLine();
        }

        #region Exercício 01
        ///TODO - Exercício 01) Filtrar todos os funcionários que são gerentes e ordenar
        ///por order alfabética. Utilizar as duas formas de LINQ, ou seja, a expressão
        ///e os métodos de extensão. Imprimir o nome completo do funcionário dos dois
        ///filtros separadamentes. Para isso, existem dois métodos para o exercício 01:
        ///* Exercicio01A() - filtro com expressão
        ///* Exercicio01B() - filtro com método de extensão
        public static IEnumerable<Funcionario> Exercicio01A()
        {
            Console.WriteLine();
            Console.WriteLine("EXERCÍCIO 01 A - expressão");

            ///TODO: Substitua o funcionários pelo filtro.
            var filtro = from f in funcionarios
                         where f is Gerente
                         orderby f.Nome
                         select f;

            ///TODO: Descomente o trecho de código abaixo para testar o filtro.
            foreach (var item in filtro)
            {
                Console.WriteLine($"{item.NomeCompleto}");
            }
            return filtro;
        }

        public static IEnumerable<Funcionario> Exercicio01B()
        {
            Console.WriteLine();
            Console.WriteLine("EXERCÍCIO 01 B - método de extensão");

            ///TODO: Substitua o funcionários pelo filtro.
            var filtro = funcionarios.OfType<Gerente>().OrderBy((f) => f.Nome);

            ///TODO: Descomente o trecho de código abaixo para testar o filtro.
            foreach (var item in filtro)
            {
                Console.WriteLine($"{item.NomeCompleto}");
            }
            return filtro;
        }
        #endregion

        #region Exercício 02
        ///TODO - Exercício 02) Filtrar todos os funcionários com mais de 8 anos de empresa
        ///e ordenar do mais velho para o mais novo. Nesse filtro, crie um tipo anônimo
        ///com o nome completo, data de admissão, anos de admissão e o nome do departamento.
        ///Utilizar as duas formas de LINQ, ou seja, a expressão e os métodos de extensão.
        ///Imprimir o nome completo, anos de admissão e nome do departamento do funcionário dos dois
        ///filtros separadamentes. Para isso, existem dois métodos para o exercício 02:
        ///* Exercicio02A() - filtro com expressão
        ///* Exercicio02B() - filtro com método de extensão
        public static IEnumerable<object> Exercicio02A()
        {
            Console.WriteLine();
            Console.WriteLine("EXERCÍCIO 02 A - expressão");
            
            ///TODO: Substitua o funcionários pelo filtro.
            var filtro = from f in funcionarios
                         where f.DataAdmissao.Year < (DateTime.Now.Year - 8)
                         orderby f.DataAdmissao ascending
                         select new
                         {
                             Nome = f.NomeCompleto,
                             Data = f.DataAdmissao,
                             Anos = DateTime.Now.Year - f.DataAdmissao.Year,
                             Depto = f.Departamento.Nome
                         };

            ///TODO: Descomente o trecho de código abaixo para testar o filtro.
            foreach (var item in filtro)
            {
                Console.WriteLine($"{item.Nome} - {item.Data:dd/MM/yyyy} - {item.Anos} - {item.Depto}");
            }
            return filtro;
        }

        public static IEnumerable<object> Exercicio02B()
        {
            Console.WriteLine();
            Console.WriteLine("EXERCÍCIO 02 B - método de extensão");
            
            ///TODO: Substitua o funcionários pelo filtro.
            var filtro = funcionarios
                            .Where((f) => f.DataAdmissao.Year < (DateTime.Now.Year - 8))
                            .OrderBy((f) => f.DataAdmissao)
                            .Select((grp) => new
                            {
                                Nome = grp.NomeCompleto,
                                Data = grp.DataAdmissao,
                                Anos = DateTime.Now.Year - grp.DataAdmissao.Year,
                                Depto = grp.Departamento.Nome
                            });

            ///TODO: Descomente o trecho de código abaixo para testar o filtro.
            foreach (var item in filtro)
            {
                Console.WriteLine($"{item.Nome} - {item.Data:dd/MM/yyyy} - {item.Anos} - {item.Depto}");
            }
            return filtro;
        }
        #endregion

        #region Exercício 03
        ///TODO - Exercício 03) Agrupar os funcionários por faixa salarial, onde, a faixa A deve
        ///agrupar todos os funcionários com o salário maior ou igual a $3000.00 e a faixa B deve
        ///agrupar todos os funcionários com o salário menor que $3000.0 e maior que $1000.0 e,
        ///por último, a Faixa C deve agrupar todos os funcionários com o salário menor ou igual
        ///a $1000.0.
        ///Utilizar as duas formas de LINQ, ou seja, a expressão e os métodos de extensão.
        ///Imprimir a faixa salarial e a quantidade de funcionários por faixa utilizando os dois tipos
        ///de filtros separadamentes. Para isso, existem dois métodos para o exercício 03:
        ///* Exercicio03A() - filtro com expressão
        ///* Exercicio03B() - filtro com método de extensão
        public static IEnumerable<object> Exercicio03A()
        {
            Console.WriteLine();
            Console.WriteLine("EXERCÍCIO 03 A - expressão");

            ///TODO: Substitua o funcionários pelo filtro.
            var filtro = from f in funcionarios
                         group f by new
                         {
                             tipo = f.Salario >= 3000 ? "Faixa A" : f.Salario < 1000 ? "Faixa C" : "Faixa B"
                         } 
                         into g
                         select new
                         {
                             Faixa = g.Key.tipo,
                             Quantidade = g.Count()
                         };

            ///TODO: Descomente o trecho de código abaixo para testar o filtro.
            foreach (var item in filtro)
            {
                Console.WriteLine($"{item.Faixa} - {item.Quantidade}");
            }
            return filtro;
        }

        public static IEnumerable<object> Exercicio03B()
        {
            Console.WriteLine();
            Console.WriteLine("EXERCÍCIO 03 B - método de extensão");
            
            ///TODO: Substitua o funcionários pelo filtro.
            var filtro = funcionarios
                            .GroupBy(f =>
                            {
                                return f.Salario >= 3000 ? "Faixa A" : f.Salario < 1000 ? "Faixa C" : "Faixa B";
                            })
                            .Select(g => new
                            {
                                Faixa = g.Key,
                                Quantidade = g.Count()
                            });

            ///TODO: Descomente o trecho de código abaixo para testar o filtro.
            foreach (var item in filtro)
            {
                Console.WriteLine($"{item.Faixa} - {item.Quantidade}");
            }
            return filtro;
        }
        #endregion

        #region Exercício 04
        ///TODO - Exercício 04) Agrupar os funcionários por departamento.
        ///Utilizar as duas formas de LINQ, ou seja, a expressão e os métodos de extensão.
        ///Imprimir o nome do departamento e a quantidade de funcionários por departamento dos dois filtros
        ///separadamentes. Para isso, existem dois métodos para o exercício 04:
        ///* Exercicio04A() - filtro com expressão
        ///* Exercicio04B() - filtro com método de extensão
        public static IEnumerable<object> Exercicio04A()
        {
            Console.WriteLine();
            Console.WriteLine("EXERCÍCIO 04 A - expressão");
            
            ///TODO: Substitua o funcionários pelo filtro.
            var filtro = funcionarios;

            ///TODO: Descomente o trecho de código abaixo para testar o filtro.
            //foreach (var item in filtro)
            //{
            //    Console.WriteLine($"{item.Depto} - {item.Quantidade}");
            //}
            return filtro;
        }

        public static IEnumerable<object> Exercicio04B()
        {
            Console.WriteLine();
            Console.WriteLine("EXERCÍCIO 04 B - método de extensão");
            
            ///TODO: Substitua o funcionários pelo filtro.
            var filtro = funcionarios;

            ///TODO: Descomente o trecho de código abaixo para testar o filtro.
            //foreach (var item in filtro)
            //{
            //    Console.WriteLine($"{item.Depto} - {item.Quantidade}");
            //}
            return filtro;
        } 
        #endregion

        private static ObservableCollection<Funcionario> LoadData()
        {
            var funcionariosTemp = new ObservableCollection<Funcionario>();

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.DateFormatString = "dd/MM/yyyy";

            string pasta = Path.GetDirectoryName(
                Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            var arquivo = Path.Combine(pasta, "Funcionario.json");
            using (StreamReader sr = new StreamReader(arquivo))
            {
                string json = sr.ReadToEnd();
                var registries = JsonConvert.DeserializeObject<ObservableCollection<Funcionario>>(json, settings);

                funcionariosTemp = new ObservableCollection<Funcionario>(registries);
            }

            arquivo = Path.Combine(pasta, "Gerente.json");
            using (StreamReader sr = new StreamReader(arquivo))
            {
                string json = sr.ReadToEnd();
                var registries = JsonConvert.DeserializeObject<ObservableCollection<Gerente>>(json, settings);

                var list = funcionariosTemp.Concat(registries);
                funcionariosTemp = new ObservableCollection<Funcionario>(list);
            }

            return funcionariosTemp;
        }
    }
}
