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

public partial class plan_bigOrder : BasePage 
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
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

        DataTable dt = GetBoCheck(txtTcpOrder1.Text.Trim(), txtSaleOrder1.Text.Trim(), txtSaleOrder2.Text.Trim(), txtWorkOrder1.Text.Trim(), txtWorkOrder2.Text.Trim(), txtWoPlanDate1.Text.Trim(), txtWoPlanDate2.Text.Trim(), txtWoPlanDateC1.Text.Trim(), txtWoPlanDateC2.Text.Trim(), ddlType.SelectedItem.Text.Trim(), chkUnAccount.Checked, chkUnPlan.Checked, Convert.ToInt32(ddlStatus.SelectedItem.Value), txtCreatedName.Text.Trim(), txtSite.Text.Trim(), chkDiff.Checked, Convert.ToInt32(dplCheckContent.SelectedValue));
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
        }
    }

    private DataTable GetBoCheck(string nbr, string soNbr1, string soNbr2, string woNbr1, string woNbr2, string plandate1, string plandate2, string plandateC1, string plandateC2, string type, bool unaccount, bool unplan, int unApprove, string createdName, string site, bool isDiff, int errType)
    {
        SqlParameter[] param = new SqlParameter[17];
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
        param[16] = new SqlParameter("@errType", errType);

        return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_bo_checkBo", param).Tables[0];
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

        Response.Redirect("bigOrderCheckExcel.aspx?nbr=" + txtTcpOrder1.Text.Trim() + "&pd1=" + txtWoPlanDate1.Text.Trim() + "&pd2=" + txtWoPlanDate2.Text.Trim()
            + "&so1=" + txtSaleOrder1.Text.Trim() + "&so2=" + txtSaleOrder2.Text.Trim() + "&wo1=" + txtWorkOrder1.Text.Trim() + "&wo2=" + txtWorkOrder2.Text.Trim()
            + "&pdc1=" + txtWoPlanDateC1.Text.Trim()  + "&pdc2=" + txtWoPlanDateC2.Text.Trim() + "&ty=" + ddlType.SelectedItem.Text.Trim()
            + "&ua=" + chkUnAccount.Checked + "&up=" + chkUnPlan.Checked + "&uap=" + ddlStatus.SelectedItem.Value.Trim() + "&bc=" + txtCreatedName.Text.Trim() + "&st=" + txtSite.Text.Trim() + "&di=" + chkDiff.Checked + "&et=" + dplCheckContent.SelectedValue.ToString(), true);
    }
}
