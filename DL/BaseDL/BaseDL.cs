using Common.Other;
using Dapper;
using DL.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        private readonly DapperContext _dapperContext;
        public BaseDL(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public object Get()
        {
            try
            {
                var sqlCommmand = $"select * from {typeof(T).Name}";
                using (var connection = _dapperContext.CreateConnection())
                {
                    var result = connection.Query<T>(sqlCommmand);

                    connection.Close();

                    return new Result
                    (
                        true,
                       "Lay danh sach doi tuong thanh cong",
                        result
                    );
                }
            }
            catch
            {
                throw new Exception("Co loi xay ra vui long lien he quan tri vien");
            }

        }

        public object GetById(Guid id)
        {
            try
            {
                var sqlCommmand = $"select * from {typeof(T).Name} where {typeof(T).Name}ID = @id";
                using (var connection = _dapperContext.CreateConnection())
                {
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("id", id);
                    var result = connection.QueryFirstOrDefault<T>(sqlCommmand, dynamicParams);
                    connection.Close();

                    return new Result
                   (
                       true,
                      "Lay thanh cong qua id",
                       result
                   );
                }
            }
            catch
            {
                throw new Exception("Co loi xay ra vui long lien he quan tri vien");
            }
        }
        public object DeleteById(Guid id)
        {
            try
            {
                var sqlCommmand = $"Delete from {typeof(T).Name} where {typeof(T).Name}ID = @id";
                using (var connection = _dapperContext.CreateConnection())
                {
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("id", id);
                    var result = connection.Execute(sqlCommmand, dynamicParams, commandType: CommandType.Text);
                    connection.Close();
                    return new Result
                    (
                        true,
                       "Xoa thanh cong",
                        result
                    );
                }
            }
            catch
            {
                throw new Exception("Co loi xay ra vui long lien he quan tri vien");
            }
        }

        public object Post(T record)
        {
            try
            {
                var procedureName = "Insert" + typeof(T).Name;
                using (var connection = _dapperContext.CreateConnection())
                {
                    var dynamicParams = new DynamicParameters();

                    PropertyInfo[] properties = typeof(T).GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        var attribute = property.Name;
                        var value = property.GetValue(record, null);
                        dynamicParams.Add(attribute, value);
                    }

                    // Generate a new GUID
                    Guid newGuid = Guid.NewGuid();
                    dynamicParams.Add($"{typeof(T).Name}ID", newGuid);

                    // Sử dụng Dapper để gọi thủ tục
                    var result = connection.Execute(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);

                    connection.Close();

                    return new Result
                    (
                        true,
                       "Them thanh cong",
                        result
                    );
                }
            }
            catch
            {
                throw new Exception("Co loi xay ra vui long lien he quan tri vien");
            }
        }

        public object Put(T record, Guid id)
        {
            try
            {
                var procedureName = "Update" + typeof(T).Name;

                using (var connection = _dapperContext.CreateConnection())
                {
                    var dynamicParams = new DynamicParameters();

                    dynamicParams.Add($"{typeof(T).Name}ID", id);

                    PropertyInfo[] properties = typeof(T).GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        var attribute = property.Name;
                        var value = property.GetValue(record, null);
                        dynamicParams.Add(attribute, value);
                    }

                    
                    var result = connection.Execute(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);

                    connection.Close();

                    return new Result
                    (
                        true,
                       "Sua thanh cong",
                        result
                    );
                }
            }
            catch
            {
                throw new Exception("Co loi xay ra vui long lien he quan tri vien");
            }
        }

        //public object PagingAndSearch(int pageIndex, int pageSize, string? keyWord)
        //{
        //    try
        //    {
        //        var procedureName = $"PagingAndSearch{typeof(T).Name}";
        //        using (var connection = _dapperContext.CreateConnection())
        //        {
        //            var dynamicParams = new DynamicParameters();
        //            dynamicParams.Add("PageIndex", pageIndex);
        //            dynamicParams.Add("PageSize", pageSize);
        //            dynamicParams.Add("KeyWord", keyWord);
        //            dynamicParams.Add("@Count", direction: ParameterDirection.Output);

        //            var result = connection.Query<T>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
        //            var total = dynamicParams.Get<Int32>("@Count");
        //            connection.Close();
        //            return new Result
        //            (
        //                true,
        //               "Lay du lieu thanh cong",
        //                new
        //                {
        //                    total, result
        //                }
        //            );
        //        }
        //    }
        //    catch
        //    {
        //        throw new Exception("Co loi xay ra vui long lien he quan tri vien");
        //    }
        //}
    }
}
