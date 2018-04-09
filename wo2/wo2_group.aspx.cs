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
using WO2Group;

public partial class wo2_group : BasePage
{
    adamClass chk = new adamClass();
    WO2 wo2 = new WO2();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        btnAdd.Enabled = this.Security["600000004"].isValid;
        if (!IsPostBack)
        {
            BindData();
        }
    }

    public void BindData()
    {
        //定义参数
        string strCode = txtGroupCode.Text.Trim();
        string strName = txtGroupName.Text.Trim();

        if (strCode == "") strCode = "0";

        gvGroup.DataSource = wo2.SelectGroupList(strCode, strName);
        gvGroup.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvGroup_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //定义参数
        string strCode = txtGroupCode.Text.Trim();
        string strName = txtGroupName.Text.Trim();
        int uID = Convert.ToInt32(Session["uID"]);

        if (strCode.Length <= 0)
        {
            ltlAlert.Text = "alert('用户组代码不能为空!');";
            return;
        }
        else
        {
            try
            {
                Decimal _dc = Convert.ToDecimal(txtGroupCode.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('用户组代码 只能为数字！');";
                return;
            }
        }

        if (strName.Length <= 0)
        {
            ltlAlert.Text = "alert('用户组名称不能为空!');";
            return;
        }

        if (wo2.InsertGroup(Convert.ToInt32(strCode), strName, uID))
        {
            ltlAlert.Text = "alert('用户组信息新增成功！'); window.location.href='" + Request.ServerVariables["Http_Referer"] + "'";
            txtGroupCode.Text = "";
            txtGroupName.Text = "";
        }
        else
        {
            ltlAlert.Text = "alert('新增数据过程中出错！');";
        }

    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvGroup_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (wo2.DeleteGroup(Convert.ToInt32(gvGroup.DataKeys[e.RowIndex].Value.ToString())))
        {
            ltlAlert.Text = "alert('删除用户组数据成功！'); window.location.href='" + Request.ServerVariables["Http_Referer"] + "'";
        }
        else
        {
            ltlAlert.Text = "alert('删除数据过程中出错！');";
        }
    }

    protected void gvGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        int intRow = 0;
        string strGroupID = string.Empty;
        string strCode = string.Empty;
        string strName = string.Empty;

        if (e.CommandName == "ViewDetail")
        {
            intRow = Convert.ToInt32(e.CommandArgument.ToString());
            strGroupID = gvGroup.DataKeys[intRow].Value.ToString();

            strCode = gvGroup.Rows[intRow].Cells[0].Text.Trim();
            strName = ((Label)gvGroup.Rows[intRow].FindControl("lblName")).Text.Trim();

            Response.Redirect("/wo2/wo2_GroupView.aspx?id=" + strGroupID + "&code=" + strCode + "&name=" + Server.UrlEncode(strName));
        }
    }

    protected void gvGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvGroup.PageIndex = e.NewPageIndex;

        BindData();
    }

    protected void gvGroup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            if (this.Security["600000005"].isValid)
            {
                btnDelete.Attributes.Add("onclick", "return confirm('你确认要删除吗?')");
            }
            else
            {
                btnDelete.Enabled = false;
                btnDelete.Text = "";
            }
        }
    }

    protected void gvGroup_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvGroup.EditIndex = e.NewEditIndex;
        BindData();
    }

    protected void gvGroup_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //定义参数
        int intGroupID = Convert.ToInt32(gvGroup.DataKeys[e.RowIndex].Value.ToString());
        string strName = ((TextBox)gvGroup.Rows[e.RowIndex].FindControl("txtName")).Text.Trim();

        if (strName.Length <= 0)
        {
            ltlAlert.Text = "alert('用户组名称不能为空！');";
            return;
        }
        else
        {
            if (wo2.UpdateGroup(intGroupID, strName))
            {
                ltlAlert.Text = "alert('更新用户组数据成功！'); window.location.href='" + Request.ServerVariables["Http_Referer"] + "'";
            }
            else
            {
                ltlAlert.Text = "alert('更新数据过程中出错！');";
            }
        }
    }

    protected void gvGroup_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvGroup.EditIndex = -1;
        BindData();
    }
}
