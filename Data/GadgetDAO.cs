using BondGadgetCollection.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BondGadgetCollection.Data
{
    internal class GadgetDAO
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BondGadget;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        // Fará todas as operações relacionadas ao banco de dados
        public List<GadgetModel> FetchAll()
        {
            List<GadgetModel> returnList = new List<GadgetModel>();

            // Acesso ao banco de dados
            // Using abra e fecha automaticamente o banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from dbo.Gadgets";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Criando um novo objeto Gadget e adicionando na lista de retorno.
                        GadgetModel gadget = new GadgetModel();
                        gadget.Id = reader.GetInt32(0);
                        gadget.Name = reader.GetString(1);
                        gadget.Description = reader.GetString(2);
                        gadget.AppearsIn = reader.GetString(3);
                        gadget.WithThisActor = reader.GetString(4);

                        returnList.Add(gadget);
                    }
                }

            }


            return returnList;
        }

        internal int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "DELETE FROM dbo.Gadgets WHERE Id = @Id ";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.VarChar, 1000).Value = id;
                
                connection.Open();

                int deletedID = command.ExecuteNonQuery();

                return deletedID;
            }
        }

        internal List<GadgetModel> SearchForName(string searchPhrase)
        {
            List<GadgetModel> returnList = new List<GadgetModel>();

            // Acesso ao banco de dados
            // Using abra e fecha automaticamente o banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from dbo.Gadgets WHERE NAME LIKE @searchForMe";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@searchForMe", System.Data.SqlDbType.NVarChar).Value = "%" + searchPhrase + "%";

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Criando um novo objeto Gadget e adicionando na lista de retorno.
                        GadgetModel gadget = new GadgetModel();
                        gadget.Id = reader.GetInt32(0);
                        gadget.Name = reader.GetString(1);
                        gadget.Description = reader.GetString(2);
                        gadget.AppearsIn = reader.GetString(3);
                        gadget.WithThisActor = reader.GetString(4);

                        returnList.Add(gadget);
                    }
                }
                return returnList;
            }
        }

        public GadgetModel FetchOne(int Id)
        {

            // Acesso ao banco de dados
            // Using abra e fecha automaticamente o banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * from dbo.Gadgets WHERE Id = @id";

                // associar @id com o parâmetro Id

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = Id;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                GadgetModel gadget = new GadgetModel();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //Criando um novo objeto Gadget e adicionando na lista de retorno.        
                        gadget.Id = reader.GetInt32(0);
                        gadget.Name = reader.GetString(1);
                        gadget.Description = reader.GetString(2);
                        gadget.AppearsIn = reader.GetString(3);
                        gadget.WithThisActor = reader.GetString(4);

                        
                    }
                }
             return gadget;
            }
            
        }

        // Create
        public int CreateOrUpdate(GadgetModel gadgetModel)
        {

            // Acesso ao banco de dados
            // Using abra e fecha automaticamente o banco de dados
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlQuery = "";

                if(gadgetModel.Id <= 0)
                {
                    sqlQuery = "INSERT INTO dbo.Gadgets Values(@Name, @Description, @AppearsIn, @WithThisActor)";
                }
                else
                {
                    //update
                    sqlQuery = "UPDATE dbo.Gadgets SET Name = @Name, Description = @Description, AppearsIn = @AppearsIn, WithThisActor = @WithThisActor WHERE Id = @Id";
                }

                // associar @id com o parâmetro Id

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.Add("@Id", System.Data.SqlDbType.VarChar, 1000).Value = gadgetModel.Id;
                command.Parameters.Add("@Name", System.Data.SqlDbType.VarChar, 1000).Value = gadgetModel.Name;
                command.Parameters.Add("@Description", System.Data.SqlDbType.VarChar, 1000).Value = gadgetModel.Description;
                command.Parameters.Add("@AppearsIn", System.Data.SqlDbType.VarChar, 1000).Value = gadgetModel.AppearsIn;
                command.Parameters.Add("@WithThisActor", System.Data.SqlDbType.VarChar, 1000).Value = gadgetModel.WithThisActor;

                connection.Open();

                int newID = command.ExecuteNonQuery();
  
                return newID;
            }

        }
        // Delete
        // Update
        // Search for name
        // Search for description
    }
}