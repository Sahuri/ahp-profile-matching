declare @TablePerbandingan table(Kode1 varchar(32),Kriteria1 varchar(50), Kode2 varchar(32),Kriteria2 varchar(50), nilai1 float,nilai2 float)
declare @CountKriteria1 int,@CountKriteria2 int,@idx1 int,@idx2 int
declare @Kode1 varchar(32),@Kode2 varchar(32),@Kriteria1 varchar(50),@Kriteria2 varchar(50)

set  @CountKriteria1=(select count(Kode) from Kriteria)
set  @CountKriteria2=@CountKriteria1
set @idx1=1
set @idx2=1

while (@idx<=@CountKriteria1)
begin
   select @Kode1= A.Kode,@Kriteria1=A.Nama from 
			(select ROW_NUMBER() over(order by Kode) as Number,* from Kriteria) A

			where Number=@idx
   SET @idx = @idx + 1;
end


select * from @TablePerbandingan