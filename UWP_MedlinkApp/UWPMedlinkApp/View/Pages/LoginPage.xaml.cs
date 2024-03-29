﻿using DbLibrary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWPMedlinkApp.View.Dialogs;
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

namespace UWPMedlinkApp.View.Pages
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            DoctorDB._currentDoctor = null;
        }

        private void lnkChangePassword_Click(object sender, RoutedEventArgs e)
        {
            LoadChangePasswordDialog();
        }

        private void lnkForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            LoadRecoverPasswordDialog();
        }

        private async Task LoadChangePasswordDialog()
        {
            LoginPage_ChangePasswordDialog changePasswordDialog = new LoginPage_ChangePasswordDialog();
            await changePasswordDialog.ShowAsync();
        }

        private async Task LoadRecoverPasswordDialog()
        {
            LoginPage_PasswordRecoveryDialog recoverPasswordDialog = new LoginPage_PasswordRecoveryDialog();
            await recoverPasswordDialog.ShowAsync();
        }

        private void txbLogUsnm_TextChanged(object sender, TextChangedEventArgs e)
        {
            txbErrorMessage.Visibility = Visibility.Collapsed;
        }

        private void txbLogPswd_TextChanged(object sender, TextChangedEventArgs e)
        {
            txbErrorMessage.Visibility = Visibility.Collapsed;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            DoctorDB._currentDoctor = null;

            string username = txbLogUsnm.Text+"";
            string password = txbLogPswd.Text+"";

            if (username.Length > 0 && password.Length > 0)
            {
                if (DoctorDB.CheckIfDoctorExistsByCredentials(username, password))
                {
                    txbErrorMessage.Visibility = Visibility.Collapsed;

                    DoctorDB._currentDoctor = DoctorDB.GetDoctorByCredentials(username, password);

                    if (DoctorDB._currentDoctor != null)
                    {
                        //GOTO PATIENS PAGE
                        Frame.Navigate(typeof(PatientsPage));
                    }
                }
                else
                {
                    txbErrorMessage.Visibility = Visibility.Visible;
                }
            }
            else
            {
                txbErrorMessage.Visibility = Visibility.Visible;
            }
        }
    }
}
