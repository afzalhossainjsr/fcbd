using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Basic
{
    public interface ILocationDAL
    {
        Task<List<Dictionary<string, string>>> GetDivisionToThana();
    }
}
