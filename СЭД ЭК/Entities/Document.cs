using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace СЭД_ЭК.Entities
{
    public class Document
    {

        public int id { get; set; }
        public string Name { get; set; }
        public string PhysicalAddres { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime planDate { get; set; }
        public DateTime factDate { get; set; }
        public string projectName { get; set; }
        public int projectId { get; set; }
        public int empId { get; set; }
        public string empName { get; set; }
        public string phaseName { get; set; }
        public int phaseId { get; set; }
        public bool IsAccessedForAll { get; set; }

        public brDocument brDocView { get; set; }
        public extDoc extDocView { get; set; }

        //Добавить методы обработки событий на формах
        public Document(int id, string name, string phaddress, string status, string description, DateTime planDate, DateTime factDate,
                        string projName, int projId, int empId, string empName, int phaseId, string phaseName)
        {
            this.id = id;
            Name = name;
            PhysicalAddres = phaddress;
            Status = status;
            Description = description;
            this.planDate = planDate;
            this.factDate = factDate;
            projectName = projName;
            projectId = projId;
            this.empId = empId;
            this.empName = empName;
            this.phaseName = phaseName;
            this.phaseId = phaseId;

            //brDocView = new brDocument
            //{
            //    physicalAddress = phaddress,
            //    empName = empName,
            //    docName = Name,
            //    projectName = this.projectName,
            //};

            //extDocView = new extDoc
            //{
            //    empName = empName,
            //    docName = Name,
            //    status = Status,
            //    planDate = planDate.ToString(),
            //    factDate = factDate.ToString(),
            //    description = Description,
            //};
        }

        public Document( string name, string description,  string empName, bool IsAccessedForAll=true)
        {
            this.id = -1;
            Name = name;
            PhysicalAddres = null;
            Status = null;
            Description = description;
            this.planDate = DateTime.MinValue;
            this.factDate = DateTime.MinValue;
            projectName = null;
            projectId = -1;
            this.empId = -1;
            this.empName = empName;
            this.phaseName = null;
            this.phaseId = -1;
            this.IsAccessedForAll = IsAccessedForAll;
        }

        public Document()
        {
            this.id = -1;
            Name = null;
            PhysicalAddres = null;
            Status = null;
            Description = null;
            this.planDate = DateTime.MaxValue;
            this.factDate = DateTime.MinValue;
            projectName = null;
            projectId = -1;
            this.empId = -1;
            this.empName = null;
            this.phaseName = null;
            this.phaseId = -1;

        }

    }
}
