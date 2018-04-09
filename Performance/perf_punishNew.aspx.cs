using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using IT;
using CommClass;
using adamFuncs;

public partial class Performance_perf_punishNew : BasePage
{

    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dd_type.SelectedIndex = 0;

            ListItem item = new ListItem("--", "-1");

            dd_type.Items.Add(item);

            string StrSql = "SELECT perf_type_id,perf_type From tcpc0.dbo.perf_type order by perf_type_id ";
            DataTable ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql).Tables[0];

            txt_stret.Text = DateTime.Now.ToString("yyyy-MM-dd");
            foreach (DataRow row in ds.Rows)
            {
                item = new ListItem(row["perf_type"].ToString(), row["perf_type_id"].ToString());

                dd_type.Items.Add(item);

            }
            

        }
    }
    protected void dd_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCause();
    }
    public void loadCause()
    {
        string StrSql = "";
        if (txtsearch.Text == string.Empty)
        {
            StrSql = "SELECT perf_defi_id,perf_cause From tcpc0.dbo.perf_definition where perf_type_id ='" + dd_type.SelectedValue.ToString() + "' order by perf_type_id,perf_cause ";
        }
        else
        {
            StrSql = "SELECT perf_defi_id,perf_cause From tcpc0.dbo.perf_definition where perf_type_id ='" + dd_type.SelectedValue.ToString() + "' and perf_cause like N'%" + txtsearch.Text.Trim() + "%' order by perf_type_id,perf_cause ";
        }
        DataTable dt = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql).Tables[0];
        dd_cause.DataSource = dt;
        dd_cause.DataBind();
    }
    protected void txb_no_TextChanged(object sender, EventArgs e)
    {
        string StrSql = "select userid,de.name ,us.userName from tcpc0.dbo.Users us LEFT  JOIN tcpc" + Session["PlantCode"] + ".dbo.Departments de ON us.departmentID = de.departmentID  where roleID<>1 and plantCode = '" + Session["PlantCode"] + "' and isnull(isactive,0)=1 and isnull(deleted,0) =0 and (leaveDate is null or datediff(month,leaveDate,getdate()) <=1 ) and userno='" + txb_no.Text.Trim() + "' ";
        DataTable dt = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql).Tables[0];

        foreach (DataRow row in dt.Rows)
        {

            txt_demp.Text = row["name"].ToString();
            txt_user.Text = row["userName"].ToString();
            lbluid.Text = row["userid"].ToString();


        }

    }
    protected void btn_next_Click(object sender, EventArgs e)
    {
        if (dd_cause.SelectedValue.ToString() == "")
        {
            this.Alert("原因不能为空");
            return;
        }
        if (txt_user.Text == "")
        {
            this.Alert("责任人不能为空");
            return;
        }
        string StrSql = "perf_insert_punish";
        string uno = txb_no.Text.Trim();
        string una = txt_user.Text.Trim();
      
      

        SqlParameter[] param = new SqlParameter[14];
        param[0] = new SqlParameter("@userid", lbluid.Text.Trim());
        param[1] = new SqlParameter("@cause", dd_cause.SelectedItem.Text.Trim());
        param[2] = new SqlParameter("@date", txt_stret.Text.Trim());
        param[4] = new SqlParameter("@createdby", Session["uID"].ToString());
        param[5] = new SqlParameter("@createdname", Session["UName"].ToString());
        // param[6] = new SqlParameter("@mid", userNo);
        param[7] = new SqlParameter("@type", dd_type.SelectedItem.Text.Trim());
        param[8] = new SqlParameter("@duty", dd_duty.SelectedItem.Text.Trim());
        param[9] = new SqlParameter("@dept", txt_demp.Text.Trim());
        param[10] = new SqlParameter("@userno", uno);
        param[11] = new SqlParameter("@uname", una);

        SqlHelper.ExecuteScalar(chk.dsnx(), CommandType.StoredProcedure, StrSql, param);
        this.Alert("保存成功!");
        txt_user.Text = "";
        txt_demp.Text = "";
       
        txb_no.Text = "";
       
    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        this.Redirect("perf_punish.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        loadCause();
    }
    protected void rdl_cause_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}