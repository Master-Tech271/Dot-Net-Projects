using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public decimal Salary { get; set; } = 0;
        public int ExperienceInYears { get; set; } = 0;
        public int ExperienceInMonths { get; set; } = 0;
    }
}
