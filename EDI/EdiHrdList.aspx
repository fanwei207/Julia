<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdiHrdList.aspx.cs" Inherits="EDI_EdiHrdList" %>

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
        <table cellspacing="0" cellpadding="0" bgcolor="#f4f4f4" border="0" style="width: 1040px;
            border: 1px solid white;">
            <tr>
                <td>
                    <asp:RadioButton ID="rbNormal" runat="server" GroupName="gpType" Checked="true" Text="Can Import"
                        AutoPostBack="True" OnCheckedChanged="rbNormal_CheckedChanged" />
<%--                    <asp:RadioButton ID="rbError" runat="server" GroupName="gpType" Text="Can Not Import"
                        AutoPostBack="True" OnCheckedChanged="rbError_CheckedChanged" />&nbsp;
                    <asp:RadioButton ID="rbPartError" runat="server" GroupName="gpType" Text="Should Input"
                        AutoPostBack="True" OnCheckedChanged="rbPartError_CheckedChanged" />--%>
                    <asp:RadioButton ID="rbFinish" runat="server" GroupName="gpType" Text="Imported"
                        AutoPostBack="True" OnCheckedChanged="rbFinish_CheckedChanged" />
<%--                    <asp:RadioButton ID="rbRejected" runat="server" GroupName="gpType" Text="Rejected"
                        AutoPostBack="True" OnCheckedChanged="rbRejected_CheckedChanged" />--%>
                    &nbsp; 订单号:<asp:TextBox ID="txtOrder" runat="server" CssClass="smalltextbox" Width="58px"> </asp:TextBox>
                    导入日期:<asp:TextBox ID="txtDate" runat="server" CssClass="smalltextbox Date" Width="75px"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnQuery" runat="server" Text="查询" CssClass="SmallButton3" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
<%--                    <asp:Button ID="btnImport" runat="server" Text="Import to Qad" OnClick="btnImport_Click"
                        OnClientClick="oneclick();" CssClass="SmallButton3" Width="86px" />--%>
    <%--                &nbsp;&nbsp;&nbsp;--%>
                    <asp:Button ID="btnExportExcel" runat="server" Text="Export Excel" OnClick="btnExportExcel_Click"
                        CssClass="SmallButton3" Width="70" />
<%--                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="更新" CssClass="SmallButton2" OnClick="Button1_Click"
                        Width="32px" />--%>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvlist" name="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            PageSize="17" Width="1200px" OnRowDataBound="gvlist_RowDataBound" OnPageIndexChanging="gvlist_PageIndexChanging"
            
            DataKeyNames="id,domain,notNeeded,detError,detNoSite,inBigOrder,fob,poNbr,isEdiConfig" OnRowCommand="gvlist_RowCommand"
            CssClass="GridViewStyle GridViewRebuild">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblPoId" runat="server" Text='<%# Bind("id")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PO Number" SortExpression="poNbr">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("poNbr")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tcp So Number" SortExpression="fob">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("fob")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Due Date" SortExpression="dueDate">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("dueDate")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ship Via" SortExpression="shipVia">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="lblPoNbr" runat="server" Text='<%# Bind("shipVia")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ship To" SortExpression="rmk">
                    <HeaderStyle Width="240px" HorizontalAlign="Center" />
                    <ItemStyle Width="240px" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("rmk")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               

<%--                <asp:TemplateField HeaderText="Import">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="linkImport" runat="server" CommandName="need">需要</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Underline="True" />
                </asp:TemplateField>--%>
                <asp:BoundField DataField="PoRecDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="接收日期"
                    HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
<%--                <asp:TemplateField>
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <input id="chkImport" type="checkbox" name="chkImport" runat="server" value='<%#Eval("id") %>' />
                        <input id="hidToPlan" runat="server" style="width: 38px" type="hidden" value='<%# Bind("toPlan")%>' />
                        <input id="hidShouldToPlan" runat="server" style="width: 38px" type="hidden" value='<%# Bind("shouldToPlan")%>' />
                    </ItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" />
                    </HeaderTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="qad_dueDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="截止日期"
                    HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="qad_perfDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="承诺日期"
                    HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
<%--                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkPlan" runat="server" CommandName="plan" Font-Bold="False"
                            Font-Underline="True" ForeColor="Black" Text="To Plan"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>--%>
<%--                <asp:TemplateField HeaderText="大订单">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkBigOrder" runat="server" CommandName="bigorder" Font-Bold="False"
                            Font-Underline="True" ForeColor="Black" Text="显示"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>--%>
<%--                <asp:TemplateField HeaderText="Cancel">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="linkCancel" runat="server" CommandName="CancelDEI">取消</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Underline="True" />
                </asp:TemplateField>--%>
                 <asp:TemplateField HeaderText="EDI Error Info" SortExpression="errMsg">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle Width="200px" HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblErrorMsg" runat="server" Text='<%# Bind("errMsg")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EDI Type" SortExpression="fileType">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("fileType")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Domain">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("mpo_domain") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ExtDoc"  HeaderText="ExtDoc" HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="Allport"  HeaderText="Allport" HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ad_line1"  HeaderText="地址1" HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ad_line2"  HeaderText="地址2" HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ad_line3"  HeaderText="地址3" HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ad_city"  HeaderText="城市" HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ad_state"  HeaderText="州" HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="ad_zip"  HeaderText="邮编" HtmlEncode="False">
                    <HeaderStyle Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:HiddenField ID="HiddenField1" runat="server" EnableViewState="true" Value="" />
        </form>
    </div>
    <script language="javascript" type="text/javascript">
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
