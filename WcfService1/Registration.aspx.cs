using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WcfService1
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void RegistrationBtn_Click(object sender, EventArgs e)
        {
            string pattern = @"^(?=.{8,50}$)(?=(.*[A-Z]){2})(?=.*[@?.*!:$%^&+=])(?=(.*[0-9]){2}).*$";
            Match result = Regex.Match(Password.Value, pattern);
            if (Name.Text == "" || Username.Text == "" || Password.Value == "" || Password1.Value == "" || Admin.Text == "")
            {
                Error.Text = "Nekatera polja niso izpoljnena !!!";
            }
            else if (!result.Success)
            {
                Error.Text = "Geslo mora imeti najmanj 2 veliki črki, en poseben znak, vsaj dve števki in mora biti dolžine najmanj 8 znakov.";
            }
            else if (!Password.Value.Equals(Password1.Value))
            {
                Error.Text = "Gesla se ne ujemata";
            }
            else
            {
                WebRequest request = WebRequest.Create("http://servicechat1.azurewebsites.net/Service1.svc/Register/" + Username.Text + "/" + Name.Text.Split(' ')[0] + "/" + Name.Text.Split(' ')[1] + "/" + Password.Value + "/" + Admin.Text + "/");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();

                if ("true".Equals(responseFromServer) && Admin.Text == "ne")
                {
                    Error.Text = "Uspešna registracija";
                }
                else if ("true".Equals(responseFromServer) && Admin.Text == "da")
                {
                    Error.Text = "Uspešno ste se registrirali kot administrator";
                }
                else if ("false".Equals(responseFromServer))
                {
                    Error.Text = "Uporabnik je že registriran!!!";
                }
            }
        }
    }
}