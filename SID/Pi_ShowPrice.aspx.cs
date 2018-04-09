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
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;
using PIInfo;
using NPOI.SS.Util;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using adamFuncs;
using System.Data.OleDb;


public partial class IT_Pi_ShowPrice : BasePage
{
    adamClass adam = new adamClass();
    PI pi = new PI();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            //BindGridView();
            //txtCrtDate1.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-7));
        }
    }

    protected void BindGridView()
    {
        if (!this.Security["550011040"].isValid)
        {
            gv.Columns[12].Visible = false;
            gv.Columns[13].Visible = false;
        }
        gv.DataSource = pi.showPriceList(txtcust.Text.Trim(), txtQAD.Text.Trim(), txtshipto.Text.Trim(), txtCrtDate1.Text.Trim(), txtCrtDate3.Text.Trim(), txtCrtDate2.Text.Trim(), txtCrtDate4.Text.Trim(), ddl_status.SelectedValue, ddl_Domain.SelectedValue);
        gv.DataBind();
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindGridView();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }


    ///// <summary>
    ///// 查询价格
    ///// </summary>
    ///// <param name="Pi_Cust"></param>
    ///// <param name="Pi_QAD"></param>
    ///// <param name="Pi_ShipTo"></param>
    ///// <returns></returns>
    //public DataTable showPriceList(string Pi_Cust, string Pi_QAD, string Pi_ShipTo)
    //{
    //    try
    //    {
    //        string strSQL = "sp_Pi_select_PriceList";
    //        SqlParameter[] parm = new SqlParameter[12];
    //        parm[0] = new SqlParameter("@cust", Pi_Cust);
    //        parm[1] = new SqlParameter("@QAD", Pi_QAD);
    //        parm[2] = new SqlParameter("@shipto", Pi_ShipTo);
    //        return SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm).Tables[0];
    //    }
    //    catch (Exception)
    //    {
    //        return null;
    //    }

    //}

    protected void gv_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        BindGridView();
    }
    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
         BindGridView();

         String Currency = ((Label)gv.Rows[e.NewEditIndex].Cells[4].FindControl("lblCurrency")).Text.Trim();

         Currency = Currency.ToUpper();
         ((DropDownList)gv.Rows[e.NewEditIndex].Cells[4].FindControl("ddlCurrency")).Items.FindByText(Currency).Selected = true;


         String ptype = ((Label)gv.Rows[e.NewEditIndex].Cells[5].FindControl("lblUM")).Text.Trim();
         ptype=ptype.ToUpper();
         ((DropDownList)gv.Rows[e.NewEditIndex].Cells[5].FindControl("drpUM")).Items.FindByText(ptype).Selected = true;

    }
    protected void gv_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        String id = gv.DataKeys[e.RowIndex].Values[0].ToString();
        String strCurrency = ((DropDownList)gv.Rows[e.RowIndex].Cells[4].FindControl("ddlCurrency")).SelectedValue.ToString().Trim();
        String strUM = ((DropDownList)gv.Rows[e.RowIndex].Cells[5].FindControl("drpUM")).SelectedValue.ToString().Trim();
        //String strStartDate = ((TextBox)gv.Rows[e.RowIndex].Cells[7].FindControl("txtStartDate")).Text.ToString().Trim();
        String strEndDate = ((TextBox)gv.Rows[e.RowIndex].Cells[8].FindControl("txtEndDate")).Text.ToString().Trim();
        String txtprice1 = ((TextBox)gv.Rows[e.RowIndex].Cells[9].FindControl("txtprice1")).Text.ToString().Trim();
        String txtprice2 = ((TextBox)gv.Rows[e.RowIndex].Cells[10].FindControl("txtprice2")).Text.ToString().Trim();
        String txtprice3 = ((TextBox)gv.Rows[e.RowIndex].Cells[11].FindControl("txtprice3")).Text.ToString().Trim();
        String txtRemark = ((TextBox)gv.Rows[e.RowIndex].Cells[17].FindControl("txtRemark")).Text.ToString().Trim();
        String txtCust = gv.Rows[e.RowIndex].Cells[0].Text.Trim();
        String txtQad = gv.Rows[e.RowIndex].Cells[1].Text.Trim();
        String txtcust2 = gv.Rows[e.RowIndex].Cells[2].Text.Trim();
        String strStartDate = gv.Rows[e.RowIndex].Cells[7].Text.Trim();
        String PiDoMain = gv.Rows[e.RowIndex].Cells[3].Text.Trim();
        
        #region 验证
        DateTime std_date = DateTime.Now;
        DateTime end_date = DateTime.Now;
        if (strStartDate.Trim().Length > 0)
        {
            try
            {
                std_date = Convert.ToDateTime(strStartDate.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('生效日期格式不正确!正确格式如：2012-01-01');";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('生效日期不能为空！');";
            return;
        }
        if (strEndDate.Trim().Length > 0)
        {
            try
            {
                end_date = Convert.ToDateTime(strEndDate.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确!正确格式如：2012-01-01');";
                return;
            }
        }
        if (end_date < std_date && strEndDate.Trim().Length > 0 && strStartDate.Trim().Length > 0)
        {
            ltlAlert.Text = "alert('截止日期必须晚于开始日期!');";
            return;
        }
        
        
        if (txtprice1 == string.Empty && txtprice2 == string.Empty && txtprice3 == string.Empty)
        {
            ltlAlert.Text = "alert('价格不能同时为空!');";
            return;
        }
        if (txtprice1 != string.Empty)
        {
            try
            {
                decimal money = Convert.ToDecimal(txtprice1);
                int st = txtprice1.IndexOf(".");
                if (st >= 0 && txtprice1.Length - st > 6)
                {
                    ltlAlert.Text = "alert('价格1 只能保留5位小数!');";
                    return;
                }
            }
            catch (Exception)
            {

                ltlAlert.Text = "alert('价格1 格式不正确!');";
                return;
            }
        }
        if (txtprice2 != string.Empty)
        {
            try
            {
                decimal money = Convert.ToDecimal(txtprice2);
                int st = txtprice2.IndexOf(".");
                if (st >= 0 && txtprice2.Length - st > 6)
                {
                    ltlAlert.Text = "alert('价格2 只能保留5位小数!');";
                    return;
                }

            }
            catch (Exception)
            {

                ltlAlert.Text = "alert('价格2 格式不正确!');";
                return;
            }
        }
        if (txtprice3 != string.Empty)
        {
            try
            {
                decimal money = Convert.ToDecimal(txtprice3);
                int st = txtprice3.IndexOf(".");
                if (st >= 0 && txtprice3.Length - st > 6)
                {
                    ltlAlert.Text = "alert('价格3 只能保留5位小数!');";
                    return;
                }

            }
            catch (Exception)
            {
                ltlAlert.Text = "alert('价格3 格式不正确!');";
                return;
            }
        }
        int bit = pi.CheckPriceList(id, txtCust, txtQad, txtcust2, strCurrency, strUM, strStartDate, strEndDate, PiDoMain);

            if (bit == 3)
        {
            ltlAlert.Text = "alert('历史价格不可修改！');";
            gv.EditIndex = -1;
            BindGridView();
            return;
        }
         else  if (bit == 0)
        {
            ltlAlert.Text = "alert('当前价格有效期内已存在指定价格！');";
            return;
        }
        else if (bit == 2)
        {
            ltlAlert.Text = "alert('价格检验失败，请重试!');";
            return;
        }
        else if (bit == 4)
        {
            ltlAlert.Text = "alert('截止日期不能超过前一个价格的起始日期!');";
            return;
        }
        #endregion

       
        //IErr = sid.UpdateShipDetail(Convert.ToString(Session["uID"]), strDID, strSNO, strBox, strQa, strWo, strweight, strvolume, strprice, strprice1, strmemo, strQtyset, strQtypcs, strPkgs, strPtype, strTcpPo);
       if(pi.UpdatePriceList(id,strCurrency,strUM,strStartDate,strEndDate,txtprice1,txtprice2,txtprice3,txtRemark,Convert.ToString(Session["uID"]),Convert.ToString(Session["uName"])))
       {
           gv.EditIndex = -1;
            BindGridView();
       }

        else
        {
            ltlAlert.Text = "alert('更新失败！');";
            return;
        }
        
    }



    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (pi.DeletePriceList(gv.DataKeys[e.RowIndex].Values[0].ToString(), Convert.ToString(Session["uID"]), Convert.ToString(Session["uName"])))
        {
            BindGridView();
        }
        else
        {
            ltlAlert.Text = "alert('删除失败！');";
            return;
        }
       
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = pi.showPriceList(txtcust.Text.Trim(), txtQAD.Text.Trim(), txtshipto.Text.Trim(), txtCrtDate1.Text.Trim(), txtCrtDate3.Text.Trim(), txtCrtDate2.Text.Trim(), txtCrtDate4.Text.Trim(), ddl_status.SelectedValue, ddl_Domain.SelectedValue);
        string title = "70^<b>客户</b>~^110^<b>物料号</b>~^70^<b>最终客户</b>~^70^<b>域</b>~^50^<b>货币</b>~^50^<b>单位</b>~^50^<b>系统单位</b>~^70^<b>开始时间</b>~^70^<b>截止时间</b>~^100^<b>价格1</b>~^100^<b>价格2</b>~^100^<b>价格3</b>~^200^<b>客户名称</b>~^200^<b>物料描述</b>~^200^<b>最终客户名称</b>~^200^<b>备注</b>~^";
        this.ExportExcel(title, dt, false);
    }
}