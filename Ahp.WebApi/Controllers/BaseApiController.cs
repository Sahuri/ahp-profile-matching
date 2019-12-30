using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using Ahp.Core.Reposirories.Concretes;
using Ahp.Core.Repositories.Abstractions;
using System.Linq;

using System;
using System.Dynamic;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Routing;
using System.Net.Http;
using System.Web;
using DataTablesParser;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.Reporting.WebForms;

namespace Ahp.WebApi.Controllers
{
    public abstract class BaseApiController<T> : ApiController, IGenericWebApiController<T>
        where T : class, new()
    {

        protected IGenericDataRepository<T> repo;
        protected string UserID
        {
            get
            {
                return this.RequestContext.Principal.Identity.Name;
            }
        }

        protected string RoleID
        {
            get
            {
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var roleID = identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                                   .Select(c => c.Value).SingleOrDefault();
                return roleID;
            }
        }

        
        protected string IsAdministrator
        {
            get
            {
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var isAdministrator = identity.Claims.Where(c => c.Type == "IsAdministrator")
                                   .Select(c => c.Value).SingleOrDefault();
                return isAdministrator;
            }
        }


        #region == Base Method ==
        [HttpGet]
        public virtual IGenericWebApiResult New()
        {
            try
            {
                using (var result = new GenericWebApiResult<T>())
                {
                    result.Data = new T();
                    result.Success = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<T>(ex))
                {
                    result.Success = false;
                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;

                    return result;
                }
            }
        }

        [HttpGet]
        public virtual IGenericWebApiResult Get()
        {
            try
            {
                using (var result = new GenericWebApiResult<T>())
                {
                    result.DataList = repo.GetAll();
                    result.Success = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<T>(ex))
                {
                    result.Success = false;
                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;

                    return result;
                }
            }
        }

        [HttpGet]
        [Route("{id}")]
        public virtual IGenericWebApiResult GetById(int id)
        {
            try
            {
                using (var result = new GenericWebApiResult<T>())
                {
                    //result.Data = repo.GetAll().FirstOrDefault(x => x.id == keyValues);

                    result.Data = repo.SelectById(id);

                    result.Success = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<T>(ex))
                {
                    result.Success = false;
                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;

                    return result;
                }
            }
        }

        [HttpGet]
        public virtual IGenericWebApiResult GetSingle(string keyValues)
        {
            try
            {
                using (var result = new GenericWebApiResult<T>())
                {
                    result.Data = repo.GetSingle(keyValues);

                    if (result.Data != null)
                    {
                        result.Success = true;
                    }
                    else { throw new Exception(BaseConstants.MESSAGE_DATA_IS_NOT_EXIST); }

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<T>(ex))
                {
                    result.Success = false;
                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;

                    return result;
                }
            }
        }

        [HttpGet]
        public virtual IGenericWebApiResult DynamicData(string term)
        {
            try
            {
                using (var result = new GenericWebApiResult<dynamic>())
                {
                    result.Data = repo.DynamicData(term);
                    if (result.Data == null) throw new Exception(BaseConstants.MESSAGE_DATA_IS_NOT_EXIST);

                    result.Success = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<dynamic>(ex))
                {
                    result.Success = false;
                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;

                    return result;
                }
            }

        }


        [HttpPost]
        public virtual IGenericWebApiResult Post(T data)
        {
            try
            {
                using (var result = new GenericWebApiResult<T>())
                {
                    result.Data = data;
                    result.Success = repo.Create(result.Data);
                    result.Message = result.Success ? BaseConstants.MESSAGE_CREATE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<T>(ex))
                {
                    result.Data = data;
                    result.Success = false;

                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;

                    return result;
                }
            }
        }

        [HttpPut]
        public virtual IGenericWebApiResult Put(T data)
        {
            try
            {
                using (var result = new GenericWebApiResult<T>())
                {
                    result.Data = data;
                    result.Success = repo.Update(result.Data);
                    result.Message = result.Success ? BaseConstants.MESSAGE_UPDATE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<T>(ex))
                {
                    result.Data = data;
                    result.Success = false;

                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;

                    return result;
                }
            }
        }

        [HttpDelete]
        public virtual IGenericWebApiResult Delete(params object[] keyValues)
        {
            try
            {
                using (var result = new GenericWebApiResult<T>())
                {
                    result.Success = repo.Delete(keyValues);
                    result.Message = result.Success ? BaseConstants.MESSAGE_DELETE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<T>(ex))
                {
                    result.Success = false;

                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;

                    return result;
                }
            }
        }


        [HttpPost]
        public virtual IGenericWebApiResult Dropdown([FromBody]string term)
        {
            try
            {
                using (var result = new GenericWebApiResult<T>())
                {
                    result.Dropdown = repo.Dropdown(new T(), term);
                    result.Success = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<T>(ex))
                {
                    result.Success = false;
                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;
                    result.Dropdown = new List<Dropdown>();

                    return result;
                }
            }
        }

        [HttpPost]
        public virtual IGenericWebApiResult DropdownByKey([FromBody]string term)
        {
            try
            {
                using (var result = new GenericWebApiResult<T>())
                {
                    result.Dropdown = repo.DropdownByKey(new T(), term);
                    result.Success = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<T>(ex))
                {
                    result.Success = false;
                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;
                    result.Dropdown = new List<Dropdown>();

                    return result;
                }
            }
        }

        [HttpPost]
        public virtual HttpResponseMessage DataTables()
        {
            try
            {
                //var request = HttpContext.Current.Request;
                //var wrapper = new HttpRequestWrapper(request);
                //var parser = new DataTablesParser<T>(wrapper, repo.GetAll().AsQueryable());
                var datatable = repo.DataTables();
                var result = Request.CreateResponse(System.Net.HttpStatusCode.OK, datatable);

                return result;
            }
            catch (Exception ex)
            {
                var result = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);

                return result;
            }
        }

