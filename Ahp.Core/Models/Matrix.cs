using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahp.Core.Models
{
    public class MatrixVal
    {
        public int? Kode1 { get; set; }
        public int? Kode2 { get; set; }
        public decimal Nilai { get; set; }
    }

    public class EigenVectorVal
    {
        public int? Kode { get; set; }
        public decimal Nilai { get; set; }
    }

    public class Matrix
    {
        public List<Kriteria> Kriterias { get; set;}
        public List<MatrixVal> PerbandinganKriteria { get; set; }
        public MatrixVal[,]  MatrixPerbandingan{ get; set; }
        public MatrixVal[,]  MatrixPerkalian { get; set; }
        public List<EigenVectorVal> EigenVector { get; set; }
    }


}
