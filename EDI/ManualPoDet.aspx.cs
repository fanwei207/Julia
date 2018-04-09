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
using CommClass;

public partial class ManualPoDet : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //有导入权限的人，方可删除
            gvlist.Columns[12].Visible = this.Security["10000030"].isValid;

            BindGridView();
        }
    }

    public DataSet GetManualPoDet(string hrd_id)
    {
        SqlParameter param = new SqlParameter("@hrd_id", hrd_id);
        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectManualPoDet", param);
        return ds;
    }

    private void BindGridView()
    {
        DataSet dsPo = GetManualPoDet(Request["hrdid"].ToString());

        if (dsPo.Tables[0].Rows.Count == 0)
        {
            dsPo.Tables[0].Rows.Add(dsPo.Tables[0].NewRow());
        }

        gvlist.DataSource = dsPo;
        gvlist.DataBind();
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //如果提交了，则不允许删除
            if (Convert.ToInt32(gvlist.DataKeys[e.Row.RowIndex].Values["mpod_submittedBy"].ToString()) > 0)
            {
                e.Row.Cells[12].Text = string.Empty;
            }
        }
    }
    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void gvlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@id", gvlist.DataKeys[e.RowIndex].Values["mpod_id"].ToString());
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_deleteManualPoDet", param);
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails!Please try again!');";
            return;
        }

        BindGridView();
    }
}
