<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QC_CertificationTestNew.aspx.cs" Inherits="QC_QC_CertificationTestNew" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <link media="all" href="m5.css" rel="stylesheet" />
    <link media="all" href="../css/jquery-ui.min.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/jquery-ui.min.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(function () {
            $("#txtNbr").blur(function () {
                var nbr = $("#txtNbr").val().trim();
                var lot = $("#txtLot").val().trim();
                if (nbr != '' && lot != '') {
                    search();
                    return false;
                }
            });
            $("#txtLot").blur(function () {
                var nbr = $("#txtNbr").val().trim();
                var lot = $("#txtLot").val().trim();
                if (nbr != '' && lot != '') {
                    search();
                    return false;
                }
            });
            function search() {                
                var nbr = $("#txtNbr").val().trim();
                var lot = $("#txtLot").val().trim();
                $.ajax({
                    type: "POST",
                    async: true,
                    url: "../Ajax/QCtest.ashx",
                    dataType: "html",
                    data: "nbr=" + nbr + "&lot=" + lot,
                    success: function (result) {
                        var part = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                        result = result.substring(result.indexOf(";") + 1);
                        $("#txtPart").val(part);
                        $("#hidPart").val(part);
                        var domail = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                        result = result.substring(result.indexOf(";") + 1);
                        $("#txtDomain").val(domail);
                        $("#hidDomain").val(domail);
                        var site = result.substring(result.indexOf(":") + 1, result.indexOf(";"));
                        result = result.substring(result.indexOf(";") + 1);
                        $("#txtSite").val(site);
                        $("#hidSite").val(site);
                        var Desc = result.substring(result.indexOf(":") + 1);
                        $("#txtDesc").val(Desc);
                        $("#hidDesc").val(Desc);
                    },
                    error: function (XMLHttpRequest, textStaus, errThrown) {
                        $("#txtPart").val('');
                        $("#hidPart").val('');
                        $("#txtDomain").val('');
                        $("#hidDomain").val('');
                        $("#txtSite").val('');
                        $("#hidSite").val('');
                        $("#txtDesc").val('');
                        $("#hidDesc").val('');
                    }
                })
            }
            $("#btnSubmit").click(function () {
                var content = $("#txtContent").val();
                if ($("input[name='rbtTestType']:checked").val() == undefined)
                {
                    alert('请选择检验结果');
                    return false;
                }
                if (content == '')
                {
                    alert('检测内容不能为空！');
                    return false;
                }
            });
        });
    </script>
    <style type="text/css">
        .title{
            background-color:#efefef;
        }
        .tdclass {
            width:130px;
            border-top:0px solid #ffffff;
            border-left:0px solid #ffffff;
            border-right:0px solid #ffffff;
        }
        td{
            border:solid #808080; 
            border-width:0px 1px 1px 0px;
        }
        table{
            border:solid #808080; 
            border-width:1px 0px 0px 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table style="margin-top:20px;" cellspacing="0" cellpadding="1">
            <tr>
                <td style="text-align:center; font-size:20px; background-color:#cacdf6;" colspan="4">认证检验</td>
            </tr>
            <tr>
                <td style="width:150px;" class="title">工单号</td>
                <td style="width:250px;">
                    <asp:TextBox ID="txtNbr" AutoComplete="Off" CssClass="SmallTextBox5" runat="server"></asp:TextBox>
                </td>
                <td style="width:150px;" class="title">ID</td>
                <td style="width:250px;">
                    <asp:TextBox ID="txtLot" AutoComplete="Off" CssClass="SmallTextBox5" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center; background-color:#cacdf6;">工单详细</td>
            </tr>
            <tr>
                <td class="title">QAD号</td>
                <td>
                    <asp:TextBox ID="txtPart" Enabled="false" AutoComplete="Off" CssClass="SmallTextBox5" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hidPart" runat="server" />
                </td>
                <td class="title" rowspan="3">描述</td>
                <td rowspan="3">
                    <asp:TextBox ID="txtDesc" Enabled="false" runat="server" AutoComplete="Off" TextMode="MultiLine" Width="250px" Height="80px" ></asp:TextBox>
                    <asp:HiddenField ID="hidDesc" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="title">域</td>
                <td>
                    <asp:TextBox ID="txtDomain" Enabled="false" AutoComplete="Off" CssClass="SmallTextBox5" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hidDomain" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="title">地点</td>
                <td>
                    <asp:TextBox ID="txtSite" Enabled="false" AutoComplete="Off" CssClass="SmallTextBox5" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hidSite" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center; background-color:#cacdf6;">检验结果</td>
            </tr>
            <tr>
                <td class="title">检验结果</td>
                <td style="text-align:center;" colspan="3">
                     <asp:RadioButtonList ID="rbtTestType" runat="server" RepeatLayout="Flow" CellPadding="20" CellSpacing="20" RepeatDirection="Horizontal" >
                        <asp:ListItem Value="1">通过</asp:ListItem>
                        <asp:ListItem Value="0">不通过</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="title">检验内容</td>
                <td colspan="3">
                    <asp:TextBox ID="txtContent" runat="server" AutoComplete="Off" TextMode="MultiLine" Width="650px" Height="100px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:center; background-color:#cacdf6;" colspan="4">附件</td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center;">
                    <input id="filename" runat="server" style="width:600px;" name="resumename"  CssClass="SmallTextBox5"  type="file"/>
                    <asp:Button ID="btnUpload" runat="server" CssClass="SmallButton2" Text="上传" OnClick="btnUpload_Click"/>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:GridView ID="gvFile" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                        DataKeyNames="ID,fname,fpath,createBy,createName,createDate"
                        AllowPaging="False" PageSize="20"
                         OnRowDeleting="gvFile_RowDeleting" OnRowCommand="gvFile_RowCommand">
                        <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                        <RowStyle CssClass="GridViewRowStyle" />
                        <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                        <PagerStyle CssClass="GridViewPagerStyle" />
                        <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                        <HeaderStyle CssClass="GridViewHeaderStyle" />
                        <EmptyDataTemplate>
                            <asp:Table ID="Table1" runat="server" CellPadding="-1" CellSpacing="0" CssClass="GridViewHeaderStyle"
                                GridLines="Vertical">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="center" Text="文件名" Width="300px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="上传日期" Width="200px"></asp:TableCell>
                                    <asp:TableCell HorizontalAlign="center" Text="创建人" Width="200px"></asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="fname" HeaderText="文件名">
                                <HeaderStyle Width="300px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="300px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="createDate" HeaderText="上传日期">
                                <HeaderStyle Width="150px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="150px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="createName" HeaderText="创建人">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" Font-Bold="False" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:ButtonField Text="View" HeaderText="查看" CommandName="View">
                                <ControlStyle Font-Bold="False" Font-Underline="True" />
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="删除">
                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" ForeColor="Black" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" Text="<u>Delete</u>" ForeColor="Black"
                                        CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center;">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="SmallButton2" Text="完成" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" CssClass="SmallButton2" Text="保存" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CssClass="SmallButton2" Text="返回" OnClick="btnBack_Click" /><br />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hidCreateBy" runat="server" />
    </form>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
