<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_SKUEdit.aspx.cs" Inherits="RDW_SKUEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body >
    <div align="center">
        <form id="Form1" method="post" runat="server">
            <table cellspacing="2" cellpadding="2" bgcolor="white" style="width: 546px; border:1px outset  white; background-color:e6e6e6;">
                <tr>
                    <td>
                        <strong>SKU/Model:</strong></td>
                    <td>
                        <asp:TextBox ID="txtSKU" runat="server" Width="150px" TabIndex="1" CssClass="SmallTextBox4" onblur="javascript:if(this.value.length > 30) alert('The length of SKU must be less then 30 characters.');" MaxLength="30"></asp:TextBox></td>
                    <td>
                        less then 30 characters[required]</td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        UPC:</strong></td>
                    <td>
                        <asp:TextBox ID="txtUPC" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="150px" onblur="javascript:if(this.value.length > 12) alert('The length of UPC must be less then 12 characters.');" MaxLength="12"></asp:TextBox></td>
                    <td>
                        12 characters[Optional]</td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        Voltage (V):</strong></td>
                    <td>
                        <asp:TextBox ID="txtVoltage" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="150px" onkeyup="value=value.replace(/[^\d]/g,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox></td>
                    <td>
                        numeric, no decimals[Optional]</td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        Wattage (W):</strong></td>
                    <td>
                        <asp:TextBox ID="txtWattage" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="150px"  onkeyup="value=value.replace(/[^\d]/g,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox></td>
                    <td>
                        numeric, no decimals[Optional]</td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        Lumens (lm):</strong></td>
                    <td>
                        <asp:TextBox ID="txtLumens" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="150px"  onkeyup="value=value.replace(/[^\d]/g,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox></td>
                    <td>
                        numeric, no decimals[Optional]</td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        LPW:</strong></td>
                    <td>
                        <asp:TextBox ID="txtLPW" runat="server" ReadOnly="true" CssClass="SmallTextBox4" TabIndex="1" Width="150px" 
                          onkeyup="value=value.replace(/[^\d.]/g,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d.]/g,''))"></asp:TextBox></td>
                    <td>
                        (=Lumens/Wattage)</td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        CBCPest:</strong></td>
                    <td>
                        <asp:TextBox ID="txtCBCPest" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="150px"    onkeyup="value=value.replace(/[^\d]/g,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox></td>
                    <td>
                        numeric, no decimals[Optional]</td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        Beam Angle:</strong></td>
                    <td>
                        <asp:TextBox ID="txtBeamAngle" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="150px"  onkeyup="value=value.replace(/[^\d]/g,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox></td>
                    <td>
                        numeric, no decimals[Optional]</td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        CCT (K):</strong></td>
                    <td>
                        <asp:TextBox ID="txtCCT" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="150px"  onkeyup="value=value.replace(/[^\d]/g,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox></td>
                    <td>
                        numeric, no decimals[Optional]</td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        CRI:</strong></td>
                    <td>
                        <asp:TextBox ID="txtCRI" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="150px"  onkeyup="value=value.replace(/[^\d]/g,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox></td>
                    <td>
                        numeric, no decimals[Optional]</td>
                </tr>
                <tr>
                    <td>
                        <strong>STK/MTO:</strong></td>
                    <td>
                        <asp:TextBox ID="txtSTKorMTO" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="150px" onkeyup="value=value.toUpperCase()" onblur="javascript:if(this.value.toUpperCase() != 'STK' && this.value.toUpperCase() != 'MTO') alert('valid values of STK or MTO.');" MaxLength="3"></asp:TextBox></td>
                    <td>
                        valid values of "Stock" or "Make to Order"</td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        Case Qty:</strong></td>
                    <td>
                        <asp:TextBox ID="txtCaseQty" runat="server" CssClass="SmallTextBox4" TabIndex="1" Width="150px"  onkeyup="value=value.replace(/[^\d]/g,'')" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox></td>
                    <td>
                        numeric, no decimals[Optional]</td>
                </tr>
                <tr>
                    <td>
                        <strong>UL:</strong></td>
                    <td>
                        <asp:TextBox ID="txtUL" runat="server" CssClass="SmallTextBox4" MaxLength="60"
                            onblur="javascript:if(this.value.length > 60) alert('The length of UL must be less then 60 characters.');"
                            TabIndex="1" Width="150px"></asp:TextBox></td>
                    <td>
                        60 characters [Optional]</td>
                </tr>
                <tr>
                    <td>
                        <strong>Product Category:</strong></td>
                    <td>
                        <asp:DropDownList ID="dropProductCategory" runat="server" DataTextField="ProductCategory" DataValueField="ProductCategory" Width="150px" Height="23px">
                        </asp:DropDownList></td>
                    <td>
                        dropdown with the following values</td>
                </tr>
                <tr>
                    <td>
                        <strong>LED Chip Type:</strong></td>
                    <td>
                        <asp:TextBox ID="txtLEDChipType" runat="server" CssClass="SmallTextBox4" MaxLength="30"
                            onblur="javascript:if(this.value.length > 30) alert('The length of LED Chip Type must be less then 30 characters.');"
                            TabIndex="1" Width="150px"></asp:TextBox></td>
                    <td>
                        30 characters [Optional]</td>
                </tr>
                <tr>
                    <td>
                        <strong>LED Chip Qty:</strong></td>
                    <td>
                        <asp:TextBox ID="txtLEDChipQty" runat="server" CssClass="SmallTextBox4" onbeforepaste="clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''))"
                            onkeyup="value=value.replace(/[^\d]/g,'')" TabIndex="1" Width="150px"></asp:TextBox></td>
                    <td>
                        numeric, no decimals [Optionall]</td>
                </tr>
                <tr>
                    <td>
                        <strong>Driver Type:</strong></td>
                    <td>
                        <asp:TextBox ID="txtDriverType" runat="server" CssClass="SmallTextBox4" MaxLength="30"
                            onblur="javascript:if(this.value.length > 30) alert('The length of Driver Type must be less then 30 characters.');"
                            TabIndex="1" Width="150px"></asp:TextBox></td>
                    <td>
                        30 characters [Optional]</td>
                </tr>
                <tr>
                    <td>
                        <strong>Customer Name:</strong></td>
                    <td>
                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="SmallTextBox4" MaxLength="30"
                            onblur="javascript:if(this.value.length > 30) alert('The length of Customer Name must be less then 30 characters.');"
                            TabIndex="1" Width="150px"></asp:TextBox></td>
                    <td>
                        30 characters [Optional]</td>
                </tr>
                <tr>
                    <td style="height: 24px">
                    </td>
                    <td style="height: 24px">
                        <asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" TabIndex="5" Text="New"
                            Width="60px" OnClick="btnNew_Click" />
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" TabIndex="5" Text="Back"
                            Width="60px" OnClick="btnBack_Click" /></td>
                    <td style="height: 24px">
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
