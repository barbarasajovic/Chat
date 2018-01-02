using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        [WebGet(UriTemplate = "Register/{username}/{name}/{surname}/{password}/{admin}/", ResponseFormat = WebMessageFormat.Json)]
        bool Register(string username, string name, string surname, string password, string admin);

        [OperationContract]
        [WebGet(UriTemplate = "Login/{username}/{password}/", ResponseFormat = WebMessageFormat.Json)]
        bool Login(string username, string password);

        [OperationContract]
        [WebGet(UriTemplate = "Send/{username}/{message}/", ResponseFormat = WebMessageFormat.Json)]
        void Send(string username, string message);

        [OperationContract]
        [WebGet(UriTemplate = "Messages/", ResponseFormat = WebMessageFormat.Json)]
        List<Message> GetMessages();

        [OperationContract]
        [WebGet(UriTemplate = "Admin/{username}/{password}/", ResponseFormat = WebMessageFormat.Json)]
        bool Admin(string username, string password);

        [OperationContract]
        [WebGet(UriTemplate = "UserCount/", ResponseFormat = WebMessageFormat.Json)]
        List<Count> GetUserCount();

        [OperationContract]
        [WebGet(UriTemplate = "AllUsers/", ResponseFormat = WebMessageFormat.Json)]
        List<User> GetUsers();
    }

    [DataContract]
    public class Message
    {
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string besedilo { get; set; }

    }

    [DataContract]
    public class Count
    {
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public int message { get; set; }

    }

    [DataContract]
    public class User
    {
        [DataMember]
        public string username { get; set; }
    }

}
