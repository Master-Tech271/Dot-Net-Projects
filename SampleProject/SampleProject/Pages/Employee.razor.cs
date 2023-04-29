using DataModels.Models;
using DataModels.Services;
using DataModels.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace SampleProject.Pages
{
    public partial class Employee
    {
        public List<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
        [Inject]
        public IEmployeeService EmployeeService { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeService.GetEmployeesAsync();
            await base.OnInitializedAsync();
        }

    }
}
