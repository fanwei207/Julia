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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;
using CommClass;
using System.Drawing;
using WHInfo;
public partial class WH_mstr : BasePage
{
    WareHouse WH = new WareHouse();
    private static adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["nbr"] != null)
            {
                txtNbr.Text = Request.QueryString["nbr"].ToString();
                txtCrtDate1.Text = Request.QueryString["date1"].ToString();
                txtCrtDate2.Text = Request.QueryString["date2"].ToString();
                ddltype.SelectedIndex = Convert.ToInt32(Request.QueryString["type"].ToString());
            }
            else
            {
                txtCrtDate1.Text = String.IsNullOrEmpty(txtCrtDate1.Text) ? DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") : null;
                txtCrtDate2.Text = String.IsNullOrEmpty(txtCrtDate2.Text) ? DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") : null;
            }
            
            ddltype.DataSource = selectwhType();
            ddltype.DataBind();
            //ddltype.Items.Insert(0,"--");
        }

        BindGridView();
    }
    public void BindGridView()
    {
        DataTable dt = GetWhView(txtNbr.Text.Trim(), txtCrtDate1.Text.Trim(), txtCrtDate2.Text.Trim(), ddltype.SelectedItem.Text.Trim(), cb_type.Checked);
        gv.DataSource = dt ;    
        gv.DataBind();

        int plantcode = Convert.ToInt32(Session["plantcode"]);
        DataTable dt1 = WH.GetUnfinished(ddltype.SelectedItem.Text.Trim(), plantcode);
        if(dt1.Rows.Count > 0)
        {
            lt_unfinish.Text = "(" + dt1.Rows[0]["amount"].ToString() +"条未处理" + ")";
            lt_unfinish.Visible = true;
        }
        else
        {
            lt_unfinish.Text = "";
            lt_unfinish.Visible= false;
        }
        #region 决定gv列名
        if (ddltype.SelectedValue.ToLower() == "rct-po")
        {
            this.gv.Columns[2].HeaderText = "采购单";
            this.gv.Columns[3].HeaderText = "送货单";
            this.gv.Columns[4].Visible = false;
            this.gv.Columns[5].HeaderText = "名称";
            this.gv.Columns[7].HeaderText = "进厂时间";
            this.gv.Columns[9].HeaderText = "发货至";
            this.gv.Columns[10].Visible = false;
            this.gv.Columns[11].Visible = false;
        }
        else if (ddltype.SelectedValue.ToLower() == "iss-wo")
        {
            this.gv.Columns[2].HeaderText = "工单号";
            this.gv.Columns[3].HeaderText = "ID号";
            this.gv.Columns[6].Visible = false;
            this.gv.Columns[7].HeaderText = "申请时间";
            this.gv.Columns[9].HeaderText = "成本中心";
            this.gv.Columns[10].HeaderText = "生产线";
        }
        else if (ddltype.SelectedValue.ToLower() == "rct-wo")
        {
            this.gv.Columns[2].HeaderText = "工单号";
            this.gv.Columns[3].HeaderText = "ID号";
            this.gv.Columns[7].HeaderText = "入库时间";
            this.gv.Columns[9].HeaderText = "成本中心";
            this.gv.Columns[10].HeaderText = "生产线";
        }
        else if (ddltype.SelectedValue.ToLower() == "iss-so")
        {
            this.gv.Columns[2].HeaderText = "出运单";
            this.gv.Columns[3].HeaderText = "PK号";
            this.gv.Columns[5].HeaderText = "出厂日期";
            this.gv.Columns[6].HeaderText = "出运日期";
            this.gv.Columns[7].HeaderText = "集装箱型";
            this.gv.Columns[9].HeaderText = "发货至";
            this.gv.Columns[10].HeaderText = "发货方式";
            this.gv.Columns[11].HeaderText = "装箱地点";
        }
        else if (ddltype.SelectedValue.ToLower() == "unp-iss")
        {
            this.gv.Columns[2].HeaderText = "单据号";
            this.gv.Columns[3].HeaderText = "领用者";
            this.gv.Columns[4].HeaderText = "领用部门";
            this.gv.Columns[5].HeaderText = "领用原因";
            this.gv.Columns[12].HeaderText = "退回原因";
            this.gv.Columns[7].HeaderText = "申请日期";
            this.gv.Columns[9].Visible = false;
            this.gv.Columns[10].Visible = false;
            this.gv.Columns[11].Visible = false;
        }
        else if (ddltype.SelectedValue.ToLower() == "unp-rct")
        {
            this.gv.Columns[2].HeaderText = "单据号";
            this.gv.Columns[3].HeaderText = "领用者";
            this.gv.Columns[4].HeaderText = "领用部门";
            this.gv.Columns[5].HeaderText = "领用原因";
            this.gv.Columns[12].HeaderText = "退回原因";
            this.gv.Columns[7].HeaderText = "申请日期";
            this.gv.Columns[9].Visible = false;
            this.gv.Columns[10].Visible = false;
            this.gv.Columns[11].Visible = false;
        }
        #endregion
    }
    public DataTable selectwhType()
    {
        try
        {
            string strSql = "sp_wh_selectType";
            return SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strSql).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    public DataTable GetWhView(string nbr, string stardate, string enddata, string type, bool backtype)
    {
        string strName = "sp_wh_selectWhView";
        
        SqlParameter[] parm = new SqlParameter[6];
        parm[0] = new SqlParameter("@nbr", nbr);
        parm[1] = new SqlParameter("@stardate", stardate);
        parm[2] = new SqlParameter("@enddata", enddata);
        parm[3] = new SqlParameter("@type", type);
        parm[4] = new SqlParameter("@plantCode", Session["PlantCode"].ToString());
        parm[5] = new SqlParameter("@backtype", backtype);

        DataSet _dataset = SqlHelper.ExecuteDataset(adm.dsn0(), CommandType.StoredProcedure, strName, parm);
        return _dataset.Tables[0];
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }
    protected void gv_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Nbr = txtNbr.Text.Trim();
            string date1 = txtCrtDate1.Text.Trim();
            string date2 = txtCrtDate2.Text.Trim();
            string type = ddltype.SelectedIndex.ToString();
            //if (gv.DataKeys[e.Row.RowIndex].Values["wh_process"].ToString() == "-1")
            //{
            //    e.Row.BackColor = Color.Yellow;
            //}
            if (ddltype.SelectedItem.Text.Trim() == "ISS-WO")
            {
                int index = e.Row.RowIndex;
                string nbr = gv.DataKeys[index].Values["wh_nbr"].ToString().Trim();
                string domain = gv.DataKeys[index].Values["wh_domain"].ToString().Trim();
                string lot = gv.DataKeys[index].Values["wh_lot"].ToString().Trim();
                string text = "window.showModalDialog('WH_WO_ISSdet.aspx?nbr=" + nbr + "&domain=" + domain + "&lot=" + lot + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 1300px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                text += "window.location.href = 'WH_mstr.aspx?nbr=" + Nbr + "&date1=" + date1 + "&date2=" + date2 + "&type=" + type + "'";
                e.Row.Attributes.Add("ondblclick", text);
            }
            else if (ddltype.SelectedItem.Text.Trim() == "RCT-WO")
            {
                int index = e.Row.RowIndex;
                string nbr = gv.DataKeys[index].Values["wh_nbr"].ToString().Trim();
                string domain = gv.DataKeys[index].Values["wh_domain"].ToString().Trim();
                string lot = gv.DataKeys[index].Values["wh_lot"].ToString().Trim();
                string text = "window.showModalDialog('WH_WO_RCTdet.aspx?nbr=" + nbr + "&domain=" + domain + "&lot=" + lot + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 1300px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                text += "window.location.href = 'WH_mstr.aspx?nbr=" + Nbr + "&date1=" + date1 + "&date2=" + date2 + "&type=" + type + "'";
                e.Row.Attributes.Add("ondblclick", text);
            }
            else if (ddltype.SelectedItem.Text.Trim() == "ISS-SO")
            {
                int index = e.Row.RowIndex;
                string id = gv.DataKeys[index].Values["wh_id"].ToString().Trim();
                string text = "window.showModalDialog('WH_SIDdet.aspx?id=" + id + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 1300px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                text += "window.location.href = 'WH_mstr.aspx?nbr=" + Nbr + "&date1=" + date1 + "&date2=" + date2 + "&type=" + type + "'";
                e.Row.Attributes.Add("ondblclick", text);
            }
            else if (ddltype.SelectedItem.Text.Trim() == "RCT-PO")
            {
                int index = e.Row.RowIndex;
                string nbr = gv.DataKeys[index].Values["wh_nbr"].ToString().Trim();
                string domain = gv.DataKeys[index].Values["wh_domain"].ToString().Trim();
                string lot = gv.DataKeys[index].Values["wh_lot"].ToString().Trim();
                string text = "window.showModalDialog('WH_PRHdet.aspx?nbr=" + nbr + "&domain=" + domain + "&lot=" + lot + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 1300px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                text += "window.location.href = 'WH_mstr.aspx?nbr=" + Nbr + "&date1=" + date1 + "&date2=" + date2 + "&type=" + type + "'";
                e.Row.Attributes.Add("ondblclick", text);
            }
            else if (ddltype.SelectedItem.Text.Trim() == "UNP-ISS")
            {
                int index = e.Row.RowIndex;
                string nbr = gv.DataKeys[index].Values["wh_nbr"].ToString().Trim();
                string lot = gv.DataKeys[index].Values["wh_lot"].ToString().Trim();
                string text = "window.showModalDialog('WH_UNP_ISSdet.aspx?nbr=" + nbr + "&lot=" + lot + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 1200px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                text += "window.location.href = 'WH_mstr.aspx?nbr=" + Nbr + "&date1=" + date1 + "&date2=" + date2 + "&type=" + type + "'";
                e.Row.Attributes.Add("ondblclick", text);
            }
            else if (ddltype.SelectedItem.Text.Trim() == "UNP-RCT")
            {
                int index = e.Row.RowIndex;
                string nbr = gv.DataKeys[index].Values["wh_nbr"].ToString().Trim();
                string lot = gv.DataKeys[index].Values["wh_lot"].ToString().Trim();
                string text = "window.showModalDialog('WH_UNP_RCTdet.aspx?nbr=" + nbr + "&lot=" + lot + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 1200px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                text += "window.location.href = 'WH_mstr.aspx?nbr=" + Nbr + "&date1=" + date1 + "&date2=" + date2 + "&type=" + type + "'";
                e.Row.Attributes.Add("ondblclick", text);
            }
        }
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.btnSearch_Click(this, new EventArgs());
    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {

        }
    }

    protected void gv_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv.EditIndex = e.NewEditIndex;
        BindGridView();

        String ptype = ((Label)gv.Rows[e.NewEditIndex].Cells[12].FindControl("lblptype")).Text.Trim();
        if (ptype == "")
        {
            ptype = "--";
        }
        ((DropDownList)gv.Rows[e.NewEditIndex].Cells[1].FindControl("drpPtype")).Items.FindByText(ptype).Selected = true;

    }
}