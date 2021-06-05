using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Entity
{
    public class Member
    {
        public int MemberId { get; set; }
        public int PhotoStorageId { get; set; }
        public string MemberCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string DateOfBirthBS { get; set; }
        public DateTime? DateOfBirthAD { get; set; }
        public int GenderId { get; set; }
        public string CitizenshipNumber { get; set; }
    }

    public class MemberDetails
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int MemberTypeId { get; set; }
        public bool IsPrimary { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
    }
}
