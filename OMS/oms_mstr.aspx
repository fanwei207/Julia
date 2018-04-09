<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oms_mstr.aspx.cs" Inherits="IT_oms_mstr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">

        $(function () {

            var _index = $("#hidTabIndex").val();

            var $tabs = $("#divTabs").tabs({ active: _index });

            //$("#gvMessagereply").find(".GridViewHeaderStyle TH:eq(1)").css({ 'text-align': 'left', 'word-break': 'break-all', 'word-wrap': 'break-word' });
            //$("#gvMessagereply").find(".GridViewHeaderStyle").hide();

        })
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Cust Code:
        <asp:Label ID="lbCustCode" runat="server" Width="100px"></asp:Label>
        &nbsp;&nbsp;&nbsp; Cust Name:
        <asp:Label ID="lbCustName" runat="server" Width="300px"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" Text="Back" Width="68px"
            OnClick="btnBack_Click1" />
        <input id="hidTabIndex" type="hidden" value="0" runat="server" />
    </div>
    <div id="divTabs">
        <ul>
            <li><a href="#tabs-1">Factory Status</a></li>
            <li><a href="#tabs-2">Products</a></li>
            <li><a href="#tabs-3">Forecast</a></li>
            <li><a href="#tabs-4">Customer Orders Tracking</a></li>
            <li><a href="#tabs-5">Project Tracking</a></li>
        </ul>
        <div id="tabs-1">
            <table width="800px">
                <tr>
                    <td>
                        Importance:
                        <asp:DropDownList ID="ddlImpt" runat="server" Height="20px" Width="120px">
                            <asp:ListItem Value="-1">--All--</asp:ListItem>
                            <asp:ListItem Value="0">Normal</asp:ListItem>
                            <asp:ListItem Value="1">Emergency</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Category:&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlCategory" runat="server" Height="20px" Width="120px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Factory:&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlDomain" runat="server" Height="20px" Width="120px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10">
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp; Filename:
                        <asp:TextBox ID="txtFilename" runat="server" CssClass="SmallTextBox" Height="20px"
                            Width="300px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearchFS" runat="server" CssClass="SmallButton3" Text="Search"
                            OnClick="btnSearchFS_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 20">
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvFactoryStatus" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                Width="800px" DataKeyNames="fsd_id" OnRowCommand="gvFactoryStatus_RowCommand"
                OnPageIndexChanging="gvFactoryStatus_PageIndexChanging" PageSize="15">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="Category" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="File Name" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Owner" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Date Created" Width="150px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Factory" Width="150px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="fsd_impt" HeaderText="Importance">
                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fsc_type" HeaderText="Category">
                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fsd_filename" HeaderText="File Name">
                        <HeaderStyle Width="250px" HorizontalAlign="Center" />
                        <ItemStyle Width="250px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fsd_domain" HeaderText="Factory">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fsd_uploadName" HeaderText="Owner">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fsd_uploadDate" HeaderText="Date Created">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="">
                        <HeaderStyle Width="60px" HorizontalAlign="Center" />
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:LinkButton ID="linkDownload" runat="server" Font-Bold="False" Font-Size="12px"
                                CommandName="Download" Font-Underline="True" Text="Download"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="tabs-2">
            <table border="0" cellpadding="0" cellspacing="0" width="500px">
                <tr>
                    <td align="right" colspan="1" style="width: 80px; height: 27px">
                        Customer Part
                    </td>
                    <td align="left" colspan="1" style="height: 27px; width: 80px;">
                        <asp:TextBox ID="txtCustPart" runat="server" CssClass="SmallTextBox" Height="20px"
                            Width="100px"></asp:TextBox>
                    </td>
                    <td align="right" colspan="1" style="width: 50px; height: 27px">
                        QAD Part
                    </td>
                    <td align="left" colspan="1" style="width: 100px; height: 27px">
                        <asp:TextBox ID="txtPart" runat="server" CssClass="SmallTextBox" Height="20px" Width="100px"></asp:TextBox>
                    </td>
                    <td align="right" style="width: 50px; height: 27px">
                        <asp:Button ID="btnSearcheProduct" runat="server" CausesValidation="False" CssClass="SmallButton3"
                            OnClick="btnSearcheProduct_Click" Text="Query" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvProduct" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CssClass="GridViewStyle" OnPageIndexChanging="gvProduct_PageIndexChanging" PageSize="20"
                Width="800px" DataKeyNames="cp_part,cp_cust_part" OnRowCommand="gvProduct_RowCommand">
                <FooterStyle CssClass="GridViewFooterStyle" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                        GridLines="Vertical" Width="1100px">
                        <asp:TableRow>
                            <asp:TableCell HorizontalAlign="center" Text="Cust. Part" Width="150px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="QAD Part" Width="100px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="Long Lead Time" Width="80px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="Desc" Width="30px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="Doc" Width="30px"></asp:TableCell>
                            <asp:TableCell HorizontalAlign="center" Text="Part Description" Width="300px"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="cp_cust_part" HeaderText="Cust. Part">
                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cp_part" HeaderText="QAD Part">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="pt_pur_lead" HeaderText="Long Lead Time">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:ButtonField Text="Detail" HeaderText="Desc" CommandName="desc" ControlStyle-Font-Underline="true">
                        <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="false" />
                        <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:ButtonField>
                    <asp:ButtonField Text="View" HeaderText="Doc" CommandName="gobom" ControlStyle-Font-Underline="true">
                        <HeaderStyle Width="30px" HorizontalAlign="Center" Font-Bold="false" />
                        <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Black" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="Description" HeaderText="Part Description">
                        <HeaderStyle Width="300px" HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="tabs-3">
            <table>
                <tr>
                    <td>
                        Product:<asp:TextBox ID="txtFCPart" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date:<asp:TextBox
                            ID="txtMonth" runat="server" Width="30px"></asp:TextBox>
                        /<asp:TextBox ID="txtFcYear" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnFcQuery" runat="server" Text="Query" CssClass="SmallButton3" OnClick="btnFcQuery_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvForecast" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                AllowPaging="True" Width="850px" DataKeyNames="id" OnPageIndexChanging="gvForecast_PageIndexChanging">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table1" Width="850px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="Customer Part" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="QAD Part" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Forecast Date" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Forecast Quantity" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Unit" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="CreatedBy" Width="100px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="CreatedDate" Width="100px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="fc_cuPart" HeaderText="Customer Part">
                        <HeaderStyle Width="200px" HorizontalAlign="Center" />
                        <ItemStyle Width="200px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fc_part" HeaderText="QAD">
                        <HeaderStyle Width="150px" HorizontalAlign="Center" />
                        <ItemStyle Width="150px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fc_date" HeaderText="Ship Date">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="qty" HeaderText="Quantity">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="unit" HeaderText="Unit">
                        <HeaderStyle Width="80px" HorizontalAlign="Center" />
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fc_createBy" HeaderText="CreatedBy">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fc_createDate" HeaderText="Created Date">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="tabs-4">
            <div align="left">
                <table border="0" cellpadding="0" cellspacing="0" width="1210px">
                    <tr>
                        <td align="right" colspan="1" style="width: 60px; height: 27px">
                            Order
                        </td>
                        <td align="right" colspan="1" style="width: 160px; height: 27px">
                            <asp:TextBox ID="txtPo1" runat="server" CssClass="SmallTextBox" Height="20px" Width="80px"></asp:TextBox>--<asp:TextBox
                                ID="txtPo2" runat="server" CssClass="SmallTextBox" Height="20px" Width="80px"></asp:TextBox>
                        </td>
                        <td align="right" colspan="1" style="width: 60px; height: 27px">
                            Order Date
                        </td>
                        <td align="left" colspan="1" style="width: 160px; height: 27px">
                            <asp:TextBox ID="txtOrdDate1" runat="server" CssClass="SmallTextBox Date" Height="20px"
                                Width="80px"></asp:TextBox>--<asp:TextBox ID="txtOrdDate2" runat="server" CssClass="SmallTextBox Date"
                                    Height="20px" Width="80px"></asp:TextBox>
                        </td>
                        <td align="right" colspan="1" style="width: 50px; height: 27px">
                            Status
                        </td>
                        <td align="left" colspan="1" style="width: 100px; height: 27px">
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="110px">
                                <asp:ListItem Text="--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Not Load QAD" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Not Finish" Value="2" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Expired" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left" colspan="1" style="width: 70px; height: 27px">
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
                        <td align="right" style="width: 45px; height: 27px">
                            <asp:Button ID="btnRefresh" runat="server" CausesValidation="False" CssClass="SmallButton3"
                                OnClick="btnRefresh_Click" Text="Refresh" />
                        </td>
                        <td align="left" colspan="1" style="width: 110px; height: 27px">
                            <asp:DropDownList ID="ddlRegion" runat="server" Width="110px" DataValueField="code_value"
                                DataTextField='code_cmmt'>
                            </asp:DropDownList>
                        </td>
                        <td align="left" colspan="1" style="width: 100px; height: 27px">
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="SmallTextBox" Height="20px"
                                Width="100px" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="que" CssClass="GridViewStyle" OnPageIndexChanging="gvlist_PageIndexChanging"
                    OnRowDataBound="gvlist_RowDataBound" PageSize="20" Width="1300px">
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
                        <asp:BoundField DataField="partNbr" HeaderText="Item">
                            <HeaderStyle Width="200px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="poNbr,poLine" DataNavigateUrlFormatString="/plan/soque_track.aspx?poNbr={0}&amp;line={1}"
                            HeaderText="Order Question" Text="Detail" Target="_blank">
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
                        <asp:BoundField DataField="sod_line" HeaderText="Line">
                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
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
                        <asp:BoundField DataField="plandate" HeaderText="PCD" DataFormatString="{0:MM/dd/yyyy}"
                            HtmlEncode="False">
                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div id="tabs-5">
            <table style="width: 850px; height: 5px;">
                <tr>
                    <td>
                        Keywords:
                        <asp:TextBox ID="txtkeywords" runat="server" CssClass="SmallTextBox" Height="20px"
                            Width="250px" />
                        &nbsp;
                        <asp:RadioButtonList ID="ralStatus" runat="server" RepeatDirection="Horizontal" 
                            RepeatLayout="Flow">
                            <asp:ListItem Selected="True" Value="1">Open</asp:ListItem>
                            <asp:ListItem Value="2">Closed</asp:ListItem>
                        </asp:RadioButtonList>
