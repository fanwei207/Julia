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
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using adamFuncs;
using System.Drawing;

public partial class plan_bigOrder : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();

            if (CheckIsApprove(Convert.ToInt32(Session["uID"].ToString())))
            {
                gvlist.Columns[25].Visible = true;
                gvlist.Columns[26].Visible = true;
                btnApprove.Visible = true;
            }
            else
            {
                gvlist.Columns[25].Visible = false;
                gvlist.Columns[26].Visible = false;
                btnApprove.Visible = false;
            }
        }  
    }

    private void BindData()
    {
        if (txtWoPlanDate1.Text.Length != 0)
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtWoPlanDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('计划日期格式为MM/dd/yyyy！')";
                return;
            }
        }
        
        if(txtWoPlanDate2.Text.Length != 0)
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtWoPlanDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('计划日期格式为MM/dd/yyyy！')";
                return;
            }
        }

        if (txtWoPlanDateC1.Text.Length != 0)
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtWoPlanDateC1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('变更日期格式为MM/dd/yyyy！')";
                return;
            }
        }

        if (txtWoPlanDateC2.Text.Length != 0)
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtWoPlanDateC2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('变更日期格式为MM/dd/yyyy！')";
                return;
            }
        }

        if (txtPageSize.Text.Length == 0)
        {
            txtPageSize.Text = "18";
        }
        else
        {
            try
            {
                int intFormat = Convert.ToInt32(txtPageSize.Text.Trim());

                if (intFormat <= 0)
                {
                    ltlAlert.Text = "alert('每页记录只能为大于0的整数！')";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('每页记录只能为大于0的整数！')";
                return;
            }
        }

        gvlist.PageSize = Convert.ToInt32(txtPageSize.Text.Trim());

        DataTable dt = GetBO(txtTcpOrder1.Text.Trim(), txtSaleOrder1.Text.Trim(), txtSaleOrder2.Text.Trim(), txtWorkOrder1.Text.Trim(), txtWorkOrder2.Text.Trim(), txtWoPlanDate1.Text.Trim(), txtWoPlanDate2.Text.Trim(), txtWoPlanDateC1.Text.Trim(), txtWoPlanDateC2.Text.Trim(), ddlType.SelectedItem.Text.Trim(), chkUnAccount.Checked, chkUnPlan.Checked, Convert.ToInt32(ddlStatus.SelectedItem.Value), txtCreatedName.Text.Trim(), txtSite.Text.Trim(), chkDiff.Checked);
        gvlist.DataSource = dt;
        gvlist.DataBind();

        lblSum.Text = "计:" + dt.Rows.Count.ToString() + "条记录";
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (gvlist.DataKeys[e.Row.RowIndex].Values["bo_status"].ToString() == "1")
            {
                e.Row.Cells[0].BackColor = System.Drawing.Color.Blue;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
            }
            else if (gvlist.DataKeys[e.Row.RowIndex].Values["bo_status"].ToString() == "2")
            {
                e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
            }
            else if (gvlist.DataKeys[e.Row.RowIndex].Values["bo_status"].ToString() == "3")
            {
                e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
            }
            else
            {
                e.Row.Cells[25].Text = string.Empty;
                e.Row.Cells[26].Text = string.Empty;   
            }
        }
    }

    private DataTable GetBO(string nbr, string soNbr1, string soNbr2, string woNbr1, string woNbr2, string plandate1, string plandate2, string plandateC1, string plandateC2, string type, bool unaccount, bool unplan, int unApprove, string createdName, string site, bool isDiff)
    {
        SqlParameter[] param = new SqlParameter[16];
        param[0] = new SqlParameter("@ord_nbr", nbr);
        param[1] = new SqlParameter("@so_nbr1", soNbr1);
        param[2] = new SqlParameter("@so_nbr2", soNbr2);
        param[3] = new SqlParameter("@wo_nbr1", woNbr1);
        param[4] = new SqlParameter("@wo_nbr2", woNbr2);
        param[5] = new SqlParameter("@ord_plandate1", plandate1);
        param[6] = new SqlParameter("@ord_plandate2", plandate2);
        param[7] = new SqlParameter("@ord_plandateC1", plandateC1);
        param[8] = new SqlParameter("@ord_plandateC2", plandateC2);
        param[9] = new SqlParameter("@wo_type", type);
        param[10] = new SqlParameter("@isUnAccount", unaccount);
        param[11] = new SqlParameter("@isUnPlan", unplan);
        param[12] = new SqlParameter("@isUnApprove", unApprove);
        param[13] = new SqlParameter("@bo_createdName", createdName);
        param[14] = new SqlParameter("@wo_site", site);
        param[15] = new SqlParameter("@isDiff", isDiff);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_selectBO2", param).Tables[0];
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (txtWoPlanDate1.Text.Length != 0)
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtWoPlanDate1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('计划日期格式为MM/dd/yyyy！')";
                return;
            }
        }

        if (txtWoPlanDate2.Text.Length != 0)
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtWoPlanDate2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('计划日期格式为MM/dd/yyyy！')";
                return;
            }
        }

        if (txtWoPlanDateC1.Text.Length != 0)
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtWoPlanDateC1.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('变更日期格式为MM/dd/yyyy！')";
                return;
            }
        }

        if (txtWoPlanDateC2.Text.Length != 0)
        {
            try
            {
                DateTime dateFormat = Convert.ToDateTime(txtWoPlanDateC2.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('变更日期格式为MM/dd/yyyy！')";
                return;
            }
        }

        Response.Redirect("bigOrderExcel1.aspx?nbr=" + txtTcpOrder1.Text.Trim() + "&pd1=" + txtWoPlanDate1.Text.Trim() + "&pd2=" + txtWoPlanDate2.Text.Trim()
            + "&so1=" + txtSaleOrder1.Text.Trim() + "&so2=" + txtSaleOrder2.Text.Trim() + "&wo1=" + txtWorkOrder1.Text.Trim() + "&wo2=" + txtWorkOrder2.Text.Trim()
            + "&pdc1=" + txtWoPlanDateC1.Text.Trim()  + "&pdc2=" + txtWoPlanDateC2.Text.Trim() + "&ty=" + ddlType.SelectedItem.Text.Trim() 
            + "&ua=" + chkUnAccount.Checked + "&up=" + chkUnPlan.Checked + "&uap=" + ddlStatus.SelectedItem.Value.Trim() + "&bc=" + txtCreatedName.Text.Trim() + "&st=" + txtSite.Text.Trim() + "&di=" + chkDiff.Checked, true);
    }

    private bool CheckIsApprove(int userID)
    {
        SqlParameter param = new SqlParameter("@userID", userID);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_checkIsApprover", param));
    }

    private bool ApproveBo(int bo_id, int bo_status, int uID, string uName)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@bo_id", bo_id);
        param[1] = new SqlParameter("@bo_status", bo_status);
        param[2] = new SqlParameter("@createdBy", uID);
        param[3] = new SqlParameter("@createdName", uName);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_approveBo", param));
    }

    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "approve")
        {
            string[] param = e.CommandArgument.ToString().Split(',');
            int bo_id = Convert.ToInt32(param[0]);
            int bo_status = Convert.ToInt32(param[1]);
            int uID = Convert.ToInt32(Session["uID"].ToString());
            string uName = Session["uName"].ToString();

            if (ApproveBo(bo_id, bo_status, uID, uName))
            {
                ltlAlert.Text = "alert('审批完成!');";
            }
            else
            {
                ltlAlert.Text = "alert('审批错误,请联系管理员!');";
            }
            BindData();
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        int chkNum = 0;
        foreach (GridViewRow row in gvlist.Rows)
        {
            CheckBox chkSinger = (CheckBox)row.FindControl("chkSinger");
            LinkButton linkApprove = (LinkButton)row.FindControl("linkApprove");

            if (chkSinger.Checked && gvlist.DataKeys[row.RowIndex].Values["bo_status"].ToString() != "0")
            {
                chkNum = chkNum + 1;
            }
        }

        if (chkNum == 0)
        {
            ltlAlert.Text = "alert('请选择需要参与审批的记录!');";
            BindData();
            return;
        }
        else
        {
            if(DeleteApproveTemp(Convert.ToInt32(Session["uID"].ToString())))
            {
                #region 创建存放数据源的表approveTemp
                DataTable approveTemp = new DataTable("approveTemp");
                DataColumn column;
                DataRow row;

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "bo_id";
                approveTemp.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "bo_status";
                approveTemp.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "bo_approvedBy";
                approveTemp.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "bo_approvedName";
                approveTemp.Columns.Add(column);

                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "bo_approvedDate";
                approveTemp.Columns.Add(column);
                #endregion

                foreach (GridViewRow row1 in gvlist.Rows)
                {
                    CheckBox chkSinger1 = (CheckBox)row1.FindControl("chkSinger");

                    if (chkSinger1.Checked && gvlist.DataKeys[row1.RowIndex].Values["bo_status"].ToString() != "0")
                    {
                        DataRow approveTempRow = approveTemp.NewRow();
                        approveTempRow["bo_id"] = Convert.ToInt32(gvlist.DataKeys[row1.RowIndex].Values["bo_id"].ToString());
                        approveTempRow["bo_status"] = Convert.ToInt32(gvlist.DataKeys[row1.RowIndex].Values["bo_status"].ToString());
                        approveTempRow["bo_approvedBy"] = Convert.ToInt32(Session["uID"].ToString());
                        approveTempRow["bo_approvedName"] = Session["uName"].ToString();
                        approveTempRow["bo_approvedDate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        approveTemp.Rows.Add(approveTempRow);
                    }
                }

                if (approveTemp != null && approveTemp.Rows.Count > 0)
                {
                    using (SqlBulkCopy bulckCopy = new SqlBulkCopy(chk.dsn0(), SqlBulkCopyOptions.UseInternalTransaction))
                    {
                        bulckCopy.DestinationTableName = "bigOrder_ApproveTemp";
                        bulckCopy.ColumnMappings.Add("bo_id", "bo_id");
                        bulckCopy.ColumnMappings.Add("bo_status", "bo_status");
                        bulckCopy.ColumnMappings.Add("bo_approvedBy", "bo_approvedBy");
                        bulckCopy.ColumnMappings.Add("bo_approvedName", "bo_approvedName");
                        bulckCopy.ColumnMappings.Add("bo_approvedDate", "bo_approvedDate");

                        try
                        {
                            bulckCopy.WriteToServer(approveTemp);
                        }
                        catch
                        {
                            ltlAlert.Text = "alert('审批失败，请联系管理员B!');";
                            BindData();
                            return;
                        }
                        finally
                        {
                            approveTemp.Dispose();
                        }
                    }
                }

                if (ApproveTempBatch(Convert.ToInt32(Session["uID"].ToString())))
                {
                    BindData();
                }
                else
                {
                    ltlAlert.Text = "alert('审批失败，请联系管理员C!');";
                    BindData();
                    return;
                }
            }
            else
            {
                ltlAlert.Text = "alert('审批失败，请联系管理员A!');";
                BindData();
                return;
            }
        }
    }

    private bool DeleteApproveTemp(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_deleteApproveTemp", param));
        }
        catch
        {
            return false;
        }
    }

    private bool ApproveTempBatch(int createdBy)
    {
        try
        {
            SqlParameter param = new SqlParameter("@createdBy", createdBy);

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_approveBoBatch", param));
        }
        catch
        {
            return false;
        }
    }
}
