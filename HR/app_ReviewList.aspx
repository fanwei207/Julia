<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app_ReviewList.aspx.cs" Inherits="HR_app_ReviewList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    <form id="form1" runat="server">
    <div align="center" style="margin-top:20px;">
        <table>
            <tr>
                <td align="right">公司：</td><td><asp:TextBox ID="txtCop" runat="server"></asp:TextBox></td>
                <td style=" width:60px;" align="right">部门：</td><td><asp:TextBox ID="txtDepart" runat="server"></asp:TextBox></td>
                <td style=" width:60px;" align="right">岗位：</td><td><asp:TextBox ID="txtProc" runat="server"></asp:TextBox></td> 
                <td><asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click"  
                        CssClass="SmallButton2"/></td>              
            </tr>
        </table>
        <table>
            <tr>
                <td style="width:100px;">审 批 意 见：　　(限500字以内)</td>
                <td><asp:TextBox ID="txtReviewOpinion" runat="server" Width="580" Height="100" CssClass="SmallTextBox"
                        TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td>提交给下一审批人</td>
                <td>
                    <asp:TextBox ID="txtChooseName" runat="server" Width="520px"></asp:TextBox>
                    <asp:TextBox ID="txtChooseEmail" runat="server" Visible="True" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtChooseid" runat="server" Visible="True" Style="display: none"></asp:TextBox>
                    <asp:Button ID="btnNextReview" runat="server" Text="选择审批人" 
                        CssClass="SmallButton2" onclick="btnNextReview_Click" /></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSubmit" runat="server" Text="提交" CssClass="SmallButton2" 
                        onclick="btnSubmit_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnRefuse" runat="server" Text="拒绝" CssClass="SmallButton2" 
                        onclick="btnRefuse_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnProcMaintain" runat="server" Text="新岗位更新"  
                        CssClass="SmallButton2" onclick="btnProcMaintain_Click" Width="80px"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnApproval" runat="server" Text="批准" CssClass="SmallButton2" 
                        onclick="btnApproval_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>

        <asp:GridView ID="gv" runat="server" CssClass="GridViewStyle AutoPageSize" AutoGenerateColumns="False">
        <RowStyle CssClass="GridViewRowStyle" />
        <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
        <FooterStyle CssClass="GridViewFooterStyle" />
        <PagerStyle CssClass="GridViewPagerStyle" />
        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        <Columns>
                <%--<asp:BoundField HeaderText="序号" DataField="plantCode">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" Height="25px" HorizontalAlign="Center" />
                </asp:BoundField>--%>
                <asp:BoundField HeaderText="公司" DataField="App_ReviewCompany">
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" Height="20px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="部门" DataField="App_ReviewDepartment">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="职务" DataField="App_ReviewProcess">
                    <HeaderStyle Width="80px" />
                    <ItemStyle Width="80px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="审批人" DataField="App_Reviewname">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="审批时间" DataField="App_ReviewTime">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="审批意见" DataField="App_ReviewOpinion">
                    <HeaderStyle Width="300px" />
                    <ItemStyle Width="300px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="审批结果" DataField="App_ReviewResult">
                    <HeaderStyle Width="60px" />
                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
        <%--<table>
            <tr>
                <td style="width:100px;">审 批 意 见：　　(限500字以内)</td>
                <td><asp:TextBox ID="txtReviewOpinion" runat="server" Width="580" Height="100" CssClass="SmallTextBox"
                        TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td>提交给下一审批人</td>
                <td>
                    <asp:TextBox ID="txtChooseName" runat="server" Width="520px"></asp:TextBox>
                    <asp:TextBox ID="txtChooseEmail" runat="server" Visible="True" Style="display: none"></asp:TextBox>
                    <asp:TextBox ID="txtChooseid" runat="server" Visible="True" Style="display: none"></asp:TextBox>
                    <asp:Button ID="btnNextReview" runat="server" Text="选择审批人" 
                        CssClass="SmallButton2" onclick="btnNextReview_Click" /></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnSubmit" runat="server" Text="提交" CssClass="SmallButton2" 
                        onclick="btnSubmit_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnRefuse" runat="server" Text="拒绝" CssClass="SmallButton2" 
                        onclick="btnRefuse_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" Text="返回" onclick="btnBack_Click"  
                        CssClass="SmallButton2"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnApproval" runat="server" Text="批准" CssClass="SmallButton2" 
                        onclick="btnApproval_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>--%>
    </div>
    <script type="text/javascript">
		<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
    </form>
</body>
</html>
