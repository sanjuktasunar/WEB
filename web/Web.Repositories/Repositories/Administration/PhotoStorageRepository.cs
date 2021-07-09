using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Database.BaseRepo;
using Web.Entity.Entity;

namespace Web.Repositories.Repositories.Administration
{
    public interface IPhotoStorageRepository
    {
        int Insert(PhotoStorages entity, IDbTransaction transaction, SqlConnection con);
        int Update(PhotoStorages entity, IDbTransaction transaction, SqlConnection con);
    }

    public class PhotoStorageRepository:IPhotoStorageRepository
    {
        BaseRepo<PhotoStorages> _photoStorageRepoRepo = new BaseRepo<PhotoStorages>();
      
        public int Insert(PhotoStorages entity, IDbTransaction transaction,SqlConnection con)
        {
            return (_photoStorageRepoRepo.Insert(entity,transaction,con));
        }

        public int Update(PhotoStorages entity, IDbTransaction transaction,SqlConnection con)
        {
            return (_photoStorageRepoRepo.Update(entity,transaction,con));
        }
    }
}
