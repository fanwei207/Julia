using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text.RegularExpressions;

public partial class wo2_routingPostionUpdate : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();

            btnUpdate.Attributes.Add("onclick", "return confirm('你确定要工位系数吗？');");
        }
    }

  

    protected  int PostionUpdate(string year, string month, string nbr, string lot, string postion)
    {
        adamClass adam = new adamClass();
        try
        {
            string str = "sp_wo2_updateWoeWhenPostionModify";
            SqlParameter[] sqlParam = new SqlParameter[7];
            sqlParam[0] = new SqlParameter("@year", year);
            sqlParam[1] = new SqlParameter("@month", month);
            sqlParam[2] = new SqlParameter("@nbr", nbr);
            sqlParam[3] = new SqlParameter("@lot", lot);
            sqlParam[4] = new SqlParameter("@postion", postion);
            sqlParam[5] = new SqlParameter("@uID", Session["uID"].ToString());
            sqlParam[6] = new SqlParameter("@retValue", SqlDbType.Int);
            sqlParam[6].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.StoredProcedure, str, sqlParam);
            return Convert.ToInt32(sqlParam[6].Value);
        }
        catch
        {
            return -1;
        }
    
    }

    protected bool checkDate(string year, string month, int plantCode)
    {
        adamClass adam = new adamClass();
        try
        {
            string str = "sp_wo2_CheckUpdateRoutingDate";
            SqlParameter[] sqlParam = new SqlParameter[3];
            sqlParam[0] = new SqlParameter("@year", year);
            sqlParam[1] = new SqlParameter("@month", month);
            sqlParam[2] = new SqlParameter("@plantCode", plantCode);
            SqlDataReader reader = SqlHelper.ExecuteReader(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);
            if (reader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        catch
        {
            return false;
        }

    }


    protected void btnUpdate_Click1(object sender, EventArgs e)
    {
        string year = @"^([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})$";
        string month = @"^(0[1-9]|[1-9]|1[0-2])$";
        if (txtYear.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('年份不能为空！');";
            return;
        }
        else
        {
            if (!new Regex(year).IsMatch(txtYear.Text.Trim()))
            {
                ltlAlert.Text = "alert('年份不合法！');";
                return;
            }
        }

        if (txtMonth.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('月份不能为空！');";
            return;
        }
        else
        {
            if (!new Regex(month).IsMatch(txtMonth.Text.Trim()))
            {
                ltlAlert.Text = "alert('月份不合法！');";
                return;
            }
        }

        if (checkDate(txtYear.Text.Trim(),txtMonth.Text.Trim(), Convert.ToInt32(Session["PlantCode"].ToString())))
        {
            ltlAlert.Text = "alert('工资已结算，不能更新！');";
            return;
        }

        int count = PostionUpdate(txtYear.Text.Trim(), txtMonth.Text.Trim(), txtNbr.Text.Trim(), txtLot.Text.Trim(), txtRouting.Text.Trim());

        if (count < 0)
        {
            ltlAlert.Text = "alert('更新失败！');";
            return;
        }
        else
        {
            
            Labcount.Text = count.ToString();
            ltlAlert.Text = "alert('更新成功！');";
        }
    }
}