using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СЭД_ЭК.Entities
{
    public class Phase
    {
        public int id { get; set; }
        public string Name { get; set; }
        public DateTime beg_date { get; set; }
        public DateTime end_date { get; set; }
        public string Description { get; set; }
        public int prev_phaseId { get; set; }
        public bool is_done { get; set; }
        public bool is_active { get; set;}
        public Phase prevPhase { get; set; }
        public int orderId { get; set; }

        public Phase(int id, string name, DateTime beg_d, DateTime end_d, string descr, int prev_Id, bool is_done, bool is_active, Phase prevPhase = null)
        {
            this.id = id;
            Name = name;
            beg_date = beg_d;
            this.end_date = end_d;
            Description = descr;
            prev_phaseId = prev_Id;
            this.is_done = is_done;
            this.is_active = is_active;
            this.prevPhase = prevPhase;
        }

        public Phase( string name, DateTime beg_d, DateTime end_d, string descr, Phase prevPhase = null)
        {
            this.id = -1;
            Name = name;
            beg_date = beg_d;
            this.end_date = end_d;
            Description = descr;
            prev_phaseId = -1;
            this.is_done = false;
            this.is_active = false;
            this.prevPhase = prevPhase;
        }

        public Phase()
        {
            this.id = -1;
            Name = null;
            beg_date = DateTime.MinValue;
            this.end_date = DateTime.MinValue;
            Description = null;
            prev_phaseId = -1;
            this.is_done = false;
            this.is_active = false;
            this.prevPhase = prevPhase;
        }

        public override bool Equals(object obj)
        {
            var phase = obj as Phase;
            if (phase?.Name == Name && beg_date == phase?.beg_date && end_date == phase?.end_date)
                return true;
            return false;

        }
        public override int GetHashCode() => Name.GetHashCode() + beg_date.GetHashCode() + end_date.GetHashCode();

    }
}
