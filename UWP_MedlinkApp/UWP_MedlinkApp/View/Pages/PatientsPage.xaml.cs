using ChoETL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_MedlinkApp.View.Pages
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PatientsPage : Page
    {
        public PatientsPage()
        {
            this.InitializeComponent();
        }

        private void Patients_Loaded(object sender, RoutedEventArgs e)
        {
            // En principi, podriem no tenir el boto de update patients i tenir 2 botons (save/cancel) en el formulari de patient
        }

        #region DATAGRID LISTENERS
        private void dtgPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region BUTTON LISTENERS
        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnEditPatient_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemovePatient_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
