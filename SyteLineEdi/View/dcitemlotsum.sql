Create View dcitemlotsum
As
Select whse,item, loc, lot,  sum(count_qty) As count_qty
	From dcitem
	Group By whse,item, loc, lot 