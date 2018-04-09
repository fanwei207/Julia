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


public partial class IT_test : System.Web.UI.Page
{
    adamClass adam = new adamClass();
    PI pi = new PI();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected bool chickcust()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@custCode", txtCust.Text.Trim());
            param[1] = new SqlParameter("@etValue", SqlDbType.NVarChar, 50);
            param[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_PI_checkCustCode", param);
            if (param[1].Value.ToString().Length == 0)
            {
                ltlAlert.Text = "alert('客户不存在！请确认代码输入是否正确！');";
                return false;
            }
            else
            {
                return true;
            }
        }

        catch (Exception)
        {
            ltlAlert.Text = "alert('客户验证失败！请刷新后重新操作一次！');";
            return false;
        }
   
    }
    protected bool chickcust2()
    {
        if (txtcust2.Text.Trim() != string.Empty)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@custCode", txtcust2.Text.Trim());
                param[1] = new SqlParameter("@etValue", SqlDbType.NVarChar, 50);
                param[1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_PI_checkCustCode", param);
                if (param[1].Value.ToString().Length == 0)
                {
                    ltlAlert.Text = "alert('客户不存在！请确认代码输入是否正确！');";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('客户验证失败！请刷新后重新操作一次！');";
                return false;
            }
        }
        else
        {
            return true;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime std_date = DateTime.Now;
        DateTime end_date = DateTime.Now;
        if (txtCust.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('客户 不能为空!');";
            return;
        }
        if (txtQad.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('物料号 不能为空!');";
            return;
        }
        if (!(chickcust() && chickcust2() && chicktxtQad()))//校验QAD
        //if (!(chickcust() && chickcust2()))
        {
            return;
        }
        if (txtStdDate.Text.Trim().Length > 0)
        {
            try
            {
                std_date = Convert.ToDateTime(txtStdDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('开始日期格式不正确!正确格式如：2012-01-01');";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('开始日期不能为空！');";
            return;
        }
        if (txtEndDate.Text.Trim().Length > 0)
        {
            try
            {
                end_date = Convert.ToDateTime(txtEndDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('截止日期格式不正确!正确格式如：2012-01-01');";
                return;
            }
        }
        if (end_date < std_date && txtEndDate.Text.Trim().Length > 0 && txtStdDate.Text.Trim().Length > 0)
        {
            ltlAlert.Text = "alert('截止日期必须晚于开始日期!');";
            return;
        }
        if (ddl_Domain.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('域不能为空!');";
            return;
        }
        if (ddlCurrency.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('必须选择货币!');";
            return;
        }
        if (ddlUM.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('必须选择单位!');";
            return;
        }
        if (txtprice1.Text == string.Empty && txtprice2.Text == string.Empty && txtprice3.Text == string.Empty)
        {
            ltlAlert.Text = "alert('价格不能同时为空!');";
            return;
        }
        if (txtprice1.Text != string.Empty )
        {
            try
            {
                decimal money = Convert.ToDecimal(txtprice1.Text);
                int st = txtprice1.Text.IndexOf(".");
                if (st>=0 &&  txtprice1.Text.Length - st > 6)
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
        if (txtprice2.Text != string.Empty)
        {
            try
            {
                decimal money = Convert.ToDecimal(txtprice2.Text);
                int st = txtprice2.Text.IndexOf(".");
                if (st >= 0 && txtprice2.Text.Length - st > 6)
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
        if (txtprice3.Text != string.Empty)
        {
            try
            {
                decimal money = Convert.ToDecimal(txtprice3.Text);
                int st = txtprice3.Text.IndexOf(".");
                if (st >= 0 && txtprice3.Text.Length - st > 6)
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
        int bit = pi.CheckPriceLists(txtCust.Text, txtQad.Text, txtcust2.Text, ddlCurrency.SelectedItem.Text, ddlUM.SelectedItem.Text, txtStdDate.Text, txtEndDate.Text, ddl_Domain.SelectedValue);
        if (bit==0)
        {
            ltlAlert.Text = "alert('当前价格有效期内已存在指定价格！');";
            return;
        }
        else if (bit == 2)
        {
            ltlAlert.Text = "alert('价格检验失败，请重试!');";
            return;
        }
        if (pi.InsertPriceList(txtCust.Text, txtQad.Text, txtcust2.Text, ddl_Domain.SelectedValue, ddlCurrency.SelectedItem.Text, ddlUM.SelectedItem.Text, txtStdDate.Text, txtEndDate.Text, txtprice1.Text, txtprice2.Text, txtprice3.Text, Session["uID"].ToString(), Session["uName"].ToString(),txtremark.Text.Trim()))
        {
            ltlAlert.Text = "alert('保存成功!');";
            txtCust.Text=string.Empty;
            txtQad.Text=string.Empty;
            txtcust2.Text=string.Empty;
            ddl_Domain.SelectedIndex = 0;
            ddlCurrency.SelectedIndex = 0;
            ddlUM.SelectedIndex = 0;
            txtStdDate.Text=string.Empty;
            txtEndDate.Text=string.Empty;
            txtprice1.Text=string.Empty;
            txtprice2.Text=string.Empty;
            txtprice3.Text = string.Empty;
            lblpart.Text = string.Empty;
            lbCustName.Text = string.Empty;
            lbCustName2.Text = string.Empty;
            txtremark.Text = string.Empty;
            return;
        }
        else
        {
            ltlAlert.Text = "alert('保存失败!');";
            return;
        }
          
    }



    protected bool chicktxtQad()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@QADpart", txtQad.Text.Trim());
            param[1] = new SqlParameter("@etValue", SqlDbType.NVarChar, 50);
            param[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_PI_checkQADpart", param);
            if (param[1].Value.ToString().Length == 0)
            {
                ltlAlert.Text = "alert('QAD号不存在！请确认代码输入是否正确！');";
                return false;
            }
            else
            {
                return true;
            }
           
        }
        catch
        {
            ltlAlert.Text = "alert('QAD号验证失败！请刷新后重新操作一次！');";
            return false;
        }
    }

    ///// <summary>
    ///// 价格导入
    ///// </summary>
    ///// <param name="SID_QAD"></param>
    ///// <param name="SID_cust_part"></param>
    ///// <param name="SID_Ptype"></param>
    ///// <param name="SID_Cust"></param>
    ///// <param name="SID_ShipTo"></param>
    ///// <param name="SID_ShipName"></param>
    ///// <param name="SID_BillTo"></param>
    ///// <param name="SID_BillName"></param>
    ///// <param name="SID_price1"></param>
    ///// <param name="SID_price2"></param>
    ///// <param name="SID_price3"></param>
    //public bool InsertPriceList(string Pi_Cust, string Pi_QAD, string Pi_ShipTo, string Pi_Currency, string Pi_UM, string Pi_StartDate, string Pi_EndDate, string Pi_price1, string Pi_price2, string Pi_price3, string Pi_createdBy, string Pi_createdName)
    //{
    //    try
    //    {
    //        Pi_price1 = (Pi_price1 == string.Empty) ? "0" : Pi_price1;
    //        Pi_price2 = (Pi_price2 == string.Empty) ? "0" : Pi_price2;
    //        Pi_price3 = (Pi_price3 == string.Empty) ? "0" : Pi_price3;
    //        string strSQL = "sp_Pi_insert_PriceList";
    //        SqlParameter[] parm = new SqlParameter[12];
    //        parm[0] = new SqlParameter("@Pi_Cust", Pi_Cust);
    //        parm[1] = new SqlParameter("@Pi_QAD", Pi_QAD);
    //        parm[2] = new SqlParameter("@Pi_ShipTo", Pi_ShipTo);
    //        parm[3] = new SqlParameter("@Pi_Currency", Pi_Currency);
    //        parm[4] = new SqlParameter("@Pi_UM", Pi_UM);
    //        parm[5] = new SqlParameter("@Pi_StartDate", Pi_StartDate);
    //        parm[6] = new SqlParameter("@Pi_EndDate", Pi_EndDate);
    //        parm[7] = new SqlParameter("@Pi_price1", Pi_price1);
    //        parm[8] = new SqlParameter("@Pi_price2", Pi_price2);
    //        parm[9] = new SqlParameter("@Pi_price3", Pi_price3);
    //        parm[10] = new SqlParameter("@Pi_createdBy", Pi_createdBy);
    //        parm[11] = new SqlParameter("@Pi_createdName", Pi_createdName);
    //        SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
    //        return true;
    //    }
    //    catch (Exception)
    //    {
    //        return false;
    //    }

    //}

    ///// <summary>
    ///// 检验价格是否存在
    ///// </summary>
    ///// <param name="Pi_Cust"></param>
    ///// <param name="Pi_QAD"></param>
    ///// <param name="Pi_ShipTo"></param>
    ///// <param name="Pi_Currency"></param>
    ///// <param name="Pi_UM"></param>
    ///// <param name="Pi_StartDate"></param>
    ///// <param name="Pi_EndDate"></param>
    ///// <returns></returns>
    //public int CheckPriceList(string Pi_Cust, string Pi_QAD, string Pi_ShipTo, string Pi_Currency, string Pi_UM, string Pi_StartDate, string Pi_EndDate)
    //{
    //    try
    //    {
    //        string strSQL = "sp_Pi_Check_PriceList";
    //        SqlParameter[] parm = new SqlParameter[7];
    //        parm[0] = new SqlParameter("@Pi_Cust", Pi_Cust);
    //        parm[1] = new SqlParameter("@Pi_QAD", Pi_QAD);
    //        parm[2] = new SqlParameter("@Pi_ShipTo", Pi_ShipTo);
    //        parm[3] = new SqlParameter("@Pi_Currency", Pi_Currency);
    //        parm[4] = new SqlParameter("@Pi_UM", Pi_UM);
    //        parm[5] = new SqlParameter("@Pi_StartDate", Pi_StartDate);
    //        parm[6] = new SqlParameter("@Pi_EndDate", Pi_EndDate);
    //        SqlDataReader read= SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strSQL, parm);
    //        while (read.Read())
    //        {
    //            return 0;
    //        }
    //        return 1;
    //    }
    //    catch (Exception)
    //    {
    //        return 2;
    //    }

    //}
}