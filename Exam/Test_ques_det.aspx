<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test_ques_det.aspx.cs" Inherits="Test_Text_ques_det" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
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
    <div align="center">
        <form id="form1" runat="server">
        <div style="background-color: #d8d8d8; width: 804px; margin-top: 4px; padding-top: 2px;
            padding-bottom: 2px;">
            <table cellspacing="0" cellpadding="2" bgcolor="#f4f4f4" style="width: 800px; margin: auto;
                border: 3px solid white;">
                <tr>
                    <td>
                        题目类型</td>
                    <td>
                       <asp:DropDownList ID="ddlStatu" runat="server" DataTextField="type_name" DataValueField ="type_id"
                            Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                       所属模块

                         <asp:DropDownList ID="ddl_category" runat="server" DataTextField="category_name" DataValueField ="category_id"
                            Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                          开始时间
                        <asp:TextBox ID="txtstratdate" runat="server" CssClass="smalltextbox Date" Width="100px"></asp:TextBox>
                        <asp:Label ID="lblid" runat="server" Text=""  Visible="false"></asp:Label>

                        <asp:CheckBox ID="ckb_key" Text="必考题" runat="server" />

                    </td>
                    <td>
                       截止时间
                    </td>
                    <td>
                          <asp:TextBox ID="txtenddate" runat="server" CssClass="smalltextbox Date" Width="100px"></asp:TextBox>
                    </td>
                   
                </tr>
                <tr>
                    <td >
                       题目
                    </td >
                    <td colspan ="5">
                       <asp:TextBox ID="txttitle" runat="server" Height="60px" TextMode="MultiLine" 
                        Width="100%" MaxLength="80"></asp:TextBox>
                    </td>
                   
                </tr>
                  <tr>
                    <td >
                       答案
                    </td >
                    <td colspan ="5">
                       <asp:TextBox ID="txtanswer" runat="server" Height="60px" TextMode="MultiLine" 
                        Width="100%" MaxLength="80"></asp:TextBox>
                    </td>
                   
                </tr>
                <tr>
                    <td>
                       
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnSaveHrd" runat="server" CssClass="SmallButton2" OnClick="btnSaveHrd_Click"
                            Text="Save" Width="66px" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" OnClick="btnBack_Click"
                            Text="Back" Width="66px" />
                    </td>
                    <td>
                       
                    </td>
                   
                </tr>
                
                <tr>
                <td colspan="8" valign="top" align="center">
                    &nbsp;</td>
            </tr>
                <tr>
                    <td style="height: 9px">
                       
                         选项 
                      

                     
                    </td>
                    <td>
                        <asp:TextBox ID="txtmark" runat="server" CssClass="smalltextbox" Width="66px"
                            MaxLength="4"></asp:TextBox>
                    </td>
                    <td>
                        内容
                    </td>
                    <td colspan ="2">
                          <asp:TextBox ID="txtdetil" runat="server" CssClass="smalltextbox"
                                Width="300px" MaxLength="30"></asp:TextBox>
                    </td>
                    <td>
                           
                        <asp:Button ID="btnSaveLine" runat="server" CssClass="SmallButton2" OnClick="btnSaveLine_Click"
                            Text="Save Line" Width="78px" Enabled="False" Height="22px" />
                    </td>
                </tr>
                
                <tr align ="center">
                    <td colspan="6" style="vertical-align: top; text-align:center;">
                        <asp:GridView ID="gvlist" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            DataKeyNames="quesd_id" OnPageIndexChanging="gvlist_PageIndexChanging"
                            OnRowDataBound="gvlist_RowDataBound" OnRowDeleting="gvlist_RowDeleting" PageSize="20"
                            OnRowCancelingEdit="gvlist_RowCancelingEdit" OnRowEditing="gvlist_RowEditing"
                            OnRowUpdating="gvlist_RowUpdating" CssClass="GridViewStyle AutoPageSize">
                            <RowStyle CssClass="GridViewRowStyle" />
                            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <FooterStyle CssClass="GridViewFooterStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                             <EmptyDataTemplate>
                                <asp:Table ID="Table3" Width="800px" CellPadding="-1" CellSpacing="0" runat="server"
                                    CssClass="GridViewHeaderStyle" GridLines="Vertical">
                                    <asp:TableRow>
                                        <asp:TableCell Width="100%" HorizontalAlign="center">无数据</asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField HeaderText="选项" DataField="quesd_mark" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:BoundField>
                                 <asp:BoundField DataField="quesd_detil" HeaderText="内容" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                                </asp:BoundField>
                                 
                                
                               
                              
                                <asp:CommandField ShowDeleteButton="True" DeleteText="Del">
                                    <ControlStyle Font-Bold="False" Font-Size="11px" Font-Underline="True" ForeColor="Black" />
                                    <HeaderStyle Width="30px" />
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:CommandField>
                                 
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </div>
    <script>
        <asp:Literal ID="ltlAlert" runat="server" EnableViewState=false></asp:Literal>
    </script>
</body>
</html>
