<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UL_Model.aspx.cs" Inherits="IT_UL_Model" %>

<!DOCTYPE html>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 163px;
        }
    </style>
</head>
<body>
     <div align="center">
          <form id="Form1" method="post" runat="server">
                <table width="600">
                    <tr>
                        <td>
                            Project Name:<asp:TextBox ID="txtProject" runat="server" Width="300px" CssClass="SmallTextBox"
                        TabIndex="1" Enabled="False"></asp:TextBox>
                        </td>
                        

                         </tr>
                      <tr>
                         <td>
                            UL Model:<asp:TextBox ID="txtDate1" runat="server" Width="300px" CssClass="SmallTextBox "
                        TabIndex="1"></asp:TextBox>
                        </td>
                         </tr>
                    <tr>
                         <td>
                            <table>
                                <tr>
                                    <td>
                                         E号:<asp:TextBox ID="txtUl_Enumber" runat="server" Width="130px" CssClass="SmallTextBox "
                        TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td>
                                        Section:<asp:TextBox ID="txtUL_Section" runat="server" Width="130px" CssClass="SmallTextBox "
                        TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                  <tr>
                                    <td>
                                         Driver JXL:<asp:TextBox ID="txtUL_DriverJXL" runat="server" Width="130px" CssClass="SmallTextBox "
                        TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td>
                                        LED JXL:<asp:TextBox ID="txtUL_LEDJXL" runat="server" Width="130px" CssClass="SmallTextBox "
                        TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                          Driver Lv:<asp:TextBox ID="txtDriverlv" runat="server" Width="130px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td>
                                        LED Lv:<asp:TextBox ID="txtLEDllv" runat="server" Width="130px" CssClass="SmallTextBox"
                        TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                          文档中查到的对应图片号:<asp:TextBox ID="txtUL_pic" runat="server" Width="130px" CssClass="SmallTextBox "
                        TabIndex="1"></asp:TextBox>
                                    </td>
                                    <td>
                                        NOTE:<asp:TextBox ID="txtUL_NOTE" runat="server" Width="130px" CssClass="SmallTextBox "
                        TabIndex="1"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                         </tr>
                    <tr>
                         <td >
                           &nbsp;&nbsp; <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" TabIndex="9" Text="Complete"
                        Width="50px" CausesValidation="false" OnClick="btnSave_Click"   />
                                &nbsp;&nbsp; <asp:Button ID="btnback" runat="server" CssClass="SmallButton2" TabIndex="9" Text="back"
                        Width="50px" CausesValidation="false" OnClick="btnback_Click"    />
                        </td>

                        
                    </tr>

                </table>

    
        </form>
    </div> 
    <script language="javascript" type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
