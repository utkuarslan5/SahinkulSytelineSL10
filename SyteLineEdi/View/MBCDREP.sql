create view MBCDREP
As

Select	coitem.co_num As CDCVNB,
		coitem.co_line As CDFCNB,
		co.charfld1 As CDAFYV
FRom coitem
Left Join co
	On coitem.co_num=co.co_num