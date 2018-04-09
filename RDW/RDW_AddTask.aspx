<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_AddTask.aspx.cs" Inherits="RDW_AddTask" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript"> 
        $(function () {
            $("#ddl_task").change(function(){
                var desc = $("#ddl_task").find("option:selected").text();
                if($("#txt_desc").val() == "")
                    $("#txt_desc").val(desc);
            })
        })
    </script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="2" cellpadding="2" bgcolor="white" border="0" style="width: 462px">
            <tr id="tr1" runat="server" >
                <td align="right" class="style1">
                <asp:Label ID="lbl_Predecessor" runat="server" Width="106px" CssClass="LabelRight"
                        Text="Predecessor:" Font-Bold="False"></asp:Label>
                   </td>
                <td align="left" >
                    <asp:DropDownList ID="ddl_Predecessor" runat="server" Height="16px" Width="249px" DataTextField = "RDW_StepName"  DataValueField ="RDW_StepID" >
                    </asp:DropDownList>
                    &nbsp;</td>
                <td align="left" >
                    <asp:CheckBox runat="server" ID ="chk_isExtra" Text="Extra Step" />
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
                <asp:Label ID="Label1" runat="server" Width="106px" CssClass="LabelRight"
                        Text="Duration :" Font-Bold="False"></asp:Label>
                </td>
                <td align="left" colspan="2">
                    <asp:TextBox ID="txtDuration" runat="server" CssClass="SmallTextBox" Width="46px"
                        TabIndex="3" MaxLength="250" ></asp:TextBox>(Days)
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblDescription" runat="server" Width="100px" CssClass="LabelRight"
                        Text="Description" Font-Bold="False"></asp:Label>
                </td>
                <td align="Left" colspan="2">
                    &nbsp;<asp:TextBox runat="server" ID="txt_desc" TextMode="MultiLine"  Width="249px" Height="80px"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lbl_StandardStep" runat="server" Width="100px" CssClass="LabelRight"
                        Text="StandardStep" Font-Bold="False"></asp:Label>
                </td>
                <td align="Left" colspan="2">
                    &nbsp;<asp:DropDownList runat="server" ID ="ddl_task" DataTextField="RDW_StepName"  DataValueField="RDW_Code" Width="249px" ></asp:DropDownList>
                </td>
            </tr>
                
            <tr>
                <td align="right" style="height: 17px">
                </td>
                <td align="left" colspan="2" style="height: 17px">
                    <asp:Label ID="lbID" runat="server" Text="0" Visible="False"></asp:Label>
                    <asp:Label ID="lbMID" runat="server" Text="0" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" TabIndex="7" Text="Save"
                        Width="50px" CausesValidation="true" ValidationGroup="chkAll" OnClick="btnSave_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnCancel" runat="server" CssClass="SmallButton2" TabIndex="8" Text="Back"
                        Width="50px" CausesValidation="false" OnClick="btnCancel_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1" style="height: 116px">
                </td>
                <td class="style1" style="height: 116px">
                </td>
                <td class="style1" style="height: 116px">
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
