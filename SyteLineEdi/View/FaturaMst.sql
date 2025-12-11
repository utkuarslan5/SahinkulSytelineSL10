
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER view [dbo].[FaturaMst]
As
SELECT Distinct
            S.reason_text As SevkNo, S.co_num, S.co_line, S.co_release,
            S.date_seq, O.cust_num, O.cust_seq, S.ship_date, A.name As [Name],
            A.Country, A.Addr##1, A.Addr##2, A.Addr##3, A.Addr##4, A.curr_code,
            A.Zip, A.City, A1.Name As shiptoname, A1.Addr##1 as AddrShip##1,
            A1.Addr##2 as AddrShip##2, A1.Addr##3 as AddrShip##3,
            A1.Addr##4 as AddrShip##4, D.Itnbr As Item, I.Description, I.u_m,
            S.qty_shipped, D.PRICE As DtyPrice, D.Ranno, P.Shipto, P.KAPI,
            P.Pusno, P.IRSNO, P.AMBKOD, P.KKOD, P.KSAY, P.Plaka, P.HKSAY,
            P.PKOD, P.PSAY, P.NETAGR, P.BRTAGR, P.HACIM, P.KAP_EN, P.KAP_BOY,
            P.SPKOD, P.SPMIK, P.KPKOD, P.KPMIK, P.KAP_YUK, P.USERF3 As SeferNo,
            P.USERF4 As NavlunNo, P.Whse, P.MURNKOD, P.MURNTNM,
            ( Select Top 1
                        cont_price
              From      ItemCustPrice_Fatura2 F
              Where     O.cust_num = F.cust_num
                        And D.Itnbr = F.Item
                        and F.cust_item = P.MURNKOD
                        and F.effect_date <= S.ship_date
              Order By  F.effect_date desc
            ) As cont_price,
--F.cont_price,
--F.effect_date, 
--C.Uf_IhracSekli As terms_code
            C.terms_code As terms_code, tax_reg_num1,
            O.tax_code1 As CustTax_Code, CI.tax_code1 As ItemTax_Code,
            S.Createdby As Kullanici, P.PICKNO, K.KNTRT, O.ship_code,
            IP.MAMBKOD, IP.MKKOD, IP.MPKOD, PP.DUNSID, TX.tax_rate,
            TC.description as TERMNM, IKT.AYNTIP as KOLIAYN,
            IPL.AYNTIP as PALETAYN, IKP.AYNTIP as KAPAKAYN,
            ( P.KSAY * PKT.u_ws_price ) + ( P.PSAY * PPL.u_ws_price )
            + ( P.KPMIK * PKP.u_ws_price ) + ( P.SPMIK * PSP.u_ws_price ) As KAPTUT
    FROM    tr_co_ship S
    LEFT OUTER JOIN co O
            ON O.co_num = S.co_num
    LEFT OUTER JOIN custaddr A
            ON O.cust_num = A.cust_num
               And A.cust_seq = 0
    LEFT OUTER JOIN custaddr A1
            ON O.cust_num = A1.cust_num
               And A1.cust_seq = O.cust_seq
    LEFT OUTER JOIN shpdty D
            ON S.reason_text = D.Shpno
               And S.co_num = D.Ordno
               And S.co_line = D.SEQNO
    LEFT OUTER JOIN shppack P
            ON S.reason_text = P.Shpno
               And d.Itnbr = P.Itnbr
               And D.PICKNO = P.PICKNO
               And P.Ambkod = D.Ambkod
               and P.Pusno = Case When substring(O.cust_po, 1, 2) not in (
                                       'EO', 'FO', '', ' ' ) Then O.cust_po
                                  Else P.Pusno
                             End

--Left Outer Join ItemCustPrice_Fatura2 F
--		On O.cust_num=F.cust_num And D.Itnbr=F.Item
--		and F.cust_item = P.MURNKOD
    LEFT OUTER JOIN Customer C
            ON O.cust_num = C.cust_num
               And C.cust_seq = 0
    LEFT OUTER JOIN dbo.coitem CI
            ON CI.co_num = S.co_num
               And CI.co_line = s.co_line
    LEFT OUTER JOIN Item I
            ON D.Itnbr = I.Item
    LEFT OUTER JOIN Kontrtpf K
            ON D.Itnbr = K.BZMITM
               And O.cust_num = K.CUST
               And P.Shipto = K.SHIPTO
               And P.KAPI = K.KAPI
    LEFT OUTER JOIN ITMPACK IP
            ON D.Itnbr = IP.ITNBR
               And P.AMBKOD = IP.AMBKOD
    LEFT OUTER JOIN PLANTPRM PP
            ON O.cust_num = PP.CANB
               And O.cust_seq = PP.cust_seq
    LEFT OUTER JOIN TAXCODE TX
            ON TX.tax_code = Case When O.tax_code1 is null Then CI.tax_code1
                                  Else O.tax_code1
                             End
    LEFT OUTER JOIN TERMS TC
            ON C.terms_code = TC.terms_code
    LEFT OUTER JOIN ITEMDIM IKT
            ON P.KKOD = IKT.ITNBR
    LEFT OUTER JOIN ITEMDIM IPL
            ON P.PKOD = IPL.ITNBR
    LEFT OUTER JOIN ITEMDIM IKP
            ON P.KPKOD = IKP.ITNBR
    LEFT OUTER JOIN ITEM PKT
            ON P.KKOD = PKT.ITEM
    LEFT OUTER JOIN ITEM PPL
            ON P.PKOD = PPL.ITEM
    LEFT OUTER JOIN ITEM PKP
            ON P.KPKOD = PKP.ITEM
    LEFT OUTER JOIN ITEM PSP
            ON P.KPKOD = PSP.ITEM
    Where   S.reason_text is not null
            And S.reason_text <> 0
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

