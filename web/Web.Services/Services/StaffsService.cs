using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Web.Entity.Dto;
using Web.Entity.Entity;
using Web.Repositories.Interface;
using Web.Repositories.Repositories.Administration;
using Web.Repositories.Utitlities;
using Web.Services.Mapping;

namespace Web.Services.Services
{
    public interface IStaffsService
    {
        Task<IEnumerable<StaffsDto>> GetStaffListAsync();
        Task<StaffsDto> GetStaffsByIdAsync(int id);
        string Insert(StaffsDto dto);
        Task<StaffsDto> DropDownMethods(StaffsDto dto);
        Task<string> Update(StaffsDto dto);
        //Task<StaffsDto> MenuAccessPermissionAsync(int staffId);
        //Task<string> AddMenuAccess(IEnumerable<MenuAccessPermissionDto> dtos);
    }

    public class StaffsService:IStaffsService
    {
        IStaffsRepository _staffsRepository;
        IMenusService _menusService;
        IPhotoStorageRepository _photoStorageRepository;
        IUsersRepository _usersRepository;
        IMessageClass _messageClass;
        private IBaseInterface _baseInterface;
        IAdministrationService _administrationService;
        public StaffsService(
            IStaffsRepository staffsRepository,
            IPhotoStorageRepository photoStorageRepository,
            IUsersRepository usersRepository,
            IMessageClass messageClass,
            IBaseInterface baseInterface,
            IAdministrationService administrationService,
            IMenusService menusService)
        {
            _staffsRepository = staffsRepository;
            _photoStorageRepository = photoStorageRepository;
            _messageClass = messageClass;
            _usersRepository = usersRepository;
            _baseInterface = baseInterface;
            _administrationService = administrationService;
            _menusService = menusService;
        }

        public async Task<IEnumerable<StaffsDto>> GetStaffListAsync()
        {
            return (await _staffsRepository.GetStaffListAsync());
        }

        public async Task<StaffsDto> GetStaffsByIdAsync(int id)
        {
            return (await _staffsRepository.GetStaffByIdAsync(id));
        }

        public string Insert(StaffsDto dto)
        {
            var conn = _baseInterface.GetConnection();
            var transaction = conn.BeginTransaction();
            string message = "";
            try
            {
                var photoStorage = new PhotoStorages();
                photoStorage.Photo = null;
                photoStorage.PhotoLocation = null;
                int photoStorageId= _photoStorageRepository.Insert(photoStorage,transaction, conn);

                var user = dto.ToUserEntity();
                user.PhotoStorageId = photoStorageId;
                user.Password = Web.Repositories.Utitlities.Security.GetMd5Sum(dto.Password);
                user.CreatedDate = DateTime.Now;
                user.CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                int userId = _usersRepository.Insert(user,transaction,conn);

                var staff = dto.ToEntity();
                staff.UserId = userId;
                int staffId = _staffsRepository.Insert(staff, transaction,conn);

                message = _messageClass.ShowSuccessMessage(staffId);
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
                transaction.Rollback();
            }
            return message;
        }

        public async Task<string> Update(StaffsDto dto)
        {
            SqlTransaction transaction;
            var conn = _baseInterface.GetConnection();
            transaction = conn.BeginTransaction();
            string message = "";
            try
            {
                var staff =await _staffsRepository.GetStaffByIdAsync(dto.StaffId);
                if (staff == null)
                    return null;
                var user =await _usersRepository.GetUserByIdAsync(staff.UserId);

                user.UserName = dto.UserName;
                user.EmailAddress = dto.EmailAddress;
                user.ContactNumber = dto.ContactNumber;
                user.UserStatusId = dto.UserStatusId;
                user.UpdatedBy =Convert.ToInt32(HttpContext.Current.Session["UserId"]);
                user.UpdatedDate = DateTime.Now;
                _usersRepository.Update(user, transaction, conn);

                var staffEntity = dto.ToEntity();
                staffEntity.UserId = user.UserId;
                int staffId = _staffsRepository.Update(staffEntity, transaction, conn);

                message = _messageClass.ShowSuccessMessage(staffId);
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                message = _messageClass.ShowErrorMessage(string.Format("{0} ~ {1}", ex.Number.ToString(), ex.Message));
                transaction.Rollback();
            }
            return message;
        }

        public async Task<StaffsDto> DropDownMethods(StaffsDto dto)
        {
            if (dto is null)
                dto = new StaffsDto();

            dto.Roles = await _administrationService.GetActiveRoleAsync();
            dto.Designations = await _administrationService.GetActiveDesignationAsync();
            dto.Departments = await _administrationService.GetActiveDepartmentAsync();
            dto.Genders = await _administrationService.GetActiveGenderAsync();
            dto.UserStatus = await _administrationService.GetActiveUserStatusAsync();
            return dto;
        }
    }
}
