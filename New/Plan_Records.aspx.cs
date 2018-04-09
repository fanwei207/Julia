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
using System.IO;
using adamFuncs;
using Portal.Fixas;
using TCPNEW;

public partial class Plan_Records : System.Web.UI.Page
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            dropEntity.DataSource = GetDataTcp.GetEntityFixAsset();
            dropEntity.DataBind();
            dropEntity.Items.Insert(0, new ListItem("--", "--"));

            dropType.DataSource = GetDataTcp.GetTypeFixAsset();
            dropType.DataBind();
            dropType.Items.Insert(0, new ListItem("--", "--"));

            dropRepairItem.DataSource = RepairItemHelper.SelectAllRepairItems();
            dropRepairItem.DataBind();
            dropRepairItem.Items.Insert(0, new ListItem("--", "--"));

            BindData();
        }
    }

    protected void BindData()
    {
        //定义参数
        string fixasNo = txtFixasNo.Text;
        string fixasName = txtFixasName.Text;
        string fixasDesc = txtFixasDesc.Text;
        string fixasType = dropType.SelectedItem.Text;
        string fixasEntity = dropEntity.SelectedItem.Text;
        string fixasVouDate = txtFixasVouDate.Text;
        string fixasSupplier = txtFixasSupplier.Text;
        string maintor = txtMaintor.Text;
        string maintedDate = txtMaintDate.Text;

        gv.DataSource = RecordHelper.SelectRecords(fixasNo, fixasName, fixasDesc, fixasType, fixasEntity, fixasVouDate, fixasSupplier, maintor, maintedDate);
        gv.DataBind();
    }

    /// <summary>
    /// 数据不足一页也显示GridView的页码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRDW_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        GridViewRow gvr = (GridViewRow)gv.BottomPagerRow;
        if (gvr != null)
        {
            gvr.Visible = true;
        }
    }

    protected void gvRDW_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvRDW_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        { 
            e.Row.Cells[8].Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(((Label)e.Row.Cells[8].FindControl("lbMaintedDate")).Text.Trim()));

            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                TextBox txMoney = (TextBox)e.Row.Cells[10].Controls[0];
                txMoney.Text = string.Format("{0:F2}", Convert.ToDouble(txMoney.Text));
                txMoney.Attributes.Add("class", "SmallTextBox4");
                txMoney.Style.Add("width", "100%");
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Plan_AddRecord.aspx?rm=" + DateTime.Now.ToString());
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Record record = new Record();
        record.ID = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["ID"].ToString());

        if (record.Delete)
        {
            ltlAlert.Text = "alert('记录删除成功！');";
        }
        else
        {
            ltlAlert.Text = "alert('记录删除失败，请刷新后重新操作！');";
        }

        BindData();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;

        BindData();
    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txMoney = (TextBox)gv.Rows[e.RowIndex].Cells[10].Controls[0];

        if (txMoney.Text != string.Empty)
        {
            try
            {
                Double _d = Convert.ToDouble(txMoney.Text);
            }
            catch
            {
                ltlAlert.Text = "alert('维护金额日期格式不正确，必须是数字！');";
                return;
            }
        }

        Record record = new Record();
        record.ID = Convert.ToInt32(gv.DataKeys[e.RowIndex].Values["ID"].ToString());

        if (txMoney.Text.Trim() == string.Empty)
        {
            record.Money = 0.0;
        }
        else
        {
            record.Money = Convert.ToDouble(txMoney.Text.Trim());
        }

        record.Modifier = new User();
        record.Modifier.ID = Convert.ToInt32(Session["uID"].ToString());
        record.Modifier.Date = DateTime.Now;

        if (record.Update)
        {
            ltlAlert.Text = "alert('记录更新成功！');";
            gv.EditIndex = -1;
        }
        else
        {
            ltlAlert.Text = "alert('记录更新失败，请刷新后重新操作！');";
        }

        BindData();
    }
    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;

        BindData();
    }
}
