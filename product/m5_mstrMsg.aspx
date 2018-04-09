<%@ Page Language="C#" AutoEventWireup="true" CodeFile="m5_mstrMsg.aspx.cs" Inherits="product_m5_mstrMsg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js?ver=20150908" type="text/javascript"></script>
     <script language="JavaScript" type="text/javascript">
         $(function () {
             var txtPending = $("#txtUserNoInput");
             txtPending.load(txtval());
             txtPending.click(
             function () {
                 txtPending.val() == "No. OR Name";
                 txtcl();

             }
             );

             function txtval() {
                 if (txtPending.val() == "") {

                     txtPending.val("No. OR Name");
                     txtPending.attr("color", "#AAAAAA");

                 }
             }
             function txtcl() {

                 txtPending.val("");

             }

         

           

            



         });
    
         function CheckBoxList_Click(sender) 
         { 
                
             var container = sender.parentNode; 
             //if(container.tagName.toUpperCase() == "TD") { // 服务器控件设置呈现为 table 布局（默认设置），否则使用流布局 
             //    container = container.parentNode.parentNode; // 层次： <table><tr><td><input /> 
             //} 
             var chkList = container.getElementsByTagName("input"); 
             var senderState = sender.checked; 
             for(var i=0; i<chkList.length;i++) { 
                 chkList[i].checked = false; 
             } 
             sender.checked = senderState; 
            
         } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td>NO</td>
                <td>
                    <asp:Label ID="labNo" runat="server" Text=""></asp:Label>
                </td>
                 <td>Market</td>
                <td><asp:DropDownList id="ddlMarket" runat="server" Width="200"  DataTextField="m5mk_name" DataValueField="m5mk_ID" CssClass="Param"   ></asp:DropDownList></td><%--OnSelectedIndexChanged="ddlMarket_SelectedIndexChanged" AutoPostBack="true"--%>
            </tr>
               
            
            <tr>
                <td>Level</td>
                <td>  
                    <asp:DropDownList ID ="ddlLevel" DataTextField ="soque_degreeName" DataValueField="soque_did" runat="server" >
                          
                         </asp:DropDownList>
                </td>
                <td>ModelNo.
                </td>
                <td>
                    <asp:TextBox ID="txtModelNo" runat="server" CssClass="SmallTextBox5"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td>Excutive Date</td>
                <td>
                    <asp:TextBox ID="txtEffDate" runat="server" CssClass="Date" Width="80px"></asp:TextBox>
                </td>
                <td>AboutBoom </td>
                <td> <asp:CheckBoxList ID="chklAboutBoom" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" >
                            <asp:ListItem Text="keep same" Value="0" onclick="CheckBoxList_Click(this);" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="modify bom" Value="10" onclick="CheckBoxList_Click(this);"></asp:ListItem>
                            <asp:ListItem Text="new bom" Value="20" onclick="CheckBoxList_Click(this);"></asp:ListItem>
                        </asp:CheckBoxList></td>
            <tr>
                <td>Excutor</td>
                <td>
                    <asp:Label ID="labUExcutor" runat="server" Text=""></asp:Label></td>
                <td colspan="2">
                  <%--  <asp:DropDownList ID="dropDomain" runat="server" Width="60px" Height="19px" AutoPostBack="True" OnSelectedIndexChanged="dropDomain_SelectedIndexChanged">
                        <asp:ListItem>--Company--</asp:ListItem>
                        <asp:ListItem Value="1">SZX</asp:ListItem>
                        <asp:ListItem Value="2">ZQL</asp:ListItem>
                        <asp:ListItem Value="5">YQL</asp:ListItem>
                        <asp:ListItem Value="8">HQL</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropDept" runat="server" Width="150px" Height="19px" AutoPostBack="True" DataTextField="name" DataValueField="departmentID" OnSelectedIndexChanged="dropDept_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="dropUser" runat="server" Width="90px" Height="19px" DataTextField="userName" DataValueField="userID">
                    </asp:DropDownList>--%>
                    
                    <asp:TextBox ID="txtUserNoInput" runat="server" Width="90px" CssClass="User"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txtUserNo" runat="server" Width="50px" BackColor="#CCCCCC" CssClass="UserNoOutput"
                        Style="ime-mode: disabled" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txtUserName" runat="server" Width="50px" BackColor="#CCCCCC" CssClass="UserNameOutput"
                        Style="ime-mode: disabled" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txtUserDept" runat="server" Width="100px" BackColor="#CCCCCC" CssClass="UserDeptOutput"
                        Style="ime-mode: disabled" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txtUserRole" runat="server" Width="100px" BackColor="#CCCCCC" CssClass="UserRoleOutput"
                        Style="ime-mode: disabled" onkeydown="return false;" onpaste="return false;"></asp:TextBox>
                    &nbsp;
                    <asp:TextBox ID="txtUserDomain" runat="server" Width="50px" BackColor="#CCCCCC" CssClass="UserDomainOutput"
                        Style="ime-mode: disabled" onkeydown="return false;" 
                        onpaste="return false;"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnChoseExcutor" runat="server" Width="80px" CssClass ="SmallButton2" Text="Chose Excutor" OnClick="btnChoseExcutor_Click"/>
                </td>
            </tr>
            <tr>
                <td>Message:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" Width="700px" Height="200px" MaxLength="500"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Attachment:</td>
                <td colspan="3">
                    <input id="fileAgree" style="width: 90%; height: 23px" type="file" size="45" name="filename1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="btnSave" runat="server" Text="Save"   CssClass="SmallButton2" OnClick="btnSave_Click" OnClientClick="return btnSaveCheckMarket()"/>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidEffDate" runat="server" />
        <asp:HiddenField ID="hidLevel" runat="server" />
        <asp:HiddenField ID="hidModelNo" runat="server" />
        <asp:HiddenField ID="hidMarket" runat="server" />
    </div>
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>

        //var btnsave = $("#btnSave");
        //btnsave.click(btnSaveCheckMarket());


        function   btnSaveCheckMarket()
        {

            var oldMarket = $("#hidMarket").val();
            var newMarket = $("#ddlMarket").val();
            if(oldMarket != newMarket)
            {
                    
                return confirm('The market be change! Do you want change the market?');
            }
            return true;
                 
             
        }
    </script>
</body>
</html>
