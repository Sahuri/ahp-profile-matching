using Ahp.Core.Common;
using Ahp.Core.Models;
using Ahp.Core.Reposirories.Concretes;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Concretes.Proses;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ahp.WebApi.Controllers.Proses
{
    [Authorize]
    [RoutePrefix(@"api/nilaiscoring")]
    public class NilaiScoringController : BaseApiController<NilaiScoring>
    {
        private GenericContext ctx;
        private NilaiScoringRepository genRepo;

        public NilaiScoringController()
        {
            ctx = new GenericContext();
            genRepo = new NilaiScoringRepository(ctx);
            base.repo = genRepo;
        }

        [HttpPost]
        public virtual IGenericWebApiResult DropdownCalonKaryawan([FromBody]string term)
        {
            try
            {
                using (var result = new GenericWebApiResult<Dropdown>())
                {
                    result.Dropdown = genRepo.DropdownCalonKaryawan(term);
                    result.Success = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<Dropdown>(ex))
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
        public virtual IGenericWebApiResult DropdownPeriode([FromBody]string term)
        {
            try
            {
                using (var result = new GenericWebApiResult<Dropdown>())
                {
                    result.Dropdown = genRepo.DropdownPeriode(term);
                    result.Success = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<Dropdown>(ex))
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
        public virtual IGenericWebApiResult DropdownPosisi(string term)
        {
            try
            {
                using (var result = new GenericWebApiResult<Dropdown>())
                {
                    result.Dropdown = genRepo.DropdownPosisi(term);
                    result.Success = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<Dropdown>(ex))
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
        public virtual IGenericWebApiResult DropdownJumlah(string term)
        {
            try
            {
                using (var result = new GenericWebApiResult<Dropdown>())
                {
                    result.Dropdown = genRepo.DropdownJumlah(term);
                    result.Success = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<Dropdown>(ex))
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

        [HttpGet]
        public virtual HttpResponseMessage SpDataTables(string posisi,string periode)
        {
            try
            {
                var datatable = genRepo.Find(periode, posisi);
                var result = Request.CreateResponse(System.Net.HttpStatusCode.OK, datatable);

                return result;
            }
            catch (Exception ex)
            {
                var result = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);

                return result;
            }
        }

        [HttpGet]
        public virtual HttpResponseMessage SpRankingScore(string periode)
        {
            try
            {
                var datatable = genRepo.FindRankingScoring(periode);
                var result = Request.CreateResponse(System.Net.HttpStatusCode.OK, datatable);

                return result;
            }
            catch (Exception ex)
            {
                var result = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);

                return result;
            }
        }

        [HttpGet]
        public virtual HttpResponseMessage SpRankingPosisi(string periode, string posisi,string jumlah)
        {
            try
            {
                var datatable = genRepo.FindRankingPosisi(periode, posisi, jumlah);
                var result = Request.CreateResponse(System.Net.HttpStatusCode.OK, datatable);

                return result;
            }
            catch (Exception ex)
            {
                var result = Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, ex.Message);

                return result;
            }
        }

        [HttpPost]
        public virtual IGenericWebApiResult Create(CalonKaryawanGap data)
        {
            try
            {
                using (var result = new GenericWebApiResult<CalonKaryawanGap>())
                {
                    result.Data = data;
                    result.Success = genRepo.Create(result.Data);
                    result.Message = result.Success ? BaseConstants.MESSAGE_CREATE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<CalonKaryawanGap>(ex))
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

        [HttpPost]
        public virtual IGenericWebApiResult Update(CalonKaryawanGap data)
        {
            try
            {
                using (var result = new GenericWebApiResult<CalonKaryawanGap>())
                {
                    result.Data = data;
                    result.Success = genRepo.Update(result.Data);
                    result.Message = result.Success ? BaseConstants.MESSAGE_CREATE_SUCCESS : BaseConstants.MESSAGE_INVALID_DATA;

                    return result;
                }
            }
            catch (Exception ex)
            {
                using (var result = new GenericWebApiResult<CalonKaryawanGap>(ex))
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
    }
}
