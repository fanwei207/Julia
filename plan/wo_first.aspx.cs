using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.IO;
using adamFuncs;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CommClass;
using System.Drawing;

public partial class plan_wo_first : System.Web.UI.Page
{
    static string strConn = ConfigurationManager.AppSettings["SqlConn.BarCodeSys"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string domain = Request.QueryString["domain"];
            string nbr = Request.QueryString["nbr"];
            string lot = Request.QueryString["lot"];
            string routing = Request.QueryString["routing"];

            #region 根据工艺，决定类别
            if (routing == string.Empty)
            {
                ddlprocess.SelectedIndex = -1;
                ddlprocess.Items.FindByText("组装").Selected = true;
            }
            else if (routing.Substring(0, 1) == "1" && routing.Substring(routing.Length - 1, 1) == "B")
            {
                try
                {
                    ddlprocess.SelectedIndex = -1;
                    ddlprocess.Items.FindByText("包装").Selected = true;
                }
                catch
                {
                    ;
                }
            }
            else if (routing.Substring(0, 1) == "1")
            {
                try
                {
                    ddlprocess.SelectedIndex = -1;
                    ddlprocess.Items.FindByText("组装").Selected = true;
                }
                catch
                {
                    ;
                }
            }
            else
            {
                try
                {
                    ddlprocess.SelectedIndex = -1;
                    ddlprocess.Items.FindByText("线路板").Selected = true;
                }
                catch
                {
                    ;
                }
            }
            #endregion

            BindData(domain, nbr, lot, routing);

            txtul.Text = GetWoMstrULInfo("", nbr, lot, "");
        }

    }

    private void BindData(string domain, string nbr, string lot, string routing)
    {
        txtDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
        txtname.Text = Session["uName"].ToString();
        txtNbr.Text = nbr;
        string boml = "";
        DataTable dtm = Getfirstmstr(domain, nbr, lot);
        foreach (DataRow row in dtm.Rows)
        {
            txtqty.Text = row["wo_qty_ord"].ToString();
            txtPartname.Text = row["pt_desc"].ToString();
            txtsample.Text = row["wof_sample"].ToString();
            ddlprocess.SelectedValue = row["wof_process"].ToString();
            txtnum.Text = row["wof_num"].ToString();
            if (row["wof_createName"].ToString() != string.Empty)
            {
                txtname.Text = row["wof_createName"].ToString();
                txtDate.Text = row["wof_createDate"].ToString();
            }
            boml = row["wo_bom_code"].ToString();
            if (ddlprocess.SelectedItem.Text == "组装")
            {
                routing = "1";
            }
            else if (ddlprocess.SelectedItem.Text == "包装")
            {
                routing = "1B";
            }
            else
            {
                routing = "2101";
            }
        }
        gvlist.DataSource = Getfirstdet(domain, nbr, lot, boml, routing);
        gvlist.DataBind();

    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("wo_wofirsttracklist.aspx?rt=" + DateTime.Now.ToFileTime().ToString());
    }
    public DataTable Getfirstmstr(string domain, string nbr, string lot)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@domain", domain);
            param[1] = new SqlParameter("@nbr", nbr);
            param[2] = new SqlParameter("@lot", lot);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_first_selectfirstmstr", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable Getfirstdet(string domain, string nbr, string lot, string bom, string routing)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@domain", domain);
            param[1] = new SqlParameter("@nbr", nbr);
            param[2] = new SqlParameter("@lot", lot);
            param[3] = new SqlParameter("@bom", bom);
            param[4] = new SqlParameter("@routing", routing);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_first_selectfirstdet", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    public bool updatefirstmstr(string domain, string nbr, string lot, string sample, string process, string num, string createBy, string createName, string createDate, string complete)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@domain", domain);
            param[1] = new SqlParameter("@nbr", nbr);
            param[2] = new SqlParameter("@lot", lot);
            param[3] = new SqlParameter("@sample", sample);
            param[4] = new SqlParameter("@process", process);
            param[5] = new SqlParameter("@num", num);
            param[6] = new SqlParameter("@createBy", createBy);
            param[7] = new SqlParameter("@createName", createName);
            param[8] = new SqlParameter("@createDate", createDate);
            param[9] = new SqlParameter("@complete", complete);
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_first_updatefirstmstr", param);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool updatefirstdet(string domain, string nbr, string lot, string part, string addr, string num, string confirm, string remark, string his)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@domain", domain);
            param[1] = new SqlParameter("@nbr", nbr);
            param[2] = new SqlParameter("@lot", lot);
            param[3] = new SqlParameter("@part", part);
            param[4] = new SqlParameter("@addr", addr);
            param[5] = new SqlParameter("@num", num);
            param[6] = new SqlParameter("@confirm", confirm);
            param[7] = new SqlParameter("@remark", remark);
            param[8] = new SqlParameter("@his", his);
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_first_updatefirstdet", param);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public DataTable Getfirstaddr(string part)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@part", part);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_first_selectfirstaddr", param).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indexs = e.Row.RowIndex;
            string part = gvlist.DataKeys[indexs].Values["part"].ToString().Trim();

            DropDownList ddladd = (DropDownList)e.Row.Cells[3].FindControl("ddladdr");
            DataTable dt = Getfirstaddr(part);
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ddladd.Items.Add(new ListItem(row["ad_name"].ToString(), row["ad_addr"].ToString()));
                }
            }
            if (gvlist.DataKeys[indexs].Values["ps__log01"].ToString().Trim() == "1")
            {
                e.Row.Cells[0].BackColor = Color.Red;
            }
            ddladd.Items.Insert(0, new ListItem("--", "--"));

            if (gvlist.DataKeys[indexs].Values["wofd_addr"].ToString().Trim() != string.Empty)
            {
                ddladd.SelectedValue = gvlist.DataKeys[indexs].Values["wofd_addr"].ToString().Trim();
            }

            TextBox txtnumd = (TextBox)e.Row.Cells[4].FindControl("txtnum");
            TextBox txtremarks = (TextBox)e.Row.Cells[5].FindControl("txtremark");
            CheckBox chk_Selects = (CheckBox)e.Row.Cells[6].FindControl("chk_Select");
            txtnumd.Text = gvlist.DataKeys[indexs].Values["wofd_num"].ToString().Trim();
            txtremarks.Text = gvlist.DataKeys[indexs].Values["wofd_remark"].ToString().Trim();
            if (gvlist.DataKeys[indexs].Values["wofd_confirm"].ToString().Trim() != string.Empty)
            {
                if (Convert.ToBoolean(gvlist.DataKeys[indexs].Values["wofd_confirm"].ToString().Trim()))
                {
                    chk_Selects.Checked = true;
                }
            }    

            e.Row.Cells[1].Text = gvlist.DataKeys[indexs].Values["ps_level"].ToString().Trim() + gvlist.DataKeys[indexs].Values["part"].ToString().Trim();
        }
    }
    protected void updateall(string type)
    {
        int com;
        int con = 1;
        try
        {
            if (txtsample.Text != string.Empty)
            {
                int samplelp = Convert.ToInt32(txtsample.Text);
            }
        }
        catch (Exception)
        {
            ltlAlert.Text = "alert('样品必须是数字！');";
            return;

        }
        string domain = Request.QueryString["domain"];
        string nbr = Request.QueryString["nbr"];
        string lot = Request.QueryString["lot"];
        string routing = Request.QueryString["routing"];
        string sample = txtsample.Text;
        string process = ddlprocess.SelectedValue;
        string num = txtnum.Text;
        string createBy = Session["uID"].ToString();
        string createName = Session["uName"].ToString();
        string createDate = txtDate.Text;
        string complete = type;
        if (updatefirstmstr(domain, nbr, lot, sample, process, num, createBy, createName, createDate, complete))
        {
            com = 1;
        }
        else
        {
            com = 0;
        }

        for (int i = 0; i < this.gvlist.Rows.Count; i++)
        {
            DropDownList ddladd = (DropDownList)this.gvlist.Rows[i].Cells[3].FindControl("ddladdr");
            TextBox txtnumd = (TextBox)this.gvlist.Rows[i].Cells[4].FindControl("txtnum");
            TextBox txtremarks = (TextBox)this.gvlist.Rows[i].Cells[5].FindControl("txtremark");
            CheckBox chk_Selects = (CheckBox)this.gvlist.Rows[i].Cells[6].FindControl("chk_Select");

            string part = gvlist.DataKeys[i].Values["part"].ToString();
            string addr = ddladd.SelectedValue;
            string nums = txtnumd.Text;
            string confirm = "";
            if (chk_Selects.Checked)
            {
                confirm = "true";
            }
            else
            {
                confirm = "false";
            }
            string remark = txtremarks.Text;
            if (!updatefirstdet(domain, nbr, lot, part, addr, nums, confirm, remark, "1"))
            {
                con = 0;
            }
        }

        if (com == 1 && con == 1)
        {
            ltlAlert.Text = "alert('保存成功！');";
        }
        else
        {
            ltlAlert.Text = "alert('保存失败！');";
        }
        BindData(domain, nbr, lot, routing);
    }
    protected void updateDet(int i)
    {
        try
        {
            if (txtsample.Text != string.Empty)
            {
                int samplelp = Convert.ToInt32(txtsample.Text);
            }
        }
        catch (Exception)
        {
            ltlAlert.Text = "alert('样品必须是数字！');";
            return;

        }
        string domain = Request.QueryString["domain"];
        string nbr = Request.QueryString["nbr"];
        string lot = Request.QueryString["lot"];
        string sample = txtsample.Text;
        string process = ddlprocess.SelectedValue;
        string num = txtnum.Text;
        string createBy = Session["uID"].ToString();
        string createName = Session["uName"].ToString();
        string createDate = txtDate.Text;
        string complete = "3";
        updatefirstmstr(domain, nbr, lot, sample, process, num, createBy, createName, createDate, complete);




        DropDownList ddladd = (DropDownList)this.gvlist.Rows[i].Cells[3].FindControl("ddladdr");
        TextBox txtnumd = (TextBox)this.gvlist.Rows[i].Cells[4].FindControl("txtnum");
        TextBox txtremarks = (TextBox)this.gvlist.Rows[i].Cells[5].FindControl("txtremark");
        CheckBox chk_Selects = (CheckBox)this.gvlist.Rows[i].Cells[6].FindControl("chk_Select");

        string part = gvlist.DataKeys[i].Values["part"].ToString();
        string addr = ddladd.SelectedValue;
        string nums = txtnumd.Text;
        string confirm = "";
        if (chk_Selects.Checked)
        {
            confirm = "true";
        }
        else
        {
            confirm = "false";
        }
        string remark = txtremarks.Text;
        updatefirstdet(domain, nbr, lot, part, addr, nums, confirm, remark, "0");

    }
    protected void btn_pass_Click(object sender, EventArgs e)
    {
        updateall("1");
    }
    protected void btn_cannel_Click(object sender, EventArgs e)
    {
        updateall("2");
    }
    protected void ddladdr_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = ((sender as DropDownList).Parent.Parent as GridViewRow).RowIndex;
        updateDet(index);
    }
    protected void txtnum_TextChanged(object sender, EventArgs e)
    {
        int index = ((sender as TextBox).Parent.Parent as GridViewRow).RowIndex;
        updateDet(index);
    }
    protected void txtremark_TextChanged(object sender, EventArgs e)
    {
        int index = ((sender as TextBox).Parent.Parent as GridViewRow).RowIndex;
        updateDet(index);
    }
    protected void chk_Select_CheckedChanged(object sender, EventArgs e)
    {
        int index = ((sender as CheckBox).Parent.Parent as GridViewRow).RowIndex;
        updateDet(index);
    }
    protected void ddlprocess_SelectedIndexChanged(object sender, EventArgs e)
    {
        string domain = Request.QueryString["domain"];
        string nbr = Request.QueryString["nbr"];
        string lot = Request.QueryString["lot"];
        string routing = Request.QueryString["routing"];
        if (ddlprocess.SelectedItem.Text == "组装")
        {
            routing = "1";
        }
        else if (ddlprocess.SelectedItem.Text == "包装")
        {
            routing = "1B";
        }
        else
        {
            routing = "2101";
        }
        string boml = "";
        DataTable dtm = Getfirstmstr(domain, nbr, lot);
        foreach (DataRow row in dtm.Rows)
        {
            txtqty.Text = row["wo_qty_ord"].ToString();
            txtPartname.Text = row["pt_desc"].ToString();
            txtsample.Text = row["wof_sample"].ToString();
            // ddlprocess.SelectedValue = row["wof_process"].ToString();
            txtnum.Text = row["wof_num"].ToString();
            if (row["wof_createName"].ToString() != string.Empty)
            {
                txtname.Text = row["wof_createName"].ToString();
                txtDate.Text = row["wof_createDate"].ToString();
            }
            boml = row["wo_part"].ToString();
        }
        gvlist.DataSource = Getfirstdet(domain, nbr, lot, boml, routing);
        gvlist.DataBind();

    }
    protected void gvlist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "doc")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string part = gvlist.DataKeys[index].Values["part"].ToString();
            Response.Redirect("/plan/qad_docviewbyitem.aspx?part=" + part + "&domain=" + Request.QueryString["domain"] + "&nbr=" + Request.QueryString["nbr"] + "&lot=" + Request.QueryString["lot"] + "&routing=" + Request.QueryString["routing"]);
            //ltlAlert.Text = "var w=window.open('/plan/qad_docviewbyitem.aspx?part=" + part + "','docitem','menubar=No,scrollbars = No,resizable=No,width=1000,height=500,top=200,left=300'); w.focus();";
        }
    }

    public string GetWoMstrULInfo(string domain, string nbr, string lot, string bom)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@domain", domain);
            param[1] = new SqlParameter("@nbr", nbr);
            param[2] = new SqlParameter("@lot", lot);
            param[3] = new SqlParameter("@bom", bom);
            param[4] = new SqlParameter("@retValue", SqlDbType.VarChar, 1000);
            param[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, "sp_wo_selectULInfo", param);

            return param[4].Value.ToString();
        }
        catch
        {
            return "";
        }
    }
}