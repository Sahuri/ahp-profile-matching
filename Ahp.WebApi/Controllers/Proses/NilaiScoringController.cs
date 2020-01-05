using Ahp.Core.Common;
using Ahp.Core.Models;
using Ahp.Core.Reposirories.Concretes;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Concretes.Proses;
using Microsoft.Reporting.WebForms;
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
        public virtual HttpResponseMessage SpRankingScore(string periode,string posisi)
        {
            try
            {
                var datatable = genRepo.FindRankingScoring(periode, posisi);
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

        private string TodayName() {

            Dictionary<int, string> todayName = new Dictionary<int, string>() {
                {1,"Senin" },
                {2,"Selasa" },
                {3,"Rabu" },
                {4,"Kamis" },
                {5,"Jumat" },
                {6,"Sabtu" },
                {7,"Minggu" }
            };

            return todayName[((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek];
        }


        private string MonthName()
        {

            Dictionary<int, string> monthName = new Dictionary<int, string>() {
                {1,"Januari" },
                {2,"Februari" },
                {3,"Maret" },
                {4,"April" },
                {5,"Mei" },
                {6,"Juni" },
                {7,"Juli" },
                {8,"Agustus" },
                {9,"September" },
                {10,"Oktober" },
                {11,"November" },
                {12,"Desember" }
            };
            return String.Format("{0}-{1}-{2}", DateTime.Now.Day,monthName[(int)DateTime.Now.Month], DateTime.Now.Year);
        }

        [HttpGet]
        public HttpResponseMessage Print(string periode, string posisi, string jumlah)
        {
            try
            {


                ReportParameter[] Params = new ReportParameter[5];
                string Nomor =String.Format("{0}/JPS-HR/SK/VI{1}/{2}",DateTime.Now.ToString("FFF"), DateTime.Now.Month.ToString("00"), DateTime.Now.Year);
                Params[0] = new ReportParameter("NomorSurat", Nomor);
                Params[1] = new ReportParameter("WeekDay", TodayName());
                Params[2] = new ReportParameter("Today", MonthName());
                Params[3] = new ReportParameter("Periode", periode);
                Params[4] = new ReportParameter("Posisi", ctx.Set<Lowongan>().Find(posisi).Nama);

                ReportDataSource reportDs = new ReportDataSource("DataSet1", genRepo.FindRankingPosisi(periode, posisi, jumlah));

                using (LocalReport localReport = new LocalReport())
                {
                    string rdlc = "print";
                    
                    localReport.DataSources.Add(reportDs);
                    

                    localReport.ReportPath = GetReportPath(rdlc);
                    localReport.SetParameters(Params);
                    localReport.Refresh();

                    return ShowPdfReport(localReport, "hasil_akhir", "0.2", "8.27", "11.69", "0.30", "0.40", "0.20");
                }

            }
            catch (ReportViewerException ex)
            {
                throw new Exception(ex.InnerException.Message);
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
