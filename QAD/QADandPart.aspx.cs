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
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web.Mail;
using System.Web.UI.WebControls.Expressions;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using CommClass;
using TCPCHINA.ODBCHelper;
using System.ComponentModel;
using Microsoft.ApplicationBlocks.Data;
using System.Data.Odbc;

public partial class QAD_QADandPart : System.Web.UI.Page
{
    String strConn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.Conn9"];
    String Conn = System.Configuration.ConfigurationManager.AppSettings["SqlConn.QAD_DATA"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindQADandPart();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtQAD.Text == string.Empty)
        {
            ltlAlert.Text = "alert('QAD不能为空！');";
            return;
        }
        BindQADandPart();
    }
    private void BindQADandPart()
    {
        OdbcDataReader dt = selectQADandPart();
        gv.DataSource = dt;
        gv.DataBind();
    }
    private OdbcDataReader selectQADandPart()
    {
        //string sql = "SELECT pt_domain,pt_part,pt_ship_wt,pt_net_wt,pt_size,pt_vend from PUB.pt_mstr where pt_domain = '" + ddlPlantCode.SelectedValue.ToString() + "' and pt_part = '" + txtQAD.Text + "' with(nolock)";
        string sql = "SELECT pt_domain,pt_part,cast(pt_ship_wt as decimal(18,6)) pt_ship_wt,cast(pt_net_wt as decimal(18,6)) pt_net_wt,cast(pt_size as decimal(18,2)) pt_size,pt_vend from PUB.pt_mstr where pt_domain = '" + ddlPlantCode.SelectedValue.ToString() + "' and pt_part = '" + txtQAD.Text + "' with(nolock)";

        return OdbcHelper.ExecuteReader(strConn, CommandType.Text, sql);
        //return SqlHelper.ExecuteDataset(conn, CommandType.Text, sql).Tables[0];
    }

    protected void gv_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
    {
        gv.EditIndex = -1;
        BindQADandPart();
    }
    protected void gv_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindQADandPart();
    }
    protected void gv_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
    {
        string ptshipwt = ((TextBox)gv.Rows[e.RowIndex].Cells[3].FindControl("txtptshipwt")).Text.ToString().Trim();
        string ptsize = ((TextBox)gv.Rows[e.RowIndex].Cells[4].FindControl("txtptsize")).Text.ToString().Trim();
        string ptnetwt = ((TextBox)gv.Rows[e.RowIndex].Cells[5].FindControl("txtptnetwt")).Text.ToString().Trim();
        string plantcode = gv.DataKeys[e.RowIndex].Values[0].ToString();
        string qad = gv.DataKeys[e.RowIndex].Values[1].ToString();

		if (ptshipwt.Length >= 8)
		{
        ptshipwt = ptshipwt.Substring(0, 8);
		}
	    if (ptsize.Length >= 8)
		{
        ptsize = ptsize.Substring(0, 8);
		}
		if (ptnetwt.Length >= 8)
		{
        ptnetwt = ptnetwt.Substring(0, 8);
		}

        string ptshipwtlog = gv.DataKeys[e.RowIndex].Values[2].ToString();
        string ptsizelog = gv.DataKeys[e.RowIndex].Values[3].ToString();
        string ptnetwtlog = gv.DataKeys[e.RowIndex].Values[4].ToString();


        //写日志
        if (insertLog(plantcode, qad, ptshipwtlog, ptsizelog, ptnetwtlog))
        {
            ltlAlert.Text = "alert('更新日志失败，请联系管理员！');";
            return;
        }
        else
        {
            if (updateUserAppProcess(ptshipwt, ptsize, ptnetwt, qad, plantcode))
            {
                gv.EditIndex = -1;
                BindQADandPart();
            }
            else
            {
                ltlAlert.Text = "alert('更新失败！');";
                return;
            }
        }
    }
    private bool insertLog(string plantcode, string qad, string ptshipwtlog, string ptsizelog, string ptnetwtlog)
    {
        string str = "Insert Into ptsize_hist(ptsize_domain,ptsize_part,ptsize_ship_wt,ptsize_net_wt ,ptsize_size,ptsize_modifiedby ,ptsize_modifyname,ptsize_modifytime) ";
        str += "Values('";
        str += plantcode;
        str += "','" + qad;
        str += "','" + ptshipwtlog;
        str += "','" + ptnetwtlog;
        str += "','" + ptsizelog;
        str += "','" + Session["uID"].ToString() + "',N'" + Session["uName"].ToString() +"',getdate())";
        //SqlParameter[] param = new SqlParameter[10];
        //param[0] = new SqlParameter("@plantcode",plantcode);
        //param[0] = new SqlParameter("@qad",qad);
        //param[0] = new SqlParameter("@ptshipwtlog", ptshipwtlog);
        //param[0] = new SqlParameter("@ptsizelog", ptsizelog);
        //param[0] = new SqlParameter("@ptnetwtlog", ptnetwtlog);

        //param[0] = new SqlParameter("@uID", Session["uID"].ToString());
        //param[0] = new SqlParameter("@uName", Session["uName"].ToString());

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(Conn, CommandType.Text, str));
    }
    private bool updateUserAppProcess(string ptshipwt, string ptsize, string ptnetwt, string qad, string plantcode)
    {
        string sql = "update pub.pt_mstr set pt_ship_wt = '" + ptshipwt + "',pt_net_wt = '"
                + ptsize + "',pt_size = '" + ptnetwt + "' where pt_domain = '" + plantcode + "' and pt_part = '"
                + qad + "'";

        return Convert.ToBoolean(OdbcHelper.ExecuteNonQuery(strConn, CommandType.Text, sql));
    }
}


