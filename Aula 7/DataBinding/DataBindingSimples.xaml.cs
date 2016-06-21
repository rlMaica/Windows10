using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DataBinding
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataBindingSimples : Page
    {
        public DataBindingSimples()
        {
            this.InitializeComponent();

            //this.textBlock2.Text = mySlider.Minimum.ToString();
            //this.textBlock4.Text = mySlider.Maximum.ToString();
        }

        //private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        //{
        //    this.textBlock6.Text = e.NewValue.ToString();
        //}
    }
}
