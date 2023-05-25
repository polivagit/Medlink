using DbLibrary.DB;
using DbLibrary.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPMedlinkApp.View.Dialogs
{
    public sealed partial class LoginPage_PasswordRecoveryDialog : ContentDialog
    {
        private static bool emailOk = false;
        private static bool emailSent = false;

        private static string senderEmail = "tmedlink@gmail.com";
        private const string APP_KEY = "bftnvincwvxagvyp";
        private static string recieverEmail = "tmedlink@gmail.com";

        public LoginPage_PasswordRecoveryDialog()
        {
            this.InitializeComponent();
        }

        private void RecoverClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            DoctorDB auxDoctor = DoctorDB.GetDoctorByEmail(txbRecoveryEmail.Text);
            string newPasswd = DBUtils.GenerateUniqueHexString(20);

            // UPDATE PASSWORD
            DoctorDB.UpdateDoctorPassword(auxDoctor.Pers_id, newPasswd);

            // SEND EMAIL (NOTIFY)
            emailSent = DBUtils.SendEmail(senderEmail,
                                            APP_KEY,
                                            recieverEmail,
                                            "Medlink password recovery",
                                            @"Hello, your password has changed. Your new password is: " + newPasswd + " . Remember that your username is: " + auxDoctor.Pers_login_username);

        }

        private void CancelClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
        }

        #region TEXTBOX LISTENERS
        private void txbRecoveryEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            emailOk = CheckEmail(txbRecoveryEmail.Text);

            CheckIfCanRecover();
        }
        #endregion

        private bool CheckEmail(string email)
        {
            if (DoctorDB.CheckIfDoctorExistsByEmail(email))
            {
                txbEmailErrorMessage.Visibility = Visibility.Collapsed;
                return true;
            }
            else
            {
                txbEmailErrorMessage.Visibility = Visibility.Visible;
                return false;
            }
        }

        private void CheckIfCanRecover()
        {
            if (emailOk)
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
