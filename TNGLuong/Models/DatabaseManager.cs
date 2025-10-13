using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace TNGLuong.Models
{
    public class DatabaseManager
    {
        private readonly DbContext _context;

        public DatabaseManager(DbContext context)
        {
            _context = context;
        }

        public DataSet ExecuteStoredProcedure(string storedProcedureName, StoredParameterCollection parameters)
        {
            var dataSet = new DataSet();

            using (var sqlConnection = new SqlConnection(_context.Database.Connection.ConnectionString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandText = storedProcedureName;
                    command.CommandType = CommandType.StoredProcedure;

                    for (int i = 0; i < parameters.Count; i++)
                    {
                        var spParameter = parameters.Item(i);
                        var sqlParameter = new SqlParameter
                        {
                            ParameterName = spParameter.ParameterName,
                            Direction = spParameter.ParameterDirection,
                            SqlDbType = spParameter.ParameterType,
                            Value = spParameter.ParameterValue,
                            Size = spParameter.ParameterSize
                        };

                        if (spParameter.ParameterSize > 0)
                        {
                            sqlParameter.Size = spParameter.ParameterSize;
                        }
                        command.Parameters.Add(sqlParameter);
                    }
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataSet);
                    }
                }
            }
            return dataSet;
        }
    }
}