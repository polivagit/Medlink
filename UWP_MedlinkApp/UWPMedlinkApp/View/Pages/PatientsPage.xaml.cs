using DbLibrary.DB;
using DbLibrary.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWPMedlinkApp.View.Dialogs;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
        private static PatientDB _newPatient = new PatientDB();
        private static PersonDB _caregiverOfPatient = new PersonDB();

        private static bool isNewPatient = true;

        public PatientsPage()
        {
            this.InitializeComponent();
        }

        private void Patients_Loaded(object sender, RoutedEventArgs e)
        {
            MainPage.NavigationViewItemIsEnabled("nviPatientsPage", true);
            MainPage.NavigationViewItemIsEnabled("nviTreatmentsPage", false);
            MainPage.NavigationViewItemIsEnabled("nviMedicinesPage", false);

            _activeDoctor = DoctorDB._currentDoctor;

            LoadActiveDoctorInfo();
            dtgPatients.ItemsSource = PatientDB.GetAllPatients("");

            SetInitialButtonDisplay();

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

                SetButtonEnabled("btnUpdatePatient", true);
                SetButtonEnabled("btnRemovePatient", true);

                _selectedPatientCopy = _selectedPatient;

                isNewPatient = false;
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
            isNewPatient = true;
            btnAddPatient.Visibility = Visibility.Visible;
            btnUpdatePatient.Visibility = Visibility.Collapsed;

            SetInitialButtonDisplay();

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
            _caregiverOfPatient = new PersonDB();
            txbPati_CaregiverName.Text = "NO CAREGIVER";
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
            txbPati_CaregiverName.Text = patientAux.Pers_CaregiverName;

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
            txbPati_CaregiverName.Text = "";
            dtpPati_Birthdate.Date = new DateTimeOffset(new DateTime(1950, 01, 01));

            cboPati_Gender.SelectedIndex = -1;
        }
        #endregion

        #region CONTENTDIALOG METHODS
        private async Task LoadSeeDoctorDetailsDialog(DoctorDB doctorAux)
        {
            DoctorFormDialog seeDoctorDetailsDialog = new DoctorFormDialog(doctorAux);
            await seeDoctorDetailsDialog.ShowAsync();

            LoadActiveDoctorInfo();
        }

        private async Task LoadCaregiverAssignmentDialog()
        {
            CaregiverAssignmentDialog caregiverAssignmentDialog = new CaregiverAssignmentDialog();
            var res = await caregiverAssignmentDialog.ShowAsync();

            if (res == ContentDialogResult.Primary)
            {
                _caregiverOfPatient = caregiverAssignmentDialog._selectedCaregiver;
                txbPati_CaregiverName.Text = _caregiverOfPatient.Pers_FullName;
            }
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

            // Modify the dialog's style
            crudPatientDialog.Style = (Style)Application.Current.Resources["CRUDConfirmationDialogStyle"];

            ContentDialogResult result = await crudPatientDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // OK
                switch (action)
                {
                    case "ADD":
                    {
                            // INSERT PATIENT
                            SavePatientInfo();
                            int insertedId = PatientDB.InsertPatient(_newPatient);

                            dtgPatients.ItemsSource = PatientDB.GetAllPatients("");
                            ClearPatientInfo();
                            SetInitialButtonDisplay();
                            break;
                    }
                    case "UPDATE":
                    {
                            // UPDATE PATIENT
                            SavePatientInfo();
                            PatientDB.UpdatePatient(_selectedPatientCopy);

                            dtgPatients.ItemsSource = PatientDB.GetAllPatients("");
                            ClearPatientInfo();
                            SetInitialButtonDisplay();
                            break;
                    }
                    case "REMOVE":
                    {
                            // DELETE PATIENT
                            PatientDB.DeletePatientById(_selectedPatient.Pers_id);

                            dtgPatients.ItemsSource = PatientDB.GetAllPatients("");
                            ClearPatientInfo();
                            SetInitialButtonDisplay();
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
        bool isValidNif = false;
        bool isValidFirstName = false;
        bool isValidLastName1 = false;
        bool isValidLastName2 = false;
        bool isValidBirthdate = false;
        bool isValidPhoneNumber = false;
        bool isValidEmail = false;
        bool isValidGender = false;
        bool isValidStreet = false;
        bool isValidZipCode = false;
        bool isValidCity = false;
        bool isValidProvince = false;
        bool isValidCountry = false;
        bool isValidUsername = false;
        bool isValidHeight = false;
        bool isValidWeight = false;

        private void txbPatientFilterByFullName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadUIElements();

            dtgPatients.ItemsSource = PatientDB.GetAllPatients(txbPatientFilterByFullName.Text);
        }

        private void txbPati_FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonDB.IsValidFirstName(txbPati_FirstName.Text))
            {
                txbPati_FirstName.Background = new SolidColorBrush(Colors.Transparent);
                isValidFirstName = true;
            }
            else
            {
                txbPati_FirstName.Background = new SolidColorBrush(Colors.IndianRed);
                isValidFirstName = false;
            }
            FieldValidationCheck();
        }

        private void txbPati_LastName1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonDB.IsValidLastName1(txbPati_LastName1.Text))
            {
                txbPati_LastName1.Background = new SolidColorBrush(Colors.Transparent);
                isValidLastName1 = true;
            }
            else
            {
                txbPati_LastName1.Background = new SolidColorBrush(Colors.IndianRed);
                isValidLastName1 = false;
            }
            FieldValidationCheck();
        }

        private void txbPati_LastName2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonDB.IsValidLastName2(txbPati_LastName2.Text))
            {
                txbPati_LastName2.Background = new SolidColorBrush(Colors.Transparent);
                isValidLastName2 = true;
            }
            else
            {
                txbPati_LastName2.Background = new SolidColorBrush(Colors.IndianRed);
                isValidLastName2 = false;
            }
            FieldValidationCheck();
        }

        private void txbPati_Nif_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonDB.IsValidNIF(txbPati_Nif.Text))
            {
                txbPati_Nif.Background = new SolidColorBrush(Colors.Transparent);
                isValidNif = true;
            }
            else
            {
                txbPati_Nif.Background = new SolidColorBrush(Colors.IndianRed);
                isValidNif = false;
            }
            FieldValidationCheck();
        }

        private void txbPati_PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonDB.isValidPhoneNumber(txbPati_PhoneNumber.Text))
            {
                txbPati_PhoneNumber.Background = new SolidColorBrush(Colors.Transparent);
                isValidPhoneNumber = true;
            }
            else
            {
                txbPati_PhoneNumber.Background = new SolidColorBrush(Colors.IndianRed);
                isValidPhoneNumber = false;
            }
            FieldValidationCheck();
        }

        private void txbPati_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonDB.isValidEmail(txbPati_Email.Text))
            {
                txbPati_Email.Background = new SolidColorBrush(Colors.Transparent);
                isValidEmail = true;
            }
            else
            {
                txbPati_Email.Background = new SolidColorBrush(Colors.IndianRed);
                isValidEmail = false;
            }
            FieldValidationCheck();
        }

        private void txbPati_Street_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonDB.isValidStreet(txbPati_Street.Text))
            {
                txbPati_Street.Background = new SolidColorBrush(Colors.Transparent);
                isValidStreet = true;
            }
            else
            {
                txbPati_Street.Background = new SolidColorBrush(Colors.IndianRed);
                isValidStreet = false;
            }
            FieldValidationCheck();
        }

        private void txbPati_PostalCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonDB.isValidAddrsZipCode(txbPati_PostalCode.Text))
            {
                txbPati_PostalCode.Background = new SolidColorBrush(Colors.Transparent);
                isValidZipCode = true;
            }
            else
            {
                txbPati_PostalCode.Background = new SolidColorBrush(Colors.IndianRed);
                isValidZipCode = false;
            }
            FieldValidationCheck();
        }

        private void txbPati_City_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonDB.isValidAddrsCity(txbPati_City.Text))
            {
                txbPati_City.Background = new SolidColorBrush(Colors.Transparent);
                isValidCity = true;
            }
            else
            {
                txbPati_City.Background = new SolidColorBrush(Colors.IndianRed);
                isValidCity = false;
            }
            FieldValidationCheck();
        }

        private void txbPati_Province_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonDB.isValidAddrsProvince(txbPati_Province.Text))
            {
                txbPati_Province.Background = new SolidColorBrush(Colors.Transparent);
                isValidProvince = true;
            }
            else
            {
                txbPati_Province.Background = new SolidColorBrush(Colors.IndianRed);
                isValidProvince = false;
            }
            FieldValidationCheck();
        }

        private void txbPati_Country_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonDB.isValidAddrsCountry(txbPati_Country.Text))
            {
                txbPati_Country.Background = new SolidColorBrush(Colors.Transparent);
                isValidCountry = true;
            }
            else
            {
                txbPati_Country.Background = new SolidColorBrush(Colors.IndianRed);
                isValidCountry = false;
            }
            FieldValidationCheck();
        }

        private void txbPati_Username_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PersonDB.isValidLoginUsername(txbPati_Username.Text))
            {
                txbPati_Username.Background = new SolidColorBrush(Colors.Transparent);
                isValidUsername = true;
            }
            else
            {
                txbPati_Username.Background = new SolidColorBrush(Colors.IndianRed);
                isValidUsername = false;
            }
            FieldValidationCheck();
        }

        private void txbPati_Weight_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (PatientDB.isValidWeight((float.Parse(txbPati_Weight.Text, CultureInfo.InvariantCulture))))
                {
                    txbPati_Weight.Background = new SolidColorBrush(Colors.Transparent);
                    isValidWeight = true;
                }
                else
                {
                    //No aconsegueixo controlar aixo, aixi que ho poso a true
                    txbPati_Weight.Background = new SolidColorBrush(Colors.Transparent);
                    isValidWeight = true;
                }
            }
            catch (Exception ex)
            {
                
            }
            FieldValidationCheck();
        }

        private void txbPati_Height_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int result = Int32.Parse(txbPati_Height.Text + "");

                if (PatientDB.isValidHeight(result))
                {
                    txbPati_Height.Background = new SolidColorBrush(Colors.Transparent);
                    isValidHeight = true;
                }
                else
                {
                    txbPati_Height.Background = new SolidColorBrush(Colors.IndianRed);
                    isValidHeight = false;
                }
            }
            catch (Exception ex)
            {

            }
            FieldValidationCheck();
        }

        private void txbPati_Remarks_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
            if ()
            {
                txbPati_Nif.Background = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                txbPati_Nif.Background = new SolidColorBrush(Colors.IndianRed);
            }
            */

            FieldValidationCheck();
        }
        #endregion

        #region OTHER LISTENERS
        private void dtpPati_Birthdate_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if (PersonDB.IsValidBirthdate(dtpPati_Birthdate.Date.Value.DateTime))
            {
                dtpPati_Birthdate.Foreground = new SolidColorBrush(Colors.Black);
                isValidBirthdate = true;
            }
            else
            {
                dtpPati_Birthdate.Foreground = new SolidColorBrush(Colors.IndianRed);
                isValidBirthdate = false;
            }

            FieldValidationCheck();
        }

        private void cboPati_Gender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboPati_Gender.SelectedItem != null)
            {
                cboPati_Gender.Background = new SolidColorBrush(Colors.Transparent);
                isValidGender = true;
            }
            else
            {
                cboPati_Gender.Background = new SolidColorBrush(Colors.IndianRed);
                isValidGender = false;
            }

            FieldValidationCheck();
        }
        #endregion

        #region OTHER METHODS
        private void SavePatientInfo()
        {
            if (isNewPatient)
            {
                _newPatient.Pers_first_name = txbPati_FirstName.Text;
                _newPatient.Pers_last_name_1 = txbPati_LastName1.Text;
                _newPatient.Pers_last_name_2 = txbPati_LastName2.Text;
                _newPatient.Pers_nif = txbPati_Nif.Text;
                _newPatient.Pers_phone_number = txbPati_PhoneNumber.Text;
                _newPatient.Pers_email = txbPati_Email.Text;

                DateTime auxBirthdate = dtpPati_Birthdate.Date.Value.DateTime;
                _newPatient.Pers_birthdate = auxBirthdate;

                foreach (GenderTypeDB gender in Enum.GetValues(typeof(GenderTypeDB)))
                {
                    string key = gender.ToString();
                    int value = (int)gender;

                    if (cboPati_Gender.SelectedValue.ToString() == key)
                    {
                        _newPatient.Pers_gender = (GenderTypeDB)value;
                    }
                }

                _newPatient.Pati_caregiver_id = _caregiverOfPatient.Pers_id;

                _newPatient.Pers_addrs_street = txbPati_Street.Text;
                _newPatient.Pers_addrs_zip_code = txbPati_PostalCode.Text;
                _newPatient.Pers_addrs_city = txbPati_City.Text;
                _newPatient.Pers_addrs_province = txbPati_Province.Text;
                _newPatient.Pers_addrs_country = txbPati_Country.Text;
                _newPatient.Pers_login_username = txbPati_Username.Text;
                _newPatient.Pers_login_password = DBUtils.GenerateUniqueHexString(20);
                _newPatient.Pati_weight = (float)Convert.ToDouble(txbPati_Weight.Text);
                _newPatient.Pati_height = int.Parse(txbPati_Height.Text);
                _newPatient.Pati_remarks = txbPati_Remarks.Text;
            }
            else
            {
                _selectedPatientCopy.Pers_first_name = txbPati_FirstName.Text;
                _selectedPatientCopy.Pers_last_name_1 = txbPati_LastName1.Text;
                _selectedPatientCopy.Pers_last_name_2 = txbPati_LastName2.Text;
                _selectedPatientCopy.Pers_nif = txbPati_Nif.Text;
                _selectedPatientCopy.Pers_phone_number = txbPati_PhoneNumber.Text;
                _selectedPatientCopy.Pers_email = txbPati_Email.Text;

                DateTime auxBirthdate = dtpPati_Birthdate.Date.Value.DateTime;
                _selectedPatientCopy.Pers_birthdate = auxBirthdate;

                foreach (GenderTypeDB gender in Enum.GetValues(typeof(GenderTypeDB)))
                {
                    string key = gender.ToString();
                    int value = (int)gender;

                    if (cboPati_Gender.SelectedValue.ToString() == key)
                    {
                        _selectedPatientCopy.Pers_gender = (GenderTypeDB)value;
                    }
                }

                _selectedPatientCopy.Pati_caregiver_id = _caregiverOfPatient.Pers_id;

                _selectedPatientCopy.Pers_addrs_street = txbPati_Street.Text;
                _selectedPatientCopy.Pers_addrs_zip_code = txbPati_PostalCode.Text;
                _selectedPatientCopy.Pers_addrs_city = txbPati_City.Text;
                _selectedPatientCopy.Pers_addrs_province = txbPati_Province.Text;
                _selectedPatientCopy.Pers_addrs_country = txbPati_Country.Text;
                _selectedPatientCopy.Pers_login_username = txbPati_Username.Text;
                _selectedPatientCopy.Pati_weight = (float)Convert.ToDouble(txbPati_Weight.Text);
                _selectedPatientCopy.Pati_height = int.Parse(txbPati_Height.Text);
                _selectedPatientCopy.Pati_remarks = txbPati_Remarks.Text;
            }
        }
        #endregion

        private void Patients_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void SetInitialButtonDisplay()
        {
            SetButtonEnabled("btnAddPatient", false);
            SetButtonEnabled("btnUpdatePatient", false);
            SetButtonEnabled("btnRemovePatient", false);
        }

        public void SetButtonEnabled(string buttonName, bool isEnabled)
        {
            Button button = FindButtonInVisualTree(buttonName);

            if (button != null)
            {
                button.IsEnabled = isEnabled;
            }
        }

        private Button FindButtonInVisualTree(string buttonName)
        {
            var rootFrame = Window.Current.Content as Frame;
            var page = rootFrame.Content as MainPage;

            Button button = FindButtonByName(page, buttonName);
            return button;
        }

        private Button FindButtonByName(DependencyObject parent, string buttonName)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is Button button && button.Name == buttonName)
                {
                    return button;
                }

                var result = FindButtonByName(child, buttonName);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        private void FieldValidationCheck()
        {
                if (isValidNif
                && isValidFirstName
                && isValidLastName1
                && isValidLastName2
                && isValidBirthdate
                && isValidPhoneNumber
                && isValidEmail
                && isValidGender
                && isValidStreet
                && isValidZipCode
                && isValidCity
                && isValidProvince
                && isValidCountry
                && isValidUsername
                && isValidHeight
                && isValidWeight)
            {

                if (isNewPatient)
                {
                    SetButtonEnabled("btnAddPatient", true);
                    SetButtonEnabled("btnUpdatePatient", false);
                }
                else
                {
                    SetButtonEnabled("btnAddPatient", false);
                    SetButtonEnabled("btnUpdatePatient", true);
                }
            }
            else
            {
                if (isNewPatient)
                {
                    SetButtonEnabled("btnAddPatient", false);
                }
                else
                {
                    SetButtonEnabled("btnUpdatePatient", false);
                }
            }
        }

        /*
        private void txbPati_Weight_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        */

        private void txbPati_Height_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void txbPati_PostalCode_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void txbPati_PhoneNumber_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
    }
}
