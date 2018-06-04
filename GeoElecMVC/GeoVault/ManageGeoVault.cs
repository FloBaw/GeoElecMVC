using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;

using GeoElecMVC.Models;

namespace GeoElecMVC.GeoVault
{
    public class ManageGeoVault : IGeoVault<Vault>
    {
        private string connectionString;
        public ManageGeoVault(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public IEnumerable<Vault> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Vault>("SELECT * FROM olidata_frame_generator order by log_date desc");
                //return dbConnection.Query<Vault>("SELECT * FROM olidata_frame_generator order by log_date asc");
                //return dbConnection.Query<Vault>("SELECT * FROM olidata_frame_generator order by log_id");
                //return dbConnection.Query<Vault>("SELECT * FROM olidata_frame_generator");
            }
        }
    }
}
