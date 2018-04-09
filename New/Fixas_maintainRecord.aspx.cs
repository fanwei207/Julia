using System;
using System.Data;
using System.Web.UI.WebControls;
using adamFuncs;
using Portal.Fixas;
using TCPNEW;

public partial class new_Fixas_maintainRecord : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropMaintainStatus.Items.Add(new ListItem("已计划", "planned"));
            dropMaintainStatus.Items.Add(new ListItem("已完成", "completed"));
            dropMaintainStatus.Items.Insert(0, new ListItem("--", ""));

            txbMaintainedDate1.Text = DateTime.Today.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            txbMaintainedDate2.Text = DateTime.Today.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

            BindCostCenter();
            BindTypes();
            BindSubTypes();
            BindData();
        }
    }

    protected void BindData()
    {
        gvMaintainRecord.DataSource = FixasMaintainHelper.SelectMaintainOrder(txbFixasNo.Text.Trim(), txbMaintainOrder.Text, txbMaintainedDate1.Text.Trim(), txbMaintainedDate2.Text.Trim(), string.Empty, string.Empty, dropMaintainStatus.SelectedValue,
            Convert.ToInt32(dropTypes.SelectedValue), Convert.ToInt32(dropSubTypes.SelectedValue), Convert.ToInt32(Session["plantCode"]), dropCC.SelectedValue);
        gvMaintainRecord.DataBind();
        lblTotal.Text = FixasMaintainHelper.Count.ToString();
    }

    protected void BindCostCenter()
    {
        DataTable cc = GetDataTcp.GetCostCenterFixAsset(Convert.ToInt32(Session["plantCode"]));
        dropCC.Items.Clear();
        dropCC.Items.Add(new ListItem(" -- ", ""));
        foreach (DataRow row in cc.Rows)
        {
            dropCC.Items.Add(new ListItem(row["fixctc_name"].ToString(), row["fixctc_no"].ToString()));
        }
        dropCC.SelectedIndex = 0;
    }

    protected void BindTypes()
    {
        dropTypes.Items.Clear();
        dropTypes.Items.Add(new ListItem(" -- ", "0"));
        DataTable dt = FixasTypeHelper.SelectFixasTypeList();
        foreach (DataRow row in dt.Rows)
        {
            dropTypes.Items.Add(new ListItem(row["fixasTypeName"].ToString(), row["fixasTypeID"].ToString()));
        }
    }

    protected void BindSubTypes()
    {
        dropSubTypes.Items.Clear();
        dropSubTypes.Items.Add(new ListItem(" -- ", "0"));
        DataTable dt = FixasTypeHelper.SelectFixasSubTypeList(dropTypes.SelectedValue);
        foreach (DataRow row in dt.Rows)
        {
            dropSubTypes.Items.Add(new ListItem(row["fixasTypeName"].ToString(), row["fixasTypeID"].ToString()));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvMaintainRecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMaintainRecord.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvMaintainRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myExport")
        {
            ltlAlert.Text = "window.open('/new/Fixas_maintainRepairExport.aspx?ty=maintainRecord&maintainOrder=" + e.CommandArgument.ToString() + "&rt=" + DateTime.Now.ToString() + "')";
        }
        else if (e.CommandName == "myMaintainOrder")
        {
            Response.Redirect("/new/Fixas_maintainRecordEdit.aspx?MaintainOrder=" + e.CommandArgument.ToString() + "&rt=" + DateTime.Now.ToString());
        }
    }

    protected void gvMaintainRecord_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text == "planned")
            {
                e.Row.Cells[1].Text = "已计划";
                e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
            }
            else if (e.Row.Cells[1].Text == "completed")
            {
                e.Row.Cells[1].Text = "已完成";
                e.Row.Cells[2].Enabled = false;
            }
            else
            {
                e.Row.Cells[1].Text = "--";
                e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void dropTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubTypes();
        BindData();
    }
}