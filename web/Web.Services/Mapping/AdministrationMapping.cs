using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Dto;
using Web.Entity.Entity;

namespace Web.Services.Mapping
{
    public static class AdministrationMapping
    {
        public static UsersDto ToDto(this Users entity)
        {
            if (entity == null)
                return null;

            return new UsersDto
            {
                UserId = entity.UserId,
                UserTypeId = entity.UserTypeId,
                PhotoStorageId = entity.PhotoStorageId,
                UserName = entity.UserName,
                Password = entity.Password,
                EmailAddress = entity.EmailAddress,
                ContactNumber=entity.ContactNumber,
            };
        }

        public static Users ToEntity(this UsersDto dto)
        {
            if (dto == null)
                return null;

            return new Users
            {
                UserId = dto.UserId,
                UserTypeId = dto.UserTypeId,
                PhotoStorageId = dto.PhotoStorageId,
                UserName = dto.UserName,
                Password = dto.Password,
                EmailAddress = dto.EmailAddress,
                ContactNumber = dto.ContactNumber,
            };
        }

        public static Menus ToEntity(this MenusDto dto)
        {
            if (dto == null)
                return null;

            return new Menus
            {
                MenuId = dto.MenuId,
                ParentMenuId = dto.ParentMenuId,
                MenuNameEnglish = dto.MenuNameEnglish?.Trim(),
                MenuNameNepali = dto.MenuNameNepali?.Trim(),
                CheckMenuName = dto.CheckMenuName?.Trim(),
                MenuLink = dto.MenuLink?.Trim(),
                MenuOrder = dto.MenuOrder,
                MenuIcon = dto.MenuIcon?.Trim(),
                Status=dto.Status,
            };
        }

        public static Staffs ToEntity(this StaffsDto dto)
        {
            if (dto == null)
                return null;

            return new Staffs
            {
                StaffId=dto.StaffId,
                UserId = dto.UserId,
                RoleId = dto.RoleId,
                DesignationId = dto.DesignationId,
                DepartmentId = dto.DepartmentId,
                StaffName = dto.StaffName,
                GenderId = dto.GenderId,
                TemporaryAddress = dto.TemporaryAddress,
                PermanentAddress = dto.PermanentAddress,
                CitizenshipNumber = dto.CitizenshipNumber,
                PanNumber = dto.PanNumber,
                BasicSalary = dto.BasicSalary,
            };
        }

        public static Users ToUserEntity(this StaffsDto dto)
        {
            if (dto == null)
                return null;

            return new Users
            {
                UserId=dto.UserId,
                UserTypeId = 2,
                PhotoStorageId = dto.PhotoStorageId,
                UserName=dto.UserName,
                Password=dto.Password,
                EmailAddress=dto.EmailAddress,
                ContactNumber=dto.ContactNumber,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                UpdatedBy = dto.UpdatedBy,
                UpdatedDate = dto.UpdatedDate,
                UserStatusId = dto.UserStatusId,
            };
        }

        public static MenuAccessPermission ToEntity(this MenuAccessPermissionDto dto)
        {
            if (dto == null)
                return null;

            return new MenuAccessPermission
            {
                MenuAccessPermissionId=dto.MenuAccessPermissionId,
                MenuId=dto.MenuId,
                StaffId = dto.StaffId,
                ReadAccess=dto.ReadAccess,
                WriteAccess=dto.WriteAccess,
                ModifyAccess=dto.ModifyAccess,
                DeleteAccess=dto.DeleteAccess,
                AdminAccess=dto.AdminAccess,
            };
        }

        public static Role ToEntity(this RoleDto dto)
        {
            if (dto == null)
                return null;

            return new Role
            {
                RoleId=dto.RoleId,
                RoleName = dto.RoleName,
                Status = dto.Status,
                CreatedDate = dto.CreatedDate,
            };
        }

        public static Designation ToEntity(this DesignationDto dto)
        {
            if (dto == null)
                return null;

            return new Designation
            {
                DesignationId = dto.DesignationId,
                DesignationName = dto.DesignationName,
                Status = dto.Status,
                CreatedDate = dto.CreatedDate,
            };
        }

        public static Department ToEntity(this DepartmentDto dto)
        {
            if (dto == null)
                return null;

            return new Department
            {
                DepartmentId = dto.DepartmentId,
                DepartmentName = dto.DepartmentName,
                Status = dto.Status,
                CreatedDate = dto.CreatedDate,
            };
        }
    }
}
