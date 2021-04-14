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
    }
}
