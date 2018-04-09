<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qc_flux_mstr.aspx.cs" Inherits="QC_qc_flux_mstr" %>

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
            <div style="width: 1002px;" class="MainContent_outer">
                <table cellspacing="0" cellpadding="1" width="1000" class="MainContent_inner">
                    <tr>
                        <td align="left">��Ʒ�ͺ�:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtProduct" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td align="left"></td>
                        <td align="left">QAD��:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtQad" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td align="left"></td>
                        <td align="left">��ƺ�:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtOldPro" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td align="left"></td>
                        <td align="left">���QAD��:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtOldQad" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">������:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPo" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>�ͻ�:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>�ӹ�����:
                        </td>
                        <td>
                            <asp:TextBox ID="txtWo" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>��Ӧ��:
                        </td>
                        <td>
                            <asp:TextBox ID="txtProvider" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>

                        <td>��������:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPeriod" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>���Ӱ��:
                        </td>
                        <td>
                            <asp:TextBox ID="txtVersion" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td align="left">�Ͳⲿ��:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDept" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>

                        <td>��������:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="smalltextbox Date" Width="80px"></asp:TextBox>
                            ---
                    <span style="background-color: #bbb; width: 230px; padding-top: 3px; padding-bottom: 0px">&nbsp;
                    <asp:TextBox ID="txtDate1" runat="server" CssClass="smalltextbox Date" Width="80px"></asp:TextBox>
                        <span style="color: red">*����д��ֻ�ڲ�ѯʱʹ��</span></span></td>
                    </tr>
                    <tr style="display: none">


                        <td align="left" >�ƹܺ�:
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtLamp" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td >���κ�:
                        </td>
                        <td >
                            <asp:TextBox ID="txtSeries" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>��ʱ�ϻ�̨��:
                        </td>
                        <td>
                            <asp:TextBox ID="txtTemp" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>�����ϻ�̨��:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAging" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="left">����Ա:
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtTester" runat="server" CssClass="SmallTextBox" Width="120px"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>״̬:
                        </td>
                        <td>
                            <asp:DropDownList ID="dropStatu" runat="server" Width="71px">
                                <asp:ListItem Value="--">--</asp:ListItem>
                                <asp:ListItem Value="����">����</asp:ListItem>
                                <asp:ListItem>����</asp:ListItem>
                                <asp:ListItem>���</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>����:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" Width="100px">
                                <asp:ListItem Value="--">--</asp:ListItem>
                                <asp:ListItem Value="1">������Ʒ</asp:ListItem>
                                <asp:ListItem Value="2">��������Ʒ</asp:ListItem>
                                <asp:ListItem Value="3">�г���Ʒ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>��ע:
                        </td>
                        <td colspan="9">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="SmallTextBox" Width="80%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnQuery" runat="server" CssClass="SmallButton3" Text="��ѯ" Visible="True"
                                Width="34px" OnClick="btnQuery_Click" /><asp:Button ID="btnAdd" runat="server" CssClass="SmallButton3"
                                    Text="���" Visible="True" Width="34px" OnClick="btnAdd_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="SmallButton3" Text="���" Visible="True"
                                Width="34px" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                    <tr runat="server" id="trupload1">
                        <td>��׼�ļ�����:
                        </td>
                        <td colspan="11">����ʱ��:<asp:TextBox ID="txtHour" runat="server" CssClass="SmallTextBox" Width="83px"></asp:TextBox>/h
                    &nbsp; &nbsp;��ȼ��ʽ:<asp:DropDownList ID="dropWay" runat="server" CssClass="SmallTextBox"
                        Width="67px">
                        <asp:ListItem>--</asp:ListItem>
                        <asp:ListItem Value="UP">UP </asp:ListItem>
                        <asp:ListItem Value="DOWN">DOWN</asp:ListItem>
                    </asp:DropDownList>
                            <br />
                            <input id="txtFile" runat="server" class="SmallTextBox" style="width: 612px; height: 22px"
                                type="file" />
                            <input id="btnUploadTxt" runat="server" class="SmallButton2" name="uploadBtn" style="width: 80px"
                                type="button" validationgroup="Import" value="��������" onserverclick="btnUploadTxt_ServerClick" />
                        </td>
                    </tr>
                    <tr runat="server" id="trupload2">
                        <td>Excel�ļ�����:
                        </td>
                        <td colspan="11">
                            <input id="excelFile" runat="server" class="SmallTextBox" style="width: 612px; height: 22px"
                                type="file" />
                            <input id="btnUploadExcel" runat="server" class="SmallButton2" name="uploadBtn" style="width: 80px"
                                type="button" validationgroup="Import" value="��������" onserverclick="btnUploadExcel_ServerClick" />
                            &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnTemplate" Text="����ģ��" runat="server" class="SmallButton2" Style="width: 80px" OnClick="btnTemplate_Click" />
                        </td>
                    </tr>
                    <tr runat="server" id="trupload3">
                        <td>�������룺</td>
                        <td colspan="11">
                            <input id="filename" style="width: 612px; height: 22px" type="file" name="filename1"
                                runat="server" />
                            <input id="btnUplodeFile" runat="server" class="SmallButton2" name="btnUplodeFile" style="width: 80px"
                                type="button" validationgroup="Import" value="��������" onserverclick="btnUplodeFile_Click" />

                        </td>
                    </tr>
                    <tr>
                        <td style="height: 4px;" colspan="11"></td>
                    </tr>
                    <tr>
                        <td colspan="11" style="vertical-align: top;">
                            <asp:Panel ID="Panel1" Style="overflow: auto" runat="server" Width="960px" BorderWidth="1px"
                                BorderColor="Black" ScrollBars="Auto" Height="305px" HorizontalAlign="Left">
                                <asp:GridView ID="gvFluxMstr" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                    PageSize="6" TabIndex="5" Width="1650px" OnRowCommand="gvFluxMstr_RowCommand"
                                    DataKeyNames="fl_id,fl_type" OnRowDeleting="gvFluxMstr_RowDeleting" OnRowDataBound="gvFluxMstr_RowDataBound"
                                    OnPageIndexChanging="gvFluxMstr_PageIndexChanging" CssClass="GridViewStyle">
                                    <Columns>
                                        <asp:BoundField HeaderText="��Ʒ�ͺ�" DataField="fl_product">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="QAD��" DataField="fl_qad">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fl_oldpro" HeaderText="�ϲ�����">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fl_oldqad" HeaderText="��QAD��">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="������" DataField="fl_po">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="�ͻ�" DataField="fl_cust">
                                            <HeaderStyle Width="50px" />
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="�ӹ�����" DataField="fl_wo">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="��Ӧ��" DataField="fl_provider">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="�ƹܺ�" DataField="fl_lamp" Visible="false">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="���κ�" DataField="fl_series" Visible="false">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="��������" DataField="fl_period">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="���Ӱ��" DataField="fl_version">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="�Ͳⲿ��" DataField="fl_dept">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="��ʱ�ϻ�̨��" DataField="fl_temp" Visible="false">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="�����ϻ�̨��" DataField="fl_aging" Visible="false">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="��������" DataField="fl_date" DataFormatString="{0:yyyy-MM-dd}"
                                            HtmlEncode="False">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="����Ա" DataField="fl_tester">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fl_statu" HeaderText="״̬">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fl_typeName" HeaderText="����">
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="��ע" DataField="fl_rmks">
                                            <HeaderStyle Width="150px" />
                                            <ItemStyle Width="150px" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkDetail" runat="server">��ϸ</asp:LinkButton>
                                            </ItemTemplate>
                                            <ControlStyle Font-Underline="True" />
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("fl_id") %>'
                                                    CommandName="Cust_Edit">�༭</asp:LinkButton>
                                            </ItemTemplate>
                                            <ControlStyle Font-Underline="True" />
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:CommandField DeleteText="<u>ɾ��</u>"
                                            ShowDeleteButton="True">
                                            <ControlStyle Font-Underline="True" />
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:CommandField>
                                    </Columns>
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                                    <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                </asp:GridView>
                            </asp:Panel>
                            <asp:TextBox ID="txtID" runat="server" CssClass="SmallTextBox" Width="140px" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <script type="text/javascript">
        <asp:Literal id="ltlAlert" runat="server"  EnableViewState="False"></asp:Literal>
    </script>
</body>
</html>
