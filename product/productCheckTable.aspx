<%@ Page Language="C#" AutoEventWireup="true" CodeFile="productCheckTable.aspx.cs" Inherits="product_productCheckTable" %>

<!DOCTYPE html>

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
    <form id="form1" runat="server">
        <div align="center">
            <div>
                QAD：&nbsp;&nbsp;<asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox5" Width="100"></asp:TextBox>&nbsp;&nbsp;
                域： &nbsp;&nbsp;<asp:DropDownList ID="ddlDomain" runat="server">
                    <asp:ListItem Text="all"></asp:ListItem>
                    <asp:ListItem Text="SZX" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="ZQL"></asp:ListItem>
                    <asp:ListItem Text="ZQZ"></asp:ListItem>
                    <asp:ListItem Text="YQL"></asp:ListItem>
                    <asp:ListItem Text="HQL"></asp:ListItem>
                </asp:DropDownList>&nbsp;&nbsp;
                成品检验日期：
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="Date" Width="80px"></asp:TextBox>&nbsp;&nbsp;--&nbsp;&nbsp;
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="Date" Width="80px"></asp:TextBox>
                &nbsp;&nbsp; &nbsp;&nbsp; 
                 <asp:CheckBox ID="chkDiff" Text="差异" runat="server" AutoPostBack="true" OnCheckedChanged="chkDiff_CheckedChanged" Checked="true" />&nbsp;&nbsp;
                &nbsp;&nbsp; &nbsp;&nbsp; 
                  <asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnSelect_Click" />
                &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
                  <asp:Button ID="btnExport" runat="server" Text="导出报表" CssClass="SmallButton2" OnClick="btnExport_Click" />
            </div>
            <div>
                <asp:GridView ID="gvInfo" AutoGenerateColumns="false" PageSize="30" AllowPaging="true"
                    runat="server" CssClass="GridViewStyle GridViewRebuild" Width="2000px"
                    DataKeyNames="isDiff,errorQuality,errorVolume,ERROR"
                    OnRowDataBound="gvInfo_RowDataBound" OnPageIndexChanging="gvInfo_PageIndexChanging">
                    <RowStyle CssClass="GridViewRowStyle" />
                    <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EmptyDataTemplate>
                        <asp:Table ID="Table1" Width="600px" CellPadding="-1" CellSpacing="0" runat="server"
                            CssClass="GridViewHeaderStyle" GridLines="Vertical">
                            <asp:TableRow>
                                <asp:TableCell Text="无数据" Width="600px" HorizontalAlign="center"></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="QAD" DataField="part" Visible="true">
                            <HeaderStyle Width="80px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="成品检验日期" DataField="prdQCDate" Visible="true" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="工单号" DataField="woNbr" Visible="true">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="域" DataField="DOMAIN" Visible="true">
                            <HeaderStyle Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="误差范围（%）" DataField="ERROR" Visible="true">
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        
                        <asp:BoundField HeaderText="标准质量（kg）" DataField="standardQuality" Visible="true">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="实际质量（kg）" DataField="ActualQuality" Visible="true">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="质量偏差（%）" DataField="errorQuality" Visible="true">
                            <HeaderStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="标准体积（m³）" DataField="standardVolume" Visible="true">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="实际体积（m³）" DataField="ActualVolume" Visible="true">
                            <HeaderStyle Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="体积偏差（%）" DataField="errorVolume" Visible="true">
                            <HeaderStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="标准长（cm）" DataField="standardLong" Visible="true">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="实际平均长（cm）" DataField="ActualLong" Visible="true">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="标准宽（cm）" DataField="standardWide" Visible="true">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="实际平均宽（cm）" DataField="ActualWide" Visible="true">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="标准高（cm）" DataField="standardHigh" Visible="true">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="实际平均高（cm）" DataField="ActualHigh" Visible="true">
                            <HeaderStyle Width="100px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </form>
</body>
</html>
