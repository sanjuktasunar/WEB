using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Entity.Dto.UserSite
{
    public class DisplayParamters
    {
        public int GetLanguageId()
        {
            int LanguageId = 2;
            if (HttpContext.Current.Session.Count>0)
            {
                LanguageId = Convert.ToInt32(HttpContext.Current.Session["LangId"]);
            }
            //return LanguageId;
            return 2;
        }
      
        public ParameterClass GetParameters()
        {
            int LanguageId = GetLanguageId();
            var obj = new ParameterClass();
            if(LanguageId==2)
            {
                obj.Home = "गृह पृष्ठ";
                obj.AboutUs = "हाम्रो बारेमा";
                obj.ContactUs = "संपर्क";
                obj.Login = "लग इन";
                obj.Products = "सामन हरु";
                obj.OurProducts = "हाम्रो सामनहरु";
                obj.Per = "प्रति";
                obj.Rs = "रु.";
                obj.Search = "खोजी गर्नुहोस्";
                obj.PhoneNumber = "संपर्क न.";
                obj.Address = "ठेगाना";
                obj.EmailAddress = "ईमेल";
                obj.ContactUsHeader = "हामी सगं जुट्नुहोस्";
                obj.OurCompany = "हाम्रो कम्पनी";
                obj.ClientContact = "ग्राहक संपर्क";
            }
            else
            {
                obj.Home = "Home";
                obj.AboutUs = "About US";
                obj.ContactUs = "Contact Us";
                obj.Login = "Log In";
                obj.Products = "Products";
                obj.OurProducts = "Our Products";
                obj.Per = "Per";
                obj.Rs = "Rs.";
                obj.Search = "Search";
                obj.PhoneNumber = "Phone Number";
                obj.Address = "Address";
                obj.EmailAddress = "Email Address";
                obj.ContactUsHeader = "Create success campaign with us";
                obj.OurCompany = "Our Company";
                obj.ClientContact = "For Client";
            }
            return obj;
        }

        public MenuLink GetMenuLink()
        {
            return new MenuLink
            {
                HomeLink = "/",
                AboutUsLink = "/AboutUs",
                ContactUsLink = "/ContactUs",
                LoginLink = "/Login",
                ProductLink = "/Products",
            };
        }
    }
}