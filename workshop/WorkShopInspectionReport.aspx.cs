using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

public partial class WorkShopInspectionReport : BasePage
{
    String strConn = ConfigurationSettings.AppSettings["SqlConn.BarCodeSys"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtstartDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-1));
            txtendDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            BindGroups();
        }
    }

    //绑定巡检类别
    private void BindGroups()
    {
        dropType.Items.Clear();
        dropType.Items.Add("--请选择一个巡检的类别--");
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@plantID", Convert.ToString(Session["plantcode"]));
            SqlDataReader reader = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, "sp_selectWsInspectionGroup", param);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dropType.Items.Add(reader["id"].ToString() + "-" + reader["groupName"].ToString());
                }
                reader.Close();
            }
        }
        catch { }

    }

    protected override void BindGridView()
    {
        try
        {
            string strName = "sp_note_selectInspectionReport";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@plantCode", dropDomain.SelectedValue);
            param[1] = new SqlParameter("@startdate", txtstartDate.Text);
            param[2] = new SqlParameter("@enddate", txtendDate.Text);
            param[3] = new SqlParameter("@user", txtUser.Text);
            param[4] = new SqlParameter("@type", dropType.SelectedItem.ToString().Split('-')[0].ToString());

            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param);
            gv.DataSource = ds;
            gv.DataBind();
        }
        catch
        {
            ;
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (!this.IsDate(txtstartDate.Text))
        {
            this.Alert("日期 格式不正确！");
            return;
        }
        if (!this.IsDate(txtendDate.Text))
        {
            this.Alert("日期 格式不正确！");
            return;
        }

        BindGridView();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;

        BindGridView();
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string _id = gv.DataKeys[index].Values["note_id"].ToString();
            this.Redirect("note_det.aspx?id=" + _id + "&rt=" + DateTime.Now.ToFileTime().ToString());
        }
    }
    protected void btn_export_Click(object sender, EventArgs e)
    {
        try
        {
            string strName = "sp_note_selectInspectionReport";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@plantCode", dropDomain.SelectedValue);
            param[1] = new SqlParameter("@startdate", txtstartDate.Text);
            param[2] = new SqlParameter("@enddate", txtendDate.Text);
            param[3] = new SqlParameter("@user", txtUser.Text);
            param[4] = new SqlParameter("@type", dropType.SelectedItem.ToString().Split('-')[0].ToString());

            DataTable table = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];

            string EXTitle = "40^<b>id</b>~^<b>公司</b>~^<b>日期</b>~^70^<b>人员</b>~^50^<b>内容</b>~^";
            this.ExportExcel(EXTitle, table, false);
            //this.ExportExcel(EXTitle, table, true, 0, "id");
        }
        catch
        {
            this.Alert("导出失败！请联系管理员！");
        }

        
    }
}