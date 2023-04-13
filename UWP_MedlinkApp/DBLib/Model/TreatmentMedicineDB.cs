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
        private float trme_quantity_per_day;
        private float trme_total_quantity;

        public TreatmentMedicineDB()
        {
        }

        public TreatmentMedicineDB(int trme_treatment_id,
            int trme_medicine_id, float trme_quantity_per_day, 
            float trme_total_quantity)
        {
            this.Trme_treatment_id = trme_treatment_id;
            this.Trme_medicine_id = trme_medicine_id;
            this.Trme_quantity_per_day = trme_quantity_per_day;
            this.Trme_total_quantity = trme_total_quantity;
        }

        public int Trme_treatment_id { get => trme_treatment_id; set => trme_treatment_id = value; }
        public int Trme_medicine_id { get => trme_medicine_id; set => trme_medicine_id = value; }
        public float Trme_quantity_per_day { get => trme_quantity_per_day; set => trme_quantity_per_day = value; }
        public float Trme_total_quantity { get => trme_total_quantity; set => trme_total_quantity = value; }

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
