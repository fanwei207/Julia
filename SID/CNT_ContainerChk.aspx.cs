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

public partial class SID_CNT_ContainerChk :  BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (!this.Security["550050025"].isValid)
            {
                bnt_check.Enabled = false;
                cblist.Enabled = false;
                rbtn_resultNO.Enabled = false;
                rbtn_resultOK.Enabled = false;
            }
            txt_entryDate.Text = Request.QueryString["entryDate"];
            txt_cntID.Text = Request.QueryString["cntID"];
            txt_plateNmb.Text = Request.QueryString["plateNmb"];
            txt_sealID.Text = Request.QueryString["sealID"];

            string sql = "select * from CNTManage_containerinfo where cnt_id='" + txt_cntID.Text + "' and cnt_entrydate='" + txt_entryDate.Text+"'";
            IDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, sql);
            while(reader.Read())
            {
                if (Convert.ToInt32(reader["cnt_bottom"]) == 1) { cblist.Items[0].Selected = true; }
                if (Convert.ToInt32(reader["cnt_inside"]) == 1) { cblist.Items[1].Selected = true; }
                if (Convert.ToInt32(reader["cnt_outside"]) == 1) { cblist.Items[2].Selected = true; }
                if (Convert.ToInt32(reader["cnt_right"]) == 1) { cblist.Items[3].Selected = true; }
                if (Convert.ToInt32(reader["cnt_left"]) == 1) { cblist.Items[4].Selected = true; }
                if (Convert.ToInt32(reader["cnt_front"]) == 1) { cblist.Items[5].Selected = true; }
                if (Convert.ToInt32(reader["cnt_plantfond"]) == 1) { cblist.Items[6].Selected = true; }
                if (Convert.ToInt32(reader["cnt_floor"] )== 1) { cblist.Items[7].Selected = true; }
                if (Convert.ToInt32(reader["cnt_result"]) == 1) { rbtn_resultOK.Checked = true; } else { rbtn_resultNO.Checked = true; }
                txt_remark.Text = reader["cnt_remark"].ToString();
            }
            reader.Close();
        }
    }
    protected void bnt_check_Click(object sender, EventArgs e)
    {
        bool bottom = false, inside = false, outside = false, right = false, left = false, front = false, plantfond = false, floor = false;
        foreach(ListItem item in cblist.Items )
        {
            if(item.Selected)
            {
                if (item.Value == "cnt_bottom")
                {
                    bottom = true;
                }
                else if (item.Value == "cnt_inside")
                {
                    inside = true;
                }
                else if (item.Value == "cnt_outside")
                {
                    outside = true;
                }
                else if (item.Value == "cnt_right")
                {
                    right = true;
                }
                else if (item.Value == "cnt_left")
                {
                    left = true;
                }
                else if (item.Value == "cnt_front")
                {
                    front = true;
                }
                else if (item.Value == "cnt_plantfond")
                {
                    plantfond = true;
                }
                else 
                {
                    floor = true;
                }
            }
        }
        bool result = false;
        if(rbtn_resultOK.Checked)
        {
            result = true;
        }

        if(rbtn_resultOK.Checked==false && rbtn_resultNO.Checked==false)
        {
            ltlAlert.Text = "alert('检查结果必须填写！')";
            return;
        }

        string remark = txt_remark.Text.Trim();
        string sql = "sp_CNT_insertcntchk";
        SqlParameter[] param = new SqlParameter[15];
        param[0]=new SqlParameter("@cnt_id",Request.Form["txt_cntID"].Trim());
        param[1]=new SqlParameter("@cnt_entrydate",Request.Form["txt_entryDate"].Trim());
        param[2]=new SqlParameter("@bottom",bottom);
        param[3]=new SqlParameter("@inside",inside);
        param[4]=new SqlParameter("@outside",outside);
        param[5]=new SqlParameter("@right",right);
        param[6]=new SqlParameter("@left",left);
        param[7]=new SqlParameter("@front",front);
        param[8]=new SqlParameter("@plantfond",plantfond);
        param[9]=new SqlParameter("@floor",floor);
        param[10]=new SqlParameter("@result",result);
        param[11]=new SqlParameter("@remark",remark);
        param[12] =new SqlParameter("@reValue",SqlDbType.Int);
        param[12].Direction=ParameterDirection.Output;
        param[13] = new SqlParameter("@checkBy",Session["uID"]);
        param[14] = new SqlParameter("@checkDate", DateTime.Now.ToString());
        SqlHelper.ExecuteNonQuery(chk.dsn0(), CommandType.StoredProcedure, sql, param);

        int re = Convert.ToInt32(param[12].Value);

        if(re==0)
        {
            ltlAlert.Text = "alert('检查记录添加失败！')";
        }
        else { ltlAlert.Text = "alert('检查记录添加成功！')"; }

    }
    protected void bnt_back_Click(object sender, EventArgs e)
    {
        string centID = Request.Form["txt_cntID"].Trim();
        string entryDate = Request.Form["txt_entryDate"].Trim();
        Response.Redirect("CNT_EntryLeaveList.aspx?centID=" + centID + "&entryDate=" + entryDate);
    }
}