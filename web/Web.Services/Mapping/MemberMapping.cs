﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Services.Mapping
{
    public static class MemberMapping
    {
        public static MemberDto ToDTO(this Member entity)
        {
            if (entity is null)
                return null;

            return new MemberDto
            {
                MemberId=entity.MemberId,
                FullName=entity.FirstName+ ' '+ (!string.IsNullOrEmpty(entity.MiddleName)?(entity.MiddleName+' '):string.Empty)+entity.MiddleName
            };
        }

        public static Member ToEntity(this MemberDto dto)
        {
            if (dto is null)
                return null;

            return new Member
            {
                MemberId = dto.MemberId,
                MemberCode = dto.MemberCode,
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                MobileNumber = dto.MobileNumber,
                Email = dto.Email,
                DateOfBirthBS = dto.DateOfBirthBS,
                DateOfBirthAD = dto.DateOfBirthAD,
                GenderId = dto.GenderId,
                OccupationId = dto.OccupationId,
                OtherOccupationRemarks = dto.OtherOccupationRemarks,
                MemberFieldId = dto.MemberFieldId,
                CitizenshipNumber = dto.CitizenshipNumber,
                IsMemberFilled = dto.IsMemberFilled,
                FormStatus = dto.FormStatus,
                ApprovalStatus = dto.ApprovalStatus,
                ApprovedDate = dto.ApprovedDate,
                ApprovedBy = dto.ApprovedBy,
                ReferenceId = dto.ReferenceId,
                ReferalCode=dto.ReferalCode,
                CreatedDate=dto.CreatedDate,
                CreatedBy=dto.CreatedBy,
                UpdatedBy=dto.UpdatedBy,
                UpdatedDate=dto.UpdatedDate,
            };
        }

        public static Member ToPersonalInfoEntity(this MemberPersonalInfoDto dto,Member entity)
        {
            if (dto is null)
                return null;
            if (entity is null)
                entity = new Member();

            entity.MemberId = dto.MemberId;
            entity.FirstName = dto.FirstName;
            entity.MiddleName = dto.MiddleName;
            entity.LastName = dto.LastName;
            entity.GenderId = dto.GenderId;
            entity.DateOfBirthBS = dto.DateOfBirthBS;
            entity.CitizenshipNumber = dto.CitizenshipNumber;
            return entity;
        }

        public static Member ToContactInfoEntity(this MemberContactInfoDto dto,Member entity)
        {
            if (dto is null)
                return null;

            entity.MobileNumber = dto.MobileNumber;
            entity.Email = dto.Email;
            return entity;
        }

        public static Address ToMemberAddress(this MemberAddressDto dto)
        {
            if (dto is null)
                return null;

            var obj = new Address();
            obj.Id = dto.Id;
            obj.MemberId = dto.MemberId;
            obj.MemberId = dto.MemberId;
            obj.PermanentIsOutsideNepal = dto.PermanentIsOutsideNepal;
            if (!dto.PermanentIsOutsideNepal)
            {
                obj.PermanentProvinceId = dto.PermanentProvinceId;
                obj.PermanentDistrictId = dto.PermanentDistrictId;
                obj.PermanentMunicipalityTypeId = dto.PermanentMunicipalityTypeId;
                obj.PermanentMunicipality = dto.PermanentMunicipality;
                obj.PermanentWardNumber = dto.PermanentWardNumber;
                obj.PermanentToleName = dto.PermanentToleName;
            }
            else
            {
                obj.PermanentCountryId = dto.PermanentCountryId;
                obj.PermanentAddress = dto.PermanentAddress;
            }
            obj.TemporaryIsOutsideNepal = dto.TemporaryIsOutsideNepal;
            if (!dto.TemporaryIsOutsideNepal)
            {
                obj.TemporaryProvinceId = dto.TemporaryProvinceId;
                obj.TemporaryDistrictId = dto.TemporaryDistrictId;
                obj.TemporaryMunicipalityTypeId = dto.TemporaryMunicipalityTypeId;
                obj.TemporaryMunicipality = dto.TemporaryMunicipality;
                obj.TemporaryWardNumber = dto.TemporaryWardNumber;
                obj.TemporaryToleName = dto.TemporaryToleName;
            }
            else
            {
                obj.TemporaryCountryId = dto.TemporaryCountryId;
                obj.TemporaryAddress = dto.TemporaryAddress;
            }
            return obj;
        }

        public static Member ToOccupationEntity(this MemberOccupationDto dto, Member entity)
        {
            if (dto is null)
                return null;

            entity.OccupationId = dto.OccupationId;
            entity.OtherOccupationRemarks = dto.OtherOccupationRemarks;
            entity.MemberFieldId = dto.MemberFieldId;
            return entity;
        }

        public static UserDocuments ToDocumentEntity(this MemberDocumentsDto dto)
        {
            if (dto is null)
                return null;

            return new UserDocuments
            {
                UserDocumentId=dto.UserDocumentId,
                MemberId = dto.MemberId,
                CitizenshipFront = dto.CitizenshipFront,
                CitizenshipBack = dto.CitizenshipBack,
                Photo=dto.MemberPhoto,
            };
        }

        public static BankDeposit ToBankDepositEntity(this MemberBankDepositDto dto)
        {
            if (dto is null)
                return null;

            return new BankDeposit
            {
                Id=dto.Id,
                MemberId = dto.MemberId,
                VoucherImage = dto.VoucherImage,
                Amount = dto.Amount,
                IsVoucherDeposit=true
            };
        }

    }
}
