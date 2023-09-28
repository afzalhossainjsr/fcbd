using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Common
{
    public interface IDataManager
    {
        Task<List<Dictionary<string, string>>> DataTableToDictionaryList(DataTable dt);
        Task<DataTable> ListToDataTable<T>(List<T> items);
        Task<DataTable> MapListToList<TInput, TOutput>(List<TInput> inputList);
        Task<DataSet> ReturnDataSet(string SpName, params object[] parameters);
        Task<DataTable> ReturnDataTableByQuery(string query);
        Task<DataTable> ReturnDataTableBySP(string SpName, params object[] parameters);
        Task<List<T>> ReturnDataTableToList<T>(DataTable dataTable) where T : class, new();
        Task<List<Dictionary<string, string>>> ReturnDictionaryListBySP(string SpName, params object[] parameters);
        Task<List<T>> ReturnListBySP<T>(string SpName, params object[] parameters) where T : class, new();
        Task<List<T>> ReturnListBySP2<T>(string SpName, params object[] parameters) where T : class, new();
        Task<ResultObject> SaveDataBySP(string SpName, params object[] parameters);
        Task<OTPMessageResultModel> SendBulkMessage(string[] phonenumber, string messageText);
        Task<OTPMessageResultModel> SendSingleMessage(string phonenumber, string messageText);
    }
}
