<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManualPoNew.aspx.cs" Inherits="ManualPoNew" %>
<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload" TagPrefix="Upload" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript">
        //<![CDATA[
        $(function () {
            var curr = $("#ddlCurr")
            curr.change(function(){
                if(curr.val()=="RMB")
                {
                    $("#lbWarning").show();
                }
                else
                {
                    $("#lbWarning").hide();
                }
            
            });

            $('#txtCust1').hide();
            if ($("#txtCust").size() > 0) {
                
                $("#txtCust").AutoComplete({
                    cols: [{
                        width: "70px",
                        name: "代码"
                    }, {
                        width: "200px",
                        name: "名称"
                    }, {
                        width: "200px",
                        name: "地址1"
                    }, {
                        width: "200px",
                        name: "地址2"
                    }],
                    fields: [{
                        val: "Code",
                        align: "center"
                    }, {
                        val: "Name",
                        align: "left"
                    }, {
                        val: "Addr1",
                        align: "left"
                    }, {
                        val: "Addr2",
                        align: "left"
                    }],
                    url: "/Ajax/Customer.ashx",
                    val: "0",
                    eVals: [{
                        targetCls: "CustomerOutput1",
                        valCol: 1
                    }],
                    isDyn: true
                },txtCust_textChanged);
            }
        }
        );

        var theForm = document.forms['form1'];
        if (!theForm) {
            theForm = document.form1;
        }
        function __doPostBack(eventTarget, eventArgument) {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;
                theForm.submit();
            }
        }

        function txtCust_textChanged()
        {
            $("#txtCust1").val( $("#txtCust").val());
            setTimeout(__doPostBack("txtCust1",""), 0);
        }

        
        //]]>
</script>

