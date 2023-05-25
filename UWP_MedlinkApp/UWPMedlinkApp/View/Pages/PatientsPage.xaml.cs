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
    public sealed partial class PatientsPage : Page
    {
        public static DoctorDB _activeDoctor = DoctorDB._currentDoctor;
        public static PatientDB _selectedPatient = new PatientDB();
        private static PatientDB _selectedPatientCopy = new PatientDB();

        public PatientsPage()
        {
            this.InitializeComponent();
        }

        private void Patients_Loaded(object sender, RoutedEventArgs e)
        {
            _activeDoctor = DoctorDB._currentDoctor;

            LoadActiveDoctorInfo();
            dtgPatients.ItemsSource = PatientDB.GetAllPatients("");

            ReloadGenderCombobox();
        }

        #region DATAGRID LISTENERS
        private void dtgPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReloadUIElements();

            _selectedPatient = (PatientDB)dtgPatients.SelectedItem;

            if (_selectedPatient != null)
            {
                dtgTreatments.ItemsSource = TreatmentDB.GetAllTreatmentsByPatientAndDoctorId("", _selectedPatient.Pers_id, _activeDoctor.Pers_id);

                LoadSelectedPatientInfo(_selectedPatient);

                _selectedPatientCopy = _selectedPatient;

                btnAddPatient.Visibility = Visibility.Collapsed;
                btnUpdatePatient.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region BUTTON LISTENERS
        private void btnClearPatientFilterByFullName_Click(object sender, RoutedEventArgs e)
        {
            txbPatientFilterByFullName.Text = "";
        }

        private void btnGoToTreatmentsDetails_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TreatmentsPage));
        }

        private void btnNewPatient_Click(object sender, RoutedEventArgs e)
        {
            btnAddPatient.Visibility = Visibility.Visible;
            btnUpdatePatient.Visibility = Visibility.Collapsed;

            ClearPatientInfo();
        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            DisplayPatientConfirmationDialog("ADD");
        }

        private void btnUpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            DisplayPatientConfirmationDialog("UPDATE");
        }

        private void btnRemovePatient_Click(object sender, RoutedEventArgs e)
        {
            DisplayPatientConfirmationDialog("REMOVE");
        }

        private void btnSeeDoctorDetails_Click(object sender, RoutedEventArgs e)
        {
            LoadSeeDoctorDetailsDialog(_activeDoctor);
        }

        private void btnSearchCaregiver_Click(object sender, RoutedEventArgs e)
        {
            LoadCaregiverAssignmentDialog();
        }

        private void btnRemoveCaregiver_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region UI METHODS
        private void ReloadUIElements()
        {
            if (dtgPatients.SelectedItem != null)
            {
                btnGoToTreatmentsDetails.IsEnabled = true;
            }
            else
            {
                btnGoToTreatmentsDetails.IsEnabled = false;
            }

            if (txbPatientFilterByFullName.Text.Length > 0)
            {
                btnClearPatientFilterByFullName.IsEnabled = true;
            }
            else
            {
                btnClearPatientFilterByFullName.IsEnabled = false;
            }
        }

        private void ReloadGenderCombobox()
        {
            cboPati_Gender.ItemsSource = PersonDB.GetGenders();
        }

        private void LoadActiveDoctorInfo()
        {
            txbDoct_FullName.Text = _activeDoctor.Pers_FullName;
            txbDoct_Specialty.Text = _activeDoctor.Pers_DoctorSpecialtyName;
        }

        private void LoadSelectedPatientInfo(PatientDB patientAux)
        {
            txbPati_FirstName.Text = patientAux.Pers_first_name;
            txbPati_LastName1.Text = patientAux.Pers_last_name_1;
            txbPati_LastName2.Text = patientAux.Pers_last_name_2;
            txbPati_Nif.Text = patientAux.Pers_nif;
            txbPati_PhoneNumber.Text = patientAux.Pers_phone_number;
            txbPati_Email.Text = patientAux.Pers_email;
            txbPati_Street.Text = patientAux.Pers_addrs_street;
            txbPati_PostalCode.Text = patientAux.Pers_addrs_zip_code;
            txbPati_City.Text = patientAux.Pers_addrs_city;
            txbPati_Province.Text = patientAux.Pers_addrs_province;
            txbPati_Country.Text = patientAux.Pers_addrs_country;
            txbPati_Username.Text = patientAux.Pers_login_username;
            txbPati_Weight.Text = patientAux.Pati_weight + "";
            txbPati_Height.Text = patientAux.Pati_height + "";
            txbPati_Remarks.Text = patientAux.Pati_remarks + "";
            txbPati_Caregiver.Text = patientAux.Pers_Caregiver;

            dtpPati_Birthdate.Date = patientAux.Pers_birthdate;

            foreach (GenderTypeDB gender in Enum.GetValues(typeof(GenderTypeDB)))
            {
                string key = gender.ToString();
                int value = (int)gender;

                if (patientAux.Pers_gender.ToString() == key)
                {
                    cboPati_Gender.SelectedValue = key;
                }
            }
        }

        private void ClearPatientInfo()
        {
            dtgPatients.SelectedItem = null;
            dtgTreatments.ItemsSource = null;

            txbPati_FirstName.Text = "";
            txbPati_LastName1.Text = "";
            txbPati_LastName2.Text = "";
            txbPati_Nif.Text = "";
            txbPati_PhoneNumber.Text = "";
            txbPati_Email.Text = "";
            txbPati_Street.Text = "";
            txbPati_PostalCode.Text = "";
            txbPati_City.Text = "";
            txbPati_Province.Text = "";
            txbPati_Country.Text = "";
            txbPati_Username.Text = "";
            txbPati_Weight.Text = "";
            txbPati_Height.Text = "";
            txbPati_Remarks.Text = "";
            txbPati_Caregiver.Text = "";

            dtpPati_Birthdate.Date = new DateTimeOffset(new DateTime(1950, 01, 01));

            cboPati_Gender.SelectedIndex = -1;
        }
        #endregion

        #region CONTENTDIALOG METHODS
        private async Task LoadSeeDoctorDetailsDialog(DoctorDB doctorAux)
        {
            DoctorFormDialog seeDoctorDetailsDialog = new DoctorFormDialog(doctorAux);
            await seeDoctorDetailsDialog.ShowAsync();
        }

        private async Task LoadCaregiverAssignmentDialog()
        {
            CaregiverAssignmentDialog caregiverAssignmentForm = new CaregiverAssignmentDialog();
            await caregiverAssignmentForm.ShowAsync();
        }

        public async Task DisplayPatientConfirmationDialog(string action)
        {
            PatientsPage_PatientCRUDConfirmationDialog crudPatientDialog = new PatientsPage_PatientCRUDConfirmationDialog()
            {
                Title = "CONFIRMATION: "+ action + " PATIENT",
                Content = "Do you really want to "+action+" the patient?",
                PrimaryButtonText = "PROCEED",
                CloseButtonText = "CANCEL"
            };

            ContentDialogResult result = await crudPatientDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // OK
                switch (action)
                {
                    case "ADD":
                    {
                            // INSERT PATIENT


                            dtgPatients.ItemsSource = PatientDB.GetAllPatients("");
                            ClearPatientInfo();
                            break;
                    }
                    case "UPDATE":
                    {
                            // UPDATE PATIENT
                            PatientDB.UpdatePatient(_selectedPatientCopy);

                            dtgPatients.ItemsSource = PatientDB.GetAllPatients("");
                            ClearPatientInfo();
                            break;
                    }
                    case "REMOVE":
                    {
                            // DELETE PATIENT
                            PatientDB.DeletePatientById(_selectedPatient.Pers_id);

                            dtgPatients.ItemsSource = PatientDB.GetAllPatients("");
                            ClearPatientInfo();
                        break;
                    }
                    default:
                        break;
                }
            }
            else
            {
                // CANCEL / DO NOTHING
            }
        }
        #endregion

        #region TEXT LISTENERS
        private void txbPatientFilterByFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadUIElements();

            dtgPatients.ItemsSource = PatientDB.GetAllPatients(txbPatientFilterByFullName.Text);
        }

        private void txbPati_FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pers_first_name = txbPati_FirstName.Text;
        }

        private void txbPati_LastName1_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pers_last_name_1 = txbPati_LastName1.Text;
        }

        private void txbPati_LastName2_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pers_last_name_2 = txbPati_LastName2.Text;
        }

        private void txbPati_Nif_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pers_nif = txbPati_Nif.Text;
        }

        private void txbPati_PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pers_phone_number = txbPati_PhoneNumber.Text;
        }

        private void txbPati_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pers_email = txbPati_Email.Text;
        }

        private void txbPati_Street_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pers_addrs_street = txbPati_Street.Text;
        }

        private void txbPati_PostalCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pers_addrs_zip_code = txbPati_PostalCode.Text;
        }

        private void txbPati_City_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pers_addrs_city = txbPati_City.Text;
        }

        private void txbPati_Province_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pers_addrs_province = txbPati_Province.Text;
        }

        private void txbPati_Country_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pers_addrs_country = txbPati_Country.Text;
        }

        private void txbPati_Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pers_login_username = txbPati_Username.Text;
        }

        private void txbPati_Weight_TextChanged(object sender, TextChangedEventArgs e)
        {
            //float weight = (float)Convert.ToDouble(txbPati_Weight.Text);

            //_selectedPatientCopy.Pati_weight = weight;
        }

        private void txbPati_Height_TextChanged(object sender, TextChangedEventArgs e)
        {
            //_selectedPatientCopy.Pati_height = Int32.Parse(txbPati_Height.Text);
        }

        private void txbPati_Remarks_TextChanged(object sender, TextChangedEventArgs e)
        {
            _selectedPatientCopy.Pati_remarks = txbPati_Remarks.Text;
        }
        #endregion

        #region OTHER LISTENERS
        private void dtpPati_Birthdate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            /*
            _selectedPatientCopy.Pers_birthdate = dtpPati_Birthdate.Date.Value;
            */
        }

        private void cboPati_Gender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            foreach (GenderTypeDB gender in Enum.GetValues(typeof(GenderTypeDB)))
            {
                string key = gender.ToString();
                int value = (int)gender;

                if (cboPati_Gender.SelectedValue.ToString() == key)
                {
                    _selectedPatientCopy.Pers_gender = value;
                }
            }
            */
        }
        #endregion
    }
}
