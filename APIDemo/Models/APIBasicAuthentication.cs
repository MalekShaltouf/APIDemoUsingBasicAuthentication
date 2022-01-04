using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace APIDemo.Models
{
    public class APIBasicAuthentication:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                //here mean that user not send Uname & Password when he did api request 
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else 
            {
                //here mean that user send Uname & Password when he did api request 

                //return credential info(uname & pass) in base64
                string authenticationInfo = actionContext.Request.Headers.Authorization.Parameter;

                //decode credential info
                string decodeAuthenticationInfo = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationInfo));

                //split uname & pass 
                string[] UserName_Pass = decodeAuthenticationInfo.Split(':');

                if (!IsAuthenticated(UserName_Pass[0], UserName_Pass[1].Trim())) 
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
        public bool IsAuthenticated(string username, string pass)
        {
            DataAccessContext context = new DataAccessContext();
            return context.Users.Any(user => user.UserName.Equals(username,StringComparison.OrdinalIgnoreCase) && user.Password.Equals(pass,StringComparison.OrdinalIgnoreCase));
        }
    }
}

