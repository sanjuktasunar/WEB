using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Repositories
{
    public interface IBaseRepo<TModel> where TModel : class, new()
    {

    }
}
