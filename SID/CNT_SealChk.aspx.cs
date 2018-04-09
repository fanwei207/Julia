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

public partial class SID_CNT_SealChk : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (!this.Security["550050024"].isValid)
            {
                bnt_check.Enabled = false;
                txt_sealID.BackColor = System.Drawing.Color.LightGray;
                txt_sealID.Enabled = false;
                txt_Qty.BackColor = System.Drawing.Color.LightGray;
                txt_Qty.Enabled = false;
                txt_remark.BackColor = System.Drawing.Color.LightGray;
                txt_remark.Enabled = false;
                txt_DistributeQty.BackColor = System.Drawing.Color.LightGray;
                txt_DistributeQty.Enabled = false;

                rbtn_resultNO.Enabled = false;
                rbtn_resultOK.Enabled = false;
            }

            txt_entry.Text=Request.QueryString["entryDate"];
            txt_cntID.Text = Request.QueryString["cntID"];

            string sql = "select * from CNTManage_stripsealcheck where cnt_id='" + txt_cntID.Text + "' and cnt_entrydate='" + txt_entry.Text + "'";
            IDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, sql);
            while (reader.Read())
            {
                txt_DistributeQty.Text = reader["seal_outquantity"].ToString();
                txt_Qty.Text = reader["seal_quantity"].ToString();
                txt_remark.Text = reader["seal_remark"].ToString();
                txt_sealID.Text = reader["seal_ID"].ToString();
                if (Convert.ToInt32(reader["seal_checkresult"]) == 1) rbtn_resultOK.Checked = true; else rbtn_resultNO.Checked = true;
            }
            reader.Close();
        }
    }
    protected void bnt_back_Click(object sender, EventArgs e)
    {
        string centID = Request.Form["txt_cntID"].Trim(); 
        string entryDate=Request.Form["txt_entry"].Trim();
        Response.Redirect("CNT_EntryLeaveList.aspx?centID=" + centID +"&entryDate=" + entryDate);
    }
    protected void bnt_check_Click(object sender, EventArgs e)
    {
        

        
        if(txt_sealID.Text.Trim().Length==0)
        {
            ltlAlert.Text = "alert('封条号必须填写！')";
            return;
        }
        if (txt_Qty.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('数量必须填写！')";
            return;
        }
        if (txt_DistributeQty.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('发出数量必须填写！')";
            return;
        }
        if (rbtn_resultNO.Checked == true && txt_remark.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('备注必须填写！')";
            return;
        }
        if (rbtn_resultNO.Checked == false && rbtn_resultOK.Checked == false)
        {
            ltlAlert.Text = "alert('检查结果必须填写！')";
            return;
        }

        bool result = false;

        if(rbtn_resultOK.Checked)
        {
            result = true;
        }

        string remark = txt_remark.Text.Trim();
        string sealID = txt_sealID.Text.Trim();
        int quantity = Convert.ToInt32(txt_Qty.Text.Trim());
        int outquantity = Convert.ToInt32(txt_DistributeQty.Text.Trim());

        if (outquantity > quantity)
        {
            txt_DistributeQty.Text = "";
            ltlAlert.Text = "alert('发出数量不能比数量大，请重新填写！')";
            return;
        }

        string sql = "sp_CNT_insertSealChk";

        SqlParameter[] param = new SqlParameter[10];
        param[0] = new SqlParameter("@cnt_id", Request.Form["txt_cntID"].Trim());
        param[1] = new SqlParameter("@cnt_entrydate", Request.Form["txt_entry"].Trim());
        param[2] = new SqlParameter("@quantity", quantity);
        param[3] = new SqlParameter("@outquantity", outquantity);
        param[4] = new SqlParameter("@result", result);
        param[5] = new SqlParameter("@sealID", sealID);
        param[6] = new SqlParameter("@remark", remark);
        param[7] = new SqlParameter("@reValue", SqlDbType.Int);
        param[7].Direction = ParameterDirection.Output;
        param[8] = new SqlParameter("@checkBy", Session["uID"]);
        param[9] = new SqlParameter("@checkDate", DateTime.Now.ToString());
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, sql, param);

        int re = Convert.ToInt32(param[7].Value);

        if (re == 0)
        {
            ltlAlert.Text = "alert('检查记录添加失败！')";
        }
        else { ltlAlert.Text = "alert('检查记录添加成功！')"; }
     }
}