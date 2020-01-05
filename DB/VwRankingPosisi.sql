USE [AHP]
GO

/****** Object:  View [dbo].[VwRankingPosisi]    Script Date: 05/01/2020 21:42:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[VwRankingPosisi]
AS
SELECT  C.Periode, C.Jumlah, A.Pendidikan, C.Kode AS Posisi, A.Kode, A.Nama, 
A.Email, 
MAX(B.NilaiTotal) AS NilaiTotal
FROM            dbo.CalonKaryawan AS A INNER JOIN
                         dbo.NilaiScoring AS B ON A.Kode = B.KodeKaryawan INNER JOIN
                         dbo.Lowongan AS C ON B.KodeLowongan = C.Kode
GROUP BY C.Periode, A.Kode, A.Nama, C.Jumlah, A.Pendidikan, C.Kode,A.Email

GO


