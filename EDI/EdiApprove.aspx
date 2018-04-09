<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EdiApprove.aspx.cs" Inherits="EdiApprove" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("input[type='checkbox'][id='chkAll']:eq(1)").remove();
            $("#chkAll").click(function () {            
                $("#gvShip input[type='checkbox'][id$='chk'][disabled!='disabled']").prop("checked",$(this).prop("checked"))
            })
        })
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table id="table1" cellspacing="0" cellpadding="0" width="880px">
            <tr>
                <td width="800">
                    <%--  系统单号<asp:TextBox ID="txtPK" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                        &nbsp;&nbsp;--%>订单号:
                    <asp:TextBox ID="txtnbr" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    订单行:
                    <asp:TextBox ID="txtpolin" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                    &nbsp;&nbsp;
                    客户代码:<asp:TextBox ID="txt_cuscode" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" Text="查询" Width="40"
                    OnClick="btnSearch_Click" />
                    <asp:Button ID="btnRestart" runat="server" CssClass="SmallButton3" Text="重启节点" Width="50"
                    OnClick="btnRestart_Click" />
                    <asp:Button ID="btnRestartAll" runat="server" CssClass="SmallButton3" Text="重启流程" Width="50"
                    OnClick="btnRestartAll_Click" />
                    <%-- 出运日期<asp:TextBox ID="txtdate1" runat="server" CssClass="SmallTextBox  Date" Width="100px"></asp:TextBox>-
                         <asp:TextBox ID="txtdate2" runat="server" CssClass="SmallTextBox  Date" Width="100px"></asp:TextBox>--%>
                </td>
                <td>

                </td>
                <td>
                    &nbsp;
                </td>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    QAD:
                    <asp:TextBox ID="txt_part" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                    &nbsp;&nbsp;     
                    客户物料号:<asp:TextBox ID="txt_cuspart" runat="server" CssClass="SmallTextBox" Width="100px"></asp:TextBox>
                    &nbsp;&nbsp;     
                    节点：<asp:DropDownList ID="dropNode" runat="server" DataTextField="Node_Name" 
                        DataValueField="Node_Id">
                    </asp:DropDownList>
                </td>      
            </tr>
        </table>
        <asp:GridView ID="gvShip" runat="server" AllowPaging="True" PageSize="18" AutoGenerateColumns="False"
            DataKeyNames="ID,hrdid" OnRowCommand="gvShip_RowCommand" OnPageIndexChanging="gvShip_PageIndexChanging"
            OnRowDataBound="gvShip_RowDataBound" Width="930px" CssClass="GridViewStyle GridViewRebuild">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <input id="chkAll" type="checkbox">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk" runat="server" Enabled='<%# Eval("checkstatus") %>'/>
                    </ItemTemplate>
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="poNbr" HeaderText="订单号">
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="cusCode" HeaderText="客户代码">
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="poline" HeaderText="订单行">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField DataField="partNbr" HeaderText="客户物料号">
                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                    <HeaderStyle HorizontalAlign="Center" Width="120px" />
                </asp:BoundField>
                <asp:BoundField DataField="qadPart" HeaderText="QAD">
                    <ItemStyle HorizontalAlign="Center" Width="110px" />
                    <HeaderStyle HorizontalAlign="Center" Width="110px" />
                </asp:BoundField>
                <asp:BoundField DataField="ordQty" HeaderText="订单数量">
                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="price" HeaderText="价格">
                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="reqDate" HeaderText="需求日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="shipto" HeaderText="发往">
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                </asp:BoundField>
                <asp:BoundField DataField="shipVia" HeaderText="运输方式">
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="domain" HeaderText="域">
                    <ItemStyle HorizontalAlign="Left" Width="40px" />
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <%--<asp:TemplateField HeaderText="重启">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" CausesValidation="False" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                            CommandName="Confirm3" Enabled='<%# Eval("checkstatus") %>' Text='<%# Eval("checkstatus1") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Size="12px" Font-Bold="False" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
