using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ReadExcelToDb.Interfaces
{
    public interface IExcelRepository
    {
        IEnumerable<IntervalData> GetIntervalDatas();
        bool SaveIntervalDatas(string conString);
        string SaveExcel(HttpPostedFileBase postedFile, string path);
    }
}
