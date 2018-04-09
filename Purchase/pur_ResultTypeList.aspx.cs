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

public partial class pur_ResultTypeList : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    PurResult result = new PurResult();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewBind();
            txt_version.Text = Request.QueryString["name"];
            txt_flag.Text = Request.QueryString["flag"];
            gv.Columns[0].Visible = false;
        }
    }
    private void GridViewBind() 
    {
        string versionid = Request.QueryString["id"].ToString();
        if (result.GetTypeListById(versionid).Rows.Count > 0)
        {
            //gv.Columns[0].Visible = false;
            //gv.Columns[6].Visible = false;
            //gv.Columns[9].Visible = false;
        }

        try
        {
            gv.DataSource = result.GetTypeList(versionid, "0", "", Session["uID"].ToString(), Session["uName"].ToString());
            gv.DataBind();
        }
        catch
        {
            ;
        }
        //foreach (GridViewRow row in gv.Rows)
        //{
        //    HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

        //    string typeid = gv.DataKeys[row.RowIndex].Values["pur_typeid"].ToString();

        //    //TextBox txDesc = (TextBox)gv.Rows[e.RowIndex].FindControl("txDesc");
        //    string typename = gv.DataKeys[row.RowIndex].Values["pur_typename"].ToString();

        //    DataTable dt = result.GetTypeListById(Request.QueryString["id"].ToString());
        //    for (int i = 0; i <= dt.Rows.Count; i++)
        //    {
        //        if (typename == dt.Rows[i]["pur_typename"].ToString())
        //        {
        //            chkImport.Checked = true;
        //        }
        //    }
        //}
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
            CheckBox chk = e.Row.FindControl("chkAll") as CheckBox;
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)e.Row.FindControl("chkImport");
            if (Convert.ToBoolean(gv.DataKeys[e.Row.RowIndex].Values["checkok"]))
            {
                chkImport.Checked = true;
            }
            else
            {
                chkImport.Checked = false;
            }
        }
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //if (!result.DeleteTypeCheck(gv.DataKeys[e.RowIndex].Values["pur_typeid"].ToString()))
        //{
        //    this.Alert("类型已在标准中被引用，无法删除，如需删除请先删除引用！");
        //    return;
        //}

        if (result.DeleteTypeVersion(gv.DataKeys[e.RowIndex].Values["pur_typeid"].ToString(), Session["uID"].ToString(), Session["uName"].ToString()))
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
        TextBox maxscore = (TextBox)gv.Rows[e.RowIndex].FindControl("txt_score");
        if (maxscore.Text.Length == 0)
        {
            this.Alert("最大分值不能为空！");
            return;
        }
        TextBox sysvalue = (TextBox)gv.Rows[e.RowIndex].FindControl("txt_sysvalue");
        if (sysvalue.Text.Length == 0)
        {
            sysvalue.Text = "";
        }
        string versionid = Request.QueryString["id"].ToString();
        int i = result.InsertVersionList(typeid, versionid, typename.Text, maxscore.Text == "" ? "0" : maxscore.Text, sysvalue.Text == "" ? "" : sysvalue.Text, false, Session["uID"].ToString(), Session["uName"].ToString());
        if (i == 1)
        {
            this.Alert("保存失败,请联系管理员！");
            return;
        }

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
            string typeid = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_typeid"].ToString();
            string versionid = Request.QueryString["id"];
            string versionname = Request.QueryString["name"];
            string flag = Request.QueryString["flag"];
            string maxvalue = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["maxvalue"].ToString();
            Response.Redirect("pur_ResultCheckStandard.aspx?typeid=" + typeid + "&id=" + versionid + "&name=" + versionname + "&flag=" + flag + "&maxvalue=" + maxvalue + "&rm=" + DateTime.Now);
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;

        foreach (GridViewRow row in gv.Rows)
        {
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

            chkImport.Checked = chkAll.Checked;
        }
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("pur_ResultVersion.aspx?" + "&rm=" + DateTime.Now);
    }
    protected void btn_valueAdd_Click(object sender, EventArgs e)
    {
        string versionid = Request.QueryString["id"];
        string versionname = Request.QueryString["name"];
        string flag = Request.QueryString["flag"];
        Response.Redirect("pur_ResultTypeListAdd.aspx?id=" + versionid + "&name=" + versionname + "&flag=" + flag + "&rm=" + DateTime.Now);
        /*
        foreach (GridViewRow row in gv.Rows)
        {
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

            if (chkImport.Checked)
            {
                string typeid = gv.DataKeys[row.RowIndex].Values["pur_typeid"].ToString();

                //TextBox txDesc = (TextBox)gv.Rows[e.RowIndex].FindControl("txDesc");
                Label maxvalue = (Label)gv.Rows[row.RowIndex].FindControl("lb_typename");

                Label sysvalue = (Label)gv.Rows[row.RowIndex].FindControl("lb_sysvalue");
                //TextBox sysvalue = (TextBox)gv.Rows[row.RowIndex].FindControl("txt_sysvalue");


                string versionid = Request.QueryString["id"].ToString();
                int i = result.InsertVersionList(typeid, versionid, maxvalue.Text == "" ? "0" : maxvalue.Text, sysvalue.Text == "" ? "" : sysvalue.Text, true, Session["uID"].ToString(), Session["uName"].ToString());
                if (i == 1)
                {
                    this.Alert("保存失败,请联系管理员！");
                    return;
                }
            }
        }
        gv.EditIndex = -1;
        GridViewBind();
         */ 
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        string versionid = Request.QueryString["id"].ToString();
        int i = result.DeleteVersionList(versionid, Session["uID"].ToString(), Session["uName"].ToString());
    }
}
