using Ahp.Core.Common;
using Ahp.Core.GenericRepositories.Abstractions;
using Ahp.Core.Models;
using DataTablesParser;
using System.Collections.Generic;

namespace Ahp.Core.Repositories.Abstractions.Proses
{
    interface INilaiScoringRepository: IGenericDataRepository<NilaiScoring>
    {
        List<Dropdown> DropdownPosisi(string term);
        Gap Find(string Periode, string Posisi);
        List<Dropdown> DropdownPeriode(string term);
        List<Dropdown> DropdownCalonKaryawan(string term);
        bool Create(CalonKaryawanGap model);
        bool Update(CalonKaryawanGap model);
        List<Dropdown> DropdownJumlah(string term);
        List<VwRankingPosisi> FindRankingPosisi(string periode, string posisi, string jumlah);
        List<VwRankingScore> FindRankingScoring(string periode);

    }
}
