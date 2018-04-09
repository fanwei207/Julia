<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_DemoNew.aspx.cs" Inherits="IT_TSK_DemoNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
       <base target="_self">
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table id = "table1" runat="server">
        <tr>
            <td>
            Title:
            </td>
            <td>
                 <asp:TextBox ID="txtTitle" runat="server" Width="300px" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                    MenuName:
            </td>
            <td align="left">
                    <asp:TextBox ID="txtname" runat="server" Width="300px"
                        MaxLength="100"></asp:TextBox>
            </td>
        </tr>
         
        <tr>
        <td>
        
        </td>
        <td align="left">
                <asp:CheckBox ID="ckb_ismenu" runat="server" Checked="True" Text="isMenu" />
        </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                
              <asp:Button ID="btnNew" runat="server" Text="NEW" CssClass="SmallButton3" 
                    onclick="btnNew_Click" />
                <asp:Button ID="btnback" runat="server" Text="Back" CssClass="SmallButton3" 
                    onclick="btnback_Click" />
            </td>
        </tr>
      
    </table>


    <table id="table2" runat="server" visible=false width = "650px">
      <tr>
            <td>
            Title:
            </td>
            <td align="left">
                 <asp:TextBox ID="txtTitle2" runat="server" Width="300px" MaxLength="50"></asp:TextBox>
                 <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="SmallButton3" 
                     Visible="False" onclick="Btn_Save_Click" />
                      <asp:Button ID="btnback2" runat="server" Text="Back" CssClass="SmallButton3" 
                    onclick="btnback_Click" />
            </td>
            <td align="right">
             <asp:Button ID="btn_close" runat="server" Text="Close" CssClass="SmallButton3" onclick="btn_close_Click" 
                     />

            </td>
        </tr>
        <tr>
            <td colspan="3">
         <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
            Width="650px" DataKeyNames="dmd_IsMenu,dmd_id" onrowdatabound="gv_RowDataBound" 
                    onrowcancelingedit="gv_RowCancelingEdit" onrowediting="gv_RowEditing" 
                    onrowdeleting="gv_RowDeleting" onrowupdating="gv_RowUpdating">
            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
            <RowStyle CssClass="GridViewRowStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <HeaderStyle CssClass="GridViewHeaderStyle" />
            <EmptyDataTemplate>
                <asp:Table ID="Table3" Width="850px" CellPadding="-1" CellSpacing="0" runat="server"
                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                    <asp:TableRow>
                        <asp:TableCell Text="Owner" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Date" Width="60px" HorizontalAlign="center"></asp:TableCell>
                        <asp:TableCell Text="Message" Width="610px" HorizontalAlign="center"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField DataField="dmd_menuName" HeaderText="menuName">
                    <HeaderStyle Width="500px" HorizontalAlign="Center" />
                    <ItemStyle Width="500px" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundField>

                    <asp:TemplateField HeaderText="IsMenu">
                        <ItemTemplate>
                          
                            <asp:CheckBox ID="ckb_ismenu2" runat="server" Checked="True" Text="isMenu" Enabled="false"/>
                        </ItemTemplate>
                         <EditItemTemplate>
                                     <asp:CheckBox ID="ckb_ismenu3" runat="server" Checked="True" Text="isMenu" Enabled="true"/>
                            </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                    </asp:TemplateField>

                     <asp:CommandField ShowEditButton="True" CancelText="<u>取消</u>" DeleteText="<u>删除</u>"
                                    EditText="<u>编辑</u>" UpdateText="<u>更新</u>">
                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                                </asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True" DeleteText="<u>删除</u>">
                                    <HeaderStyle Width="40px" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ControlStyle Font-Bold="False" Font-Size="12px" />
                                </asp:CommandField>
               

            </Columns>
        </asp:GridView>
            </td>
        </tr>
    </table>
    </div>
    </form>
      <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
