<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pc_newPriceCheckBetweenQADAnd100.aspx.cs" Inherits="price_pc_newPriceCheckBetweenQADAnd100" %>

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
        <div >
            <div align="left">
                QAD：&nbsp;&nbsp;<asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox5" Width="100"></asp:TextBox>&nbsp;&nbsp;
            供应商：&nbsp;&nbsp;<asp:TextBox ID="txtVender" runat="server" CssClass="SmallTextBox5" Width="80"></asp:TextBox>&nbsp;&nbsp;
            供应商名称：&nbsp;&nbsp;<asp:TextBox ID="txtVenderName" runat="server" CssClass="SmallTextBox5" Width="100"></asp:TextBox>&nbsp;&nbsp;
            域： &nbsp;&nbsp;<asp:DropDownList ID="ddlDomain" runat="server">
                <asp:ListItem Text="all"></asp:ListItem>
                <asp:ListItem Text="SZX" Selected="true"></asp:ListItem>
                <asp:ListItem Text="ZQL"></asp:ListItem>
                <asp:ListItem Text="ZQZ"></asp:ListItem>
                <asp:ListItem Text="YQL"></asp:ListItem>
                <asp:ListItem Text="HQL"></asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp;
             类型：&nbsp;&nbsp;<asp:DropDownList ID="ddlType" runat="server">
             </asp:DropDownList>&nbsp;&nbsp;
          差异：&nbsp;&nbsp;
            <asp:DropDownList ID="ddlDiff" runat="server">
                <asp:ListItem Text="全部" Value="0"></asp:ListItem>
                <asp:ListItem Text="有异常" Value="1" Selected="True" ></asp:ListItem>
                <asp:ListItem Text="主键异常" Value="2"></asp:ListItem>
                <asp:ListItem Text="价格异常" Value="3"></asp:ListItem>
                <asp:ListItem Text="单位异常" Value="4"></asp:ListItem>
                <asp:ListItem Text="币种异常" Value="5" ></asp:ListItem>
                <asp:ListItem Text="折扣表异常" Value="6"></asp:ListItem>
                <asp:ListItem Text="结束时间异常" Value="7"></asp:ListItem>
                <asp:ListItem Text="无异常" Value="8"></asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp;
             <asp:Button ID="btnSelect" runat="server" Text="查询" CssClass="SmallButton2" OnClick="btnSelect_Click" />
                &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
            <asp:Button ID="btnExport" runat="server" Text="导出报表" CssClass="SmallButton2" OnClick="btnExport_Click" />
            </div>
            <div align="left">
                图例：&nbsp;&nbsp;   
             <asp:Label ID="lbPrimaryKey" runat ="server" BackColor="Red" Width ="15px">&nbsp;&nbsp;  </asp:Label>：主键异常 &nbsp;&nbsp; 
                 <asp:Label ID="lbUm" runat ="server" BackColor="LightGreen" Width ="15px"> &nbsp;&nbsp; </asp:Label>：单位异常 &nbsp;&nbsp; 
                 <asp:Label ID="lbCurr" runat ="server" BackColor="LightBlue" Width ="15px"> &nbsp;&nbsp; </asp:Label>：货币异常 &nbsp;&nbsp; 
                 <asp:Label ID="lbPrice" runat ="server" BackColor="Yellow" Width ="15px">&nbsp;&nbsp;  </asp:Label>：价格异常 &nbsp;&nbsp; 
                 <asp:Label ID="lbAmt" runat ="server" BackColor="Orange" Width ="15px">&nbsp;&nbsp;  </asp:Label>：折扣表异常 &nbsp;&nbsp; 
                 <asp:Label ID="lbExpire" runat ="server" BackColor="Tomato" Width ="15px">&nbsp;&nbsp;  </asp:Label>：终止时间异常 &nbsp;&nbsp; 
            </div><%--GridViewRebuild--%>
            <div>
                <asp:GridView ID="gvInfo" AutoGenerateColumns="false" PageSize="30" AllowPaging="true"
                    runat="server" CssClass="GridViewStyle GridViewRebuild"
                    DataKeyNames="id,part,list,domain,startDate,t_pc_expire,q_pc_expire,t_pc_um ,q_pc_um,t_pc_curr ,q_pc_curr
                     , t_pc_price , q_pc_price  ,price1,priceDate,remark, t_pc_amt , q_pc_amt
                     ,isDiffPrimaryKey,isDiffUM,isDiffCurr,isDiffPrice,isDiffAmt,isDiffExpire,ad_name"
                     OnPageIndexChanging="gvInfo_PageIndexChanging" OnRowDataBound="gvInfo_RowDataBound" Width="3000px">
                    <RowStyle CssClass="GridViewRowStyle" />
                    <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                    <FooterStyle CssClass="GridViewFooterStyle" />
                    <PagerStyle CssClass="GridViewPagerStyle" />
                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                    <EmptyDataTemplate>
                        <asp:Table ID="Table1" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                            CssClass="GridViewHeaderStyle" GridLines="Vertical">
                            <asp:TableRow>
                                <asp:TableCell Text="无数据" Width="600px" HorizontalAlign="center"></asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="QAD" DataField="part" Visible="true"><%--0--%>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width ="100px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="供应商" DataField="list" Visible="true"><%--1--%>
                            <HeaderStyle Width="80px" />
                            <ItemStyle Width ="80px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="供应商名称" DataField="ad_name" Visible="true"><%--2--%>
                            <HeaderStyle Width="200px" />
                            <ItemStyle Width ="200px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="域" DataField="domain" Visible="true"><%--3--%>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width ="50px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="起始时间" DataField="startDate" Visible="true"><%--4--%>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width ="100px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="100终止时间" DataField="t_pc_expire" Visible="true"><%--5--%>
                            <HeaderStyle Width="100px" /><ItemStyle Width ="100px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="QAD终止时间" DataField="q_pc_expire" Visible="true"><%--6--%>
                            <HeaderStyle Width="100px" /><ItemStyle Width ="100px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="100价格" DataField="t_pc_price" Visible="true"><%--7--%>
                            <HeaderStyle Width="100px" /><ItemStyle Width ="100px " />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                          <asp:BoundField HeaderText="QAD价格" DataField="q_pc_price" Visible="true"><%--8--%>
                            <HeaderStyle Width="100px" /><ItemStyle Width ="100px " />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="100折扣表" DataField="t_pc_amt" Visible="true"><%--9--%>
                            <HeaderStyle Width="150px" /><ItemStyle Width ="150px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="QAD折扣表" DataField="q_pc_amt" Visible="true"><%--10--%>
                            <HeaderStyle Width="150px" /><ItemStyle Width ="150px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="100单位" DataField="t_pc_um" Visible="true"><%--11--%>
                            <HeaderStyle Width="50px" /><ItemStyle Width ="50px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="QAD单位" DataField="q_pc_um" Visible="true"><%--12--%>
                            <HeaderStyle Width="50px" /><ItemStyle Width ="50px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="100币种" DataField="t_pc_curr" Visible="true"><%--13--%>
                            <HeaderStyle Width="50px" /><ItemStyle Width ="50px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="QAD币种" DataField="q_pc_curr" Visible="true"><%--14--%>
                            <HeaderStyle Width="50px" /><ItemStyle Width ="50px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="生成时间" DataField="priceDate" Visible="true"><%--15--%>
                            <HeaderStyle Width="100px" /><ItemStyle Width ="100px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="100系统记录含税价" DataField="price1" Visible="true"><%--16--%>
                            <HeaderStyle Width="100px" /><ItemStyle Width ="100px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="备注" DataField="remark" Visible="true"><%--17--%>
                            <HeaderStyle Width="250px" /><ItemStyle Width ="250px " />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
