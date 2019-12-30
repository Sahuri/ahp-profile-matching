using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using Ahp.Core.Repositories.Abstractions;
using Ahp.Core.Repositories.Abstractions.Administrasi;
using Ahp.Core.Security;
using System.Collections.Generic;
using System.Linq;
using DataTablesParser;
using System;
using System.Linq.Expressions;
using Ahp.Core.Repositories.Abstractions.Proses;
using System.Web;

namespace Ahp.Core.Repositories.Concretes.Proses
{
    public class PerbandinganKriteriaRepository : GenericDataRepository<PerbandinganKriteria, IGenericContext>, IPerbandinganKriteriaRepository
    {
        protected IGenericContext ctx;
        public PerbandinganKriteriaRepository(IGenericContext ctx)
            : base(ctx)
        {
            this.ctx = ctx;
        }
        private List<EigenVectorVal> CalEigenVector(MatrixVal[,] value, int c, int r) {
            List<EigenVectorVal> eigenVectors = new List<EigenVectorVal>();
            for (var x = 0; x < c; x++)
            {
                decimal result = 0;
                int? kode=0;
                for (var y = 0; y < r; y++)
                {
                    result = result + value[x, y].Nilai;
                    kode = value[x, y].Kode1;
                }
                var eigen = new EigenVectorVal() { Kode = kode, Nilai = result };
                eigenVectors.Add(eigen);
            }

            var total =eigenVectors.Sum(x => x.Nilai);
            eigenVectors.ForEach(i =>
            {
                i.Nilai= Math.Round((i.Nilai/total)+0.0000m, 4, MidpointRounding.ToEven);
            });

            return eigenVectors;
        }
        private MatrixVal[,] PerkalianMatrix(MatrixVal[,] value,int c,int r) {
            MatrixVal[,] MatrixPerkalian = new MatrixVal[c, r];

            for (var x = 0; x < c; x++)
            {
                for (var y = 0; y < r; y++)
                {
                    decimal result = 0;
                    for (var k = 0; k < r; k++)
                    {
                        result = result + Math.Round((value[x, k].Nilai * value[k, y].Nilai) + 0.0000m, 4, MidpointRounding.ToEven);
                    }
                    var cell = new MatrixVal();
                    cell.Kode1 = value[x, y].Kode1;
                    cell.Kode2 = value[x, y].Kode2;
                    cell.Nilai = Math.Round(result + 0.0000m, 4, MidpointRounding.ToEven);
                    MatrixPerkalian[x, y] = cell;
                }

            }

            return MatrixPerkalian;
        }

