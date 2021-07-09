using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Repositories.Utitlities
{
    public interface IMessageClass
    {
        string ShowErrorMessage(string exception);
        string ShowSuccessMessage(int result);
        string ShowDeleteMessage(int result);
        string ShowConfirmMessage(int result);
    }
    public class MessageClass:IMessageClass
    {
        public string ShowErrorMessage(string exception)
        {
            string errorNumber, errorMessage, message;
            string[] splitErrorMessage = new string[2];
            splitErrorMessage = exception.Split(new char[] { '~' }, 2);

            errorNumber = splitErrorMessage[0].Trim();
            errorMessage = splitErrorMessage[1].Trim();

            switch (errorNumber)
            {
                case "547":
                    //Referenced data error
                    message = @"<b>माफ गर्नुहोला यो विवरणलाई हटाउन मिल्दैन !</b> <br/>                                 
                                यदि यो विवरण नचाहिने भएमा यस विवरणलाई सम्पादन गरी निष्क्रिय गर्नुहोस् ।";
                    break;
                case "2601":
                    //Unique data error, if both information are from different table
                    message = errorMessage;
                    break;
                default:
                    message = string.Format(@"<b>H2O आन्तरीक समस्या । </b> <br/>
                                                कृपया यस ईरर मेसेज सहित सफ्टल्याब ईंकमा सम्पर्क गर्नुहोस् ।  <br/>
                                                ईरर नं.: {0} <br> {1} <br /> <br />
                                            ", errorNumber, errorMessage);
                    break;
            }

            return message+"+"+-1;
        }

        public string ShowSuccessMessage(int result)
        {
            string str = "";
            if (result >= 0)
            {
                str = "तपाईको डाटा सुरक्षित राखिएको छ ।";
            }
            else
            {
                str = "माफ गर्नुहोला, सिस्टमको डाटा सम्पादन गर्न मिल्दैन";
            }
            return str+"+"+result;
        }

        public string ShowConfirmMessage(int result)
        {
            return "के तपाई यो डाटा मेटाउन चाहनुहुन्छ ?";
        }

        public string ShowDeleteMessage(int result)
        {
            string str = "";
            if (result == 0)
            {
                str = "तपाईको डाटा हटाईएको छ ।";
            }
            else
            {
                str = "माफ गर्नुहोला, सिस्टमको डाटा हटाउन मिल्दैन !";
            }
            return str+"+"+result;
        }

        public string ShowCustomerMessage(int result)
        {
            string str = "";
            //if (result == 0)
            //{
            //    str = "तपाईको ";
            //}
            //else
            //{
            //    str = "Your Query has been submitted successfully";
            //}
            str = "Query has been submitted successfully";
            return str + "+" + result;
        }
    }
}
