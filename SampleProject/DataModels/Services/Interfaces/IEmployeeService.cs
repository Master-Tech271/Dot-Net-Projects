using DataModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeModel>> GetEmployeesAsync();
    }
}
