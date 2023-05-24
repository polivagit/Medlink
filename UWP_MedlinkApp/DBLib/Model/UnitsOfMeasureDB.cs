using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Model
{
    public class UnitsOfMeasureDB
    {
        private int unme_id;
        private String unme_name;
        private String unme_abbreviation;

        public UnitsOfMeasureDB()
        {

        }

        public UnitsOfMeasureDB(int unme_id, string unme_name, string unme_abbreviation)
        {
            this.Unme_id = unme_id;
            this.Unme_name = unme_name;
            this.Unme_abbreviation = unme_abbreviation;
        }

        public int Unme_id { get => unme_id; set => unme_id = value; }
        public string Unme_name { get => unme_name; set => unme_name = value; }
        public string Unme_abbreviation { get => unme_abbreviation; set => unme_abbreviation = value; }

        public override bool Equals(object obj)
        {
            return obj is UnitsOfMeasureDB dB &&
                   unme_id == dB.unme_id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(unme_id);
        }
    }
}
