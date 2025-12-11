
ALTER view [dbo].[itemcustprice_maxdate] 
as
select Distinct item,cust_num,max(effect_date)as MaxDate 
from itemcustprice
 where effect_date <= getdate() 
   group by item,cust_num