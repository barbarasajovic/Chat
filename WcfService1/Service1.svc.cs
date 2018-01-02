using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    public class Service1 : IService1
    {
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

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
        }

        public bool Register(string username, string name, string surname, string password, string admin)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["servicechatConnectionString"].ConnectionString);
                conn.Open();
                string preveriUporabnika = "INSERT INTO Uporabnik (username,ime,priimek,geslo,admin) VALUES (@Username, @Ime, @Priimek, @Geslo, @Admin)";
                SqlCommand comm = new SqlCommand(preveriUporabnika, conn);
                comm.Parameters.AddWithValue("@Username", username);
                comm.Parameters.AddWithValue("@Ime", name);
                comm.Parameters.AddWithValue("@Priimek", surname);
                comm.Parameters.AddWithValue("@Geslo", MD5Hash(password));
                comm.Parameters.AddWithValue("@Admin", admin);

                comm.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Login(string username, string password)
        {
            string cookie = Auth.Authenticate(username, password);

            if (cookie != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Send(string username, string message)
        {
            if (AuthenticateUser() == true)
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

                Pogovor p = new Pogovor();
                p.besedilo = message;
                p.username = username;
                p.cas = "krnekj";

                p.Persist();
            }
            else
            {
                //Something is wrong
            }

        }

        private bool AuthenticateUser()
        {
            WebOperationContext ctx = WebOperationContext.Current;
            string authHeader = ctx.IncomingRequest.Headers[HttpRequestHeader.Authorization];
            if (authHeader == null)
                return false;
            string[] loginData = authHeader.Split(':');
            if (loginData.Length == 2 && Login(loginData[0], loginData[1]))
                return true;
            return false;
        }

        public List<Message> GetMessages()
        {
            if (!AuthenticateUser())
                throw new FaultException("Napačno uporabniško ime ali geslo.");

            var ret = new List<Message>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["servicechatConnectionString"].ConnectionString);
            conn.Open();
            string pridobi = "Select username, besedilo from Pogovor";
            SqlCommand comm = new SqlCommand(pridobi, conn);
            using (var command = comm)
            {

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                        ret.Add(new Message { username = reader.GetString(0), besedilo = reader.GetString(1) });

                }
            }
            conn.Close();
            return ret;
        }

        public bool Admin(string username, string password)
        {
            if (Login(username, password) == true)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["servicechatConnectionString"].ConnectionString);
                conn.Open();
                string jeUser = "SELECT admin FROM Uporabnik WHERE username='" + username + "'";
                SqlCommand comm = new SqlCommand(jeUser, conn);
                string aliJe = comm.ExecuteScalar().ToString();
                if ("da".Equals(aliJe))
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public List<Count> GetUserCount()
        {
            var ret = new List<Count>();

            if (!AuthenticateUser())
                throw new FaultException("Napačno uporabniško ime ali geslo.");

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["servicechatConnectionString"].ConnectionString);
            conn.Open();
            string pridobi = "SELECT username, count(*) AS stSporocil FROM Pogovor GROUP by username";
            SqlCommand comm = new SqlCommand(pridobi, conn);
            using (var command = comm)
            {

                using (var reader = command.ExecuteReader())
                {

                    while (reader.Read())
                        ret.Add(new Count { username = reader.GetString(0), message = reader.GetInt32(1) });

                }
            }
            conn.Close();
            return ret;
        }

        public List<User> GetUsers()
        {
            var ret = new List<User>();

            if (!AuthenticateUser())
                throw new FaultException("Napačno uporabniško ime ali geslo.");

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["servicechatConnectionString"].ConnectionString);
            conn.Open();
            string pridobi = "SELECT username FROM Uporabnik";
            SqlCommand comm = new SqlCommand(pridobi, conn);
            using (var command = comm)
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                        ret.Add(new User { username = reader.GetString(0) });

                }
            }
            conn.Close();
            return ret;
        }

        
    }
}
