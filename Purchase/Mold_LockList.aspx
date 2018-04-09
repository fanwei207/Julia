<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mold_LockList.aspx.cs" Inherits="Purchase_Mold_LockList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link media="all" href="../css/main.css" rel="stylesheet" />
    <script language="JavaScript" src="../script/jquery-latest.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.object.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.validate.js" type="text/javascript"></script>
    <script language="JavaScript" src="../script/julia.common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        $(function () {
            $("input[name$='chkAll']:eq(1)").remove();
            $("#chkAll").click(function () {
                $("#gvDet input[type='checkbox'][id$='chk'][disabled!='disabled']").prop("checked", $(this).prop("checked"))
            })
        })
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="width:1200px">
            <label>QAD<asp:TextBox ID="txtQAD" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label> 
            &nbsp;&nbsp;&nbsp;&nbsp;
            <label>供应商<asp:TextBox ID="txtVender" runat="server" CssClass="SmallTextBox5 Supplier" Width="80px"></asp:TextBox></label> 
            &nbsp;&nbsp;&nbsp;&nbsp;
            <label>供应商名<asp:TextBox ID="txtvenderName" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label> 
            &nbsp;&nbsp;&nbsp;&nbsp;
            <label>模具类<asp:TextBox ID="txtMoldMstrName" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label> 
            &nbsp;&nbsp;&nbsp;&nbsp;
             <label>模具号<asp:TextBox ID="txtMoldDetName" runat="server" CssClass="SmallTextBox5" Width="80px"></asp:TextBox></label> 
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSelect" runat="server" Width="60px"  CssClass="SmallButton2" Text="查询" OnClick="btnSelect_Click"/>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnFree" runat="server"  Width="60px" CssClass="SmallButton2" Text="解锁" OnClick="btnFree_Click"/>
            <label style="color:red">*本页面显示的均为锁定的模具</label>

        </div>
        <div>
            <asp:GridView ID="gvDet" runat="server"  Width="900px"
                            AutoGenerateColumns="False" AllowPaging="true" PageSize="100"
                            CssClass="GridViewStyle GridViewRebuild" DataKeyNames="detID,moldDetName,moldMstrName,Mold_Vend,Mold_VendName,Mold_Status" OnRowDataBound="gvDet_RowDataBound" OnRowCommand="gvDet_RowCommand">
                            <FooterStyle CssClass="GridViewFooterStyle" Font-Bold="False" />
                            <RowStyle CssClass="GridViewRowStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <PagerStyle CssClass="GridViewPagerStyle" />
                            <AlternatingRowStyle CssClass="GridViewAlternatingRowStyle" />
                            <HeaderStyle CssClass="GridViewHeaderStyle" />
                            <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                            <Columns>
                                 <asp:TemplateField>
                                    <HeaderTemplate>
                                            <input id="chkAll" type="checkbox">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server"/>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 <asp:BoundField HeaderText="模具类" DataField="moldMstrName" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                 <asp:BoundField HeaderText="模具号" DataField="moldDetName" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>

                                <asp:BoundField HeaderText="供应商" DataField="Mold_Vend" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="供应商名称" DataField="Mold_VendName" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="200px" />
                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                </asp:BoundField>
                               <asp:TemplateField HeaderText="文档">
                                <ControlStyle Font-Bold="False" Font-Size="12px" Font-Underline="True" />
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lkbtnSelectQADDOC" runat="server" CommandName="lkbtnSelectQADDOC" CommandArgument='<%# Eval("detID") %>'
                                        Text="view"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:BoundField HeaderText="状态" DataField="Mold_Status" ReadOnly="True">
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                

                            </Columns>
                        </asp:GridView>

        </div>
    </div>
    </form>
     <script type="text/javascript">
         <asp:literal runat="server" id="ltlAlert" EnableViewState="False"></asp:literal>
    </script>
</body>
</html>
