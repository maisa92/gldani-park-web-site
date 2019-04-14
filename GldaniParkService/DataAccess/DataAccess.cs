using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper; 
using GldaniParkService.Properties;

namespace GldaniParkService.DataAccess
{
    public class DataAccess<TEntity>
    {
        private readonly string _connectionString =  Settings.Default.ConnectionString;

        public void Save(string queryName, DynamicParameters entity)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Query(queryName, entity, commandType: CommandType.StoredProcedure);
                con.Close();
            }
        }


        public IEnumerable<TEntity> Get(string queryName, DateTime startDate, DateTime endDate)
        {
            IEnumerable<TEntity> result;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@StartDate", startDate);
                param.Add("@EndDate", endDate);
                result = con.Query<TEntity>(queryName, param, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            return result;
        }

        public IEnumerable<TEntity> Get(string queryName, string cardId)
        {
            IEnumerable<TEntity> result;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@CardId", cardId); 
                result = con.Query<TEntity>(queryName, param, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            return result;
        }

        public IEnumerable<TEntity> Get(string queryName)
        {
            IEnumerable<TEntity> result;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                var param = new DynamicParameters();
                result = con.Query<TEntity>(queryName, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            return result;
        }

        public TEntity GetById(string queryName, DynamicParameters param)
        {
            IEnumerable<TEntity> result;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open(); 
                result = con.Query<TEntity>(queryName, param, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            return result.ToList().FirstOrDefault();
        }

        public void DeleteById(string queryName, DynamicParameters param)
        {
            IEnumerable<TEntity> result;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                result = con.Query<TEntity>(queryName, param, commandType: CommandType.StoredProcedure);
                con.Close();
            }
        }
    }
}
