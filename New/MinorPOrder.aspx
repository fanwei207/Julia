<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MinorPOrder.aspx.cs" Inherits="new_MinorPOrder" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .hidden
        {
            display: none;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table id="Table1" cellpadd ing="0" cellspacing="0" width="500px" bordercolor="Black"
            gridlines="Both" runat="server">
            <tr>
                <td align="left">
                    <asp:Label ID="lblInfor" runat="server" Width="500px" Text="申请信息 ：" Font-Size="Medium"
                        Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblApid" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbHeader1" cellpadd ing="0" cellspacing="0" width="500px" bordercolor="Black"
            gridlines="Both" runat="server">
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 部门：&nbsp;<asp:DropDownList ID="dropDept"
                        runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;零件分类：&nbsp;<asp:DropDownList ID="dropType" runat="server"
                        Width="100px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;数量：&nbsp;<asp:TextBox ID="txtQuantity"
                        runat="server" Width="100px" CssClass="smalltextbox Numeric"></asp:TextBox>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;单价：&nbsp;<asp:TextBox
                        ID="txtPrice" runat="server" Width="100px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    零件描述：&nbsp;<asp:TextBox ID="txtPart" runat="server" Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;供应商：&nbsp;<asp:TextBox ID="txtSP" runat="server" Width="400px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table id="tbHeader2" cellpadd ing="0" cellspacing="0" width="500px" bordercolor="Black"
            gridlines="Both" runat="server" visible="false" style="font-size: 10pt">
            <tr style="height: 23px">
                <td style="height: 23px" colspan="2">
                    &nbsp;&nbsp;&nbsp; 申请人：&nbsp;<asp:Label ID="lblApper" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td style="height: 23px">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 部门：&nbsp;<asp:Label ID="lblDept" runat="server"
                        Width="100px"></asp:Label>
                </td>
                <td style="height: 23px">
                    &nbsp;&nbsp;&nbsp;&nbsp;零件分类：&nbsp;<asp:Label ID="lblType" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;数量：&nbsp;<asp:Label ID="lblQuantity"
                        runat="server" Width="100px"></asp:Label>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;单价：&nbsp;<asp:Label
                        ID="lblprice" runat="server" Width="100px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td colspan="2">
                    零件描述：&nbsp;<asp:Label ID="lblPart" runat="server" Width="400px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;供应商：&nbsp;<asp:Label ID="lblSP" runat="server" Width="400px"></asp:Label>
                </td>
            </tr>
            <tr style="height: 23px">
                <td colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;总价：&nbsp;<asp:Label ID="lbltotal" runat="server"
                        Width="100px"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:GridView ID="gvAC" AllowSorting="True" AutoGenerateColumns="False" CssClass="GridViewStyle"
            runat="server" Width="460px" DataSourceID="obdsAp">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <Columns>
                <asp:BoundField HeaderText="审批人" DataField="userName" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="所属部门" DataField="dname" ReadOnly="True">
                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField HeaderText="审批日期" DataField="Appdate" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False" ReadOnly="True">
                    <ItemStyle Width="80px" HorizontalAlign="center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="备注" DataField="comments">
                    <ItemStyle Width="150px" HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Table ID="table" runat="server" CellPadding="-1" BorderWidth="1" CellSpacing="0"
                    CssClass="GridViewHeaderStyle" GridLines="Both">
                    <asp:TableRow>
                        <asp:TableCell Text="审批人" Width="80px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell Text="所属部门" Width="120px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                        <asp:TableCell Text="审批日期" Width="80px" Font-Bold="true" HorizontalAlign="Center"
                            DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False"> </asp:TableCell>
                        <asp:TableCell Text="备注" Width="150px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
        </asp:GridView>
        <br />
        <br />
        <b>___________________________________________________________________</b>
        <br />
        <table id="tbHeader3" cellpadd ing="0" cellspacing="0" width="500px" bordercolor="Black"
            gridlines="Both" runat="server" visible="true">
            <tr>
                <td>
                    提交给 ：&nbsp;<asp:TextBox ID="txtApplication" Width="200px" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtuid" runat="server" Style="display: none"></asp:TextBox>
                </td>
                <td style="width: 102px">
                    <asp:Button ID="btnChuser" runat="server" CssClass="SmallButton2" Text="选择" OnClick="btnChuser_Click" />
                </td>
            </tr>
            <tr visible="false">
                <td>
                    抄送给 ：&nbsp;<asp:TextBox ID="TextBox1" Width="200px" runat="server"></asp:TextBox>
                </td>
                <td style="width: 102px">
                    <asp:Button ID="btnChother" runat="server" CssClass="SmallButton2" Text="选择" />
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="bottom">
                    备注 ：&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    <asp:TextBox ID="txtComments" Height="100px" Width="450" TextMode="MultiLine" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    附件上传 ：&nbsp;
                    <input type="file" id="filename" runat="server" style="width: 300px; height: 22px"
                        size="45" name="filename" class="smallbutton2">
                </td>
                <td style="width: 102px">
                    <asp:Button ID="btnAhSave" runat="server" Text="上传" CssClass="SmallButton2" OnClick="btnAhSave_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="gvAttached" AllowSorting="True" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        runat="server" Width="430px" DataSourceID="obdsAtt" DataKeyNames="AttID" OnRowCommand="gvAttached_RowCommand">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField HeaderText="附件名" DataField="attname" ReadOnly="True">
                                <ItemStyle Width="170px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="上传人" DataField="attuser" ReadOnly="True">
                                <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="上传时间" DataField="attdate" DataFormatString="{0:yyyy-MM-dd}"
                                HtmlEncode="False">
                                <ItemStyle Width="80px" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:ButtonField Text="<u>查看</u>" ItemStyle-ForeColor="black" ItemStyle-Width="50px"
                                ItemStyle-HorizontalAlign="Center" CommandName="1" />
                            <asp:ButtonField Text="<div onclick=&quot;JavaScript:return confirm('你确认要删除这条申请吗?')&quot;><u>删除</u><div/>"
                                ItemStyle-ForeColor="black" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                CommandName="2" />
                            <asp:BoundField DataField="AttUserID" ReadOnly="True">
                                <ItemStyle CssClass="hidden" />
                                <HeaderStyle CssClass="hidden" />
                                <FooterStyle CssClass="hidden" />
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Table ID="table" runat="server" CellPadding="-1" BorderWidth="1" CellSpacing="0"
                                GridLines="Both">
                                <asp:TableRow BackColor="#006699" ForeColor="White">
                                    <asp:TableCell Text="附件名" Width="170px" Font-Bold="true" HorizontalAlign="Center"></asp:TableCell>
                                    <asp:TableCell Text="上传人" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                                    <asp:TableCell Text="上传时间" Width="80px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                                    <asp:TableCell Width="50px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                                    <asp:TableCell Width="50px" Font-Bold="true" HorizontalAlign="Center"> </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    邮箱地址：<asp:TextBox ID="txtEmail" runat="server" Width="400"> </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <font style="color: red">请在上面邮件地址栏中正确填写你本人的邮件地址,否则对方无法收到你的申请.</font>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Button ID="btnSave" runat="server" Text="提交" CssClass="SmallButton2" OnClick="btnSave_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="SmallButton2" OnClick="btnBack_Click" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnFinish" runat="server" Text="审批结束" CssClass="SmallButton2" OnClick="btnFinish_Click" />
        <asp:ObjectDataSource ID="obdsAtt" runat="server" SelectMethod="AttachedSelect" TypeName="MinorP.MinorPurchase">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblApid" DefaultValue="0" Name="intAid" PropertyName="Text"
                    Type="Int32" />
                <asp:SessionParameter Name="intUid" SessionField="uid" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="obdsAp" runat="server" SelectMethod="ApplySelect" TypeName="MinorP.MinorPurchase">
            <SelectParameters>
                <asp:ControlParameter ControlID="lblApid" Name="intAid" PropertyName="Text" Type="Int32" />
                <asp:SessionParameter Name="intPlant" SessionField="plantcode" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
