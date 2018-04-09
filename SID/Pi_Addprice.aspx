<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pi_Addprice.aspx.cs" Inherits="IT_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="form1" runat="server">
        <table cellspacing="0" cellpadding="2" bgcolor="white" border="0" 
            style="background-position: inherit; background-image: url('../images/bg_tb17.jpg');
            background-repeat: repeat-x; margin-top: 20px; height:267px; background-attachment: scroll;">
            
            <tr style="height: 16px">
                <td rowspan="12" style="width: 4px; background-image: url(../images/bg_tb16.jpg);
                    background-repeat: no-repeat; background-position: left top;">
                </td>
                <td style="height: 1px; text-align: right;">
                </td>
                <td align="right" style="width: 60px; ">
                    客户:
                </td>
                <td align="Left" style="width: 213px">
                    <asp:TextBox ID="txtCust" runat="server" CssClass="smalltextbox Customer" 
                        Width="122px" MaxLength="10" ></asp:TextBox>
                         <asp:Label ID="lbCustName" runat="server" CssClass="smalltextbox" ></asp:Label>
                </td>
                <td rowspan="12" style="width: 4px; background-image: url(../images/bg_tb18.jpg);
                    background-repeat: no-repeat; background-position: right top;">
                </td>
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;">
                </td>
                <td align="right" style="width: 60px">
                     物料号:
                </td>
                <td align="Left" style="width: 213px">
                        <asp:TextBox ID="txtQad" runat="server" CssClass="smalltextbox Part" 
                            Width="122px" MaxLength="15"></asp:TextBox>

                            <asp:Label ID="lblpart" runat="server" CssClass="smalltextbox"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;">
                </td>
                <td align="right" style="width: 60px">
                     &nbsp;最终客户:
                </td>
                <td align="Left" style="width: 213px">
                    <asp:TextBox ID="txtcust2" runat="server" CssClass="smalltextbox Customer" 
                        Width="122px" MaxLength="10"></asp:TextBox>
                     <asp:Label ID="lbCustName2" runat="server" CssClass="smalltextbox"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;"></td>
                <td align="right" style="width: 60px">域:</td>
                <td align="Left" style="width: 213px">                    
                    <asp:DropDownList ID="ddl_Domain" runat="server" Width="81px">
                        <asp:ListItem Value="0">--</asp:ListItem>
                        <asp:ListItem Value="SZX">SZX</asp:ListItem>
                        <asp:ListItem Value="ZQL">ZQL</asp:ListItem>
                        <asp:ListItem Value="ZQZ">ZQZ</asp:ListItem>
                        <asp:ListItem Value="YQL">YQL</asp:ListItem>
                        <asp:ListItem Value="HQL">HQL</asp:ListItem>
                        <asp:ListItem Value="ATL">ATL</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;">
                   
                </td>
                <td align="right" style="width: 60px">
                  货币:
                </td>
                <td align="Left" style="width: 213px">
                      <asp:DropDownList ID="ddlCurrency" runat="server">
                       <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                       <asp:ListItem Value="1">USD</asp:ListItem>
                        <asp:ListItem Value="2">RMB</asp:ListItem>
                       <asp:ListItem Value="3">EUR</asp:ListItem>
                       <asp:ListItem Value="4">HKD</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;">
                   
                </td>
                <td align="right" style="width: 60px">
                   单位:
                </td>
                <td align="Left" style="width: 213px">
                     <asp:DropDownList ID="ddlUM" runat="server">
                       <asp:ListItem Selected="True" Value="0">--</asp:ListItem>
                       <asp:ListItem Value="1">SETS</asp:ListItem>
                       <asp:ListItem Value="2">PCS</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;">
                </td>
                <td align="right" style="width: 60px">
                    &nbsp;开始日期:
                </td>
                <td align="Left" style="width: 213px">
                        <asp:TextBox ID="txtStdDate" runat="server" CssClass="smalltextbox Date" Width="82px"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;">
                   
                </td>
                <td align="right" style="width: 60px">
                    到期日期:
                </td>
                <td align="Left" style="width: 213px">
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="smalltextbox Date" Width="82px"
                        MaxLength="10"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;">
                </td>
                 <td align="right" style="width: 60px">
                   价格1：
                </td>
                  <td align="Left" style="width: 213px">
                        <asp:TextBox ID="txtprice1" runat="server" CssClass="smalltextbox" Width="82px"
                        MaxLength="10"></asp:TextBox>
                        (SZX--&gt; ATL)
                </td>
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;">
                   
                </td>
                 <td align="right" style="width: 60px">
                   价格2：
                </td>
                  <td align="Left" style="width: 213px">
                        <asp:TextBox ID="txtprice2" runat="server" CssClass="smalltextbox"   Width="82px"
                        MaxLength="10"></asp:TextBox>
                        (ATL--&gt;TCP)
                </td>
            </tr>
            <tr>
                <td style="height: 1px; text-align: right;">
                </td>
                 <td align="right" style="width: 60px">
                    价格3：
                </td>
                <td align="Left" style="width: 213px">
                        <asp:TextBox ID="txtprice3" runat="server" CssClass="smalltextbox"   Width="82px" MaxLength="10"></asp:TextBox>
                        (~--&gt;CUSTOMER)
                </td>
            </tr>
            <tr>
                <td  text-align: right;">
                </td>
                 <td align="right" style="width: 60px">
                    备注：
                </td>
                <td align="Left" style="width: 213px">
                        <asp:TextBox ID="txtremark" runat="server" CssClass="smalltextbox" Width="82px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="Center" colspan="5" style="height: 25px">
                    <asp:Button ID="btnSave" runat="server" Text="保存" 
                        Width="70px" onclick="btnSave_Click"/>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
