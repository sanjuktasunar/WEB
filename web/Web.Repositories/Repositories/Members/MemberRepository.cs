using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Database;
using Web.Database.BaseRepo;
using Web.Entity.Dto;
using Web.Entity.Entity;
using Web.Entity.Infrastructure;

namespace Web.Repositories.Repositories.Members
{
    public interface IMemberRepository
    {
        Task<bool> CheckCitizenshipNumber(int MemberId, string CitizenshipNumber);
        int Insert(Member entity, IDbTransaction transaction, SqlConnection con);
        int InsertMemberDetails(MemberDetails entity, IDbTransaction transaction, SqlConnection con);
        Task<string> GetMemberCode();
        Task<Member> GetMemberById(int memberId);
        Task<Address> GetMemberAddressById(int memberId);
        int Update(Member entity);
        int InsertAddress(Address entity);
        int UpdateAddress(Address entity);
        int InsertMemberDocument(UserDocuments entity);
        int UpdateMemberDocument(UserDocuments entity);
        Task<bool> CheckPhoneNumber(int MemberId, string MobileNumber);
        Task<bool> CheckEmail(int MemberId, string Email);
    }

    public class MemberRepository:IMemberRepository
    {
        private readonly IDapperManager _dapperManager;
        BaseRepo<Member> _memberRepo = new BaseRepo<Member>();
        BaseRepo<MemberDetails> _memberDetailsRepo = new BaseRepo<MemberDetails>();
        BaseRepo<Address> _addressRepo = new BaseRepo<Address>();
        BaseRepo<UserDocuments> _userDocumentsRepo = new BaseRepo<UserDocuments>();
        public MemberRepository(IDapperManager dapperManager)
        {
            _dapperManager = dapperManager;
        }

        public int Insert(Member entity, IDbTransaction transaction, SqlConnection con)
        {
            entity.CreatedDate = DateTime.Now;
            entity.FormStatus = FormStatus.Incomplete;
            entity.IsMemberFilled = true;
            entity.ApprovalStatus = ApprovalStatus.UnApproved;
            return (_memberRepo.Insert(entity, transaction, con));
        }
        public int InsertMemberDetails(MemberDetails entity, IDbTransaction transaction, SqlConnection con)
        {
            entity.CreatedDate = DateTime.Now;
            return (_memberDetailsRepo.Insert(entity, transaction, con));
        }

        public int Update(Member entity)
        {
            return (_memberRepo.Update(entity));
        }

        public int InsertAddress(Address entity)
        {
            return (_addressRepo.Insert(entity));
        }

        public int UpdateAddress(Address entity)
        {
            return (_addressRepo.Update(entity));
        }

        public int InsertMemberDocument(UserDocuments entity)
        {
            return (_userDocumentsRepo.Insert(entity));
        }

        public int UpdateMemberDocument(UserDocuments entity)
        {
            return (_userDocumentsRepo.Update(entity));
        }

        public async Task<string> GetMemberCode()
        {
            var members = (await _dapperManager.QueryAsync<Member>("SELECT TOP 1 * FROM Member ORDER BY MemberId DESC"));
            DateTime currentDate = DateTime.Now;
            string memberCode = "";
            int i = 1;
            if (members.Count() > 0)
            {
                var number = members.FirstOrDefault().MemberCode.Split('-');
                i = Convert.ToInt32(number[2]);
                i=i+ 1;
            }
            memberCode= "BKP-" + currentDate.Year + "-"+i;
            return memberCode;
        }

        public async Task<Member> GetMemberById(int memberId)
        {
            return await _dapperManager.QuerySingleAsync<Member>("SELECT * FROM Member WHERE MemberId=@id",new { id=memberId });
        }

        public async Task<Address> GetMemberAddressById(int memberId)
        {
            return await _dapperManager.QuerySingleAsync<Address>("SELECT * FROM Address WHERE MemberId=@id", new { id = memberId });
        }

        public async Task<UserDocumentDto> GetMemberDocumentsById(int memberId)
        {
            return await _dapperManager.QuerySingleAsync<UserDocumentDto>("SELECT * FROM [dbo].[MemberDocumentView] WHERE MemberId=@id", new { id = memberId });
        }

        public async Task<bool> CheckCitizenshipNumber(int MemberId, string CitizenshipNumber)
        {
            var obj = await _dapperManager.QueryAsync<MemberDto>("SELECT TOP 1 * FROM Member WHERE MemberId!=@MemberId AND CitizenshipNumber=@CitizenshipNumber", new { MemberId, CitizenshipNumber });
            if (obj.Count() > 0)
                return true;
            return false;
        }
        public async Task<bool> CheckPhoneNumber(int MemberId, string MobileNumber)
        {
            var obj = await _dapperManager.QueryAsync<MemberDto>("SELECT TOP 1 * FROM Member WHERE MemberId!=@MemberId AND MobileNumber=@MobileNumber", new { MemberId, MobileNumber });
            if (obj.Count() > 0)
                return true;
            return false;
        }

        public async Task<bool> CheckEmail(int MemberId, string Email)
        {
            var obj = await _dapperManager.QueryAsync<MemberDto>("SELECT * FROM Member WHERE MemberId!=@MemberId AND Email=@Email", new { MemberId, Email });
            if (obj.Count() > 0)
                return true;
            return false;
        }
    }
}
