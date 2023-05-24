using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Model
{
    public class TreatmentMedicineDB
    {
        private int trme_treatment_id;
        private int trme_medicine_id;
        private DateTime trme_date_start;
        private DateTime trme_date_end;
        private float trme_quantity_per_day;
        private float trme_total_quantity;
        private int trme_units_of_measure_id;

        public TreatmentMedicineDB()
        {
        }

        public TreatmentMedicineDB(int trme_treatment_id, int trme_medicine_id, 
            DateTime trme_date_start, DateTime trme_date_end, float trme_quantity_per_day, 
            float trme_total_quantity, int trme_units_of_measure_id)
        {
            this.Trme_treatment_id = trme_treatment_id;
            this.Trme_medicine_id = trme_medicine_id;
            this.Trme_date_start = trme_date_start;
            this.Trme_date_end = trme_date_end;
            this.Trme_quantity_per_day = trme_quantity_per_day;
            this.Trme_total_quantity = trme_total_quantity;
            this.Trme_units_of_measure_id = trme_units_of_measure_id;
        }

        public int Trme_treatment_id { get => trme_treatment_id; set => trme_treatment_id = value; }
        public int Trme_medicine_id { get => trme_medicine_id; set => trme_medicine_id = value; }
        public DateTime Trme_date_start { get => trme_date_start; set => trme_date_start = value; }
        public DateTime Trme_date_end { get => trme_date_end; set => trme_date_end = value; }
        public float Trme_quantity_per_day { get => trme_quantity_per_day; set => trme_quantity_per_day = value; }
        public float Trme_total_quantity { get => trme_total_quantity; set => trme_total_quantity = value; }
        public int Trme_units_of_measure_id { get => trme_units_of_measure_id; set => trme_units_of_measure_id = value; }

        #region EXTRA_PROPERTIES
        public String Trme_MedicineName
        {
            get
            {
                return Trme_medicine_id + " - SAMPLE NAME";
            }
        }

        public String Trme_MedicineCategory
        {
            get
            {
                return Trme_medicine_id + " - SAMPLE CATEGORY";
            }
        }
        #endregion

        public override bool Equals(object obj)
        {
            return obj is TreatmentMedicineDB dB &&
                   trme_treatment_id == dB.trme_treatment_id &&
                   trme_medicine_id == dB.trme_medicine_id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(trme_treatment_id, trme_medicine_id);
        }
    }
}
