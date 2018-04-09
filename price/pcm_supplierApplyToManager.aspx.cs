using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;

public partial class price_pcm_supplierApplyToManager : BasePage
{
    PCM_price help = new PCM_price();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bind();
        }
    }

    private void bind()
    {
        string qad = txtPart.Text.Trim();
        string vender = txtVender.Text.Trim();
        string venderName = txtVenderName.Text.Trim();

        gvVender.DataSource = help.selectApplyForPriceUp(qad, vender, venderName);
        gvVender.DataBind();
    }
    protected void gvVender_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnSelectQADDOC")
        {
            ltlAlert.Text = "$.window('文档查看', 600, 800,'./price/pcm_selectQadDoc.aspx?QADDet=" + e.CommandArgument.ToString() + "')";
            //Response.Redirect("pcm_selectQadDoc.aspx?PQID=" + lbPQID.Text + "&QADDet=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "lkbtnBasis")
        {
            ltlAlert.Text = "$.window('文档查看', 600, 800,'./price/pcm_selectBasis.aspx?PQDetID=" + e.CommandArgument.ToString() + "')";

        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable TempTable = new DataTable("TempTable");
        DataColumn TempColumn;
        DataRow TempRow;

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "DetId";
        TempTable.Columns.Add(TempColumn);



        foreach (GridViewRow row in gvVender.Rows)
        {
            if (((CheckBox)(row.FindControl("chk"))).Checked)
            {
                int detId = Convert.ToInt32(gvVender.DataKeys[row.RowIndex].Values["DetId"].ToString());

                TempRow = TempTable.NewRow();//创建新的行

                TempRow["DetId"] = detId;

                TempTable.Rows.Add(TempRow);
            }
        }
        if (TempTable.Rows.Count == 0)
        {
            ltlAlert.Text = "alert('您未选中确认的项目');";
            return;
        }
        else
        {
            int flag = help.confirmPrice(TempTable, Session["uID"].ToString(), Session["uName"].ToString());

            if (flag == 1)
            {
                ltlAlert.Text = "alert('确认成功');";
                bind();
            }
            else
            {
                ltlAlert.Text = "alert('确认失败!请联系管理员');";
            }
        }


    }
    protected void btnRejectTow_Click(object sender, EventArgs e)
    {
        int count = 0;
        DataTable TempTable = new DataTable("TempTable");
        DataColumn TempColumn;
        DataRow TempRow;

        TempColumn = new DataColumn();
        TempColumn.DataType = System.Type.GetType("System.String");
        TempColumn.ColumnName = "DetId";
        TempTable.Columns.Add(TempColumn);



        foreach (GridViewRow row in gvVender.Rows)
        {

            if (((CheckBox)(row.FindControl("chk"))).Checked)
            {
                int detId = Convert.ToInt32(gvVender.DataKeys[row.RowIndex].Values["DetId"].ToString());

                TempRow = TempTable.NewRow();//创建新的行

                TempRow["DetId"] = detId;

                TempTable.Rows.Add(TempRow);
            }
        }
        if (TempTable.Rows.Count == 0)
        {
            ltlAlert.Text = "alert('您未选中确认的项目');";
            return;
        }
        else if (!string.Empty.Equals(txtCloseReason.Text.Trim()))
        {
            int flag = help.updateRejectFromSupplier(TempTable, Session["uID"].ToString(), Session["uName"].ToString(), txtCloseReason.Text.Trim());

            if (flag == 1)
            {
                ltlAlert.Text = "alert('驳回成功');";

                bind();


            }
            else
            {
                ltlAlert.Text = "alert('驳回失败');";
            }
        }
        else
        {
            ltlAlert.Text = "alert('驳回原因不能为空');";

        }

    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }
}