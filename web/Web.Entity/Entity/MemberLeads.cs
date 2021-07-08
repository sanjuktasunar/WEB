using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class MemberLeads: BaseEntityData
    {
        public int LeadId { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public DateTime VisitedDateAD { get; set; }
        public string VisitedDateBS { get; set; }
    }

    public class MemberLeadFollowup
    {
        public int FollowupId { get; set; }
        public int LeadId { get; set; }
        public string Remarks { get; set; }
        public DateTime FollowupDateAD { get; set; }
        public string FollowupDateBS { get; set; }
    }
}
