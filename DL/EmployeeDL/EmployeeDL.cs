using Common.DTO;
using Common.Entities;
using Common.Other;
using Dapper;
using DL.BaseDL;
using DL.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DL.EmployeeDL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        private readonly DapperContext _dapperContext;
        public EmployeeDL(DapperContext dapperContext) : base(dapperContext)
        {
            _dapperContext = dapperContext;
        }
        public string GetNewEmployeeCode()
        {
            try
            {
                var procedureName = "GenerateNewEmployeeCode";
                using (var connection = _dapperContext.CreateConnection())
                {
                    var result = connection.Query<string>(procedureName, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                    connection.Close();
                    return result;
                }
            }
            catch
            {
                throw new Exception("Co loi xay ra vui long lien he quan tri vien");
            }
        }

        public object PagingAndSearch(int pageIndex, int pageSize, string? keyWord)
        {
            try
            {
                var procedureName = $"PagingAndSearchEmployee";
                using (var connection = _dapperContext.CreateConnection())
                {
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("PageIndex", pageIndex);
                    dynamicParams.Add("PageSize", pageSize);
                    dynamicParams.Add("KeyWord", keyWord);
                    dynamicParams.Add("@Count", direction: ParameterDirection.Output);

                    var result = connection.Query<EmployeeDTO>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
                    var total = dynamicParams.Get<Int32>("@Count");
                    connection.Close();
                    return new Result
                    (
                        true,
                       "Lay du lieu thanh cong",
                        new
                        {
                            total,
                            result
                        }
                    );
                }
            }
            catch
            {
                throw new Exception("Co loi xay ra vui long lien he quan tri vien");
            }
        }

        public object DeleteMulty(string[] arrayID)
        {
            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();

                string sqlCommmand = $"DELETE FROM employee e WHERE e.EmployeeID IN @arrayID";
                //IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("arrayID", arrayID);
                    var result = connection.Execute(sqlCommmand, dynamicParams, commandType: CommandType.Text);
                    //transaction.Commit();
                    connection.Close();

                    return new Result
                   (
                       true,
                      "Xoa thanh cong",
                       result
                   );
                }
                catch
                {
                    //transaction.Rollback();
                    connection.Close();
                    throw new Exception("Co loi xay ra vui long lien he quan tri vien");

                }
            }
        }

        public IEnumerable<Employee> ExportExcel(string query = "")
        {
            try
            {
                var procedureName = "ExportExcel";
                using (var connection = _dapperContext.CreateConnection())
                {
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("keyWord", query);
                    var result = connection.Query<Employee>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
                    connection.Close();
                    return result;
                }
            }
            catch
            {
                throw new Exception("Co loi xay ra vui long lien he quan tri vien");

            }
        }
    }
}
