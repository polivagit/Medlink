using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Model
{
    public class MedicineCategoryDB
    {
        private int meca_id;
        private String meca_name;

        public MedicineCategoryDB()
        {
        }

        public MedicineCategoryDB(int meca_id, string meca_name)
        {
            this.Meca_id = meca_id;
            this.Meca_name = meca_name;
        }

        public int Meca_id { get => meca_id; set => meca_id = value; }
        public string Meca_name { get => meca_name; set => meca_name = value; }

        public override bool Equals(object obj)
        {
            return obj is MedicineCategoryDB dB &&
                   meca_id == dB.meca_id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(meca_id);
        }
    }
}
