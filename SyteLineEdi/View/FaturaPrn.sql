
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER view [dbo].[FaturaPrn]
As
SELECT  ih.inv_num, ih.inv_date, ih.CreatedBy As PRNSTAT, ih.CreateDate,
            D.SHPNO As SevkNo, ii.co_num, ii.co_line, ii.co_release,
            ih.cust_num, ih.cust_seq, ih.ship_date, A.name As [Name],
            A.Country, A.Addr##1, A.Addr##2, A.Addr##3, A.Addr##4, A.curr_code,
            A.Zip, A1.City, A1.Name As shiptoname, A1.Addr##1 as AddrShip##1,
            A1.Addr##2 as AddrShip##2, A1.Addr##3 as AddrShip##3,
            A1.Addr##4 as AddrShip##4, ii.Item As Item, I.Description, I.u_m,
            ii.qty_invoiced as qty_shipped, ii.PRICE As DtyPrice, D.Ranno,
            P.Shipto, P.KAPI, P.Pusno, P.IRSNO, P.AMBKOD, P.KKOD, P.KSAY,
            P.Plaka, P.HKSAY, P.PKOD, P.PSAY, P.NETAGR, P.BRTAGR, P.HACIM,
            P.KAP_EN, P.KAP_BOY, P.SPKOD, P.SPMIK, P.KPKOD, P.KPMIK, P.KAP_YUK,
            P.USERF3 As SeferNo, P.USERF4 As NavlunNo, P.Whse, P.MURNKOD,
            P.MURNTNM, P.FATURASERINO, C.Uf_IhracSekli As terms_code,
            tax_reg_num1, uf_taxoffice, O.tax_code1 As CustTax_Code,
            CI.tax_code1 As ItemTax_Code, ih.Createdby As Kullanici, P.PICKNO,
            K.KNTRT, O.ship_code, IP.MAMBKOD, IP.MKKOD, IP.MPKOD, PP.DUNSID,
            TX.tax_rate, TX.sales_tax, TC.description as TERMNM,
            IKT.AYNTIP as KOLIAYN, IPL.AYNTIP as PALETAYN,
            IKP.AYNTIP as KAPAKAYN,
            ( P.KSAY * isnull(PKT.u_ws_price, 0) ) + ( P.PSAY
                                                       * isnull(PPL.u_ws_price, 0) )
            + ( P.KPMIK * isnull(PKP.u_ws_price, 0) ) + ( P.SPMIK
                                                          * isnull(PSP.u_ws_price, 0) )
            As KAPTUT
    FROM    Tr_ShppackSum P
    LEFT OUTER JOIN shpdty D
            ON D.SHPNO = P.SHPNO
               And d.Itnbr = P.Itnbr
               And D.PICKNO = P.PICKNO
               And P.Ambkod = D.Ambkod
    Left Join inv_hdr ih
            On Cast(ih.do_num as Numeric(10, 0)) = D.DO_NUM
    LEFT OUTER JOIN Tr_Inv_ItemSum ii
            ON ih.inv_num = ii.inv_num
               AND ih.inv_seq = ii.inv_seq
               And ii.co_num = D.Ordno
               And ii.co_line = D.SEQNO
    LEFT OUTER JOIN co O
            ON O.co_num = ii.co_num
    LEFT OUTER JOIN custaddr A
            ON A.cust_num = O.cust_num
               And A.cust_seq = 0
    LEFT OUTER JOIN custaddr A1
            ON A1.cust_num = O.cust_num
               And A1.cust_seq = O.cust_seq
    LEFT OUTER JOIN Customer C
            ON ih.cust_num = C.cust_num
               And C.cust_seq = 0
    LEFT OUTER JOIN dbo.coitem CI
            ON CI.co_num = D.ORDNO
               And CI.co_line = D.SEQNO
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
    LEFT OUTER JOIN Tr_inv_staxSum TX
            ON TX.inv_num = ih.inv_num
    LEFT OUTER JOIN TERMS TC
            ON ih.terms_code = TC.terms_code
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
    where   ii.co_num <> '0'
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

