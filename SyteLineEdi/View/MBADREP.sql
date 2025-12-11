
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER View [dbo].[MBADREP]
As
Select 
co_num As  ADCVNB,
item as ADAITX,
co_line as ADFCNB,
(qty_ordered-qty_shipped) As  ADAQQT,
due_date_day As ADBJDT ,
whse as ADA3CD,
qty_packed As ADIKST,
qty_ordered As ADDZVA,
stat as ADIIST,
packed ,
qty_packed
from coitem
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

