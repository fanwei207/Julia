<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SID_PackingDoc.aspx.cs"
    Inherits="SID_PackingDoc" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" width="960px" border="0">
            <tr>
                <td>
                    <asp:Label ID="lblShipNo" runat="server" Width="55px" CssClass="LabelRight" Text="货 运 单:"
                        Font-Bold="false"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtShipNo" runat="server" Width="260px" TabIndex="1"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton2" TabIndex="2" Text="查询"
                        Width="50px" OnClick="btnQuery_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton2" TabIndex="2" Text="返回"
                        Width="50px" OnClick="btn_back_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lb_boxno" runat="server" Width="55px" CssClass="LabelRight" Text="箱&nbsp;&nbsp; &nbsp;&nbsp; 号:"
                        Font-Bold="false"></asp:Label>
                     &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txt_boxno" runat="server" Width="100px"></asp:TextBox>
                    <asp:Label ID="lb_bl" runat="server" Width="55px" CssClass="LabelRight" Text="提&nbsp;&nbsp;单 &nbsp;&nbsp;号:"
                        Font-Bold="false"></asp:Label>
                     &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txt_bl" runat="server" Width="100px"></asp:TextBox>
                    <asp:Label ID="lb_lcno" runat="server" Width="55px" CssClass="LabelRight" Text="L/C&nbsp;&nbsp;&nbsp;&nbsp;NO: "
                        Font-Bold="false"></asp:Label>
                     &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txt_lcno" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td>
                    <asp:Label ID="Label1" runat="server" Width="55px" CssClass="LabelRight" Text="SHIP&nbsp; TO:"
                        Font-Bold="false"></asp:Label>
                     &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txt_shipto" runat="server" Width="445px"></asp:TextBox>
                     &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_save" runat="server" CssClass="SmallButton2" TabIndex="2" Text="保存" Visible="false" 
                        Width="50px" OnClick="btn_save_Click" />
                </td>               
            </tr>
                        <tr>
                <td align="left">
                     <input id="File1" runat="server" style="width: 520px" type="file" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lb_doctype" runat="server">上传文件类型:</asp:Label>
                    &nbsp;&nbsp;&nbsp;
                    <asp:RadioButtonList ID="doctype" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" RepeatColumns="3" Width="100px">
                        <asp:ListItem Value="提单" Selected="True">提单</asp:ListItem>
                        <asp:ListItem Value="测试">测试</asp:ListItem>
                    </asp:RadioButtonList>

                    <asp:Button ID="btnUpload" runat="server" CssClass="SmallButton2" Text="上传" Width="48px"
                        OnClick="btnUpload_Click" />
                </td>
            </tr>
            <tr style="vertical-align: top; height: 20px;">
                <td colspan="2" style="width: 928px; text-align: center;">
                    <asp:GridView ID="dg" runat="server" Style="vertical-align: top" CssClass="GridViewStyle"
                        AllowPaging="True" AutoGenerateColumns="False" PageSize="14" OnRowDataBound="dg_RowDataBound"
                        OnPageIndexChanging="dg_PageIndexChanging" 
                        DataKeyNames="sid_doc_id,sid_doc_name,sid_doc_path" OnRowDeleting="dg_RowDeleting"
                        OnRowCommand="dg_RowCommand">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="文件名" DataField="sid_doc_name">
                                <HeaderStyle Width="480px" />
                                <ItemStyle HorizontalAlign="Left" Width="480px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="文件类型" DataField="sid_doc_type">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="上传人" DataField="sid_doc_crt_user">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataFormatString="{0:yyyy-MM-dd}" HeaderText="上传日期" HtmlEncode="False"
                                DataField="sid_doc_crt_date">
                                <HeaderStyle Width="80px" />
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True" DeleteText="删除">
                                <HeaderStyle Width="60px" />
                                <ItemStyle HorizontalAlign="Center" Width="60px" />
                                <ControlStyle ForeColor="Black" Font-Underline="True" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="下载">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="DownLoad" Font-Underline="True"
                                        ForeColor="Black">下载</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>


        </form>
    </div>
    <script>
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
