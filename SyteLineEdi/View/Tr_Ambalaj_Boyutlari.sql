Create View Tr_Ambalaj_Boyutlari
As
select	itmpack.AMBKOD,
		itmpack.Itnbr,
		isnull(I1.unit_weight,0) *  isnull(I1.Uf_BrmGn,0) * 
		isnull(I1.Uf_BrmUz,0) * isnull(I1.Uf_BrmYk,0) * 
		isnull(I1.Uf_BrmHc,0) * 
		Case When I2.Item is not null  
		Then isnull(I1.unit_weight,0) *  isnull(I1.Uf_BrmGn,0) * 
		isnull(I1.Uf_BrmUz,0) * isnull(I1.Uf_BrmYk,0) * 
		isnull(I1.Uf_BrmHc,0)
		Else 1
		End As Boyut
from itmpack
Left Join Item I1 On I1.Item=itmpack.KKOD
Left Join Item I2 On I2.Item=itmpack.PKOD