using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Services.Services.Administration;

namespace Web.Services.Services
{
    public interface IEmailTemplateService
    {
        Task<string> GetGeneralTemplate();
        Task<string> GetMemberApproveTemplate();
        Task<string> GetPlainMemberApproveTemplate();
    }

    public class EmailTemplateService:IEmailTemplateService
    {
        private readonly OrganizationInfoService _organizationInfoService;
        public EmailTemplateService(OrganizationInfoService organizationInfoService)
        {
            _organizationInfoService = organizationInfoService;
        }

        public async Task<string> BasicTemplateLayout()
        {
            var organizationInfo = await _organizationInfoService.GetOrganizationInfo();
            var template = "{{Heading}},<br /> " +
                "{{Message}} " +
                "<br /><br />{{organizationName}}" +
                "<br />{{Address}}" +
                "<br />" +
                "Contact Number : {{PhoneNumber}}";
            template = template.Replace("{{organizationName}}", organizationInfo.OrganizationName);
            template = template.Replace("{{Address}}", organizationInfo.Address);
            template = template.Replace("{{PhoneNumber}}", organizationInfo.ContactNumber1);
            return template;
        }

        public async Task<string> GetGeneralTemplate()
        {
            var template = await BasicTemplateLayout();
            template = template.Replace("{{Heading}}", "Dear {{Name}}");
            return template;
        }

        public async Task<string> GetMemberApproveTemplate()
        {
            var template = await BasicTemplateLayout();
            template = template.Replace("{{Heading}}", "Congratulations {{Name}} !!!!");
            template = template.Replace("{{Message}}","Your Form has been approved by company administration.Now,You are in list of Top 100 Team members of our company<br />Your Login Credentials are as follow: <br />Username : {{UserName}},Password : {{Password}} <br /><br /> Your Member Details are as follow:<br />Member Name : {{MemberName}}<br />Member Code : {{MemberCode}}<br /> Referal Code : {{ReferalCode}}<br /> Please give this Referal Code while joining other people.<br /> Thankyou !!!!  ");
            return template;
        }

        public async Task<string> GetPlainMemberApproveTemplate()
        {
            var template = await BasicTemplateLayout();
            template = template.Replace("{{Heading}}", "Congratulations {{Name}} !!!!");
            template = template.Replace("{{Message}}", "Your Form has been approved by company administration.Now,You are in list of Top 100 Team members of our company" +
                "Your Login Credentials are as follow: " +
                "Username : {{UserName}},Password : {{Password}} " +
                "" +
                "" +
                "Your Member Details are as follow:" +
                "Member Name : {{MemberName}}" +
                "Member Code : {{MemberCode}}" +
                "Referal Code : {{ReferalCode}}" +
                "Please give this Referal Code while joining other people." +
                "Thankyou !!!!  ");
            return template;
        }
    }
}
