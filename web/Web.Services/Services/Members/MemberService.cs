﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Entity.Entity;
using Web.Entity.Infrastructure;
using Web.Entity.Model;
using Web.Repositories.Interface;
using Web.Repositories.Repositories.Administration;
using Web.Repositories.Repositories.Members;
using Web.Repositories.Utitlities;
using Web.Services.Mapping;

namespace Web.Services.Services.Members
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetMemberList();
        Task<MemberDto> GetMemberByIdAsync(int id);
        Task<int> InsertUpdatePersonalInfo(MemberPersonalInfoDto dto);
        Task<int> AddContactInfo(MemberContactInfoDto dto);
        Task<int> AddModifyMemberAddress(MemberAddressDto dto);
        Task<int> AddOccupation(MemberOccupationDto dto);
        Task<int> AddMemberDocument(MemberDocumentsDto dto);
        Task<int> AddBankDeposit(MemberBankDepositDto dto);
        Task<AddressDto> GetMemberAddressAsync(int memberId);
        Task<BankDeposit> GetBankDepositAsync(int memberId);
        Task<UserDocumentDto> GetMemberDocumentAsync(int memberId);
        Task<List<KeyValuePairDto>> ValidatePersonalInfo(MemberPersonalInfoDto dto);
        Task<List<KeyValuePairDto>> ValidateContactInfo(MemberContactInfoDto dto);
        Task<SearchMemberDto> GetMemberByAttrAsync(string memberAttr);
        Task<MemberDto> GeMemberByReferenceCode(string referalCode);
        Task<List<KeyValuePairDto>> ValidateBankDeposit(MemberBankDepositDto dto);
        Task SendEmailOnFormCompletion(int id);
    }

    public class MemberService:IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IDateService _dateService;
        private readonly IMessageClass _messageClass;
        private readonly IBaseInterface _baseInterface;
        private readonly IPhotoStorageRepository _photoStorageRepository;
        private readonly IImageService _imageService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailService _emailService;
        public MemberService(IMemberRepository memberRepository,
            IDateService dateService, IMessageClass messageClass,
            IBaseInterface baseInterface,
            IPhotoStorageRepository photoStorageRepository,
            IImageService imageService, IEmailTemplateService emailTemplateService,
            EmailService emailService)
        {
            _memberRepository = memberRepository;
            _dateService = dateService;
            _messageClass = messageClass;
            _baseInterface = baseInterface;
            _photoStorageRepository = photoStorageRepository;
            _imageService = imageService;
            _emailTemplateService = emailTemplateService;
            _emailService = emailService;
        }
        public async Task<MemberDto> GetMemberByIdAsync(int id)
        {
            var obj= await _memberRepository.GetMemberById(id);
            if (obj.PermanentIsOutsideNepal == true)
            {
                obj.PermanentFullAddress = obj.PermanentAddress + "," + obj.PermanentCountryName;
                obj.TemporaryFullAddress = obj.TemporaryAddress + "," + obj.TemporaryCountryName;
            }
            else
            {
                obj.PermanentFullAddress = obj.PermanentMunicipalityName + "-" + obj.PermanentWardNumber + "," + obj.PermanentDistrictName;
            }
            if (obj.TemporaryIsOutsideNepal == true)
            {
                obj.TemporaryFullAddress = obj.TemporaryAddress + "," + obj.TemporaryCountryName;
            }
            else
            {
                obj.TemporaryFullAddress = obj.TemporaryMunicipalityName + "-" + obj.TemporaryWardNumber + "," + obj.TemporaryDistrictName;
            }
            return obj;
        }

        public async Task<IEnumerable<MemberDto>> GetMemberList()
        {
            var obj =await _memberRepository.GetMemberList();
            return obj;
        }

        public async Task<SearchMemberDto> GetMemberByAttrAsync(string memberAttr)
        {
            var obj= await _memberRepository.GetMemberByAttr(memberAttr);
            var data = new SearchMemberDto();
            data.IsNotFoundOrReject = true;
            data.ErrorMessage = "No Record Found From this Citizenship Number or Phone Number or Email Address,Please fill up new one.........";
            if (obj != null)
            {
                data.ErrorMessage = "Your Form has approved already,You cannot modify from here,If you want to change your details please login to system or contact to admin.....";
                if (obj.ApprovalStatus == ApprovalStatus.UnApproved)
                {
                    data.IsNotFoundOrReject = false;
                    data.Member = obj;
                }
                else if (obj.ApprovalStatus == ApprovalStatus.Rejected)
                {
                    data.IsNotFoundOrReject = true;
                    data.ErrorMessage = "Your Form has been rejected,You cannot modify it.<br>Please fill up form again";
                }
            }
            return data;
        }
        public async Task<int> InsertUpdatePersonalInfo(MemberPersonalInfoDto dto)
        {
            var conn = _baseInterface.GetConnection();
            var transaction = conn.BeginTransaction();
            try
            {
                int memberId = dto.MemberId;
                if (dto.MemberId == 0)
                {
                    var member = dto.ToPersonalInfoEntity(null);
                    member.MemberCode = await _memberRepository.GetMemberCode();
                    member.DateOfBirthAD= Convert.ToDateTime(_dateService.ConvertToEnglishDate(dto.DateOfBirthBS));
                    memberId = _memberRepository.Insert(member, transaction, conn);
                    var memberDetails = new MemberDetails();
                    memberDetails.MemberId = memberId;
                    memberDetails.MemberTypeId = 1;
                    memberDetails.IsPrimary = true;
                    _memberRepository.InsertMemberDetails(memberDetails, transaction, conn);
                }
                else
                {
                    var obj = await _memberRepository.GetMemberById(dto.MemberId);
                    if (obj is null)
                        return 0;
                    var entity = dto.ToPersonalInfoEntity(obj.ToEntity());
                    entity.DateOfBirthAD = Convert.ToDateTime(_dateService.ConvertToEnglishDate(dto.DateOfBirthBS));
                    _memberRepository.UpdateWithTransaction(entity,transaction,conn);
                    memberId = obj.MemberId;
                }
                transaction.Commit();
                return memberId;
            }
            catch (SqlException)
            {
                transaction.Rollback();
                return 0;
            }
            
        }
        public async Task<int> AddContactInfo(MemberContactInfoDto dto)
        {
            try
            {
                int memberId = dto.MemberId;
                var obj = await _memberRepository.GetMemberById(dto.MemberId);
                if (obj is null)
                    return 0;
                var entity = dto.ToContactInfoEntity(obj.ToEntity());
                _memberRepository.Update(entity);
                //await SendEmailOnFormCompletion(obj);
                return memberId;
            }
            catch (SqlException)
            {
                return 0;
            }
        }
        public async Task<int> AddModifyMemberAddress(MemberAddressDto dto)
        {
            try
            {
                int memberId = dto.MemberId;
                var entity = dto.ToMemberAddress();
                var obj = await _memberRepository.GetMemberById(dto.MemberId);
                if (obj is null)
                    return 0;
                var address =await _memberRepository.GetMemberAddressById(memberId);
                if(address is null)
                    _memberRepository.InsertAddress(entity);
                else
                {
                    entity.Id = address.Id;
                    _memberRepository.UpdateAddress(entity);
                }
                return memberId;
            }
            catch (SqlException)
            {
                return 0;
            }

        }
        public async Task<int> AddOccupation(MemberOccupationDto dto)
        {
            try
            {
                int memberId = dto.MemberId;
                var obj = await _memberRepository.GetMemberById(dto.MemberId);
                if (obj is null)
                    return 0;
                var entity = dto.ToOccupationEntity(obj.ToEntity());
                _memberRepository.Update(entity);
                return memberId;
            }
            catch (SqlException)
            {
                return 0;
            }

        }
        public async Task<int> AddMemberDocument(MemberDocumentsDto dto)
        {
            var conn = _baseInterface.GetConnection();
            var transaction = conn.BeginTransaction();
            try
            {
                int memberId = dto.MemberId;
                var obj = await _memberRepository.GetMemberById(dto.MemberId);
                var document = await _memberRepository.GetMemberDocumentsById(memberId);
                if (obj is null)
                    return 0;
               
                var entity = dto.ToDocumentEntity();
                if (document is null)
                    _memberRepository.InsertMemberDocument(entity,transaction,conn);
                else
                {
                    entity.UserDocumentId = document.UserDocumentId;
                    _memberRepository.UpdateMemberDocument(entity,transaction,conn);
                }
                transaction.Commit();
                return memberId;
            }
            catch (SqlException)
            {
                transaction.Rollback();
                return 0;
            }

        }
        public async Task<int> AddBankDeposit(MemberBankDepositDto dto)
        {
            var conn = _baseInterface.GetConnection();
            var transaction = conn.BeginTransaction();
            try
            {
                int? ReferenceId = null;
                if (dto.ReferalCode != null)
                {
                    var referalMember = await GeMemberByReferenceCode(dto.ReferalCode);
                    if (referalMember == null)
                    {
                        throw new Exception("Referal Code is not valid");
                    }
                    ReferenceId = referalMember.MemberId;
                }
                
                int memberId = dto.MemberId;
                var obj = await _memberRepository.GetMemberById(dto.MemberId);
                if (obj is null)
                    return 0;
                var bankDeposit = await _memberRepository.GetMemberBankDepositById(memberId);
                var entity = dto.ToBankDepositEntity();
                if (bankDeposit is null)
                    _memberRepository.InsertBankDeposit(entity, transaction, conn);
                else
                {
                    entity.Id = bankDeposit.Id;
                    _memberRepository.UpdateBankDeposit(entity, transaction, conn);
                }
                obj.FormStatus = FormStatus.Complete;
                obj.ReferenceId = ReferenceId;
                _memberRepository.UpdateWithTransaction(obj.ToEntity(), transaction, conn);
                transaction.Commit();
                return memberId;
            }
            catch (SqlException)
            {
                transaction.Rollback();
                return 0;
            }
        }
       
        public async Task<AddressDto> GetMemberAddressAsync(int memberId)
        {
            var obj= await _memberRepository.GetMemberAddressById(memberId);
            return obj;
        }

        public async Task<UserDocumentDto> GetMemberDocumentAsync(int memberId)
        {
            var obj= await _memberRepository.GetMemberDocumentsById(memberId);
            return obj;
        }
        public async Task<BankDeposit> GetBankDepositAsync(int memberId)
        {
            var obj = await _memberRepository.GetMemberBankDepositById(memberId);
            return obj;
        }

        public async Task<MemberDto> GeMemberByReferenceCode(string referalCode)
        {
            var obj = await _memberRepository.GetMemberByReferalCode(referalCode);
            return obj;
        }

        public async Task<List<KeyValuePairDto>> ValidatePersonalInfo(MemberPersonalInfoDto dto)
        {
            var obj = new List<KeyValuePairDto>();
            var IsCitizenship = await _memberRepository.CheckCitizenshipNumber(dto.MemberId, dto.CitizenshipNumber);
            if (IsCitizenship)
            {
                var data = new KeyValuePairDto
                {
                    Key = "CitizenshipNumber",
                    Value = "CitizenshipNumber must be unique"
                };
                obj.Add(data);
            }
            var isDate = _dateService.CheckNepaliDateValidity(dto.DateOfBirthBS);
            if (isDate == false)
            {
                var data = new KeyValuePairDto
                {
                    Key = "DateOfBirthBS",
                    Value = "Date is not in correct format"
                };
                obj.Add(data);
            }
            return obj;
        }
        public async Task<List<KeyValuePairDto>> ValidateContactInfo(MemberContactInfoDto dto)
        {
            var obj = new List<KeyValuePairDto>();
            var IsMobileNumber = await _memberRepository.CheckPhoneNumber(dto.MemberId, dto.MobileNumber);
            if (IsMobileNumber)
            {
                var data = new KeyValuePairDto
                {
                    Key = "IsMobileNumber",
                    Value = "IsMobileNumber must be unique"
                };
                obj.Add(data);
            }
            var isEmail =await _memberRepository.CheckEmail(dto.MemberId,dto.Email);
            if (isEmail)
            {
                var data = new KeyValuePairDto
                {
                    Key = "Email",
                    Value = "Email must be unique"
                };
                obj.Add(data);
            }
            return obj;
        }

        public async Task<List<KeyValuePairDto>> ValidateBankDeposit(MemberBankDepositDto dto)
        {
            var obj = new List<KeyValuePairDto>();
            if (dto.ReferalCode != null)
            {
                var member = await _memberRepository.GetMemberByReferalCode(dto.ReferalCode);
                if (member == null)
                {
                    var data = new KeyValuePairDto
                    {
                        Key = "ReferalCode",
                        Value = "Invalid Referal Code"
                    };
                    obj.Add(data);
                }
            }
            return obj;
        }

        
        public async Task SendEmailOnFormCompletion(int id)
        {
            var dto = await GetMemberByIdAsync(id);
            var template = await _emailTemplateService.GetGeneralTemplate();
            template=template.Replace("{{Name}}", dto.FullName);
            template=template.Replace("{{Message}}", "Your form has been submitted successfully,<br />Please wait for admin response<br/>Thankyou!!!!!!!<br />");
            _emailService.SendEmail(dto.Email, "Form Completion", template);
        }
    }
}
