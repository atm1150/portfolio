using CarDealership.Data.Interfaces;
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
    public class BodyColorRepositoryADO : IBodyColorRepository
    {
        public IEnumerable<BodyColor> GetAll()
        {
            List<BodyColor> bodyColors = new List<BodyColor>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("BodyColorSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BodyColor currentRow = new BodyColor();
                        currentRow.BodyColorID = (int)dr["BodyColorID"];
                        currentRow.BodyColorName = dr["BodyColorName"].ToString();

                        bodyColors.Add(currentRow);
                    }
                }
            }
            return bodyColors;
        }
    }
}
