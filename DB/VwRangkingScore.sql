USE [AHP]
GO

/****** Object:  View [dbo].[VwRankingScore]    Script Date: 05/01/2020 21:42:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[VwRankingScore]
AS
SELECT C.Periode, A.Kode, A.Nama,
 C.Kode Posisi,
 C.Jumlah,
 MAX(B.NilaiTotal) AS NilaiTotal
FROM            dbo.CalonKaryawan AS A INNER JOIN
                         dbo.NilaiScoring AS B ON A.Kode = B.KodeKaryawan INNER JOIN
                         dbo.Lowongan AS C ON B.KodeLowongan = C.Kode
GROUP BY C.Periode, A.Kode, A.Nama,C.Kode,C.Jumlah
GO


