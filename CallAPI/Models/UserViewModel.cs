using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallAPI.Models
{
    public class UserViewModel
    {
        public int USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string CONFIRM_PASSWORD { get; set; }
    }
}