using DbLibrary.Model;
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

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPMedlinkApp.View.Dialogs
{
    public sealed partial class CaregiverAssignmentDialog : ContentDialog
    {
        public PersonDB _selectedCaregiver = null;
        private static bool caregiverSelected = false;

        public CaregiverAssignmentDialog()
        {
            this.InitializeComponent();

            dtgCaregivers.ItemsSource = PersonDB.GetAllCaregivers("","","","");
        }

        #region DATAGRID LISTENERS
        private void dtgCaregiver_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedCaregiver = (PersonDB)dtgCaregivers.SelectedItem;

            if (dtgCaregivers.SelectedItem != null)
            {
                caregiverSelected = true;
            }
            else
            {
                caregiverSelected = false;
            }
            

            CheckIfSelectedCaregiver();
        }
        #endregion

        #region BUTTON LISTENERS
        private void btnClearCaregiverFilter_Click(object sender, RoutedEventArgs e)
        {
            txbCaregiverFilterByNIF.Text = "";
            txbCaregiverFilterByFullName.Text = "";
            txbCaregiverFilterByPhoneNumber.Text = "";
            txbCaregiverFilterByEmail.Text = "";
        }

        private void SaveChangesClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void CancelClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
        }
        #endregion

        #region TEXTBOX LISTENERS
        private void txbCaregiverFilterByNIF_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadUIElements();

            dtgCaregivers.ItemsSource = PersonDB.GetAllCaregivers(txbCaregiverFilterByNIF.Text,
                                                                    txbCaregiverFilterByFullName.Text,
                                                                    txbCaregiverFilterByPhoneNumber.Text,
                                                                    txbCaregiverFilterByEmail.Text);
        }

        private void txbCaregiverFilterByFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadUIElements();

            dtgCaregivers.ItemsSource = PersonDB.GetAllCaregivers(txbCaregiverFilterByNIF.Text, 
                                                                    txbCaregiverFilterByFullName.Text,
                                                                    txbCaregiverFilterByPhoneNumber.Text,
                                                                    txbCaregiverFilterByEmail.Text);
        }

        private void txbCaregiverFilterByPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadUIElements();

            dtgCaregivers.ItemsSource = PersonDB.GetAllCaregivers(txbCaregiverFilterByNIF.Text,
                                                                    txbCaregiverFilterByFullName.Text,
                                                                    txbCaregiverFilterByPhoneNumber.Text,
                                                                    txbCaregiverFilterByEmail.Text);
        }

        private void txbCaregiverFilterByEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadUIElements();

            dtgCaregivers.ItemsSource = PersonDB.GetAllCaregivers(txbCaregiverFilterByNIF.Text,
                                                                    txbCaregiverFilterByFullName.Text,
                                                                    txbCaregiverFilterByPhoneNumber.Text,
                                                                    txbCaregiverFilterByEmail.Text);
        }
        #endregion

        #region UI METHODS
        private void ReloadUIElements()
        {
            if (txbCaregiverFilterByNIF.Text.Length > 0
                || txbCaregiverFilterByFullName.Text.Length > 0
                || txbCaregiverFilterByPhoneNumber.Text.Length > 0
                || txbCaregiverFilterByEmail.Text.Length > 0)
            {
                btnClearCaregiverFilter.IsEnabled = true;
            }
            else
            {
                btnClearCaregiverFilter.IsEnabled = false;
            }
        }

        private void CheckIfSelectedCaregiver()
        {
            if (caregiverSelected)
            {
                IsPrimaryButtonEnabled = true;
            }
            else
            {
                IsPrimaryButtonEnabled = false;
            }
        }
        #endregion
    }
}
