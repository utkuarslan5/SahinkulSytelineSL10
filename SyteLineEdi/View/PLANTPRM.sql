Alter View Plantprm
As
Select 
Customer.cust_num As CANB      ,
Customer.cust_seq  As cust_seq ,
Uf_B9cd As  B9CD      ,
Uf_TTIME As TTIME     ,
Uf_TARTIP AS TARTIP    ,
Uf_SFRPRM As   SFRPRM   , 
Uf_SFRSIP As    SFRSIP   , 
Uf_ASNPRM As   ASNPRM    ,
Uf_YUVPRM As    YUVPRM    ,
Uf_ETKADR1 As  ETKADR1   ,
Uf_ETKADR2 As  ETKADR2   ,
Uf_ETKKUT  As  ETKKUT    ,
Uf_ETKPAL  As  ETKPAL    ,
Uf_CALID   As CALID   ,
Uf_SDRODID As SDRODID ,
Uf_SDRODCD As SDRODCD ,
Uf_RCVODID As RCVODID ,
Uf_RCVODCD As RCVODCD ,
Uf_ASNODID As ASNODID ,
Uf_ASNODCD As ASNODCD ,
Uf_INVPRM  As INVPRM  ,
Uf_ISSUER  As ISSUER  ,
Uf_DUNSID  As DUNSID  ,
Uf_USERF1  As USERF1   ,  
Uf_USERF2  As USERF2    , 
Uf_USERF3  As USERF3     ,
Uf_USERF4  As    USERF4   ,  
Uf_USERF5  As    USERF5    , 
Uf_CPLANT  As    CPLANT     ,
Uf_LIEFERDRM As  LIEFERDRM  ,
Uf_CONSCODE  As  CONSCODE   ,
Uf_ASNKONS   As  ASNKONS     ,
Uf_KumulDuzeyi as KUMLEVEL,  
ship_code As Carrier,
cu.name As PlantName
From Customer
Left Join Custaddr As Cu
On Customer.Cust_num=Cu.Cust_num
And Customer.cust_seq=Cu.Cust_seq
