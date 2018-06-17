using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;

using GeoElecMVC.Models;

namespace GeoElecMVC.SuperAdminPlace
{
    public class ManageSuperAdminPlace : ISuperAdminPlace<Place>
    {
        private string connectionString;
        public ManageSuperAdminPlace(IConfiguration configuration)
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

        public void Add(Place item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO oliplace (address,postcode,city,country) VALUES(@Address,@Postcode,@City,@Country)", item);
            }

        }

        public IEnumerable<Place> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Place>("SELECT * FROM oliplace order by address");
            }
        }

        public Place FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Place>("SELECT * FROM oliplace WHERE place_id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM oliplace WHERE place_id=@Id", new { Id = id });
            }
        }

        public void Update(Place item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE oliplace SET address=@Address, postcode=@Postcode, city=@City, country=@Country WHERE place_id = @Id", item);
            }
        }
    }
}
