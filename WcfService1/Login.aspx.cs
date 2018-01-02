using System;
using System.IO;
using System.Net;

namespace WcfService1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegistrationBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            //Prebereš username in password
            //Preveri, če je vse ok

            string cookie = Auth.Authenticate(Username.Text, Password.Value);

            if (cookie != null)
            {

                Userdata u = new Userdata();
                u.Username = Username.Text;
                u.Token = cookie;

                //Daj mu cookie
                Session["userdata"] = u;
                Session["username"] = Username.Text;
                Session["pass"] = Password.Value;
                Response.Redirect("Chat.aspx");
                //Redirect na chat

            }
            else
            {
                Error.Text = "Pri prijavi je prišlo do napake!!!";
            }
        }

        protected void AdminBtn_Click(object sender, EventArgs e)
        {
           
            
            WebRequest request = WebRequest.Create("http://servicechat1.azurewebsites.net/Service1.svc/Admin/" + Username.Text + "/" + Password.Value + "/");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();

            if ("true".Equals(responseFromServer))
            {
                string cookie = Auth.Authenticate(Username.Text, Password.Value);
                if(cookie != null)
                {
                    Userdata u = new Userdata();
                    u.Username = Username.Text;
                    u.Token = cookie;

                    //Daj mu cookie
                    Session["userdata"] = u;
                }
                Session["username"] = Username.Text;
                Session["pass"] = Password.Value;
                Response.Redirect("~/admin.aspx");
            }
            else
            {
                Error.Text = "Prijava v administratorske strani ni uspešna !!!";
            }
           
        }
    }
}