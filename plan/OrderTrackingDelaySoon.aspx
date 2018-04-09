<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderTrackingDelaySoon.aspx.cs" Inherits="plan_OrderTrackingDelaySoon" %>

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
        <table border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td align="right" colspan="1" style="height: 27px">
                    类型：<asp:DropDownList ID="dropType" runat="server" Width="50px">
                        <asp:ListItem Text="--" Value=""></asp:ListItem>
                        <asp:ListItem Text="CFL" Value="CFL"></asp:ListItem>
                        <asp:ListItem Text="LED" Value="LED"></asp:ListItem>
                        <asp:ListItem Text="卤素灯" Value="HAL"></asp:ListItem>
                        <asp:ListItem Text="其他" Value="OTH"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style=" height: 27px">
                    &nbsp;&nbsp;&nbsp;
                    Order
                </td>
                <td align="left" colspan="1" style="height: 27px;">
                    <asp:TextBox ID="txtPo1" runat="server" CssClass="SmallTextBox" Height="20px" Width="80px"></asp:TextBox>--<asp:TextBox
                        ID="txtPo2" runat="server" CssClass="SmallTextBox" Height="20px" Width="80px"></asp:TextBox>
                </td>
                <td align="right" colspan="1" style="height: 27px">
                    &nbsp;&nbsp;&nbsp;
                    Region
                </td>
                <td align="left" colspan="1" style=" height: 27px">
                    <asp:DropDownList ID="ddlRegion" runat="server" DataValueField="code_value"
                        DataTextField='code_cmmt'>
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="1" style=" height: 27px">
                    &nbsp;&nbsp;&nbsp;
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
                
               
                <td align="left" colspan="1" style="height: 27px">
                    &nbsp;&nbsp;&nbsp;
                    Total<asp:TextBox ID="txtTotal" runat="server" CssClass="TextRight" Height="20px"
                        ReadOnly="true" Width="38px"></asp:TextBox>
                </td>
                <td align="right" style=" height: 27px">
                    <asp:Button ID="btnSearch" runat="server" CausesValidation="False" CssClass="SmallButton3"
                        OnClick="btnSearch_Click" Text="Query" />
                     <asp:Button ID="btnExport" runat="server" CausesValidation="False" CssClass="SmallButton3"
                     Text="Export" OnClick="btnExport_Click" />
                </td>
               
            </tr>
          
        </table>

        <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="que,planDate,poNbr,poLine,sod_ord_date,SID_nbr" CssClass="GridViewStyle GridViewRebuild" OnPageIndexChanging="gvlist_PageIndexChanging" PageSize="20" Width="1730px">
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
                <asp:BoundField DataField="poNbr" HeaderText="Order#" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="SID_nbr" HeaderText="SID nbr" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PoRecDate" HeaderText="Order Date" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                
                 <asp:BoundField DataField="reqdate" HeaderText="Request Date" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                 <asp:BoundField DataField="sid_insp_matchdate" HeaderText="Ship Date" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="cusCode" HeaderText="Customer Code" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="poLine" HeaderText="Line" ReadOnly="True">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="partNbr" HeaderText="Item" ReadOnly="True">
                    <HeaderStyle Width="200px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:BoundField>
             
                <asp:BoundField DataField="sod_ord_date" HeaderText="Load QAD Date" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                  <asp:BoundField DataField="so_nbr" HeaderText="Qad So#" DataFormatString="{0:MM/dd/yyyy}"
                    HtmlEncode="False" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                
                <asp:BoundField DataField="sod_part" HeaderText="QAD Part" ReadOnly="True">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_qty_ord" DataFormatString="{0:F0}" HeaderText="Order Qty"
                    HtmlEncode="False" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="wo_qty_ord" DataFormatString="{0:F0}" HeaderText="Wo Qty" 
                    HtmlEncode="False" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>

               
                <asp:BoundField DataField="wt_qty" DataFormatString="{0:F0}" HeaderText="Online Qty"
                    HtmlEncode="False" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="sod_qty_ship" DataFormatString="{0:F0}" HeaderText="Ship Qty"
                    HtmlEncode="False" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="require_QTY" DataFormatString="{0:F0}" HeaderText="request Qty"
                    HtmlEncode="False" ReadOnly="True">
                    <HeaderStyle Width="80px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="qadType" HeaderText="分类" ReadOnly="True">
                    <HeaderStyle Width="60px" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
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