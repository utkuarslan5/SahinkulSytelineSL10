Create View dcitemsum
As
Select whse,item, sum(count_qty) As count_qty
	From dcitem
	Group By whse,item