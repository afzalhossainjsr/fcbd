using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace DAL.Common
{
    public class DataManager : IDataManager
    {
        private readonly SqlConnection _myconn;
        private readonly EmailConfiguration _emailConfig;
        public DataManager(EmailConfiguration emailConfig)
        {
            _myconn = new SqlConnection(GetConfiguration().GetSection("Data").GetSection("DefaultConnection").Value);
            _emailConfig = emailConfig;
        }
        private IConfigurationRoot GetConfiguration()
        {
            ///Solving Issue 
            ///https://stackoverflow.com/questions/36001695/setting-base-path-using-configurationbuilder
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
            return builder.Build();
        }
        public async Task<List<T>> ReturnListBySP<T>(string SpName, params object[] parameters) where T : class, new()
        {
            var Conn =  _myconn;
            var lst = new List<T>();
            try
            {
                using (var command = Conn.CreateCommand())
                {
                    command.CommandText = SpName;
                    command.CommandType = CommandType.StoredProcedure;
                    Conn.Open();
                    if (parameters != null)
                        foreach (var p in parameters)
                            command.Parameters.Add(p);

                    using (var reader = command.ExecuteReader())
                    {
                        var lstColumns = new T().GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
                        while (reader.Read())
                        {
                            var newObject = new T();
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                var name = reader.GetName(i);
                                PropertyInfo? prop = lstColumns.Find(a => a.Name.ToLower().Equals(name.ToLower()));
                                if (prop == null)
                                {
                                    continue;
                                }
                                var val = reader.IsDBNull(i) ? null : reader[i];
                                prop.SetValue(newObject, val?.ToString(), null);
                            }
                            lst.Add(newObject);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message?.ToString();
            }
            finally
            {
                Conn.Close();
            }
            return await Task.Run(() => lst);
        }

        public async Task<List<T>> ReturnListBySP2<T>(string SpName, params object[] parameters) where T : class, new()
        {
            var Conn = _myconn;
            var lst = new List<T>();
            try
            {
                using (var command = Conn.CreateCommand())
                {
                    command.CommandText = SpName;
                    command.CommandType = CommandType.StoredProcedure;
                    Conn.Open();
                    if (parameters != null)
                        foreach (var p in parameters)
                            command.Parameters.Add(p);

                    using (var reader = command.ExecuteReader())
                    {

                        var lstColumns = new T().GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).ToList();
                        while (reader.Read())
                        {
                            var newObject = new T();
                            for (var i = 0; i < reader.FieldCount; i++)
                            {
                                var name = reader.GetName(i);
                                PropertyInfo? prop = lstColumns.Find(a => a.Name.ToLower().Equals(name.ToLower()));
                                if (prop == null)
                                {
                                    continue;
                                }
                                var val = reader.IsDBNull(i) ? null : reader[i];
                                prop.SetValue(newObject, val, null);
                            }
                            lst.Add(newObject);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message?.ToString();
            }
            finally
            {
                Conn.Close();
            }
            return await Task.Run(() => lst);
        }

        public async Task<List<T>> ReturnDataTableToList<T>(DataTable dataTable) where T : class, new()
        {
            var modelList = new List<T>();

            try
            {
                var dataTableColumns = dataTable.Columns.Cast<DataColumn>().ToList();
                var modelProperties = typeof(T).GetProperties().ToList();
                var matchingColumnsAndProperties = dataTableColumns.Join(modelProperties,
                    c => c.ColumnName,
                    p => p.Name,
                    (c, p) => new { Column = c, Property = p });
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    var model = new T();
                    foreach (var match in matchingColumnsAndProperties)
                    {
                        var value = dataRow[match.Column];
                        if (value != DBNull.Value)
                        {
                            match.Property.SetValue(model, value, null);
                        }
                    }
                    modelList.Add(model);
                }
            }
            catch (Exception ex)
            {
                ex.Message?.ToString();
            }
            return await Task.Run(() => modelList);
        }

        
      ///result = await ReturnDataTableToList<Base>(dt); 
      
        public async Task<DataSet> ReturnDataSet(string SpName, params object[] parameters)
        {
            var Conn = _myconn;
            DataSet ds = new DataSet();

            try
            {
                using (var command = Conn.CreateCommand())
                {
                    command.CommandText = SpName;
                    command.CommandType = CommandType.StoredProcedure;
                    Conn.Open();
                    if (parameters != null)
                        foreach (var p in parameters)
                            command.Parameters.Add(p);
                    SqlDataAdapter adapt = new SqlDataAdapter(command);
                    adapt.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                ex.Message?.ToString();
            }
            finally
            {
                Conn.Close();
            }
            return await Task.Run(() => ds);
        }

        
        ////result = await ReturnDatSet(@"SP_Get_CompleteJobListWithAmount", parameters);
       

        public async Task<List<Dictionary<string, string>>> ReturnDictionaryListBySP(string SpName, params object[] parameters)
        {
            var Conn = _myconn;
            var dt = new DataTable();
            List<Dictionary<string, string>> _objList = new List<Dictionary<string, string>>();
            try
            {
                using (var command = Conn.CreateCommand())
                {
                    command.CommandText = SpName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 600000;
                    Conn.Open();
                    if (parameters != null)
                        foreach (var p in parameters)
                            command.Parameters.Add(p);
                    using (var reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                        foreach (DataRow d in dt.Rows)
                        {
                            Dictionary<string, string> list = new Dictionary<string, string>();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                list[dc.ToString()] = d[dc].ToString();
                            }
                            _objList.Add(list);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ex.ToString();
            }
            finally
            {
                Conn.Close();
            }
            return await Task.Run(() => _objList);
        }
       
        public async Task<DataTable> ReturnDataTableBySP(string SpName, params object[] parameters)
        {
            var Conn = _myconn;
            var dt = new DataTable();
            try
            {
                using (var command = Conn.CreateCommand())
                {
                    command.CommandText = SpName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 1200; //20 minutes = 1200 seconds
                    Conn.Open();
                    if (parameters != null)
                        foreach (var p in parameters)
                            command.Parameters.Add(p);
                    SqlDataReader dr = command.ExecuteReader();
                    dt.Load(dr);
                }
            }
            catch (SqlException ex)
            {
                ex.ToString();
            }
            finally
            {
                Conn.Close();
            }
            return await Task.Run(() => dt);
        }
        public async Task<DataTable> ReturnDataTableByQuery(string query)
        {
            var Conn = _myconn;
            var dt = new DataTable();
            try
            {
                using (var command = Conn.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 1200; //20 minutes = 1200 seconds
                    Conn.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    dt.Load(dr);
                }
            }
            catch (SqlException ex)
            {
                ex.ToString();
            }
            finally
            {
                Conn.Close();
            }
            return await Task.Run(() => dt);
        }
        public async Task<DataTable> ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i]?.GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return await Task.Run(() => dataTable);
        }
        public async Task<List<Dictionary<string, string>>> DataTableToDictionaryList(DataTable dt)
        {

            List<Dictionary<string, string>>? _objList = new List<Dictionary<string, string>>();
            foreach (DataRow d in dt.Rows)
            {
                Dictionary<string, string> list = new Dictionary<string, string>();
                foreach (DataColumn dc in dt.Columns)
                {
                    list[dc.ToString()] = d[dc].ToString();
                }
                _objList.Add(list);
            }

            return await Task.Run(() => _objList);
        }
        public async Task<DataTable> MapListToList<TInput, TOutput>(List<TInput> inputList)
        {
            List<TOutput> outputList = new List<TOutput>();
            PropertyInfo[] outputProperties = typeof(TOutput).GetProperties();
            foreach (var inputItem in inputList)
            {
                TOutput outputItem = Activator.CreateInstance<TOutput>();
                foreach (var outputProperty in outputProperties)
                {
                    PropertyInfo inputProperty = typeof(TInput).GetProperty(outputProperty.Name);

                    if (inputProperty != null)
                    {
                        object value = inputProperty.GetValue(inputItem);
                        outputProperty.SetValue(outputItem, value);
                    }
                }
                outputList.Add(outputItem);
            }
            var result = await ListToDataTable(outputList);
            return await Task.Run(() => result);
        }

        public async Task<ResultObject> SaveDataBySP(string SpName, params object[] parameters)
        {
            var Conn = _myconn;
            ResultObject objResult = new ResultObject() { ResultID = "", Error = "", Message = "0", NoOfRows = "", ExMessage = "" };
            try
            {
                using (var cmd = Conn.CreateCommand())
                {
                    cmd.CommandText = SpName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                        foreach (var p in parameters)
                            cmd.Parameters.Add(p);
                    Conn.Open();
                    objResult.NoOfRows = cmd.ExecuteNonQuery().ToString();
                    objResult.Error = cmd.Parameters["@ErrNo"].Value.ToString();
                    objResult.ResultID = cmd.Parameters["@IdentityValue"].Value.ToString();

                    if (int.Parse(objResult.NoOfRows) > 0 && int.Parse(objResult.ResultID ?? "0") > 0)
                    {
                        objResult.Message = "Operation successful.";
                    }
                    else
                    {
                        if (objResult.Error == "-1")
                        {
                            objResult.Message = "Operation failed.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                objResult.ExMessage = ex.ToString();
                objResult.ResultID = "0";
                objResult.Error = "-1";
                objResult.Message = "Exception: Operation failed.";
            }
            finally
            {
                Conn.Close();
            }
            return await Task.Run(() => objResult);
        }
        
            //List<SqlParameter> parameterList = new List<SqlParameter>();
            //parameterList.Add(new SqlParameter("@SaveOption", SaveOption));
            //parameterList.Add(new SqlParameter("@ID", obj.ID));
            //parameterList.Add(new SqlParameter("@IdentityValue", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            //parameterList.Add(new SqlParameter("@ErrNo", SqlDbType.Int, 20, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null));
            //SqlParameter[] parameters = parameterList.ToArray();
            //result = dm.TExecuteNonQuery(cls.getConnection251(), SpName, parameters);
            //return Json(result);
         
       
        public async Task<OTPMessageResultModel> SendSingleMessage(string phonenumber, string messageText)
        {
            OTPMessageResultModel objResult = new OTPMessageResultModel() { statusCode = "", message = "-1" };
            var data = new OTPMessageModel
            {
                username = _emailConfig.message_username,
                password = _emailConfig.message_password,
                apicode = "1",
                msisdn = phonenumber,
                countrycode = _emailConfig.message_countrycode,
                cli = _emailConfig.message_cli,
                messagetype = "1",
                message = messageText,
                messageid = "0"
            };
            try
            {
                var jsonPayload = JsonConvert.SerializeObject(data);
                using (var client = new HttpClient())
                {
                    var apiUrl = @"https://gpcmp.grameenphone.com/ecmapigw/webresources/ecmapigw.v2"; // Replace with your API endpoint URL
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<OTPMessageResultModel>(responseContent);
                    if (result != null)
                    {
                        objResult = result;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return await Task.Run(() => objResult);
        }
        public async Task<OTPMessageResultModel> SendBulkMessage(string[] phonenumber, string messageText)
        {
            OTPMessageResultModel objResult = new OTPMessageResultModel() { statusCode = "", message = "-1" };
            var data = new OTPBulkMessageModel
            {
                username = _emailConfig.message_username,
                password = _emailConfig.message_password,
                apicode = "6",
                msisdn = phonenumber,
                countrycode = _emailConfig.message_countrycode,
                cli = _emailConfig.message_cli,
                messagetype = "1",
                message = messageText,
                messageid = "0"
            };
            try
            {
                var jsonPayload = JsonConvert.SerializeObject(data);
                using (var client = new HttpClient())
                {
                    var apiUrl = "https://gpcmp.grameenphone.com/ecmapigw/webresources/ecmapigw.v2"; // Replace with your API endpoint URL
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<OTPMessageResultModel>(responseContent);
                    if (result != null)
                    {
                        objResult = result;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return await Task.Run(() => objResult);
        }
    }

    public class ResultObject
    {
        public string? ResultID { get; set; }
        public string? Message { get; set; }
        public string? NoOfRows { get; set; }
        public string? Error { get; set; }
        public string? ExMessage { get; set; }
    }
    public class OTPMessageModel
    {

        public string? username { get; set; }
        public string? password { get; set; }
        public string? apicode { get; set; }
        public string? msisdn { get; set; }
        public string? countrycode { get; set; }
        public string? cli { get; set; }
        public string? messagetype { get; set; }
        public string? message { get; set; }
        public string? messageid { get; set; }
    }
    public class OTPBulkMessageModel
    {

        public string? username { get; set; }
        public string? password { get; set; }
        public string? apicode { get; set; }
        public string[]? msisdn { get; set; }
        public string? countrycode { get; set; }
        public string? cli { get; set; }
        public string? messagetype { get; set; }
        public string? message { get; set; }
        public string? messageid { get; set; }
    }
    public class OTPMessageResultModel
    {

        public string? statusCode { get; set; }
        public string? message { get; set; }

    }
    public class EmailConfiguration
    {
        public string? From { get; set; }
        public string? SmtpServer { get; set; }
        public int Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }




        public string? message_username { get; set; }
        public string? message_password { get; set; }
        public string? message_cli { get; set; }
        public string? message_countrycode { get; set; }
    }
    public class UserRegistrationInfoModel
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumberPrefix { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? GenderId { get; set; }
        public string? GenderName { get; set; }
        public string? UserImage { get; set; }

    }
    public class AspNetUsersSocialUserReferenceViewModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Status { get; set; }

    }
    public class AspNetUsersSocialUserReferenceSearchModel
    {
        public string? SocialUserId { get; set; }
        public string? Provider { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
   

}