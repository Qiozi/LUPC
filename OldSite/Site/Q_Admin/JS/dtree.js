/*
Author:Fason(阿信)
EMAIL:fason_p@163.com
Date:2003-8-3
Mark:供参考和技术交流，使用请注明出处，多谢.
*/

var imgPath=["../app_themes/default/images/expand.gif","../app_themes/default/images/collapse.gif","../app_themes/default/images/node.gif"]

function dtree(obj,target,check){
	this.obj=obj;
	this.target=target
	this.child=0
	this.node=0
	this.msg=[]
	this.icon=[]
	this.showCheck=check
	this.html="<table id='deeptree' onselectstart='return false' cellspacing=0 cellpadding=0 border=0>"
	for(i=0;i<imgPath.length;i++){
		var tem=new Image()
		this.icon[i]=tem.src=imgPath[i]
	}
}
	
dtree.prototype.addFolder=function(txt,link,show){
	this.msg[this.node]=[txt,link?link:'']
	this.html+="<tr><td class='node'><img src='"+(show?this.icon[1]:this.icon[0])+"' id='img"+this.child+"' border=0 align='absmiddle' onclick='"+this.obj+".expand("+this.child+")'><input type='checkbox' name='treeFolder' onclick='"+this.obj+".checkAll(this,"+this.child+",event)' style='display:"+(this.showCheck?'':'none')+"'><span onmouseover='doOver(this)' onmouseout='doOut(this)' onmousedown='"+this.obj+".Light(this,"+this.node+");' title='"+txt+"'>"+txt+"</span></td></tr><tr id='child"+this.child+"' style='display:"+(show?'':'none')+"'><td class='node'>"
	this.html+="<table cellspacing=0 cellpadding=0 border=0 style='margin-left:18;'>"
	this.child++
	this.node++;
}

dtree.prototype.addNode=function(txt,link){
	this.msg[this.node]=[txt,link?link:'frmMane']
	this.html+="<tr><td class='node'><img src='"+this.icon[2]+"' align='absmiddle' onclick='"+this.obj+".Light(this.nextSibling.nextSibling,"+this.node+")'><input type='checkbox' name='treeNode' onclick='"+this.obj+".parentCheck(this,event)' style='display:"+(this.showCheck?'':'none')+"'><span onmouseover='doOver(this)' onmouseout='doOut(this)' onmousedown='"+this.obj+".Light(this,"+this.node+")' title='"+txt+"'>"+txt+"</span></td></tr>"
	this.node++;
}

dtree.prototype.endFolder=function(){
	this.html+="</table></td></tr>"
}


dtree.prototype.expand=function(childNum,flag){
var isExpand=document.getElementById("child"+childNum).style.display
document.getElementById("img"+childNum).src=isExpand=='none'?this.icon[1]:this.icon[0]
document.getElementById("child"+childNum).style.display=isExpand=='none'?'':'none'
}

dtree.prototype.expandAll=function(flag){
	if(this.child>0)
	for(i=0;i<this.child;i++){
	document.getElementById("img"+i).src=flag?this.icon[1]:this.icon[0]
	document.getElementById("child"+i).style.display=flag?'':'none'
}
}

dtree.prototype.checkAll=function(obj,childNum,evt){
	obj.blur()
	var evt=window.event
	if(!evt.shiftKey)return;
	var child=document.getElementById("child"+childNum)
	var node=child.getElementsByTagName("INPUT")
	for(i=0;i<node.length;i++)node[i].checked=obj.checked
	this.parentCheck(obj,evt)
}

dtree.prototype.parentCheck=function(obj,evt){
	obj.blur()
	evt=evt?evt:(window.event)?window.event:""
	if(!evt.shiftKey)return;
	for(i=this.child-1;i>=0;i--){
	var checkParent=true
	var c=document.getElementById("child"+i)
	var node=c.getElementsByTagName("INPUT")
	for(j=0;j<node.length;j++)if(!node[j].checked)checkParent=false
	document.getElementById("img"+i).nextSibling.checked=checkParent
	}
}

dtree.prototype.getCheckedValue=function(){
	var value=[]
	var node=document.getElementById('deeptree').getElementsByTagName("INPUT")
	for(i=0;i<node.length;i++)if(node[i].checked&&node[i].name=="treeNode")value[value.length]=this.msg[i][0]
	return value

}
dtree.prototype.init=function(){
	this.html+=(this.node>0?"":"<tr><td>暂时没有数据</td></tr>")+"</table>"
	document.write(this.html)
}

var tem=null
function doOver(o){
o.className='NodeOver'
}

function doOut(o){
o.className=(tem==o?'NodeFocus':'')
}
dtree.prototype.Light=function(o,nodeNum){
if(!tem)tem=o
tem.className=''
o.className='NodeFocus'
tem=o
if(this.msg[nodeNum][1])window.open(this.msg[nodeNum][1],this.target);//修改目标框架
}
