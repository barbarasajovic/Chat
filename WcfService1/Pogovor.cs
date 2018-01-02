using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WcfService1
{
    public class Pogovor
    {
        public int Id { get; set; }

        public string username { get; set; }

        public string besedilo { get; set; }

        public string cas { get; set; }


        public Pogovor()
        {

        }
        
        public Pogovor(DataRow row)
        {
            Id = (int)row["id"];
            username = row["username"].ToString();
            besedilo = row["besedilo"].ToString();
            cas = row["cas"].ToString();

        }

        public static List<Pogovor> GetHistory()
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["servicechatConnectionString"].ConnectionString))
            {
                string cmd = "Select * from Pogovor";

                connection.Open();
                using (SqlCommand command = new SqlCommand(cmd, connection))
                {
                    //command.Parameters.Add(new SqlParameter("username", username));
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(data);
                    }
                }

                connection.Close();
            }

            List<Pogovor> p_list = new List<Pogovor>();

            foreach (DataRow row in data.Rows)
            {
                Pogovor p = new Pogovor(row);
                p_list.Add(p);
            }
            

            return p_list;
        }




        public string ToDataRow()
        {
            return this.username + ": " + this.besedilo;
        }

        public string showUser()
        {
            return this.username;
        }

        public void Persist()
        {
            if (this.Id == 0)
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["servicechatConnectionString"].ConnectionString))
                {
                    string cmd = @"Insert into Pogovor
                                    (
                                    username,
                                    besedilo,
                                    cas
                                    )
                                    Values
                                    (
                                    @username,
                                    @besedilo,
                                    @cas
                                    )";

                    connection.Open();

                    using (SqlCommand command = new SqlCommand(cmd,connection))
                    {
                        command.Parameters.Add(new SqlParameter("username",this.username));
                        command.Parameters.Add(new SqlParameter("besedilo", this.besedilo));
                        command.Parameters.Add(new SqlParameter("cas", this.cas));
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            else
            {
                //Tega zaenkrat ne rabimo
            }
        }

        public void DeleteUserChat(string username)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["servicechatConnectionString"].ConnectionString);
            conn.Open();
            string sql = "DELETE FROM Pogovor WHERE username='" + username + "'";
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.ExecuteNonQuery();
            conn.Close();

        }

        public void DeleteUser(string username)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["servicechatConnectionString"].ConnectionString);
            conn.Open();
            string sql = "DELETE FROM Uporabnik WHERE username='" + username + "'"; 
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.ExecuteNonQuery();
            conn.Close();
        }

        public void userAdmin(string username)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["servicechatConnectionString"].ConnectionString))
            {
                string cmd = @"INSERT into Uporabnik
                                (
                                admin
                                )
                                Values
                                (
                                @admin
                                )";
                
                connection.Open();

                using (SqlCommand command = new SqlCommand(cmd, connection))
                {
                    command.Parameters.Add(new SqlParameter("username", this.username));
                    command.Parameters.Add(new SqlParameter("admin", "ne"));
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

        }

        public static List<Pogovor> getUsers()
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["servicechatConnectionString"].ConnectionString))
            {
                string cmd = "Select * from Uporabnik";

                connection.Open();
                using (SqlCommand command = new SqlCommand(cmd, connection))
                {
                    //command.Parameters.Add(new SqlParameter("username", username));
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(data);
                    }
                }

                connection.Close();
            }

            List<Pogovor> u_list = new List<Pogovor>();

            foreach (DataRow row in data.Rows)
            {
                Pogovor p = new Pogovor(row);
                u_list.Add(p);
            }


            return u_list;
        }

    }
}