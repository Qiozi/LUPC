
jQuery.fn.floatdiv=function(location){
		//
	var isIE6=false;
	var Sys = {};
    var ua = navigator.userAgent.toLowerCase();
    var s;
	
    (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] : 0;
	if(Sys.ie && Sys.ie=="6.0"){
		isIE6=true;
	}
	
	var windowWidth,windowHeight;//
	//
	if (self.innerHeight) {
		windowWidth=self.innerWidth;
		windowHeight=self.innerHeight;
	}else if (document.documentElement&&document.documentElement.clientHeight) {
		windowWidth=document.documentElement.clientWidth;
		windowHeight=document.documentElement.clientHeight;
	} else if (document.body) {
		windowWidth=document.body.clientWidth;
		windowHeight=document.body.clientHeight;
	}
	return this.each(function(){
		var loc;//
		if(location==undefined || location.constructor == String){
			switch(location){
				case("customize"):
					l=(windowWidth-960)/2-$(this).width()-10;
					loc={right:l+"px", bottom:"40px"};
					break;
				case("rightbottom")://
					loc={right:"0px",bottom:"0px"};
					break;
				case("leftbottom")://
					loc={left:"0px",bottom:"0px"};
					break;	
				case("lefttop")://
					loc={left:"0px",top:"0px"};
					break;
				case("righttop")://
					loc={right:"0px",top:"0px"};
					break;
				case("middletop")://
					loc={left:windowWidth/2-$(this).width()/2+"px",top:"0px"};
					break;
				case("middlebottom")://
					loc={left:windowWidth/2-$(this).width()/2+"px",bottom:"0px"};
					break;
				case("leftmiddle")://
					loc={left:"0px",top:windowHeight/2-$(this).height()/2+"px"};
					break;
				case("rightmiddle")://
					loc={right:"0px",top:windowHeight/2-$(this).height()/2+"px"};
					break;
				case("middle")://
					var l=0;//
					var t=0;//
					l=windowWidth/2-$(this).width()/2;
					t=windowHeight/2-$(this).height()/2;
					loc={left:l+"px",top:t+"px"};
					break;
				default://
					location="rightbottom";
					loc={right:"0px",bottom:"0px"};
					break;
			}
		}else{
			loc=location;
		}
		$(this).css(loc).css("position","fixed");
		/*fied ie6 css hack*/
		if (isIE6)
		{
			$(this).css(loc).css("position","absolute");
			/**/
			//var offset=$(this).offset();
			//$(this).css({top:offset.top+"px",left:offset.left+"px"});
			$("body").css({margin:"0px",height:"100%",overflow:"auto"}).css("overflow_y","auto");
			$("html").css({overflow:"hidden"}).css("overflow_x","auto").css("overflow_y","hidden");
			/*修正ie6下浮动层在右边显示时出现在滚动条上的问题*/
			if(isIE6 && loc.right=="0px"){
						$(this).css("right","18px");
			}
		}
	});
};
