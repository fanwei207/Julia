<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManualPoHrd.aspx.cs" Inherits="ManualPoHrd" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="left">
        <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" bgcolor="white" border="0" style="width: 1000px;
            margin-top: 4px;">
            <tr style="background-image: url(../images/bg_tb2.jpg); background-repeat: repeat-x;
                height: 35px; font-family: 微软雅黑;">
                <td>
                    <asp:TextBox ID="txtCust" runat="server" CssClass="smalltextbox" Width="82px" 
                        MaxLength="8"></asp:TextBox><asp:TextBox
                        ID="txtPoNbr" runat="server" CssClass="smalltextbox" Width="165px" 
                        MaxLength="24"></asp:TextBox><asp:TextBox
                            ID="txtReqDate" runat="server" CssClass="smalltextbox Date"  
                        Width="70px"></asp:TextBox><asp:TextBox
                                ID="txtDueDate" runat="server" CssClass="smalltextbox Date" 
                        Width="70px"></asp:TextBox><asp:TextBox
                                    ID="txtShipTo" runat="server" CssClass="smalltextbox" 
                        Width="82px"></asp:TextBox><asp:TextBox
                                        ID="txtShipVia" runat="server" CssClass="smalltextbox" 
                        Width="80px"></asp:TextBox><asp:TextBox
                                            ID="txtCreatedBy" runat="server" 
                        CssClass="smalltextbox" Width="80px"></asp:TextBox><asp:TextBox
                                                ID="txtCreatedDate" runat="server" 
                        CssClass="smalltextbox Date" Width="75px"></asp:TextBox>&nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" OnClick="btnQuery_Click"
                        Text="Query" Width="50px" />
                    &nbsp;
                    <asp:Button ID="btnExcel" runat="server" CssClass="SmallButton2" OnClick="btnExcel_Click"
                        Text="Excel" Width="50px" />
                    &nbsp; &nbsp;<asp:Button ID="btnHelp" runat="server" CssClass="SmallButton2" OnClick="btnHelp_Click"
                        Text="Help" Width="50px" />
                </td>
                <td align="right">
                    <asp:Button ID="btnSave" runat="server" Text="Submit" OnClick="btnSave_Click" OnClientClick="oneclick();"
                        CssClass="SmallButton2" Width="50px" />&nbsp;
                </td>
                <td style="width: 3px; background-image: url(../images/bg_tb3.jpg); background-repeat: no-repeat;
                    background-position: right top;">
                </td>
            </tr>
            <tr>
                <td colspan="5" style="height: 4px;">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="18" OnRowDataBound="gvlist_RowDataBound" OnPageIndexChanging="gvlist_PageIndexChanging"
                        
                        DataKeyNames="mpo_id,mpo_flg_sub,mpo_submittedBy,mpo_flg_err,mpo_nbr_exist,mpo_nbr,mpod_isAppended,mpo_isVerify,mpo_isVerifyDate" Width="1000px"
                        OnRowDeleting="gvlist_RowDeleting" OnRowCommand="gvlist_RowCommand" 
                        CssClass="GridViewStyle AutoPageSize">
                        <RowStyle CssClass="GridViewRowStyle" />
                        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="Cust" DataField="mpo_cust">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Cust Po">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkNbr" runat="server" CommandName="detail" Font-Bold="False"
                                        Font-Underline="True" ForeColor="Black" Text='<%# Bind("mpo_nbr") %>' CommandArgument='<%# Bind("mpo_id") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="170px" />
                                <ItemStyle HorizontalAlign="Center" Width="170px" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Req Date" DataField="mpo_req_date" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle Width="70px" />
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Due Date" DataField="mpo_due_date" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle Width="70px" />
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ship To" DataField="mpo_shipto">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ship Via" DataField="mpo_shipvia">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ord By" DataField="mpo_createdName">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ord Date" DataField="mpo_createdDate" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle Width="70px" />
                                <ItemStyle Width="70px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <input id="chkImport" type="checkbox" name="chkImport" runat="server" value='<%#Eval("mpo_id") %>' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Submit Date" DataField="mpo_submittedDate" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True">
                                <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:CommandField>
                            <asp:BoundField HeaderText="So Nbr" DataField="mpo_so_nbr">
                                <HeaderStyle Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Domain" DataField="mpo_domain">
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkPlan" runat="server" CommandName="plan" Font-Bold="False"
                                        Font-Underline="True" ForeColor="Black" Text="To Plan" CommandArgument='<%# Bind("mpo_id") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkVerify" runat="server" CommandName="verify" Font-Bold="False"
                                        Font-Underline="True" ForeColor="Black" Text="Approve" CommandArgument='<%# Bind("mpo_id") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Sample">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="ckbSample"  runat="server" Checked='<%# Bind("mpo_isSample") %>' Enabled="False" />
                                </ItemTemplate>
                                     
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