        #endregion

        #region == Other Method ==
        protected virtual IGenericWebApiResult GetLates()
        {
            try
            {
                using (var result = new GenericWebApiResult<T>())
                {
                    result.Data = repo.GetLates() ?? new T();

                    result.Success = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<T>(ex))
                {
                    result.Success = false;
                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;

                    return result;
                }
            }
        }

        protected virtual HttpResponseMessage DataTables(IQueryable<T> queryable)
        {
            try
            {
                var request = HttpContext.Current.Request;
                var wrapper = new HttpRequestWrapper(request);
                var parser = new DataTablesParser<T>(wrapper, queryable);
                var result = Request.CreateResponse(System.Net.HttpStatusCode.OK, parser.Parse());

                return result;
            }
            catch (Exception ex)
            {
                var result = Request.CreateResponse(System.Net.HttpStatusCode.NoContent, ex.Message);

                return result;
            }
        }

        protected string GetReportPath(string localReportName)

        {
            var file =  HttpContext.Current.Server.MapPath(@"~/Reports/" + localReportName + ".rdlc");
            
            return file;
        }

        protected string ReportFileName(string fileName)
        {
            return fileName + DateTime.Now.ToString("ddMMyyyyHHmmss");
        }
        protected HttpResponseMessage ShowPdfReport(LocalReport localReport, string fileName)
        {
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>8.27in</PageWidth>" +
            "  <PageHeight>11.69in</PageHeight>" +
            "  <MarginTop>1.9685in</MarginTop>" +
            "  <MarginLeft>0.7874in</MarginLeft>" +
            "  <MarginRight>0.59055in</MarginRight>" +
            "  <MarginBottom>0.7874in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report
            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(renderedBytes)
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format(fileName + ".pdf")
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }
        protected HttpResponseMessage ShowPdfReport(LocalReport localReport, string fileName,
            string MarginBottom = "0.7874", string pageWidth = "8.27", string pageHeight = "11.69",
            string marginTop = "1.9685", string marginLeft = "0.7874", string marginRight = "0.59055")
        {
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>" + pageWidth + "in</PageWidth>" +
            "  <PageHeight>" + pageHeight + "in</PageHeight>" +
            "  <MarginTop>" + marginTop + "in</MarginTop>" +
            "  <MarginLeft>" + marginLeft + "in</MarginLeft>" +
            "  <MarginRight>" + marginRight + "in</MarginRight>" +
            "  <MarginBottom>" + MarginBottom + "in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report
            renderedBytes = localReport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(renderedBytes)
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = string.Format(fileName + ".pdf")
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }

        protected HttpResponseMessage ExportToExcelReport(MemoryStream memoryStream, string fileName)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(memoryStream.GetBuffer())
            };

            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileName = string.Format("{0}.xlsx", fileName)
            };

            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            return result;
        }

        protected string GetTemplatePath(string templateName)
        {
            return HttpContext.Current.Server.MapPath("~/Templates/" + templateName);
        }

        protected string GetUploadPath(string fileName)
        {
            return HttpContext.Current.Server.MapPath("~/Uploads/" + fileName);
        }
        protected void DeleteUploadFile(string fileName)
        {
            try
            {
                var file = this.GetUploadPath(fileName);
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                else
                {
                    throw new Exception("File not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region == Overide ==
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            UserProfile userProfile = new UserProfile()
            {
                UserID = this.UserID,
                ProfileID = this.RoleID,
                IsAdministrator = Convert.ToBoolean(this.IsAdministrator)
            };

            repo.UserProfile = userProfile;
        }
        #endregion
    }
}
