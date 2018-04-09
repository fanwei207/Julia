using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TCPNEW;

public partial class New_FixAssetReservation : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtInputDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddDays(-7));

            txtVouDate.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString();

            Bind();                                 
        }
        
    }

    private void Bind()
    {
        SqlDataReader sdr =   GetFixAssetReserve(); 

        while (sdr.Read())
        {
            if (sdr["type"].ToString() == "1")
            {
                lblLastInputDate.Text = sdr["fixas_reserve_inputDate"].ToString();
                lblLastVouDate.Text = sdr["fixas_reserve_vouDate"].ToString();
            }
            else
            {
                if (sdr["fixas_reserve_uID"].ToString() == string.Empty && sdr["fixas_reserve_uName"].ToString() == string.Empty && sdr["fixas_reserve_inputDate"].ToString() == string.Empty
                    && sdr["fixas_reserve_vouDate"].ToString() == string.Empty && sdr["fixas_reserve_createDate"].ToString() == string.Empty)
                {
                    reserveInfo.Visible = false;
                }
                else
                {
                    lbluID.Text = sdr["fixas_reserve_uID"].ToString();
                    lbluName.Text = sdr["fixas_reserve_uName"].ToString();
                    lblinputDate.Text = sdr["fixas_reserve_inputDate"].ToString();
                    lblvouDate.Text = sdr["fixas_reserve_vouDate"].ToString();
                    lbldate.Text = sdr["fixas_reserve_createDate"].ToString();
                }
                
            }

        }
        sdr.Dispose();
        sdr.Close();


    }
    protected void btnReserve_Click(object sender, EventArgs e)
    {
       SqlDataReader sdr =   GetFixAssetReserve();

           if (txtInputDate.Text.Trim() == string.Empty)
           {
               ltlAlert.Text = " Alert('输入日期 不能为空!');";
               return;
           }
           else
           {
               try
               {
                   DateTime _dt = Convert.ToDateTime(txtInputDate.Text + "-1");
               }
               catch (Exception)
               {

                   ltlAlert.Text = "Alert('输入格式不正确 ！格式如：2013-09-01');";
               }
           }
           if (txtVouDate.Text.Trim() != string.Empty)
           {
               try
               {
                   DateTime _dt = Convert.ToDateTime(txtVouDate.Text + "-1");
               }
               catch
               {
                   ltlAlert.Text = "alert('折旧截止月份格式不正确！格式如：2011-09');";
                   return;
               }
           }
           else
           {
               ltlAlert.Text = "alert('折旧截止月份格式不正确！格式如：2011-09');";
               return;
           }
           try
           {
               if (sdr.Read())
               {
                   if (sdr["fixas_reserve_inputDate"].ToString() == txtInputDate.Text.Trim() && sdr["fixas_reserve_vouDate"].ToString() == txtVouDate.Text.Trim())
                   {
                       Alert("已预订该信息");
                   }
                   else
                   {
                       UpdateFixAssetReserve(txtInputDate.Text, txtVouDate.Text);
                       this.ltlAlert.Text = " alert('预定成功'); var loc=$('body', parent.document).find('#ifrm_100103118')[0].contentWindow.location; loc.replace(loc.href);$.loading('none');$('BODY', parent.parent.parent.document).find('#j-modal-dialog').remove();";
                   }
               }                           
           }
           catch
           {
               Alert("预定失败");
           }
         
    }

    private void UpdateFixAssetReserve(string AssetDate, string VouDate)
    {
        string str = "sp_Fix_UpdateAssetReserve";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@AssetDate", txtInputDate.Text.ToString().Trim());
        param[1] = new SqlParameter("@VouDate", txtVouDate.Text.ToString().Trim());
        param[2] = new SqlParameter("@uID",Session["uID"].ToString());
        param[3] = new SqlParameter("@uName", Session["uName"].ToString());

        SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, param);
    }
    public SqlDataReader GetFixAssetReserve()
    {
        string strName = "sp_Fix_GetFixAssetReserve";


       return SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, strName);
    }
}