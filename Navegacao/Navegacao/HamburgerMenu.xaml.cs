using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Navegacao
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HamburgerMenu : Page
    {
        public new Frame Frame => App.RootFrame;

        public HamburgerMenu()
        {
            this.InitializeComponent();
            this.Frame.Navigated += Frame_Navigated;

            this.Frame.Navigate(typeof(Favoritos));
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            this.MySplitView.Content = this.Frame;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            this.MySplitView.IsPaneOpen = !this.MySplitView.IsPaneOpen;
        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Favoritos));
        }

        private void ListViewItem_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Todos));
        }

        private void ListViewItem_Tapped_2(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Emails));
        }

        private void ListViewItem_Tapped_3(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Telefones));
        }
    }
}
