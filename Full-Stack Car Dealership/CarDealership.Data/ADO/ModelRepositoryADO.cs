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
    public class ModelRepositoryADO : IModelRepository
    {
        public IEnumerable<Model> GetAll()
        {
            List<Model> models = new List<Model>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Model currentRow = new Model();
                        currentRow.ModelID = (int)dr["ModelID"];
                        currentRow.MakeID = (int)dr["MakeID"];
                        currentRow.ModelName = dr["ModelName"].ToString();
                        currentRow.DateAdded = (DateTime)dr["DateAdded"];

                        models.Add(currentRow);
                    }
                }
            }
            return models;
        }

        public IEnumerable<ModelListItem> GetModelList()
        {
            List<ModelListItem> models = new List<ModelListItem>();
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelListSelectAll", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        ModelListItem currentRow = new ModelListItem();
                        currentRow.ModelID = (int)dr["ModelID"];
                        currentRow.ModelName = dr["ModelName"].ToString();
                        currentRow.MakeName = dr["MakeName"].ToString();
                        currentRow.DateAdded = (DateTime)dr["DateAdded"];
                        currentRow.Email = dr["Email"].ToString();

                        models.Add(currentRow);
                    }
                }
            }
            return models;
        }

        public IEnumerable<Model> GetModelsByMake(int id)
        {
            List<Model> modelList = new List<Model>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                string query = "SELECT ModelName, ModelID FROM Model md ";
                query += "INNER JOIN Make mk ON md.MakeID = mk.MakeID ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;

                query += "WHERE mk.MakeID = @MakeID ";
                cmd.Parameters.AddWithValue("@MakeID", id);

                query += "ORDER BY ModelName";
                cmd.CommandText = query;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Model row = new Model();

                        row.ModelID = (int)dr["ModelID"];
                        row.ModelName = dr["ModelName"].ToString();

                        modelList.Add(row);
                    }
                }
            }
            return modelList;
        }

        public void Insert(Model model)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("ModelInsert", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("ModelID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.Parameters.Add(param);

                cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
                cmd.Parameters.AddWithValue("@MakeID", model.MakeID);
                cmd.Parameters.AddWithValue("@UserID", model.UserID);

                cn.Open();

                cmd.ExecuteNonQuery();

                model.ModelID = (int)param.Value;
            }
        }
    }
}
