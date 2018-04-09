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
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;

public partial class part_chk_maintainLocs : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        DataSet ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_chk_selectLocsInfo");
        gvLocs.DataSource = ds;
        gvLocs.DataBind();
        ds.Dispose();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            ltlAlert.Text = "alert('请重新登录！')";
        }
        else
        {
            string strSQL = string.Empty;
            string loc = string.Empty;
            string site = string.Empty;
            bool originalStatus = false;
            foreach (GridViewRow row in gvLocs.Rows)
            {
                CheckBox chkLocs = (CheckBox)row.FindControl("chkLocs");
                loc = row.Cells[0].Text;
                site = row.Cells[3].Text;
                if (gvLocs.DataKeys[row.RowIndex]["isActive"].ToString().ToLower() == "true")
                {
                    originalStatus = true;
                }
                else if (gvLocs.DataKeys[row.RowIndex]["isActive"].ToString().ToLower() == "false")
                {
                    originalStatus = false;
                }
                if (chkLocs.Checked != originalStatus)
                {
                    if (strSQL == string.Empty)
                    {
                        strSQL = loc + "-" + site;
                    }
                    else
                    {
                        strSQL = strSQL + ";" + loc + "-" + site;
                    }
                }
            }
            //Response.Write(strSQL);
            // return;
            bool isSuccess = false;

            if (strSQL != string.Empty)
            {
                try
                {
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@locs", strSQL);
                    param[1] = new SqlParameter("@uID", Session["uID"].ToString());
                    param[2] = new SqlParameter("@retValue", DbType.Boolean);
                    param[2].Direction = ParameterDirection.Output;

                    SqlHelper.ExecuteNonQuery(chk.dsnx(), CommandType.StoredProcedure, "sp_chk_updateLocsInfo", param);
                    isSuccess = Convert.ToBoolean(param[2].Value);
                }
                catch
                {
                    isSuccess = false;
                }
                if (isSuccess)
                {
                    ltlAlert.Text = "alert('保存成功！')";
                }
                else
                {
                    ltlAlert.Text = "alert('保存失败！请重试！')";
                }
            }
            BindData();
        }
    }

    protected void gvLocs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
}
