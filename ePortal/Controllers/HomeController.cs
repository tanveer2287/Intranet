using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataViewModels;
using Microsoft.AspNetCore.Mvc;
using ePortal.Models;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using Services.Interrface;

namespace WebAppUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _empService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(IEmployeeService empService, IHostingEnvironment hostingEnvironment)
        {
            _empService = empService;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _empService.GetEmployees();
            var aa = "test";
            return View(model);
            /*
            string rootFolder = _hostingEnvironment.WebRootPath;
            string fileName = @"DMCCEmployeeUploadTemplate_Core.xlsx";
            string fileExport = @"DHAMemberCensus.xlsx";

            FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

            FileInfo exportExcel = new FileInfo(Path.Combine(rootFolder, fileExport));

            var export = (Path.Combine(rootFolder, fileExport));

            var fileLocation = Path.Combine(rootFolder, @"DHAMemberCensus_Template.xlsx");

            if (System.IO.File.Exists(export))
                System.IO.File.Delete(export);

            System.IO.File.Copy(fileLocation, export);

          
            
            List<EmployeeVM> employeeList = new List<EmployeeVM>();

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets["MemberDetails"];
                int totalRows = workSheet.Dimension.End.Row;


                try
                {
                    for (int i = 4; i <= totalRows; i++)
                    {
                        employeeList.Add(new EmployeeVM
                        {
                            FirstName = workSheet.Cells[i, 2].Value.ToString(),
                            LastName = workSheet.Cells[i, 4].Value.ToString()
                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
               

            }
            using (ExcelPackage epackage = new ExcelPackage(exportExcel))
            {
                //ExcelWorksheet workSheet = epackage.Workbook.Worksheets.Add("MemberDetails");
                ExcelWorksheet workSheet = epackage.Workbook.Worksheets["MemberDetails"];

                try
                {
                    for (int i = 3; i <= employeeList.Count; i++)
                    {
                        workSheet.Cells[i, 1].Value = employeeList[i - 3].FirstName;                        
                        workSheet.Cells[i, 3].Value = employeeList[i - 3].LastName;
                    }

                    var fileStream = new FileStream(export, FileMode.Create, FileAccess.Write);

                    fileStream.Position = 0;
                    fileStream.Write(epackage.GetAsByteArray());
                    fileStream.Close();
                    fileStream.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
              

            }           

            //Write content to excel file    
            //File.WriteAllBytes(p_strPath, objExcelPackage.GetAsByteArray());
            */

        }
       
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployee(EmployeeVM model)
        {
            await _empService.SaveEmployee(model);
            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Edit(int id)
        {
            ViewData["Message"] = "Your Edit page.";

            return View();
        }

        public IActionResult Delete(int id)
        {
            ViewData["Message"] = "Your Edit page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
