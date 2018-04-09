using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Data.SqlClient;

public partial class price_pc_priceList : BasePage
{
    PC_price pc = new PC_price();
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindddlUmType();
            btnExport.Visible = this.Security["1210000290"].isValid;
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        bind();
    }

    private void bind()
    {
        DataTable dt = pc.selectPCMstr(txtQAD.Text.ToString().Trim(), txtVender.Text.ToString().Trim(), txtVenderName.Text.ToString().Trim(), ddlDomain.SelectedItem.Text, chkIsNotEnd.Checked, curr.SelectedItem.Text, ddlUmType.SelectedItem.Text, chkIsNotEffect.Checked);
        gvPriceMstr.DataSource = dt;
        gvPriceMstr.DataBind();
    }

    protected void gvPriceMstr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPriceMstr.PageIndex = e.NewPageIndex;
        bind();
    }

    private void bindddlUmType()
    {
        ddlUmType.Items.Clear();
        ddlUmType.DataSource = selectUmType();
        ddlUmType.DataBind();
        ddlUmType.Items.Insert(0, new ListItem("all", "all"));
    }
    private DataTable selectUmType()
    {
        string sqlstr = "sp_price_selectUmType";

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr).Tables[0];
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
      
        try
        {
            DataTable errDt = selectPriceListExport(txtQAD.Text.ToString().Trim(), txtVender.Text.ToString().Trim(), txtVenderName.Text.ToString().Trim(), ddlDomain.SelectedItem.Text, chkIsNotEnd.Checked, curr.SelectedItem.Text, ddlUmType.SelectedItem.Text, chkIsNotEffect.Checked);//输出错误信息
            string title = "100^<b>QAD</b>~^100^<b>供应商</b>~^100^<b>供应商名称</b>~^100^<b>单位</b>~^100^<b>币种</b>~^"
            + "100^<b>无税价格</b>~^100^<b>含税价格</b>~^100^<b>最小值</b>~^100^<b>最大值</b>~^100^<b>税率</b>~^100^<b>起始时间</b>~^100^<b>终止时间</b>~^100^<b>域</b>~^";
            if (errDt != null && errDt.Rows.Count > 0)
            {
                ExportExcel(title, errDt, false);
            }
        }
        catch (TimeoutException ex)
        {

            Alert("导出数据过多，请增加筛选条件");
        }
    }

    private DataTable selectPriceListExport(string QAD, string vender, string venderName, string domain, bool isNotEnd, string curr, string ddlUmType, bool IsNotEffect)
    {
        string sqlstr = "sp_pc_selectPriceListExport";
        SqlParameter[] param = new SqlParameter[]{
        new SqlParameter("@vender",vender )
        ,new SqlParameter("@venderName" ,venderName)
        ,new SqlParameter("@QAD" ,QAD)
        ,new SqlParameter("@domain" ,domain)
        ,new SqlParameter("@isNotEnd",isNotEnd)
        ,new SqlParameter("@curr",curr)
        ,new SqlParameter("@ddlUmType",ddlUmType)
        ,new SqlParameter("@IsNotEffect",IsNotEffect)
        };

        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).Tables[0];
    }

    //protected void gvPriceMstr_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        if (gvPriceMstr.DataKeys[e.Row.RowIndex].Values("Show_supplierDet"))
    //        {
      
    //        }
    //    }
      
    //}

    protected void gvPriceMstr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lkbtnSupplierDet")
        {

            string supplierNo = e.CommandArgument.ToString();              
            string supplierID;

            if (!supplierNo.Equals(string.Empty))
            {
                supplierID = SelectSupplierID(supplierNo);
                if (!supplierID.Equals("0"))
                {
                    ltlAlert.Text = "$.window('查看供应商详细信息', '70%', '80%','/Supplier/Supplier_InfoView.aspx?supplierNo=" + supplierNo + "&supplierID=" + supplierID + "&pc_mstr=1', true)";
                    
                }
                else
                {
                    ltlAlert.Text = "alert('您选择的供应商没有历史资料，请询问供应商开发部')";
                }
                
            }
            
        }
        if (e.CommandName == "lkbtnQadDet")
        {
            ltlAlert.Text = "$.window('查看QAD详细信息', '70%', '80%','/price/pc_selectQadDoc.aspx?QADDet=" + e.CommandArgument.ToString() + "', true)";
        }
    }

    private string SelectSupplierID(string supplierNo)
    {
        try
        {
            string sqlstr = "sp_pc_selectSupplierID";
            SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@supplierNo",supplierNo)
            };
            return SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, sqlstr, param).ToString();
        }
        catch (Exception)
        {

            return "-1";
        }

    }
}