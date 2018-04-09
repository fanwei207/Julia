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
using QCProgress;
using CommClass;
using System.IO;


public partial class wl_calendar : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txb_year.Text = DateTime.Now.Year.ToString();
            txb_month.Text = DateTime.Now.Month.ToString();
            dropDepartmentBind();
            dropWorkshopBind();
            bindData();
        }
    }

    private void bindData()
    {
        DataTable dt = GetWorkOrderSubsidy(Convert.ToInt32(ddl_dp.SelectedItem.Value), txb_userno.Text.Trim(), Convert.ToInt32(txb_year.Text.Trim()), Convert.ToInt32(txb_month.Text.Trim()), Convert.ToInt32(ddl_ws.SelectedItem.Value), txb_workcenter.Text.Trim(), chkIsValue.Checked);
        gvWF.DataSource = dt;
        gvWF.DataBind();
    }

    private void dropDepartmentBind()
    {
        DataTable dt = GetDepartment();
        this.ddl_dp.DataSource = dt;
        this.ddl_dp.DataBind();
        this.ddl_dp.Items.Insert(0, new ListItem("--", "-1"));
    }

    private void dropWorkshopBind()
    {
        DataTable dt = GetWorkshop(Convert.ToInt32(ddl_dp.SelectedItem.Value));
        ddl_ws.DataSource = dt;
        ddl_ws.DataBind();
        ddl_ws.Items.Insert(0, new ListItem("--", "-1"));
    }

    private DataTable GetDepartment()
    {
        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_selectManufactureDepartment").Tables[0];
    }

    private DataTable GetWorkshop(int dept)
    {
        SqlParameter param = new SqlParameter("@dept", dept);
        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_selectWorkShopBydept", param).Tables[0];
    }

    private DataTable GetWorkOrderSubsidy(int departmentID, string loginName, int uYear, int uMonth, int workshop, string workcenter, bool isValue)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@dept", departmentID);
        param[1] = new SqlParameter("@userNo", loginName);
        param[2] = new SqlParameter("@year", uYear);
        param[3] = new SqlParameter("@month", uMonth);
        param[4] = new SqlParameter("@workshop", workshop);
        param[5] = new SqlParameter("@workcenter", workcenter);
        param[6] = new SqlParameter("@isValue", isValue);

        return SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.StoredProcedure, "sp_selectWorkOrderSubsidy", param).Tables[0];
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        if (txb_year.Text == string.Empty || txb_month.Text == string.Empty)
        {
            ltlAlert.Text = "alert('年、月不能为空!')";
            return;
        }
        else
        {
            int _month = Convert.ToInt32(txb_month.Text.Trim());
            if (_month > 12 || _month < 1)
            {
                ltlAlert.Text = "alert('月份必须是1-12之间的数字！');";
                return;
            }
        }

        if (txb_workcenter.Text != string.Empty)
        {
            try
            {
                int intFormat = Convert.ToInt32(txb_workcenter.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('成本中心格式非法！')";
                return;
            }
        }
        bindData();
    }

    protected void gvWF_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWF.PageIndex = e.NewPageIndex;
        bindData();
    }

    protected void btn_export_Click(object sender, EventArgs e)
    {
        if (txb_year.Text == string.Empty || txb_month.Text == string.Empty)
        {
            ltlAlert.Text = "alert('年、月不能为空!')";
            return;
        }
        else
        {
            int _month = Convert.ToInt32(txb_month.Text.Trim());
            if (_month > 12 || _month < 1)
            {
                ltlAlert.Text = "alert('月份必须是1-12之间的数字！');";
                return;
            }
        }

        if (txb_workcenter.Text != string.Empty)
        {
            try
            {
                int intFormat = Convert.ToInt32(txb_workcenter.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('成本中心格式非法！')";
                return;
            }
        }
        ltlAlert.Text = "window.open('wo2_WorkOrderSubsidyExcel.aspx?dp=" + ddl_dp.SelectedItem.Value.ToString() + "&un=" + txb_userno.Text.Trim() +
            "&ye=" + txb_year.Text.Trim() + "&mo=" + txb_month.Text.Trim() + "&ws=" + ddl_ws.SelectedItem.Value.ToString() + "&wc=" + txb_workcenter.Text.Trim() + "&iv=" + chkIsValue.Checked + "','_blank')";
    }

    protected void ddl_dp_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropWorkshopBind();
        bindData();
    }

    protected void ddl_ws_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindData();
    }

    protected void chkIsValue_CheckedChanged(object sender, EventArgs e)
    {
        bindData();
    }
}
