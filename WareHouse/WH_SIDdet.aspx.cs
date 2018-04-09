using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Principal;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data.OleDb;
public partial class plan_WH_SIDdet : System.Web.UI.Page
{
    private static adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string id = Request.QueryString["id"];
            BindGridView(id);
        }
    }
    public void BindGridView(string id)
    {
        DataTable dt = GetWhViewDet(id);
        foreach (DataRow item in dt.Rows)
        {
            txtshipto.Text = item["SID_shipto"].ToString();
            txtdomain.Text = item["SID_domain"].ToString();
            txtPK.Text = item["SID_PK"].ToString();
            txtnbr.Text = item["SID_nbr"].ToString();
        }
        gv.DataSource = GetWhView(id);
        gv.DataBind();
    }
    public static DataTable GetWhView(string id)
    {
        string strName = "sp_wh_selectWhSidDet";
        SqlParameter[] parm = new SqlParameter[1];
        parm[0] = new SqlParameter("@id", id);
        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        return _dataset.Tables[0];
    }

    public static DataTable GetWhViewDet(string id)
    {
        string strName = "sp_wh_selectWhViewDet";
        SqlParameter[] parm = new SqlParameter[5];
        parm[0] = new SqlParameter("@nbr", "");
        parm[1] = new SqlParameter("@domain", "");
        parm[2] = new SqlParameter("@lot", "");
        parm[3] = new SqlParameter("@type", "ISS-SO");
        parm[4] = new SqlParameter("@id", id);
        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        return _dataset.Tables[0];
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        if (updatetwhView(id))
        {
            //ltlAlert.Text = "alert('提交成功!');";
            ltlAlert.Text = "window.close();";
        }
        else
        {
            ltlAlert.Text = "alert('提交失败!');";
        }
    }
    public static bool updatetwhView(string id)
    {
        try
        {
            string strName = "sp_wh_updatewhSID";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@id", id);
            SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;
            //只要存在一条未扫描的，则无法提交
            if (Convert.ToInt32(rowView["wh_qty_loc"]) == 0)
            {
                btn_update.Enabled = false;
            }
        }
    }
    protected void btn_cancel_Click(object sender, EventArgs e)
    {
        if (txt_reason.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须要填写理由!');";
            return;
        } 
        string id = Request.QueryString["id"];

        string strName = "sp_wh_cancelwhSid";
        SqlParameter[] parm = new SqlParameter[4];
        parm[0] = new SqlParameter("@id", id);
        parm[1] = new SqlParameter("@cancelBy", Convert.ToInt32(Session["uID"]));
        parm[2] = new SqlParameter("@cancelName", Session["uName"].ToString());
        parm[3] = new SqlParameter("@reason", txt_reason.Text.Trim());
        try
        {
            SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        }
        catch
        {
            ltlAlert.Text = "alert('取消失败!');";
        }
        ltlAlert.Text = "window.close();";
    }
}