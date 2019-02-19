
                        <form action="page_error_info_exec.asp" method="post" target="iframe1">
                                <input type="hidden" name="parent_id" value="<%=request("parent_id") %>" />
                                <input type="hidden" name="id" value="<%= request("id") %>" />
                                    <%
                                        dim Page_error_info_exec_datetime : Page_error_info_exec_datetime = now()
                                        session("Page_error_info_exec_datetime") = Page_error_info_exec_datetime
                                        response.write ("<input type='hidden' name='run_time' value='"& Page_error_info_exec_datetime &"'/>")
                                     %>
                                    <ul  class="error_area_ul">
                                        <li>
                                            <b>Help us continuously improve by reporting any errors on the page:</b>
                                        </li>
                                        <li>
                                                &nbsp;<input type="radio" name="page_error_type" value="1" />The information above is incorrect or conflicting.
                                        </li>
                                        <li>    
                                                &nbsp;<input type="radio" name="page_error_type" value="2" />This page has misspellings and/or bad grammar.
                                        </li>
                                        <li>
                                                &nbsp;<input type="radio" name="page_error_type" value="3" />This page did not load currectly on my browser or generated an error.
                                        </li>
                                        <li>
                                                &nbsp;<input type="radio" name="page_error_type" value="4" />The rebate information is incorrect.
                                        </li>
                                        <li>
                                                <br />
                                                Please provide an example of any missing or incorrect information.
                                        </li>
                                        <li>
                                                <textarea rows="8" cols="60" name="content"></textarea>
                                        </li>
                                        <li>
                                                Email Address: <input type="text" name="email_address" maxlength="50" id="error_submit_email_address"/>
                                        </li>
                                        <li style="text-align:center">
                                                    <input type="submit" value="Submit Info" />
                                        </li>
                               </ul>
                            </form>