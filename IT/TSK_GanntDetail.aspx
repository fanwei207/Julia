<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TSK_GanntDetail.aspx.cs"
    Inherits="TSK_GanntDetail" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
   <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="Left">
            <asp:Label ID="Label1" runat="server" Font-Size="12pt" Text=""></asp:Label>

        <table >
            
                <tr align="center">
                    <td align="left">
                        目标时间：
                        <asp:TextBox ID="txt_date" runat="server"  CssClass="Date Param" Width="90px" ></asp:TextBox>
                        目标内容：
                        <asp:TextBox ID="txt_dec" runat="server"  CssClass="Param" Width="290px"></asp:TextBox>
                          <asp:Button ID="btn_update" runat="server" Text="新增目标" OnClick="btn_update_Click"  CssClass="SmallButton2"/>
                    </td>
                 
                    <td>
                       
                    </td>
                    
                </tr>
             
           
                <tr>
                    <td>
                      <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        PageSize="20" AllowPaging="True" 
           
                        >
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
         
        </asp:GridView>
                     </td>
                    <td>

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
