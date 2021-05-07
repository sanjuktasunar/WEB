﻿using System;
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
            return LanguageId;
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
            }
            else
            {
                obj.Home = "Home";
                obj.AboutUs = "About US";
                obj.ContactUs = "Contact Us";
                obj.Login = "Log In";
                obj.Products = "Products";
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