Alter View KONTRTPF
As

Select 
Tr.Site As SRKT      ,
Tr.Cust_num as CUST  ,    
i.cust_item as ITEM ,      
Tr.contract_num as  KNTRT     ,
c.Uf_B9CD as SHIPTO    ,
Tr.item as BZMITM    ,
Tr.gate as KAPI      ,
i.UF_customeritemdesc As REFADI    ,
Tr.pack_inv as AFKOD     ,
Tr.pack_type As AMBKOD  ,  
Tr.kanban As KANBAN    ,
Tr.USER1     ,
Tr.USER2		,
Tr.USER3   ,
Tr.USER4   ,
Tr.whse as HOUSE  
From Tr_itemcust_edi Tr
Left Join itemcust  i
	On  Tr.cust_num=i.cust_num
	And Tr.item=i.item
	And Tr.cust_item_seq=i.cust_item_seq
Left Join customer c
	On Tr.cust_num=c.Cust_num
	And tr.cust_seq=c.cust_seq
