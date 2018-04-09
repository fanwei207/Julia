<%@ Page Language="C#" AutoEventWireup="true" CodeFile="perf_Add.aspx.cs" Inherits="Performance_perf_Add" %>

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
        <table id="table1" cellpadding="0" cellspacing="0" width="750">
            <tr>
                <td rowspan="2" align="right">
                    <asp:Label  runat="server" ID="Label1" Width="50px">类型：</asp:Label>
                </td>
                <td rowspan="2" >
                  <asp:DropDownList ID="dd_type" runat="server" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="dd_type_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Label runat="server" ID="Label7" Width="60px">工厂：</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dropPlant" runat="server" Width="250px" AutoPostBack="True"
                             TabIndex="1" 
                            Style="text-align: center;" OnSelectedIndexChanged="dropPlant_SelectedIndexChanged" >
                           
                            <asp:ListItem Value="1">上海强凌电子有限公司 SZX</asp:ListItem>
                            <asp:ListItem Value="2">镇江强凌电子有限公司 ZQL</asp:ListItem>
                            <asp:ListItem Value="5">扬州强凌有限公司 YQL</asp:ListItem>
                            <asp:ListItem Value="8">淮安强陵照明有限公司  HQL</asp:ListItem>
                              
                        </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td align="right">
                    <asp:Label runat="server" ID="Label8" Width="60px">工号：</asp:Label>
                </td>
                <td>
                   
                     <asp:TextBox ID="txb_no" runat="server" Width="250px" TextMode="SingleLine" AutoPostBack="True"
                        TabIndex="1" OnTextChanged="txb_no_TextChanged"></asp:TextBox>

                 
                </td>
            </tr>
            <tr>
                <td align="right" rowspan="4" style="vertical-align:top;">
                    <asp:Label runat="server" ID="Label3" Width="50px">原因：</asp:Label>
                </td>
                <td rowspan="4"  style="vertical-align:top;">
                    <asp:TextBox ID="txtsearch" runat="server" AutoPostBack="True" OnTextChanged="txtsearch_TextChanged" ></asp:TextBox>（查询）
                    <asp:DropDownList ID="dd_cause" runat="server" Width="300" AutoPostBack="True" DataTextField="perf_cause" DataValueField="perf_defi_id" OnSelectedIndexChanged="rdl_cause_SelectedIndexChanged">
                    </asp:DropDownList>
                   <%-- <asp:RadioButtonList ID="rdl_cause" runat="server" DataTextField="perf_cause" DataValueField="perf_defi_id" AutoPostBack="True" OnSelectedIndexChanged="rdl_cause_SelectedIndexChanged"></asp:RadioButtonList>--%>
                </td>
                <td align="right">
                    <asp:Label runat="server" ID="Label6" Width="60px">部门：</asp:Label>
                </td>
                <td>
                  
                    <%--<asp:TextBox ID="txt_demp" runat="server" ReadOnly="true" TextMode="SingleLine"></asp:TextBox>--%>
                    <asp:DropDownList ID="dd_dept" runat="server" Width="300px" AutoPostBack="true" TabIndex="10" OnSelectedIndexChanged="dd_dept_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label runat="server" ID="Label2" Width="60px">姓名：</asp:Label>
                </td>
                <td>
                   
                      <%-- <asp:TextBox ID="txt_user" runat="server"  ReadOnly="true"  TextMode="SingleLine"></asp:TextBox>--%>
                     <asp:DropDownList ID="dd_user" runat="server" Width="300px" AutoPostBack="false"
                        TabIndex="11">
                    </asp:DropDownList>
                    <asp:Label ID="lbluName" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label runat="server" ID="countLabel" Width="60px" TabIndex="2">考评对象：</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="dd_duty" runat="server" Width="250px" AutoPostBack="true" TabIndex="5">
                      
                        <asp:ListItem Selected="false" Value="1">第一责任人</asp:ListItem>
                       
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label runat="server" ID="Label4" Width="60px">评分：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_ref" runat="server" ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    <asp:TextBox ID="txb_mark" runat="server" Enabled="true" TextMode="SingleLine" Width="140px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top">
                    <asp:Label ID="Label5" runat="server"> 说明：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_comm" runat="server" Height="200px" Width="300" TextMode="MultiLine"
                        ></asp:TextBox>
                </td>
                <td align="right" valign="top">
                    <asp:Label ID="lbl_appcontent" runat="server"> 描述：</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txb_note" runat="server" Height="200px" Width="300" TextMode="MultiLine"
                        TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" height="10">
                    上传图片：<input id="filename1" style="width: 563px; height: 22px" type="file" name="filename1"
                                runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="btn_next" runat="server" CssClass="SmallButton2" Width="80px" Text="保存"
                        TabIndex="20" OnClick="btn_next_Click"></asp:Button>
                    <asp:Button ID="btn_back" runat="server" CssClass="SmallButton2" Text="返回" OnClick="btn_back_Click"></asp:Button>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <script>
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
