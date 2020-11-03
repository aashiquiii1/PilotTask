using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace CSA.WebService.Common.Authorization
{
    public class BasicAuthorization : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //Check client passed any value in header or not
            if (actionContext.Request.Headers.Authorization == null || actionContext.Request.Headers.Authorization.Parameter == null)
            {
                  actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                //Get the Hear values
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

                //Decoded the authenticationToken values becouse client passed the user namd and password in encoded form
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                //Split the user name and password by : because client passed the user name and password as"userNameValue:Passwordvalue"
                string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                if (WebConfigurationManager.AppSettings["BasicAuthUsername"] == username && WebConfigurationManager.AppSettings["BasicAuthPassword"] == password)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                     actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }

    }
}
