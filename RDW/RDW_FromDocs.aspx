<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RDW_FromDocs.aspx.cs" Inherits="RDW_RDW_FromDocs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
</head>
<body>
    <div align="center">
        <form id="Form1" method="post" runat="server">
            <table id="tbID_DocAdd" runat="server" style="text-align: center; width: 750px;">
            <tr style="list-style-type: none; border-style: solid none none none; border-top-width: thin;
                border-top-color: #000000">
                <td align="left">
                    <asp:Label ID="Label3" runat="server" Text="Please Select Category and Type:" ForeColor="#0000ff"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="BtnBack" runat="server" CssClass="SmallButton2" Text="Back" 
                        Width="40px" onclick="BtnBack_Click" ></asp:Button>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    Category<asp:DropDownList ID="ddl_Type" runat="server" Width="180px" AutoPostBack="True"
                        DataTextField="typename" DataValueField="typeid" OnSelectedIndexChanged="ddl_Type_SelectedIndexChanged">
                    </asp:DropDownList>
                    Type<asp:DropDownList ID="ddl_Category" runat="server" Width="180px" AutoPostBack="True"
                        DataTextField="catename" DataValueField="cateid" OnSelectedIndexChanged="ddl_Category_SelectedIndexChanged">
                    </asp:DropDownList>
                    Key Word<asp:TextBox ID="txt_KeyWordSearch" runat="server" CssClass="SmallTextBox"
                        Width="200px"></asp:TextBox>(*)&nbsp;&nbsp;
                    <asp:Button ID="btn_DocSearch" runat="server" CssClass="SmallButton2" Text="Search" Width="40px"
                        OnClick="btn_DocSearch_Click"></asp:Button>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:GridView ID="gv_allDoc" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CssClass="GridViewStyle" OnPageIndexChanging="gv_allDoc_PageIndexChanging" OnRowCommand="gv_allDoc_RowCommand"
                        PageSize="15" DataKeyNames="id,accFileName,typeid,cateid,Path,filename" Width="750px">
                        <FooterStyle CssClass="GridViewFooterStyle" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <Columns>
                            <asp:BoundField DataField="typeid" HeaderText="cateid" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="cateid" HeaderText="typeid" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="id" HeaderText="docid" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="filepath" HeaderText="FilePath" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="typename" HeaderText="Category">
                                <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="catename" HeaderText="Type">
                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="name" HeaderText="DocName">
                                <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fileName" HeaderText="FileName">
                                <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="version" HeaderText="Ver">
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                            </asp:BoundField>
                            <asp:ButtonField Text="Add" HeaderText="" CommandName="myAddlink" ControlStyle-Font-Underline="true">
                                <HeaderStyle Width="50px" HorizontalAlign="Center" Font-Bold="false" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" ForeColor="Black" />
                            </asp:ButtonField>
                        </Columns>
                    </asp:GridView>
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
