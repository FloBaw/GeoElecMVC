using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;

using GeoElecMVC.Models;

namespace GeoElecMVC.GeoGenerator
{
    public class ManageGeoGenerator: IGeoGenerator<Generator>
    {
        private string connectionString;
        public ManageGeoGenerator(IConfiguration configuration)
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

        public IEnumerable<Generator> FindAllGen()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Generator>("SELECT oligenerator.generator_id, " +
                    "oliclient.company, " +
                    "oliplace.address, " +
                    "oliplace.city, " +
                    "oliplace.country, " +
                    "oligenerator.start_date, " +
                    "oligenerator.end_date, " +
                    "oligenerator.maintenance, " +
                    "oligenerator.installation_type, " +
                    "oligenerator.specification_id, " +
                    "olilessee.name, " +
                    "olilessee.first_name " +
                    "FROM oligenerator, oliclient, oliplace, olilessee " +
                    "WHERE oligenerator.client_id = oliclient.client_id " +
                    "AND oligenerator.place_id = oliplace.place_id " +
                    "AND oligenerator.lessee_id=olilessee.lessee_id");
                //return dbConnection.Query<Generator>("SELECT * FROM oligenerator order by generator_id asc");
            }
        }

        public Generator FindByGenerator(string generatorid)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Generator>("SELECT oligenerator.generator_id, " +
                    "oliclient.company, " +
                    "oliplace.address, " +
                    "oliplace.city, " +
                    "oliplace.country, " +
                    "oligenerator.start_date, " +
                    "oligenerator.end_date, " +
                    "oligenerator.maintenance, " +
                    "oligenerator.installation_type, " +
                    "oligenerator.specification_id, " +
                    "olilessee.name, " +
                    "oligenerator.client_id, " +
                    "oligenerator.lessee_id, " +
                    "oligenerator.place_id " +
                    "FROM oligenerator, oliclient, oliplace, olilessee " +
                    "WHERE oligenerator.client_id = oliclient.client_id " +
                    "AND oligenerator.place_id = oliplace.place_id " +
                    "AND oligenerator.lessee_id=olilessee.lessee_id " +
                    "AND oligenerator.generator_id = @Generatorid", new { Generatorid = generatorid }).FirstOrDefault();
            }
        }


        public Customer FindCustByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Customer>("SELECT * FROM oliclient WHERE client_id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<Customer> FindAllClient()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Customer>("SELECT * FROM oliclient order by company");
            }
        }

        public IEnumerable<Lessee> FindAllLessee()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Lessee>("SELECT * FROM olilessee order by name");
            }
        }

        public IEnumerable<Place> FindAllPlace()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Place>("SELECT * FROM oliplace order by address");
            }
        }
    }


}
