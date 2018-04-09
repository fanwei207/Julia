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

public partial class wsline_wl_woTrackCanceloffline : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            onlineBeginDate.Text = DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd");

            BindPlants();
            BindData();
        }
    }

    private void BindData()
    {
        string strPalntCode = dropPlants.SelectedIndex == -1 ? Session["plantCode"].ToString() : dropPlants.SelectedValue;

        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@nbr", txbNbr.Text.Trim());
        param[1] = new SqlParameter("@onlineBeginDate", onlineBeginDate.Text.Trim());
        param[2] = new SqlParameter("@onlineEndDate", onlineEndDate.Text.Trim());
        param[3] = new SqlParameter("@offlineBeginDate", offlineBeginDate.Text.Trim());
        param[4] = new SqlParameter("@offlineEndDate", offlineEndDate.Text.Trim());
        param[5] = new SqlParameter("@plantCode", strPalntCode);
        DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_selectWoTrackForCancelOffline", param);

        gvWoTrack.DataSource = ds.Tables[0];
        gvWoTrack.DataBind();
        //gvWoTrack.PageIndex = 0;

        if (ds != null)
        {
            lblWoCount.Text = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblWoCount.Text = "0";
        }

        if (ds != null)
        {
            ds.Dispose();
        }
    }

    private void BindPlants()
    {
        string strSQL = "SELECT plantID,description From Plants where isAdmin=0 and plantID in(1, 2, 5, 8) order by plantID";
        dropPlants.Items.Clear();

        try
        {
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dropPlants.Items.Add(new ListItem(reader["description"].ToString(), reader["plantID"].ToString()));
                }
                reader.Close();
            }
        }
        catch
        { }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvWoTrack_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[7].Text.ToString() == "&nbsp;" || e.Row.Cells[7].Text.ToString() == string.Empty)
            {
                e.Row.Cells[8].Text = "";
            }
        }
    }

    protected void gvWoTrack_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWoTrack.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvWoTrack_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "undoOffline")
        {
             
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string _woNbr = gvWoTrack.DataKeys[index]["nbr"].ToString();
            string _woLotID = gvWoTrack.DataKeys[index]["lot"].ToString(); 

            string strPalntCode = dropPlants.SelectedIndex == -1 ? Session["plantCode"].ToString() : dropPlants.SelectedValue;
            string userID = Session["uID"].ToString();
            string userName = Session["uName"].ToString(); 

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@nbr", _woNbr);
            param[1] = new SqlParameter("@lot", _woLotID);
            param[2] = new SqlParameter("@plantCode", strPalntCode);
            param[3] = new SqlParameter("@uID", userID);
            param[4] = new SqlParameter("@uName", userName);

            if (Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_wo2_updateWoTrackToCancelOffline", param)))
            {
                ltlAlert.Text = "alert('已取消加工单" + _woNbr + "的下线操作,请至工单追踪处查看');";
            }
            else
            {
                ltlAlert.Text = "alert('取消加工单" + _woNbr + "的下线操作失败,请重试');";
            }

            BindData();
        }

    }



}