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
    public sealed partial class MedicinesPage : Page
    {
        public static DoctorDB _activeDoctor = PatientsPage._activeDoctor;
        public static PatientDB _selectedPatient = PatientsPage._selectedPatient;
        public static TreatmentDB _selectedTreatment = new TreatmentDB();
        private static TreatmentMedicineDB _selectedTreatmentMedicine = new TreatmentMedicineDB();
        private static TreatmentMedicineDB _selectedTreatmentMedicineCopy = new TreatmentMedicineDB();
        private static TreatmentMedicineDB _newTreatmentMedicine = new TreatmentMedicineDB();

        private static bool isNewTreatmentMedicine = true;

        public MedicinesPage()
        {
            this.InitializeComponent();
        }

        private void Medicines_Loaded(object sender, RoutedEventArgs e)
        {
            MainPage.NavigationViewItemIsEnabled("nviMedicinesPage", true); 
            MainPage.NavigationViewItemIsEnabled("nviTreatmentsPage", true);

            SetInitialButtonDisplay();
            _selectedTreatment = TreatmentsPage._selectedTreatment;

            LoadActiveDoctorInfo();
            LoadCategoriesCombobox();
            LoadUOMsCombobox();

            dtgAllMedicines.ItemsSource = MedicineDB.GetAllMedicines("",0);
            dtgMedicinesOfTreatment.ItemsSource = TreatmentMedicineDB.GetAllTretmentMedicinesByTreatmentId(_selectedTreatment.Trea_id);
        }

        #region DATAGRID LISTENERS
        private void dtgAllMedicines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearTreatmentMedicineInfo();

            MedicineDB selectedNewMedicine = (MedicineDB)dtgAllMedicines.SelectedItem;
            if (selectedNewMedicine != null)
            {
                txbMedi_Name.Text = selectedNewMedicine.Medi_name;
            }

            isNewTreatmentMedicine = true;
            btnAddMedicine.Visibility = Visibility.Visible;
            btnUpdateMedicine.Visibility = Visibility.Collapsed;

            dtgMedicinesOfTreatment.SelectedItem = null;
        }

        private void dtgMedicinesOfTreatment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearTreatmentMedicineInfo();

            _selectedTreatmentMedicine = (TreatmentMedicineDB)dtgMedicinesOfTreatment.SelectedItem;

            if (_selectedTreatmentMedicine != null)
            {
                LoadSelectedMedicineInfo(_selectedTreatmentMedicine);

                _selectedTreatmentMedicineCopy = _selectedTreatmentMedicine;

                isNewTreatmentMedicine = false;
                btnAddMedicine.Visibility = Visibility.Collapsed;
                btnUpdateMedicine.Visibility = Visibility.Visible;

                SetButtonEnabled("btnRemoveMedicine", true);
            }
            else
            {
                SetButtonEnabled("btnRemoveMedicine", false);
            }
        }
        #endregion

        #region BUTTON LISTENERS
        private void btnClearMedicineFilter_Click(object sender, RoutedEventArgs e)
        {
            txbMedicineFilterByName.Text = "";
            cboMedicineFilterByCategory.SelectedIndex = -1;
            cboMedicineFilterByCategory.SelectedItem = null;
        }

        private void btnSeeDoctorDetails_Click(object sender, RoutedEventArgs e)
        {
            LoadSeeDoctorDetailsDialog(_activeDoctor);
        }

        private void btnNewMedicine_Click(object sender, RoutedEventArgs e)
        {
            isNewTreatmentMedicine = true;
            btnAddMedicine.Visibility = Visibility.Visible;
            btnUpdateMedicine.Visibility = Visibility.Collapsed;

            dtgAllMedicines.SelectedItem = null;

            ClearTreatmentMedicineInfo();
        }

        private void btnAddMedicine_Click(object sender, RoutedEventArgs e)
        {
            DisplayTreatmentMedicineConfirmationDialog("ADD");
        }

        private void btnUpdateMedicine_Click(object sender, RoutedEventArgs e)
        {
            DisplayTreatmentMedicineConfirmationDialog("UPDATE");
        }

        private void btnRemoveMedicine_Click(object sender, RoutedEventArgs e)
        {
            DisplayTreatmentMedicineConfirmationDialog("REMOVE");
        }
        #endregion

        #region TEXT LISTENERS
        bool isValidQuantity = false;
        bool isValidDateStart = false;
        bool isValidDateEnd = false;
        bool isValidUOM = false;
        bool isValidFrequency = false;

        private void txbMedicineFilterByName_TextChanged(object sender, TextChangedEventArgs e)
        {
            ReloadUIElements();

            MedicineCategoryDB aux = new MedicineCategoryDB();
            aux = (MedicineCategoryDB)cboMedicineFilterByCategory.SelectedItem;

            if (aux != null)
            {
                dtgAllMedicines.ItemsSource = MedicineDB.GetAllMedicines(txbMedicineFilterByName.Text, aux.Meca_id);
            }
            else
            {
                dtgAllMedicines.ItemsSource = MedicineDB.GetAllMedicines(txbMedicineFilterByName.Text, 0);
            }
        }

        private void txbMedi_TotalQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (float.TryParse(txbMedi_TotalQuantity.Text, out float quantity))
            {
                if (TreatmentMedicineDB.IsValidTotalQuantity(quantity))
                {
                    txbMedi_TotalQuantity.Background = new SolidColorBrush(Colors.Transparent);
                    isValidQuantity = true;
                }
                else
                {
                    txbMedi_TotalQuantity.Background = new SolidColorBrush(Colors.IndianRed);
                    isValidQuantity = false;
                }
            }
            else
            {
                txbMedi_TotalQuantity.Background = new SolidColorBrush(Colors.IndianRed);
                isValidQuantity = false;
            }
            FieldValidationCheck();
        }

        private void txbMedi_Frequency_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TreatmentMedicineDB.IsValidFrequency(txbMedi_Frequency.Text))
            {
                txbMedi_Frequency.Background = new SolidColorBrush(Colors.Transparent);
                isValidFrequency = true;
            }
            else
            {
                txbMedi_Frequency.Background = new SolidColorBrush(Colors.IndianRed);
                isValidFrequency = false;
            }
            FieldValidationCheck();
        }

        private void txbMedi_TotalQuantity_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
        #endregion

        #region OTHER LISTENERS
        private void cboMedicineFilterByCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReloadUIElements();

            MedicineCategoryDB aux = new MedicineCategoryDB();
            aux = (MedicineCategoryDB)cboMedicineFilterByCategory.SelectedItem;

            if (aux != null)
            {
                dtgAllMedicines.ItemsSource = MedicineDB.GetAllMedicines(txbMedicineFilterByName.Text, aux.Meca_id);
            }
            else
            {
                dtgAllMedicines.ItemsSource = MedicineDB.GetAllMedicines(txbMedicineFilterByName.Text, 0);
            }
        }

        private void dtpMedi_DateEnd_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if ((dtpMedi_DateEnd.Date.Value.DateTime >= _selectedTreatment.Trea_date_start)
                && (dtpMedi_DateEnd.Date.Value.DateTime <= _selectedTreatment.Trea_date_end))
            {
                if (dtpMedi_DateStart.Date.HasValue)
                {
                    if (dtpMedi_DateEnd.Date.Value.DateTime > dtpMedi_DateStart.Date.Value.DateTime)
                    {
                        dtpMedi_DateEnd.Background = new SolidColorBrush(Colors.Transparent);
                        isValidDateEnd = true;
                    }
                    else
                    {
                        dtpMedi_DateEnd.Background = new SolidColorBrush(Colors.IndianRed);
                        isValidDateEnd = false;
                    }
                }
                else
                {
                    dtpMedi_DateEnd.Background = new SolidColorBrush(Colors.IndianRed);
                    isValidDateEnd = false;
                }
            }
            else
            {
                dtpMedi_DateEnd.Background = new SolidColorBrush(Colors.IndianRed);
                isValidDateEnd = false;
            }
            FieldValidationCheck();
        }

        private void dtpMedi_DateStart_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            if ((dtpMedi_DateStart.Date.Value.DateTime >= _selectedTreatment.Trea_date_start) 
                && (dtpMedi_DateStart.Date.Value.DateTime <= _selectedTreatment.Trea_date_end))
            {
                if (dtpMedi_DateEnd.Date.HasValue)
                {
                    if (dtpMedi_DateStart.Date.Value.DateTime < dtpMedi_DateEnd.Date.Value.DateTime)
                    {
                        dtpMedi_DateStart.Background = new SolidColorBrush(Colors.Transparent);
                        isValidDateStart = true;
                    }
                    else
                    {
                        dtpMedi_DateStart.Background = new SolidColorBrush(Colors.IndianRed);
                        isValidDateStart = false;
                    }
                }
                else
                {
                    dtpMedi_DateStart.Background = new SolidColorBrush(Colors.IndianRed);
                    isValidDateStart = false;
                }
            }
            else
            {
                dtpMedi_DateStart.Background = new SolidColorBrush(Colors.IndianRed);
                isValidDateStart = false;
            }
            FieldValidationCheck();
        }

        private void cboMedi_UOM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboMedi_UOM.SelectedItem != null)
            {
                cboMedi_UOM.Background = new SolidColorBrush(Colors.Transparent);
                isValidUOM = true;
            }
            else
            {
                cboMedi_UOM.Background = new SolidColorBrush(Colors.IndianRed);
                isValidUOM = false;
            }
            FieldValidationCheck();
        }

        private void FieldValidationCheck()
        {
            if (isValidQuantity
                && isValidFrequency
                && isValidUOM
                && isValidDateStart
                && isValidDateEnd)
            {

                if (isNewTreatmentMedicine)
                {
                    SetButtonEnabled("btnAddMedicine", true);
                    SetButtonEnabled("btnUpdateMedicine", false);
                }
                else
                {
                    SetButtonEnabled("btnAddMedicine", false);
                    SetButtonEnabled("btnUpdateMedicine", true);
                }
            }
            else
            {
                if (isNewTreatmentMedicine)
                {
                    SetButtonEnabled("btnAddMedicine", false);
                }
                else
                {
                    SetButtonEnabled("btnUpdateMedicine", false);
                }
            }
        }

        private void SetInitialButtonDisplay()
        {
            SetButtonEnabled("btnAddMedicine", false);
            SetButtonEnabled("btnUpdateTreatmentMedicine", false);
            SetButtonEnabled("btnRemoveMedicine", false);
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
        #endregion

        #region UI METHODS
        private void ReloadUIElements()
        {
            if (txbMedicineFilterByName.Text.Length > 0 || cboMedicineFilterByCategory.SelectedItem != null)
            {
                btnClearMedicineFilter.IsEnabled = true;
            }
            else
            {
                btnClearMedicineFilter.IsEnabled = false;
            }
        }
        private void LoadActiveDoctorInfo()
        {
            txbDoct_FullName.Text = _activeDoctor.Pers_FullName;
            txbDoct_Specialty.Text = _activeDoctor.Pers_DoctorSpecialtyName;
        }

        private void LoadCategoriesCombobox()
        {
            cboMedicineFilterByCategory.ItemsSource = MedicineCategoryDB.GetAllMedicineCategories();
            cboMedicineFilterByCategory.DisplayMemberPath = "Meca_name";
        }

        private void LoadUOMsCombobox()
        {
            cboMedi_UOM.ItemsSource = UnitsOfMeasureDB.GetAllUOMs();
            cboMedi_UOM.DisplayMemberPath = "Unme_name";
        }

        private void LoadSelectedMedicineInfo(TreatmentMedicineDB treatmentMedicineAux)
        {
            
            txbMedi_Name.Text = treatmentMedicineAux.Trme_MedicineName;
            txbMedi_TotalQuantity.Text = treatmentMedicineAux.Trme_total_quantity+"";
            
            txbMedi_Name.Text = treatmentMedicineAux.Trme_MedicineName;
            dtpMedi_DateStart.Date = treatmentMedicineAux.Trme_date_start;
            dtpMedi_DateEnd.Date = treatmentMedicineAux.Trme_date_end;

            UnitsOfMeasureDB uomAux = new UnitsOfMeasureDB();
            uomAux = UnitsOfMeasureDB.GetUOMById(treatmentMedicineAux.Trme_units_of_measure_id);
            cboMedi_UOM.SelectedItem = uomAux;

            txbMedi_Frequency.Text = treatmentMedicineAux.Trme_frequency;
        }

        private void ClearTreatmentMedicineInfo()
        {
            txbMedi_Name.Text = "";
            txbMedi_TotalQuantity.Text = "";

            dtpMedi_DateStart.Date = new DateTimeOffset(DateTime.Now);
            dtpMedi_DateEnd.Date = new DateTimeOffset(DateTime.Now.AddDays(1));

            cboMedi_UOM.SelectedItem = null;

            txbMedi_Frequency.Text = "";
        }

        private void SaveTreatmentMedicineInfo()
        {
            if (isNewTreatmentMedicine)
            {
                try
                {
                    _newTreatmentMedicine.Trme_treatment_id = _selectedTreatment.Trea_id;
                    MedicineDB auxMedicine = (MedicineDB)dtgAllMedicines.SelectedItem;
                    _newTreatmentMedicine.Trme_medicine_id = auxMedicine.Medi_id;

                    DateTime auxDateStart = dtpMedi_DateStart.Date.Value.DateTime;
                    _newTreatmentMedicine.Trme_date_start = auxDateStart;
                    DateTime auxDateEnd = dtpMedi_DateEnd.Date.Value.DateTime;
                    _newTreatmentMedicine.Trme_date_end = auxDateEnd;

                    float total_qty = (float)Convert.ToDouble(txbMedi_TotalQuantity.Text);
                    _newTreatmentMedicine.Trme_total_quantity = total_qty;
                    TimeSpan duration = auxDateEnd - auxDateStart;
                    float quantity_per_day = total_qty / (float)duration.TotalDays;
                    _newTreatmentMedicine.Trme_quantity_per_day = quantity_per_day;

                    UnitsOfMeasureDB auxUOM = (UnitsOfMeasureDB)cboMedi_UOM.SelectedItem;
                    _newTreatmentMedicine.Trme_units_of_measure_id = auxUOM.Unme_id;

                    _newTreatmentMedicine.Trme_frequency = txbMedi_Frequency.Text;
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                _selectedTreatmentMedicineCopy.Trme_treatment_id = _selectedTreatmentMedicine.Trme_treatment_id;
                _selectedTreatmentMedicineCopy.Trme_medicine_id = _selectedTreatmentMedicine.Trme_medicine_id;

                DateTime auxDateStart = dtpMedi_DateStart.Date.Value.DateTime;
                _selectedTreatmentMedicineCopy.Trme_date_start = auxDateStart;
                DateTime auxDateEnd = dtpMedi_DateEnd.Date.Value.DateTime;
                _selectedTreatmentMedicineCopy.Trme_date_end = auxDateEnd;

                float total_qty = (float)Convert.ToDouble(txbMedi_TotalQuantity.Text);
                _selectedTreatmentMedicineCopy.Trme_total_quantity = total_qty;
                TimeSpan duration = auxDateEnd - auxDateStart;
                float quantity_per_day = total_qty / (float)duration.TotalDays;
                _selectedTreatmentMedicineCopy.Trme_quantity_per_day = quantity_per_day;

                UnitsOfMeasureDB auxUOM = (UnitsOfMeasureDB)cboMedi_UOM.SelectedItem;
                _selectedTreatmentMedicineCopy.Trme_units_of_measure_id = auxUOM.Unme_id;

                _selectedTreatmentMedicineCopy.Trme_frequency = txbMedi_Frequency.Text;
            }
        }
        #endregion

        #region CONTENT DIALOG METHODS
        private async Task LoadSeeDoctorDetailsDialog(DoctorDB doctorAux)
        {
            DoctorFormDialog seeDoctorDetailsDialog = new DoctorFormDialog(doctorAux);
            await seeDoctorDetailsDialog.ShowAsync();

            LoadActiveDoctorInfo();
        }

        public async Task DisplayTreatmentMedicineConfirmationDialog(string action)
        {
            MedicinePage_TreatmentCRUDConfirmationDialog crudMedicineDialog = new MedicinePage_TreatmentCRUDConfirmationDialog()
            {
                Title = "CONFIRMATION: " + action + " MEDICINE",
                Content = "Do you really want to " + action + " the medicine?",
                PrimaryButtonText = "PROCEED",
                CloseButtonText = "CANCEL"
            };

            // Modify the dialog's style
            crudMedicineDialog.Style = (Style)Application.Current.Resources["CRUDConfirmationDialogStyle"];

            ContentDialogResult result = await crudMedicineDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                // OK
                switch (action)
                {
                    case "ADD":
                        {
                            // INSERT MEDICINE
                            SaveTreatmentMedicineInfo();
                            int insertedId = TreatmentMedicineDB.InsertTreatmentMedicine(_newTreatmentMedicine);

                            ClearTreatmentMedicineInfo();

                            isNewTreatmentMedicine = true;
                            btnAddMedicine.Visibility = Visibility.Visible;
                            btnUpdateMedicine.Visibility = Visibility.Collapsed;

                            dtgMedicinesOfTreatment.ItemsSource = TreatmentMedicineDB.GetAllTretmentMedicinesByTreatmentId(_selectedTreatment.Trea_id);
                            SetInitialButtonDisplay();
                            break;
                        }
                    case "UPDATE":
                        {
                            // UPDATE MEDICINE
                            SaveTreatmentMedicineInfo();
                            TreatmentMedicineDB.UpdateTreatmentMedicine(_selectedTreatmentMedicineCopy);

                            ClearTreatmentMedicineInfo();

                            isNewTreatmentMedicine = true;
                            btnAddMedicine.Visibility = Visibility.Visible;
                            btnUpdateMedicine.Visibility = Visibility.Collapsed;

                            dtgMedicinesOfTreatment.ItemsSource = TreatmentMedicineDB.GetAllTretmentMedicinesByTreatmentId(_selectedTreatment.Trea_id);
                            SetInitialButtonDisplay();
                            break;
                        }
                    case "REMOVE":
                        {
                            // DELETE MEDICINE

                            /*
                                TODO: DISABLE REMOVE BUTTON IF ISNEWTREATMENTMEDICINE 
                             */

                            TreatmentMedicineDB.DeleteTreatmentMedicineByTreatmentAndMedicineId(_selectedTreatment.Trea_id, _selectedTreatmentMedicine.Trme_medicine_id);

                            ClearTreatmentMedicineInfo();

                            isNewTreatmentMedicine = true;
                            btnAddMedicine.Visibility = Visibility.Visible;
                            btnUpdateMedicine.Visibility = Visibility.Collapsed;

                            dtgMedicinesOfTreatment.ItemsSource = TreatmentMedicineDB.GetAllTretmentMedicinesByTreatmentId(_selectedTreatment.Trea_id);
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

        private void Medicines_Unloaded(object sender, RoutedEventArgs e)
        {
            MainPage.NavigationViewItemIsEnabled("nviMedicinesPage", false);
        }

        
    }
}
