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

        public brDocument brDocView { get; set; }
        public extDoc extDocView { get; set; }

        //Добавить методы обработки событий на формах
        public Document(int id, string name, string phaddress, string status, string description, DateTime planDate, DateTime factDate,
                        string projName, int projId, int empId,string empName, string phaseName, int phaseId)
        {
            this.id = id;
            Name = name;
            PhysicalAddres = phaddress;
            Status = status;
            Description = description;
            this.planDate = planDate;
            this.factDate = factDate;
            projectName = projectName;
            projectId = projectId;
            this.empId = empId;
            this.empName = empName;
            this.phaseName = phaseName;
            this.phaseId = phaseId;

            brDocView = new brDocument
            {
                empName = empName,
                docName = Name,
                projectName = projectName,
            };

            extDocView = new extDoc
            {
                empName = empName,
                docName = Name,
                status = Status,
                planDate = planDate.ToString(),
                factDate = factDate.ToString(),
                description = Description,
            };
        }
    }
}
