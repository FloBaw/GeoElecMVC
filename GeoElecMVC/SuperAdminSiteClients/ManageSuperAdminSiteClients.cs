using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;

using GeoElecMVC.Models;

namespace GeoElecMVC.SuperAdminSiteClients
{
    public class ManageSuperAdminSiteClients : ISuperAdminSiteClients<SiteClients>
    {
        private string connectionString;
        public ManageSuperAdminSiteClients(IConfiguration configuration)
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

        public void Add(SiteClients item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO olisiteclients (user_id, client_id) " +
                    "VALUES ((SELECT \"AspNetUsers\".\"Id\" FROM \"AspNetUsers\" WHERE \"AspNetUsers\".\"UserName\"=@UserName)," +
                    "(SELECT oliclient.client_id FROM oliclient WHERE oliclient.company =@Company))", item);
            }
        }

        public IEnumerable<SiteClients> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<SiteClients>("SELECT \"AspNetUsers\".\"UserName\", oliclient.company " +
                    "FROM \"AspNetUsers\", oliclient, olisiteclients " +
                    "WHERE \"AspNetUsers\".\"Id\" = olisiteclients.user_id " +
                    "AND oliclient.client_id = olisiteclients.client_id " +
                    "order by \"AspNetUsers\".\"UserName\", oliclient.company");
            }
        }


        public SiteClients FindByUserName(string username)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<SiteClients>("SELECT \"AspNetUsers\".\"UserName\", oliclient.company " +
                    "FROM \"AspNetUsers\", oliclient, olisiteclients " +
                    "WHERE \"AspNetUsers\".\"Id\" = olisiteclients.user_id " +
                    "AND oliclient.client_id = olisiteclients.client_id " +
                    "AND \"AspNetUsers\".\"UserName\" = @UserName " +
                    "order by \"AspNetUsers\".\"UserName\", oliclient.company", new { Username = username }).FirstOrDefault();
            }
        }

        public void Remove(string username)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM olisiteclients " +
                    "WHERE olisiteclients.user_id IN (SELECT olisiteclients.user_id " +
                    "FROM olisiteclients, \"AspNetUsers\" " +
                    "WHERE olisiteclients.user_id = \"AspNetUsers\".\"Id\" " +
                    "AND \"AspNetUsers\".\"UserName\" = @UserName)", new { Username = username });
            }
        }

        public void Update(SiteClients item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE olisiteclients SET " +
                    "user_id = (SELECT \"AspNetUsers\".\"Id\" FROM \"AspNetUsers\" WHERE \"AspNetUsers\".\"UserName\" = @UserName), " +
                    "client_id = (SELECT oliclient.client_id FROM oliclient WHERE oliclient.company = @Company) " +
                    "WHERE user_id = (SELECT \"AspNetUsers\".\"Id\" FROM \"AspNetUsers\" WHERE \"AspNetUsers\".\"UserName\" = @UserName)", item);
                //dbConnection.Query("UPDATE \"AspNetUserRoles\" SET \"UserId\" = @UserId,  \"RoleId\"  = @RoleId WHERE \"UserId\" = @UserId", item);
            }
        }


        public IEnumerable<SiteClients> FindAwaitingUsers()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<SiteClients>("SELECT \"AspNetUsers\".\"UserName\" " +
                    "FROM \"AspNetUsers\", \"AspNetRoles\", \"AspNetUserRoles\" " +
                    "WHERE \"AspNetUsers\".\"Id\" NOT IN (SELECT olisiteclients.user_id FROM olisiteclients) " +
                    "AND \"AspNetUsers\".\"Id\" = \"AspNetUserRoles\".\"UserId\" " +
                    "AND \"AspNetRoles\".\"Id\" = \"AspNetUserRoles\".\"RoleId\" " +
                    "AND (\"AspNetRoles\".\"Name\" = 'SuperAdmin' OR \"AspNetRoles\".\"Name\" = 'Admin')");

                // return dbConnection.Query<SiteClients>("SELECT \"AspNetUsers\".\"UserName\" FROM \"AspNetUsers\" WHERE \"AspNetUsers\".\"Id\" NOT IN (SELECT olisiteclients.user_id FROM olisiteclients)");
            }
        }

        public SiteClients FindAwaitingUserByUserName(string username)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<SiteClients>("SELECT \"AspNetUsers\".\"UserName\" " +
                    "FROM \"AspNetUsers\" " +
                    "WHERE \"AspNetUsers\".\"UserName\" = @Username", new { Username = username }).FirstOrDefault();
            }
        }
    }
}
