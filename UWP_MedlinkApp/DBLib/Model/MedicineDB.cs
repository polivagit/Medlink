using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Model
{
    public class MedicineDB
    {
        private int medi_id;
        private String medi_name;
        private int medi_category_id;

        public MedicineDB()
        {
        }

        public MedicineDB(int medi_id, string medi_name, int medi_category_id)
        {
            this.Medi_id = medi_id;
            this.Medi_name = medi_name;
            this.Medi_category_id = medi_category_id;
        }

        public int Medi_id { get => medi_id; set => medi_id = value; }
        public string Medi_name { get => medi_name; set => medi_name = value; }
        public int Medi_category_id { get => medi_category_id; set => medi_category_id = value; }

        public override bool Equals(object obj)
        {
            return obj is MedicineDB dB &&
                   medi_id == dB.medi_id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(medi_id);
        }
    }
}
