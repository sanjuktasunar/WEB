﻿using System;
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
        [MaxLength(20, ErrorMessage = "MobileNumber must be less than 20")]
        public string MobileNumber { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage = "Email must be less than 150")]
        public string Email { get; set; }
    }

    public class MemberAddressDto
    {
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public bool PermanentIsOutsideNepal { get; set; }

        public int? PermanentProvinceId { get; set; }

        public int? PermanentDistrictId { get; set; }

        public int? PermanentMunicipalityTypeId { get; set; }

        [StringLength(200,ErrorMessage ="Municipality must be less than 200")]
        public string PermanentMunicipality { get; set; }

        [StringLength(4, ErrorMessage = "Ward Number must be less than 4")]
        public string PermanentWardNumber { get; set; }

        [StringLength(200, ErrorMessage = "Tole name must be less than 200")]
        public string PermanentToleName { get; set; }

        public int? PermanentCountryId { get; set; }

        [StringLength(200, ErrorMessage = "Address must be less than 200")]
        public string PermanentAddress { get; set; }

        [Required]
        public bool TemporaryIsOutsideNepal { get; set; }

        public int? TemporaryProvinceId { get; set; }
        public int? TemporaryDistrictId { get; set; }
        public int? TemporaryMunicipalityTypeId { get; set; }

        [StringLength(200, ErrorMessage = "Municipality must be less than 200")]
        public string TemporaryMunicipality { get; set; }

        [StringLength(200, ErrorMessage = "Ward number must be less than 200")]
        public string TemporaryWardNumber { get; set; }

        [StringLength(200, ErrorMessage = "Tole name must be less than 200")]
        public string TemporaryToleName { get; set; }

        public int? TemporaryCountryId { get; set; }

        [StringLength(200, ErrorMessage = "Address must be less than 200")]
        public string TemporaryAddress { get; set; }
    }

    public class MemberOccupationDto
    {
        [Required]
        public int MemberId { get; set; }

        [Required]
        public int OccupationId { get; set; }

        [StringLength(150,ErrorMessage ="Occupation Remarks must be less than 150")]
        public string OtherOccupationRemarks { get; set; }

        [Required]
        public int MemberFieldId { get; set; }
    }

    public class MemberDocumentsDto
    {
        public int UserDocumentId { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public string MemberPhoto { get; set; }

        [Required]
        public string CitizenshipFront { get; set; }

        [Required]
        public string CitizenshipBack { get; set; }
    }

    public class MemberBankDepositDto
    {
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public string VoucherImage { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
