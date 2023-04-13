using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_MedlinkApp.View.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace UWP_MedlinkApp
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            frmPrincipal.Navigate(typeof(LoginPage));
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem.Equals(nviLoginPage.Content) && !frmPrincipal.SourcePageType.Equals(typeof(LoginPage)))
            {
                // If PatientsPage tab is selected
                frmPrincipal.Navigate(typeof(LoginPage));
            }
            else if (args.InvokedItem.Equals(nviPatientsPage.Content) && !frmPrincipal.SourcePageType.Equals(typeof(PatientsPage)))
            {
                // If LoginPage tab is selected
                frmPrincipal.Navigate(typeof(PatientsPage));

                /*
                if (EditorPage._levels != null)
                {
                    frmPrincipal.Navigate(typeof(GamePage), EditorPage._levels); //Passem els levels per parametre
                }
                */
            }
        }
    }
}
