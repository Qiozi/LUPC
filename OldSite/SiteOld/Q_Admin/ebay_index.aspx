<%@ Page Language="C#" MasterPageFile="~/Q_Admin/ebay.master" AutoEventWireup="true" CodeFile="ebay_index.aspx.cs" Inherits="Q_Admin_ebay_index" Title="Ebay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="div_top" style="line-height: 50px; background:#fff">
    Ebay Manage
    </div>
    <div id="top_main_nav" style="padding-left:5px; background: #fff">
        <ul>
            <li id="main_btn_1" class="a_main_btn">                
                <table>
                    <tr>
                        <td><a href="ebay_list_system.aspx" onclick="ChnageMainBtn('main_btn_1', this.href, 1); return false;" target="iframe1">R</a></td>
                        <td><a href="" onclick="ChnageMainBtn('main_btn_1', null, 1); return false;"> System List</a></td>
                    </tr>
                </table>
                
            </li>
            <li id="main_btn_4" class="a_main_btn">
                <table>
                    <tr>
                        <td style="width: 20%"><a href="" onclick="ViewSystemDetail(document.getElementById('current_ebay_store_id').value); return false;">R</a></td>
                        <td><a id="main_btn_4_a" href="" onclick="ChnageMainBtn('main_btn_4', null, 4); return false;">System Detail</a></td>
                    </tr>
                </table>
            </li>
            <li id="main_btn_2" class="a_main_btn">
                <table>
                    <tr>
                        <td><a href="ebay_list_part.aspx" onclick="ChnageMainBtn('main_btn_2', this.href, 2); return false;">R</a> </td>
                        <td><a href="" onclick="ChnageMainBtn('main_btn_2', null, 2); return false;">Part List</a></td>
                    </tr>
                </table>
            </li>
            <li id="main_btn_5" class="a_main_btn">
                <table>
                    <tr>
                        <td><a href="" onclick="ViewPartDetail(document.getElementById('current_ebay_store_id_part').value); return false;">R</a></td>
                        <td><a id="main_btn_5_a" href="" onclick="ChnageMainBtn('main_btn_5', null, 5); return false;">Part Detail</a></td>
                    </tr>
                </table>
            </li>
            <li id="main_btn_3" class="a_main_btn">
                <table>
                    <tr>
                        <td><a href="ebay_list_templete.aspx" onclick="ChnageMainBtn('main_btn_3', this.href, 3); return false;"">R</a></td>
                        <td><a href="" onclick="ChnageMainBtn('main_btn_3', null, 3); return false;">Templete List</a></td>
                    </tr>
                </table>
            </li>
            
        </ul>
        <div style="text-align: left; border: 1px solid #aaccee; clear:both" id="div_main">
            <iframe src="about:blank" id="iframe1" name="iframe1" style="width: 99%; height: 100%; display:none" frameborder="0"></iframe>
            <iframe src="about:blank" id="iframe2" name="iframe2" style="width: 99%; height: 100%; display:none" frameborder="0"></iframe>
            <iframe src="about:blank" id="iframe3" name="iframe3" style="width: 99%; height: 100%; display:none" frameborder="0"></iframe>
            <iframe src="about:blank" id="iframe4" name="iframe4" style="width: 99%; height: 100%; display:none" frameborder="0"></iframe>   
            <iframe src="about:blank" id="iframe5" name="iframe5" style="width: 99%; height: 100%; display:none" frameborder="0"></iframe>         
        </div>
    </div>
    <input type="hidden" id="current_ebay_store_id" value="0" />
    <input type="hidden" id="current_ebay_store_id_part" value ="0" />
    <script type="text/javascript">
    
    var _current_iframe_id = null;
    var _current_li_id = null;
    var _current_ebay_store_id= 0;
    
    function ChnageMainBtn(id, href, index)
    {
        if (_current_iframe_id != null)
        {
            document.getElementById(_current_iframe_id).className = "a_main_btn";
            
        }
        _current_iframe_id = id;
        document.getElementById(id).className="a_main_btn_selected";
        ViewIFrame(index,href);
        
    }
    
    function ViewIFrame(e, href)
    {
        
        var id = "iframe"+e;
        
        if(_current_li_id != null)
        {
            document.getElementById(_current_li_id).style.display = "none";
            
        }
        _current_li_id = id;
        document.getElementById(id).style.display = '';

        if(href != null)
            document.getElementById(id).src = href;
    }
    
    function ViewSystemDetail(sku)
    {
      
        document.getElementById("current_ebay_store_id").value = sku;
        ChnageMainBtn('main_btn_4', "ebay_detail_system.aspx?id="+ sku, 4); 
        document.getElementById("main_btn_4_a").innerHTML = "System Detail("+ sku +")";
    }
    
    function ViewPartDetail(sku)
    {
        document.getElementById("current_ebay_store_id_part").value = sku;
        ChnageMainBtn('main_btn_5', "ebay_detail_part.aspx?id="+ sku, 5); 
        document.getElementById("main_btn_5_a").innerHTML = "Part Detail("+ sku +")";
    }
    //ViewSystemDetail("1234");
    document.getElementById("div_main").style.height = parseInt( document.body.clientHeight) - 82;
</script>
</asp:Content>

