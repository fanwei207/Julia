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
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;

public partial class EdiDetDistribute : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    private void BindGridView()
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@hrdId", Request.QueryString["id"].ToString());

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_getEdiPoDetDistribute", param);

        gvlist.DataSource = ds;
        gvlist.DataBind();
    }

    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {                
            DropDownList dropDomain = (DropDownList)e.Row.FindControl("dropDomain");
            DropDownList dropSite = (DropDownList)e.Row.FindControl("dropSite");
            HtmlInputHidden hSite = (HtmlInputHidden)e.Row.FindControl("hSite");
            hSite.Value = gvlist.DataKeys[e.Row.RowIndex].Values["site"].ToString();

            string _domain = gvlist.DataKeys[e.Row.RowIndex].Values["domain"].ToString();

            if (string.IsNullOrEmpty(_domain))
            {
                _domain = Request.QueryString["domain"];
            }

            #region 更新参数domain重新绑定地点，只显示该域下面的地点
            switch (_domain)
            {
                case "SZX":
                    dropSite.Items.Clear();
                    dropSite.Items.Add(new ListItem("--", "0"));
                    dropSite.Items.Add(new ListItem("1000", "1000"));
                    dropSite.Items.Add(new ListItem("1200", "1200"));
                    dropSite.Items.Add(new ListItem("1400", "1400"));
                    dropSite.Items.Add(new ListItem("1500", "1500"));
                    break;
                case "ZQL":
                    dropSite.Items.Clear();
                    dropSite.Items.Clear();
                    dropSite.Items.Add(new ListItem("--", "0"));
                    dropSite.Items.Add(new ListItem("2000", "2000"));
                    break;
                case "ZQZ":
                    dropSite.Items.Clear();
                    dropSite.Items.Add(new ListItem("--", "0"));
                    dropSite.Items.Add(new ListItem("3000", "3000"));
                    break;
                case "ATL":
                    dropSite.Items.Clear();
                    dropSite.Items.Add(new ListItem("--", "0"));
                    dropSite.Items.Add(new ListItem("9000", "9000"));
                    break;
                default:
                    break;
            }
            #endregion

            try
            {
                dropSite.Items.FindByValue(gvlist.DataKeys[e.Row.RowIndex].Values["site"].ToString()).Selected = true;

                dropDomain.Items.FindByValue(gvlist.DataKeys[e.Row.RowIndex].Values["domain"].ToString()).Selected = true;
            }
            catch
            {
                dropDomain.SelectedIndex = -1;
                dropSite.SelectedIndex = -1;
            }
        }
        else if(e.Row.RowType==DataControlRowType.Header)
        {
            DropDownList drpDomain = (DropDownList)e.Row.FindControl("drpDomain");
            DropDownList drpSite = (DropDownList)e.Row.FindControl("drpSite");
            switch (Request.QueryString["domain"])
            {
                case "SZX":
                    drpDomain.Items.FindByValue(Request.QueryString["domain"]).Selected = true;

                    drpSite.Items.Clear();
                    drpSite.Items.Add(new ListItem("--", "0"));
                    drpSite.Items.Add(new ListItem("1000", "1000"));
                    drpSite.Items.Add(new ListItem("1200", "1200"));
                    drpSite.Items.Add(new ListItem("1400", "1400"));
                    drpSite.Items.Add(new ListItem("1500", "1500"));
                    break;
                case "ZQL":
                    drpDomain.Items.FindByValue(Request.QueryString["domain"]).Selected = true;

                    drpSite.Items.Clear();
                    drpSite.Items.Add(new ListItem("--", "0"));
                    drpSite.Items.Add(new ListItem("2000", "2000"));
                    break;
                case "ZQZ":
                    drpDomain.Items.FindByValue(Request.QueryString["domain"]).Selected = true;

                    drpSite.Items.Clear();
                    drpSite.Items.Add(new ListItem("--", "0"));
                    drpSite.Items.Add(new ListItem("3000", "3000"));
                    break;
                case "YQL":
                    drpDomain.Items.FindByValue(Request.QueryString["domain"]).Selected = true;

                    drpSite.Items.Clear();
                    drpSite.Items.Add(new ListItem("--", "0"));
                    drpSite.Items.Add(new ListItem("4000", "4000"));
                    break;
                case "HQL":
                    drpDomain.Items.FindByValue(Request.QueryString["domain"]).Selected = true;

                    drpSite.Items.Clear();
                    drpSite.Items.Add(new ListItem("--", "0"));
                    drpSite.Items.Add(new ListItem("5000", "5000"));
                    break;
                case "ATL":
                    drpDomain.Items.FindByValue(Request.QueryString["domain"]).Selected = true;

                    drpSite.Items.Clear();
                    drpSite.Items.Add(new ListItem("--", "0"));
                    drpSite.Items.Add(new ListItem("9000", "9000"));
                    break;
                default:
                    break;
            }
        }
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvlist.PageIndex = e.NewPageIndex;

        BindGridView();
    }

    protected void drpSite_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (((DropDownList)sender).SelectedIndex != 0)
        {
            foreach (GridViewRow row in gvlist.Rows)
            {
                HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

                if (chkImport.Checked)
                {
                    ((DropDownList)row.FindControl("dropSite")).SelectedValue = ((DropDownList)sender).SelectedValue;
                }
            }
        }
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkAll = (CheckBox)sender;

        foreach (GridViewRow row in gvlist.Rows)
        {
            HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

            chkImport.Checked = chkAll.Checked;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvlist.Rows)
        {
            string id = gvlist.DataKeys[row.RowIndex].Values["id"].ToString();

            DropDownList dropDomain = (DropDownList)row.FindControl("dropDomain");
            DropDownList dropSite = (DropDownList)row.FindControl("dropSite");
            HtmlInputHidden hSite = (HtmlInputHidden)row.FindControl("hSite");

            if (dropSite.SelectedIndex != 0 && hSite.Value != dropSite.SelectedValue)
            {
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@detid", gvlist.DataKeys[row.RowIndex].Values["id"].ToString());
                param[1] = new SqlParameter("@domain", dropDomain.SelectedValue);
                param[2] = new SqlParameter("@site", dropSite.SelectedValue);

                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateEdiDetSite", param);
            }
        }

        BindGridView();

        ltlAlert.Text = "alert('保存结束！如果地点未变，表示保存成功，否则，请重新保存一次！');";
    }
    protected void dropDomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void drpDomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (((DropDownList)sender).SelectedIndex != 0)
        {
            DropDownList drpSite = (DropDownList)gvlist.HeaderRow.FindControl("drpSite");
            #region 绑定Header中的地点
            switch (((DropDownList)sender).SelectedValue)
            {
                case "SZX":
                    drpSite.Items.Clear();
                    drpSite.Items.Add(new ListItem("--", "0"));
                    drpSite.Items.Add(new ListItem("1000", "1000"));
                    drpSite.Items.Add(new ListItem("1200", "1200"));
                    drpSite.Items.Add(new ListItem("1400", "1400"));
                    drpSite.Items.Add(new ListItem("1500", "1500"));
                    break;
                case "ZQL":
                    drpSite.Items.Clear();
                    drpSite.Items.Add(new ListItem("--", "0"));
                    drpSite.Items.Add(new ListItem("2000", "2000"));
                    break;
                case "ZQZ":
                    drpSite.Items.Clear();
                    drpSite.Items.Add(new ListItem("--", "0"));
                    drpSite.Items.Add(new ListItem("3000", "3000"));
                    break;
                case "YQL":
                    drpSite.Items.Clear();
                    drpSite.Items.Add(new ListItem("--", "0"));
                    drpSite.Items.Add(new ListItem("4000", "4000"));
                    break;
                case "HQL":
                    drpSite.Items.Clear();
                    drpSite.Items.Add(new ListItem("--", "0"));
                    drpSite.Items.Add(new ListItem("5000", "5000"));
                    break;
                case "ATL":
                    drpSite.Items.Clear();
                    drpSite.Items.Add(new ListItem("--", "0"));
                    drpSite.Items.Add(new ListItem("9000", "9000"));
                    break;
                default:
                    break;
            }
            #endregion
            foreach (GridViewRow row in gvlist.Rows)
            {
                HtmlInputCheckBox chkImport = (HtmlInputCheckBox)row.FindControl("chkImport");

                DropDownList dropSite = (DropDownList)row.FindControl("dropSite");
                #region 绑定地点
                switch (((DropDownList)sender).SelectedValue)
                {
                    case "SZX":
                        dropSite.Items.Clear();
                        dropSite.Items.Add(new ListItem("--", "0"));
                        dropSite.Items.Add(new ListItem("1000", "1000"));
                        dropSite.Items.Add(new ListItem("1200", "1200"));
                        dropSite.Items.Add(new ListItem("1400", "1400"));
                        dropSite.Items.Add(new ListItem("1500", "1500"));
                        break;
                    case "ZQL":
                        dropSite.Items.Clear();
                        dropSite.Items.Add(new ListItem("--", "0"));
                        dropSite.Items.Add(new ListItem("2000", "2000"));
                        break;
                    case "ZQZ":
                        dropSite.Items.Clear();
                        dropSite.Items.Add(new ListItem("--", "0"));
                        dropSite.Items.Add(new ListItem("3000", "3000"));
                        break;
                    case "YQL":
                        dropSite.Items.Clear();
                        dropSite.Items.Add(new ListItem("--", "0"));
                        dropSite.Items.Add(new ListItem("4000", "4000"));
                        break;
                    case "HQL":
                        dropSite.Items.Clear();
                        dropSite.Items.Add(new ListItem("--", "0"));
                        dropSite.Items.Add(new ListItem("5000", "5000"));
                        break;
                    case "ATL":
                        dropSite.Items.Clear();
                        dropSite.Items.Add(new ListItem("--", "0"));
                        dropSite.Items.Add(new ListItem("9000", "9000"));
                        break;
                    default:
                        break;
                }
                #endregion
                if (chkImport.Checked)
                {
                    ((DropDownList)row.FindControl("dropDomain")).SelectedValue = ((DropDownList)sender).SelectedValue;
                }
            }
        }
    }
    protected void dropDomain_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (((DropDownList)sender).SelectedIndex != 0)
        {
            int index = (((DropDownList)sender).Parent.Parent as GridViewRow).RowIndex;
            GridViewRow row = gvlist.Rows[index];

            DropDownList dropSite = (DropDownList)row.FindControl("dropSite");
            #region 绑定地点
            switch (((DropDownList)sender).SelectedValue)
            {
                case "SZX":
                    dropSite.Items.Clear();
                    dropSite.Items.Add(new ListItem("--", "0"));
                    dropSite.Items.Add(new ListItem("1000", "1000"));
                    dropSite.Items.Add(new ListItem("1200", "1200"));
                    dropSite.Items.Add(new ListItem("1400", "1400"));
                    dropSite.Items.Add(new ListItem("1500", "1500"));
                    break;
                case "ZQL":
                    dropSite.Items.Clear();
                    dropSite.Items.Add(new ListItem("--", "0"));
                    dropSite.Items.Add(new ListItem("2000", "2000"));
                    break;
                case "ZQZ":
                    dropSite.Items.Clear();
                    dropSite.Items.Add(new ListItem("--", "0"));
                    dropSite.Items.Add(new ListItem("3000", "3000"));
                    break;
                case "YQL":
                    dropSite.Items.Clear();
                    dropSite.Items.Add(new ListItem("--", "0"));
                    dropSite.Items.Add(new ListItem("4000", "4000"));
                    break;
                case "HQL":
                    dropSite.Items.Clear();
                    dropSite.Items.Add(new ListItem("--", "0"));
                    dropSite.Items.Add(new ListItem("5000", "5000"));
                    break;
                case "ATL":
                    dropSite.Items.Clear();
                    dropSite.Items.Add(new ListItem("--", "0"));
                    dropSite.Items.Add(new ListItem("9000", "9000"));
                    break;
                default:
                    break;
            }
            #endregion
        }
    }
}
