<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pur_ResultReport.aspx.cs" Inherits="pur_ResultReport" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            height: 68px;
        }
    </style>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" style="width: 930px" class="main_top">
            <tr>
                <td width="60px">
                    <asp:Label ID = "lb_verdor" Text = "供应商" runat="server"></asp:Label>
                </td>
                <td Width="100px" >
                    <asp:TextBox ID="txt_vendor" runat="server" Width="80px" ></asp:TextBox>
                </td>
               <%-- <td Width="100px" >
                    <asp:DropDownList ID = "ddl_vendor" runat ="server" Width="120px" AutoPostBack="True" DataTextField="ad_name" DataValueField="ad_addr"
                        onselectedindexchanged="ddl_vendor_SelectedIndexChanged">
                        <asp:ListItem Value = "0" Selected = "True">--请选择--</asp:ListItem>
                        <asp:ListItem Value = "1">品质考评</asp:ListItem>
                        <asp:ListItem Value = "2">采购考评</asp:ListItem>
                        <asp:ListItem Value = "3">技术考评</asp:ListItem>
                    </asp:DropDownList>
                </td>--%>
                <td width="40px">
                    <asp:Label ID="Label2" Text="年份" runat="server"></asp:Label>
                </td>
                <td width="60px">
                    <asp:DropDownList ID="ddl_year" runat="server">
                        <asp:ListItem Value=1>2015</asp:ListItem>
                        <asp:ListItem Value=2>2016</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="40px">
                    <asp:Label ID="lb_month" Text="月份" runat="server"></asp:Label>
                </td>
                <td width="60px">
                    <asp:DropDownList ID="ddl_mounth" runat="server">
                        <asp:ListItem Value=1>1</asp:ListItem>
                        <asp:ListItem Value=2>2</asp:ListItem>
                        <asp:ListItem Value=3>3</asp:ListItem>
                        <asp:ListItem Value=4>4</asp:ListItem>
                        <asp:ListItem Value=5>5</asp:ListItem>
                        <asp:ListItem Value=6>6</asp:ListItem>
                        <asp:ListItem Value=7>7</asp:ListItem>
                        <asp:ListItem Value=8>8</asp:ListItem>
                        <asp:ListItem Value=9>9</asp:ListItem>
                        <asp:ListItem Value=10>10</asp:ListItem>
                        <asp:ListItem Value=11>11</asp:ListItem>
                        <asp:ListItem Value=12>12</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="80px">
                    <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" OnClick="btnQuery_Click"
                        TabIndex="0" Text="查询" />
                </td>
                <td width="80px">
                    <asp:Button ID="btn_Export" runat="server" CssClass="SmallButton3" OnClick="btn_Export_Click"
                        TabIndex="0" Text="导出" />
                </td>
                <td>
                </td>
            </tr>
        </table>
        <table style="width: 830px" >
            <div id="div_add" runat="server" visible ="false" width="830px">
                <tr>

                </tr>
            </div>
        </table>
        <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="1230px" OnRowCancelingEdit="gv_RowCancelingEdit" OnRowDeleting="gv_RowDeleting"
            OnRowEditing="gv_RowEditing" OnRowUpdating="gv_RowUpdating" OnRowDataBound="gv_RowDataBound"
            PageSize="20" DataKeyNames="pur_id" AllowPaging="True" 
            OnPageIndexChanging="gv_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField HeaderText="序号" DataField="id" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商代码" DataField="prh_vend" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="160px" />
                    <ItemStyle HorizontalAlign="Center" Width="160px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="供应商名称" DataField="ad_name" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="160px" />
                    <ItemStyle HorizontalAlign="Center" Width="160px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="年份" DataField="pur_year" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="月份" DataField="pur_mounth" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="批次合格率" DataField="pur_lotPassRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="反馈及时性" DataField="pur_fackBackRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="退件次数" DataField="pur_8dReturnRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="异常重复发生次数" DataField="pur_8dUnusualRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="客诉次数" DataField="pur_8dCustComplainRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="产线抱怨次数" DataField="pur_8dLineComplainRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="交货及时率" DataField="pur_deliveryRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="订单确认率" DataField="pur_orderCheckRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="是否寄售" DataField="pur_ConsignRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="账期" DataField="pur_finRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="报价及时性" DataField="pur_overRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="价格水平" DataField="pur_PriceRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="技术资料的提供" DataField="pur_docRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="样品交付合格率" DataField="pur_samplePassRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="样品交付及时率" DataField="pur_sampleTimeRate" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="批次合格率得分" DataField="pur_lotPassScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="反馈及时性得分" DataField="pur_fackBackScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="退件次数得分" DataField="pur_8dReturnScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="异常重复发生次数得分" DataField="pur_8dUnusualScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="客诉次数得分" DataField="pur_8dCustComplainScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="产线抱怨次数得分" DataField="pur_8dLineComplainScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="交货及时得分" DataField="pur_deliveryScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="订单确认得分" DataField="pur_orderCheckScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="是否寄售得分" DataField="pur_ConsignScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="账期得分" DataField="pur_finScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="报价及时性得分" DataField="pur_overScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="价格高低得分" DataField="pur_PriceScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="技术资料的提供得分" DataField="pur_docScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="样品交付合格得分" DataField="pur_samplePassScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="样品交付及时得分" DataField="pur_sampleTimeScore" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="评定总计得分" DataField="ValueTotal" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="评定等级" DataField="ValueGrade" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="备注" DataField="pur_Remark" ReadOnly="True">
                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>

            </Columns>
            <PagerStyle CssClass="GridViewPagerStyle" />
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
   <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
