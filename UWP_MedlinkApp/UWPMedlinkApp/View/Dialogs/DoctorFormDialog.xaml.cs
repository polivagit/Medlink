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
    public sealed partial class DoctorFormDialog : ContentDialog
    {
        private static DoctorDB _doctorCopy = new DoctorDB();
        private bool dataChanged = false;

        public DoctorFormDialog(DoctorDB doctorAux)
        {
            this.InitializeComponent();

            IsPrimaryButtonEnabled = dataChanged;

            ReloadGenderCombobox();
            LoadDoctorInfo(doctorAux);
            _doctorCopy = doctorAux;
        }

        #region BUTTON LISTENERS
        private void SaveChangesClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            SaveTreatmentInfo();
            DoctorDB.UpdateDoctor(_doctorCopy);
        }

        private void CancelClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            this.Hide();
        }
        #endregion

        #region UI METHODS
        private void LoadDoctorInfo(DoctorDB doctor)
        {
            txbDoctorFirstName.Text = doctor.Pers_first_name;
            txbDoctorLastName1.Text = doctor.Pers_last_name_1;
            txbDoctorLastName2.Text = doctor.Pers_last_name_2;
            txbDoctorNif.Text = doctor.Pers_nif;
            txbDoctorPhoneNumber.Text = doctor.Pers_phone_number;
            txbDoctorEmail.Text = doctor.Pers_email;
            txbDoctorStreet.Text = doctor.Pers_addrs_street;
            txbPostalCode.Text = doctor.Pers_addrs_zip_code;
            txbCity.Text = doctor.Pers_addrs_city;
            txbProvince.Text = doctor.Pers_addrs_province;
            txbCountry.Text = doctor.Pers_addrs_country;
            txbDoctorCollegiateUID.Text = doctor.Pers_doct_collegiate_uid;
            txbDoctorSpecialty.Text = SpecialtyDB.GetSpecialtyById(doctor.Pers_doct_specialty_id).Spec_name.ToUpper();
            txbDoctorUsername.Text = doctor.Pers_login_username;

            dtpDoctorBirthdate.Date = doctor.Pers_birthdate;

            foreach (GenderTypeDB gender in Enum.GetValues(typeof(GenderTypeDB)))
            {
                string key = gender.ToString();
                int value = (int)gender;

                if (doctor.Pers_gender.ToString() == key)
                {
                    cboDoctorGender.SelectedValue = key;
                }
            }
        }

        private void ReloadGenderCombobox()
        {
            cboDoctorGender.ItemsSource = PersonDB.GetGenders();
        }

        private void SaveTreatmentInfo()
        {
            _doctorCopy.Pers_first_name = txbDoctorFirstName.Text;
            _doctorCopy.Pers_last_name_1 = txbDoctorLastName1.Text;
            _doctorCopy.Pers_last_name_2 = txbDoctorLastName2.Text;
            _doctorCopy.Pers_phone_number = txbDoctorPhoneNumber.Text;
            _doctorCopy.Pers_email = txbDoctorEmail.Text;
            _doctorCopy.Pers_addrs_street = txbDoctorStreet.Text;
            _doctorCopy.Pers_addrs_zip_code = txbPostalCode.Text;
            _doctorCopy.Pers_addrs_city = txbCity.Text;
            _doctorCopy.Pers_addrs_province = txbProvince.Text;
            _doctorCopy.Pers_addrs_country = txbCountry.Text;
        }
        #endregion

        #region TEXTBOX LISTENERS
        private void txbDoctorFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = true;
        }

        private void txbDoctorLastName1_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = true;
        }

        private void txbDoctorLastName2_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = true;
        }

        private void txbDoctorStreet_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = true;
        }

        private void txbPostalCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = true;
        }

        private void txbCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = true;
        }

        private void txbProvince_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = true;
        }

        private void txbCountry_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = true;
        }

        private void txbDoctorPhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = true;
        }

        private void txbDoctorEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = true;
        }
        #endregion

        #region OTHER LISTENERS
        private void cboDoctorGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsPrimaryButtonEnabled = true;
        }
        #endregion
    }
}
