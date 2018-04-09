<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MF_det.aspx.cs" Inherits="IT_MF_det" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
     <script language="JavaScript" type="text/javascript">

         $(function () {

             $(".GridViewStyle").prop("cellpadding", "5");


         })
   
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div >
           
     <table style="width: 900px">
        <tr>
            <td style="width:400" align="right">
               Title：
            </td>
            <td align="left">
              
                <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                 Authorize：
            </td>
            <td align="left">
            
               <asp:Label ID="lblAuthorize" runat="server" Text="Label"  Width="700"></asp:Label>
            
                <asp:Button ID="btn_new" runat="server" Text="Back" CssClass="SmallButton2" 
                            Width="35px" onclick="btn_new_Click"  />

                             &nbsp;

                             <asp:Button ID="btn_newitem" runat="server" Text="New" CssClass="SmallButton2" 
                            Width="35px" onclick="btn_newitem_Click"   />
            </td>
        </tr>
         <tr>
            <td align="right">
                Decription：
            </td>
            <td align="left">
              <asp:TextBox ID="txtDecription" runat="server" TextMode="MultiLine" Width="820px" Height="60px"
                        MaxLength="200"></asp:TextBox>
               
            
            </td>
        </tr>

       
         <tr>
            <td align="right">
               KeyWords：
            </td>
            <td align="left">
                <asp:TextBox ID="txtkey1" runat="server" Width="820px"></asp:TextBox>
            
            </td>
        </tr>

       
     </table>
    
         <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild"
                AllowPaging="True" Width="900px" DataKeyNames="FM_id" 
                onpageindexchanging="gv_PageIndexChanging" onrowcommand="gv_RowCommand" 
               >
                <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                <RowStyle CssClass="GridViewRowStyle" />
                <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                <HeaderStyle CssClass="GridViewHeaderStyle" />
                <EmptyDataTemplate>
                    <asp:Table ID="Table3" Width="980px" CellPadding="-1" CellSpacing="0" runat="server"
                        CssClass="GridViewHeaderStyle" GridLines="Vertical">
                        <asp:TableRow>
                            <asp:TableCell Text="Owner" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Date" Width="60px" HorizontalAlign="center"></asp:TableCell>
                            <asp:TableCell Text="Message" Width="610px" HorizontalAlign="center"></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="FM_Step" HeaderText="Step">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Label ID="lbltitle" runat="server" Text='<%# Bind("FM_title") %>'  ></asp:Label>
                              --
                            <asp:Label ID="lbldepart" runat="server" Text='<%# Bind("FM_depart") %>'  ></asp:Label>


                            <hr align="left" style="width: 100%; border-top: 1px dashed #000; border-bottom: 0px dashed #000;
                                height: 0px">
                          
                            <asp:Label ID="lbldesc" runat="server" Text='<%# Bind("FM_desc") %>' Style="word-break: break-all"
                                Width="600px" Font-Size="Medium"></asp:Label>
                            <br />
                            <br />
                             <hr align="left" style="width: 100%; border-top: 1px dashed #000; border-bottom: 0px dashed #000;
                                height: 0px">
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("createby") %>' Width="650px" ></asp:Label>
                                 <asp:LinkButton ID="LinkButton3" runat="server" Font-Bold="False"  Width="650px" Visible='<%# Bind("FM_isReview") %>'
                                CommandName="review" Font-Underline="True" Text='<%# Bind("isReview") %>'></asp:LinkButton>

                          
                           

                          <%--  <asp:LinkButton ID="Download1" runat="server" Font-Bold="False" 
                                CommandName="New" Font-Underline="True" Text='<%# Bind("isnew") %>'></asp:LinkButton>--%>

                                 <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="False" 
                                CommandName="Change" Font-Underline="True" Text='<%# Bind("ischange") %>'></asp:LinkButton>

                                  <asp:LinkButton ID="LinkButton2" runat="server" Font-Bold="False" 
                                CommandName="Save" Font-Underline="True" Text='<%# Bind("issave") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="12px" />
                        <HeaderStyle HorizontalAlign="Left" Width="700px" />
                        <ItemStyle HorizontalAlign="Left" Width="700px" Height="100px" VerticalAlign="Top"
                            Font-Bold="False" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
          
            <br />
        </div>
    </form>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>

