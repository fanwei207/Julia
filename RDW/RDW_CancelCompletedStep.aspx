<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_CancelCompletedStep.aspx.cs"
    Inherits="RDW_ViewPartner" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 49px;
        }
        .style2
        {
            width: 46px;
        }
        .style3
        {
            width: 203px;
            height: 3px;
        }
        .style4
        {
            width: 49px;
            height: 3px;
        }
        .style5
        {
            height: 3px;
        }
    </style>
       
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" width="350px" bgcolor="white" border="0">
            <tr>
                <td align="right" class="style1"  >
                    <asp:Label ID="lblProject" runat="server" Width="100px" CssClass="LabelRight" Text="Project Name:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2"  >
                    <asp:Label ID="lblProjectData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1" >
                    <asp:Label ID="lblProdCode" runat="server" Width="100px" CssClass="LabelRight" Text="Product Code:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2"  >
                    <asp:Label ID="lblProdCodeData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1" >
                    <asp:Label ID="lblStepName" runat="server" Width="101px" CssClass="LabelRight" Text="Step Name:"
                        Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:Label ID="lblStepNameData" runat="server" CssClass="LabelLeft" Font-Bold="false"></asp:Label>
                </td>
            </tr>
            <tr style="width:1px;">
                <td align="right" class="style4" >
                    </td>
                <td align="left" class="style3"  >
                    </td>
                    <td align="left" class="style5"  >
                        </td>
            </tr>
            <tr>
                <td align="right" colspan="3" >
                    <hr  />
                </td>
            </tr>
            <tr>
                <td align="right" class="style1" >
                    <asp:Label ID="lblDisReason" runat="server" Text="DisApprove Reason:" 
                        CssClass="LabelRight" Width="100px"  Font-Bold="False" ></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtReason" runat="server" Height="53px" Width="258px" TextMode="MultiLine"
                        MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="273px" Height="146px"
                        BorderWidth="0" GroupingText="Cancel Member Finish" HorizontalAlign="left" 
                        ScrollBars="Auto">
                        <asp:CheckBoxList ID="chkUser" runat="server" DataTextField="userInfo" DataValueField="userID">
                        </asp:CheckBoxList>
                        <br />
                        <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" Text="Select All" OnCheckedChanged="chkAll_CheckedChanged" />
                    </asp:Panel>
                </td>
                <td rowspan="3" valign="middle"  >
                    <asp:CheckBox ID="chkEmail" runat="server" Text="Send Email" Checked="true" 
                        Width="78px" /> 
                        <br />
                    <asp:Button ID="btnOK" runat="server" CssClass="SmallButton2" Text="OK" Width="60px"
                        OnClick="btnOK_Click" />
                </td>
            </tr>
            <tr>  
                <td align="right" colspan="2" class="style2">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Panel ID="plSubSteps" Style="overflow: auto" runat="server" Width="280px" Height="160px"
                        BorderWidth="0" GroupingText="Cancel Subsequent Completed Steps" HorizontalAlign="left"
                        ScrollBars="Auto">
                        <asp:CheckBoxList ID="ckbSubCompSteps" runat="server" DataTextField="RDW_StepName"
                            DataValueField="RDW_DetID">
                        </asp:CheckBoxList>
                        <br />
                    </asp:Panel>
                </td>
            </tr> 
        </table>
        </form>
    </div>
    <script type="text/javascript">
            <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
