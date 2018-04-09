<%@ Page Language="vb" AutoEventWireup="false" Inherits="tcpc.partlist" CodeFile="partlist.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function(){
            $("#txtPartCode").focus(function(){
                if($("#txtPartCode").val()=="请输入部件号")
                {
                    $("#txtPartCode").val("");
                }
                return true;
            });
        })
    </script>
</head>
<body>
    <div align="left">
        <form id="Form1" method="post" runat="server">
        <table cellspacing="1" cellpadding="1" width="1100px" bgcolor="white" border="0">
            <tr>
                <td align="left">
                    部件号
                    <asp:TextBox ID="txtPartCode" runat="server" Width="153px" MaxLength="50" 
                        CssClass="SmallTextBox">请输入部件号</asp:TextBox>&nbsp;
                    QAD号<asp:TextBox ID="txtQad" runat="server" Width="95px" MaxLength="50" 
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp;分类
                    <asp:TextBox ID="txtCategory" runat="server" Width="100px" MaxLength="30" CssClass="SmallTextBox"></asp:TextBox>&nbsp;描述
                    <asp:TextBox ID="txtDesc" TabIndex="0" runat="server" Width="100px" MaxLength="255"
                        CssClass="SmallTextBox"></asp:TextBox>&nbsp; 
                    <asp:RadioButton ID="radNormal" runat="server" Text="使用" AutoPostBack="True" Checked="True"
                        GroupName="RadGroup"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="radTry" runat="server" Text="试用" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup"></asp:RadioButton>&nbsp;
                    <asp:RadioButton ID="radStop" runat="server" Text="停用" AutoPostBack="True" Checked="false"
                        GroupName="RadGroup"></asp:RadioButton>&nbsp;
                         <asp:Button ID="BtnQuery" runat="server" CssClass="SmallButton3" Text="查询" 
                        Width="48px"></asp:Button>
                </td>
                <td align="right">
                    <asp:Label ID="lblCount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="BtnReplace" runat="server" CssClass="SmallButton3" Text="替换名字" Width="60">
                    </asp:Button>&nbsp;<asp:Button ID="BtnAdd" runat="server" CssClass="SmallButton3" Text="增加"></asp:Button>
                    <asp:Button ID="ButExcel" runat="server" CssClass="SmallButton3" 
                        Text="Excel" Width="60px" onclick="ButExcel_Click"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:DataGrid ID="DataGrid1" runat="server" Width="2040px" AutoGenerateColumns="False"
            HeaderStyle-Font-Bold="false" PagerStyle-HorizontalAlign="Center" AllowPaging="True"
            PageSize="25" PagerStyle-Mode="NumericPages" PagerStyle-BackColor="#99ffff" PagerStyle-Font-Size="12pt"
            PagerStyle-ForeColor="#0033ff" CssClass="GridViewStyle GridViewRebuild">
            <ItemStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundColumn Visible="False" DataField="partid" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="gsort" SortExpression="gsort" ReadOnly="True" Visible="False"
                    HeaderText="<b>序号</b>">
                    <HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="code" SortExpression="gsort" HeaderText="<b>部件号</b>">
                    <HeaderStyle Width="220px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="220px" HorizontalAlign="Left"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="item_qad" SortExpression="gsort" HeaderText="<b>部件QAD号</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="category" SortExpression="category" HeaderText="<b>分类</b>">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="minQty" SortExpression="qty" HeaderText="<b>最小库存量</b>">
                    <HeaderStyle Width="90px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="unitName" SortExpression="unitName" HeaderText="<b>单位</b>">
                    <HeaderStyle Width="60px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="60px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TranUnit" SortExpression="TranUnit" HeaderText="<b>转换前单位</b>">
                    <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Tran_Rate" SortExpression="TranRate" HeaderText="<b>转换系数</b>">
                    <HeaderStyle Width="80px" HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                </asp:BoundColumn>
                <asp:ButtonColumn Text="<u>编辑</u>" HeaderText="<b>编辑</b>" CommandName="EditBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px" HorizontalAlign="Center">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>用于</u>" HeaderText="<b>用于</b>" CommandName="UsedByBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px" HorizontalAlign="Center">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>供货</u>" HeaderText="<b>供货</b>" CommandName="SupplyBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px" HorizontalAlign="Center">
                    </HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                    <asp:ButtonColumn Text="<u>尺寸</u>" HeaderText="尺寸" CommandName="SizeByBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>文档</u>" HeaderText="<b>文档</b>" CommandName="DocByBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>转产品</u>" HeaderText="<b>转产品</b>" CommandName="ToProdBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="50px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:ButtonColumn Text="<u>历史</u>" HeaderText="<b>历史</b>" CommandName="HisBtn">
                    <HeaderStyle Font-Size="8pt" Font-Names="Tahoma,Arial" Width="30px"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:ButtonColumn>
                <asp:BoundColumn DataField="description" SortExpression="description" HeaderText="<b>部件描述</b>">
                    <HeaderStyle Width="1200px" HorizontalAlign="Left"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left" Width="1200px"></ItemStyle>
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
        </asp:DataGrid></form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
