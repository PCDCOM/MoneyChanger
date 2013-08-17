

CREATE PROCEDURE SaveTransaction(
	@CreatedBy   varchar(20), --get the login id send. as of now we can send as hardcoded value 'FF' 
	@CurrencyCode VarChar(20), --ForeignCurrency Code
	@BuyF Money, -- Buy Amount Foreign , we should send 0 for selling
	@BuyL Money, -- Buy Amount Local , we should send 0 for selling
	@SellF Money, -- Sell Amount Forign , we should send 0 for buying
	@SellL Money, -- sell Amount Local , we should send 0 for buying
	@TranType  varchar(1), -- 'B' for buy or 'S' for sell
	@Rate   DECIMAL(18,8), --Rate
	@FAmount  money, -- Famount entered r calculated
    @LAmount  money -- Samount entered or calculated
	 
)
as
BEGIN

-- getdate format 'Aug 07,2013 00:00:00'
	--@TransNo comes from this procedure
	Exec UpdateBuySell 0,GETDATE(),'MC' ,'' ,'' ,'CASH CUSTOMER' ,'.' ,'.' ,'' ,'' ,'' ,'CASH' ,'' ,'L1' ,'N' ,'' ,'N' ,@CreatedBy ,@CreatedBy ,'I' ,'' ,'O' 
	--
	Exec UpdateBuySellDetail @TransNo,1,@TranType ,@CurrencyCode ,@Rate,@FAmount,@LAmount,'N' ,0,@CreatedBy ,'' ,'I'
	
	Exec UpdateLocationCurrencyOS 'L1' ,@CurrencyCode ,GETDATE(),@BuyF,@BuyL,@SellF,@SellL,0,0,0,0,0,0,0,@FAmount,@CreatedBy ,@CreatedBy 
	
	Exec GetAvgCostLocationOs @CurrencyCode ,'L1' ,getdate()
	
	declare @tmpAmt money
	
	if(@TranType = 's')
		set @amt = @FAmount = @FAmount * -1
	else
		set @amt = @FAmount	
		
		
	Exec UpdateLocationCurrencyStock 'L1' ,@CurrencyCode ,@amt ,@Rate,@TranType

	Exec UpdateCurrencyStock @CurrencyCode ,@amt,@Rate,@TranType
	
	declare @ContraTranType varchar(1)
	if(@TranType = 'B')
		Set @ContraTranType = 'S'
	else
		Set @ContraTranType = 'B'
		
	Exec UpdateBuySellDetail TransNo,2,@ContraTranType ,'SGD' ,1,@LAmount,@LAmount,'N' ,0,@CreatedBy ,'' ,'I'
	
	
	  Exec UpdateLocationCurrencyOS 'L1' ,'SGD' ,GETDATE(),
	  @SellF,@SellF,@BuyF,@BuyF,0,0,0,0,0,0,@LAmount,0,@CreatedBy ,@CreatedBy 
	
	
	Exec UpdateLocationCurrencyStock 'L1' ,'SGD' ,@LAmount,1,@ContraTranType
	
	Exec UpdateCurrencyStock 'SGD' ,@LAmount,1,@ContraTranType
	
	Exec UpdateCustomersOs 'CASH' ,getdate(),0,100,'FE' ,''
	
	
END

