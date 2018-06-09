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

        public IEnumerable<Vault> getAllGenFram(string searchid)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Vault>("SELECT olidata_frame_generator.log_id, " +
                    "olidata_frame_generator.log_timestamp, " +
                    "olidata_frame_generator.log_date, olidata_frame_generator.generator_id, " +
                    "olidata_frame_generator.nrj_tot, " +
                    "olidata_frame_generator.power_1, " +
                    "olidata_frame_generator.power_2, " +
                    "olidata_frame_generator.power_3, " +
                    "olidata_frame_generator.current_1, " +
                    "olidata_frame_generator.current_2, " +
                    "olidata_frame_generator.current_3, " +
                    "olidata_frame_generator.voltage, " +
                    "olidata_frame_generator.relay_1, " +
                    "olidata_frame_generator.relay_2, " +
                    "olidata_frame_generator.magnetic, " +
                    "olidata_frame_generator.latitude, " +
                    "olidata_frame_generator.longitude, " +
                    "olidata_frame_generator.payment " +
                    "FROM oliclient, oligenerator, olidata_frame_generator " +
                    "WHERE oligenerator.generator_id = olidata_frame_generator.generator_id " +
                    "and oligenerator.client_id = oliclient.client_id " +
                    "and oligenerator.generator_id LIKE Concat(@generator_id,'%') " +
                    "order by log_date desc",
                    new { generator_id = searchid});
            }
        }

        public IEnumerable<Vault> getAllGenFram (DateTime datebegin, DateTime dateend)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Vault>("SELECT olidata_frame_generator.log_id, " +
                    "olidata_frame_generator.log_timestamp, " +
                    "olidata_frame_generator.log_date, olidata_frame_generator.generator_id, " +
                    "olidata_frame_generator.nrj_tot, " +
                    "olidata_frame_generator.power_1, " +
                    "olidata_frame_generator.power_2, " +
                    "olidata_frame_generator.power_3, " +
                    "olidata_frame_generator.current_1, " +
                    "olidata_frame_generator.current_2, " +
                    "olidata_frame_generator.current_3, " +
                    "olidata_frame_generator.voltage, " +
                    "olidata_frame_generator.relay_1, " +
                    "olidata_frame_generator.relay_2, " +
                    "olidata_frame_generator.magnetic, " +
                    "olidata_frame_generator.latitude, " +
                    "olidata_frame_generator.longitude, " +
                    "olidata_frame_generator.payment " +
                    "FROM oliclient, oligenerator, olidata_frame_generator " +
                    "WHERE oligenerator.generator_id = olidata_frame_generator.generator_id " +
                    "and oligenerator.client_id = oliclient.client_id " +
                    "and log_date <= @dateEnd " +
                    "and log_date >= @dateBegin " +
                    "order by log_date desc",
                    new {dateBegin = datebegin, dateEnd = dateend });
            }
        }

        public IEnumerable<Vault> getAllGenFram(string searchid, DateTime datebegin, DateTime dateend)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Vault>("SELECT olidata_frame_generator.log_id, " +
                    "olidata_frame_generator.log_timestamp, " +
                    "olidata_frame_generator.log_date, olidata_frame_generator.generator_id, " +
                    "olidata_frame_generator.nrj_tot, " +
                    "olidata_frame_generator.power_1, " +
                    "olidata_frame_generator.power_2, " +
                    "olidata_frame_generator.power_3, " +
                    "olidata_frame_generator.current_1, " +
                    "olidata_frame_generator.current_2, " +
                    "olidata_frame_generator.current_3, " +
                    "olidata_frame_generator.voltage, " +
                    "olidata_frame_generator.relay_1, " +
                    "olidata_frame_generator.relay_2, " +
                    "olidata_frame_generator.magnetic, " +
                    "olidata_frame_generator.latitude, " +
                    "olidata_frame_generator.longitude, " +
                    "olidata_frame_generator.payment " +
                    "FROM oliclient, oligenerator, olidata_frame_generator " +
                    "WHERE oligenerator.generator_id = olidata_frame_generator.generator_id " +
                    "and oligenerator.client_id = oliclient.client_id " +
                    "and oligenerator.generator_id LIKE Concat(@generator_id,'%') " +
                    "and log_date <= @dateEnd " +
                    "and log_date >= @dateBegin " +
                    "order by log_date desc",
                    new { generator_id = searchid, dateBegin = datebegin, dateEnd = dateend });
            }
        }

        // En cours
        public float getNrjTot(string searchid, DateTime datebegin, DateTime dateend)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Execute("SELECT sum(olidata_frame_generator.nrj_tot) as tot_nrj " +
                    "FROM oliclient, oligenerator, olidata_frame_generator " +
                    "WHERE oligenerator.generator_id = olidata_frame_generator.generator_id " +
                    "and oligenerator.client_id = oliclient.client_id " +
                    "and oligenerator.generator_id = @generator_id " +
                    "and log_date <= @dateEnd " +
                    "and log_date >= @dateBegin",
                    new { generator_id = searchid, dateBegin = datebegin, dateEnd = dateend });
            }
        }
    }
}
