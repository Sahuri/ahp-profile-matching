using Ahp.Core.Common;
using Ahp.Core.Models;
using Ahp.Core.Reposirories.Concretes;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Abstractions.Administrasi;
using Ahp.Core.Repositories.Concretes.Administrasi;
using Ahp.WebApi.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Http;

namespace Ahp.WebApi.Controllers.Administrasi
{
    [Authorize]
    [RoutePrefix(@"api/administrasiuser")]
    public class AdministrasiUserController : BaseApiController<AdministrasiUser>
    {
        private GenericContext ctx;
        private IAdministrasiUserRepository objRepo;

        public AdministrasiUserController()
        {
            ctx = new GenericContext();
            objRepo = new AdministrasiUserRepository(ctx);
            base.repo = objRepo;
        }

        [HttpGet, Route("GetSimpleData")]
        public IGenericWebApiResult GetSimpleData(string keyValues)
        {
            try
            {
                using (var result = new GenericWebApiResult<AdministrasiUserLogin>())
                {
                    var rec = repo.GetSingle(keyValues);
                    var data = new AdministrasiUserLogin()
                    {
                        Kode = rec.Kode,
                        Nama = rec.Nama,
                        Alamat = rec.Alamat,
                        Telepon = rec.Telepon,
                        Email = rec.Email,
                        Avatar = rec.Avatar ?? "male.png"
                    };

                    var dir = HttpContext.Current.Server.MapPath("~/Avatars");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    var path = Path.Combine(dir, data.Avatar);
                    if (File.Exists(path))
                    {
                        using (Image image = Image.FromFile(path))
                        {
                            using (MemoryStream m = new MemoryStream())
                            {
                                image.Save(m, image.RawFormat);
                                byte[] imageBytes = m.ToArray();

                                // Convert byte[] to Base64 String
                                string base64String = Convert.ToBase64String(imageBytes).TrimStart(',');
                                data.Avatar = "data:image/png;base64," + base64String;
                            }
                        }
                    }

                    result.Data = data;
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
                using (var result = new GenericWebApiResult<AdministrasiUserLogin>(ex))
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
        public IGenericWebApiResult ChangePassword(string oldPass, string newPass)
        {
            try
            {
                using (var result = new GenericWebApiResult<dynamic>())
                {
                    result.Data = new ExpandoObject();
                    result.Success = objRepo.ChangePassword(oldPass, newPass);
                    result.Message = result.Success ? BaseConstants.MESSAGE_CHANGE_PASSWORD_SUCCESS: BaseConstants.MESSAGE_INVALID_OLD_PASSWORD;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<dynamic>(ex))
                {
                    result.Data = new ExpandoObject();
                    result.Success = false;

                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;

                    return result;
                }
            }
        }

        [HttpPost, Route("ResetPassword")]
        public IGenericWebApiResult ResetPassword()
        {
            try
            {
                using (var result = new GenericWebApiResult<dynamic>())
                {
                    result.Data = new ExpandoObject();
                    result.Success = objRepo.ResetPassword();
                    result.Message = result.Success ? BaseConstants.MESSAGE_RESET_PASSWORD_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<dynamic>(ex))
                {
                    result.Data = new ExpandoObject();
                    result.Success = false;

                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;

                    return result;
                }
            }
        }

        [HttpPost, Route("UploadAvatar")]
        public HttpResponseMessage UploadAvatar()
        {
            try
            {
                using (var result = new GenericWebApiResult<dynamic>())
                {
                    var forms = HttpContext.Current.Request.Form;
                    var formAvatar = forms.GetValues("avatar");

                    var avatar = formAvatar.Length > 0 ? JsonConvert.DeserializeObject<string>(formAvatar[0]) : "male.png";

                    result.Success = objRepo.UploadAvatar(avatar);
                    result.Data = new ExpandoObject();
                    result.Message = result.Success ? BaseConstants.MESSAGE_UPDATE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                    if (result.Success)
                    {
                        var fileCount = HttpContext.Current.Request.Files.Count;
                        for (int i = 0; i < fileCount; i++)
                        {
                            HttpPostedFile file = HttpContext.Current.Request.Files[i];
                            var dir = HttpContext.Current.Server.MapPath("~/Avatars");
                            if (!Directory.Exists(dir))
                            {
                                Directory.CreateDirectory(dir);
                            }

                            var path = Path.Combine(dir, file.FileName);
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }

                            file.SaveAs(path);

                            using (Image image = Image.FromFile(path))
                            {
                                using (MemoryStream m = new MemoryStream())
                                {
                                    image.Save(m, image.RawFormat);
                                    byte[] imageBytes = m.ToArray();

                                    // Convert byte[] to Base64 String
                                    string base64String = Convert.ToBase64String(imageBytes).TrimStart(',');
                                    dynamic data = new ExpandoObject();
                                    data.Avatar = "data:image/png;base64," + base64String;
                                    result.Data = data;
                                }
                            }
                        }
                    }

                    return Request.CreateResponse(result);
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<ExpandoObject>(ex))
                {
                    result.Success = false;

                    dynamic more = new ExpandoObject();
                    more.Errors = ex.Message;
                    result.More = more;

                    return Request.CreateResponse(result);
                }
            }
        }

        [HttpPost]
        public HttpResponseMessage DataTablesAuditor()
        {
            try
            {
                var queryable = objRepo.DatatablesAuditor();

                return base.DataTables(queryable);
            }
            catch (Exception ex)
            {
                var result = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);

                return result;
            }
        }

    }
}
