// JavaScript Document
//-----------------------------------------------------------------------------------------------
// Diagram Functions
//-----------------------------------------------------------------------------------------------
function SetDiagramPosition ()
{			
	var xPos;
	var yStart;
	var yPos;
	var yInc;
	
	if (pGI)
	{	
		if (IsNetScapeBrowser ())
		{
			
			xPos = window.document.body.clientWidth  - (window.document.body.clientWidth - 776)/2 + 10 ;//window.pageXOffset ;//+ window.innerWidth ;//- 222;

			var yHeight = document.getElementById ("IconDiagram_Layer").clientHeight;
			if (yHeight == 0)
				yHeight = 128;
			yPos = window.pageYOffset + 20;// window.innerHeight - yHeight - 40;
			
			if (pGI.AlienwareRequest > 0)
				yStart = yPos;		
			else
			{			
				yStart = document.getElementById("IconDiagram_Layer").style.top;			
				yInc = Math.log(Math.pow(yPos - yStart, 3));
				
				if (yStart + yInc >= yPos)
					yStart = yPos;
				else if (yStart < yPos)	
					yStart += yInc;
				else
					yStart = yPos;		
			}
			
			document.getElementById("IconDiagram_Layer").style.top = yStart-20;
			//document.getElementById("IconDiagram_Layer").style.left = xPos;
			document.getElementById("IconDiagram_Layer").style.left = 0;//xPos;
			
//			document.getElementById("SystemDiagram_Layer").style.top  = yStart - 450;
//			document.getElementById("SystemDiagram_Layer").style.left = xPos - 300;
		}
		else
		{
			xPos = document.body.scrollLeft + window.document.body.clientWidth - 20;	
			xPos = window.document.body.clientWidth  - (window.document.body.clientWidth - 776)/2 + 112 ;
			yPos = document.body.scrollTop + 20;// + window.document.body.clientHeight ;//- IconDiagram_Layer.clientHeight - 400;
			
			if (pGI.AlienwareRequest > 0)
				yStart = yPos;		
			else
			{
				yStart = IconDiagram_Layer.style.pixelTop;		
				yInc = Math.log(Math.pow(yPos - yStart, 2));		
				if (yStart + yInc >= yPos)
					yStart = yPos;
				else if (yStart < yPos)	
					yStart += yInc;
				else
					yStart = yPos;		
			}
			
			IconDiagram_Layer.style.pixelTop  = yStart-5 ;
			IconDiagram_Layer.style.pixelLeft = 0;//xPos - 100;
			//IconDiagram_Layer.style.pixelLeft = document.getElementById("SystemDiagram_Layer").style.pixelLeft+ 376 - 182;;
			//
//			SystemDiagram_Layer.style.pixelTop  = yStart - 0;
//			SystemDiagram_Layer.style.pixelLeft = xPos - 295;	
		}
	}
	
	var nTimeOut = 20;
	if (pGI && pGI.AlienwareRequest > 0)
		nTimeOut = 2000;
	setTimeout('SetDiagramPosition()', nTimeOut);
}

function IsNetScapeBrowser ()
{
	return !document.all;
}

var DiagramVisible = false;
function __OnToggle_Diagram ()
{		
	DiagramVisible = !GetLayerVisibility ('SystemDiagram_Layer');
	if (DiagramVisible)							
		CheckRequirementsAndProvisions ();
	
	SetLayerVisibility ('SystemDiagram_Layer', DiagramVisible);
}

function __OnLoad_float()
{		
	SetLayerVisibility ('IconDiagram_Layer', true);
	SetDiagramPosition ();
}

function SetLayerVisibility (sLayerId, bVisible)
{
	var sStatement;
	var sVisibility = (bVisible) ? "visible" : "hidden";
	if (IsNetScapeBrowser ())
		sStatement = "document.getElementById ('" + sLayerId + "').style.visibility = '" + sVisibility + "';";		
	else
		sStatement = sLayerId + ".style.visibility = '" + sVisibility + "';";	
		
	try {
		eval (sStatement);
	} catch (e) {}	
	
}

function GetLayerVisibility (sLayerId)
{
	var sStatement;
	var sResult = '';
	
	if (IsNetScapeBrowser ())
		sStatement = "sResult = document.getElementById ('" + sLayerId + "').style.visibility;";		
	else
		sStatement = "sResult = " + sLayerId + ".style.visibility;";	
		
	try {
		eval (sStatement);
	} catch (e) {}			
	
	return sResult == 'visible';
}
var pGI =new TGI (0,1,2,0,1,15,0,0,'$XXX,YYY','$XXX,YYY.ZZ');

function TGI (TaxPercentage, ShipRate, OrderProcessingTime, PotentialShippingDelay, FinancingEnabled, FinancingMinimumMonthlyPayment, AlienwareRequest, Total2Enabled, CurrencyFormat0Decimal, CurrencyFormat2Decimal)
{
	this.TaxPercentage = TaxPercentage;
	this.ShipRate = ShipRate;	
	this.OrderProcessingTime = OrderProcessingTime;
	this.PotentialShippingDelay = PotentialShippingDelay;	
	this.FinancingEnabled = FinancingEnabled;
	this.FinancingMinimumMonthlyPayment = FinancingMinimumMonthlyPayment;

	this.AlienwareRequest = AlienwareRequest;
	this.Total2Enabled = Total2Enabled;	
	this.CurrencyFormat0Decimal = CurrencyFormat0Decimal;
	this.CurrencyFormat2Decimal = CurrencyFormat2Decimal;
}// JavaScript Document