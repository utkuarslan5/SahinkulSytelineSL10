CREATE VIEW [dbo].[IRSLYMST] AS
SELECT S.co_num As DHCVNB , S.reason_text As DHZ969,
O.cust_num  As DHCANB, S.ship_date As DHIVNB,
P.B9CD As DHB9CD,  A1.name As DHBYTX, S.createdby As DHAFVN, 
CI.u_m As  DHCQCD, O.contact As DHF1CD,   
O.ship_code As DHCDTX, CI.item As DDAITX, cast(S.qty_shipped As numeric(18,2)) As DDARQT, 
I.description As DDALTX, S.co_line As  DDFCNB, cast(S.Price As numeric(18,6)) As  DDAAGP,
CI.u_m As DDDHCD, S.unit_weight As DDAAGK, A2.name As CUSNM, 
CI.whse As DHA3CD , S.co_release, S.date_seq
FROM co_ship S 
LEFT OUTER JOIN co O ON O.co_num=S.co_num
LEFT OUTER JOIN custaddr A1 ON  O.cust_num=A1.cust_num And O.cust_seq=A1.cust_seq
LEFT OUTER JOIN PLANTPRM P On O.cust_num=P.canb And O.cust_seq=P.cust_seq
LEFT OUTER JOIN coitem CI On S.co_num=CI.co_num And S.co_line=CI.co_line
LEFT OUTER JOIN Item I On CI.Item=I.Item
LEFT OUTER JOIN custaddr A2 ON  O.cust_num=A2.cust_num And A2.cust_seq=0
