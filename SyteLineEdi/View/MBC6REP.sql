alter view MBC6REP
As
Select 
co_num As C6CVNB,
cust_num As C6CANB,
B9CD As C6B9CD,
contact As C6F1CD,
whse As C6A3CD,
ship_code As C6CDTX,
cust_po As CJCBTX,
stat as C6FNST
from co c
Left Join Plantprm p
On c.cust_num=p.canb
and c.cust_seq=p.cust_seq

