using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http.Cors;
using System.Threading;
using System.Globalization;
using System.Web.Http.Routing;

namespace Ahp.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //Configure enable CORS
            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);
            config.MessageHandlers.Add(new PreflightRequestsHandler());


            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" });
            config.Routes.MapHttpRoute("DefaultApiWithAction", "Api/{controller}/{action}");
            config.Routes.MapHttpRoute("DefaultApiWithActionAndId", "Api/{controller}/{action}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" });


            config.Routes.MapHttpRoute("DefaultApiGet", "Api/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute("DefaultApiNew", "Api/{controller}", new { action = "New" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute("DefaultApiGetSingle", "Api/{controller}", new { action = "GetSingle" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute("DefaultApiDynamicData", "Api/{controller}", new { action = "DynamicData" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute("DefaultApiPost", "Api/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            config.Routes.MapHttpRoute("DefaultApiDropdown", "Api/{controller}", new { action = "Dropdown" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            config.Routes.MapHttpRoute("DefaultApiDropdown2", "Api/{controller}", new { action = "Dropdown2" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            config.Routes.MapHttpRoute("DefaultApiDropdownByKey", "Api/{controller}", new { action = "DropdownByKey" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            config.Routes.MapHttpRoute("DefaultApiDatatables", "Api/{controller}", new { action = "DataTables" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });
            config.Routes.MapHttpRoute("DefaultApiPut", "Api/{controller}", new { action = "Put" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });
            config.Routes.MapHttpRoute("DefaultApiDelete", "Api/{controller}", new { action = "Delete" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) });

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            config.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                // = new LowercaseContractResolver(),
                Culture = new CultureInfo("en-US"),
                DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat,
                DateFormatString = "yyyy-MM-dd HH:mm:ss.fff",
                DateParseHandling = Newtonsoft.Json.DateParseHandling.None,
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc
            };
        }
    }

    public class PreflightRequestsHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Contains("Origin") && request.Method.Method == "OPTIONS")
            {
                var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
                response.Headers.Add("Access-Control-Allow-Origin", "*");
                response.Headers.Add("Access-Control-Allow-Headers", "Origin, Content-Type, Accept, Authorization");
                response.Headers.Add("Access-Control-Allow-Methods", "*");
                var tcs = new TaskCompletionSource<HttpResponseMessage>();
                tcs.SetResult(response);
                return tcs.Task;
            }
            return base.SendAsync(request, cancellationToken);
        }
    }

    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return !string.IsNullOrEmpty(propertyName) ? propertyName.ToLower() : propertyName;
        }

        protected override IValueProvider CreateMemberValueProvider(System.Reflection.MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                var pi = (PropertyInfo)member;
                if (pi.PropertyType.FullName == "System.String")
                {
                    return new NullableValueProvider(member);
                }
            }


            return base.CreateMemberValueProvider(member);
        }
    }

    public class NullableValueProvider : IValueProvider
    {

        private readonly IValueProvider _underlyingValueProvider;


        public NullableValueProvider(MemberInfo memberInfo)
        {
            _underlyingValueProvider = new DynamicValueProvider(memberInfo);
        }

        public void SetValue(object target, object value)
        {
            _underlyingValueProvider.SetValue(target, value);
        }

        public object GetValue(object target)
        {
            return _underlyingValueProvider.GetValue(target) ?? "";
        }
    }
}
