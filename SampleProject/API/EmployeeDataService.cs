using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DataModels.Models;
using SQLORM;
using DataModels;
using System.Data;
using System.Collections.Generic;

namespace API
{
    public static class EmployeeDataService
    {
        [FunctionName("Employees")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Employees")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<EmployeeModel> Employees = new List<EmployeeModel>();


            try
            {
                DataSet DataSet = await SQLData.FetchDataSet(StoreProcedure.Usp_GetEmployees.ToString(), null, CommandType.StoredProcedure);

                if (DataSet != null)
                {
                    DataTable DataTable = DataSet.Tables[0];
                    Employees = HelperClass.ConvertDataTable<EmployeeModel>(DataTable);
                }
            } catch(Exception ex)
            {
                log.LogError(ex.Message, ex);
            }

            return new OkObjectResult(JsonConvert.SerializeObject(Employees));
        }
    }
}
