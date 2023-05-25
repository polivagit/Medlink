using DbLibrary.Model;
using System;
using System.Collections.Generic;
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
    public sealed partial class TreatmentsPage : Page
    {
        public static DoctorDB _activeDoctor = PatientsPage._activeDoctor;
        public static PatientDB _selectedPatient = PatientsPage._selectedPatient;
        public static TreatmentDB _selectedTreatment = new TreatmentDB();
        private static TreatmentDB _selectedTreatmentCopy = new TreatmentDB();
        private static TreatmentDB _newTreatment = new TreatmentDB();

        private static bool isNewTreatment = true;

        public TreatmentsPage()
        {
            this.InitializeComponent();
        }

        private void Treatments_Loaded(object sender, RoutedEventArgs e)
        {
            MainPage.NavigationViewItemIsEnabled("nviPatientsPage", true);
            MainPage.NavigationViewItemIsEnabled("nviTreatmentsPage", true);
            MainPage.NavigationViewItemIsEnabled("nviMedicinesPage", false);

            LoadActiveDoctorInfo();

            _activeDoctor = PatientsPage._activeDoctor;
            _selectedPatient = PatientsPage._selectedPatient;
            _selectedTreatment = new TreatmentDB();

            if (_selectedPatient != null)
            {
                dtgTreatments.ItemsSource = TreatmentDB.GetAllTreatmentsByPatientAndDoctorId("", _selectedPatient.Pers_id, _activeDoctor.Pers_id);
            }

            SetInitialButtonDisplay();
        }

        #region DATAGRID LISTENERS
        private void dtgTreatments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReloadUIElements();

            _selectedTreatment = new TreatmentDB();
            _selectedTreatment = (TreatmentDB)dtgTreatments.SelectedItem;

            if (_selectedTreatment != null)
            {
                dtgMedicines.ItemsSource = TreatmentMedicineDB.GetAllTretmentMedicinesByTreatmentId(_selectedTreatment.Trea_id);

                _selectedTreatmentCopy = _selectedTreatment;

                LoadSelectedTreatmentInfo(_selectedTreatment);

                SetButtonEnabled("btnUpdatePatient", true);
                SetButtonEnabled("btnRemovePatient", true);

                isNewTreatment = false;
                btnAddTreatment.Visibility = Visibility.Collapsed;
                btnUpdateTreatment.Visibility = Visibility.Visible;
            }
        }

        private void dtgMedicines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region BUTTON LISTENERS
        private void btnClearTreatmentFilterByName_Click(object sender, RoutedEventArgs e)
        {
            txbTreatmentFilterByName.Text = "";
        }

        private void btnGoToMedicinesDetails_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MedicinesPage));
        }

        private void btnNewTreatment_Click(object sender, RoutedEventArgs e)
        {
            isNewTreatment = true;
            btnAddTreatment.Visibility = Visibility.Visible;
            btnUpdateTreatment.Visibility = Visibility.Collapsed;

            SetInitialButtonDisplay();

            ClearTreatmentInfo();
        }

        private void btnAddTreatment_Click(object sender, RoutedEventArgs e)
        {
            DisplayTreatmentConfirmationDialog("ADD");
        }

        private void btnUpdateTreatment_Click(object sender, RoutedEventArgs e)
        {
            DisplayTreatmentConfirmationDialog("UPDATE");
        }

        private void btnRemoveTreatment_Click(object sender, RoutedEventArgs e)
        {
            DisplayTreatmentConfirmationDialog("REMOVE");
        }

        private void btnSeeDoctorDetails_Click(object sender, RoutedEventArgs e)
        {
            LoadSeeDoctorDetailsDialog(_activeDoctor);
        }
        #endregion

        #region UI METHODS
        private void ReloadUIElements()
        {
            if (dtgTreatments.SelectedItem != null)
            {
                btnGoToMedicinesDetails.IsEnabled = true;
            }
            else
            {
                btnGoToMedicinesDetails.IsEnabled = false;
            }

            if (txbTreatmentFilterByName.Text.Length > 0)
            {
                btnClearTreatmentFilterByName.IsEnabled = true;
            }
            else
            {
                btnClearTreatmentFilterByName.IsEnabled = false;
            }
        }

        private void LoadActiveDoctorInfo()
        {
            txbDoct_FullName.Text = _activeDoctor.Pers_FullName;
            txbDoct_Specialty.Text = _activeDoctor.Pers_DoctorSpecialtyName;
        }

        private void LoadSelectedTreatmentInfo(TreatmentDB treatmentAux)
        {
            txbTrea_Name.Text = treatmentAux.Trea_name;
            txbTrea_Description.Text = treatmentAux.Trea_description;
            txbTrea_Observations.Text = treatmentAux.Trea_observations + "";

            dtpTrea_DateStart.Date = treatmentAux.Trea_date_start;
            dtpTrea_DateEnd.Date = treatmentAux.Trea_date_end;

            if (treatmentAux.Trea_is_active)
            {
                chkTrea_IsActive.IsChecked = true;
            }
            else
            {
                chkTrea_IsActive.IsChecked = false;
            }
        }

        private void ClearTreatmentInfo()
        {
            dtgTreatments.SelectedItem = null;
            dtgMedicines.ItemsSource = null;

            txbTrea_Name.Text = "";
            txbTrea_Description.Text = "";
            txbTrea_Observations.Text = "";

            dtpTrea_DateStart.Date = new DateTimeOffset(DateTime.Now);
            dtpTrea_DateEnd.Date = new DateTimeOffset(DateTime.Now.AddDays(1));

            chkTrea_IsActive.IsChecked = true;
        }
        #endregion

        #region CONTENTDIALOG METHODS
        private async Task LoadSeeDoctorDetailsDialog(DoctorDB doctorAux)
        {
            DoctorFormDialog seeDoctorDetailsDialog = new DoctorFormDialog(doctorAux);
            await seeDoctorDetailsDialog.ShowAsync();

            LoadActiveDoctorInfo();
        }

        public async Task DisplayTreatmentConfirmationDialog(string action)
        {
            TreatmentPage_TreatmentCRUDConfirmationDialog crudTreatmentDialog = new TreatmentPage_TreatmentCRUDConfirmationDialog()
            {
                Title = "CONFIRMATION: " + action + " TREATMENT",
                Content = "Do you really want to " + action + " the treatment?",
                PrimaryButtonText = "PROCEED",
                CloseButtonText = "CANCEL"
            };

            // Modify the dialog's style
            crudTreatmentDialog.Style = (Style)Application.Current.Resources["CRUDConfirmationDialogStyle"];

            ContentDialogResult result = await crudTreatmentDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // OK
                switch (action)
                {
                    case "ADD":
                        {
                            // INSERT TREATMENT
                            SaveTreatmentInfo();
                            int insertedId = TreatmentDB.InsertTreatment(_newTreatment);

                            isNewTreatment = true;
                            btnAddTreatment.Visibility = Visibility.Visible;
                            btnUpdateTreatment.Visibility = Visibility.Collapsed;

                            ClearTreatmentInfo();
                            dtgTreatments.ItemsSource = TreatmentDB.GetAllTreatmentsByPatientAndDoctorId("", _selectedPatient.Pers_id, _activeDoctor.Pers_id);
                            SetInitialButtonDisplay();
                            break;
                        }
                    case "UPDATE":
                        {
                            // UPDATE TREATMENT
                            SaveTreatmentInfo();
                            TreatmentDB.UpdateTreatment(_selectedTreatmentCopy);

                            isNewTreatment = true;
                            btnAddTreatment.Visibility = Visibility.Visible;
                            btnUpdateTreatment.Visibility = Visibility.Collapsed;

                            ClearTreatmentInfo();
                            dtgTreatments.ItemsSource = TreatmentDB.GetAllTreatmentsByPatientAndDoctorId("", _selectedPatient.Pers_id, _activeDoctor.Pers_id);
                            SetInitialButtonDisplay();
                            break;
                        }
                    case "REMOVE":
                        {
                            // DELETE TREATMENT

                            /*
                                TODO: DISABLE REMOVE BUTTON IF ISNEWTREATMENTMEDICINE 
                             */

                            TreatmentDB.DeleteTreatmentById(_selectedTreatment.Trea_id);

                            isNewTreatment = true;
                            btnAddTreatment.Visibility = Visibility.Visible;
                            btnUpdateTreatment.Visibility = Visibility.Collapsed;

                            ClearTreatmentInfo();
                            dtgTreatments.ItemsSource = TreatmentDB.GetAllTreatmentsByPatientAndDoctorId("", _selectedPatient.Pers_id, _activeDoctor.Pers_id);
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

        bool isValidName = false;
        bool isValidDescripition = false;
        bool isValidDateStart = false;
        bool isValidDateEnd = false;

        private void txbTreatmentFilterByName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadUIElements();

            dtgTreatments.ItemsSource =
                TreatmentDB.GetAllTreatmentsByPatientAndDoctorId(txbTreatmentFilterByName.Text,
                                                                    _selectedPatient.Pers_id,
                                                                    _activeDoctor.Pers_id);
        }

        private void txbTrea_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TreatmentDB.IsValidName(txbTrea_Name.Text))
            {
                txbTrea_Name.Background = new SolidColorBrush(Colors.Transparent);
                isValidName = true;
            }
            else
            {
                txbTrea_Name.Background = new SolidColorBrush(Colors.IndianRed);
                isValidName = false;
            }
            FieldValidationCheck();
        }

        private void txbTrea_Description_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TreatmentDB.isValidDescription(txbTrea_Description.Text))
            {
                txbTrea_Description.Background = new SolidColorBrush(Colors.Transparent);
                isValidDescripition = true;
            }
            else
            {
                txbTrea_Description.Background = new SolidColorBrush(Colors.IndianRed);
                isValidDescripition = false;
            }
            FieldValidationCheck();
        }

        private void txbTrea_Observations_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion

        #region OTHER LISTENERS
        private void chkTrea_IsActive_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chkTrea_IsActive_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        private void dtpTrea_DateStart_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            try
            {
                if (TreatmentDB.IsValidDateStart(dtpTrea_DateStart.Date.Value.DateTime))
                {
                    dtpTrea_DateStart.Background = new SolidColorBrush(Colors.Transparent);
                    isValidDateStart = true;
                }
                else
                {
                    dtpTrea_DateStart.Background = new SolidColorBrush(Colors.IndianRed);
                    isValidDateStart = false;
                }
            }
            catch (Exception ex)
            {

            }
            FieldValidationCheck();
        }

        private void dtpTrea_DateEnd_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            try
            {
                if (TreatmentDB.IsValidDateEnd(dtpTrea_DateEnd.Date.Value.DateTime))
                {
                    dtpTrea_DateEnd.Background = new SolidColorBrush(Colors.Transparent);
                    isValidDateEnd = true;
                }
                else
                {
                    dtpTrea_DateEnd.Background = new SolidColorBrush(Colors.IndianRed);
                    isValidDateEnd = false;
                }
            }
            catch (Exception ex)
            {

            }
            FieldValidationCheck();
        }
        #endregion

        #region OTHER METHODS
        private void SaveTreatmentInfo()
        {
            if (isNewTreatment)
            {
                _newTreatment.Trea_name = txbTrea_Name.Text;
                _newTreatment.Trea_description = txbTrea_Description.Text;
                _newTreatment.Trea_observations = txbTrea_Observations.Text;
                _newTreatment.Trea_is_active = chkTrea_IsActive.IsChecked.Value;
                _newTreatment.Trea_date_start = dtpTrea_DateStart.Date.Value.DateTime;
                _newTreatment.Trea_date_end = dtpTrea_DateEnd.Date.Value.DateTime;
                _newTreatment.Trea_doctor_id = _activeDoctor.Pers_id;
                _newTreatment.Trea_patient_id = _selectedPatient.Pers_id;
            }
            else
            {
                _selectedTreatmentCopy.Trea_name = txbTrea_Name.Text;
                _selectedTreatmentCopy.Trea_description = txbTrea_Description.Text;
                _selectedTreatmentCopy.Trea_observations = txbTrea_Observations.Text;
                _selectedTreatmentCopy.Trea_is_active = chkTrea_IsActive.IsChecked.Value;
                _selectedTreatmentCopy.Trea_date_start = dtpTrea_DateStart.Date.Value.DateTime;
                _selectedTreatmentCopy.Trea_date_end = dtpTrea_DateEnd.Date.Value.DateTime;
                _selectedTreatmentCopy.Trea_doctor_id = _activeDoctor.Pers_id;
                _selectedTreatmentCopy.Trea_patient_id = _selectedPatient.Pers_id;
            }
        }
        #endregion

        private void Treatments_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void FieldValidationCheck()
        {
           if (isValidName
                || isValidDescripition
                || isValidDateStart
                || isValidDateEnd)
            {

                if (isNewTreatment)
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
                if (isNewTreatment)
                {
                    SetButtonEnabled("btnAddPatient", false);
                }
                else
                {
                    SetButtonEnabled("btnUpdatePatient", false);
                }
            }
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
    }
}