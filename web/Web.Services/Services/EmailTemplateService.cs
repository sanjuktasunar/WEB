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
    }

    public class EmailTemplateService:IEmailTemplateService
    {
        private readonly OrganizationInfoService _organizationInfoService;
        public EmailTemplateService(OrganizationInfoService organizationInfoService)
        {
            _organizationInfoService = organizationInfoService;
        }

        public async Task<string> GetGeneralTemplate()
        {
            var organizationInfo =await _organizationInfoService.GetOrganizationInfo();
            var template = "Dear {{Name}},<br /> " +
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
    }
}
