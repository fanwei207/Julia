﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_TrackingPrjByTamplate.aspx.cs" Inherits="RDW_RDW_TrackingPrjByTamplate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 127px;
        }
        .auto-style2 {
            width: 110px;
        }
        .auto-style3 {
            width: 131px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div align="Center">
            <table width="1200px">
                <tr>
                    <td align="right">
                        <asp:Label ID="lbl_region" runat="server" CssClass="LabelRight" Font-Bold="False" Text="Region:"
                            Width="50px" Height="16px"></asp:Label>
                    </td>
                    <td>
                    <asp:DropDownList ID="ddl_region" runat="server" DataTextField="cate_region"
                        DataValueField="cate_region" Width="80px">
                    </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label5" runat="server" CssClass="LabelRight" Font-Bold="False" Text="Project Category:"
                            Width="100px"></asp:Label>
                    </td>
                    <td>
                    <asp:DropDownList ID="ddl_Category" runat="server" DataTextField="cate_name"
                        DataValueField="cate_id" Width="80px">
                    </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Label ID="lblLampType" runat="server" CssClass="LabelRight" Font-Bold="False" Text="LampType:"
                            Width="60px"></asp:Label>
                    </td>
                    <td align="left" class="auto-style3" >
                        <asp:TextBox runat="server" ID="txtLampType" Width="120px" CssClass="SmallTextBox"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label1" runat="server" CssClass="LabelRight" Font-Bold="False" Text="Project Name:"
                            Width="79px"></asp:Label>
                    </td>
                    <td align="left" class="auto-style1" >
                        <asp:TextBox runat="server" ID="txt_prod" Width="120px"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label2" runat="server" CssClass="LabelRight" Font-Bold="False" Text="Project Code:"
                            Width="70px"></asp:Label>
                    </td>
                    <td align="left" class="auto-style2" >
                        <asp:TextBox runat="server" ID="txt_code" Width="100px"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label3" runat="server" CssClass="LabelRight" Font-Bold="False" Text="Start Date:"
                            Width="60px"></asp:Label>
                    </td>
                    <td align="left" class="auto-style3" >
                        <asp:TextBox runat="server" ID="txt_startDate" Width="120px" CssClass="SmallTextBox EnglishDate"></asp:TextBox>
                    </td>
                    
                    <td>
                        <asp:Button runat="server" ID="bnt_query" Text="Query" CssClass="SmallButton2" OnClick="bnt_query_Click" />
                        
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btn_export1" Text="Exports" CssClass="SmallButton2" OnClick="btn_export1_Click" />
                    </td>
                </tr>
                <tr>
                      <td align="right">
                        <asp:Label ID="Label4" runat="server" CssClass="LabelRight" Font-Bold="False" Text="Status:"
                            Width="40px"></asp:Label>
                    &nbsp;</td>
                    <td>
                    <asp:DropDownList ID="ddl_Status" runat="server" DataTextField="SKU" DataValueField="SKU"
                        Width="80px"> 
                        <asp:ListItem Value="--">--</asp:ListItem>
                        <asp:ListItem Value="PROCESS" Selected="True">In Process</asp:ListItem>
                        <asp:ListItem Value="SUSPEND">Suspend</asp:ListItem>
                        <asp:ListItem Value="CLOSE">Close</asp:ListItem>
                        <asp:ListItem Value="CANCEL">Cancel</asp:ListItem>
                    </asp:DropDownList>
                </td>
                    <td align="right"><label CssClass="LabelRight" Font-Bold="False" 
                            Width="40px">Comments lines qty:</label> </td>  
                    <td>
                        <asp:TextBox id ="txtLineNo" runat="server" CssClass="SmallTextBox5" Width="80px">3</asp:TextBox>

                    </td>
                </tr>
            </table>
            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                PageSize="10" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />

            </asp:GridView>
        </div>
    </form>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
