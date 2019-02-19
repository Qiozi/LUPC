// JScript 文件
//
//  Anthem_PreCallBack()
//  Anthem_PostCallBack()
//  confirm_PreCallBack(button)
//  confirm2_PreCallBack(button)
//  displayAndClose(e)
//  RemoveLoadWait()
//  LoadWait()
//  ParentLoadWait()
//  ParentRemoveLoadWait()
//
//
//
//
//
//
//
//
//

//-----------------------------------------------------------------------------
function Anthem_PreCallBack() {
//-----------------------------------------------------------------------------
	var loading = document.createElement("div");
	loading.id = "loading";
	loading.style.color = "black";
	loading.style.backgroundColor = "red";
	loading.style.paddingLeft = "55px";
	loading.style.paddingRight = "55px";
	loading.style.position = "absolute";
	loading.style.right = "100px";
	loading.style.top = "100px";
	loading.style.zIndex = "9999";
	loading.style.height="30px";
    loading.style.lineHeight = "30px";
	loading.innerHTML = "working...";
	document.body.appendChild(loading);
}

//-----------------------------------------------------------------------------
function Anthem_PostCallBack() {
//-----------------------------------------------------------------------------
	var loading = document.getElementById("loading");
	document.body.removeChild(loading);
}


//-----------------------------------------------------------------------------
function confirm_PreCallBack(button)
//-----------------------------------------------------------------------------
{
if (!confirm('Are you Delete?')) {
						return false;}
}


//-----------------------------------------------------------------------------
function confirm2_PreCallBack(button)
//-----------------------------------------------------------------------------
{
if (!confirm('Are youu sure?')) {
						return false;}
}


//-----------------------------------------------------------------------------
function displayAndClose(e)
//-----------------------------------------------------------------------------
{
    if(document.getElementById(e).style.display == "none")
        document.getElementById(e).style.display = '';
    else
        document.getElementById(e).style.display = "none";

}



//-----------------------------------------------------------------------------
function RemoveLoadWait()
//-----------------------------------------------------------------------------
{
    alert("dd");
   // $('#page_loading').css("display", "none");
}



//-----------------------------------------------------------------------------
function LoadWait()
//-----------------------------------------------------------------------------
{
    $('#page_loading').css("display", "");
}


//-----------------------------------------------------------------------------
function ParentRemoveLoadWait()
//-----------------------------------------------------------------------------
{
    try{
	    var el = parent.document.getElementById("loading");
	    if(el != null)
		    parent.document.body.removeChild(el);
    }
    catch(e){}
}



//-----------------------------------------------------------------------------
function ParentLoadWait()
//-----------------------------------------------------------------------------
{
		ParentRemoveLoadWait();
		
		var xPos = 0;
		var yPos = 0;
		var eWidth = 200;
		var eHeight = 100;
		var textHeight = 0;
		
		if (typeof parent.window.pageYOffset != 'undefined') {
		   yPos =parent.window.pageYOffset;
		}
		else if (typeof parent.document.compatMode != 'undefined' &&
			 parent.document.compatMode != 'BackCompat') {
		   yPos = parent.document.documentElement.scrollTop;
		   textHeight = parent.document.documentElement.clientHeight;
		}
		else if (typeof parent.document.body != 'undefined') {
		   yPos = parent.document.body.scrollTop;
		   textHeight = parent.document.body.clientHeight;
		}

		if(!parent.document.all)
		{
			// firefox 
			xPos = (parent.window.pageXOffset + parent.window.innerWidth - eWidth)/2;
			yPos = parent.window.pageYOffset +( parent.window.innerHeight - eHeight)/2 ;
		}
		else
		{
			xPos = (parent.document.body.scrollLeft + parent.window.document.body.clientWidth - eWidth)/2;			
			yPos = yPos + (textHeight - eHeight)/2;
			
		}

		var loading = parent.document.createElement("div");
		
		loading.id = "loading";
		loading.style.color = "black";
		loading.style.backgroundColor = "#f2f2f2";
	//	loading.style.paddingLeft = "55px";
//		loading.style.paddingRight = "55px";
		loading.style.paddingTop = "20px";
		loading.style.position = "absolute";
		loading.style.right = xPos + "px";
		loading.style.top = yPos + "px";
		loading.style.width= eWidth + "px";;
		loading.style.zIndex = "9999";
		loading.style.height= 50 + "px";
		loading.style.lineHeight = 50 + "px";
		loading.innerHTML = "Loading...";
		//loading.style.z_index="1111";
		loading.filter = "alpha(opacity=80)";
		loading.mozOpacity = "0.8"; 
		loading.style.border = "1px solid #cccccc";
		loading.style.textAlign="center";

		if(parent.document.getElementById('loading') == null);
			parent.document.body.appendChild(loading);
}






function JSAParent(the)
{
    
    parent.window.document.getElementById('iframe1').src=the.href;
    ParentLoadWait();
}