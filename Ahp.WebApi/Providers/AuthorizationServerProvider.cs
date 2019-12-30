using Ahp.Core.Models;
using Ahp.Core.Reposirories.Concretes;
using Ahp.Core.Repositories.Concretes.Administrasi;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ahp.WebApi.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            var req = await context.Request.ReadFormAsync();

            var userName = context.UserName;
            var password = context.Password;

            var ldapDomain = ConfigurationManager.AppSettings["LdapDomain"];
            var ldapServer = ConfigurationManager.AppSettings["LdapServer"] ?? "";
            var ldapPort = Int32.Parse(ConfigurationManager.AppSettings["LdapPort"]);

            try
            {
                var user = new AdministrasiUserLogin();
                using (var ctx = new GenericContext())
                {
                    using (var repoUser = new AdministrasiUserRepository(ctx))
                    {
                        user = repoUser.Login(userName, password);
                        if (user != null)
                        {
                            try
                            {
                                                             try
                                {
                             
                                    var avatar = "male.png"; //user.Avatar;

                                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                                    identity.AddClaim(new Claim(ClaimTypes.Name, userName.ToUpper()));
                                    identity.AddClaim(new Claim(ClaimTypes.Role, user.Roles));
                                    identity.AddClaim(new Claim("UserRole", user.Roles));
                                    
                                    
                                    identity.AddClaim(new Claim("IsAdministrator", user.IsAdministrator.ToString()));
                                    
                                    identity.AddClaim(new Claim("as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId));

                                    var props = new AuthenticationProperties(new Dictionary<string, string>
                                    {
                                        { 
                                            "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                                        },
                                        { 
                                            "UserName", userName.ToUpper()
                                        },
                                        {
                                            "Avatar", avatar
                                        },
                                        {
                                            "UserRole", user.Roles
                                        }
                                    });

                                    var ticket = new AuthenticationTicket(identity, props);
                                    context.Validated(ticket);

                                    context.Request.Context.Authentication.SignIn(identity);

                                }
                                catch (LdapException ex)
                                {
                                    var errMsg = string.Format(ex.Message);

                                    context.SetError("invalid_authorize", errMsg);
                                    context.Response.StatusCode = 401;
                                    return;
                                }
                            }
                            catch (LdapException ex)
                            {
                                var errMsg = string.Format("Username tidak terdaftar pada Domain {0}.", ldapDomain);// + ex.Message;

                                context.SetError("invalid_authorize", errMsg);
                                context.Response.StatusCode = 401;
                                return;
                            }

                        }
                        else
                        {
                            var errMsg = string.Format("Username atau Password salah.");

                            context.SetError("invalid_authorize", errMsg);
                            context.Response.StatusCode = 401;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var errMsg = ex.InnerException.Message; //string.Format("Gagal pada saat proses Login. Silahkan menghubungi Team ICT");

                context.SetError("Internal Server Error", errMsg);
                context.Response.StatusCode = 500;
                return;
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }
    }
}