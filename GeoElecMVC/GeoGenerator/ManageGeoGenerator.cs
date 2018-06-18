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

        public void Add(Generator item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO oligenerator (generator_id,start_date,end_date,maintenance,installation_type,specification_id) VALUES(@Generator_id, @Start_date,@End_date,@Maintenance,@Installation_type,@Specification_id)", item);
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
                    "AND oligenerator.lessee_id=olilessee.lessee_id " +
                    "order by generator_id");
                //return dbConnection.Query<Generator>("SELECT * FROM oligenerator order by generator_id asc");
            }
        }

        public IEnumerable<Generator> FindAwaitingGen()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Generator>("SELECT * FROM oligenerator " +
                    "WHERE generator_id NOT IN (SELECT oligenerator.generator_id " +
                    "FROM oligenerator, oliclient, oliplace, olilessee " +
                    "WHERE oligenerator.client_id = oliclient.client_id " +
                    "AND oligenerator.place_id = oliplace.place_id " +
                    "AND oligenerator.lessee_id=olilessee.lessee_id) " +
                    "order by generator_id");
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
                    "olilessee.first_name, " +
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
        

        public Generator FindByAwaitingGenerator(string generatorid)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Generator>("SELECT * " +
                    "FROM oligenerator " +
                    "WHERE oligenerator.generator_id = @Generatorid", new { Generatorid = generatorid }).FirstOrDefault();
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
                return dbConnection.Query<Lessee>("SELECT * FROM olilessee order by name, first_name");
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

        /*
        public void UpdateGen(Generator item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE oligenerator SET start_date=@Start_date, end_date=@End_date, maintenance=@Maintenance, installation_type=@Installation_type, specification_id=@Specification_id WHERE generator_id = @Generator_id", item);
            }
        }

        */

        public void UpdateGen(Generator item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE oligenerator SET start_date=@Start_date, end_date=@End_date, maintenance=@Maintenance, installation_type=@Installation_type, specification_id=@Specification_id WHERE generator_id = @Generator_id", item);
            }
        }

        public void UpdateGenClient(string generatorid, int clientid)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE oligenerator SET client_id=@Client_id WHERE generator_id =@Generator_id", new { Generator_id = generatorid, Client_id = clientid });
            }
        }

        public void UpdateGenLessee(string generatorid, int lesseeid)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE oligenerator SET lessee_id=@Lessee_id WHERE generator_id =@Generator_id", new { Generator_id = generatorid, Lessee_id = lesseeid });
            }
        }

        public void UpdateGenPlace(string generatorid, int placeid)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE oligenerator SET place_id=@Place_id WHERE generator_id =@Generator_id", new { Generator_id = generatorid, Place_id = placeid });
            }
        }
    }


}
