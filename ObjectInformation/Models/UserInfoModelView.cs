using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ObjectInformation.Models
{
    public class UserInfoModelView
    {
        public UserInfoModelView()
        {

        }

        public UserInfoModelView(string id, string email)
        {
            Id = id;
            Email = email;
        }

        public string Id { get; set; }
        public string Email { get; set; }
    }
}