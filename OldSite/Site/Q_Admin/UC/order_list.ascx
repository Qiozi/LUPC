<%@ Control Language="C#" AutoEventWireup="true" CodeFile="order_list.ascx.cs" Inherits="Q_Admin_UC_order_list" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

     <table>
        <tr>
                <td>
                    
                    &nbsp;</td>
               <td align="right">
                     <div>
                        <asp:TextBox runat="server" ID="txt_keyword" width="100"
                           ></asp:TextBox>
                        
                        <asp:DropDownList runat="server" ID="ddl_search_filed"  width="105">
                            <asp:ListItem Value="-1" Text="Select"></asp:ListItem>
                            <asp:ListItem Value="order_helper_serial_no" Text="ID"></asp:ListItem>
                            <asp:ListItem Value="oh.order_code" Text="ORDER#"></asp:ListItem>
                            <asp:ListItem Value="cs.customer_serial_no" Text="CUT#"></asp:ListItem>
                            <asp:ListItem Value="customer_shipping_first_name" Text="FIRST NAME"></asp:ListItem>
                            <asp:ListItem Value="customer_shipping_last_name" Text="LAST NAME"></asp:ListItem>                           
                        </asp:DropDownList> 
                        
                        <asp:Button ID="btn_search" runat="server" Text="Search" 
                        onclick="btn_search_Click" />
                    </div>
                    <div>
                       
                        <asp:DropDownList ID="DropDownList_OutStatus" runat="server"  width="105" 
                      >
                        </asp:DropDownList>
                         <asp:Button ID="btn_search_2" runat="server" Text="Search" 
                            onclick="btn_search_2_Click" />
                    </div>
                    <div>
                       
                        <asp:DropDownList ID="DropDownList_pay_method" runat="server"  width="105">
                        </asp:DropDownList>
                         <asp:Button ID="btn_search_3" runat="server" Text="Search" onclick="btn_search_3_Click" 
                             />
                    </div>
               </td> 
             
               
               <td>
                
                            <asp:Button ID="btn_clear_search" runat="server" Text="Clear Search" 
                                Height="74px" onclick="btn_clear_search_Click" />
                   
               </td>

               <td>
                 
                            
                            <asp:Button ID="btn_new_order" Height="74px" runat="server" Text="New Order"  
                                OnClientClick="return confirm('Are You Sure ?');" onclick="btn_new_order_Click" 
                                Width="109px"  />
                            
                    </asp:Panel>
               </td>
        </tr>
   </table> 
   <hr size="1" />
   <div>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="true"
            CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条" 
            ShowCustomInfoSection="Left" onpagechanged="AspNetPager1_PageChanged" 
            PageSize="30" >
        </webdiyer:AspNetPager>
    </div>
    <asp:ListView ID="ListView1" runat="server" ItemContainerID="itemPlaceholder" 
        onitemcommand="ListView1_ItemCommand" 
        onitemediting="ListView1_ItemEditing" onitemcanceling="ListView1_ItemCanceling" 
        onitemupdating="ListView1_ItemUpdating" 
        >
        
        <EmptyDataTemplate>
            data is empty
        </EmptyDataTemplate>
        <LayoutTemplate>
            
           <ul class="ul_table_heard" width="1300" >
                <li style="width: 100%; clear: left;">
                    <ul class="ul_row" style="margin:0px;background-color:#dab5a2;">
                        <li style="width:40px; text-align:center; background-color:#dab5a2;">Edit</li>
                        <li style="width:60px; text-align:center; background-color:#dab5a2;">Modify</li>
                        <li style="width: 40px;background-color:#DAB5A2;">ID</li>
                        <li style="width:60px;background-color:#DAB5A2;">ORDER#</li>
                        <li style="width:150px;background-color:#DAB5A2;" >DATE</li>
                        <li style="width:80px; text-align:right;background-color:#DAB5A2;">AMNT$</li>
                        <li style="width: 140px;text-align:center;background-color:#DAB5A2;">PAY</li>
                        <li style="width:150px;background-color:#DAB5A2;">NAME</li>
                        <li style="width:150px; text-align:right;background-color:#DAB5A2;">SHIPPING STATE</li>
                        <li style="width: 40px;text-align:center;background-color:#DAB5A2;">CUT#</li>
                        <li style="width:150px;background-color:#DAB5A2;" >BK STAT</li>
                        <li style="width:150px;background-color:#DAB5A2;">FRT STAT</li>                       
                    </ul>
                </li>
            <div id="itemPlaceholder" runat="server"/>
           
           </ul>
           <div style="clear:both">
              
          </div>
        </LayoutTemplate>
        <AlternatingItemTemplate>
            <li style="width: 100%; clear: left; border-bottom: 1px solid #cccccc;margin:0px; background:#f2f2f2 ">
                <ul class="ul_row" style="line-height:15px">
                    <li style="width:40px; text-align:center">&nbsp;<a href="sale_add_order.aspx?menu_id=2&order_code=<%# DataBinder.Eval(Container.DataItem, "order_code") %>">edit</a></li>
                    <li style="width: 60px"><asp:LinkButton runat="server" ID="_lb_add_note" CommandName="Edit" Text="Modify"></asp:LinkButton> </li>
                    <li style="width: 40px"> <div onclick="OpenOrderDetail('<%# DataBinder.Eval(Container.DataItem, "order_code") %>')" style="cursor:pointer"><asp:Literal runat="server" ID="_lt_id" Text='<%# DataBinder.Eval(Container.DataItem, "order_helper_serial_no")%>'></asp:Literal></div> </li>
                    <li style="width:60px">&nbsp; <asp:Literal runat="server" ID="_lt_order_code" Text='<%# DataBinder.Eval(Container.DataItem, "order_code")%>'></asp:Literal></li>
                    <li style="width:150px; text-align:right" >&nbsp;<asp:Literal runat="server" ID="_lt_order_date" Text='<%# DataBinder.Eval(Container.DataItem, "order_date").ToString()%>'></asp:Literal></li>
                    <li style="width: 80px;text-align:right">
                        $<%# DataBinder.Eval(Container.DataItem, "grand_total")%>
                        </li>
                    <li style="width:140px">&nbsp;<%# DataBinder.Eval(Container.DataItem, "pay_method")%></li>
                    <li style="width:150px"><div style="cursor:pointer" onclick="winOpen('sales_customer_history.aspx?customer_id=<%# DataBinder.Eval(Container.DataItem, "customer_serial_no")%>','order_history', 1000, 600, 300, 300)">&nbsp;<%# DataBinder.Eval(Container.DataItem, "name")%></div></li>
                    <li style="width:150px">&nbsp;<%# DataBinder.Eval(Container.DataItem, "customer_shipping_state")%></li>
                    <li style="width: 40px">&nbsp;<%# DataBinder.Eval(Container.DataItem, "customer_serial_no")%></li>
                    <li style="width:150px; color:<%# DataBinder.Eval(Container.DataItem, "fs_back_color")%>" >&nbsp;<%# DataBinder.Eval(Container.DataItem, "facture_state_name")%></li>
                    <li style="width:150px; color:<%# DataBinder.Eval(Container.DataItem, "pre_back_color")%>">&nbsp;<%# DataBinder.Eval(Container.DataItem, "pre_status_name")%></li>    
                </ul>
                
            </li>
            <%# DataBinder.Eval(Container.DataItem, "out_note").ToString() != "" ? "<li style=\"width: 100%; clear: left;margin:0px; border-bottom: 1px solid #cccccc\"><pre style=\"font-size:8.5pt;background-color: #ffffc4;margin-top:-1px;padding-left:5px; border-bottom: 1px solid #cccccc; *border: 0px\">" + DataBinder.Eval(Container.DataItem, "order_helper_serial_no") + " Note:&nbsp;&nbsp;" + DataBinder.Eval(Container.DataItem, "out_note") + "</pre></li>" : ""%>
            
        </AlternatingItemTemplate>
        <ItemTemplate>
            <li style="width: 100%; clear: left; border-bottom: 1px solid #cccccc;margin:0px; ">
                <ul class="ul_row" style="line-height:15px">
                    <li style="width:40px; text-align:center">&nbsp;<a href="sale_add_order.aspx?menu_id=2&order_code=<%# DataBinder.Eval(Container.DataItem, "order_code") %>">edit</a></li>
                    <li style="width: 60px"><asp:LinkButton runat="server" ID="_lb_add_note" CommandName="Edit" Text="Modify"></asp:LinkButton> </li>
                    <li style="width: 40px"> <div onclick="OpenOrderDetail('<%# DataBinder.Eval(Container.DataItem, "order_code") %>')" style="cursor:pointer"><asp:Literal runat="server" ID="_lt_id" Text='<%# DataBinder.Eval(Container.DataItem, "order_helper_serial_no")%>'></asp:Literal></div> </li>
                    <li style="width:60px">&nbsp; <asp:Literal runat="server" ID="_lt_order_code" Text='<%# DataBinder.Eval(Container.DataItem, "order_code")%>'></asp:Literal></li>
                    <li style="width:150px; text-align:right" >&nbsp;<asp:Literal runat="server" ID="_lt_order_date" Text='<%# DataBinder.Eval(Container.DataItem, "order_date").ToString()%>'></asp:Literal></li>
                    <li style="width: 80px;text-align:right">
                       
                        $<%# DataBinder.Eval(Container.DataItem, "grand_total")%>
                        </li>
                    <li style="width:140px">&nbsp;<%# DataBinder.Eval(Container.DataItem, "pay_method")%></li>
                    <li style="width:150px"><div style="cursor:pointer" onclick="winOpen('sales_customer_history.aspx?customer_id=<%# DataBinder.Eval(Container.DataItem, "customer_serial_no")%>','order_history', 1000, 600, 300, 300)">&nbsp;<%# DataBinder.Eval(Container.DataItem, "name")%></div></li>
                    <li style="width:150px">&nbsp;<%# DataBinder.Eval(Container.DataItem, "customer_shipping_state")%></li>
                    <li style="width: 40px">&nbsp;<%# DataBinder.Eval(Container.DataItem, "customer_serial_no")%></li>
                     <li style="width:150px; color:<%# DataBinder.Eval(Container.DataItem, "fs_back_color")%>" >&nbsp;<%# DataBinder.Eval(Container.DataItem, "facture_state_name")%></li>
                     <li style="width:150px; color:<%# DataBinder.Eval(Container.DataItem, "pre_back_color")%>">&nbsp;<%# DataBinder.Eval(Container.DataItem, "pre_status_name")%></li>
             
                </ul>
                
            </li>
            <%# DataBinder.Eval(Container.DataItem, "out_note").ToString() != "" ? "<li style=\"width: 100%; clear: left;margin:0px; border-bottom: 1px solid #cccccc\"><pre style=\"font-size:8.5pt;background-color: #ffffc4;margin-top:-1px;padding-left:5px; border-bottom: 1px solid #cccccc; *border: 0px\">" + DataBinder.Eval(Container.DataItem, "order_helper_serial_no") + " Note:&nbsp;&nbsp;" + DataBinder.Eval(Container.DataItem, "out_note") + "</pre></li>" : ""%>
            
        </ItemTemplate>
       <EditItemTemplate>
            <table cellpadding="0" cellspacing="0" style="background:Honeydew; width:100%">
                <tr>
                    <td style="width: 100px;font-size:8pt">
                    <asp:LinkButton ID="Button4" runat="server"
                    CommandName="Cancel" Text="Cancel" />
                    <asp:LinkButton runat="server" ID="_lb_Update" CommandName="Update" Text="Update"></asp:LinkButton>
                    </td>                    
                    <td  style="width: 40px;font-size:8pt"><asp:Literal runat="server" ID="_lt_order_helper_serial_no" Text='<%# DataBinder.Eval(Container.DataItem, "order_helper_serial_no")%>'></asp:Literal></td>
                    <td  style="width: 60px;font-size:8pt"><%# DataBinder.Eval(Container.DataItem, "order_code") %>
                    </td>
                    <td style="width: 120px">
                        <asp:DropDownList runat="server" ID="_dropDownList_out_status" DataSource="<%# FactureStateDB %>" DataTextField="facture_state_name" DataValueField="facture_state_serial_no" ></asp:DropDownList>
                        <asp:HiddenField runat="server" ID="_hf_back_end_status" Value='<%# DataBinder.Eval(Container.DataItem, "out_status") %>' />                   
                    </td>
                    <td style="width: 120px">
                        <asp:DropDownList runat="server" ID="_dropDownList_pre_status" DataSource="<%# PreStatusDB %>" DataTextField="pre_status_name" DataValueField="pre_status_serial_no" ></asp:DropDownList>
                        <asp:HiddenField runat="server" id="_hf_pre_status" Value='<%# DataBinder.Eval(Container.DataItem, "pre_status_serial_no") %>' />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="_txt_order_note" Columns="50"></asp:TextBox>
                    </td>
                    <td style="width: 20%"></td>
                </tr>
                
            </table>   
                    
                </ul>                
            </li>
       </EditItemTemplate>

    </asp:ListView>