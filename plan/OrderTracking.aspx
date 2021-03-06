﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderTracking.aspx.cs" Inherits="plan_OrderTracking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        <table border="0" cellpadding="0" cellspacing="0" style=" width:1500px;">
            <tr>
                <td align="right" colspan="1" style="height: 27px">
                    <asp:DropDownList ID="dropType" runat="server" Width="50px">
                        <asp:ListItem Text="--" Value=""></asp:ListItem>
                        <asp:ListItem Text="CFL" Value="CFL"></asp:ListItem>
                        <asp:ListItem Text="LED" Value="LED"></asp:ListItem>
                        <asp:ListItem Text="卤素灯" Value="HAL"></asp:ListItem>
                        <asp:ListItem Text="其他" Value="OTH"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    Order
                </td>
                <td align="left" colspan="1" style="height: 27px;">
                    <asp:TextBox ID="txtPo1" runat="server" CssClass="SmallTextBox" Height="20px" Width="80px"></asp:TextBox>--<asp:TextBox
                        ID="txtPo2" runat="server" CssClass="SmallTextBox" Height="20px" Width="80px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="height: 27px">
                    Region
                </td>
                <td align="left" colspan="1" style=" height: 27px">
                    <asp:DropDownList ID="ddlRegion" runat="server" Width="110px" DataValueField="code_value"
                        DataTextField='code_cmmt'>
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style="width: 80px; height: 27px">
                    Customer Code
                </td>
                <td align="left" colspan="1" style="height: 27px">
                    <asp:TextBox ID="txtCustomer" runat="server" CssClass="SmallTextBox" Height="20px"
                        Width="100px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 60px; height: 27px">
                    Order Date
                </td>
                <td align="left" colspan="1" style="height: 27px">
                    <asp:TextBox ID="txtOrdDate1" runat="server" CssClass="SmallTextBox Date" Height="20px"
                        Width="80px"></asp:TextBox>--<asp:TextBox ID="txtOrdDate2" runat="server" CssClass="SmallTextBox Date"
                            Height="20px" Width="80px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="width: 50px; height: 27px">
                    Status
                </td>
                <td align="left" colspan="1" style="height: 27px">
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="110px">
                        <asp:ListItem Text="--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Not Load QAD" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Not Finish" Value="2" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Expired" Value="3"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="left" colspan="1" style="height: 27px">
                    Total<asp:TextBox ID="txtTotal" runat="server" CssClass="TextRight" Height="20px"
                        ReadOnly="true" Width="38px"></asp:TextBox>
                </td>
                <td align="right" style="width: 45px; height: 27px">
                    <asp:Button ID="btnSearch" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnSearch_Click" Text="Query" />
                </td>
                <td align="right" style="width: 45px; height: 27px">
                    <asp:Button ID="btnExcel" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnExcel_Click" Text="Export" />
                </td>
                <td align="right" style="width: 55px; height: 27px">
                    <asp:Button ID="btnExportWo" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnExportWo_Click" Text="Export(Wo)" Width="70px" />
                </td>
                <td align="right" style="width: 45px; height: 27px">
                    <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnRefresh_Click" Text="Refresh" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="que,planDate,poNbr,poLine,sod_ord_date" CssClass="GridViewStyle GridViewRebuild" OnPageIndexChanging="gvlist_PageIndexChanging"
            OnRowDataBound="gvlist_RowDataBound" OnRowCommand="gvlist_RowCommand" PageSize="20" Width="1730px">
            <FooterStyle CssClass="GridViewFooterStyle" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                    GridLines="Vertical" Width="1300px">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="center" Text="Order#" Width="140px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Order Date" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Customer Code" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Item" Width="200px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Order Question" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Load Qad Date" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Qad So#" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Line" Width="50px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="QAD Part" Width="100px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Order Qty" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Due Date" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Wo Qty" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Online Qty" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Ship Qty" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Inspection Date" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="Book Space Date" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="PCD" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="PCD创建人" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="PCD创建日期" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="价格" Width="80px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="分类" Width="60px"></asp:TableCell>
                        <asp:TableCell HorizontalAlign="center" Text="PCD备注" Width="150px"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="poNbr" HeaderText="Order#">
                    <HeaderStyle Width="140px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PoRecDate" HeaderText="Order Date" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="cusCode" HeaderText="Customer Code">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="poLine" HeaderText="Line">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="partNbr" HeaderText="Item">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
               <%-- <asp:HyperLinkField DataNavigateUrlFields="poNbr,poLine" DataNavigateUrlFormatString="/plan/soque_track.aspx?poNbr={0}&amp;line={1}"
                    HeaderText="Order Question" Text="Detail" Target="_blank">
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>--%>
                 <asp:HyperLinkField DataNavigateUrlFields="poNbr" DataNavigateUrlFormatString="/NWF/nwf_workflowInstanceResult.aspx?FlowId=8C930BDD-4C65-45E6-A9D7-3EDD873005A3&amp;keyWords={0}"
                    HeaderText="Order Approve" Text="Detail" Target="_blank">
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle Width="70px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="sod_ord_date" HeaderText="Load QAD Date" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="so_nbr,sod_line" DataNavigateUrlFormatString="/SID/SID_ShipEng.aspx?nbr={0}&amp;line={1}"
                    HeaderText="Qad So#" DataTextField="so_nbr" Target="_blank">
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="sod_part" HeaderText="QAD Part">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_qty_ord" DataFormatString="{0:F0}" HeaderText="Order Qty"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_due_date" HeaderText="Request Date" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:HyperLinkField DataNavigateUrlFields="so_nbr,sod_part,sod_line" DataNavigateUrlFormatString="/plan/WoTracking.aspx?soNbr={0}&amp;part={1}&amp;line={2}"
                    HeaderText="Wo Qty" DataTextField="wo_qty_ord" DataTextFormatString="{0:F0}"
                    Target="_blank">
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="wt_qty" DataFormatString="{0:F0}" HeaderText="Online Qty"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="entryDate" HeaderText="Warehousing Date" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_qty_ship" DataFormatString="{0:F0}" HeaderText="Ship Qty"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="SID_insp_date" HeaderText="Inspection Date" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SID_insp_matchdate" HeaderText="Ship Date" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
      <%--          <asp:HyperLinkField DataNavigateUrlFields="poNbr,poLine" DataNavigateUrlFormatString="/plan/PCD_ApplyReason.aspx?poNbr={0}&amp;poLine={1}"
                    HeaderText="PCD" DataTextField="planDate" DataTextFormatString="{0:MM/dd/yyyy}"
                    Target="_blank">
                    <ControlStyle Font-Underline="True" />
                    <HeaderStyle Width="80px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:HyperLinkField>--%>
                <asp:TemplateField HeaderText="PCD">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkPCD" runat="server" CommandName="PCD" Font-Bold="False" Font-Underline="True"
                                ForeColor="Black" Text='<%# Bind("planDate","{0:MM/dd/yyyy}") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="det_planName" HeaderText="PCD创建人" HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="det_planDate" HeaderText="PCD创建日期" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="det_price" HeaderText="价格">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="qadType" HeaderText="分类">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="det_planRmks" HeaderText="PCD备注">
                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
