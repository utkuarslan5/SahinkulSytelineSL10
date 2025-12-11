Create View itemcustprice_Fatura
As
Select i.cust_num, i.item, i.cont_price,i.effect_date
From itemcustprice i
inner Join itemcustprice_maxdate d
	On d.item=i.item 
	and d.cust_num=i.cust_num 
	and d.maxdate=i.effect_date
