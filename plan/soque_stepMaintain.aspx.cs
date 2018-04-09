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

public partial class plan_soque_stepMaintain : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    { 
        if (!IsPostBack)
        {
            BindData();
            BindPlants();
            BindRegion();
        }
    }

    protected void BindData()
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@stepName", txbStep.Text.Trim());
        param[1] = new SqlParameter("@plantCode", dropPlants.SelectedValue);
        param[2] = new SqlParameter("@userNo", txbUserNo.Text.Trim());
        DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_plan_selectSoqueStep", param);
        gvSteps.DataSource = ds;
        gvSteps.DataBind();
        ds.Dispose();

        labStepID.Text = string.Empty;
    }
    /// <summary>
    /// °ó¶¨ÇøÓò
    /// </summary>
    protected void BindRegion()
    {
        DataSet ds = SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "Qad_Data.dbo.sp_cm_selectCmRegion");

        dropRegion.DataSource = ds;
        dropRegion.DataBind();

        ds.Dispose();

        dropRegion.Items.Insert(0, new ListItem("--", "--"));
    }

    protected void BindPlants()
    {
        dropPlants.Items.Clear();
        dropPlants.Items.Add(new ListItem("--Select A Company--", "0"));
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (Session["uID"] == null)
        {
            ltlAlert.Text = "alert('Reload system£¡')";
            return;
        }
        if (txbStep.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('Status is required£¡')";
            return;
        }
        if (dropPlants.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('Company is required£¡')";
            return;
        }
        if (txbUserNo.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('User No is required£¡')";
            return;
        }

        int success = 1; 
        if (btnUpdate.Text == "Save")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@id", labStepID.Text);
                param[1] = new SqlParameter("@stepName", txbStep.Text.Trim());
                param[2] = new SqlParameter("@userNo", txbUserNo.Text.Trim());
                param[3] = new SqlParameter("@plantCode", dropPlants.SelectedValue);
                param[4] = new SqlParameter("@regionCode", dropRegion.SelectedIndex == 0 ? "" : dropRegion.SelectedValue);
                param[5] = new SqlParameter("@region", dropRegion.SelectedIndex == 0 ? "" : dropRegion.SelectedItem.Text);
                param[6] = new SqlParameter("@uID", Convert.ToInt32(Session["uID"]));
                param[7] = new SqlParameter("@retValue", DbType.Int32);
                param[7].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_plan_updateSoqueStep", param);
                success = Convert.ToInt32(param[7].Value);
            }
            catch
            {
                success = 0;
            }
            if (success == 1)
            {
                ltlAlert.Text = "alert('Success£¡')";
            }
            else if (success == 0)
            {
                ltlAlert.Text = "alert('Failure£¡')";
            }
            else if (success == -1)
            {
                ltlAlert.Text = "alert('User does not exist£¡')";
            }

            btnUpdate.Text = "New";
        }
        else if (btnUpdate.Text == "New")
        {
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@stepName", txbStep.Text.Trim());
                param[1] = new SqlParameter("@userNo", txbUserNo.Text.Trim());
                param[2] = new SqlParameter("@plantCode", dropPlants.SelectedValue);
                param[3] = new SqlParameter("@regionCode", dropRegion.SelectedIndex == 0 ? "" : dropRegion.SelectedValue);
                param[4] = new SqlParameter("@region", dropRegion.SelectedIndex == 0 ? "" : dropRegion.SelectedItem.Text);
                param[5] = new SqlParameter("@uID", Convert.ToInt32(Session["uID"]));
                param[6] = new SqlParameter("@retValue", DbType.Int32);
                param[6].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, "sp_plan_insertSoqueStep", param);
                success = Convert.ToInt32(param[6].Value);
            }
            catch
            {
                success = 0;
            }
            if (success == -1)
            {
                ltlAlert.Text = "alert('User does not exist£¡')";
            }
            else if (success == -2)
            {
                ltlAlert.Text = "alert('Status does exist£¡')";
            }
            else if (success == 1)
            {
                ltlAlert.Text = "alert('Success£¡')";
            }
            else if (success == 0)
            {
                ltlAlert.Text = "alert('Failure£¡')";
            }
        }
        else
        {
            btnUpdate.Text = "New";
        }

        txbStep.Text = string.Empty;
        dropPlants.SelectedValue = "0";
        txbUserNo.Text = string.Empty;
        dropRegion.SelectedIndex = -1;
        BindData();
    }

    protected void gvSteps_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvSteps_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myEdit")
        {
            labStepID.Text = e.CommandArgument.ToString();
            string strSQL = "select soques_step, soques_plant, soques_userNo, soques_region_code from soque_step where soques_id = " + e.CommandArgument.ToString();
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL);
            if (reader.HasRows && reader.Read())
            {
                txbStep.Text = reader["soques_step"].ToString();
                dropPlants.SelectedValue = reader["soques_plant"].ToString();
                txbUserNo.Text = reader["soques_userNo"].ToString();

                dropRegion.SelectedIndex = -1;
                try
                {
                    dropRegion.Items.FindByValue(reader["soques_region_code"].ToString()).Selected = true;
                }
                catch
                { }
            }
            reader.Close();
            btnUpdate.Text = "Save";
        }
    }

    protected void gvSteps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSteps.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
}
