using DataModels.Models;
using DataModels.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataModels.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> Employees = new List<EmployeeModel>();
            try
            {
                var result = await _httpClient.GetAsync($"api/Employees");
                if (result.IsSuccessStatusCode)
                {
                    var reader = await result.Content.ReadAsStringAsync();
                    Employees = JsonConvert.DeserializeObject<List<EmployeeModel>>(reader);
                }
            }
            catch (Exception ex)
            {
                //TODO: Log Exception
                HelperClass.LogError(ex.Message, ex);
            }
            return Employees;
        }
    }
}
