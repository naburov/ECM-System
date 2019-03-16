using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СЭД_ЭК.Entities
{
    public class Project
    {
        public int id { get; set; }
        public string Name { get; set; }
        public DateTime begin_Date { get; set; }
        public DateTime end_Date { get; set; }
        public DateTime end_PhaseDate { get; set; }
        public string Description { get; set; }
        public int activePhaseId { get; set; }
        public string activePhaseName { get; set; }
        public DateTime end_Date_Fact { get; set; }
        public int clId { get; set; }

        public Project(int Id, string Name, DateTime beg_date, DateTime end_date, string descr, int activePhaseId, string activePhase, DateTime end_PhaseDate)
        {
            this.id = Id;
            this.Name = Name;
            this.begin_Date = beg_date;
            this.end_Date = end_date;
            this.Description = descr;
            this.activePhaseId = activePhaseId;
            this.activePhaseName = activePhase;
            this.end_PhaseDate = end_PhaseDate;
        }

        public Project()
        {
            this.id = -1;
            this.Name = null;
            this.begin_Date = DateTime.MaxValue;
            this.end_Date = DateTime.MaxValue;
            this.Description = null;
            this.activePhaseId = -1;
            this.activePhaseName = null;
            clId = -1;
            this.end_PhaseDate = DateTime.MaxValue;

        }
    }
}
