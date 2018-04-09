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
using QCProgress;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class pur_ResultVersion : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    PurResult result = new PurResult();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPro();
            GridViewBind();
        }
    }
    private void BindPro()
    {
        try
        {
            ddl_pro.DataSource = result.GetPro();
            ddl_pro.DataBind();
            ddl_pro.Items.Insert(0, "--请选择--");
            ddl_pro.SelectedIndex = 0;
        }
        catch
        {
            this.Alert("获取版本失败!");
        }
    }
    private void GridViewBind() 
    {
        string proid = "";
        string typename = "";
        if (ddl_pro.SelectedIndex == 0)
        {
            proid = "0";
        }
        else
        {
            proid = ddl_pro.SelectedValue;
        }
        typename = txt_typename.Text;

        try
        {
            gv.DataSource = result.GetTypeList(proid, typename);
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        GridViewBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (!result.DeleteTypeCheck(gv.DataKeys[e.RowIndex].Values["pur_typeid"].ToString()))
        {
            this.Alert("类型已在标准中被引用，无法删除，如需删除请先删除引用！");
            return;
        }

        if (result.DeleteType(gv.DataKeys[e.RowIndex].Values["pur_typeid"].ToString(), Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("删除成功！");
        }
        else
        {
            this.Alert("删除失败！");
        }

        GridViewBind();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        GridViewBind();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string typeid = gv.DataKeys[e.RowIndex].Values["pur_typeid"].ToString();
        TextBox typename = (TextBox)gv.Rows[e.RowIndex].FindControl("txt_typename");
        if (typename.Text.Length == 0)
        {
            this.Alert("类型名称不能为空！");
            return;
        }

        int i = result.UpateType(typeid, typename.Text == "" ? "" : typename.Text, Session["uID"].ToString(), Session["uName"].ToString());
        if (i == 1)
        {
            this.Alert("已存在相同名称！");
            return;
        }
        else if (i == 2 || i == 3) 
        {
            this.Alert("保存失败,请联系管理员！");
            return;
        }
        this.Alert("修改成功！");
        gv.EditIndex = -1;
        GridViewBind();
    }
    protected void gvShip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail")
        {
            if (gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
            {
                ltlAlert.Text = "alert('此行是空行！');";
                return;
            }
            string versionid = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_versionId"].ToString();
            string versionname = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_versionName"].ToString();
            string flag = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_versionFlag"].ToString();
            Response.Redirect("pur_ResultCheckStandard.aspx?id=" + versionid + "&name=" + versionname + "&flag=" + flag + "&rm=" + DateTime.Now);
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        //BindTypes();
        GridViewBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
    protected void ddl_pro_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (ddl_pro.SelectedIndex == 0)
        {
            this.Alert("请选择一个项目名称!");
            return;
        }
        if (string.IsNullOrEmpty(txt_typename.Text))
        {
            this.Alert("类别不能为空!");
            return;
        }
        string proid = ddl_pro.SelectedValue;
        string proname = ddl_pro.SelectedItem.Text;
        string typename = txt_typename.Text;
        string sysvalue = txt_sysvalue.Text;
        string score = "";
        if (string.IsNullOrEmpty(txt_score.Text))
        {
            this.Alert("分值不能为空!");
            return;
        }
        score = txt_score.Text;
        if (!result.SavTypeCheckScore(proid, score, typename))
        {
            this.Alert("类型分值不得大于项目总分值！");
            return;
        }

        int i = result.SavType(proid, proname, typename, score, sysvalue, Session["uID"].ToString(), Session["uName"].ToString());

        if (i == 1)
        {
            this.Alert("已存在相同记录!");
            return;
        }
        if (i == 2)
        {
            this.Alert("记录保存失败!");
            return;
        }
        this.Alert("记录保存成功!");
        GridViewBind();
    }

}