</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <div style="background-color: #d8d8d8; width: 1004px; margin-top: 4px; padding-top: 2px;
            padding-bottom: 2px;">
            <table cellspacing="0" cellpadding="2" bgcolor="#f4f4f4" style="width: 1000px; margin: auto;
                border: 3px solid white;">
                <tr>
                    <td>
                        Cust Po:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPoNbr" runat="server" CssClass="smalltextbox" Width="100px" MaxLength="20"></asp:TextBox>
                    </td>
                    <td>
                        Cust Code:
                    </td>
                    <td>
                       <asp:TextBox ID="txtCust" runat="server" CssClass="smalltextbox" Width="100px" MaxLength="8"
                            ></asp:TextBox>
                    
                        <asp:TextBox ID="txtCust1" runat="server" CssClass="smalltextbox" Width="100px" MaxLength="8"
                            AutoPostBack="true" OnTextChanged="txtCust_TextChanged"></asp:TextBox>
                    </td>
                    <td>
                        Ship To:
                    </td>
                    <td>
                        <asp:DropDownList ID="dropShipTo" runat="server" DataTextField="ad_name" DataValueField="ad_addr"
                            Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Ship Via:
                    </td>
                    <td>
                        <asp:TextBox ID="txtShipVia" runat="server" CssClass="smalltextbox" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Req Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtReqDate" runat="server" CssClass="smalltextbox Date" Width="100px"></asp:TextBox>
                    </td>
                    <td>
                        Due Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDueDate" runat="server" CssClass="smalltextbox Date" Width="100px"></asp:TextBox>
                    </td>
                    <td>
                        Channel:
                    </td>
                    <td>
                        <asp:DropDownList ID="dropChannel" runat="server" Width="100px" 
                            DataTextField="code_cmmt" DataValueField="code_value">
                        </asp:DropDownList>
                    </td>
                    <td>
                        
                        Ord Date:
                    </td>
                    <td>
                        
                        <asp:TextBox ID="txtCreatedDate" runat="server" CssClass="smalltextbox5" Width="100px"
                            ReadOnly="True"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>
                        Sale Order Domain:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSodomain" runat="server" Width="100px" >
                            <asp:ListItem Text="SZX" Value="SZX" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="ZQZ" Value="ZQZ" ></asp:ListItem>
                            <asp:ListItem Text="YQL" Value="YQL" ></asp:ListItem>
                            <asp:ListItem Text="ZQL" Value="ZQL" ></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        Curr:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCurr" runat="server" Width="80px">
                            <asp:ListItem Text="USD" Value="USD" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="RMB" Value="RMB"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td colspan="4"><label id="lbWarning" style="color:red;display:none">*Please fill in the tax price,the reason is that you choose RMB. </label></td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="ckbSample" runat="server" Text="样品" />
                        <asp:CheckBox ID="ckbType" runat="server" Text="售后" />
                    </td>
                    <td>
                        
                    </td>
                    <td>
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnSaveHrd" runat="server" CssClass="SmallButton2" OnClick="btnSaveHrd_Click"
                            Text="Save" Width="66px" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnApprove" runat="server" CssClass="SmallButton2" OnClick="btnApprove_Click"
                            Text="Approve" Width="66px" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" OnClick="btnBack_Click"
                            Text="Back" Width="66px" />
                    </td>
                    <td>
                        <input id="hidHrdID" runat="server" style="width: 33px" type="hidden" value="0" />
                        <input id="hidAppr" runat="server" style="width: 33px" type="hidden" value="0" />
                        <asp:CheckBox ID="chkSubmit" runat="server" Visible="False" />
                        <asp:CheckBox ID="chkVerify" runat="server" Visible="False" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr id="trUpload" runat="server">
                    <td>
                        File Upload:
                    </td>
                    <td colspan="7">
                        <Upload:InputFile ID="fileAttachFile" runat="server" Width="400px" />
                        &nbsp
                        <asp:Button ID="btnUpload" runat="server" CssClass="SmallButton2" Text="Upload" OnClick="btnUpload_Click"
                        Width="78px" Height="22px"></asp:Button>

                    </td>
                </tr>
                <tr>
                <td colspan="8" valign="top" align="center">
                    <asp:GridView ID="gvUpload" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Width="800px" CssClass="GridViewStyle" PageSize="10" OnRowDataBound="gvUpload_RowDataBound"
                        OnRowCommand="gvUpload_RowCommand" OnRowDeleting="gvUpload_RowDeleting" 
                        DataKeyNames="DocId,hrdId,Path,CreatedBy,PhysicalName,Name">
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
                                    <asp:TableCell Text=""   Width="50px" HorizontalAlign="center"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Attach File Name">
                                <HeaderStyle Width="540px" HorizontalAlign="Center" />
                                <ItemStyle Width="540px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Uploader" HeaderText="Upload User">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CreatedDate" HeaderText="Upload Date">
                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            </asp:BoundField>
                            <asp:ButtonField Text="View" HeaderText="View" CommandName="View">
                                <ControlStyle Font-Bold="False" Font-Underline="True" />
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                            </asp:ButtonField>
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
                        <asp:TextBox ID="txtDetLine" runat="server" CssClass="smalltextbox" Width="28px"
                            MaxLength="4"></asp:TextBox><asp:TextBox ID="txtDetCustPart" runat="server" CssClass="smalltextbox"
                                Width="128px" MaxLength="30"></asp:TextBox>
                        <asp:TextBox ID="txtSKU" runat="server" CssClass="smalltextbox"
                                Width="40px" MaxLength="50"></asp:TextBox>
                        <asp:TextBox ID="txtDetQad" runat="server"
                                    CssClass="smalltextbox" Width="100px" MaxLength="15"></asp:TextBox><asp:TextBox ID="txtDetQty"
                                        runat="server" CssClass="smalltextbox" Width="50px" 
                            MaxLength="10"></asp:TextBox><asp:TextBox
                                            ID="txtDetUm" runat="server" CssClass="smalltextbox" Width="31px" Style="text-align: center;"
                                            ReadOnly="True">EA</asp:TextBox>
                        <asp:TextBox ID="txtDetPrice" runat="server" CssClass="smalltextbox" Width="62px"
                            MaxLength="10"></asp:TextBox>
                        <asp:TextBox ID="txtDetReqDate" runat="server" CssClass="smalltextbox Date" Width="62px"></asp:TextBox><asp:TextBox
                            ID="txtDetDueDate" runat="server" CssClass="smalltextbox Date" Width="62px"></asp:TextBox>
                        <asp:TextBox ID="txtDetDesc" runat="server" CssClass="smalltextbox" Width="141px" MaxLength="100"></asp:TextBox>
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="smalltextbox" Width="141px" MaxLength="50"></asp:TextBox>


                        
                        <asp:Button ID="btnSaveLine" runat="server" CssClass="SmallButton2" OnClick="btnSaveLine_Click"
                            Text="Save Line" Width="78px" Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 4px;" colspan="8">
                    </td>
                </tr>
                <tr>
                    <td colspan="8" style="vertical-align: top;">
                        <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            DataKeyNames="mpod_id,mpod_hrd_id,mpod_submittedBy,mpod_isAppended,mpod_IsSample" OnPageIndexChanging="gvlist_PageIndexChanging"
                            OnRowDataBound="gvlist_RowDataBound" OnRowDeleting="gvlist_RowDeleting" PageSize="20"
                            OnRowCancelingEdit="gvlist_RowCancelingEdit" OnRowEditing="gvlist_RowEditing"
                            OnRowUpdating="gvlist_RowUpdating" CssClass="GridViewStyle AutoPageSize">
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
                                        <asp:TableCell HorizontalAlign="center" Text="Description" Width="150px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="Remarks" Width="150px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="Ord Date" Width="60px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="Ord By" Width="60px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="" Width="100px"></asp:TableCell>
                                        <asp:TableCell HorizontalAlign="center" Text="" Width="30px"></asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField HeaderText="Line" DataField="mpod_line" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="mpod_cust_part" HeaderText="Cust Part" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:BoundField>
                                 
                                 <asp:TemplateField HeaderText="SKU">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txSKU" runat="server" CssClass="smalltextbox" Text='<%# Bind("mpod_SKU") %>'
                                            Width="100%"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSKU" runat="server" Text='<%# Bind("mpod_SKU") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="mpod_qad" HeaderText="QAD" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Qty">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txQty" runat="server" CssClass="smalltextbox" Text='<%# Bind("mpod_ord_qty", "{0:F0}") %>'
                                            Width="100%"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("mpod_ord_qty", "{0:F0}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txChkQtyEdit" runat="server" CssClass="smalltextbox" Text='<%# Bind("mpod_ord_qty", "{0:F0}") %>'
                                            Width="100%"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txChkQty" runat="server" CssClass="smalltextbox" Text='<%# Bind("mpod_ord_qty", "{0:F0}") %>'
                                            Width="100%"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="UM" DataField="mpod_um" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Price">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txPrice" runat="server" CssClass="smalltextbox" Text='<%# Bind("mpod_price", "{0:N5}") %>'
                                            Width="100%"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("mpod_price") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Req Date">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txReqDate" runat="server" CssClass="smalltextbox" Text='<%# Bind("mpod_req_date", "{0:yyyy-MM-dd}") %>'
                                            Width="100%"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("mpod_req_date", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Due Date">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txDueDate" runat="server" CssClass="smalltextbox" Text='<%# Bind("mpod_due_date", "{0:yyyy-MM-dd}") %>'
                                            Width="100%"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("mpod_due_date", "{0:yyyy-MM-dd}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txDesc" runat="server" CssClass="smalltextbox" MaxLength="100"
                                            Text='<%# Bind("mpod_desc") %>' Width="100%"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbDesc" runat="server" Text='<%# Bind("mpod_desc") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txRemarks" runat="server" CssClass="smalltextbox" MaxLength="50"
                                            Text='<%# Bind("mpod_rmks") %>' Width="100%"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("mpod_rmks") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" Width="150px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="mpod_createdName" HeaderText="Ord By" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="mpod_createdDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Ord Date"
                                    HtmlEncode="False" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:BoundField>
                                <asp:CommandField ShowEditButton="True">
                                    <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                                    <HeaderStyle Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True" DeleteText="Del">
                                    <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:CommandField>
                                  <asp:TemplateField HeaderText="样品">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:Label ID="lblSample" runat="server" Text='<%# Bind("mpod_IsSample") %>'></asp:Label>
                                </ItemTemplate>
                                      <EditItemTemplate>
                                          <asp:CheckBox ID="ckbSample"  runat="server" Checked='<%# Bind("mpod_IsSample") %>' Enabled="False" />

                                      </EditItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
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
