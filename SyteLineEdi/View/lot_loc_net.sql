Create View lot_loc_net
As
Select l.whse, l.item, l.loc, l.lot, l.createdate, (qty_on_hand - isnull(count_qty,0)) As qty_on_hand
	From lot_loc l
	left join dcitemlotsum d on l.whse=d.whse 
						and l.item=d.item 
						and l.loc=d.loc 
						and l.lot=d.lot