Alter VIEW EDISIPCOM AS 
SELECT co.cust_num As C6CANB, co.cust_seq, customer.UF_B9CD As C6B9CD, 
co.whse As C6A3CD, co.co_num As C6CVNB, co.contact As C6F1CD , coitem.item As CDAITX
FROM co , coitem, customer
WHERE co.co_num=coitem.co_num 
AND co.cust_num=customer.cust_num
And co.cust_seq=customer.cust_seq