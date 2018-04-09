using System;
using System.Data;
using System.Web.UI.WebControls;
using adamFuncs;
using Portal.Fixas;
using TCPNEW;

public partial class new_Fixas_maintainPlan : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropMaintainStatus.Items.Add(new ListItem("已计划", "planned"));
            dropMaintainStatus.Items.Add(new ListItem("已完成", "completed"));
            dropMaintainStatus.Items.Insert(0, new ListItem("--", ""));

            txbMaintainDate1.Text = DateTime.Today.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            txbMaintainDate2.Text = DateTime.Today.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");

            BindCostCenter();
            BindTypes();
            BindSubTypes();
            BindData();
        }
    }

    protected void BindData()
    {
        gvMaintainPlan.DataSource = FixasMaintainHelper.SelectMaintainOrder(txbFixasNo.Text, txbMaintainOrder.Text.Trim(), txbMaintainDate1.Text.Trim(), txbMaintainDate2.Text.Trim(), string.Empty, string.Empty, dropMaintainStatus.SelectedValue,
               Convert.ToInt32(dropTypes.SelectedValue), Convert.ToInt32(dropSubTypes.SelectedValue), Convert.ToInt32(Session["plantCode"]), dropCC.SelectedValue);
        gvMaintainPlan.DataBind();
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

    protected void gvMaintainPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMaintainPlan.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvMaintainPlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "myExport")
        {
            ltlAlert.Text = "window.open('/new/Fixas_maintainRepairExport.aspx?ty=maintainPlan&maintainOrder=" + e.CommandArgument.ToString() + "&rt=" + DateTime.Now.ToString() + "')";
        }
        else if (e.CommandName == "myDelete")
        {
            if (!this.Security["110103520"].isValid)
            {
                ltlAlert.Text = "alert('你没有删除保养计划的权限！')";
            }
            else
            {
                FixasMaintain fixasMaintain = new FixasMaintain();
                fixasMaintain.MaintainOrder = e.CommandArgument.ToString();
                if (fixasMaintain.DeletePlan)
                {
                    BindData();
                }
                else
                {
                    ltlAlert.Text = "alert('删除失败，请重试！')";
                }
            }
        }
        else if (e.CommandName == "myMaintainOrder")
        {
            if (!this.Security["110103520"].isValid)
            {
                ltlAlert.Text = "alert('你没有编辑保养计划的权限！')";
            }
            else
            {
                Response.Redirect("/new/Fixas_maintainPlanEdit.aspx?ty=edit&maintainOrder=" + e.CommandArgument.ToString() + "&rt=" + DateTime.Now.ToString());
            }
        }
    }

    protected void gvMaintainPlan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[2].Text == "planned")
            {
                e.Row.Cells[2].Text = "已计划";
                e.Row.Cells[2].BackColor = System.Drawing.Color.Yellow;
            }
            else if (e.Row.Cells[2].Text == "completed")
            {
                e.Row.Cells[2].Text = "已完成";
                e.Row.Cells[1].Enabled = false;
                e.Row.Cells[3].Enabled = false;
            }
            else
            {
                e.Row.Cells[2].Text = "--";
                e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void dropTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubTypes();
        BindData();
    }
}