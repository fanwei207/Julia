<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDI_ChangePoNew.aspx.cs" Inherits="EDI_EDI_ChangePoNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        $(function(){
            var desc = $("#txtDesc");
            var divDesc = $("#min");
            var txtDescBig = $("#txtDescBig")

            desc.focus(function(){
                   
                show(this);
                
                txtDescBig.val(desc.val()) ;

                $("#txtDescBig")[0].focus();
               
            })
                
            divDesc.focusout(function(){
                divDesc.hide();
                
                desc.val(txtDescBig.val()) 
               
            })
            

            function show(objCheckprice){
            var offclick = GetElementPoint(objCheckprice)
            var beginX = offclick["x"];
            var beginY = offclick["y"];
                  
            divDesc.css("left",beginX +3+ "px"); 
            divDesc.css("top",beginY+3+ "px");

            divDesc.show();
            }

            function GetElementPoint(object)
            {
                var x=0,y=0;
                while(object.offsetParent)
                {
         
                    x+=object.offsetLeft;
                    y+=object.offsetTop;
                    object=object.offsetParent;
                }
                x+=object.offsetLeft;
                y+=object.offsetTop;
                return {'x':x,'y':y};
            }
            
        
        })
        
    </script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
            <asp:HiddenField  ID="haveDoc" runat="server" Visible="false" Value="false"/>
            <asp:HiddenField  ID="haveDet" runat="server" Visible="false" Value="false"/>
            <div style="background-color: #d8d8d8; width: 1208px; margin-top: 4px; padding-top: 2px; padding-bottom: 2px;">
                <table cellspacing="0" cellpadding="2" bgcolor="#f4f4f4" style="width: 1200px; margin: auto; border: 3px solid white;">
                    <tr>
                        <td>Cust Po:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPoNbr" runat="server" CssClass="smalltextbox" AutoPostBack="true" Width="100px" MaxLength="20" OnTextChanged="txtPoNbr_TextChanged"></asp:TextBox>
                        </td>
                        <td>Cust Code:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCust" runat="server" CssClass="smalltextbox" Width="100px" MaxLength="8"
                                AutoPostBack="True" Enabled="false"></asp:TextBox>
                        </td>
                        <td>Ship To:
                        </td>
                        <td>
                            <asp:DropDownList ID="dropShipTo" runat="server" DataTextField="ad_name" DataValueField="ad_addr"
                                Width="100px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td>Ship Via:
                        </td>
                        <td>
                            <asp:TextBox ID="txtShipVia" runat="server" CssClass="smalltextbox" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <%--<td>Req Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtReqDate" runat="server" CssClass="smalltextbox Date" Width="100px" Enabled="false"></asp:TextBox>
                        </td>--%>
                        <td>Due Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDueDate" runat="server" CssClass="smalltextbox Date" Width="100px" Enabled="false"></asp:TextBox>
                        </td>
                        <td>Channel:
                        </td>
                        <td>
                            <asp:DropDownList ID="dropChannel" runat="server" Width="100px"
                                DataTextField="code_cmmt" DataValueField="code_value" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td>Ord Date:
                        </td>
                        <td>

                            <asp:TextBox ID="txtCreatedDate" runat="server" CssClass="smalltextbox5" Width="100px"
                                ReadOnly="True" Enabled="false"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>

                        <td>Reason for revision:</td>
                        <td colspan="6">
                            <asp:TextBox runat="server" ID="txtReason" Width =" 500px" CssClass="SmallTextBox" 
                                TextMode="MultiLine" Height="50px" ></asp:TextBox>

                        </td>
                        <td colspan="1">
                            <asp:Button Text="Save" runat="server" ID ="btnSave"  CssClass="SmallButton2" Width="80px" OnClick="btnSave_Click"/>
                        </td>
                    </tr>

                    <tr id="trUpload" runat="server">
                        <td>File Upload:
                        </td>
                        <td colspan="7">
                            <input id="fileManager" style="width: 400px; height: 23px" type="file" size="45" name="filename2"
                                runat="server" />
                            &nbsp
                        <asp:Button ID="btnUpload" runat="server" CssClass="SmallButton2"
                            Text="Upload" OnClick="btnUpload_Click" Enabled="false"
                            Width="78px" Height="22px"></asp:Button>

                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" valign="top" align="center">
                            <asp:GridView ID="gvUpload" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                Width="800px" CssClass="GridViewStyle" PageSize="20" OnRowDataBound="gvUpload_RowDataBound"
                                OnRowCommand="gvUpload_RowCommand" OnRowDeleting="gvUpload_RowDeleting"
                                DataKeyNames="id,poc_docFileName,poc_docURL,createdName,createdDate,flag">
                                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="false" />
                                <RowStyle CssClass="GridViewRowStyle" Wrap="false" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <HeaderStyle CssClass="GridViewHeaderStyle" />
                                <EmptyDataTemplate>
                                    <asp:Table ID="Table2" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                        <asp:TableRow>
                                            <asp:TableCell Text="Attach File Name" Width="540px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="Upload User" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="Upload Date" Width="80px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="View" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="Delete" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                            <asp:TableCell Text="" Width="50px" HorizontalAlign="center"></asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="poc_docFileName" HeaderText="Attach File Name">
                                        <HeaderStyle Width="540px" HorizontalAlign="Center" />
                                        <ItemStyle Width="540px" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="createdName" HeaderText="Upload User">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="createdDate" HeaderText="Upload Date">
                                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                     <asp:TemplateField HeaderText="view">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnView" runat="server" CommandName="lkbtnView" CommandArgument='<%# Eval("poc_docURL") %>'
                                        Text="view"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" Text="<u>Delete</u>" ForeColor="Black"
                                                CommandName="Delete"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8" style="height: 9px">
                            <asp:TextBox ID="txtDetLine" runat="server" CssClass="smalltextbox" Width="33px"
                                MaxLength="4"></asp:TextBox>
                            <asp:TextBox ID="txtDetCustPart" runat="server" CssClass="smalltextbox"
                                    Width="142px" MaxLength="50"></asp:TextBox>
                            <asp:TextBox ID="txtSKU" runat="server" CssClass="smalltextbox"
                                Width="50px" MaxLength="50"></asp:TextBox>
                            <asp:TextBox ID="txtDetQad" runat="server"
                                CssClass="smalltextbox" Width="141px" MaxLength="15" Enabled ="false"></asp:TextBox>
                            <asp:TextBox ID="txtDetQty"
                                    runat="server" CssClass="smalltextbox" Width="61px"
                                    MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="txtDetUm" runat="server" CssClass="smalltextbox" Width="31px" Style="text-align: center;"
                                  ReadOnly="True">EA</asp:TextBox>
                            <asp:TextBox ID="txtDetPrice" runat="server" CssClass="smalltextbox" Width="62px"
                                MaxLength="10"></asp:TextBox>
                            <asp:TextBox ID="txtDetReqDate" runat="server" CssClass="smalltextbox Date" Width="62px"></asp:TextBox>
                            <asp:TextBox ID="txtDetDueDate" runat="server" CssClass="smalltextbox Date" Width="62px"></asp:TextBox>
                             <asp:TextBox ID="txtDesc" runat="server" Width="90px" CssClass="smalltextbox"  ></asp:TextBox>
                                <div id="min" style="position: absolute; width: 161px; height: 90px;background-color:#dee;display: none">
                     <asp:TextBox runat="server" ID="txtDescBig" Width="160px" CssClass="SmallTextBox"
                        TextMode="MultiLine" Height="90px"></asp:TextBox>
                 </div>
                            <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="smalltextbox" Width="141px" MaxLength="50"></asp:TextBox>


                            <asp:Button ID="btnSaveLine" runat="server" CssClass="SmallButton2" OnClick="btnSaveLine_Click"
                                Text="Save Line" Width="78px" Enabled="False" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 4px;" colspan="8"></td>
                    </tr>
                    <tr>
                        <td colspan="8" style="vertical-align: top;">
                            <asp:GridView ID="gvlist" runat="server"  AllowPaging="true"  AutoGenerateColumns="False"
                                DataKeyNames="id,parentLine,poLine,partNbr,sku,qadPart,ordQty,um,price,dueDate,reqDate,remark,addflag,desc" OnPageIndexChanging="gvlist_PageIndexChanging"
                                OnRowDataBound="gvlist_RowDataBound"  PageSize="20"
                                CssClass="GridViewStyle AutoPageSize" OnRowCommand="gvlist_RowCommand">
                                <RowStyle CssClass="GridViewRowStyle" />
                                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                <FooterStyle CssClass="GridViewFooterStyle" />
                                <PagerStyle CssClass="GridViewPagerStyle" />
                                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                <EmptyDataTemplate>
                                    <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                        GridLines="Vertical">
                                        <asp:TableRow>
                                            <asp:TableCell HorizontalAlign="center" Text="Line" Width="30px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Cust Part" Width="150px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="SKU" Width="50px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="QAD" Width="150px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Qty" Width="60px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="UM" Width="30px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Price" Width="60px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Req Date" Width="60px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Due Date" Width="60px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Remarks" Width="150px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Ord Date" Width="60px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="Ord By" Width="60px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="" Width="100px"></asp:TableCell>
                                            <asp:TableCell HorizontalAlign="center" Text="" Width="30px"></asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField HeaderText="Line" DataField="poLine" ReadOnly="True">
                                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:BoundField>

                                    <asp:TemplateField HeaderText="Cust Part" SortExpression="partNbr">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblpartNbr" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkpartNbr" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="SKU" SortExpression="sku">
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSKU" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkSKU" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="QAD" SortExpression="qadPart">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblqadPart" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkqadPart" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qty" SortExpression="ordQty">
                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblordQty" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkordQty" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UM" SortExpression="um">
                                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        <ItemStyle Width="30px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblum" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkum" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Price" SortExpression="price">
                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblprice" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkprice" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Req Date" SortExpression="reqDate">
                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblreqDate" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkreqDate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                      <asp:TemplateField HeaderText="Due Date" SortExpression="dueDate">
                                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        <ItemStyle Width="60px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbldueDate" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkdueDate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" SortExpression="desc">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesc" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkdesc" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Remarks" SortExpression="remark">
                                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                        <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblremark" runat="server"></asp:Label>
                                            <br />
                                            <asp:Label ID="linkremark" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                   <asp:TemplateField HeaderText="" >
                                       <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                       <ItemTemplate>
                                            <asp:LinkButton ID="lkbEdit" runat="server" Text ="Edit" Font-Underline="true" CommandName ="btnEdit" CommandArgument=' <%# Eval("id") %>' Visible="false"></asp:LinkButton>
                                           &nbsp;&nbsp;&nbsp;
                                           <asp:LinkButton ID="lkbCancel" runat="server" Text ="Cancel" Font-Underline="true" CommandName ="btnCancel" CommandArgument=' <%# Eval("id") %>' Visible="false"></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp; 
                                      </ItemTemplate>
                                        </asp:TemplateField>
                                  <asp:TemplateField HeaderText="" >
                                       <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        <ItemStyle Width="80px" HorizontalAlign="Left" />
                                       <ItemTemplate>
                                                    <asp:LinkButton ID="lkbEide" runat="server" Text ="Edit" Font-Underline="true" CommandName ="lkbEide" CommandArgument=' <%# Eval("poLine") %>' Visible="true"></asp:LinkButton>
                                           &nbsp;&nbsp;&nbsp; 
                                                 <asp:LinkButton ID="lkbDeleteLine" runat="server" Text ="Delete" Font-Underline="true" CommandName ="lkbDeleteLine" CommandArgument=' <%# Eval("poLine") %>' Visible="true"></asp:LinkButton>
                                       </ItemTemplate>
                                        </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                      
                        <td colspan="8" align="center">
                            <asp:Button ID="btnCommit" runat="server" CssClass="SmallButton2"
                                Text="commit" Width="66px" OnClick="btnCommit_Click" />
                            &nbsp;&nbsp;
                            
                            &nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" OnClick="btnBack_Click"
                            Text="Back" Width="66px" />
                        &nbsp;<br />
                           <div style="color:red"> *Please fill out the application reason and upload the file, also you have revised the order line, then you can click the submit button   </div>   
                            <div style="color:red"> *若提交按钮不可点，请查看一下一上三个条件是否全，1.有填写修改的原因并且点击过“SAVE”按钮；2.有您自己上传的附件；3.进行了对订单行的操作 </div>   


                        </td>
                        <td>
                            <input id="hidPocId" runat="server" style="width: 33px" type="hidden" value="0" />

                        </td>
                       
                    </tr>
                </table>
             
            </div>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>

