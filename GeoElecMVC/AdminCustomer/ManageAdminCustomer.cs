using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;

using GeoElecMVC.Models;

namespace GeoElecMVC.AdminCustomer
{
    public class ManageAdminCustomer : IAdminCustomer<Customer>
    {
        private string connectionString;
        public ManageAdminCustomer(IConfiguration configuration)
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

        public void Add(Customer item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO oliclient (company,name,first_name,address,postcode,city,country,phone_number,email,fax,business_number) VALUES(@Company,@Name,@First_name,@Address,@Postcode,@City,@Country,@Phone_number,@Email,@Fax,@Business_number)", item);
            }

        }

        public IEnumerable<Customer> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Customer>("SELECT * FROM oliclient");
            }
        }

        public Customer FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Customer>("SELECT * FROM oliclient WHERE client_id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM oliclient WHERE client_id=@Id", new { Id = id });
            }
        }

        public void Update(Customer item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE oliclient SET company=@Company, name=@Name, first_name=@First_name, address=@Address, postcode=@Postcode, city=@City, country=@Country, phone_number=@Phone_number, email=@Email, fax=@Fax, business_number=@Business_number WHERE client_id = @Id", item);
            }
        }
    }
}
