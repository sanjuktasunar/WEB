using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Entity.Dto
{
    public class MemberDto
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

    public class MemberPersonalInfoDto
    {
        public int MemberId { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="FirstName must be less than 20")]
        public string FirstName { get; set; }

        [StringLength(20, ErrorMessage = "MiddleName must be less than 20")]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "LastName must be less than 20")]
        public string LastName { get; set; }

        [Required]
        public int GenderId { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "DateOfBirth must be less than 10")]
        public string DateOfBirthBS { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "CitizenshipNumber must be less than 20")]
        public string CitizenshipNumber { get; set; }
    }

    public class MemberContactInfoDto
    {
        public int MemberId { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public string Email { get; set; }
    }

    public class MemberDocumentsDto
    {
        public int UserDocumentId { get; set; }
        public int? MemberId { get; set; }
        public string CitizenshipFront { get; set; }
        public string CitizenshipBack { get; set; }
    }

    public class MemberBankDepositDto
    {
        public int Id { get; set; }
        public int? MemberId { get; set; }
        public string VoucherImage { get; set; }
        public decimal Amount { get; set; }
    }
}
