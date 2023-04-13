using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib.Model
{
    public class SpecialtyDB
    {
        private int spec_id;
        private String spec_name;

        public SpecialtyDB()
        {
        }

        public SpecialtyDB(int spec_id, string spec_name)
        {
            this.Spec_id = spec_id;
            this.Spec_name = spec_name;
        }

        public int Spec_id { get => spec_id; set => spec_id = value; }
        public string Spec_name { get => spec_name; set => spec_name = value; }

        public override bool Equals(object obj)
        {
            return obj is SpecialtyDB specialty &&
                   spec_id == specialty.spec_id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(spec_id);
        }
    }
}
