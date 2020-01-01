using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahp.Core.Models
{
    public class NilaiGapBobot {
        public int? kodeKriteria { get; set; }
        public string kriteria { get; set; }
        public decimal nilaiTarget { get; set; }
        public decimal nilaiGap { get; set; }
        public decimal nilaiBobot { get; set; }
        public decimal nilaiAlternatif { get; set; }
        public decimal rangkingScoring { get; set; }
        public decimal ncf { get; set; }
        public decimal nsf { get; set; }
        public decimal nilaiTotal { get; set; }
    }

    public class CalonKaryawanGap {
        
        public string kodeKaryawan { get; set; }
        public string namaKaryawan { get; set; }
        public string kodeLowongan { get; set; }
        public string periode { get; set; }
        public List<NilaiGapBobot> nilaiGapBobots{ get; set; }
    }

    public class Gap
    {
        public List<Kriteria> kriterias { get; set; }
        public List<SpProseGap_Result> prosesGap { get; set; }
        public List<CalonKaryawanGap> calonKaryawanGaps { get; set; }


    }
}
