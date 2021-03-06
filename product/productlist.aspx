<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.productlist" CodeFile="productlist.aspx.vb" %>

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
        $(function(){
            $("#txtCode").focus(function(){
                if($("#txtCode").val()=="请输入产品型号")
                {
                    $("#txtCode").val("");
                }
                return true;
            });
        })
    </script>
</head>
<body>
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" border="0" width="1100px">
            <tr>
                <td align="left">
                    产品型号
                    <asp:TextBox ID="txtCode" TabIndex="0" runat="server" Width="160px" MaxLength="50"
                        CssClass="SmallTextBox Param">请输入产品型号</asp:TextBox>&nbsp;QAD号
                    <asp:TextBox ID="txtQad" TabIndex="0" runat="server" Width="100px" MaxLength="30"
                        CssClass="SmallTextBox Param"></asp:TextBox>&nbsp;分类
                    <asp:TextBox ID="txtCategory" TabIndex="0" runat="server" Width="100px" MaxLength="30"
                        CssClass="SmallTextBox Param"></asp:TextBox>&nbsp;描述
                    <asp:TextBox ID="txtDesc" TabIndex="0" runat="server" Width="100px" MaxLength="255"
                        CssClass="SmallTextBox Param"></asp:TextBox>&nbsp;
                    <asp:RadioButton ID="radNormal" runat="server" Text="使用" AutoPostBack="True" Checked="True"
                        GroupName="RadGroup" CssClass="Param"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="radTry" runat="server" Text="试用" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup" CssClass="Param"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="radStop" runat="server" Text="停用" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup" CssClass="Param"></asp:RadioButton>&nbsp;
                    <asp:Button ID="btnQuery" TabIndex="0" runat="server" CssClass="SmallButton3" 
                        Text="查询" Width="46px">
                    </asp:Button>&nbsp;&nbsp;
                    <asp:Label ID="lblCount" runat="server" Width="80px"></asp:Label>&nbsp;&nbsp;
                    <asp:Button ID="BtnReplace" TabIndex="0" runat="server" CssClass="SmallButton3" Text="替换名字"
                        Width="58px"></asp:Button>&nbsp;
                    <asp:Button ID="btnAddNew" runat="server" CssClass="SmallButton3" Text="增加" 
                        Width="41px"></asp:Button>
                    &nbsp;
                    <asp:Button ID="BtnExport" runat="server" CssClass="SmallButton3" Text="导出" 
                        Width="49px">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="dgProduct" runat="server" Width="2200px" PagerStyle-Mode="NumericPages"
            PageSize="22" AllowPaging="True" AutoGenerateColumns="False" CssClass="GridViewStyle GridViewRebuild Param">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" Mode="NumericPages" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="productID" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn Visible="False" DataField="gsort" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="code" SortExpression="gsort" HeaderText="<b>型号</b>">
                    <HeaderStyle Width="250px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="qad" SortExpression="qad" HeaderText="<b>QAD号</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="category" SortExpression="category" HeaderText="<b>分类</b>">
                    <HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="status" SortExpression="status" HeaderText="<b>状态</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundColumn>
                 <asp:BoundColumn DataField="logs" SortExpression="logs" HeaderText="<b>锁定</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                   <asp:BoundColumn DataField="ulmd" SortExpression="ulmd" HeaderText="<b>锁定信息</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="240px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="240px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>编辑</u>" HeaderText="编辑" CommandName="EditBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>结构</u>" HeaderText="结构" CommandName="StruBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>用于</u>" HeaderText="用于" CommandName="UsedByBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>供货</u>" HeaderText="供货" CommandName="SupplyBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px" HorizontalAlign="Center">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>尺寸</u>" HeaderText="尺寸" CommandName="SizeByBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>文档</u>" HeaderText="文档" CommandName="DocByBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>转部件</u>" HeaderText="<b>转部件</b>" CommandName="ToPartBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>历史</u>" HeaderText="历史" CommandName="HisBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="shipGroup" SortExpression="shipGroup" HeaderText="<b>出运系列</b>">
                    <HeaderStyle HorizontalAlign="center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Customer" SortExpression="Customer" HeaderText="<b>所属客户</b>">
                    <HeaderStyle HorizontalAlign="center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Simple" SortExpression="Simple" HeaderText="<b>产品简称</b>">
                    <HeaderStyle HorizontalAlign="center" Width="100px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="left" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="description" SortExpression="description" HeaderText="<b>描述</b>">
                    <HeaderStyle HorizontalAlign="Left" Width="1340px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="1340px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createddate1" SortExpression="createddate1" HeaderText="<b>创建日期</b>">
                    <HeaderStyle HorizontalAlign="center" Width="120px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="120px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="createdby1" SortExpression="createdby1" HeaderText="<b>创建人</b>">
                    <HeaderStyle HorizontalAlign="center" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="center" Width="50px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn Visible="true" ReadOnly="True"></asp:BoundColumn>
            </Columns>
        </asp:DataGrid>
        </form>
    </div>
    <script>
          <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
