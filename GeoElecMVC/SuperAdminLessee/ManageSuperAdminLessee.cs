using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;

using GeoElecMVC.Models;

namespace GeoElecMVC.SuperAdminLessee
{
    public class ManageSuperAdminLessee : ISuperAdminLessee<Lessee>
    {
        private string connectionString;
        public ManageSuperAdminLessee(IConfiguration configuration)
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

        public void Add(Lessee item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO olilessee (name,first_name,address,postcode,city,country,phone_number,email,fax) VALUES(@Name,@First_name,@Address,@Postcode,@City,@Country,@Phone_number,@Email,@Fax)", item);
            }

        }

        public IEnumerable<Lessee> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Lessee>("SELECT * FROM olilessee order by name, first_name");
            }
        }

        public Lessee FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Lessee>("SELECT * FROM olilessee WHERE lessee_id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM olilessee WHERE lessee_id=@Id", new { Id = id });
            }
        }

        public void Update(Lessee item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE olilessee SET name=@Name, first_name=@First_name, address=@Address, postcode=@Postcode, city=@City, country=@Country, phone_number=@Phone_number, email=@Email, fax=@Fax WHERE lessee_id = @Id", item);
            }
        }
    }
}
