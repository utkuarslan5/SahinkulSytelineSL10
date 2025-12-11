     CREATE VIEW KONTRTSIP 
(PLANTID , GATEID , MPXITM ,MPXCUS ,                                                                  
 MATHNDCD ,KANBANNO , USERF1 ,PUSNO )                                                                   
 AS                                                                        
SELECT M.PLANTID, M.GATEID, M.MPXITM, M.MPXCUS, 
M.MATHNDCD, M.KANBANNO, M.USERF1, M.PUSNO 
FROM EDIMST M , KONTRTPF K   
WHERE M.PLANTID=K.SHIPTO AND M.GATEID=K.KAPI 
AND M.MPXCUS=K.CUST AND M.MPXITM=K.BZMITM 
AND (M.MATHNDCD<>K.USER1 OR M.KANBANNO<>K.KANBAN OR M.USERF1 <> K.USER2)      