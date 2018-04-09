<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManualPoCancelSubmit.aspx.cs"
    Inherits="EDI_ManualPoCancelSubmit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="0" bgcolor="white" border="0" style="width: 1000px;
            margin-top: 4px;">
            <tr>
                <td>
                    Cust
                    <asp:TextBox ID="txtCust" runat="server" CssClass="smalltextbox" Width="114px" MaxLength="8"></asp:TextBox>&nbsp;
                    &nbsp;&nbsp; Cust Po
                    <asp:TextBox ID="txtPoNbr" runat="server" CssClass="smalltextbox" Width="87px" MaxLength="24"></asp:TextBox>
                    &nbsp; &nbsp;&nbsp; Createdby<asp:TextBox ID="txtCreatedBy" runat="server" CssClass="smalltextbox"
                        Width="83px"></asp:TextBox>
                    &nbsp;&nbsp; CreatedDate<asp:TextBox ID="txtCreatedDate" runat="server" CssClass="smalltextbox Date"
                        Width="83px"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="chkCancelled" runat="server" Text="Cancelled Submission" />
                    &nbsp;&nbsp;<asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" OnClick="btnQuery_Click"
                        Text="Query" Width="50px" />
                    &nbsp;
                </td>
                <td align="right">
                    &nbsp;
                </td>
                <td style="width: 3px; background-image: url(../images/bg_tb3.jpg); background-repeat: no-repeat;
                    background-position: right top;">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="18" OnRowDataBound="gvlist_RowDataBound" OnPageIndexChanging="gvlist_PageIndexChanging"
            DataKeyNames="mpo_id,mpo_flg_sub,mpo_submittedBy,mpo_flg_err,mpo_nbr_exist,mpo_nbr"
            Width="1110px" OnRowCommand="gvlist_RowCommand" OnRowEditing="gvlist_RowEditing"
            OnRowUpdating="gvlist_Updating" OnRowCancelingEdit="gvlist_RowCancelingEdit"
            CssClass="GridViewStyle AutoPageSize">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="Cust" DataField="mpo_cust" ReadOnly="true">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Cust Po">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkNbr" runat="server" CommandName="detail" Font-Bold="False"
                            Font-Underline="True" ForeColor="Black" Text='<%# Bind("mpo_nbr") %>' CommandArgument='<%# Bind("mpo_id") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="Req Date" DataField="mpo_req_date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False" ReadOnly="true">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Due Date" DataField="mpo_due_date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False" ReadOnly="true">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Ship To" DataField="mpo_shipto" ReadOnly="true">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Ship Via" DataField="mpo_shipvia" ReadOnly="true">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Ord By" DataField="mpo_createdName" ReadOnly="true">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Ord Date" DataField="mpo_createdDate" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False" ReadOnly="true">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Submit Date" DataField="mpo_submittedDate" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False" ReadOnly="true">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:CommandField EditText="<u>Cancel</u>" ShowEditButton="True" CancelText="&lt;u&gt;Cancel&lt;/u&gt; "
                    UpdateText=" &lt;u&gt;Update&lt;/u&gt;">
                    <ItemStyle Width="90px" Font-Underline="true" HorizontalAlign="Center" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="Cancel Reason">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("mpo_cancelledReason") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCancelReason" runat="server" Text='<%# Bind("mpo_cancelledReason") %>'
                            Width="280px"></asp:TextBox>
                    </EditItemTemplate>
                    <HeaderStyle Width="280px" />
                    <ItemStyle HorizontalAlign="Left" Width="280px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="So Nbr" DataField="mpo_so_nbr" ReadOnly="true">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Domain" DataField="mpo_so_domain" ReadOnly="true">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                  <asp:BoundField HeaderText="Cancel by" DataField="mpo_cancelledName" ReadOnly="true">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                  <asp:BoundField HeaderText="Cancel Date" DataField="mpo_cancelledDate"  ReadOnly="true"  DataFormatString="{0:yyyy-MM-dd}">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkPlan" runat="server" CommandName="plan" Font-Bold="False"
                            Font-Underline="True" ForeColor="Black" Text="To Plan" CommandArgument='<%# Bind("mpo_id") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
