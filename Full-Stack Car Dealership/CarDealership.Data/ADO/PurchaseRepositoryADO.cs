using CarDealership.Data.Interfaces;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.ADO
{
    public class PurchaseRepositoryADO : IPurchaseRepository
    {
        public IEnumerable<Purchase> GetAll()
        {
            List<Purchase> purchases = new List<Purchase>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchaseSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Purchase currentRow = new Purchase();
                        currentRow.PurchaseID = (int)dr["PurchaseID"];
                        currentRow.PurchasePrice = (decimal)dr["PurchasePrice"];
                        currentRow.PurchaseDate = (DateTime)dr["PurchaseDate"];
                        currentRow.CustomerID = (int)dr["CustomerID"];
                        currentRow.PurchaseType = dr["PurchaseType"].ToString();
                        currentRow.UserID = dr["UserID"].ToString();
                        currentRow.VehicleID = (int)dr["VehicleID"];

                        purchases.Add(currentRow);
                    }
                }
            }
            return purchases;
        }

        public void Insert(Purchase purchase)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("PurchaseInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@PurchaseID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@PurchaseType", purchase.PurchaseType);
                cmd.Parameters.AddWithValue("@PurchasePrice", purchase.PurchasePrice);
                cmd.Parameters.AddWithValue("@CustomerID", purchase.CustomerID);
                cmd.Parameters.AddWithValue("@UserID", purchase.UserID);
                cmd.Parameters.AddWithValue("@VehicleID", purchase.VehicleID);
                cmd.Parameters.AddWithValue("@IsAvailable", false);

                cn.Open();

                cmd.ExecuteNonQuery();

                purchase.PurchaseID = (int)param.Value;
                
            }
        }

        public IEnumerable<SalesReportItem> SearchSales(SalesSearchParameters parameters)
        {
            List<SalesReportItem> sales = new List<SalesReportItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT FirstName, LastName, Sum(PurchasePrice) AS TotalSales, Count(*) AS TotalVehicles FROM Purchase ";
                query += "INNER JOIN AspNetUsers ON Purchase.UserID = AspNetUsers.Id ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                if (!string.IsNullOrEmpty(parameters.UserID))
                {
                    query += "WHERE Purchase.UserID = @UserID ";
                    cmd.Parameters.AddWithValue("@UserID", parameters.UserID);
                }
                else
                {
                    query += "WHERE 1 = 1  ";
                }

                if (parameters.FromDate.HasValue)
                {
                    query += "AND PurchaseDate >= @FromDate ";
                    cmd.Parameters.AddWithValue("@FromDate", parameters.FromDate);
                }

                if (parameters.ToDate.HasValue)
                {
                    query += "AND PurchaseDate <= @ToDate ";
                    cmd.Parameters.AddWithValue("@ToDate", parameters.ToDate);
                }

                query += "GROUP BY LastName, FirstName ";
                query += "ORDER BY LastName ASC";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SalesReportItem row = new SalesReportItem();

                        row.FirstName = dr["FirstName"].ToString();
                        row.LastName = dr["LastName"].ToString();
                        row.TotalSales = (decimal)dr["TotalSales"];
                        row.TotalVehicles = (int)dr["TotalVehicles"];

                        sales.Add(row);
                    }
                }
            }
            return sales;


        }
    }
}
