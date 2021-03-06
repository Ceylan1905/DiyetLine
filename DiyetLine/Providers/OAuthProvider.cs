﻿using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace DiyetLine.Providers
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        #region[GrantResourceOwnerCredentials]
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var userName = context.UserName;
                var password = context.Password;
                var userService = new UserService(); // our created one
                var user = userService.ValidateUser(userName, password);
                if (user != null)
                {
                    if (user.isletme_sahibi.Restorant_Email != null)
                    {
                        var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Sid, Convert.ToString(user.isletme_sahibi.Sq_id)),
                        new Claim(ClaimTypes.Name, user.isletme_sahibi.Restorant_Adi),
                        new Claim(ClaimTypes.Email, user.isletme_sahibi.Restorant_Email),
                       new Claim(ClaimTypes.Role, Convert.ToString(user.isletme_sahibi.Rol_id))
                    };
                        ClaimsIdentity oAuthIdentity = new ClaimsIdentity(claims,
                                    Startup.OAuthOptions.AuthenticationType);

                        var properties = CreateProperties(user.isletme_sahibi.Restorant_Adi);
                        var ticket = new AuthenticationTicket(oAuthIdentity, properties);
                        context.Validated(ticket);
                    }
                    else
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect");
                    }
                }
                else if(user.kullanici.Email!=null)
                {
                  
                        var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Sid, Convert.ToString(user.kullanici.Sq_id)),
                        new Claim(ClaimTypes.Name, user.kullanici.Ad),
                        new Claim(ClaimTypes.Email, user.kullanici.Email),
                        new Claim(ClaimTypes.Role, Convert.ToString(user.kullanici.Rol_id))
                    };
                        ClaimsIdentity oAuthIdentity = new ClaimsIdentity(claims,
                                    Startup.OAuthOptions.AuthenticationType);

                        var properties = CreateProperties(user.kullanici.Ad);
                        var ticket = new AuthenticationTicket(oAuthIdentity, properties);
                        context.Validated(ticket);
                    }
                    else
                    {
                        context.SetError("invalid_grant", "The user name or password is incorrect");
                    }


                
            });
        }
        #endregion

        #region[ValidateClientAuthentication]
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
                context.Validated();

            return Task.FromResult<object>(null);
        }
        #endregion

        #region[TokenEndpoint]
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
        #endregion

        #region[CreateProperties]
        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
        #endregion
    }
}
