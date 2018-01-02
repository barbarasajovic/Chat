using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WcfService1
{
    public class RootObject1
    {
        public string username { get; set; }
        public string message { get; set; }
    }

    public class RootObject3
    {
        public string Users { get; set; }
    }

    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userdata"] != null)
            {
                Userdata tmp_user = (Userdata)Session["userdata"];
                CurrentUser.Text = tmp_user.Username;

                if (!Globals.active_users.Contains(tmp_user))
                {
                    Globals.active_users.Add(tmp_user);
                }

                AllUsers();

            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                
                userCount();
            }
        }

        protected void userCount()
        {
            var myUri = new Uri("http://servicechat1.azurewebsites.net/Service1.svc/UserCount/");
            var myWebRequest = WebRequest.Create(myUri);
            var myHttpWebRequest = (HttpWebRequest)myWebRequest;
            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Headers.Add("Authorization", (string)(Session["username"]) + ":" + (string)(Session["pass"]));
            myHttpWebRequest.Accept = "application/json";
            var myWebResponse = myWebRequest.GetResponse();
            var responseStream = myWebResponse.GetResponseStream();

            var myStreamReader = new StreamReader(responseStream, Encoding.Default);
            var json = myStreamReader.ReadToEnd();

            // PARSE
            var jsonObj = new JavaScriptSerializer().Deserialize<RootObject1[]>(json);


            foreach (var a in jsonObj)
            {
                ListBox1.Items.Add(string.Format("Uporabnik {0} ima {1} sporočila", a.username, a.message));
            }


            responseStream.Close();
            myWebResponse.Close();
        }

        protected void LogoutAdmin_Click(object sender, EventArgs e)
        {
            Session.Remove(CurrentUser.Text);

            Userdata u = Globals.active_users.FirstOrDefault(x => x.Username.Equals(CurrentUser.Text));

            Globals.active_users.Remove(u);

            Response.Redirect("Login.aspx");
        }

        protected void Izbriši_Click(object sender, EventArgs e)
        {
            String deleteUsername = UsersToEdit.SelectedItem.ToString();
            Pogovor p = new Pogovor();
            p.DeleteUserChat(deleteUsername);
            p.DeleteUser(deleteUsername);

            // ta koda se nj še izvede da bo tut vidt da je uporabnik zbrisan
            //Users.Items.Clear();
            //Users.allUsers();            
        }

        protected void PostaneAdmin_Click(object sender, EventArgs e)
        {
            String adminUsername = UsersToEdit.SelectedItem.ToString();
            Pogovor p = new Pogovor();
            p.userAdmin(adminUsername);

        }

        protected void AllUsers()
        {
            var myUri2 = new Uri("http://servicechat1.azurewebsites.net/Service1.svc/AllUsers/");
            var myWebRequest2 = WebRequest.Create(myUri2);
            var myHttpWebRequest2 = (HttpWebRequest)myWebRequest2;
            myHttpWebRequest2.PreAuthenticate = true;
            myHttpWebRequest2.Headers.Add("Authorization", (string)(Session["username"]) + ":" + (string)(Session["pass"]));
            myHttpWebRequest2.Accept = "application/json";
            var myWebResponse2 = myWebRequest2.GetResponse();
            var responseStream2 = myWebResponse2.GetResponseStream();
            var myStreamReader2 = new StreamReader(responseStream2, Encoding.Default);
            var json2 = myStreamReader2.ReadToEnd();
            // PARSE
            var jsonObj2 = new JavaScriptSerializer().Deserialize<RootObject3[]>(json2);
            foreach (var a in jsonObj2)
            {
                UsersToEdit.Items.Add(a.Users);
            }
            responseStream2.Close();
            myWebResponse2.Close();
        }
    }
}