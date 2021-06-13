using NDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Services.Services
{
    public interface IDateService
    {
        string ConvertToEnglishDate(string nepaliDateStr);
        string ConvertToNepaliDate(DateTime englishDate);
        bool CheckNepaliDateValidity(string nepaliDateStr);
    }

    public class DateService:IDateService
    {

        public string ConvertToNepaliDate(DateTime englishDate)
        {
            DateConverter nepaliDate = NDC.DateConverter.ConvertToNepali(englishDate.Year, englishDate.Month, englishDate.Day);
            string month = nepaliDate.Month.ToString();
            if (nepaliDate.Month < 10)
            {
                month = "0" + month;
            }
            string day = nepaliDate.Day.ToString();
            if (nepaliDate.Day < 10)
            {
                day = "0" + day;
            }
            return string.Format("{0}-{1}-{2}", nepaliDate.Year, month, day);
        }

        public string ConvertToEnglishDate(string nepaliDateStr)
        {
            //string[] str = nepaliDateStr.Split('-');
            //string nepaliYear = str[0];
            //string nepaliMonth = str[1];
            //string nepaliDate = str[2];

            //if (string.IsNullOrEmpty(nepaliMonth) || string.IsNullOrEmpty(nepaliDate) || string.IsNullOrEmpty(nepaliYear))
            //{
            //    return "";
            //}
            var data=SplitNepaliDate(nepaliDateStr);
            return GetEnglishDate(data[0], data[1], data[2]);
        }

        public List<string> SplitNepaliDate(string nepaliDateStr)
        {
            string[] str = nepaliDateStr.Split('-');
            if (str.Count() < 3)
            {
                return null;
            }
            string nepaliYear = str[0];
            string nepaliMonth = str[1];
            string nepaliDate = str[2];

            if (string.IsNullOrEmpty(nepaliMonth) || string.IsNullOrEmpty(nepaliDate) || string.IsNullOrEmpty(nepaliYear))
            {
                return null;
            }
            //var strList = new List<string>();
            //strList.Add(nepaliYear);
            //strList.Add(nepaliMonth);
            //strList.Add(nepaliDate);
            return str.ToList();
            //return strList;
        }

        public string GetEnglishDate(string nepaliYear,string nepaliMonth,string nepaliDate)
        {
            DateConverter englishDate = NDC.DateConverter.ConvertToEnglish(Convert.ToInt32(nepaliYear),
                                                                            Convert.ToInt32(nepaliMonth),
                                                                            Convert.ToInt32(nepaliDate));
            
            return string.Format("{0}/{1}/{2}", englishDate.Year, englishDate.Month, englishDate.Day);
        }
        public bool CheckNepaliDateValidity(string nepaliDateStr)
        {
            var split = SplitNepaliDate(nepaliDateStr);
            if (split == null)
                return false;
            var data = GetEnglishDate(split[0], split[1], split[2]);
            if (data == null || data=="0/0/0")
                return false;

            return true;
        }
    }
}
