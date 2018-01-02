using System;
using System.Collections;
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
    public class RootObject
    {
        public string besedilo { get; set; }
        public string username { get; set; }
    }
    public partial class Chat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Userdata"] != null)
                {
                    Userdata tmp_user = (Userdata)Session["userdata"];
                    CurrentUser.Text = tmp_user.Username;
                    if (!Globals.active_users.Contains(tmp_user))
                    {
                        Globals.active_users.Add(tmp_user);
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
                
            }
            Refresh_Click(null, null);
        }

        /*
        private void AddUsers()
        {
            foreach (string s in active_users)
            {
                Users.Items.Add(s);
            }

        }
        */
        /*
        protected void Button3_Click(object sender, EventArgs e)
        {
            string besedilo = Message.Text;
            Message.Text = "";
            string dodaj = (string)Session["uporabnik"] + ":" + besedilo;
            Messages.Items.Add(dodaj);
            history.Add(dodaj);

        }
        */

        protected void Logout_Click(object sender, EventArgs e)
        {
            Session.Remove(CurrentUser.Text);

            Userdata u = Globals.active_users.FirstOrDefault(x => x.Username.Equals(CurrentUser.Text));

            Globals.active_users.Remove(u);

            Refresh_Click(null, null);

            Response.Redirect("Login.aspx");
        }

        protected void Send_Click(object sender, EventArgs e)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            string besedilo = Message.Text;
            Message.Text = "";

            Pogovor p = new Pogovor();
            p.besedilo = besedilo;
            p.username = CurrentUser.Text;
            p.cas = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone).ToString("HH:mm:ss");

            p.Persist();

            //string dodaj = (Userdata)Session["userdata"] + ":" + besedilo;

            //Messages.Items.Add(p.ToDataRow());
            Refresh_Click(null, null);
            //history.Add(dodaj);
        }

        public ListBox getMessageBox()
        {
            return Messages;
        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            Users.Items.Clear();
            foreach (Userdata s in Globals.active_users)
            {
                Users.Items.Add(s.Username);
            }

            Messages.Items.Clear();
            var myUri = new Uri("http://servicechat1.azurewebsites.net/Service1.svc/Messages/");
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
            var jsonObj = new JavaScriptSerializer().Deserialize<RootObject[]>(json);
            foreach (var a in jsonObj)
            {
                Messages.Items.Add(string.Format("{0} : {1}", a.username, a.besedilo));
            }

            responseStream.Close();
            myWebResponse.Close();
        }
    }
}