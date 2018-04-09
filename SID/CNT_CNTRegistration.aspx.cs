using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SID_CNTRegistration : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        { 

            txt_entryTime.Text = DateTime.Now.ToString();
        }
        
    }
    protected void btn_regist_Click(object sender, EventArgs e)
    {
        string sql="sp_Cnt_Registration";
        SqlParameter[] param = new SqlParameter[11];
        param[0] = new SqlParameter("@plate_number", txt_plateCode.Text.Trim());
        param[1] = new SqlParameter("@cnt_id",txt_cntID.Text.Trim());
        param[2] = new SqlParameter("@cnt_entrydate",Convert.ToDateTime(txt_entryTime.Text.Trim()));
        param[3] = new SqlParameter("@driver_name",txt_DriverName.Text.Trim());
        param[4] = new SqlParameter("@driver_IDCard",txt_DriveridCard.Text.Trim());
        param[5] = new SqlParameter("@temporary_ID",txt_temporaryID.Text.Trim());
        param[6] = new SqlParameter("@driver_phone",txt_DriverPhone.Text.Trim());
        param[7] = new SqlParameter("@motorcade_phone",txt_MotorcadePhone.Text.Trim());
        param[8] = new SqlParameter("@retValue",SqlDbType.Int);
        param[8].Direction=ParameterDirection.Output;
        param[9] = new SqlParameter("@registBy", Session["uID"]);
        param[10] = new SqlParameter("@registDate",Convert.ToDateTime( DateTime.Now.ToString()));

        
        if (txt_plateCode.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('车牌号不能为空！（所有的可填空格必须填写！）')";
            return;
        }
        if (txt_DriverName.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('司机姓名不能为空！（所有的可填空格必须填写！）')";
            return;
        }
        if (txt_DriveridCard.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('司机身份证号不能为空！（所有的可填空格必须填写！）')";
            return;
        }
        if (txt_entryTime.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('进厂日期不能为空！（所有的可填空格必须填写！）')";
            return;
        }
        if (txt_DriverPhone.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('司机电话不能为空！（所有的可填空格必须填写！）')";
            return;
        }
        if (txt_MotorcadePhone.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('车队电话不能为空！（所有的可填空格必须填写！）')";
            return;
        }
        if (txt_cntID.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('集装箱号不能为空！（所有的可填空格必须填写！）')";
            return;
        }
        if (txt_temporaryID.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('临时证号不能为空！（所有的可填空格必须填写！）')";
            return;
        }
        

        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, sql, param);
        int re = Convert.ToInt32(param[8].Value);

        if (re == 0)
        {
            ltlAlert.Text = "alert('添加失败，可能已存该进厂登记！')";
        }
        else
            ltlAlert.Text = "alert('添加成功！')";
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("CNT_EntryLeaveList.aspx");
    }
}