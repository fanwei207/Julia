//summary
//     Author :   Simon
//Create Date :   May 31 ,2009
//Description :   Maintenance  fix asset, detail and it's increment .
//summary

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
using Microsoft.ApplicationBlocks.Data;
using TCPNEW;
using System.Text;
using System.IO;
using System.Data.SqlClient;

public partial class new_FixAssetSearch : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            //txtInputDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-7));

            //txtVouDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();

            //LblInputDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-7));
            //LblVouDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();
            //if (Request.QueryString["back"]!= null)
            //{
            //    LblInputDate.Text = Request["fixas_reserve_inputDate"].ToString();
            //    LblVouDate.Text = Request["fixas_reserve_vouDate"].ToString();
            //}
            Bind();

        }        
        
    }

    /// <summary> 
    /// 获取前月月末日期 
    /// </summary> 
    /// <returns> </returns> 
    public static DateTime getStartMonth()
    {
        int span = Convert.ToInt32(System.DateTime.Now.Day);
        //span = span - 1;
        System.DateTime d1 = System.DateTime.Now.AddDays(-span);
        return d1;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //if (LblInputDate.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('输入日期 不能为空！');";
        //    return;
        //}
        //else
        //{
        //    try
        //    {
        //        DateTime _dt = Convert.ToDateTime(txtInputDate.Text + "-1");
        //    }
        //    catch
        //    {
        //        ltlAlert.Text = "alert('输入日期 格式不正确！格式如：2013-09-01');";
        //        return;
        //    }
        //}

        //if (txtVouDate.Text.Trim() != string.Empty)
        //{
        //    try
        //    {
        //        DateTime _dt = Convert.ToDateTime(txtVouDate.Text + "-1");
        ////    }
        ////    catch
        ////    {
        ////        ltlAlert.Text = "alert('折旧截止月份格式不正确！格式如：2011-09');";
        ////        return;
        ////    }
        ////}
        ////else
        ////{
        ////    ltlAlert.Text = "alert('折旧截止月份格式不正确！格式如：2011-09');";
        ////    return;
        ////}

        //if (txtCanZhiRate.Text == string.Empty)
        //{
        //    ltlAlert.Text = "alert('残值率不能为空！');";

        //    return;
        //}
        //else
        //{
        //    try
        //    {
        //        int rate = Convert.ToInt16(txtCanZhiRate.Text.Trim());

        //        if (rate <= 0 || rate >= 100)
        //        {
        //            ltlAlert.Text = "alert('残值率必须是小于100的整数！');";

        //            return;
        //        }
        //    }
        //    catch
        //    {
        //        ltlAlert.Text = "alert('残值率必须是小于100的整数！');";
        //        return;
        //    }
        //}

        try
        {
            DataTable dtSearch = null;
            dtSearch = GetDataTcp.GetFixAssetReserve(txtInputNo.Text, LblInputDate.Text, LblVouDate.Text, txtCanZhiRate.Text.Trim());
            gvFix.DataSource = dtSearch;
            gvFix.DataBind();
            dtSearch.Clear();

        }
        catch
        {
        }

    }

    protected void gvFix_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFix.PageIndex = e.NewPageIndex;

        DataTable dtSearch = null;
        dtSearch = GetDataTcp.GetFixAssetReserve(txtInputNo.Text, LblInputDate.Text, LblVouDate.Text, txtCanZhiRate.Text.Trim());
        
        gvFix.DataSource = dtSearch;
        gvFix.DataBind();
        dtSearch.Clear();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {

        ltlAlert.Text = "window.open('/New/FixAssetSearchExportExcel.aspx?inputNo=" + txtInputNo.Text.Trim() + "&inputDate=" + LblInputDate.Text + "&vouDate=" + LblVouDate.Text + "&canZhiRate=" + txtCanZhiRate.Text.Trim() + "&rt=" + DateTime.Now.ToString() + "', '_blank');";
    }



    protected void btnReserve_Click(object sender, EventArgs e)
    {
     
        //ltlAlert.Text = "$.window('预定信息', '70%', '80%','/new/FixAssetReservation.aspx?fixas_reserve_inputDate="+ Request["fixas_reserve_inputDate"].ToString()+"&fixas_reserve_vouDate=" + Request["fixas_reserve_vouDate"].ToString()+"', true)";
        ltlAlert.Text = "$.window('预定信息', '70%', '80%','/new/FixAssetReservation.aspx', true)";
    }
    public SqlDataReader GetFixAssetReserve()
    {
        string strName = "sp_Fix_GetFixAssetReserve";


        return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strName);
    }
    private void Bind()
    {
        SqlDataReader sdr = GetFixAssetReserve();

        while (sdr.Read())
        {
            if (sdr["type"].ToString() == "1")
            {
                LblInputDate.Text = string.Format("{0:yyyy-MM-dd}", sdr["fixas_reserve_inputDate"].ToString());
                
                LblVouDate.Text = sdr["fixas_reserve_vouDate"].ToString();               
            }           
        }
        sdr.Dispose();
        sdr.Close();


    }
}
