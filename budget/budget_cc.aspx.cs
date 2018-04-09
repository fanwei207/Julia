using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using BudgetProcess;
using System.Data.SqlTypes;

public partial class budget_cc : BasePage
{
    adamClass chk = new adamClass();
    int nRet;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            
            btnAdd.Attributes.Add("onclick", "return CheckAll()");
            
            BindData();
        }

        #region Check All JavaScript Code
        string scr = @"<script>
            function CheckAll()
            {
                try 
                {   
                    var domain = document.getElementById('" + this.txtDomain.ClientID + "').value;";
                    scr = scr + @"
                    var code = document.getElementById('" + this.txtCode.ClientID + "').value;";
                    scr = scr + @"
                    var desc = document.getElementById('" + this.txtDesc.ClientID + "').value;";
                    scr = scr + @"
                    var dept = document.getElementById('" + this.txtDept.ClientID + "').value;";
                    scr = scr + @"
                    if(domain == '')
                    {
                        alert('域不能为空!');
                        return false;
                    }
                    if(domain.toLowerCase() == 'szx' || domain.toLowerCase() == 'zql' || domain.toLowerCase() == 'zqz' || domain.toLowerCase() == 'yql') {}
                    else
                    {
                        alert('域只可以是SZX,ZQL,ZQZ,YQL!');
                        return false;
                    }
                    if(code == '')
                    {
                        alert('成本中心编号不能为空!');
                        return false;
                    }
                    if(dept == '')
                    {
                        alert('部门不能为空!');
                        return false;
                    }   
                    if(desc == '')
                    {
                        alert('成本中心描述不能为空!');
                        return false;
                    }        
                    return confirm('确认吗?');
                }
                catch(e)
                {
                    //alert(e.description);  
                }
                finally{}
            }            
            </script>";

        Page.ClientScript.RegisterStartupScript(this.GetType(), "checkall", scr);
        #endregion 

    }

    public void BindData()
    {
        DataSet dst = Budget.getBudgetCC(txtDomain.Text.Trim(), txtCode.Text.Trim(), txtMaster.Text.Trim(), txtDept.Text.Trim(),txtDesc.Text.Trim());
        dgBudgetCC.DataSource = dst;

        if (dgBudgetCC.CurrentPageIndex > (dst.Tables[0].Rows.Count - SqlInt32.Mod(dst.Tables[0].Rows.Count, dgBudgetCC.PageSize)) / dgBudgetCC.PageSize + 1 || dst.Tables[0].Rows.Count == 0)
        {
            dgBudgetCC.CurrentPageIndex = 0;
        }
        dgBudgetCC.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        switch (Budget.insertBudgetCC(txtDomain.Text.Trim(), txtCode.Text.Trim(), txtDept.Text.Trim(), txtDesc.Text.Trim()))
        {
            case 0:
                ltlAlert.Text = "alert('添加数据失败!');";
                break;

            case -1:
                ltlAlert.Text = "alert('添加数据已存在!');";
                break;

            default:
                txtDomain.Text = "";
                txtCode.Text = "";
                txtDept.Text = "";
                txtDesc.Text = "";
                txtMaster.Text = "";
                ltlAlert.Text = "alert('添加成功!');";
                break;
        }
        BindData();
    }

    protected void dgBudgetCC_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
           
        }
    }

    protected void dgBudgetCC_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        TableCell myDeleteButton;
        switch(e.Item.ItemType)
        {
            case ListItemType.EditItem:
                //Where 1 is the column containing your ButtonColumn 
                ((TextBox)e.Item.Cells[1].Controls[0]).Width = new Unit(30);
                ((TextBox)e.Item.Cells[1].Controls[0]).MaxLength  = 3;
                ((TextBox)e.Item.Cells[1].Controls[0]).CssClass = "TextLeft";
                ((TextBox)e.Item.Cells[1].Controls[0]).Height = new Unit(16);
                ((TextBox)e.Item.Cells[2].Controls[0]).Width = new Unit(90);
                ((TextBox)e.Item.Cells[2].Controls[0]).MaxLength = 30;
                ((TextBox)e.Item.Cells[2].Controls[0]).CssClass = "TextLeft";
                ((TextBox)e.Item.Cells[2].Controls[0]).Height = new Unit(16);
                ((TextBox)e.Item.Cells[3].Controls[0]).Width = new Unit(60);
                ((TextBox)e.Item.Cells[3].Controls[0]).MaxLength = 4;
                ((TextBox)e.Item.Cells[3].Controls[0]).CssClass = "TextLeft";
                ((TextBox)e.Item.Cells[3].Controls[0]).Height = new Unit(16);
                ((TextBox)e.Item.Cells[4].Controls[0]).Width = new Unit(120);
                ((TextBox)e.Item.Cells[4].Controls[0]).MaxLength = 30;
                ((TextBox)e.Item.Cells[4].Controls[0]).CssClass = "TextLeft";
                ((TextBox)e.Item.Cells[4].Controls[0]).Height = new Unit(16);
                break; 

        }
    }

    protected void dgBudgetCC_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgBudgetCC.EditItemIndex = -1;
        BindData();
    }

    protected void dgBudgetCC_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string strdomain = ((TextBox)e.Item.Cells[1].Controls[0]).Text.Trim();
        string strcode = ((TextBox)e.Item.Cells[3].Controls[0]).Text.Trim();
        string strdept = ((TextBox)e.Item.Cells[2].Controls[0]).Text.Trim();
        string strdesc = ((TextBox)e.Item.Cells[4].Controls[0]).Text.Trim();

        if (strdomain == "")
        {
            ltlAlert.Text = "alert('域不能为空!');";
            return;
        }
        if (strdomain.ToUpper() != "SZX" && strdomain.ToUpper() != "ZQL" && strdomain.ToUpper() != "ZQZ" && strdomain.ToUpper() != "YQL"
            && strdomain.ToUpper() != "ZF")
        {
            ltlAlert.Text = "alert('域只可以是SZX,ZQL,ZQZ,YQL,ZF!');";
            return;
        }
        if (strcode == "")
        {
            ltlAlert.Text = "alert('成本中心编号不能为空!');";
            return;
        }
        if (strdept == "")
        {
            ltlAlert.Text = "alert('部门不能为空!');";
            return;
        }
        if (strdesc == "")
        {
            ltlAlert.Text = "alert('成本中心不能为空!');";
            return;
        }
        

        Budget.updateBudgetCC(e.Item.Cells[0].Text.Trim(),strdomain, strcode, strdept, strdesc);
        dgBudgetCC.EditItemIndex = -1;
        BindData();
    }

    protected void dgBudgetCC_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "DeleteBtn":
                Budget.deleteBudgetCC(e.Item.Cells[0].Text.Trim());
                BindData();
                break;

            case "ModifyBtn":
                ltlAlert.Text = "window.open('/budget/budget_manager.aspx?type=2&id=" + e.Item.Cells[0].Text.Trim() + "&rm=" + DateTime.Now 
                    + "','','menubar=no,scrollbars=no,resizable=no,width=800,height=500,top=0,left=0');";
                break;

            case "ReaderBtn":
                ltlAlert.Text = "window.open('/budget/budget_manager.aspx?type=1&id=" + e.Item.Cells[0].Text.Trim() + "&rm=" + DateTime.Now 
                    + "','','menubar=no,scrollbars=no,resizable=no,width=800,height=500,top=0,left=0');";
                break;

            case "MasterBtn":
                ltlAlert.Text = "window.open('/budget/budget_manager.aspx?type=0&id=" + e.Item.Cells[0].Text.Trim() + "&rm=" + DateTime.Now 
                    + "','','menubar=no,scrollbars=no,resizable=no,width=800,height=500,top=0,left=0');";
                break;

            default:
                break;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        dgBudgetCC.CurrentPageIndex = 0;
        BindData();
    }

    protected void dgBudgetCC_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgBudgetCC.EditItemIndex = -1;
        dgBudgetCC.CurrentPageIndex = e.NewPageIndex;
        BindData();
    }

    protected void dgBudgetCC_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgBudgetCC.EditItemIndex = e.Item.ItemIndex;
        BindData();
    }
}
