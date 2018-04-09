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
            BindVersion();
            GridViewBind();
        }
    }
    private void BindVersion()
    {
        try
        {
            string strName = "sp_purresult_selectVersion";

            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName);
            ddl_version.DataSource = ds;
            ddl_version.DataBind();
            ddl_version.Items.Insert(0, "--请选择--");
            ddl_version.SelectedIndex = 0;
        }
        catch
        {
            this.Alert("获取版本失败!");
        }
    }
    private void GridViewBind() 
    {
        string versionid = "";
        if (ddl_version.SelectedIndex == 0)
        {
            versionid = "0";
        }
        else
        {
            versionid = ddl_version.SelectedValue;
        }
        try
        {
            gv.DataSource = result.GetVersionList(versionid);
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
        if (result.DeleteVersion(gv.DataKeys[e.RowIndex].Values["pur_versionId"].ToString(), Session["uID"].ToString(), Session["uName"].ToString()))
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
            Response.Redirect("pur_ResultTypeList.aspx?id=" + versionid + "&name=" + versionname + "&flag=" + flag + "&rm=" + DateTime.Now);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {        

        try
        {
            //string strName = "sp_vc_insertVendCompCate";
            //SqlParameter[] param = new SqlParameter[4];
            //param[0] = new SqlParameter("@name", ddl_checkpro.SelectedValue);
            //param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            //param[2] = new SqlParameter("@uName", Session["uName"].ToString());
            //param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            //param[3].Direction = ParameterDirection.Output;

            //SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            //if (Convert.ToBoolean(param[3].Value))
            //{
            //    this.Alert("新建成功！");
            //}
            //else
            //{
            //    this.Alert("新建失败！");
            //}
        }
        catch
        {
            this.Alert("新建失败！请联系管理员！"); ;
        }

        GridViewBind();
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
    protected void ddl_version_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewBind();
        if (ddl_version.SelectedIndex != 0)
        {
            int re = result.GetVersionValue(ddl_version.SelectedValue, Session["uID"].ToString(), Session["uName"].ToString());
            txt_valueTotal.Text = re.ToString();
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        string versionname = txt_VersionName.Text;
        string startdate;
        string enddate;
        if (string.IsNullOrEmpty(versionname))
        {
            this.Alert("版本名称不能为空!");
            return;
        }
        startdate = txt_StartDate.Text.Trim();
        try
        {
            Convert.ToDateTime(txt_StartDate.Text.Trim());
        }
        catch
        {
            this.Alert("版本起始日期必须为日期格式!");
            return;
        }
        enddate = txt_EndDate.Text;
        try
        {
            if (!string.IsNullOrEmpty(txt_EndDate.Text))
            {
                Convert.ToDateTime(txt_EndDate.Text);
                if (Convert.ToDateTime(enddate) < Convert.ToDateTime(startdate))
                {
                    this.Alert("版本截至日期必须大于截至日期!");
                    return;
                }
            }
        }
        catch
        {
            this.Alert("版本截至日期必须为日期格式!");
            return;
        }
        try
        {
            int result1 = (int)result.CheckVersionExists(versionname, startdate, enddate);
            if (result1 == 1)
            {
                this.Alert("此版本名称已存在!");
                return;
            }
            if (result1 == 2 || result1 == 3)
            {
                this.Alert("此区间已存在记录!");
                return;
            }
            if (result1 == 4)
            {
                this.Alert("区间必须与上个版本区间连续!");
                return;
            }
            if (!result.SavVersion(versionname, startdate, enddate, Session["uID"].ToString(), Session["uName"].ToString()))
            {
                this.Alert("记录保存失败!");
                return;
            }
            this.Alert("记录保存成功!");
        }
        catch
        {
            this.Alert("记录保存失败!");
            return;
        }
        div_add.Visible = false;
        btn_save.Visible = false;
        BindVersion();
        GridViewBind();

    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        div_add.Visible = true;
        btn_save.Visible = true;
        btn_cancel.Visible = true;

    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        div_add.Visible = false;
        btn_save.Visible = false;
    }
    protected void btn_start_Click(object sender, EventArgs e)
    {
        if (ddl_version.SelectedIndex == 0)
        {
            this.Alert("请先选择一个启用版本!");
            return;
        }
        //int re = result.StartVersionCheckValue(ddl_version.SelectedValue, Session["uID"].ToString(), Session["uName"].ToString());
        //if (re < 100)
        //{
        //    this.Alert("此版本总分值为:"+re+",总分值小于100,无法启用!");
        //    return;
        //}
        //if (txt_valueTotal.Text != "100")
        //{
        //    this.Alert("此版本总分值为:" + txt_valueTotal.Text + ",总分值小于100,无法启用!");
        //    return;
        //}
        if (!result.StartVersionCheck(ddl_version.SelectedValue, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("已停用版本无法启用,请建立新的版本!");
            return;
        }
        if (!result.StartVersion(ddl_version.SelectedValue, true, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("记录保存失败!");
            return;
        }
        this.Alert("版本" + ddl_version.SelectedItem.Text + "已启用!");
        GridViewBind();
    }
    protected void btn_stop_Click(object sender, EventArgs e)
    {
        if (ddl_version.SelectedIndex == 0)
        {
            this.Alert("请先选择一个启用版本!");
            return;
        }
        if (!result.StartVersion(ddl_version.SelectedValue, false, Session["uID"].ToString(), Session["uName"].ToString()))
        {
            this.Alert("记录保存失败!");
            return;
        }
        this.Alert("版本" + ddl_version.SelectedItem.Text + "已停用!");
        GridViewBind();
    }
}
