using ReadExcelToDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadExcelToDb.Controllers
{
    public class ExcelController : Controller
    {
        IExcelRepository _excelRepository;
        public ExcelController(IExcelRepository excelRepository)
        {
            _excelRepository = excelRepository;
        }
        public ActionResult Index(bool isredirected = false)
        {
           var intervals = _excelRepository.GetIntervalDatas();
            if(isredirected) ViewBag.Message = "File uploaded successfully.";
            return View(intervals);
        }

        [HttpPost]
        public ActionResult UploadExcel(HttpPostedFileBase postedFile)
        {
            string path = Server.MapPath("~/Uploads/");
            var connString = _excelRepository.SaveExcel(postedFile, path);
            var isSaved = _excelRepository.SaveIntervalDatas(connString);
            return RedirectToAction("Index", new { isredirected = isSaved });
        }
    }
}