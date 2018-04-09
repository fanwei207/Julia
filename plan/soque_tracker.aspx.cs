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

public partial class plan_soque_tracker : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPlants();
            BindData();
        }
    }

    protected void BindPlants()
    {
        dropPlants.Items.Clear();
        dropPlants.Items.Add(new ListItem("--请选择一个公司--", "0"));
        string strSQL = "select plantID, description plantName from Plants where isAdmin = 0 order by plantID";
        SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL);
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                dropPlants.Items.Add(new ListItem(reader["plantName"].ToString(), reader["plantID"].ToString()));
            }
        }
        reader.Close();
    }

    protected void BindData()
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@userNo", txbUserNo.Text.Trim());
        param[1] = new SqlParameter("@plantCode", dropPlants.SelectedValue);
        DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_plan_selectSoqueTracker", param);
        gvTrackers.DataSource = ds;
        gvTrackers.DataBind();
        ds.Dispose();
        labTrackerID.Text = string.Empty;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            ltlAlert.Text = "alert('请重新登录！')";
            return;
        }
        if (dropPlants.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('请选择一个公司！')";
            return;
        }
        if (txbUserNo.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请填写工号！')";
            return;
        }

        int success = 1;
        if (btnUpdate.Text == "新增")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@userNo", txbUserNo.Text.Trim());
                param[1] = new SqlParameter("@plantCode", dropPlants.SelectedValue);
                param[2] = new SqlParameter("@uID", Convert.ToInt32(Session["uID"]));
                param[3] = new SqlParameter("@retValue", DbType.Int32);
                param[3].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_plan_insertSoqueTracker", param);
                success = Convert.ToInt32(param[3].Value);
            }
            catch
            {
                success = 0;
            }

            if (success == -1)
            {
                ltlAlert.Text = "alert('新输入的用户不存在！')";
            }
            else if (success == -2)
            {
                ltlAlert.Text = "alert('该追踪人已存在！')";
            }
            else if (success == 0)
            {
                ltlAlert.Text = "alert('新增追踪人失败！')";
            }
        }
        else if (btnUpdate.Text == "保存")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@id", labTrackerID.Text);
                param[1] = new SqlParameter("@plantCode", dropPlants.SelectedValue);
                param[2] = new SqlParameter("@userNo", txbUserNo.Text);
                param[3] = new SqlParameter("uID", Convert.ToInt32(Session["uID"]));
                param[4] = new SqlParameter("@retValue", DbType.Int32);
                param[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_plan_updateSoqueTracker", param);
                success = Convert.ToInt32(param[4].Value);
            }
            catch
            {
                success = 0;
            }
            if (success == 1)
            { }
            else if (success == 0)
            {
                ltlAlert.Text = "alert('更新失败！')";
            }
            else
            {
                ltlAlert.Text = "alert('新输入的用户不存在！')";
            }

            btnUpdate.Text = "新增";
        }
        else
        {
            btnUpdate.Text = "新增";
        }

        dropPlants.SelectedIndex = 0;
        txbUserNo.Text = string.Empty;
        BindData();
    }

    protected void gvTrackers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTrackers.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvTrackers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='lightblue';");
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
        }
    }

    protected void gvTrackers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {
            labTrackerID.Text = e.CommandArgument.ToString();
            string strSQL = "select sqt_plant, sqt_userNo from soque_tracker where sqt_id = " + e.CommandArgument.ToString();
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL);
            if (reader.HasRows && reader.Read())
            {
                dropPlants.SelectedValue = reader["sqt_plant"].ToString();
                txbUserNo.Text = reader["sqt_userNo"].ToString();
            }
            reader.Close();

            btnUpdate.Text = "保存";
        }
        if (e.CommandName == "myDelete")
        {
            string strSQL = "delete from soque_tracker where sqt_id = " + e.CommandArgument.ToString();
            int rowCount = Convert.ToInt32(SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.Text, strSQL));
            if (rowCount <= 0)
            {
                ltlAlert.Text = "alert('删除失败，请重试！')";
                return;
            }
            BindData();
        }
    }
}
