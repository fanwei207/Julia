using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using adamFuncs;
using System.IO;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Security.Principal;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text;
using WHInfo;

public partial class WH_account_mstr : BasePage
{
    WareHouse WH = new WareHouse();
    private static adamClass adm = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlType.DataSource = GetWhType();
            ddlType.DataBind();

            if (Request.QueryString["nbr"] != null)
            {
                txtNbr.Text = Request.QueryString["nbr"].ToString();
                txtCrtDate1.Text = Request.QueryString["date1"].ToString();
                txtCrtDate2.Text = Request.QueryString["date2"].ToString();
                ddlType.SelectedIndex = Convert.ToInt32(Request.QueryString["type"].ToString());
                ddlprocess.SelectedIndex = Convert.ToInt32(Request.QueryString["process"].ToString());
            }
        }
        BindGridView();
    }
    public void BindGridView()
    {
        string Process = ddlprocess.SelectedValue.ToString();
        if (Process == "3")
        {
            Process = "-1";
        }
        gv.DataSource = GetWhView(txtNbr.Text.Trim(), txtCrtDate1.Text.Trim(), txtCrtDate2.Text.Trim(), ddlType.SelectedItem.Text.Trim(), Process, Session["PlantCode"].ToString());
        gv.DataBind();

        #region 决定gv列名
        if (ddlType.SelectedValue.ToLower() == "rct-po")
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
        else if (ddlType.SelectedValue.ToLower() == "iss-wo")
        {
            this.gv.Columns[2].HeaderText = "工单号";
            this.gv.Columns[3].HeaderText = "ID号";
            this.gv.Columns[6].Visible = false;
            this.gv.Columns[7].HeaderText = "申请时间";
            this.gv.Columns[9].HeaderText = "成本中心";
            this.gv.Columns[10].HeaderText = "生产线";
        }
        else if (ddlType.SelectedValue.ToLower() == "rct-wo")
        {
            this.gv.Columns[2].HeaderText = "工单号";
            this.gv.Columns[3].HeaderText = "ID号";
            this.gv.Columns[7].HeaderText = "入库时间";
            this.gv.Columns[9].HeaderText = "成本中心";
            this.gv.Columns[10].HeaderText = "生产线";
        }
        else if (ddlType.SelectedValue.ToLower() == "iss-so")
        {
            this.gv.Columns[2].HeaderText = "出运单";
            this.gv.Columns[3].HeaderText = "PK号";
            this.gv.Columns[7].HeaderText = "出运时间";
            this.gv.Columns[9].HeaderText = "发货至";
            this.gv.Columns[10].HeaderText = "发货方式";
            this.gv.Columns[11].HeaderText = "装箱地点";
        }
        else if (ddlType.SelectedValue == "UNP-ISS")
        {
            this.gv.Columns[2].HeaderText = "单据号";
            this.gv.Columns[3].HeaderText = "领用者";
            this.gv.Columns[4].Visible = true;
            this.gv.Columns[4].HeaderText = "领用部门";
            this.gv.Columns[5].HeaderText = "领用事由";
            this.gv.Columns[6].HeaderText = "申请日期";
            this.gv.Columns[6].ItemStyle.Width = 30;
            this.gv.Columns[7].Visible = false;
            this.gv.Columns[9].Visible = false;
            this.gv.Columns[10].Visible = false;
            this.gv.Columns[11].Visible = false;
        }
        else if (ddlType.SelectedValue == "UNP-RCT")
        {
            this.gv.Columns[2].HeaderText = "单据号";
            this.gv.Columns[3].HeaderText = "领用者";
            this.gv.Columns[4].Visible = true;
            this.gv.Columns[4].HeaderText = "领用部门";
            this.gv.Columns[5].HeaderText = "领用事由";
            this.gv.Columns[6].HeaderText = "申请日期";
            this.gv.Columns[6].ItemStyle.Width = 30;
            this.gv.Columns[7].Visible = false;
            this.gv.Columns[9].Visible = false;
            this.gv.Columns[10].Visible = false;
            this.gv.Columns[11].Visible = false;
        }
        #endregion
    }
    public DataTable GetWhType()
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
    public static DataTable GetWhView(string nbr, string stardate, string enddata, string type, string process, string plantCode)
    {
        string strName = "sp_wh_selectWhViewAccount";
        SqlParameter[] parm = new SqlParameter[6];
        parm[0] = new SqlParameter("@nbr", nbr);
        parm[1] = new SqlParameter("@stardate", stardate);
        parm[2] = new SqlParameter("@enddata", enddata);
        parm[3] = new SqlParameter("@type", type);
        parm[4] = new SqlParameter("@process", process);
        parm[5] = new SqlParameter("@plantCode", plantCode);
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
            int indexs = e.Row.RowIndex;
            string Nbr = txtNbr.Text.Trim();
            string date1 = txtCrtDate1.Text.Trim();
            string date2 = txtCrtDate2.Text.Trim();
            string type = ddlType.SelectedIndex.ToString();
            string tpyes = gv.DataKeys[indexs].Values["wh_type"].ToString().Trim();

            string process = ddlprocess.SelectedIndex.ToString();
            if (tpyes == "ISS-WO")
            {
                int index = e.Row.RowIndex;
                string nbr = gv.DataKeys[index].Values["wh_nbr"].ToString().Trim();
                string domain = gv.DataKeys[index].Values["wh_domain"].ToString().Trim();
                string lot = gv.DataKeys[index].Values["wh_lot"].ToString().Trim();
                string text = "window.showModalDialog('WH_WO_ISSdetAccount.aspx?nbr=" + nbr + "&domain=" + domain + "&lot=" + lot + "&process=" + process + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 1300px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                text += "window.location.href = 'WH_account_mstr.aspx?nbr=" + Nbr + "&date1=" + date1 + "&date2=" + date2 + "&type=" + type + "&process=" + process + "'";
                e.Row.Attributes.Add("ondblclick", text);
            }
            else if (tpyes == "RCT-WO")
            {
                int index = e.Row.RowIndex;
                string nbr = gv.DataKeys[index].Values["wh_nbr"].ToString().Trim();
                string domain = gv.DataKeys[index].Values["wh_domain"].ToString().Trim();
                string lot = gv.DataKeys[index].Values["wh_lot"].ToString().Trim();
                string text = "window.showModalDialog('WH_WO_RCTdetAccount.aspx?nbr=" + nbr + "&domain=" + domain + "&lot=" + lot + "&process=" + process + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 1300px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                text += "window.location.href = 'WH_account_mstr.aspx?nbr=" + Nbr + "&date1=" + date1 + "&date2=" + date2 + "&type=" + type + "&process=" + process + "'";
                e.Row.Attributes.Add("ondblclick", text);
            }
            else if (tpyes == "ISS-SO")
            {
                int index = e.Row.RowIndex;
                string id = gv.DataKeys[index].Values["wh_id"].ToString().Trim();
                string text = "window.showModalDialog('WH_SIDdetAccount.aspx?id=" + id + "&process=" + process + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 1300px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                text += "window.location.href = 'WH_account_mstr.aspx?nbr=" + Nbr + "&date1=" + date1 + "&date2=" + date2 + "&type=" + type + "&process=" + process + "'";
                e.Row.Attributes.Add("ondblclick", text);
            }
            else if (tpyes == "RCT-PO")
            {
                int index = e.Row.RowIndex;
                string nbr = gv.DataKeys[index].Values["wh_nbr"].ToString().Trim();
                string domain = gv.DataKeys[index].Values["wh_domain"].ToString().Trim();
                string lot = gv.DataKeys[index].Values["wh_lot"].ToString().Trim();
                string text = "window.showModalDialog('WH_PRHdetAccount.aspx?nbr=" + nbr + "&domain=" + domain + "&lot=" + lot + "&process=" + process + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 1300px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                text += "window.location.href = 'WH_account_mstr.aspx?nbr=" + Nbr + "&date1=" + date1 + "&date2=" + date2 + "&type=" + type + "&process=" + process + "'";
                e.Row.Attributes.Add("ondblclick", text);
            }
            else if (tpyes == "UNP-ISS")
            {
                int index = e.Row.RowIndex;
                string nbr = gv.DataKeys[index].Values["wh_nbr"].ToString().Trim();
                string lot = gv.DataKeys[index].Values["wh_lot"].ToString().Trim();
                string domain = "";
                string text = "window.showModalDialog('WH_UNP_ISSdetAccount.aspx?nbr=" + nbr + "&domain=" + domain + "&lot=" + lot + "&process=" + process + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 1200px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                text += "window.location.href = 'WH_account_mstr.aspx?nbr=" + Nbr + "&date1=" + date1 + "&date2=" + date2 + "&type=" + type + "&process=" + process + "'";
                e.Row.Attributes.Add("ondblclick", text);
            }
            else if (tpyes == "UNP-RCT")
            {
                int index = e.Row.RowIndex;
                string nbr = gv.DataKeys[index].Values["wh_nbr"].ToString().Trim();
                string lot = gv.DataKeys[index].Values["wh_lot"].ToString().Trim();
                string domain = "";
                string text = "window.showModalDialog('WH_UNP_RCTdetAccount.aspx?nbr=" + nbr + "&domain=" + domain + "&lot=" + lot + "&process=" + process + "&rt=" + DateTime.Now.ToFileTime().ToString() + "', window, 'dialogHeight: 600px; dialogWidth: 1200px;  edge: Raised; center: Yes; help: no; resizable: Yes; status: no;');";
                text += "window.location.href = 'WH_account_mstr.aspx?nbr=" + Nbr + "&date1=" + date1 + "&date2=" + date2 + "&type=" + type + "&process=" + process + "'";
                e.Row.Attributes.Add("ondblclick", text);
            }
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.btnSearch_Click(this, new EventArgs());
    }
    protected void ddlprocess_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.btnSearch_Click(this, new EventArgs());
    }
}