using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using adamFuncs;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;


public partial class plan_Po_det : BasePage
{
    private NewWorkflow helper = new NewWorkflow();
    static string strConn = ConfigurationManager.AppSettings["SqlConn.qadplan"];
    public string _pov_nbr
    {
        get
        {
            return ViewState["pov_nbr"].ToString();
        }
        set
        {
            ViewState["pov_nbr"] = value;
        }
    }
    public string _pov_domain
    {
        get
        {
            return ViewState["pov_domain"].ToString();
        }
        set
        {
            ViewState["pov_domain"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _pov_nbr = HttpUtility.UrlDecode(Request.QueryString["pov_nbr"]);
            _pov_domain = HttpUtility.UrlDecode(Request.QueryString["pov_domain"]);
            //_pov_nbr = "HP210879";
            //_pov_domain = "HQL";
            if (Request.QueryString["Node_Id"] == null)
            {
                btnPass.Visible = false;
                btnFail.Visible = false;
            }
            gvDataBand();

        }
    }
    public void gvDataBand()
    {
        setPomstr();
        gvShipdetail.DataSource = getPodet();
        gvShipdetail.DataBind();
    }
    public DataTable getPodet()
    {
        try
        {
            string strName = "sp_pur_selectpodappv";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@pov_nbr", _pov_nbr);
            param[1] = new SqlParameter("@pov_domain", _pov_domain);

           return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
           



        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public void setPomstr()
    {
        try
        {
            string strName = "sp_pur_selectpoappv";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@pov_nbr", _pov_nbr);
            param[1] = new SqlParameter("@pov_domain", _pov_domain);

            SqlDataReader read = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strName, param);
            if (read.Read())
            {
                lblpov_domain.Text = read["pov_domain"].ToString();
                lblpov_due_date.Text = read["pov_due_date"].ToString();
                lblpov_nbr.Text = read["pov_nbr"].ToString();
                lblpov_ord_date.Text = read["pov_ord_date"].ToString();
                lblpov_ship.Text = read["pov_ship"].ToString();
                if (read["pov_TotleCost"].ToString() != "")
                {
                    double TotleCost = Convert.ToDouble(read["pov_TotleCost"].ToString());
                    lblpov_TotleCost.Text = string.Format("{0:#,##0.00000}", TotleCost);
                }
                lblpov_vend.Text = read["pov_vend"].ToString();
            }

            read.Close();
            
        }
        catch (Exception ex)
        {
           
        }
    }

    protected void btnPass_Click(object sender, EventArgs e)
    {
        string nodeId = Request.QueryString["Node_Id"];
            string userId = Session["uID"].ToString();
            DataTable table = GetSelectedData();
            string message = "";
            updatePodetRemark(nodeId, userId, lblpov_nbr.Text.Trim(), txtremark.Text.Trim());
            int result = helper.Pass(nodeId, userId, table, out message);
            if (result == 1)
            {
                //ltlAlert.Text = "alert('审批成功！');";
                ltlAlert.Text = "alert('审批成功！');$('body', $('body', parent.document).find('#ifrm_120000013').contents()).find('#btnSearch').click();$.loading('none');$('BODY', parent.parent.parent.document).find('#j-modal-dialog').remove();";
            }
            else if (result == -1)
            {
                ltlAlert.Text = "alert('" + message + "');";
            }
            else
            {
                ltlAlert.Text = "alert('操作失败,请联系管理员！');";
            }
        
      
    }
    private DataTable GetSelectedData()
    {
        DataTable table = new DataTable("FormData");
        foreach (string key in Request.QueryString.AllKeys)
        {
            if (key != "Node_Id")
            {
                DataColumn column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = key;
                table.Columns.Add(column);
            }
        }
            DataRow row = table.NewRow();
            foreach (string key in Request.QueryString.AllKeys)
            {
                if (key != "Node_Id")
                {
                    row[key] = Request.QueryString[key];
                }
            }
            table.Rows.Add(row);
        return table;
    }
    protected void btnFail_Click(object sender, EventArgs e)
    {
        string nodeId = Request.QueryString["Node_Id"];
        string userId = Session["uID"].ToString();
        DataTable table = GetSelectedData();
        string message = "";
        updatePodetRemark(nodeId, userId, lblpov_nbr.Text.Trim(), txtremark.Text.Trim());
        int result = helper.Fail(nodeId, userId, table, out message);
        if (result==1)
        {
            //ltlAlert.Text = "alert('拒绝成功！');";
            ltlAlert.Text = "alert('拒绝成功！');$('body', $('body', parent.document).find('#ifrm_120000013').contents()).find('#btnSearch').click();$.loading('none');$('BODY', parent.parent.parent.document).find('#j-modal-dialog').remove();";
        }
        else if (result == -1)
        {
            ltlAlert.Text = "alert('" + message + "');";
        }
        else
        {
            ltlAlert.Text = "alert('操作失败,请联系管理员！');";
        }
    }
    public bool updatePodetRemark(string nodeId, string userId, string nbr, string remark)
    {
        

        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@NodeId", nodeId);
        param[1] = new SqlParameter("@UserId", userId);
        param[2] = new SqlParameter("@nbr", nbr);
        param[3] = new SqlParameter("@remark", remark);
        bool bol = Convert.ToBoolean(SqlHelper.ExecuteScalar(ConfigurationSettings.AppSettings["SqlConn.Conn_WF"], CommandType.StoredProcedure, "sp_pur_updatePodetRemark", param));
        return bol;
    }
}