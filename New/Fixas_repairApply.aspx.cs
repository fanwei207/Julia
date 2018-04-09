using System;
using System.Data;
using System.Web.UI.WebControls;
using adamFuncs;
using Portal.Fixas;
using TCPNEW;

public partial class new_Fixas_repairApply : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txbRepairDate1.Text = DateTime.Today.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            txbRepairDate2.Text = DateTime.Today.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

            dropRepairStatus.Items.Add(new ListItem("已报修", "repair"));
            dropRepairStatus.Items.Add(new ListItem("维修中", "in-repair"));
            dropRepairStatus.Items.Add(new ListItem("已完成", "completed"));
            dropRepairStatus.Items.Insert(0, new ListItem("--", ""));

            BindCostCenter();
            BindTypes();
            BindSubTypes();
            BindData();
        }
    }

    protected void BindData()
    {
        gvRepairApply.DataSource = FixasRepairHelper.SelectRepairOrder(txbFixasNo.Text.Trim(), txbRepairOrder.Text.Trim(), txbRepairDate1.Text.Trim(), txbRepairDate2.Text.Trim(), string.Empty, string.Empty, dropRepairStatus.SelectedValue,
            Convert.ToInt32(dropTypes.SelectedValue), Convert.ToInt32(dropSubTypes.SelectedValue), Convert.ToInt32(Session["plantCode"]), dropCC.SelectedValue);
        gvRepairApply.DataBind();
        lblTotal.Text = FixasRepairHelper.Count.ToString();
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

    protected void gvRepairApply_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRepairApply.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvRepairApply_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            switch (e.Row.Cells[3].Text)
            {
                case "repair":
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
                    e.Row.Cells[3].Text = "已报修";
                    break;
                case "in-repair":
                    e.Row.Cells[3].BackColor = System.Drawing.Color.LightYellow;
                    e.Row.Cells[3].Text = "维修中";
                    e.Row.Cells[0].Enabled = false;
                    e.Row.Cells[1].Enabled = false;
                    e.Row.Cells[4].Enabled = false;
                    break;
                case "completed":
                    e.Row.Cells[3].Text = "已完成";
                    e.Row.Cells[0].Enabled = false;
                    e.Row.Cells[1].Enabled = false;
                    e.Row.Cells[4].Enabled = false;
                    break;
                default:
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
                    e.Row.Cells[3].Text = "--";
                    break;
            }
        }
    }

    protected void gvRepairApply_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myExport")
        {
            ltlAlert.Text = "window.open('/new/Fixas_maintainRepairExport.aspx?ty=repairApply&repairOrder=" + e.CommandArgument.ToString() + "&rt=" + DateTime.Now + "','_blank')";
        }
        else if (e.CommandName == "myRepairOrder")
        {
            Response.Redirect("/new/Fixas_repairApplyEdit.aspx?repairOrder=" + e.CommandArgument.ToString() + "&rt=" + DateTime.Now.ToString());
        }
        else if (e.CommandName == "myDelete")
        {
            FixasRepair fixasRepair = new FixasRepair();
            fixasRepair.RepairOrder = e.CommandArgument.ToString();
            if (fixasRepair.DeleteApply)
            {
                BindData();
            }
            else
            {
                ltlAlert.Text = "alert('删除失败！')";
            }
        }
        else if (e.CommandName == "myAssign")
        {
            Response.Redirect("/new/Fixas_repairApplyAssign.aspx?repairOrder=" + e.CommandArgument.ToString() + "&rt=" + DateTime.Now.ToString());
        }
    }

    protected void dropTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubTypes();
        BindData();
    }
}