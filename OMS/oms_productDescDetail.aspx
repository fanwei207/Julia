<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oms_productDescDetail.aspx.cs" Inherits="OMS_oms_productDescDetail" %>

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
    <form id="form1" runat="server">
    <div align="center">
    <table cellspacing="2" cellpadding="2" bgcolor="white" style="width: 600px; border:1px outset  white; background-color:e6e6e6;">
                <tr>
                    <td>
                        <strong>Item Number:</strong></td>
                    <td>
                        <asp:TextBox ID="txtItemNo" runat="server" Width="150px" TabIndex="0"></asp:TextBox></td>
                    <td>
                        <strong>UPC Number:</strong></td>
                    <td>
                        <asp:TextBox ID="txtUpcNo" runat="server" Width="150px"  TabIndex="1"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        Description:</strong></td>
                    <td colspan="3">
                        <asp:TextBox ID="txtDesc" runat="server" CssClass="SmallTextBox4" TabIndex="2" Width="440px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        Wattage:</strong></td>
                    <td>
                        <asp:TextBox ID="txtWattage" runat="server" CssClass="SmallTextBox4" TabIndex="3" Width="150px"></asp:TextBox></td>
                    <td>
                        <strong>
                        Equiv:</strong></td>
                    <td>
                        <asp:TextBox ID="txtEquiv" runat="server" CssClass="SmallTextBox4" TabIndex="4" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        Lumens:</strong></td>
                    <td>
                        <asp:TextBox ID="txtLumens" runat="server" CssClass="SmallTextBox4" TabIndex="5" Width="150px"></asp:TextBox></td>
                    <td>
                        <strong>
                        LPW:</strong></td>
                    <td>
                        <asp:TextBox ID="txtLPW" runat="server" CssClass="SmallTextBox4" TabIndex="6" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        CBCP est.:</strong></td>
                    <td>
                        <asp:TextBox ID="txtCBCPest" runat="server" CssClass="SmallTextBox4" TabIndex="7" Width="150px"></asp:TextBox></td>
                    <td>
                        <strong>
                        Beam Angle:</strong></td>
                    <td>
                        <asp:TextBox ID="txtBeamAngle" runat="server" CssClass="SmallTextBox4" TabIndex="8" Width="150px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        CCT:</strong></td>
                    <td>
                        <asp:TextBox ID="txtCCT" runat="server" CssClass="SmallTextBox4" TabIndex="9" Width="150px"  ></asp:TextBox></td>
                    <td>
                        <strong>
                        CRI:</strong></td>
                    <td>
                        <asp:TextBox ID="txtCRI" runat="server" CssClass="SmallTextBox4" TabIndex="10" Width="150px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        MOL:</strong></td>
                    <td>
                        <asp:TextBox ID="txtMOL" runat="server" CssClass="SmallTextBox4" TabIndex="11" Width="150px"  ></asp:TextBox></td>
                    <td>
                        <strong>
                        Dia:</strong></td>
                    <td>
                        <asp:TextBox ID="txtDia" runat="server" CssClass="SmallTextBox4" TabIndex="12" Width="150px" ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        IP:</strong></td>
                    <td>
                        <asp:TextBox ID="txtIP" runat="server" CssClass="SmallTextBox4" TabIndex="13" Width="150px"  ></asp:TextBox></td>
                    <td>
                        <strong>
                        MP:</strong></td>
                    <td>
                        <asp:TextBox ID="txtMP" runat="server" CssClass="SmallTextBox4" TabIndex="14" Width="150px"  ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        List:</strong></td>
                    <td>
                        <asp:TextBox ID="txtList" runat="server" CssClass="SmallTextBox4" TabIndex="15" Width="150px"  ></asp:TextBox></td>
                    <td>
                        <strong>
                        Price:</strong></td>
                    <td>
                        <asp:TextBox ID="txtPrice" runat="server" CssClass="SmallTextBox4" TabIndex="16" Width="150px"  ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        STK/MTO:</strong></td>
                    <td>
                        <asp:TextBox ID="txtSTKMTO" runat="server" CssClass="SmallTextBox4" TabIndex="17" Width="150px"></asp:TextBox></td>
                    <td>
                        <strong>
                        A4:</strong></td>
                    <td>
                        <asp:TextBox ID="txtA4" runat="server" CssClass="SmallTextBox4" TabIndex="18" Width="150px"  ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>LM79/Life:</strong></td>
                    <td>
                        <asp:TextBox ID="txtLM79Life" runat="server" CssClass="SmallTextBox4" TabIndex="19" Width="150px"></asp:TextBox></td>
                     <td>
                        <strong>IES:</strong></td>
                    <td>
                        <asp:TextBox ID="txtIES" runat="server" CssClass="SmallTextBox4" TabIndex="20" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>
                        UL:</strong></td>
                    <td>
                        <asp:TextBox ID="txtUL" runat="server" CssClass="SmallTextBox4" TabIndex="21" Width="150px"></asp:TextBox></td>
                    <td>
                        <strong>
                        LDL :</strong></td>
                    <td>
                        <asp:TextBox ID="txtLDL" runat="server" CssClass="SmallTextBox4" TabIndex="22" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>Energy Star:</strong></td>
                    <td>
                        <asp:TextBox ID="txtEnergyStar" runat="server" CssClass="SmallTextBox4"
                            TabIndex="23" Width="150px"></asp:TextBox></td>
                    <td>
                        <strong>Model #:</strong></td>
                    <td>
                        <asp:TextBox ID="txtModel" runat="server" CssClass="SmallTextBox4"
                            TabIndex="24" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>Caution Statement:</strong></td>
                    <td colspan="3">
                        <asp:TextBox ID="txtCaution" runat="server" CssClass="SmallTextBox4"
                            TabIndex="25" Width="440px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>Country of Origin:</strong></td>
                    <td>
                        <asp:TextBox ID="txtCountry" runat="server" CssClass="SmallTextBox4" 
                            TabIndex="26" Width="150px"></asp:TextBox></td>
                     <td>
                        <strong>pf:</strong></td>
                    <td>
                        <asp:TextBox ID="txtPf" runat="server" CssClass="SmallTextBox4" 
                            TabIndex="27" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>Date Code:</strong></td>
                    <td>
                        <asp:TextBox ID="txtDateCode" runat="server" CssClass="SmallTextBox4"  TabIndex="28" Width="150px"></asp:TextBox></td>
                    <td>
                        <strong>UL File#:</strong></td>
                    <td>
                        <asp:TextBox ID="txtULFile" runat="server" CssClass="SmallTextBox4"  TabIndex="29" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>UL Control#:</strong></td>
                    <td>
                        <asp:TextBox ID="txtULControl" runat="server" CssClass="SmallTextBox4" 
                            TabIndex="30" Width="150px"></asp:TextBox></td>
                     <td>
                        <strong>UL Group:</strong></td>
                    <td>
                        <asp:TextBox ID="txtULGroup" runat="server" CssClass="SmallTextBox4" 
                            TabIndex="31" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>Voltage:</strong></td>
                    <td>
                        <asp:TextBox ID="txtVoltage" runat="server" CssClass="SmallTextBox4" 
                            TabIndex="32" Width="150px"></asp:TextBox></td>
                     <td>
                        <strong>Frequency:</strong></td>
                    <td>
                        <asp:TextBox ID="txtFrequency" runat="server" CssClass="SmallTextBox4" 
                            TabIndex="33" Width="150px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <strong>Amperage:</strong></td>
                    <td>
                        <asp:TextBox ID="txtAmperage" runat="server" CssClass="SmallTextBox4" 
                            TabIndex="34" Width="150px"></asp:TextBox></td>
                     <td>
                        </td>
                    <td>
                        <asp:Label ID="lblId" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                </tr>
                <tr>
                    <td style="height: 24px">
                    </td>
                    <td style="height: 24px"  align="right">
                        <%--<asp:Button ID="btnNew" runat="server" CssClass="SmallButton2" TabIndex="5" Text="New"
                            Width="60px" OnClick="btnNew_Click" />--%>
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" TabIndex="35" Text="Back"
                            Width="60px" OnClick="btnBack_Click" /></td>
                    <td style="height: 24px">
                    </td>
                    <td style="height: 24px">
                        </td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