        public Matrix Create(List<SpPerbandinganKriteria_Result> model)
        {
            var matrix = new Matrix();
            matrix.Kriterias= base.ctx.Set<Kriteria>().OrderBy(x=>x.Kode).ToList();
            List<MatrixVal> arr = new List<MatrixVal>();
            int idxR,idxC, r, c = matrix.Kriterias.Count();
            r = c;
            idxC = 0;
            
            MatrixVal[,] MatrixValue = new MatrixVal[c, r];

            foreach (var kriteria in matrix.Kriterias) {
                idxR = 0;
                foreach (var item in model.Where(x => x.Kode1.Equals(kriteria.Kode))) {
                    var matrixVal = new MatrixVal();
                    matrixVal.Kode1 = item.Kode1;
                    matrixVal.Kode2 = item.Kode2;
                    matrixVal.Nilai = Math.Round(((decimal)item.Nilai1 / (decimal)item.nilai2) + 0.0000m, 4,MidpointRounding.ToEven);
                    MatrixValue[idxC,idxR] = matrixVal;
                    arr.Add(matrixVal);
                    idxR++;
                }
                idxC++;
            }
            matrix.PerbandinganKriteria = arr;
            matrix.MatrixPerbandingan = MatrixValue;
            var matrixPerkalian = PerkalianMatrix(MatrixValue, c, r);
            matrix.MatrixPerkalian = matrixPerkalian;
            matrix.EigenVector=CalEigenVector(matrixPerkalian, c, r);



            foreach (var item in model)
            {
                var perbandinganKriteria = base.ctx.Set<PerbandinganKriteria>().SingleOrDefault(
                    x => x.Kode==item.Kode1 && x.Kode2==item.Kode2);
                if (perbandinganKriteria == null)
                {
                    perbandinganKriteria = new PerbandinganKriteria();
                    perbandinganKriteria.Kode =(int) item.Kode1;
                    perbandinganKriteria.Kode2 = (int)item.Kode2;
                    perbandinganKriteria.Nilai = item.Nilai1;
                    perbandinganKriteria.Nilai2 = item.nilai2;
                    base.ctx.Set<PerbandinganKriteria>().Add(perbandinganKriteria);
                }

                perbandinganKriteria.Kode = (int)item.Kode1;
                perbandinganKriteria.Kode2 = (int)item.Kode2;
                perbandinganKriteria.Nilai = item.Nilai1;
                perbandinganKriteria.Nilai2 = item.nilai2;

            }

            foreach (var krit in matrix.Kriterias) {
                var e= matrix.EigenVector.SingleOrDefault(u => u.Kode==krit.Kode);
                if (e != null) {
                    krit.EigenvectorScore =(double)e.Nilai;
                }
            }
        

            var rest=base.ctx.SaveChanges() > 0;

            return matrix;
        }

        public override bool Create(PerbandinganKriteria model)
        {
            var result = base.Create(model);

            return result;
        }

        public override bool Update(PerbandinganKriteria model)
        {
            
           
            var rec = base.ctx.Set<PerbandinganKriteria>().SingleOrDefault(x=>x.Kode.Equals(model.Kode) && x.Kode2.Equals(model.Kode2));
            if (rec != null)
            {
                rec.Kode = model.Kode;
                rec.Kode2 = model.Kode2;
                rec.Nilai = model.Nilai;
                rec.CreatedBy = model.CreatedBy;
                rec.CreatedDate = model.CreatedDate;
                rec.Nilai2 = model.Nilai2;
                rec.UpdatedBy = this.UserProfile.UserID;
                rec.UpdatedDate = DateTime.Now;
                
            }

            return base.Update(rec);
        }





        public List<SpPerbandinganKriteria_Result> SpDataTables()
        {
            List<SpPerbandinganKriteria_Result> queryable;
            using (var ctx = new AhpEntities()) {
                queryable = ctx.SpPerbandinganKriteria().ToList();
                

            }
          

            return queryable;
        }



        public override FormatedList<PerbandinganKriteria> DataTables()
        {
            var dataTables = base.DataTables();
            return dataTables;
        }

        public override IQueryable<PerbandinganKriteria> FindBy(Expression<Func<PerbandinganKriteria, bool>> predicate)
        {
            var recs = base.FindBy(predicate);
            return recs;
        }

        public override List<PerbandinganKriteria> GetAll()
        {
            var recs = base.GetAll();
            return recs;
        }


        public override List<PerbandinganKriteria> GetList(Func<PerbandinganKriteria, bool> where, params Expression<Func<PerbandinganKriteria, object>>[] navigationProperties)
        {
            var recs = base.GetList(where, navigationProperties);
            return recs;
        }

        public override PerbandinganKriteria GetSingle(Func<PerbandinganKriteria, bool> where, params Expression<Func<PerbandinganKriteria, object>>[] navigationProperties)
        {
            var rec = base.GetSingle(where, navigationProperties);
 
            return rec;
        }

        public override PerbandinganKriteria GetSingle(params object[] keyValues)
        {
            var rec = base.GetSingle(keyValues);
            
            return rec;
        }

        public override PerbandinganKriteria GetSingle(string paramValues)
        {
            var rec = base.GetSingle(paramValues);
             return rec;
        }

    }
}
