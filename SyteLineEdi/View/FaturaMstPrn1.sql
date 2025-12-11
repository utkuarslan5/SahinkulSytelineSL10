
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

ALTER view [dbo].[FaturaMstPrn1]
As
SELECT Distinct
            S.reason_text As SevkNo, S.co_num, S.co_line, S.co_release,
            O.cust_num, O.cust_seq, S.ship_date, A.name As [Name], A.Country,
            A.Addr##1, A.Addr##2, A.Addr##3, A.Addr##4, A.curr_code, A.Zip,
            A.City, A1.Name As shiptoname, A1.Addr##1 as AddrShip##1,
            A1.Addr##2 as AddrShip##2, A1.Addr##3 as AddrShip##3,
            A1.Addr##4 as AddrShip##4, D.Itnbr As Item, I.Description, I.u_m,
            S.qty_shipped, D.PRICE As DtyPrice, D.Ranno,
            C.Uf_IhracSekli As terms_code, tax_reg_num1, Uf_Taxoffice,
            O.tax_code1 As CustTax_Code, CI.tax_code1 As ItemTax_Code,
            S.Createdby As Kullanici, O.ship_code, PP.DUNSID, TX.tax_rate,
            D.Pickno, TC.description as TERMNM
    FROM    Tr_co_Ship_Sum S
    LEFT OUTER JOIN co O
            ON O.co_num = S.co_num
    LEFT OUTER JOIN custaddr A
            ON O.cust_num = A.cust_num
               And A.cust_seq = 0
    LEFT OUTER JOIN custaddr A1
            ON O.cust_num = A1.cust_num
               And A1.cust_seq = O.cust_seq
    LEFT OUTER JOIN shpdty D
            ON D.Shpno = S.reason_text
               And D.Ordno = S.co_num
               And D.SEQNO = S.co_line
    LEFT OUTER JOIN dbo.coitem CI
            ON CI.co_num = S.co_num
               And CI.co_line = S.co_line
    LEFT OUTER JOIN Customer C
            ON O.cust_num = C.cust_num
               And C.cust_seq = 0
    LEFT OUTER JOIN Item I
            ON D.Itnbr = I.Item
    LEFT OUTER JOIN PLANTPRM PP
            ON O.cust_num = PP.CANB
               And O.cust_seq = PP.cust_seq
    LEFT OUTER JOIN TAXCODE TX
            ON TX.tax_code = Case When O.tax_code1 is null Then CI.tax_code1
                                  Else O.tax_code1
                             End
    LEFT OUTER JOIN TERMS TC
            ON C.terms_code = TC.terms_code
    Where   S.reason_text is not null
            And S.reason_text <> 0
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

