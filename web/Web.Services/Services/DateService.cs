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
            string[] str = nepaliDateStr.Split('-');
            string nepaliYear = str[0];
            string nepaliMonth = str[1];
            string nepaliDate = str[2];

            if (string.IsNullOrEmpty(nepaliMonth) || string.IsNullOrEmpty(nepaliDate) || string.IsNullOrEmpty(nepaliYear))
            {
                return "";
            }

            DateConverter englishDate = NDC.DateConverter.ConvertToEnglish(Convert.ToInt32(nepaliYear),
                                                                            Convert.ToInt32(nepaliMonth),
                                                                            Convert.ToInt32(nepaliDate));

            return string.Format("{0}/{1}/{2}", englishDate.Year, englishDate.Month, englishDate.Day);
        }
    }
}
