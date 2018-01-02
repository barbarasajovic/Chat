using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WcfService1
{
    public class Auth
    {
        /// <summary>
        /// Vrne token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Authenticate(string username, string password)
        {
            DataTable data = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["servicechatConnectionString"].ConnectionString))
            {
                string cmd = "Select geslo from Uporabnik where username = @username";

                connection.Open();
                using (SqlCommand command = new SqlCommand(cmd,connection))
                {
                    command.Parameters.Add(new SqlParameter("username",username));
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(data);
                    }
                }
                
                connection.Close();
            }

            if (data.Rows.Count > 0)
            {
                string hashGesloTable = (string)data.Rows[0][0];

                //byte[] hashGeslo = ComputeHash.ComputeHashMD5(password);

                string hashGeslo = ComputeHash(password);

                if (hashGeslo.Equals(hashGesloTable))
                {
                    //
                    Random rnd = new Random();

                    int coo = rnd.Next(1, Int32.MaxValue);

                    return "cookie" + coo;

                }
                return null;

            }
            
            return null;
            





            /*
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            string cmd = "Select geslo from Uporabnik where username = @username";

            SqlCommand comm = new SqlCommand(cmd, conn);
            comm.Parameters.AddWithValue("@Username", username);
            string hashGeslo = (string)comm.ExecuteScalar();
            
            conn.Close();

            return "";
            */

        }



        /// <summary>
        /// Generira hash za geslo
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static string ComputeHash(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();

            //return sb.ToString();

        }
    }
}