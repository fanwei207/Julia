using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

public partial class product_productCheckTable : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind(); 
        }
    }
    protected void gvInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInfo.PageIndex = e.NewPageIndex;
        Bind();    
    }

    private void Bind()
    {
        DataTable dt = this.selectproductAndQCSizelist(txtQAD.Text,ddlDomain.SelectedItem.Text,chkDiff.Checked,txtStartDate.Text,txtEndDate.Text);

        gvInfo.DataSource = dt;
        gvInfo.DataBind();
    
    }

    private DataTable selectproductAndQCSizelist(string part, string domain, bool isDiff, string StartDate , string EndDate)
    {
        try
        {
            string sqlstr = "sp_product_productAndQCSizelist";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@part",part)
                , new SqlParameter("@domain",domain)
                , new SqlParameter("@diff",isDiff)
                , new SqlParameter("@dateBegin",StartDate)
                , new SqlParameter("@dateEnd",EndDate)
            };

            return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void gvInfo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string diff = gvInfo.DataKeys[e.Row.RowIndex].Values["isDiff"].ToString();

            bool isDiff = Convert.ToBoolean(diff);

            string errorQualityStr = gvInfo.DataKeys[e.Row.RowIndex].Values["errorQuality"].ToString();
            decimal errorQuality = 0;
            string errorVolumeStr = gvInfo.DataKeys[e.Row.RowIndex].Values["errorVolume"].ToString();
            decimal errorVolume = 0;
            string ERRORStr = gvInfo.DataKeys[e.Row.RowIndex].Values["ERROR"].ToString();
            decimal ERROR = 0;

            if (decimal.TryParse(ERRORStr, out ERROR))
            {
                if (decimal.TryParse(errorQualityStr, out errorQuality))
                {
                    if (errorQuality >= ERROR)
                    {
                        e.Row.Cells[7].BackColor = System.Drawing.Color.Red;
                    }
                
                }
                else if (errorQualityStr.Equals("NaN"))
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Red;
                }

                if (decimal.TryParse(errorVolumeStr, out errorVolume))
                {
                    if (errorVolume >= ERROR)
                    {
                        e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
                    }

                }
                else if (errorVolumeStr.Equals("NaN"))
                {
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
                }
            
            }


            if (isDiff)
            {
                e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
               
            }
        }
    }
    protected void chkDiff_CheckedChanged(object sender, EventArgs e)
    {
        Bind();
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Bind();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable dt = this.selectproductAndQCSizelist(txtQAD.Text, ddlDomain.SelectedItem.Text, chkDiff.Checked, txtStartDate.Text, txtEndDate.Text);

        string title = "100^<b>QAD</b>~^100^<b>成品检验日期</b>~^100^<b>工单号</b>~^60^<b>域</b>~^" +
           "70^<b>误差范围(%)</b>~^"+"80^<b>是否存在差异</b>~^"+
            "100^<b>标准质量(kg)</b>~^150^<b>实际质量(kg)</b>~^70^<b>质量偏差(%)</b>~^" +
           "100^<b>标准体积(m³)</b>~^100^<b>实际体积(m³)</b>~^70^<b>体积偏差(%)</b>~^" +
           "100^<b>标准长(cm)</b>~^100^<b>实际平均长(cm)</b>~^" +
           "100^<b>标准宽(cm)</b>~^100^<b>实际平均宽(cm)</b>~^100^<b>标准高(cm)</b>~^100^<b>实际平均高(cm)</b>~^" ;
        if (dt != null && dt.Rows.Count > 0)
        {
            ExportExcel(title, dt, false);
        }
    }
}