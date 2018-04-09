using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class price_pc_PriceAddDomain : System.Web.UI.Page
{
    PC_price pc = new PC_price();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            
        
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        this.BindData();
    }

    private void BindData()
    {
      
            string QAD = txtQAD.Text.Trim();
            string vender = txtVender.Text.Trim();
            string venderName = txtVenderName.Text.Trim();
            string code = txtCode.Text.Trim();
            bool isAddDomain = chkIsAddDomai.Checked;

            if (!string.Empty.Equals(QAD) || !string.Empty.Equals(vender) || !string.Empty.Equals(venderName) || !string.Empty.Equals(code))
            {
                gvDet.DataSource = pc.selectAddDomain(QAD, vender, venderName, code, isAddDomain);
                gvDet.DataBind();
            }
            else 
            {
                ltlAlert.Text = "alert('请填写一个条件再查询！')";
                return;
            }

            

       
    }
    protected void btnCimload_Click(object sender, EventArgs e)
    {
        DataTable dt = this.GetQADandVender();
        bool isAdd = chkIsAddDomai.Checked;
        string uID = Session["uID"].ToString();
        string tempFile = Server.MapPath("/docs/QadPrice.xls");
        string outputFile = "QadPrice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        try
        {
            pc.ExportExcel(tempFile, Server.MapPath("../Excel/") + outputFile, dt, isAdd, uID);
        }
        catch (Exception ex)
        {
            ltlAlert.Text = "alert('" + ex.Message.ToString().Replace("'", "|") + "');";
            return;
            
        }
        ltlAlert.Text = "window.open('/Excel/" + outputFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
        
    }

    private DataTable GetQADandVender()
    {
        try
        {
            DataTable table = new DataTable("QADandVender");
            DataColumn column;
            DataRow row;


            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "QAD";
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "vender";
            table.Columns.Add(column);

            foreach (GridViewRow gvRow in gvDet.Rows)
            {
                 CheckBox chk = gvRow.FindControl("chk") as CheckBox;
                if(chk != null && chk.Checked)  
                {
                
                    row = table.NewRow();
                    row["QAD"] = gvDet.DataKeys[gvRow.RowIndex].Values["pc_part"].ToString();
                    row["vender"] = gvDet.DataKeys[gvRow.RowIndex].Values["pc_list"].ToString();
                    table.Rows.Add(row);
                }
            }
            return table;
        }
        catch
        {
            return null;
        
        }
    }
    protected void chkIsAddDomai_CheckedChanged(object sender, EventArgs e)
    {
        this.BindData();
    }
    protected void gvDet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (!"&nbsp;".Equals(e.Row.Cells[11].Text.ToString()))
            {

                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
            
        }
    }
    protected void btnHandle_Click(object sender, EventArgs e)
    {
        DataTable dt = this.GetQADandVender();

        string uID = Session["uID"].ToString();

        if (pc.addDomainToPcMstr(dt, uID))
        {
            ltlAlert.Text = "alert('处理成功！')";
            BindData();
        }
        else
        {
            ltlAlert.Text = "alert('处理失败！')";
        }
    }
}