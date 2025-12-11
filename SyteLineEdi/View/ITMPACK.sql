Create View ITMPACK
As

Select  
	[pack_type]	As	AMBKOD  
      ,[pack_desc]	As	AMBTAN  
	,[item]	As	ITNBR   
      ,[box_code]	As	KKOD    
      ,[box_qty]	As	KMIK    
      ,[palette_code]	As	PKOD    
      ,[palette_qty]	As	PKMIK   
      ,[palette_seq_box_qty]	As	PKSIR   
      ,[cover_code]	As	KPKKOD  
      ,[cover_qty]	As	KPMIK   
      ,[separator_code]	As	SPKOD   
      ,[separator_qty]	As	SPMIK   
      ,[cust_pack_code]	As	 MAMBKOD 
      ,[cust_box_code]	As	 MKKOD   
      ,[cust_palette_code]	As	 MPKOD   
      ,[cust_seperator_code]	As	 MSPKOD  
      ,[cust_cover_code]	As	 MKPKOD  
      ,[item_rev_num]	As	 REVNO   
      ,[item_rev_date]	As	 REVTAR  
      ,[label_type]	As	 ETKTIP  
      ,[userf1]	As	 USERF1  
      ,[userf2]	As	USERF2  
      ,[userf3]	As	USERF3  
      ,[userf4]	As	USERF4  
      ,[userf5]	As	USERF5  
from TR_itempack_edi