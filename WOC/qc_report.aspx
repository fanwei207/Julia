<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_report.aspx.cs" Inherits="QC_qc_report" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
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
        <table cellspacing="0" cellpadding="0" width="1150px" class="main_top">
            <tr>
                <td class="main_left">
                </td>
                <td align="left">
                    工单号:<asp:TextBox ID="txtOrder" runat="server" CssClass="SmallTextBox"
                            TabIndex="2" Width="90px"></asp:TextBox>
                    ID号:<asp:TextBox ID="txtReceiver" runat="server" CssClass="SmallTextBox" Width="90px"
                        TabIndex="1"></asp:TextBox>&nbsp;<asp:TextBox ID="txtLine" runat="server"
                                CssClass="SmallTextBox" TabIndex="2" Width="90px" Visible="false"></asp:TextBox>
                    物料号:<asp:TextBox ID="txtPart" runat="server" CssClass="SmallTextBox" Width="90px"
                        TabIndex="3"></asp:TextBox>产线:<asp:TextBox ID="txtCus" runat="server" CssClass="SmallTextBox"
                            TabIndex="3" Width="90px"></asp:TextBox>评审日期:<asp:TextBox ID="txtStd" runat="server"
                                CssClass="smalltextbox Date" Width="100px" onpaste="return false"></asp:TextBox>
                    —<asp:TextBox ID="txtEnd" runat="server" CssClass="smalltextbox Date" Width="100px"
                        onpaste="return false"></asp:TextBox>
                </td>
                <td>
                    巡检类别
                </td>
                <td style="width:100px;">
                    <asp:DropDownList ID="dType" runat="server" Width="100%" DataTextField="typeName"
                            DataValueField="typeID">
                        </asp:DropDownList>
                </td>
                
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="SmallButton3" OnClick="btnSearch_Click"
                        TabIndex="4" Text="查询" />
                    <asp:Button ID="btnExport" runat="server" Text="Excel" CssClass="SmallButton3" OnClick="btnExport_Click"  />
                </td>
                <td class="main_right">
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle AutoPageSize"
            Width="1000px" PageSize="18" OnRowDataBound="gvReport_RowDataBound" DataKeyNames="ok,no,prh_group,flag,Identity"
            OnRowCommand="gvReport_RowCommand">
            <Columns>
                <asp:TemplateField Visible="False">
                    <EditItemTemplate>
                        &nbsp;
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/Block.GIF" CommandName="expand"
                            ImageAlign="Baseline" />
                    </ItemTemplate>
                    <ItemStyle Width="30px" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="工单" DataField="prh_nbr">
                    <HeaderStyle Width="70px" HorizontalAlign="Center"/>
                    <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="ID号" DataField="prh_receiver" HeaderStyle-Width="70px">
                  <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="行号" DataField="prh_line" Visible="false">
                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" />
                    <ItemStyle Font-Bold="False" />
                </asp:BoundField>
                <asp:BoundField HeaderText="物料号" DataField="prh_part">
                     <HeaderStyle Width="100px"  HorizontalAlign="Center"/>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="产线" DataField="prh_vend">
                    <HeaderStyle Width="80px" />
                     <ItemStyle  HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField DataField="prh_rcvd" HeaderText="数量" DataFormatString="{0:N0}">
                    <HeaderStyle Width="80px" />
                     <ItemStyle  HorizontalAlign="Right"/>
                </asp:BoundField>
                <asp:BoundField DataField="prh_rmn" Visible="false" HeaderText="剩余数量" DataFormatString="{0:N0}">
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="评审日期" DataField="prh_rcp_date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                     <HeaderStyle Width="80px"  HorizontalAlign="Center"/>
                    <ItemStyle HorizontalAlign="Center" />

                </asp:BoundField>

                <asp:BoundField HeaderText="关单日期" DataField="wo_close_date" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False">
                    <HeaderStyle Width="80px"  HorizontalAlign="Center"/>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="检验项目">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="link">添加项目</asp:LinkButton>
                    </ItemTemplate>
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="65px" />
                    <ItemStyle Font-Bold="False" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="光色检验" Visible="false">
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                    <HeaderStyle HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="link">导入</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="link">查看</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="判定">
                    <HeaderStyle Width="40px" />
                </asp:BoundField>
                <asp:TemplateField ShowHeader="False" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="false" CommandName="compete"
                            Text="完成"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton6" runat="server" CausesValidation="false" CommandName="tecai"
                            Text="特采"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton7" runat="server" CausesValidation="false" CommandName="tuihuo"
                            Text="退货"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100px" HorizontalAlign="Center" />
                    <ControlStyle Font-Underline="True" Font-Bold="False" Font-Size="12px" />
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton5" runat="server" CommandName="refuse">重检</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                    <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                </asp:TemplateField>
            </Columns>
            <RowStyle CssClass="GridViewRowStyle" />
            <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
            <FooterStyle CssClass="GridViewFooterStyle" />
            <PagerStyle CssClass="GridViewPagerStyle" />
            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
        </asp:GridView>
        <asp:TextBox ID="txtPageIndex" runat="server" Visible="False" Width="49px"></asp:TextBox><asp:TextBox
            ID="txtPageCount" runat="server" Visible="False" Width="49px"></asp:TextBox><asp:TextBox
                ID="txtIndex" runat="server" Visible="False" Width="49px"></asp:TextBox>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
