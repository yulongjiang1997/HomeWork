using Timor.HomeWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timor.HomeWork.IService
{
    public interface IDbService
    {
        T QueryById<T>(int id) where T : BaseModel;


        List<T> QueryAll<T>() where T : BaseModel;

        bool Insert<T>(T model) where T : BaseModel;

        bool DeleteById<T>(int id) where T : BaseModel;

        bool UpdateById<T>(int id, T model) where T : BaseModel;
    }
}
