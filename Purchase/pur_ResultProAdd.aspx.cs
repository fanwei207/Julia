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

public partial class pur_ResultProAdd : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.qadplan"];
    PurResult result = new PurResult();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewBind();
        }
    }
    private void GridViewBind() 
    {
        string proname = txt_pro.Text;
        try
        {
            gv.DataSource = result.GetProList(proname);
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

        if(!result.DeleteProCheck(gv.DataKeys[e.RowIndex].Values["pur_id"].ToString()))
        {
            this.Alert("项目名称已被类型引用，无法删除，需先删除引用再做删除！");
            return;
        }

        string id = gv.DataKeys[e.RowIndex].Values["pur_id"].ToString();
        if (result.DeletePro(gv.DataKeys[e.RowIndex].Values["pur_id"].ToString(), Session["uID"].ToString(), Session["uName"].ToString()))
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
        string _id = gv.DataKeys[e.RowIndex].Values["vcc_id"].ToString();
        TextBox txDesc = (TextBox)gv.Rows[e.RowIndex].FindControl("txDesc");
        if (txDesc.Text.Length == 0)
        {
            this.Alert("罚款科目不能为空！");
            return;
        }
        try
        {
            string strName = "sp_vc_updateVendCompCate";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@id", _id);
            param[1] = new SqlParameter("@name", txDesc.Text);
            param[2] = new SqlParameter("@uID", Session["uID"].ToString());
            param[3] = new SqlParameter("@uName", Session["uName"].ToString());
            param[4] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (Convert.ToBoolean(param[4].Value))
            {
                this.Alert("更新成功！");
            }
            else
            {
                this.Alert("更新失败！");
            }
        }
        catch
        {
            this.Alert("更新失败！请联系管理员！"); ;
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
            string versionid = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_versionId"].ToString();
            string versionname = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_versionName"].ToString();
            string flag = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values["pur_versionFlag"].ToString();
            Response.Redirect("pur_ResultCheckStandard.aspx?id=" + versionid + "&name=" + versionname + "&flag=" + flag + "&rm=" + DateTime.Now);
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_pro.Text))
        {
            this.Alert("项目名称不能为空!");
            return;
        }
        if (string.IsNullOrEmpty(txt_scoreTotal.Text))
        {
            this.Alert("总分值不能为空!");
            return;
        }
        string proname = txt_pro.Text;
        string proScore = txt_scoreTotal.Text;
        int i = result.SavPro(proname, proScore, Session["uID"].ToString(), Session["uName"].ToString());

        if (i == 1)
        {
            this.Alert("已存在相同名称!");
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
