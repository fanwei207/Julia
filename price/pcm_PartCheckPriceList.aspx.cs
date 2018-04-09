using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class price_pcm_PartCheckPriceList : BasePage
{
    private PCM_FinCheckApply helper = new PCM_FinCheckApply();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        string part = txtPart.Text.Trim();
        string code = txtItemCode.Text.Trim();
        string vender = txtVender.Text.Trim();
        string inquiry = txtInquiry.Text.Trim();
        string venderName = txtVenderName.Text.Trim();

        gv.DataSource = helper.GetPartCheckPriceFinishedList(part, code, vender, inquiry, venderName);
        gv.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }
    protected void chkStatus_CheckedChanged(object sender, EventArgs e)
    {
        BindData();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            if (Convert.ToBoolean(gv.DataKeys[e.Row.RowIndex].Values["isout"]))
            {
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Orange;
            }
        }
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "look")
        //{
        //    int rowIndex = int.Parse(e.CommandArgument.ToString());
        //    string part = gv.DataKeys[rowIndex].Values["part"].ToString();
        //    string PQID = gv.DataKeys[rowIndex].Values["PQID"].ToString();
        //    ltlAlert.Text = "var w=window.open('pcm_FinCheckApplyPart.aspx?part=" + part + "&PQID="+PQID+"')"; 
        //}
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        //int count = 0;

        DataTable table = new DataTable("DetID");
        DataColumn column;
        DataRow rowin;

        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "DetID";
        table.Columns.Add(column);


        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox chk = row.FindControl("chk") as CheckBox;
            string DetID = gv.DataKeys[row.RowIndex].Values["DetId"].ToString();
            if (chk.Checked)
            {
                rowin = table.NewRow();
                rowin["DetID"] = DetID;
                table.Rows.Add(rowin);
                count++;
            }
        }
        if (count == 0)
        {
            ltlAlert.Text = "alert('请选择要提交的申请！')";
        }
        else
        {
            if (helper.AddApplyToCheck(table, Session["uID"].ToString()))
            {
                ltlAlert.Text = "alert('提交成功！')";
                BindData();
            }
            else
            {
                ltlAlert.Text = "alert('提交失败！')";

            }
        }
    }
}