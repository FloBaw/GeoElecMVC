using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;

using GeoElecMVC.Models;

namespace GeoElecMVC.SuperAdminSiteLessee
{
    public class ManageSuperAdminSiteLessee
    {
        private string connectionString;
        public ManageSuperAdminSiteLessee(IConfiguration configuration)
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

        public void Add(SiteLessee item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO olisitelessee (user_id, lessee_id) " +
                    "VALUES ((SELECT \"AspNetUsers\".\"Id\" FROM \"AspNetUsers\" WHERE \"AspNetUsers\".\"UserName\"=@UserName)," +
                    " (@Lessee_id))", item);
            }
        }

        public IEnumerable<SiteLessee> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<SiteLessee>("SELECT olisitelessee.sitelessee_id, \"AspNetUsers\".\"UserName\", olilessee.name, olilessee.first_name, olilessee.lessee_id " +
                    "FROM \"AspNetUsers\", olilessee, olisitelessee " +
                    "WHERE \"AspNetUsers\".\"Id\" = olisitelessee.user_id " +
                    "AND olilessee.lessee_id = olisitelessee.lessee_id " +
                    "order by \"AspNetUsers\".\"UserName\", olilessee.name");
            }
        }

        public SiteLessee FindById(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<SiteLessee>("SELECT olisitelessee.sitelessee_id, \"AspNetUsers\".\"UserName\", olilessee.name, olilessee.first_name, olilessee.lessee_id " +
                    "FROM \"AspNetUsers\", olilessee, olisitelessee " +
                    "WHERE \"AspNetUsers\".\"Id\" = olisitelessee.user_id " +
                    "AND olilessee.lessee_id = olisitelessee.lessee_id " +
                    "AND olisitelessee.sitelessee_id =@Sitelessee " +
                    "order by \"AspNetUsers\".\"UserName\", olilessee.name", new { Sitelessee = id }).FirstOrDefault();
            }
        }


        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM olisitelessee " +
                    "WHERE olisitelessee.sitelessee_id= @Sitelessee", new { Sitelessee = id });
            }
        }

        public void Update(SiteLessee item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE olisitelessee SET lessee_id = @Lessee_id " +
                    "WHERE olisitelessee.sitelessee_id =@Id", item);
            }
        }

        public IEnumerable<SiteLessee> FindAwaitingUsers()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<SiteLessee>("SELECT \"AspNetUsers\".\"UserName\" " +
                    "FROM \"AspNetUsers\", \"AspNetUserRoles\" " +
                    "WHERE \"AspNetUsers\".\"Id\" = \"AspNetUserRoles\".\"UserId\" " +
                    "AND \"AspNetUsers\".\"Id\" NOT IN (SELECT olisitelessee.user_id FROM olisitelessee) " +
                    "order by \"AspNetUsers\".\"UserName\"");
            }
        }

        public SiteLessee FindAwaitingUserByUserName(string username)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<SiteLessee>("SELECT \"AspNetUsers\".\"UserName\" " +
                    "FROM \"AspNetUsers\" " +
                    "WHERE \"AspNetUsers\".\"UserName\" = @Username", new { Username = username }).FirstOrDefault();
            }
        }
    }
}
