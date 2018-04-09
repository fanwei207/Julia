<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_exam_Show.aspx.cs" Inherits="Test_Test_exam_Show" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 1100px; position:fixed; left:0; top:0;">
            <table cellspacing="0" cellpadding="0" style="width: 1100px; position:fixed; left:0; top:0;">

                <tr>


                    <asp:Label ID="lblid" runat="server" Text="Label" Visible="false"></asp:Label>
                    <asp:Label ID="second" runat="server" Text="Label"></asp:Label>
                    <br />
                     <asp:Label ID="min" runat="server" Text="Label" ForeColor="White"></asp:Label>

                </tr>
            </table>
        </div>
        <div align="center">
            

            <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                AllowPaging="True" Width="900px" DataKeyNames="ques_id,ques_type_id,scores"
                OnRowCommand="gvMessagereply_RowCommand" OnPageIndexChanging="gvMessagereply_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
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
                    <asp:BoundField DataField="_no" HeaderText="序号">
                        <HeaderStyle Width="100px" HorizontalAlign="Center" />
                        <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>

                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("ques_title") %>' Style="margin-top: 10px; padding-top: 10px;"></asp:Label>
                            <br />
                            <br />
                            <hr align="left" style="width: 100%; border-top: 1px dashed #000; border-bottom: 0px dashed #000; height: 0px">

                            <asp:RadioButtonList ID="radType" runat="server" RepeatDirection="Horizontal" DataTextField="name" DataValueField="id">
                                <%-- <asp:ListItem Selected="True">新增供应商验厂</asp:ListItem>
                            <asp:ListItem>周期验厂</asp:ListItem>--%>
                            </asp:RadioButtonList>
                            <asp:CheckBoxList ID="ckbtype" runat="server" RepeatDirection="Horizontal" DataTextField="name" DataValueField="id">
                            </asp:CheckBoxList>


                        </ItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                        <ControlStyle Font-Bold="False" Font-Size="12px" />
                        <HeaderStyle HorizontalAlign="Left" Width="700px" />
                        <ItemStyle HorizontalAlign="Left" Width="700px" Height="100px" VerticalAlign="Top"
                            Font-Bold="False" />
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <table cellspacing="0" cellpadding="0" style="width: 1100px; text-align: center;">

                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="提交" OnClick="btnSave_Click" OnClientClick="oneclick();"
                            CssClass="" Width="100px" Height="50px" />
                    </td>



                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript">
        var hour = 0
        var min = document.getElementById("second").innerHTML;
        document.getElementById("min").innerHTML = document.getElementById("second").innerHTML;
        while (min >= 60)
        {
            min=min-60
            hour++
        }
               var second = 0;
               var timer;
               function change() {
                   second--;
                   if (hour > -1) {
                       if (min > -1) {

                           if (second > -1) {
                               document.getElementById("second").innerHTML = hour+":"+ min + ":" + second;
                               timer = setTimeout('change()', 1000);//调用自身实现
                           }
                           else {
                               min--;
                               second = 59
                               document.getElementById("second").innerHTML = hour + ":" + min + ":" + second;
                               document.getElementById("min").innerHTML = document.getElementById("min").innerHTML -1
                               timer = setTimeout('change()', 1000);//调用自身实现

                           }
                       }
                       else {
                           hour--;
                           min = 59
                           document.getElementById("second").innerHTML = hour + ":" + min + ":" + second;
                           timer = setTimeout('change()', 1000);//调用自身实现
                       }
                   }
                   else {
                       clearTimeout(timer);
                   }
               }
               timer = setTimeout('change()', 1000);
    </script>
</body>
</html>
