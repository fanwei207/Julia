<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EDIPOExceptionList.aspx.cs" Inherits="EDI_EDIPOExceptionList" %>

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
    <div>
        <table style="width:1300px">
            <tr>
                <td>
                    订单号：
                </td>
                <td>
                    <asp:TextBox ID="txtPoNbr" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    客户号：
                </td>
                <td>
                    <asp:TextBox ID="txtCustNo" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    客户名称：
                </td>
                <td>
                    <asp:TextBox ID="txtCustName" runat="server" Width="100px"></asp:TextBox>
                </td>
               <%-- <td>
                    地区：
                </td>
                <td>
                    <asp:DropDownList ID="ddlPland" runat="server" DataValueField="" DataTextField="">
                    </asp:DropDownList>
                </td>--%>
                <td>
                    负责人：
                </td>
                <td>
                    <asp:TextBox ID="txtOwnName" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td>
                    错误代码：
                </td>
                <td>
                     <asp:DropDownList ID="ddlExceptionCode" runat="server" DataValueField="code" DataTextField="DESCRIPTION" >
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSelect" runat="server" Width="80px" CssClass="SmallButton2" Text="查询" OnClick="btnSelect_Click" />
                </td>
                <td>
                    <asp:Button ID="btnExport" runat="server" Width="80px" CssClass="SmallButton2"  Text="导出" OnClick="btnExport_Click"/>
                </td>
            </tr>

        </table>
         <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            
           
            AllowPaging="False" PageSize="20">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="投诉单号" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="客户" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="原订单" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Date Code" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Due Date" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="问题描述" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="创建人" Width="150px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="创建日期" Width="150px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
               <asp:BoundField DataField="oredrNo" HeaderText="Order NO">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="orderDate" HeaderText="Order Date">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:BoundField DataField="ad_name" HeaderText="Customer Name">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                  <asp:BoundField DataField="ad_country" HeaderText="Area">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                  <asp:BoundField DataField="poline" HeaderText="Line">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:BoundField DataField="partNbr" HeaderText="Item">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:BoundField DataField="ordQty" HeaderText="QTY">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="price" HeaderText="unit Price">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>

            <asp:BoundField DataField="totalPrice" HeaderText="Total Cost">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                  <asp:BoundField DataField="reqDate" HeaderText="Order Requst Date">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>

                 <asp:BoundField DataField="ownerName" HeaderText="Owner">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:BoundField DataField="code" HeaderText="Reason Code">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>
                 <asp:BoundField DataField="starDate" HeaderText="Start">
                    <HeaderStyle Width="110px" HorizontalAlign="Left" Font-Bold="False" />
                    <ItemStyle Width="110px" HorizontalAlign="Left" />
                </asp:BoundField>

                  
              <%--  <asp:BoundField DataField="createName" HeaderText="创建人">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="createDate" HeaderText="创建日期">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" Font-Bold="False" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>--%>
               
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>
