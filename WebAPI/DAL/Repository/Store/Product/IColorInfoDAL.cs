using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Common;
using Model.Store.Product;

namespace DAL.Repository.Store.Product
{
    public interface IColorInfoDAL
    {
        Task<ResultObject> DeleteColorInfo(string? userName, int? Id);
        Task<ResultObject> SaveColorInfo(string? userName, StoreInfoModel obj);
        Task<ResultObject> UpdateColorInfo(string? userName, StoreInfoModel obj);
    }
}