&nbsp;&nbsp; <asp:Button ID="btn_messageselect" runat="server" Text="Query" CssClass="SmallButton2"
                            OnClick="btn_messageselect_Click" />
                    </td>
                    <td style="text-align: right;">
                        <asp:Button ID="btn_back" runat="server" Text="BACK" CssClass="SmallButton2" Visible="False"
                            OnClick="btn_back_Click" />
                        <asp:Button ID="btn_reply" runat="server" Text="REPLY" CssClass="SmallButton2" Visible="False"
                            OnClick="btn_reply_Click" />
                        <asp:Button ID="btn_new" runat="server" Text="NEW" CssClass="SmallButton2" OnClick="btn_new_Click" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gvMessage" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                AllowPaging="True" Width="920px" OnPageIndexChanging="gv_PageIndexChanging" DataKeyNames="fst_id,fst_filepath,fst_IsClosed,fst_createBy"
                OnRowCommand="gvMessage_RowCommand" PageSize="22" OnRowDataBound="gvMessage_RowDataBound">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="Owner" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Date" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Message" Width="610px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField HeaderText="NO.">
                        <ItemTemplate>
                            <br />
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject">
                        <ItemTemplate>
                            <br />
                            <asp:LinkButton ID="reply" runat="server" Font-Bold="False" Font-Size="12px" CommandName="reply"
                                Font-Underline="True" Text='<%# Bind("fst_desc") %>' Style="padding-left: 5px;"></asp:LinkButton>
                            <br />
                            <br />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="600px" />
                        <ItemStyle HorizontalAlign="Left" Width="600px" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="fst_createName" HeaderText="Author">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fst_newDate" HeaderText="Last Post">
                        <HeaderStyle Width="120px" HorizontalAlign="Center" />
                        <ItemStyle Width="120px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:LinkButton ID="close" runat="server" Font-Bold="False" Font-Size="12px" CommandName="close"
                                Font-Underline="True" Text='<%# Bind("fst_close") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:GridView ID="gvMessagereply" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                AllowPaging="True" Width="900px" DataKeyNames="fst_id,fst_filepath" Visible="False"
                OnRowCommand="gvMessagereply_RowCommand" OnPageIndexChanging="gvMessagereply_PageIndexChanging">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="Owner" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Date" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Message" Width="610px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="fst_createName" HeaderText="Author">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            Post At:
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("fst_createDate") %>' Style="margin-top: 10px;
                                padding-top: 10px;"></asp:Label>
                            <hr align="left" style="width: 100%; border-top: 1px dashed #000; border-bottom: 0px dashed #000;
                                height: 0px">
                          
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("fst_desc") %>' Style="word-break: break-all"
                                Width="600px" Font-Size="Medium"></asp:Label>
                            <br />
                            <br />
                            <asp:LinkButton ID="Download1" runat="server" Font-Bold="False" Font-Size="12px"
                                CommandName="DownloadFile" Font-Underline="True" Text='<%# Bind("fst_filename") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="12px" />
                        <HeaderStyle HorizontalAlign="Left" Width="700px" />
                        <ItemStyle HorizontalAlign="Left" Width="700px" Height="100px" VerticalAlign="Top"
                            Font-Bold="False" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
        </div>
    </div>
    </form>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
