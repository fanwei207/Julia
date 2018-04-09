<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NWF_FlowNode.aspx.cs" Inherits="NWF_NWF_FlowNode" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $(".txtSort").blur(function () {
                if ($(this).val() != "" && !isInt($(this).val())) {
                    alert("排序必须是正整数");
                    $(this).val("");
                }
            });
        })

        function isInt(str) {
            var reg1 = /^\d+$/;
            return reg1.test(str);
        }
    </script> 
</head>
<body onunload="javascript: if(w12) w12.window.close();">
    <div align="center">
        <form id="Form1" method="post" runat="server">
        <table border="0" cellpadding="5" cellspacing="0" width="900" style="background-image: url('../images/bg_tb14.jpg');
            background-repeat: repeat-x; margin-top: 20px; height: 199px;">
            <tr>
                <td rowspan="5" style="width: 4px; background-image: url(../images/bg_tb13.jpg);
                    background-repeat: no-repeat; background-position: left top;">
                </td>
                <td align="right" style="width: 80px;" valign="middle">
                    模板名称:
                </td>
                <td style="width: 100px;" valign="middle">
                    <b>
                        <asp:TextBox ID="txtFlowName" runat="server" CssClass="SmallTextBox" Width="190px"
                            ReadOnly="True"></asp:TextBox></b>
                </td>
                <td align="right" style="width: 80px;" valign="middle">
                    步骤名称:
                </td>
                <td style="width: 235px;" valign="middle">
                    <asp:TextBox ID="txtNodeName" runat="server" Height="20px" Width="190px" CssClass="SmallTextBox"></asp:TextBox>
                    <asp:Label ID="lblid" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td></td>
                <td rowspan="5" style="width: 4px; background-image: url(../images/bg_tb15.jpg);
                    background-repeat: no-repeat; background-position: right top;">
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 80px;" valign="middle">
                    步骤描述:
                </td>
                <td colspan="3" valign="middle">
                    <asp:TextBox ID="txtNodeDesc" runat="server" Height="50px" MaxLength="1000" TextMode="MultiLine"
                        Width="600px" CssClass="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 80px;" valign="middle">
                    步骤类型:
                </td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server" Width="190px">
                    <asp:ListItem Text="Or" Value="Or" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="And" Value="And"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" style="width: 80px;" valign="middle">
                    步骤序号:
                </td>
                <td>
                    <asp:TextBox ID="txtNodeSort" runat="server" CssClass="SmallTextBox txtSort" Width="190px"></asp:TextBox>
                    <asp:CheckBox ID="ckb_Email" runat="server" Text="发邮件" />
                </td>

            </tr>
            <tr>
                <td align="center" valign="middle" colspan="4">
                    <asp:Button ID="btn_add" runat="server" CausesValidation="true" CssClass="SmallButton3"
                        OnClick="btn_add_Click" Text="增加" Height="25px" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btn_return" runat="server" Text="返回" CssClass="SmallButton3" CausesValidation="true"
                        OnClick="btn_return_Click" Height="25px" Visible="False"></asp:Button>
                        
                                  
                </td>
                <td align="right" valign="middle">
                     <asp:Button ID="btnFormDesign" runat="server" Text="报表设置" CssClass="SmallButton3" CausesValidation="true"
                        OnClick="btnFormDesign_Click" Height="25px"></asp:Button>
                     <asp:Button ID="btn_set" runat="server" Text="步骤流程" CssClass="SmallButton3" CausesValidation="true"
                        OnClick="btn_set_Click" Height="25px"></asp:Button>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvFN" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            CssClass="GridViewStyle" PageSize="20" DataKeyNames="Node_ID" Width="900px" OnRowDeleting="gvFN_RowDeleting"
            OnRowCommand="gvFN_RowCommand" OnPageIndexChanging="gvFN_PageIndexChanging">
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
            <Columns>
                <asp:BoundField DataField="Node_Name" HeaderText="步骤名称">
                    <HeaderStyle Width="100px" HorizontalAlign="Center" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Node_Desc" HeaderText="步骤描述">
                    <HeaderStyle Width="250px" HorizontalAlign="Center" />
                    <ItemStyle Width="250px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="Node_Type" HeaderText="步骤类型">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Node_Sort" HeaderText="序号">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Node_CreatedBy" HeaderText="创建者">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Node_CreatedDate" HeaderText="创建日期" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkedit" Text="<u>编辑</u>" ForeColor="Blue" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("Node_ID") %>' CommandName="myEdit" />
                    </ItemTemplate>
                    <HeaderStyle Width="30px" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkdelete" Text="<u>删除</u>" ForeColor="Blue" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("Node_ID") %>' CommandName="Delete" />
                    </ItemTemplate>
                    <HeaderStyle Width="30px" />
                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="linkDesign" Text="<u>表单设置</u>" ForeColor="Blue" Font-Underline="true"
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("Node_ID") %>' CommandName="design" />
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作人">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkperson" ForeColor="Blue" Font-Underline="true" Text='<%# Bind("FN_Member") %>'
                            Font-Size="12px" runat="server" CommandArgument='<%# Eval("Node_ID") %>' CommandName="Reviewer">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                    <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </form>
    </div>
    <script type="text/javascript">
			<asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>

