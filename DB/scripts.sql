USE [AHP]
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'VwRankingScore', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VwRankingScore'
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'VwRankingScore', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VwRankingScore'
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'VwRankingPosisi', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VwRankingPosisi'
GO
IF  EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'VwRankingPosisi', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VwRankingPosisi'
GO
/****** Object:  StoredProcedure [dbo].[SpProseGap]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpProseGap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SpProseGap]
GO
/****** Object:  StoredProcedure [dbo].[SpPerbandinganKriteria]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpPerbandinganKriteria]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SpPerbandinganKriteria]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_AdministrasiUser_KodePlant]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AdministrasiUser] DROP CONSTRAINT [DF_AdministrasiUser_KodePlant]
END
GO
/****** Object:  View [dbo].[VwRankingScore]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwRankingScore]'))
DROP VIEW [dbo].[VwRankingScore]
GO
/****** Object:  View [dbo].[VwRankingPosisi]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwRankingPosisi]'))
DROP VIEW [dbo].[VwRankingPosisi]
GO
/****** Object:  Table [dbo].[PerbandinganKriteria]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PerbandinganKriteria]') AND type in (N'U'))
DROP TABLE [dbo].[PerbandinganKriteria]
GO
/****** Object:  Table [dbo].[NilaiScoring]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NilaiScoring]') AND type in (N'U'))
DROP TABLE [dbo].[NilaiScoring]
GO
/****** Object:  Table [dbo].[Lowongan]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lowongan]') AND type in (N'U'))
DROP TABLE [dbo].[Lowongan]
GO
/****** Object:  Table [dbo].[Kriteria]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kriteria]') AND type in (N'U'))
DROP TABLE [dbo].[Kriteria]
GO
/****** Object:  Table [dbo].[CalonKaryawan]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CalonKaryawan]') AND type in (N'U'))
DROP TABLE [dbo].[CalonKaryawan]
GO
/****** Object:  Table [dbo].[AdministrasiUser]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiUser]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrasiUser]
GO
/****** Object:  Table [dbo].[AdministrasiTombol]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiTombol]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrasiTombol]
GO
/****** Object:  Table [dbo].[AdministrasiRoleUser]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiRoleUser]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrasiRoleUser]
GO
/****** Object:  Table [dbo].[AdministrasiRoleDtl]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiRoleDtl]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrasiRoleDtl]
GO
/****** Object:  Table [dbo].[AdministrasiRole]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiRole]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrasiRole]
GO
/****** Object:  Table [dbo].[AdministrasiMenu]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiMenu]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrasiMenu]
GO
/****** Object:  Table [dbo].[AdministrasiHakAksesTombol]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiHakAksesTombol]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrasiHakAksesTombol]
GO
/****** Object:  Table [dbo].[AdministrasiHakAksesRole]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiHakAksesRole]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrasiHakAksesRole]
GO
/****** Object:  Table [dbo].[AdministrasiHakAksesMenu]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiHakAksesMenu]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrasiHakAksesMenu]
GO
/****** Object:  Table [dbo].[AdministrasiHakAkses]    Script Date: 01/01/2020 15:30:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiHakAkses]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrasiHakAkses]
GO
/****** Object:  Table [dbo].[AdministrasiHakAkses]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiHakAkses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdministrasiHakAkses](
	[Kode] [varchar](30) NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_AdministrasiHakAkses] PRIMARY KEY CLUSTERED 
(
	[Kode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AdministrasiHakAksesMenu]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiHakAksesMenu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdministrasiHakAksesMenu](
	[KodeHakAkses] [varchar](30) NOT NULL,
	[KodeMenu] [varchar](15) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_AdministrasiHakAksesMenu] PRIMARY KEY CLUSTERED 
(
	[KodeHakAkses] ASC,
	[KodeMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AdministrasiHakAksesRole]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiHakAksesRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdministrasiHakAksesRole](
	[KodeHakAkses] [varchar](30) NOT NULL,
	[KodeRole] [varchar](30) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_AdministrasiHakAksesRole] PRIMARY KEY CLUSTERED 
(
	[KodeHakAkses] ASC,
	[KodeRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AdministrasiHakAksesTombol]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiHakAksesTombol]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdministrasiHakAksesTombol](
	[KodeHakAkses] [varchar](30) NOT NULL,
	[KodeTombol] [varchar](15) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_AdministrasiHakAksesTombol] PRIMARY KEY CLUSTERED 
(
	[KodeHakAkses] ASC,
	[KodeTombol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AdministrasiMenu]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiMenu]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdministrasiMenu](
	[Kode] [varchar](15) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Url] [varchar](100) NOT NULL,
	[Icon] [varchar](30) NULL,
	[Class] [varchar](30) NULL,
	[Parent] [varchar](15) NOT NULL,
	[Sequence] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](15) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](15) NULL,
 CONSTRAINT [PK_AdministrasiMenu] PRIMARY KEY CLUSTERED 
(
	[Kode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AdministrasiRole]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiRole]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdministrasiRole](
	[Kode] [varchar](30) NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_AdministrasiRole] PRIMARY KEY CLUSTERED 
(
	[Kode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AdministrasiRoleDtl]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiRoleDtl]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdministrasiRoleDtl](
	[KodeRole] [varchar](30) NOT NULL,
	[KodeWilayahKerja] [varchar](15) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_AdministrasiRoleDtl] PRIMARY KEY CLUSTERED 
(
	[KodeRole] ASC,
	[KodeWilayahKerja] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AdministrasiRoleUser]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiRoleUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdministrasiRoleUser](
	[KodeRole] [varchar](30) NOT NULL,
	[KodeUser] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_AdministrasiRoleUser] PRIMARY KEY CLUSTERED 
(
	[KodeRole] ASC,
	[KodeUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AdministrasiTombol]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiTombol]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdministrasiTombol](
	[Kode] [varchar](15) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Icon] [varchar](30) NULL,
	[Class] [varchar](30) NULL,
	[KodeMenu] [varchar](15) NULL,
	[Parameter] [varchar](255) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_AdministrasiTombol] PRIMARY KEY CLUSTERED 
(
	[Kode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AdministrasiUser]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrasiUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdministrasiUser](
	[Kode] [varchar](50) NOT NULL,
	[Nama] [varchar](100) NOT NULL,
	[Alamat] [varchar](255) NULL,
	[Telepon] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Avatar] [varchar](100) NULL,
	[IsAdministrator] [bit] NULL,
	[Aktif] [bit] NOT NULL,
	[KodePlant] [varchar](10) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
 CONSTRAINT [PK_AdministrasiUser] PRIMARY KEY CLUSTERED 
(
	[Kode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CalonKaryawan]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CalonKaryawan]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CalonKaryawan](
	[Kode] [varchar](32) NOT NULL,
	[Nama] [varchar](50) NULL,
	[Alamat] [varchar](50) NULL,
	[Telepon] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Kelamin] [varchar](20) NULL,
	[Tgllahir] [date] NULL,
	[Pendidikan] [varchar](10) NULL,
	[NilaiAkhir] [float] NULL,
	[Keterangan] [varchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_CalonKaryawan] PRIMARY KEY CLUSTERED 
(
	[Kode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Kriteria]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Kriteria]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Kriteria](
	[Kode] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [varchar](50) NULL,
	[Faktor] [int] NULL,
	[NilaiTarget] [int] NULL,
	[JumlahBaris] [int] NULL,
	[EigenvectorScore] [float] NULL,
	[NilaiKali] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_Kriteria] PRIMARY KEY CLUSTERED 
(
	[Kode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Lowongan]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lowongan]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Lowongan](
	[Kode] [varchar](32) NOT NULL,
	[Nama] [varchar](20) NULL,
	[Jumlah] [int] NULL,
	[TglMulai] [date] NULL,
	[TglAkhir] [date] NULL,
	[Periode] [varchar](50) NULL,
	[NoSurat] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_Lowongan] PRIMARY KEY CLUSTERED 
(
	[Kode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[NilaiScoring]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NilaiScoring]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NilaiScoring](
	[Kode] [int] IDENTITY(1,1) NOT NULL,
	[KodeKaryawan] [varchar](32) NULL,
	[KodeLowongan] [varchar](32) NULL,
	[KodeKriteria] [int] NULL,
	[NilaiGap] [float] NULL,
	[NilaiBobot] [float] NULL,
	[NilaiAlternatif] [float] NULL,
	[RangkingScoring] [float] NULL,
	[NilaiTotal] [float] NULL,
	[Ncf] [float] NULL,
	[Nsf] [float] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_NilaiScoring] PRIMARY KEY CLUSTERED 
(
	[Kode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PerbandinganKriteria]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PerbandinganKriteria]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PerbandinganKriteria](
	[Kode] [int] NOT NULL,
	[Kode2] [int] NOT NULL,
	[Nilai] [float] NOT NULL,
	[Nilai2] [float] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_PerbandinganKriteria] PRIMARY KEY CLUSTERED 
(
	[Kode] ASC,
	[Kode2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[VwRankingPosisi]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwRankingPosisi]'))
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[VwRankingPosisi]
AS
SELECT        TOP (100) PERCENT C.Periode, C.Jumlah, A.Pendidikan, C.Kode AS Posisi, A.Kode, A.Nama, MAX(B.NilaiTotal) AS NilaiTotal
FROM            dbo.CalonKaryawan AS A INNER JOIN
                         dbo.NilaiScoring AS B ON A.Kode = B.KodeKaryawan INNER JOIN
                         dbo.Lowongan AS C ON B.KodeLowongan = C.Kode
GROUP BY C.Periode, A.Kode, A.Nama, C.Jumlah, A.Pendidikan, C.Kode
ORDER BY NilaiTotal DESC
' 
GO
/****** Object:  View [dbo].[VwRankingScore]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VwRankingScore]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[VwRankingScore]
AS
SELECT        TOP (100) PERCENT C.Periode, A.Kode, A.Nama, MAX(B.NilaiTotal) AS NilaiTotal
FROM            dbo.CalonKaryawan AS A INNER JOIN
                         dbo.NilaiScoring AS B ON A.Kode = B.KodeKaryawan INNER JOIN
                         dbo.Lowongan AS C ON B.KodeLowongan = C.Kode
GROUP BY C.Periode, A.Kode, A.Nama
ORDER BY NilaiTotal DESC
' 
GO
INSERT [dbo].[AdministrasiHakAkses] ([Kode], [Nama], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'82b62399-6a73-487c-8984-3224b2', N'Menu Conceptor', CAST(N'2018-10-08T06:53:09.720' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-10-08T06:53:09.720' AS DateTime), NULL)
INSERT [dbo].[AdministrasiHakAksesMenu] ([KodeHakAkses], [KodeMenu], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'82b62399-6a73-487c-8984-3224b2', N'4095422d-a0e5-4', CAST(N'2018-10-08T06:53:09.790' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-10-08T06:53:09.790' AS DateTime), NULL)
INSERT [dbo].[AdministrasiHakAksesMenu] ([KodeHakAkses], [KodeMenu], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'82b62399-6a73-487c-8984-3224b2', N'b7eed872-ea12-4', CAST(N'2018-10-08T06:53:09.807' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-10-08T06:53:09.807' AS DateTime), NULL)
INSERT [dbo].[AdministrasiHakAksesRole] ([KodeHakAkses], [KodeRole], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'82b62399-6a73-487c-8984-3224b2', N'Conceptor', CAST(N'2018-10-08T06:53:09.773' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-10-08T06:53:09.773' AS DateTime), NULL)
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'09ed2124-225b-4', N'Report', N'#', N'fa fa-lg fa-fw fa-file-text-o', N'fa fa-lg fa-fw fa-file-text-o', N'#', 10, 1, CAST(N'2017-08-12T01:18:09.877' AS DateTime), N'MHILMAN.FADLI', CAST(N'2019-12-14T15:47:31.413' AS DateTime), N'MHILMAN.FADLI')
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'151e5756-3cd6-4', N'Role', N'app.administrasi.role', NULL, NULL, N'15e75191-4326-4', 2, 1, CAST(N'2017-08-12T01:22:05.573' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'15e75191-4326-4', N'Administration', N'#', N'fa fa-lg fa-fw fa-gear', N'fa fa-lg fa-fw fa-gear', N'#', 11, 1, CAST(N'2017-08-12T01:19:59.300' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-12-15T08:11:32.597' AS DateTime), N'FAJRI.LUTHFI')
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'4095422d-a0e5-4', N'Data Calon Karyawan', N'app.masterdata.calonkaryawan', N'a fa-calendar-o', N'a fa-calendar-o', N'#', 3, 1, CAST(N'2018-09-27T11:08:30.933' AS DateTime), N'MHILMAN.FADLI', CAST(N'2019-12-14T20:51:22.387' AS DateTime), N'M.IDRUS')
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'40af4c71-aed5-4', N'GAP dan NCF & NSF', N'app.proses.nilaigap', N'fa fa-columns', N'fa fa-columns', N'c1fb5246-5a3a-4', 2, 1, CAST(N'2019-12-31T04:55:05.800' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T14:30:00.103' AS DateTime), N'M.IDRUS')
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'430efc4a-1001-4', N'Data Kriteria', N'app.masterdata.kriteria', N'fas fa-archive', N'fas fa-archive', N'#', 8, 1, CAST(N'2018-12-15T08:04:32.723' AS DateTime), N'FAJRI.LUTHFI', CAST(N'2019-12-27T21:24:25.823' AS DateTime), N'M.IDRUS')
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'60fa5861-1d6c-4', N'Dashboard', N'app.dashboard', N'dashboard', N'fa fa-lg fa-fw fa-dashboard', N'#', 0, 1, CAST(N'2017-08-12T00:39:40.987' AS DateTime), N'MHILMAN.FADLI', CAST(N'2017-11-23T08:43:24.393' AS DateTime), N'MHILMAN.FADLI')
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'7a80fb24-c7db-4', N'Role User', N'app.administrasi.roleuser', NULL, NULL, N'15e75191-4326-4', 3, 1, CAST(N'2017-08-12T01:23:03.153' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'7c70c83f-2f90-4', N'User', N'app.administrasi.user', NULL, NULL, N'15e75191-4326-4', 1, 1, CAST(N'2017-08-12T01:21:20.607' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'7d99dea2-905b-4', N'Menu', N'app.administrasi.menu', NULL, NULL, N'15e75191-4326-4', 4, 1, CAST(N'2017-08-12T01:23:24.423' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'863936bd-7d8e-4', N'Perbandingan Kriteria', N'app.proses.perbandingankriteria', N'fa fa-certificate', N'fa fa-certificate', N'c1fb5246-5a3a-4', 0, 1, CAST(N'2019-12-27T23:22:22.383' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T14:33:44.637' AS DateTime), N'M.IDRUS')
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'8c9c94d0-069e-4', N'Data Lowongan', N'app.masterdata.lowongan', N'fa fa-clipboard', N'fa fa-clipboard', N'#', 7, 1, CAST(N'2018-09-30T00:05:19.770' AS DateTime), N'MHILMAN.FADLI', CAST(N'2019-12-14T22:25:22.227' AS DateTime), N'M.IDRUS')
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'968de452-da89-4', N'Aksi', N'app.administrasi.tombol', NULL, NULL, N'15e75191-4326-4', 5, 1, CAST(N'2017-08-12T01:23:40.163' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'9d503235-b929-4', N'Hak Akses', N'app.administrasi.hakakses', NULL, NULL, N'15e75191-4326-4', 6, 1, CAST(N'2017-08-12T01:24:07.540' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'b99cf3be-fe4b-4', N'Hasil Akhir', N'app.proses.hasilakhir', N'fa fa-list-alt', N'fa fa-list-alt', N'c1fb5246-5a3a-4', 3, 1, CAST(N'2020-01-01T14:26:06.110' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T14:30:45.377' AS DateTime), N'M.IDRUS')
INSERT [dbo].[AdministrasiMenu] ([Kode], [Title], [Url], [Icon], [Class], [Parent], [Sequence], [IsActive], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'c1fb5246-5a3a-4', N'Proses Data', N'#', N'fa fa-gears', N'fa fa-gears', N'#', 9, 1, CAST(N'2017-08-12T00:41:13.807' AS DateTime), N'MHILMAN.FADLI', CAST(N'2020-01-01T14:34:31.207' AS DateTime), N'M.IDRUS')
INSERT [dbo].[AdministrasiRole] ([Kode], [Nama], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Administrator', N'Administrator', CAST(N'2017-08-11T08:25:53.593' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiRole] ([Kode], [Nama], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'AreaHululais', N'Area/Project Hululais', CAST(N'2017-08-11T08:26:36.317' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiRole] ([Kode], [Nama], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'BudgetHolder', N'Budget Holder', CAST(N'2018-12-01T22:49:25.847' AS DateTime), N'FAJRI.LUTHFI', CAST(N'2018-12-01T22:49:25.847' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRole] ([Kode], [Nama], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Conceptor', N'Conceptor', CAST(N'2018-10-08T06:22:51.090' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-10-08T06:22:51.090' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRole] ([Kode], [Nama], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Managment', N'Management', CAST(N'2018-08-30T16:05:23.157' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-08-30T16:05:23.157' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleDtl] ([KodeRole], [KodeWilayahKerja], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Administrator', N'HLS', CAST(N'2017-08-11T08:25:53.600' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[AdministrasiRoleDtl] ([KodeRole], [KodeWilayahKerja], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Administrator', N'KMJ', CAST(N'2017-08-11T08:25:53.613' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[AdministrasiRoleDtl] ([KodeRole], [KodeWilayahKerja], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Administrator', N'KRH', CAST(N'2017-08-11T08:25:53.613' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[AdministrasiRoleDtl] ([KodeRole], [KodeWilayahKerja], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Administrator', N'L001', CAST(N'2017-08-11T08:25:53.617' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[AdministrasiRoleDtl] ([KodeRole], [KodeWilayahKerja], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Administrator', N'LMB', CAST(N'2017-08-11T08:25:53.617' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[AdministrasiRoleDtl] ([KodeRole], [KodeWilayahKerja], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Administrator', N'SBK', CAST(N'2017-08-11T08:25:53.617' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[AdministrasiRoleDtl] ([KodeRole], [KodeWilayahKerja], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Administrator', N'SPN', CAST(N'2017-08-11T08:25:53.617' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[AdministrasiRoleDtl] ([KodeRole], [KodeWilayahKerja], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Administrator', N'UBL', CAST(N'2017-08-11T08:25:53.620' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[AdministrasiRoleDtl] ([KodeRole], [KodeWilayahKerja], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'AreaHululais', N'HLS', CAST(N'2017-08-11T08:26:36.320' AS DateTime), N'', NULL, NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Administrator', N'Rudiana', CAST(N'2017-08-11T08:28:14.043' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'AdminQM', N'AdminQM', CAST(N'2018-08-30T16:02:07.480' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-08-30T16:02:07.480' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'AreaHululais', N'user.hululais', CAST(N'2017-08-11T08:28:03.940' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Auditor', N'Auditor1', CAST(N'2018-09-03T08:58:32.840' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-09-03T08:58:32.840' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Auditor', N'LeadAuditor', CAST(N'2018-09-03T08:58:32.860' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-09-03T08:58:32.860' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Auditor2', N'Auditor2', CAST(N'2018-09-03T08:57:24.477' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-09-03T08:57:24.477' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'BudgetHolder', N'budget.holder', CAST(N'2018-12-01T22:50:31.283' AS DateTime), N'FAJRI.LUTHFI', CAST(N'2018-12-01T22:50:31.283' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Conceptor', N'conceptor', CAST(N'2018-12-01T22:49:48.990' AS DateTime), N'FAJRI.LUTHFI', CAST(N'2018-12-01T22:49:48.990' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Conceptor', N'Rudiana', CAST(N'2018-12-01T22:49:49.003' AS DateTime), N'FAJRI.LUTHFI', CAST(N'2018-12-01T22:49:49.003' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Juri', N'Juri1', CAST(N'2018-08-30T16:13:27.487' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-08-30T16:13:27.487' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Juri', N'Juri2', CAST(N'2018-08-30T16:13:27.490' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-08-30T16:13:27.490' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Juri', N'Juri3', CAST(N'2018-08-30T16:13:27.493' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-08-30T16:13:27.493' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Juri', N'Juri4', CAST(N'2018-08-30T16:13:27.497' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-08-30T16:13:27.497' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Managment', N'Managment', CAST(N'2018-08-30T16:07:27.830' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-08-30T16:07:27.830' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'2018-UPSTREAM-02-CIP-01', CAST(N'2018-09-10T10:43:25.730' AS DateTime), N'', CAST(N'2018-09-10T10:43:25.730' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'2018-UPSTREAM-02-CIP-02', CAST(N'2018-09-10T10:47:05.023' AS DateTime), N'', CAST(N'2018-09-10T10:47:05.023' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'2018-UPSTREAM-02-CIP-03', CAST(N'2018-09-13T10:47:28.260' AS DateTime), N'', CAST(N'2018-09-13T10:47:28.260' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'2018-UPSTREAM-02-CIP-04', CAST(N'2018-09-13T11:18:07.763' AS DateTime), N'', CAST(N'2018-09-13T11:18:07.763' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'2018-UPSTREAM-02-CIP-05', CAST(N'2018-09-13T12:17:46.587' AS DateTime), N'', CAST(N'2018-09-13T12:17:46.587' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'2018-UPSTREAM-02-CIP-06', CAST(N'2018-09-13T13:32:06.200' AS DateTime), N'', CAST(N'2018-09-13T13:32:06.200' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'2018-UPSTREAM-02-CIP-07', CAST(N'2018-09-13T14:20:28.413' AS DateTime), N'', CAST(N'2018-09-13T14:20:28.413' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'2018-UPSTREAM-02-CIP-08', CAST(N'2018-09-13T14:50:50.250' AS DateTime), N'', CAST(N'2018-09-13T14:50:50.250' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'2018-UPSTREAM-02-CIP-09', CAST(N'2018-09-13T15:16:53.823' AS DateTime), N'', CAST(N'2018-09-13T15:16:53.823' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'PGE-FT2018-001', CAST(N'2018-08-27T14:32:20.133' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-08-27T14:32:20.133' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'PGE-FT2018-0161', CAST(N'2018-08-28T14:11:08.390' AS DateTime), N'', CAST(N'2018-08-28T14:11:08.390' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'PGE-PC2018-001', CAST(N'2018-09-03T15:10:57.433' AS DateTime), N'', CAST(N'2018-09-03T15:10:57.433' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'PGE-PC2018-0162', CAST(N'2018-08-30T19:55:39.417' AS DateTime), N'', CAST(N'2018-08-30T19:55:39.417' AS DateTime), NULL)
INSERT [dbo].[AdministrasiRoleUser] ([KodeRole], [KodeUser], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'Peserta', N'PGE-PC2018-0163', CAST(N'2018-08-30T22:39:19.980' AS DateTime), N'', CAST(N'2018-08-30T22:39:19.980' AS DateTime), NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'00D7FE4A-17C7-', N' Hapus Negosiasi', N'fa fa-remove', N'fa fa-remove', N'03355854-94da-4', N'Negosiasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'08e0d5ef-81ff-4', N'Hapus Usulan', N'fa fa-remove', N'fa fa-remove', N'03355854-94da-4', N'Usulan', CAST(N'2018-05-27T16:37:19.230' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'0C31B8C8-2953-', N' Update Negosiasi', N'fa fa-edit', N'fa fa-edit', N'03355854-94da-4', N'Negosiasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'1A4A85D3-A208-', N' Update Verifikasi', N'fa fa-edit', N'fa fa-edit', N'03355854-94da-4', N'Verifikasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'23F14F1A-D9C0-', N' Hapus Inventasisasi Lahan', N'fa fa-remove', N'fa fa-remove', N'03355854-94da-4', N'Inventasisasi Lahan', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'24260409-D05E-', N' Daftar Inventasisasi Lahan', N'fa fa-table', N'fa fa-table', N'03355854-94da-4', N'Inventasisasi Lahan', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'26F6890A-4298-', N' Hapus Persiapan', N'fa fa-remove', N'fa fa-remove', N'03355854-94da-4', N'Persiapan', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'2c1faaaf-83bb-4', N'Tambah', N'fa fa-plus', N'fa fa-plus', NULL, NULL, CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-05-27T16:39:35.383' AS DateTime), N'MHILMAN.FADLI')
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'372E3D2E-7EA0-', N' Update Sertifikasi', N'fa fa-edit', N'fa fa-edit', N'03355854-94da-4', N'Sertifikasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'3D0C61C8-1563-', N' Hapus Verifikasi', N'fa fa-remove', N'fa fa-remove', N'03355854-94da-4', N'Verifikasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'4158801A-7677-', N' Hapus Sosialisasi', N'fa fa-remove', N'fa fa-remove', N'03355854-94da-4', N'Sosialisasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'41F65F9C-B304-', N' Update Persiapan', N'fa fa-edit', N'fa fa-edit', N'03355854-94da-4', N'Persiapan', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'42B64B0A-3307-', N' Tambah Inventasisasi Lahan', N'fa fa-plus', N'fa fa-plus', N'03355854-94da-4', N'Inventasisasi Lahan', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI')
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'4AF71C3B-ED53-', N' Hapus Pembayaran', N'fa fa-remove', N'fa fa-remove', N'03355854-94da-4', N'Pembayaran', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'5C6A9FF6-2291-', N' Hapus Pelaporan', N'fa fa-remove', N'fa fa-remove', N'03355854-94da-4', N'Pelaporan', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'5E4A370D-FFCE-', N' Tambah Sosialisasi', N'fa fa-plus', N'fa fa-plus', N'03355854-94da-4', N'Sosialisasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI')
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'5F293B7A-F9D0-', N' Update Sosialisasi', N'fa fa-edit', N'fa fa-edit', N'03355854-94da-4', N'Sosialisasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'6A3F190C-7264-', N' Tambah Persiapan', N'fa fa-plus', N'fa fa-plus', N'03355854-94da-4', N'Persiapan', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI')
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'6C64D364-D4F0-', N' Daftar Pelaporan', N'fa fa-table', N'fa fa-table', N'03355854-94da-4', N'Pelaporan', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'7373D01C-0174-', N' Tambah Pembayaran', N'fa fa-plus', N'fa fa-plus', N'03355854-94da-4', N'Pembayaran', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI')
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'828DBD1A-97E6-', N' Tambah Verifikasi', N'fa fa-plus', N'fa fa-plus', N'03355854-94da-4', N'Verifikasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI')
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'8DEAD4D5-B6F3-', N' Daftar Sertifikasi', N'fa fa-table', N'fa fa-table', N'03355854-94da-4', N'Sertifikasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'91C384C4-5108-', N' Tambah Negosiasi', N'fa fa-plus', N'fa fa-plus', N'03355854-94da-4', N'Negosiasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI')
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'9980332E-D98B-', N' Daftar Verifikasi', N'fa fa-table', N'fa fa-table', N'03355854-94da-4', N'Verifikasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'9BEF6982-DB0F-', N' Update Pelaporan', N'fa fa-edit', N'fa fa-edit', N'03355854-94da-4', N'Pelaporan', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'9cc27c3d-bba0-4', N'Update', N'fa fa-edit', N'fa fa-edit', NULL, NULL, CAST(N'2018-05-27T16:38:54.830' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'a24ec2c4-97fb-4', N'Update Usulan', N'fa fa-edit', N'fa fa-edit', N'03355854-94da-4', N'Usulan', CAST(N'2018-05-27T16:33:02.443' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'A2D01CDD-460F-', N' Hapus Sertifikasi', N'fa fa-remove', N'fa fa-remove', N'03355854-94da-4', N'Sertifikasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'A4BCA6B7-15DF-', N' Tambah Sertifikasi', N'fa fa-plus', N'fa fa-plus', N'03355854-94da-4', N'Sertifikasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI')
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'ab69743a-4f74-4', N'Daftar', N'fa fa-table', N'fa fa-table', NULL, NULL, CAST(N'2018-05-27T16:39:22.037' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'C1D00439-A0A5-', N' Daftar Negosiasi', N'fa fa-table', N'fa fa-table', N'03355854-94da-4', N'Negosiasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'c44da812-3323-4', N'Tambah Usulan', N'fa fa-plus', N'fa fa-plus', N'03355854-94da-4', N'Usulan', CAST(N'2018-05-27T16:18:08.833' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-05-27T16:31:31.193' AS DateTime), N'MHILMAN.FADLI')
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'C6A92D00-A9A5-', N' Tambah Pelaporan', N'fa fa-plus', N'fa fa-plus', N'03355854-94da-4', N'Pelaporan', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI')
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'd07e4c8b-90c4-4', N'Daftar Usulan', N'fa fa-table', N'fa fa-table', N'03355854-94da-4', N'Usulan', CAST(N'2018-05-27T16:34:52.223' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'DB1B9229-B480-', N' Daftar Pembayaran', N'fa fa-table', N'fa fa-table', N'03355854-94da-4', N'Pembayaran', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'E714CF13-257E-', N' Daftar Sosialisasi', N'fa fa-table', N'fa fa-table', N'03355854-94da-4', N'Sosialisasi', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'EA74983C-2B8C-', N' Daftar Persiapan', N'fa fa-table', N'fa fa-table', N'03355854-94da-4', N'Persiapan', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'F369F591-2E52-', N' Update Pembayaran', N'fa fa-edit', N'fa fa-edit', N'03355854-94da-4', N'Pembayaran', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'F72173D3-52EF-', N' Update Inventasisasi Lahan', N'fa fa-edit', N'fa fa-edit', N'03355854-94da-4', N'Inventasisasi Lahan', CAST(N'2018-05-27T16:38:34.947' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiTombol] ([Kode], [Title], [Icon], [Class], [KodeMenu], [Parameter], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'fa90fa20-8316-4', N'Hapus', N'fa fa-remove', N'fa fa-remove', NULL, NULL, CAST(N'2018-05-27T16:40:03.283' AS DateTime), N'MHILMAN.FADLI', NULL, NULL)
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'budget.holder', N'budget.holder', NULL, NULL, N'huriezu21@gmail.com', NULL, 1, 1, NULL, CAST(N'2018-12-01T22:48:24.937' AS DateTime), N'FAJRI.LUTHFI', CAST(N'2018-12-01T22:55:25.930' AS DateTime), N'MHILMAN.FADLI', N'')
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'budyi.permono', N'Budyi Permono', N'Jakarta', N'1599', N'budyi.permono@pertamina.com', NULL, 1, 1, N'CE01', CAST(N'2018-02-01T10:30:15.990' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-02-01T10:39:18.880' AS DateTime), N'MHILMAN.FADLI', NULL)
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'conceptor', N'conceptor', NULL, NULL, N'huriely@hotmail.com', NULL, 1, 1, NULL, CAST(N'2018-12-01T22:47:43.740' AS DateTime), N'FAJRI.LUTHFI', CAST(N'2018-12-01T22:55:33.067' AS DateTime), N'MHILMAN.FADLI', N'')
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'evaliana.akay', N'Evaliana Akay', NULL, NULL, N'evaliana.akay@pertamina.com', NULL, 1, 1, N'CE01', CAST(N'2017-08-24T10:03:23.630' AS DateTime), N'BAGUS.WIBISONO', NULL, NULL, NULL)
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'fajri.luthfi', N'fajri luthfi', N'Karaha', NULL, N'fajri.luthfi@pertamina.com', NULL, 1, 1, N'CE01', CAST(N'2017-08-24T09:55:21.983' AS DateTime), N'MHILMAN.FADLI', NULL, NULL, N'CGTonctfclgywMVkywJ7og==')
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'israyudi', N'Israyudi', NULL, NULL, N'israyudi@pertamina.com', NULL, 1, 1, N'CE01', CAST(N'2017-09-19T07:53:48.353' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-08-18T20:11:52.857' AS DateTime), N'MHILMAN.FADLI', N'')
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'Joseph.aryanto', N'Joseph Rich Aryanto', N'Jkt', NULL, N'joseph.aryanto@pertamina.com', NULL, 0, 0, N'CE01', CAST(N'2017-10-16T16:54:30.387' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-02-05T15:12:42.017' AS DateTime), N'BUDYI.PERMONO', NULL)
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'lamp.lhd', N'LAMP LAHENDONG', N'Lahendong', N'021', N'lamp.lhd@pertamina.com', NULL, 1, 1, N'CE01', CAST(N'2018-01-22T07:47:45.480' AS DateTime), N'MHILMAN.FADLI', NULL, NULL, NULL)
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'lufan.faswara', N'Lufan', N'Lahendong', NULL, N'lufan.faswara@pertamina.com', NULL, 1, 1, N'CE01', CAST(N'2017-10-04T12:17:36.353' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-08-19T01:12:24.797' AS DateTime), N'MHILMAN.FADLI', N'')
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'M.IDRUS', N'Muhammad Idrus', N'Jakarta', N'081211112222', N'huriezu21@gmail.com', NULL, 1, 1, N'CE01', CAST(N'2017-07-24T00:00:00.000' AS DateTime), N'MHILMAN.FADLI', CAST(N'2017-08-22T06:50:35.217' AS DateTime), N'MHILMAN.FADLI', NULL)
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'mk.myudistiro', N'Muhammad Rifky Yudistiro', N'Jakarta', N'1542', N'mk.myudistiro@mitrakerja.pertamina.com', NULL, 1, 1, N'CE01', CAST(N'2018-02-01T10:31:38.430' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-02-01T10:32:13.073' AS DateTime), N'MHILMAN.FADLI', NULL)
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'okta.risman', N'Okta Risman', N'Jakarta', NULL, N'okta.risman@pertamina.com', NULL, 1, 1, N'CE01', CAST(N'2017-08-24T10:01:14.893' AS DateTime), N'MHILMAN.FADLI', NULL, NULL, NULL)
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'reinhart', N'Reinhart', NULL, NULL, N'reinhartnelwan@gmail.com', NULL, 1, 1, N'CE01', CAST(N'2017-08-24T10:00:01.703' AS DateTime), N'BAGUS.WIBISONO', CAST(N'2017-08-24T10:00:24.127' AS DateTime), N'BAGUS.WIBISONO', NULL)
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'rolly.masengi', N'Petra', N'Lahendong', N'0765', N'mk.rolly.masengi@mitrakerja.pertamina.com', NULL, 1, 1, N'CE01', CAST(N'2017-08-24T09:59:07.780' AS DateTime), N'BAGUS.WIBISONO', NULL, NULL, NULL)
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'Rudiana', N'Rudiana', N'Bekasi', N'087882692792', N'rudiana8388@gmail.com', NULL, 0, 0, N'CE01', CAST(N'2017-08-11T08:25:38.747' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-10-08T06:22:16.800' AS DateTime), N'MHILMAN.FADLI', N'')
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'UserA', N'User A', NULL, NULL, NULL, NULL, NULL, 1, N'CE01', CAST(N'2018-10-05T15:03:05.220' AS DateTime), N'MHILMAN.FADLI', CAST(N'2018-10-05T15:03:05.220' AS DateTime), NULL, N'CGTonctfclgywMVkywJ7og==')
INSERT [dbo].[AdministrasiUser] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Avatar], [IsAdministrator], [Aktif], [KodePlant], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy], [Password]) VALUES (N'wuri.handarini', N'Wuri', NULL, NULL, N'wuri.handarini@pertamina.com', NULL, 1, 1, N'CE01', CAST(N'2017-08-24T10:03:52.757' AS DateTime), N'BAGUS.WIBISONO', NULL, NULL, NULL)
INSERT [dbo].[CalonKaryawan] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Kelamin], [Tgllahir], [Pendidikan], [NilaiAkhir], [Keterangan], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'191231227', N'Indah Pertiwi', N'Jakarta', N'021-9232323', N'fini@gmail.com', N'Perempuan', CAST(N'2010-01-04' AS Date), N'S1', NULL, NULL, CAST(N'2019-12-31T13:48:10.227' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T10:10:48.653' AS DateTime), N'M.IDRUS')
INSERT [dbo].[CalonKaryawan] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Kelamin], [Tgllahir], [Pendidikan], [NilaiAkhir], [Keterangan], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'191231465', N'Dina Aulia', N'Jakarta', N'021-93434343', N'idrus@gmail.com', N'Perempuan', CAST(N'2010-01-12' AS Date), N'S2', NULL, NULL, CAST(N'2019-12-31T13:46:11.470' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T10:18:31.413' AS DateTime), N'M.IDRUS')
INSERT [dbo].[CalonKaryawan] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Kelamin], [Tgllahir], [Pendidikan], [NilaiAkhir], [Keterangan], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'200101227', N'Rika Mubarokah', N'Jakarta', N'021-3434343', N'rika@gmail.com', N'Perempuan', CAST(N'2010-01-05' AS Date), N'S3', NULL, NULL, CAST(N'2020-01-01T10:19:17.227' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T10:19:17.227' AS DateTime), NULL)
INSERT [dbo].[CalonKaryawan] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Kelamin], [Tgllahir], [Pendidikan], [NilaiAkhir], [Keterangan], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'20010135', N'Dinda Amelia', N'Jakarta', N'021-78743434', N'dinda@gmail.com', N'Perempuan', CAST(N'2011-01-04' AS Date), N'S1', NULL, NULL, CAST(N'2020-01-01T10:18:15.357' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T10:18:15.357' AS DateTime), NULL)
INSERT [dbo].[CalonKaryawan] ([Kode], [Nama], [Alamat], [Telepon], [Email], [Kelamin], [Tgllahir], [Pendidikan], [NilaiAkhir], [Keterangan], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'200101873', N'Nurul Fatimah', N'Jakarta', N'021-82323232', N'nurul@gmail.com', N'Perempuan', CAST(N'2010-01-05' AS Date), N'DIPLOMA', NULL, NULL, CAST(N'2020-01-01T10:19:59.873' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T10:19:59.873' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Kriteria] ON 

INSERT [dbo].[Kriteria] ([Kode], [Nama], [Faktor], [NilaiTarget], [JumlahBaris], [EigenvectorScore], [NilaiKali], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, N'Nilai Test Teknis ( NTT )', 1, 5, NULL, 0.4424, NULL, CAST(N'2019-12-30T15:58:16.570' AS DateTime), N'M.IDRUS', CAST(N'2019-12-30T15:58:46.813' AS DateTime), N'M.IDRUS')
INSERT [dbo].[Kriteria] ([Kode], [Nama], [Faktor], [NilaiTarget], [JumlahBaris], [EigenvectorScore], [NilaiKali], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, N'Pengalaman Kerja (PK)', 1, 4, NULL, 0.2152, NULL, CAST(N'2019-12-30T15:59:11.887' AS DateTime), N'M.IDRUS', CAST(N'2019-12-30T15:59:11.887' AS DateTime), NULL)
INSERT [dbo].[Kriteria] ([Kode], [Nama], [Faktor], [NilaiTarget], [JumlahBaris], [EigenvectorScore], [NilaiKali], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, N'Nilai Hasil Wawancara (NHW)', 1, 4, NULL, 0.1957, NULL, CAST(N'2019-12-30T15:59:32.187' AS DateTime), N'M.IDRUS', CAST(N'2019-12-30T15:59:32.187' AS DateTime), NULL)
INSERT [dbo].[Kriteria] ([Kode], [Nama], [Faktor], [NilaiTarget], [JumlahBaris], [EigenvectorScore], [NilaiKali], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (4, N'Nilai Psikotes (NP)', 2, 2, NULL, 0.0807, NULL, CAST(N'2019-12-30T15:59:51.017' AS DateTime), N'M.IDRUS', CAST(N'2019-12-30T15:59:51.017' AS DateTime), NULL)
INSERT [dbo].[Kriteria] ([Kode], [Nama], [Faktor], [NilaiTarget], [JumlahBaris], [EigenvectorScore], [NilaiKali], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (5, N'Nilai Tes Bahasa Inggris (NTB)', 2, 3, NULL, 0.0661, NULL, CAST(N'2019-12-30T16:00:20.387' AS DateTime), N'M.IDRUS', CAST(N'2019-12-30T16:00:20.387' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Kriteria] OFF
INSERT [dbo].[Lowongan] ([Kode], [Nama], [Jumlah], [TglMulai], [TglAkhir], [Periode], [NoSurat], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'19123102257', N'Scrum Master', 2, CAST(N'2019-12-31' AS Date), CAST(N'2020-01-31' AS Date), N'Desember 2019', N'01', CAST(N'2019-12-31T14:12:31.267' AS DateTime), N'M.IDRUS', CAST(N'2019-12-31T14:12:31.267' AS DateTime), NULL)
INSERT [dbo].[Lowongan] ([Kode], [Nama], [Jumlah], [TglMulai], [TglAkhir], [Periode], [NoSurat], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (N'19123102856', N'Middle Developer', 3, CAST(N'2019-12-01' AS Date), CAST(N'2020-01-01' AS Date), N'Desember 2019', N'02', CAST(N'2019-12-31T14:13:04.857' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T15:18:43.027' AS DateTime), N'M.IDRUS')
SET IDENTITY_INSERT [dbo].[NilaiScoring] ON 

INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, N'191231465', N'19123102257', 1, 0, 5, 5, NULL, 0.98053, 1.38955, 0.367, CAST(N'2019-12-31T23:45:54.853' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T00:58:15.227' AS DateTime), N'M.IDRUS')
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, N'191231465', N'19123102257', 2, 0, 5, 4, NULL, 0.98053, 1.38955, 0.367, CAST(N'2019-12-31T23:46:11.603' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T00:58:15.247' AS DateTime), N'M.IDRUS')
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, N'191231465', N'19123102257', 3, 1, 4.5, 5, NULL, 0.98053, 1.38955, 0.367, CAST(N'2019-12-31T23:46:25.483' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T00:58:15.260' AS DateTime), N'M.IDRUS')
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (4, N'191231465', N'19123102257', 4, 0, 5, 2, NULL, 0.98053, 1.38955, 0.367, CAST(N'2019-12-31T23:46:25.483' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T00:58:15.300' AS DateTime), N'M.IDRUS')
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (5, N'191231465', N'19123102257', 5, 0, 5, 3, NULL, 0.98053, 1.38955, 0.367, CAST(N'2019-12-31T23:46:25.483' AS DateTime), N'M.IDRUS', CAST(N'2020-01-01T00:58:15.303' AS DateTime), N'M.IDRUS')
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (6, N'191231227', N'19123102856', 1, -2, 3, 3, NULL, 0.71844, 1.0261333333333333, 0.2569, CAST(N'2020-01-01T00:59:43.057' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (7, N'191231227', N'19123102856', 2, 1, 4.5, 5, NULL, 0.71844, 1.0261333333333333, 0.2569, CAST(N'2020-01-01T00:59:43.057' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (8, N'191231227', N'19123102856', 3, -1, 4, 3, NULL, 0.71844, 1.0261333333333333, 0.2569, CAST(N'2020-01-01T00:59:43.057' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (9, N'191231227', N'19123102856', 4, 2, 3.5, 4, NULL, 0.71844, 1.0261333333333333, 0.2569, CAST(N'2020-01-01T00:59:43.057' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (10, N'191231227', N'19123102856', 5, 2, 3.5, 5, NULL, 0.71844, 1.0261333333333333, 0.2569, CAST(N'2020-01-01T00:59:43.057' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (11, N'20010135', N'19123102257', 1, -3, 2, 2, NULL, 0.61797, 0.8428, 0.280725, CAST(N'2020-01-01T10:24:42.460' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (12, N'20010135', N'19123102257', 2, -1, 4, 3, NULL, 0.61797, 0.8428, 0.280725, CAST(N'2020-01-01T10:24:42.460' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (13, N'20010135', N'19123102257', 3, -1, 4, 3, NULL, 0.61797, 0.8428, 0.280725, CAST(N'2020-01-01T10:24:42.460' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (14, N'20010135', N'19123102257', 4, 1, 4.5, 3, NULL, 0.61797, 0.8428, 0.280725, CAST(N'2020-01-01T10:24:42.460' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (15, N'20010135', N'19123102257', 5, -2, 3, 1, NULL, 0.61797, 0.8428, 0.280725, CAST(N'2020-01-01T10:24:42.463' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (21, N'200101873', N'19123102856', 1, -2, 3, 3, NULL, 0.72091999999999989, 0.98376666666666657, 0.32665, CAST(N'2020-01-01T10:27:37.167' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (22, N'200101873', N'19123102856', 2, -2, 3, 2, NULL, 0.72091999999999989, 0.98376666666666657, 0.32665, CAST(N'2020-01-01T10:27:37.167' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (23, N'200101873', N'19123102856', 3, 0, 5, 4, NULL, 0.72091999999999989, 0.98376666666666657, 0.32665, CAST(N'2020-01-01T10:27:37.167' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (24, N'200101873', N'19123102856', 4, -1, 4, 1, NULL, 0.72091999999999989, 0.98376666666666657, 0.32665, CAST(N'2020-01-01T10:27:37.167' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (25, N'200101873', N'19123102856', 5, 0, 5, 3, NULL, 0.72091999999999989, 0.98376666666666657, 0.32665, CAST(N'2020-01-01T10:27:37.167' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (26, N'200101227', N'19123102856', 1, -1, 4, 4, NULL, 0.81622, 1.1377333333333333, 0.33394999999999997, CAST(N'2020-01-01T15:22:03.000' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (27, N'200101227', N'19123102856', 2, -1, 4, 3, NULL, 0.81622, 1.1377333333333333, 0.33394999999999997, CAST(N'2020-01-01T15:22:03.000' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (28, N'200101227', N'19123102856', 3, -1, 4, 3, NULL, 0.81622, 1.1377333333333333, 0.33394999999999997, CAST(N'2020-01-01T15:22:03.000' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (29, N'200101227', N'19123102856', 4, 0, 5, 2, NULL, 0.81622, 1.1377333333333333, 0.33394999999999997, CAST(N'2020-01-01T15:22:03.000' AS DateTime), N'M.IDRUS', NULL, NULL)
INSERT [dbo].[NilaiScoring] ([Kode], [KodeKaryawan], [KodeLowongan], [KodeKriteria], [NilaiGap], [NilaiBobot], [NilaiAlternatif], [RangkingScoring], [NilaiTotal], [Ncf], [Nsf], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (30, N'200101227', N'19123102856', 5, -1, 4, 2, NULL, 0.81622, 1.1377333333333333, 0.33394999999999997, CAST(N'2020-01-01T15:22:03.000' AS DateTime), N'M.IDRUS', NULL, NULL)
SET IDENTITY_INSERT [dbo].[NilaiScoring] OFF
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, 1, 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, 2, 4, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, 3, 5, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, 4, 7, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (1, 5, 6, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, 1, 1, 4, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, 2, 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, 3, 1, 4, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, 4, 8, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (2, 5, 4, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, 1, 1, 5, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, 2, 4, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, 3, 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, 4, 2, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (3, 5, 1, 2, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (4, 1, 1, 7, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (4, 2, 1, 8, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (4, 3, 1, 2, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (4, 4, 1, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (4, 5, 5, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (5, 1, 1, 6, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (5, 2, 1, 4, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (5, 3, 2, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (5, 4, 1, 5, NULL, NULL, NULL, NULL)
INSERT [dbo].[PerbandinganKriteria] ([Kode], [Kode2], [Nilai], [Nilai2], [CreatedDate], [CreatedBy], [UpdatedDate], [UpdatedBy]) VALUES (5, 5, 1, 1, NULL, NULL, NULL, NULL)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF_AdministrasiUser_KodePlant]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[AdministrasiUser] ADD  CONSTRAINT [DF_AdministrasiUser_KodePlant]  DEFAULT ('') FOR [KodePlant]
END
GO
/****** Object:  StoredProcedure [dbo].[SpPerbandinganKriteria]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpPerbandinganKriteria]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SpPerbandinganKriteria] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SpPerbandinganKriteria]	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare @TablePerbandingan table(
		Kode1 int,
		Kriteria1 varchar(50), 
		Kode2 int,
		Kriteria2 varchar(50),
		nilai1 float,nilai2 float)
	declare @CountKriteria1 int,@CountKriteria2 int,@idx1 int,@idx2 int
	declare @Kode1 int,@Kode2 int,@Kriteria1 varchar(50),@Kriteria2 varchar(50)

	set  @CountKriteria1=(select count(Kode) from Kriteria)
	set  @CountKriteria2=@CountKriteria1
	set @idx1=1

	while (@idx1<=@CountKriteria1)
	begin
		select @Kode1= A.Kode,@Kriteria1=A.Nama from 
			(select ROW_NUMBER() over(order by Kode) as Number,* from Kriteria) A
			where Number=@idx1
			set @idx2=1
			while(@idx2<=@CountKriteria2)
			begin
			  select @Kode2= B.Kode,@Kriteria2=B.Nama from 
					(select ROW_NUMBER() over(order by Kode) as Number,* from Kriteria) B
					where Number=@idx2
					insert into @TablePerbandingan values(@Kode1,@Kriteria1,@Kode2,@Kriteria2,null,null)  
			  
			  SET @idx2 = @idx2 + 1;
			end
		SET @idx1 = @idx1 + 1;
	end

	select C.Kode1,C.Kriteria1,C.Kode2,C.Kriteria2,isnull(D.Nilai,0) Nilai1,isnull(D.Nilai2,0) nilai2  from @TablePerbandingan C 
	left join PerbandinganKriteria D on C.Kode1=D.Kode and C.Kode2=D.Kode2
	
END
GO
/****** Object:  StoredProcedure [dbo].[SpProseGap]    Script Date: 01/01/2020 15:30:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpProseGap]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SpProseGap] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
--exec SpProseGap 'Middle Developer','Desember 2019'
ALTER PROCEDURE [dbo].[SpProseGap]
	-- Add the parameters for the stored procedure here
	@posisi varchar(20), 
	@periode varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
select A.Kode,A.KodeKaryawan,B.Nama,A.KodeLowongan,
A.KodeKriteria,C.Nama Kriteria,A.NilaiGap,A.NilaiBobot
,A.NilaiAlternatif
,A.Ncf
,A.Nsf
,A.RangkingScoring
,A.NilaiTotal
,D.Periode
 from NilaiScoring A
inner Join CalonKaryawan B on A.KodeKaryawan=B.Kode
inner join Kriteria C on A.KodeKriteria=C.Kode
inner join Lowongan D on A.KodeLowongan=D.Kode
where lower(A.KodeLowongan) like lower(isnull(@posisi,'%')) and lower(D.Periode) like lower(isnull(@periode,'%'))
END


GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'VwRankingPosisi', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 6
               Left = 262
               Bottom = 136
               Right = 457
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "C"
            Begin Extent = 
               Top = 6
               Left = 495
               Bottom = 136
               Right = 681
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VwRankingPosisi'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'VwRankingPosisi', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VwRankingPosisi'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'VwRankingScore', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "B"
            Begin Extent = 
               Top = 6
               Left = 262
               Bottom = 136
               Right = 457
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "C"
            Begin Extent = 
               Top = 6
               Left = 495
               Bottom = 136
               Right = 681
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VwRankingScore'
GO
IF NOT EXISTS (SELECT * FROM sys.fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'VwRankingScore', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VwRankingScore'
GO
