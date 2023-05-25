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
    public sealed partial class LoginPage_ChangePasswordDialog : ContentDialog
    {
        private static bool creadentialsOk = false;
        private static bool passwordConfirmationOk = false;

        public LoginPage_ChangePasswordDialog()
        {
            this.InitializeComponent();
        }

        private void ChangeClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            DoctorDB auxDoctor = DoctorDB.GetDoctorByCredentials(txbChangeUsername.Text, txbChangeOldPassword.Text);
            DoctorDB.UpdateDoctorPassword(auxDoctor.Pers_id, txbChangeNewPassword.Text);
        }

        private void CancelClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
        }

        #region TEXTBOX LISTENERS
        private void txbChangeUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            creadentialsOk = CheckCredentials(txbChangeUsername.Text, txbChangeOldPassword.Text);
            
            CheckIfCanApplyChanges();
        }

        private void txbChangeOldPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            creadentialsOk = CheckCredentials(txbChangeUsername.Text, txbChangeOldPassword.Text);

            CheckIfCanApplyChanges();
        }

        private void txbChangeNewPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordConfirmationOk = CheckPasswordConfirmation(txbChangeNewPassword.Text, txbChangeConfirmPassword.Text);
            
            CheckIfCanApplyChanges();
        }

        private void txbChangeConfirmPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordConfirmationOk = CheckPasswordConfirmation(txbChangeNewPassword.Text, txbChangeConfirmPassword.Text);

            CheckIfCanApplyChanges();
        }
        #endregion

        private bool CheckCredentials(string username, string password)
        {
            if (DoctorDB.CheckIfDoctorExistsByCredentials(username, password))
            {
                txbCredentialsErrorMessage.Visibility = Visibility.Collapsed;
                return true;
            }
            else
            {
                txbCredentialsErrorMessage.Visibility = Visibility.Visible;
                return false;
            }
        }

        private bool CheckPasswordConfirmation(string newPassword, string confirmationPassword)
        {
            if (newPassword == confirmationPassword)
            {
                txbPsswdConfirmationErrorMessage.Visibility = Visibility.Collapsed;
                return true;
            }
            else
            {
                txbPsswdConfirmationErrorMessage.Visibility = Visibility.Visible;
                return false;
            }
        }

        private void CheckIfCanApplyChanges()
        {
            if (creadentialsOk && passwordConfirmationOk)
            {
                IsPrimaryButtonEnabled = true;
            }
            else 
            {
                IsPrimaryButtonEnabled = false;
            }
        }
    }
}
