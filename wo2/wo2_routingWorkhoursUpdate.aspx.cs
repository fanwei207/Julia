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

public partial class wo2_routingWorkhoursUpdate : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();

            btnUpdate.Attributes.Add("onclick", "return confirm('你确定要更新工时吗？');");
        }
    }

  

    protected  int RoutingUpdate(string year, string month, int plantCode, string nbr, string lot, string routing)
    {
        adamClass adam = new adamClass();
        try
        {
            string str = "sp_wo2_UpdateRoutingWorkhours";
            SqlParameter[] sqlParam = new SqlParameter[7];
            sqlParam[0] = new SqlParameter("@year", year);
            sqlParam[1] = new SqlParameter("@month", month);
            sqlParam[2] = new SqlParameter("@plantCode", plantCode);
            sqlParam[3] = new SqlParameter("@nbr", nbr);
            sqlParam[4] = new SqlParameter("@lot", lot);
            sqlParam[5] = new SqlParameter("@routing", routing);
            sqlParam[6] = new SqlParameter("@retValue", SqlDbType.Int);
            sqlParam[6].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.StoredProcedure, str, sqlParam);
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

        if (DropDomain.SelectedValue == "0")
        {
            ltlAlert.Text = "alert('公司不能为空！');";
            return;
        }

        if (checkDate(txtYear.Text.Trim(),txtMonth.Text.Trim(), Convert.ToInt32(DropDomain.SelectedValue)))
        {
            ltlAlert.Text = "alert('工资已结算，不能更新！');";
            return;
        }

        int count = RoutingUpdate(txtYear.Text.Trim(), txtMonth.Text.Trim(), Convert.ToInt32(DropDomain.SelectedValue), txtNbr.Text.Trim(), txtLot.Text.Trim(), txtRouting.Text.Trim());

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