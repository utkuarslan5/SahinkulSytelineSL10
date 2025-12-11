alter view MBBFREP
As
Select cust_num as CUSNO, name As CUSNM
From custaddr
where cust_seq=0