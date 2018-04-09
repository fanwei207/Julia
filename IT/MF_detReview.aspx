<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MF_detReview.aspx.cs" Inherits="IT_MF_detReview" %>

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
   <script type="text/javascript">
       var last = null; //最后访问的RadioButton的ID
       function judge(obj) {

           if (last == null) {

               last = obj.id;

               //      alert(last);

           }

           else {

               var lo = document.getElementById(last);

               lo.checked = "";

               //        alert(last + "   " + lo.checked);

               last = obj.id;

           }

           obj.checked = "checked";

          

           //dealid.value = obj.;

       }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
       
         <table style="width: 770px">
        <tr>
            <td style="width:200" align="right">
               Title：
            </td>
            <td align="left">
                <asp:TextBox ID="txtTitle" runat="server" Width="600px" MaxLength="50"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td align="right">
                 responsible：
            </td>
            <td align="left">
            
              
            
                <asp:TextBox ID="txtresponsible" runat="server"></asp:TextBox>
            
                <asp:Label ID="lblstep" runat="server" Text="0" Visible="False"></asp:Label>
            
            </td>
        </tr>
         <tr>
            <td align="right">
                Decription：
            </td>
            <td align="left">
              <asp:TextBox ID="txtDecription" runat="server" TextMode="MultiLine" Width="600px" Height="60px"
                        MaxLength="200"></asp:TextBox>
               
            
            </td>
        </tr>

     </table>
     <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                AllowPaging="True" Width="700px" DataKeyNames="FM_id" 
            onpageindexchanging="gv_PageIndexChanging" onrowcommand="gv_RowCommand" onrowdatabound="gv_RowDataBound" 
              
               >
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="700px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                           
                            
                            <asp:TableCell Text="NULL Information" Width="610px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                   

                     <asp:TemplateField HeaderText="Step">
                      <ItemTemplate>
                        <asp:Label ID="lblstep" runat="server" Text='<%# Bind("FM_Step") %>'  Width="80px"></asp:Label>
                        <asp:Label ID="lblid" runat="server" Text='<%# Bind("FM_id") %>'  Width="80px" Visible="false"></asp:Label>
                          <br>
                        </br>
                        <asp:RadioButton ID="raButton" runat="server" CommandName="Review">


                    </asp:RadioButton>
                      </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="12px" />
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                        <ItemStyle HorizontalAlign="Center" Width="100px" Height="100px" VerticalAlign="Top"
                            Font-Bold="False" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                           
                            <asp:Label ID="lbldepart" runat="server" Text='<%# Bind("FM_depart") %>'  Width="80px"></asp:Label>

                                |
                             <asp:Label ID="lbltitle" runat="server" Text='<%# Bind("FM_title") %>'  ></asp:Label>

                            <hr align="left" style="width: 100%; border-top: 1px dashed #000; border-bottom: 0px dashed #000;
                                height: 0px">
                          
                            <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("FM_desc") %>' Style="word-break: break-all"
                                Width="600px" Font-Size="Medium"></asp:Label>
                            <br />
                            <br />
                             <hr align="left" style="width: 100%; border-top: 1px dashed #000; border-bottom: 0px dashed #000;
                                height: 0px">
                                

                          <asp:Label ID="Label4" runat="server" Text='<%# Bind("createby") %>'  Width="650px"></asp:Label>
                           

                          <%--  <asp:LinkButton ID="Download1" runat="server" Font-Bold="False" 
                                CommandName="New" Font-Underline="True" Text='<%# Bind("isnew") %>'></asp:LinkButton>--%>

                                
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="12px" />
                        <HeaderStyle HorizontalAlign="Left" Width="700px" />
                        <ItemStyle HorizontalAlign="Left" Width="700px" Height="100px" VerticalAlign="Top"
                            Font-Bold="False" />
                    </asp:TemplateField>
                </Columns>
                
            </asp:GridView>
            <asp:Button ID="btnsave" runat="server" Text="Save" onclick="btnsave_Click" 
                         Height="26px"  CssClass="SmallButton2" />
    </div>
       <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    
    </form>
       </body>
</html>