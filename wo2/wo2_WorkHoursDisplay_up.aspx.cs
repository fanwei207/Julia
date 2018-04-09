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
using WOrder;
using adamFuncs;
using Wage;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;

public partial class wo2_WorkHoursDisplay_up : BasePage
{
    adamClass adam = new adamClass();
    WorkOrder wd = new WorkOrder();
    HR hr_salary = new HR();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtStart.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-DateTime.Now.Day + 1));
            txtEnd.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            dropDeptBind();
            GridViewBind();
        }
    }

    private void dropDeptBind()
    {
        ListItem item;
        item = new ListItem("--", "0");
        dropDept.Items.Add(item);

        DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
        if (dtDropDept.Rows.Count > 0)
        {
            for (int i = 0; i < dtDropDept.Rows.Count; i++)
            {
                item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                dropDept.Items.Add(item);
            }
        }

        dropDept.SelectedIndex = 0;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gv.PageIndex = 0;
        GridViewBind();
    }

    private void GridViewBind()
    {
        try
        {
            string strSql = "sp_hr_SalaryWorkHoursUp";
            SqlParameter[] sqlParam = new SqlParameter[11];
            sqlParam[0] = new SqlParameter("@starttime", txtStart.Text);
            sqlParam[1] = new SqlParameter("@endtime", txtEnd.Text);
            sqlParam[2] = new SqlParameter("@PlantCode", Session["PlantCode"]);
            sqlParam[3] = new SqlParameter("@Center", txtCenter.Text);
            sqlParam[4] = new SqlParameter("@Site", txtSite.Text);
            sqlParam[5] = new SqlParameter("@WorkOrder", txtWorkOrder.Text);
            sqlParam[6] = new SqlParameter("@OrderID", txtID.Text);
            sqlParam[7] = new SqlParameter("@UserNo", txtUserNo.Text);
            sqlParam[8] = new SqlParameter("@UserName", txtUserName.Text);
            sqlParam[9] = new SqlParameter("@isClosed", chkClose.Checked);
            sqlParam[10] = new SqlParameter("@Department", dropDept.SelectedValue);

            DataSet ds = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.StoredProcedure, strSql, sqlParam);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ltlAlert.Text = "alert('获取数据时发生错误！请刷新！');";
            return;
        }
    }

    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        bool blWo2, blWo;
        blWo2 = false;
        blWo = false;

        if (e.CommandName == "1")
            blWo2 = true;
        if (e.CommandName == "2")
            blWo2 = true;
        if (e.CommandName == "3")
            blWo2 = true;

        if (e.CommandName == "4")
            blWo = true;
        if (e.CommandName == "5")
            blWo = true;
        if (e.CommandName == "6")
            blWo = true;

        if (blWo2)
        {
            Session["EXSQL1"] = wd.Wo2Export(gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString(), txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, chkClose.Checked, Convert.ToInt32(e.CommandName), Convert.ToInt32(Session["uRole"]), Convert.ToString(Session["Uid"]), 0);
            Session["EXTitle1"] = wd.Wo2Export(gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString(), txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, chkClose.Checked, Convert.ToInt32(e.CommandName), Convert.ToInt32(Session["uRole"]), Convert.ToString(Session["Uid"]), 1);
            Session["EXHeader1"] = "";
            ltlAlert.Text = "window.open('/public/exportExcel1.aspx','_blank')";
        }
        else
        {
            if (blWo)
            {

                Session["EXSQL1"] = wd.WoExport(gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString(), txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, chkClose.Checked, Convert.ToInt32(e.CommandName), Convert.ToInt32(Session["uRole"]), Convert.ToString(Session["Uid"]), 0);
                Session["EXTitle1"] = wd.WoExport(gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString(), txtStart.Text, txtEnd.Text, Convert.ToInt32(Session["Plantcode"]), txtCenter.Text, txtSite.Text, txtWorkOrder.Text, txtID.Text, chkClose.Checked, Convert.ToInt32(e.CommandName), Convert.ToInt32(Session["uRole"]), Convert.ToString(Session["Uid"]), 1);
                Session["EXHeader1"] = "";
                ltlAlert.Text = "window.open('/public/exportExcel1.aspx','_blank')";
            }
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        GridViewBind();
    }
    protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string strYear = DateTime.Now.Year.ToString();

            TableCellCollection header = e.Row.Cells;
            header.Clear();
            #region
            header.Add(new TableCell());
            header[0].RowSpan = 2;
            header[0].Style.Add("text-align", "center");
            header[0].Text = "工号";

            header.Add(new TableCell());
            header[1].RowSpan = 2;
            header[1].Style.Add("text-align", "center");
            header[1].Text = "姓名";

            header.Add(new TableCell());
            header[2].RowSpan = 2;
            header[2].Style.Add("text-align", "center");
            header[2].Text = "部门";

            header.Add(new TableCell());
            header[3].RowSpan = 2;
            header[3].Style.Add("text-align", "center");
            header[3].Text = "工段";

            header.Add(new TableCell());
            header[4].ColumnSpan = 4;
            header[4].Style.Add("border-bottom", "solid 1px sliver");
            header[4].Style.Add("text-align", "center");
            header[4].Text = "工单工时";

            header.Add(new TableCell());
            header[5].ColumnSpan = 4;
            header[5].Style.Add("border-bottom", "solid 1px sliver");
            header[5].Style.Add("text-align", "center");
            header[5].Text = "单价法";

            header.Add(new TableCell());
            header[6].RowSpan = 2;
            header[6].Style.Add("border-bottom", "solid 1px sliver");
            header[6].Style.Add("text-align", "center");
            header[6].Text = "调整额";

            header.Add(new TableCell());
            header[7].RowSpan = 2;
            header[7].Style.Add("background-color", "#006699");
            header[7].Style.Add("color", "#ffffff");
            header[7].Style.Add("text-align", "center");
            header[7].Text = "工资合计</TD></TR><TR><TD style='background-color:#006699;color:ffffff;text-align:center;'>WO2";

            header.Add(new TableCell());
            header[8].RowSpan = 1;
            header[8].Style.Add("background-color", "#006699");
            header[8].Style.Add("color", "#ffffff");
            header[8].Style.Add("text-align", "center");
            header[8].Text = "WO";

            header.Add(new TableCell());
            header[9].RowSpan = 1;
            header[9].Style.Add("background-color", "#006699");
            header[9].Style.Add("color", "#ffffff");
            header[9].Style.Add("text-align", "center");
            header[9].Text = "计划外";

            header.Add(new TableCell());
            header[10].RowSpan = 1;
            header[10].Style.Add("background-color", "#006699");
            header[10].Style.Add("color", "#ffffff");
            header[10].Style.Add("text-align", "center");
            header[10].Text = "合计";

            header.Add(new TableCell());
            header[11].RowSpan = 1;
            header[11].Style.Add("background-color", "#006699");
            header[11].Style.Add("color", "#ffffff");
            header[11].Style.Add("text-align", "center");
            header[11].Text = "WO2";

            header.Add(new TableCell());
            header[12].RowSpan = 1;
            header[12].Style.Add("background-color", "#006699");
            header[12].Style.Add("color", "#ffffff");
            header[12].Style.Add("text-align", "center");
            header[12].Text = "WO";

            header.Add(new TableCell());
            header[13].RowSpan = 1;
            header[13].Style.Add("background-color", "#006699");
            header[13].Style.Add("color", "#ffffff");
            header[13].Style.Add("text-align", "center");
            header[13].Text = "计划外";

            header.Add(new TableCell());
            header[14].RowSpan = 1;
            header[14].Style.Add("background-color", "#006699");
            header[14].Style.Add("color", "#ffffff");
            header[14].Style.Add("text-align", "center");
            header[14].Text = "合计";
            #endregion
        }
    }
}
