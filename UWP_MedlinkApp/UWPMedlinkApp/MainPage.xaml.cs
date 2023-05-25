using DbLibrary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPMedlinkApp.View.Pages;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace UWPMedlinkApp
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
            //frmPrincipal.Navigate(typeof(PatientsPage));
            //frmPrincipal.Navigate(typeof(TreatmentsPage));
            //frmPrincipal.Navigate(typeof(MedicinesPage));
        }

        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem.Equals(nviLoginPage.Content) && !frmPrincipal.SourcePageType.Equals(typeof(LoginPage)))
            {
                // If PatientsPage or TreatmentsPage tab is selected
                frmPrincipal.Navigate(typeof(LoginPage));
            }
            else if (args.InvokedItem.Equals(nviPatientsPage.Content) && !frmPrincipal.SourcePageType.Equals(typeof(PatientsPage)))
            {
                if (DoctorDB._currentDoctor != null)
                {
                    // If PatientsPage tab is selected from active LoginPage
                    frmPrincipal.Navigate(typeof(PatientsPage));
                }

            }else if (args.InvokedItem.Equals(nviTreatmentsPage.Content) && !frmPrincipal.SourcePageType.Equals(typeof(TreatmentsPage)))
            {
                if (DoctorDB._currentDoctor != null)
                {
                    // If TreatmentsPage tab is selected
                    frmPrincipal.Navigate(typeof(TreatmentsPage));
                }
            }
            else if (args.InvokedItem.Equals(nviMedicinesPage.Content) && !frmPrincipal.SourcePageType.Equals(typeof(MedicinesPage)))
            {
                if (DoctorDB._currentDoctor != null)
                {
                    // If MedicinesPage tab is selected and _selectedTreatment is not null
                    frmPrincipal.Navigate(typeof(MedicinesPage));
                }
            }
        }

        public static void NavigationViewItemIsEnabled(string navigationItemName, bool isEnabled)
        {
            // Access NavigationView items from another page
            var rootFrame = Window.Current.Content as Frame;
            var mainPage = rootFrame.Content as MainPage;

            if (mainPage != null)
            {
                NavigationView navigationView = mainPage.FindName("nvgMain") as NavigationView;

                if (navigationView != null)
                {
                    // Access the NavigationView items
                    var navigationItems = navigationView.MenuItems;

                    // Modify the IsEnabled property for each item
                    foreach (NavigationViewItemBase item in navigationItems)
                    {
                        if (item.Name.Equals(navigationItemName))
                        {
                            item.IsEnabled = isEnabled; // Set the desired IsEnabled value for each item
                        }
                    }
                }
            }
        }
    }
}
