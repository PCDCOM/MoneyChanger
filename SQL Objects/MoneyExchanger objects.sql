--Data Source=DELL-PC;Initial Catalog=ForexMoney;User ID=sa

Exec GetLocationCurrency '' ,'FOREIGN' 

Exec GetLocationCODE 

Exec GetLocationCurrency '' ,'LOCAL' 

Exec GetUser 'FE' ,'FE' 


Select ForeignCurrency=sum(Stock*AvgCost)   From TblLocationCurrency   Where CurrencyCode <> 'SGD'

--To get the master list for customer codes

Select CustCode as Code, CustName as Description from tblCustomers where CustName like '%%' order by CustName


--To get the master list for currency codes

Select CurrencyCode as Code, CurrencyName as Description from tblCurrency where CurrencyName like '%%' order by CurrencyName


-- To get the conversion rate, average cost and stock
Exec GetLocationCurrency 'L1' ,'INRS'

Exec GetCurrency 'INRS' ,'' 


--to save the conversion details
Exec UpdateBuySell 0,'Aug 01,2013 00:00:00','MC' ,'' ,'' ,'CASH CUSTOMER' ,'.' ,'.' ,'' ,'' ,'' ,'CASH' ,'' ,'L1' ,'N' ,'' ,'N' ,'FE' ,'' ,'I' ,'' ,'O' 
Exec UpdateBuySell 0,'Aug 01,2013 00:00:00','MC' ,'' ,'' ,'CASH CUSTOMER' ,'.' ,'.' ,'' ,'' ,'' ,'CASH' ,'' ,'L1' ,'N' ,'' ,'N' ,'FE' ,'' ,'I' ,'' ,'O' 
Exec UpdateLocationCurrencyOS'L1' ,'INRS' ,'Aug 01,2013 00:00:00',0,0,1000,30,0,0,0,0,0,0,0,1000,'FE' ,'' 
Exec GetAvgCostLocationOs 'INRS' ,'L1' ,'Aug 01,2013 00:00:00'
Exec UpdateLocationCurrencyStock 'L1' ,'INRS' ,-1000,0.02225089,'S' 
Exec UpdateCustomersOs 'CASH' ,'Aug 01,2013 00:00:00',30,0,'FE' ,'' 
Exec UpdateCustomersBalance 'CASH' ,30,'' ,'U'

Exec UpdateBuySellDetail 264093,2,'B' ,'SGD' ,1,30,30,'N' ,0,'FE' ,'' ,'I' 
Exec UpdateLocationCurrencyOS'L1' ,'SGD' ,'Aug 01,2013 00:00:00',30,30,0,0,0,0,0,0,0,0,30,0,'FE' ,'' 
Exec GetAvgCostLocationOs 'SGD' ,'L1' ,'Aug 01,2013 00:00:00'
Exec UpdateLocationCurrencyStock 'L1' ,'SGD' ,30,1,'B' 
Exec UpdateCurrencyStock 'SGD' ,30,1,'B' 
Exec UpdateCustomersOs 'CASH' ,'Aug 01,2013 00:00:00',0,30,'FE' ,'' 
Exec UpdateCustomersBalance 'CASH' ,-30,'' ,'U'
Exec GetLocationCurrency '' ,'LOCAL' 
Exec GetLocationCurrency '' ,'FOREIGN' 




