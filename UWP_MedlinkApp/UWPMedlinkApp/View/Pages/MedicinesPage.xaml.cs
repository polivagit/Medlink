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
        private static TreatmentMedicineDB _newTreatmentMedicine = new TreatmentMedicineDB();

        private static bool isNewTreatmentMedicine = true;

        public MedicinesPage()
        {
            this.InitializeComponent();
        }

        private void Medicines_Loaded(object sender, RoutedEventArgs e)
        {
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
            MedicineDB selectedNewMedicine = (MedicineDB)dtgAllMedicines.SelectedItem;
            if (selectedNewMedicine != null)
            {
                txbMedi_Name.Text = selectedNewMedicine.Medi_name;
            }

            ClearTreatmentMedicineInfo();

            isNewTreatmentMedicine = false;
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
            }

            isNewTreatmentMedicine = false;
            btnAddMedicine.Visibility = Visibility.Collapsed;
            btnUpdateMedicine.Visibility = Visibility.Visible;
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

            ClearTreatmentMedicineInfo();
        }

        private void btnAddMedicine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateMedicine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveMedicine_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region TEXT LISTENERS
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

        }

        private void dtpMedi_DateStart_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {

        }

        private void cboMedi_UOM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
        }

        private void ClearTreatmentMedicineInfo()
        {
            dtgAllMedicines.SelectedItem = null;

            txbMedi_Name.Text = "";
            txbMedi_TotalQuantity.Text = "";

            dtpMedi_DateStart.Date = new DateTimeOffset(DateTime.Now);
            dtpMedi_DateEnd.Date = new DateTimeOffset(DateTime.Now.AddDays(1));

            cboMedi_UOM.SelectedItem = null;
        }
        #endregion

        #region CONTENT DIALOG METHODS
        private async Task LoadSeeDoctorDetailsDialog(DoctorDB doctorAux)
        {
            DoctorFormDialog seeDoctorDetailsDialog = new DoctorFormDialog(doctorAux);
            await seeDoctorDetailsDialog.ShowAsync();
        }
        #endregion
    }
}
