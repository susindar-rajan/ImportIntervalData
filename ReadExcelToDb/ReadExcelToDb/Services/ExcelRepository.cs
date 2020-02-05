using ReadExcelToDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;

namespace ReadExcelToDb.Services
{
    public class ExcelRepository : IExcelRepository
    {
        public IEnumerable<IntervalData> GetIntervalDatas()
        {
            var context = new ExcelEntities();
            return context.IntervalDatas.ToList();
        }
        public bool SaveIntervalDatas(string conString)
        {
            var context = new ExcelEntities();
            try
            {
                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            cmdExcel.Connection = connExcel;

                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();

                            foreach (DataRow row in dt.Rows)
                            {
                                var id = new IntervalData
                                {
                                    DeliveryPoint = long.Parse(row.ItemArray[0].ToString()),
                                    Date = DateTime.Parse(row.ItemArray[1].ToString()),
                                    TimeSlot = new TimeSpan(Convert.ToInt32(row.ItemArray[2].ToString())),
                                    SlotVal = Decimal.Parse(row.ItemArray[3].ToString())
                                };
                                context.IntervalDatas.Add(id);
                            }
                            context.SaveChanges();
                            return true;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public string SaveExcel(HttpPostedFileBase postedFile, string path)
        {
            string conString = string.Empty;
            string filePath = string.Empty;
            try
            {
                if (postedFile != null)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    if (File.Exists(filePath))
                    { 
                        File.Delete(filePath);
                        Console.WriteLine("File deleted.");
                    }
                    postedFile.SaveAs(filePath);
                    switch (extension)
                    {
                        case ".xls":
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx":
                            conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            break;
                    }
                    conString = string.Format(conString, filePath);
                }
            }
            catch
            {
                return string.Empty;
            }
            return conString;
        }
    }
}