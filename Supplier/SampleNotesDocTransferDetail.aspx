<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SampleNotesDocTransferDetail.aspx.vb" Inherits="Supplier_SampleNotesDocTransferDetail" %>

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
    <div align="Center">
    <form id="form1" runat="server">
        <input type="hidden" id="hidOldFileName" name="OldFileName" runat="server" style="width: 63px" />
        <input type="hidden" id="hidOldAccFileName" name="OldAccFileName" runat="server"
            style="width: 63px" />
        <table width="800px" style="border: 1px solid">
            <tr>
                <td align="left" colspan="4">
                    正在试图转移:
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 80px">
                    订单:
                </td>
                <td align="left" style="width: 220px">
                    <asp:Label ID="lbNbr" runat="server"></asp:Label>
                </td>
                <td align="right" style="width: 80px">
                    行:
                </td>
                <td align="left" style="width: 220px">
                    <asp:Label ID="lbLine" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 80px">
                    部件号:
                </td>
                <td align="left" style="width: 220px">
                    <asp:Label ID="lbCode" runat="server"></asp:Label>
                </td>
                <td align="right" style="width: 80px">
                    QAD号:
                </td>
                <td align="left">
                    <asp:Label ID="lbQAD" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lbDocDiskName" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lbFileType" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="oldFileName" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lbDomain" runat="server" Visible ="false"></asp:Label>
                    <asp:Label ID="lbStepID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lbMid" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    转移至:
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="800px">
            <tr>
                <td align="right" style="width: 80px">
                    Type:
                </td>
                <td align="left" colspan="3">
                    <asp:DropDownList ID="ddlType" runat="server" Width="200px" DataTextField="typename"
                        DataValueField="typeid" AutoPostBack="True">
                    </asp:DropDownList>
                    Category:<asp:DropDownList ID="ddlCategory" runat="server" Width="200px" DataTextField="catename"
                        DataValueField="cateid" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">
                DocName:
                </td>
                <td align="left">
                <asp:TextBox ID="txtDocName" runat="server" CssClass="smalltextbox" Width="400px"></asp:TextBox>
                </td>
                <td colspan="2">
                Doc Ver:<asp:TextBox ID="txtDocVer" runat="server" CssClass="smalltextbox"
                        Width="40px"></asp:TextBox>&nbsp;&nbsp;&nbsp; Doc Level:<asp:DropDownList ID="ddlLevel"
                            Width="40px" runat="server">
                            <asp:ListItem Value="0" Text="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                        </asp:DropDownList>
                </td>
            
            </tr>
            <tr>
            <td align="right">
            FileName:
            </td>
            <td align="left">
            <asp:TextBox ID="txtFileName" runat="server" ReadOnly="true" Width="400px"></asp:TextBox>
            </td>
            <td align="right">
            IsPublic:
            </td>
            <td align="left">
            <asp:CheckBox ID="chkIsPublic" runat="server" /><font style="color: Red">(默认不公用)</font>
            </td>
            
            </tr>
          
            <tr>
                <td align="right">
                    Doc Desc:
                </td>
                <td align="left" style="height: 20px">
                    <asp:TextBox ID="txtDocDesc" Width="400px" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                    
                </td>
                <td align="right">
                IsApprove:
                </td>
                <td align="left">
                <asp:CheckBox ID="chkIsApprove" runat="server" />
                    <font style="color: Red">(默认未审批)</font>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 80px">
                    Vend:
                </td>
                <td align="left">
                    <asp:CheckBox ID="chkVend" runat="server" />
                    <asp:Label ID="lbVend" runat="server"></asp:Label><font style="color: Red">(默认不关联供应商)</font>
                </td>
                <td align="right">
                For All Items:
                </td>
                <td align="left">
                <asp:CheckBox ID ="chkForAllItems" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td align="left" style="width: 300px">
                    <asp:Label ID="lbVendName" runat="server"></asp:Label>
                </td>
                 <td align="right">
                修改关联文档:
                </td>
                <td align="left">
                <asp:CheckBox ID ="chkAccFileName" runat="server" />
                </td>
            </tr>
            <tr>
                <td runat="server" align="left" colspan="4" id="operatingArea">
                    <asp:Button ID="btnadd" runat="server" CssClass="SmallButton3" Text="Add"></asp:Button>
                    <asp:Button ID="Btnedit" runat="server" CssClass="SmallButton3" Text="Modify" Width="70">
                    </asp:Button>
                    <asp:Button ID="Butcancel" runat="server" CssClass="SmallButton3" Text="Cancel" Width="70">
                    </asp:Button>
                    <asp:Button ID="btnback" runat="server" CssClass="SmallButton3" Text="Back"></asp:Button>
                </td>
            </tr>
        </table>
        <div class="GridViewPanel" style="width:800px;overflow:auto; padding:top; height:auto; ">
            <asp:DataGrid ID="datagrid1" runat="server" Width="1810px" AutoGenerateColumns="False"
                HeaderStyle-Font-Bold="false" PagerStyle-HorizontalAlign="Center" AllowPaging="True"
                PagerStyle-Mode="NumericPages" PagerStyle-BackColor="#99ffff" PagerStyle-Font-Size="12pt"
                PagerStyle-ForeColor="#0033ff" CssClass="GridViewStyle AutoPageSize">
                <ItemStyle CssClass="GridViewRowStyle" />
                <HeaderStyle Font-Bold="True" CssClass="GridViewHeaderStyle" ForeColor="Black" />
                <AlternatingItemStyle CssClass="GridViewAlternatingRowStyle" />
                <FooterStyle CssClass="GridViewFooterStyle" />
                <PagerStyle CssClass="GridViewPagerStyle" />
                <SelectedItemStyle CssClass="GridViewSelectedRowStyle" />
                <Columns>
                    <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>
                    <asp:BoundColumn DataField="name" HeaderText="DocName">
                        <HeaderStyle HorizontalAlign="Center" Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="200px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="filename" HeaderText="FileName">
                        <HeaderStyle HorizontalAlign="Center" Width="350px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="350px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="typename" HeaderText="TypeName">
                        <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="catename" HeaderText="CateName">
                        <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Level" HeaderText="Lv">
                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="version" HeaderText="Ver">
                        <HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="isAppr" HeaderText="Appr">
                        <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="isall" HeaderText="All">
                        <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="isPublic" HeaderText="Public">
                        <HeaderStyle HorizontalAlign="Center" Width="25px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:ButtonColumn Text="&lt;u&gt;Edit&lt;/u&gt;" CommandName="Select">
                        <HeaderStyle HorizontalAlign="Center" Font-Size="8pt" Font-Names="Tahoma,Arial" Width="25px">
                        </HeaderStyle>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="True" HorizontalAlign="Center" />
                    </asp:ButtonColumn>
                    <asp:ButtonColumn Text="&lt;u&gt;Del&lt;/u&gt;" CommandName="DeleteClick">
                        <HeaderStyle HorizontalAlign="Center" Font-Size="8pt" Font-Names="Tahoma,Arial" Width="25px">
                        </HeaderStyle>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="True" HorizontalAlign="Center" />
                    </asp:ButtonColumn>
                    <asp:BoundColumn DataField="preview" HeaderText="View">
                        <HeaderStyle HorizontalAlign="Center" Width="25px" Font-Bold="False"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="25px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="oldview" HeaderText="P.Ver">
                        <HeaderStyle HorizontalAlign="Center" Width="20px" Font-Bold="False"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="20px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:ButtonColumn DataTextField="assText" HeaderText="Assoc" CommandName="associated_item">
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                    </asp:ButtonColumn>
                    <asp:ButtonColumn DataTextField="vendText" HeaderText="vend" CommandName="associated_vend">
                        <HeaderStyle Width="30px" HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle Width="30px" HorizontalAlign="Center"></ItemStyle>
                    </asp:ButtonColumn>
                    <asp:ButtonColumn Text="&lt;u&gt;Submit&lt;/u&gt;" CommandName="checkedBy" HeaderText="Submit">
                        <HeaderStyle HorizontalAlign="Center" Width="30px" Font-Bold="False"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                    </asp:ButtonColumn>
                    <asp:BoundColumn DataField="pictureNo" HeaderText="图号">
                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="description" HeaderText="Description">
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="verifycnt"></asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="creator"></asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="filename1"></asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="accFileName"></asp:BoundColumn>
                    <asp:BoundColumn DataField="createdname" HeaderText="上载者">
                        <HeaderStyle HorizontalAlign="Center" Width="60px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="60px" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="createdDate" HeaderText="上载日期">
                        <HeaderStyle HorizontalAlign="Center" Width="120px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" Width="120px" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                    </asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        </form>
    </div>
    <script type="text/javascript">
        <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
