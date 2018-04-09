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
using adamFuncs;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Text;
using System.IO;
using System.Net;
using CommClass;
using System.Text.RegularExpressions;

public partial class ManualPoNew : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
            hidHrdID.Value = "0";
            BindChannel();
            if (Request.QueryString["approve"] == "1")
            {
                hidAppr.Value = "1";
            }
            if (Request.QueryString["hrd_id"] != null)
            {
                hidHrdID.Value = Request.QueryString["hrd_id"].ToString();

                GetManualPoHrd();

                //部分信息不允许修改
                txtPoNbr.ReadOnly = true;
                txtPoNbr.CssClass = "smalltextbox5";

                txtCust.ReadOnly = true;
                txtCust.CssClass = "smalltextbox5";

                btnBack.Visible = true;
                btnSaveLine.Enabled = true;

                BindGridView();
                BindUpload();
            }
            else
            {
                btnApprove.Visible = false;
            }
                  
        }
    }

    protected void BindGridView()
    {
        if (!chkVerify.Checked && hidAppr.Value == "1")
        {
            btnApprove.Visible = true;
            gvlist.Columns[4].Visible = false;
            gvlist.Columns[5].Visible = true;
        }
        else
        {
            btnApprove.Visible = false;
            gvlist.Columns[4].Visible = true;
            gvlist.Columns[5].Visible = false;
        }
        SqlParameter[] param = new SqlParameter[2];

        param[0] = new SqlParameter("@hrd_id", hidHrdID.Value);
        param[1] = new SqlParameter("@approve", hidAppr.Value);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectManualPoDet", param);

        gvlist.DataSource = ds;
        gvlist.DataBind();
    }

    private void BindUpload()
    {

        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Id", hidHrdID.Value);

        DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectManualPoDocs", param);

        gvUpload.DataSource = ds;
        gvUpload.DataBind();
    }

    protected string CheckManualPoHrd()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@hrd_id", hidHrdID.Value);
            param[1] = new SqlParameter("@cust", txtCust.Text.Trim());
            param[2] = new SqlParameter("@nbr", txtPoNbr.Text.Trim());
            param[3] = new SqlParameter("@shipto", dropShipTo.SelectedValue);
            param[4] = new SqlParameter("@retValue", SqlDbType.VarChar, 100);
            param[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_checkManualPoHrd", param);

            return param[4].Value.ToString();
        }
        catch
        {
            return "Operation fails!Please try again!";
        }
    }

    protected string CheckManualPoDet()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@hrd_id", hidHrdID.Value);
            param[1] = new SqlParameter("@cust", txtCust.Text.Trim());
            param[2] = new SqlParameter("@line", txtDetLine.Text.Trim());
            param[3] = new SqlParameter("@part", txtDetCustPart.Text.Trim());
            param[4] = new SqlParameter("@retValue", SqlDbType.VarChar, 100);
            param[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_checkManualPoDet", param);

            return param[4].Value.ToString();
        }
        catch
        {
            return "Operation fails!Please try again!";
        }
    }

    protected void GetManualPoHrd()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@hrd_id", hidHrdID.Value);

            DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectManualPoHrd", param);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtPoNbr.Text = ds.Tables[0].Rows[0]["mpo_nbr"].ToString();
                txtCust.Text = ds.Tables[0].Rows[0]["mpo_cust"].ToString();
                ckbSample.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["mpo_isSample"].ToString());
                ckbType.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["mpo_type"].ToString());
                ckbSample.Enabled = false;
                ckbType.Enabled = false;
                ddlCurr.SelectedValue = ds.Tables[0].Rows[0]["mpo_curr"].ToString();


                

                if (string.Empty.Equals(ds.Tables[0].Rows[0]["mpo_saleDomain"].ToString()))
                {
                    if (Session["plantCode"].ToString() == "11" || (Session["plantCode"].ToString() == "1" && Session["deptID"].ToString() == "1372"))
                    {
                        ddlSodomain.SelectedValue = "YQL";
                    }
                }
                else
                {
                    ddlSodomain.SelectedValue = ds.Tables[0].Rows[0]["mpo_saleDomain"].ToString();
                }
                Session["plantCode"].ToString();

                
                BindShipTo();
                dropShipTo.SelectedIndex = -1;
                try
                {
                    dropShipTo.Items.FindByValue(ds.Tables[0].Rows[0]["mpo_shipto"].ToString()).Selected = true;
                }
                catch
                {
                    dropShipTo.SelectedIndex = -1;
                }
                BindChannel();
                dropChannel.SelectedIndex = -1;
                try
                {
                    dropChannel.Items.FindByValue(ds.Tables[0].Rows[0]["mpo_channel"].ToString()).Selected = true;
                }
                catch
                {
                    dropChannel.SelectedIndex = -1;
                }

                txtShipVia.Text = ds.Tables[0].Rows[0]["mpo_shipvia"].ToString();
                txtReqDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[0]["mpo_req_date"]));
                txtDueDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[0]["mpo_due_date"]));
                txtCreatedDate.Text = string.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(ds.Tables[0].Rows[0]["mpo_createdDate"]));
                chkSubmit.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["mpo_flg_sub"]);
                if (chkSubmit.Checked)
                {
                    btnSaveHrd.Enabled = false;
                }
                if (ds.Tables[0].Rows[0]["mpo_isVerify"].ToString().ToLower() == "true")
                {
                    chkVerify.Checked = true;
                    trUpload.Visible = false;
                    btnApprove.Visible = false;
                }
                if (!chkVerify.Checked && hidAppr.Value == "1")
                {
                    btnApprove.Visible = true;
                    gvlist.Columns[4].Visible = false;
                    gvlist.Columns[5].Visible = true;
                }
                else
                {
                    btnApprove.Visible = false;
                    gvlist.Columns[4].Visible = true;
                    gvlist.Columns[5].Visible = false;
                }
                if (ckbSample.Checked == true && (txtCust.Text.Trim() == "R1900001" || txtCust.Text.Trim() == "R1900002"))
                {
                    txtDetPrice.Enabled = false;
                    txtDetPrice.Text = "0";
                }
            }
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails while getting po!');";
            BindGridView();
        }
    }
    protected void gvlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (chkSubmit.Checked)
            {
                if (!Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["mpod_isAppended"]))
                {
                    e.Row.Cells[14].Text = string.Empty;
                    e.Row.Cells[15].Text = string.Empty;
                   
                }
            }
            if (Convert.ToBoolean(gvlist.DataKeys[e.Row.RowIndex].Values["mpod_IsSample"]))
            {
               // ((CheckBox)e.Row.FindControl("ckbSample")).Checked = true;
                //((Label)e.Row.FindControl("lblSample")).Text = "true";
            }
            else
            {
                //((CheckBox)e.Row.FindControl("ckbSample")).Checked = false;
               // ((Label)e.Row.FindControl("lblSample")).Text = "false";
            }
            try
            {
                if (ckbSample.Checked == true && (txtCust.Text.Trim() == "R1900001" || txtCust.Text.Trim() == "R1900002"))
                {
                    ((TextBox)e.Row.FindControl("txPrice")).Text = "0";
                    ((TextBox)e.Row.FindControl("txPrice")).Enabled = false;
                }
            }
            catch (Exception)
            {
                
            }
            
        }
    }

    protected void gvlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        if (btnApprove.Visible)
        {
            UpdateChkQty();
        }
        gvlist.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    protected void gvlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@id", gvlist.DataKeys[e.RowIndex].Values["mpod_id"].ToString());
            param[1] = new SqlParameter("@uID", Session["uID"].ToString());
            param[2] = new SqlParameter("@uName", Session["uName"].ToString());

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_deleteManualPoDet", param);
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails while deleting!Please try again!');";
            BindGridView();
        }

        BindGridView();
    }

    protected void btnSaveHrd_Click(object sender, EventArgs e)
    {
        string type = "0";
        if (txtPoNbr.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('Cust Po can not be empty!');";
            return;
        }
        else if (txtPoNbr.Text.Trim().Length < 2)
        {
            ltlAlert.Text = "alert('Cust Po must be at least 2 characters long !');";
            return;
        }

        if (txtCust.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('Cust Code can not be empty!');";
            return;
        }

        if (dropShipTo.SelectedItem.Text == "----")
        {
            ltlAlert.Text = "alert('Ship To can not be empty!');";
            return;
        }

        if (txtShipVia.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('Ship Via can not be empty!');";
            return;
        }

        if (txtReqDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtReqDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Req Date format is incorrect!');";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('Req Date can not be empty!');";
            return;
        }

        if (txtDueDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtDueDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Due Date format is incorrect!');";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('Due Date can not be empty!');";
            return;
        }
        if (txtCust.Text.Trim().Substring(0,3)=="R19")
        {
            if (txtPoNbr.Text.Trim().Length != 10)
            {
                ltlAlert.Text = "alert('The length of this PoNbr must be 10');";
                return;
            }
        }
        if (ckbType.Checked)
        {
            type = "1";
            if (txtPoNbr.Text.Trim().Substring(0, 2).ToLower() != "cs")
            {
                ltlAlert.Text = "alert('PoNbr must be CS');";
                return;
            }
        }
        string strMsg = CheckManualPoHrd();
        if (strMsg.Length == 0)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[15];
                param[0] = new SqlParameter("@nbr", txtPoNbr.Text.Trim());
                param[1] = new SqlParameter("@cust", txtCust.Text.Trim());
                param[2] = new SqlParameter("@shipTo", dropShipTo.SelectedValue);
                param[3] = new SqlParameter("@shipVia", txtShipVia.Text.Trim());
                param[4] = new SqlParameter("@reqDate", txtReqDate.Text.Trim());
                param[5] = new SqlParameter("@dueDate", txtDueDate.Text.Trim());
                param[6] = new SqlParameter("@createdBy", Session["uID"].ToString());
                param[7] = new SqlParameter("@createdName", Session["uName"].ToString());
                param[8] = new SqlParameter("@plantCode", Session["plantCode"].ToString());
                param[9] = new SqlParameter("@channel", dropChannel.SelectedValue);
                param[10] = new SqlParameter("@retValue", SqlDbType.Int);
                param[10].Direction = ParameterDirection.Output;
                param[11] = new SqlParameter("@isSample", ckbSample.Checked);
                param[12] = new SqlParameter("@type", type);
                param[13] = new SqlParameter("@curr", ddlCurr.SelectedValue);
                param[14] = new SqlParameter("@sodomain", ddlSodomain.SelectedValue);
                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_insertManualPoHrd", param);

                if (Convert.ToInt32(param[10].Value) > 0)
                {
                    hidHrdID.Value = param[10].Value.ToString();

                    txtDetReqDate.Text = txtReqDate.Text.Trim();
                    txtDetDueDate.Text = txtDueDate.Text.Trim();

                    btnSaveLine.Enabled = true;

                    BindGridView();
                }
                else
                {
                    ltlAlert.Text = "alert('Operation fails!Please try again!');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('Operation fails!Please try again!');";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('" + strMsg + "!');";
        }
    }
    protected void btnSaveLine_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hidHrdID.Value) == 0)
        {
            ltlAlert.Text = "alert('You must get a po first!');";
            BindGridView();
            return;
        }

        if (txtDetLine.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('Line can not be empty!');";
            BindGridView();
            return;
        }
        else
        {
            try
            {
                int _line = Convert.ToInt32(txtDetLine.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Line must be numeric!');";
                BindGridView();
                return;
            }
        }

        if (txtDetCustPart.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('Cust Part can not be empty!');";
            BindGridView();
            return;
        }

        if (txtDetQty.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('Qty can not be empty!');";
            BindGridView();
            return;
        }
        else
        {
            try
            {
                int _qty = Convert.ToInt32(txtDetQty.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Qty must be numeric!');";
                BindGridView();
                return;
            }
        }

        if (txtDetPrice.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('Price can not be empty!');";
            BindGridView();
            return;
        }
        else
        {
            try
            {
                decimal _price = Convert.ToDecimal(txtDetPrice.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Price must be numeric!');";
                BindGridView();
                return;
            }
        }

        if (ckbSample.Checked)
        {
            if (txtCust.Text.Trim().StartsWith("R19"))
            {
                if (Convert.ToDecimal(txtDetPrice.Text.Trim()) != 0)
                {
                    ltlAlert.Text = "alert('sample Price must be 0!');";
                    BindGridView();
                    return;
                }
            }
        }

        if (txtDetReqDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtDetReqDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Req Date format is incorrect!');";
                BindGridView();
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('Req Date can not be empty!');";
            BindGridView();
            return;
        }

        if (txtDetDueDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txtDetDueDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Due Date format is incorrect!');";
                BindGridView();
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('Due Date can not be empty!');";
            BindGridView();
            return;
        }

        if (txtDetDesc.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('Description can not be empty!');";
            BindGridView();
            return;
        }

        string strMsg = CheckManualPoDet();

        if (strMsg.Length == 0)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[17];
                param[0] = new SqlParameter("@hrd_id", hidHrdID.Value);
                param[1] = new SqlParameter("@cust", txtCust.Text.Trim());
                param[2] = new SqlParameter("@line", txtDetLine.Text.Trim());
                param[3] = new SqlParameter("@part", txtDetCustPart.Text.Trim());
                param[4] = new SqlParameter("@qad", txtDetQad.Text.Trim());
                param[5] = new SqlParameter("@qty", txtDetQty.Text.Trim());
                param[6] = new SqlParameter("@um", txtDetUm.Text.Trim());
                param[7] = new SqlParameter("@price", txtDetPrice.Text.Trim());
                param[8] = new SqlParameter("@reqDate", txtDetReqDate.Text.Trim());
                param[9] = new SqlParameter("@dueDate", txtDetDueDate.Text.Trim());
                param[10] = new SqlParameter("@rmks", txtDetRemarks.Text.Trim());
                param[11] = new SqlParameter("@createdName", Session["uName"]);
                param[12] = new SqlParameter("@createdBy", Session["uID"]);
                param[13] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[13].Direction = ParameterDirection.Output;
                param[14] = new SqlParameter("@SKU", txtSKU.Text.Trim());
                param[15] = new SqlParameter("@IsSample", ckbSample.Checked);
                param[16] = new SqlParameter("@desc", txtDetDesc.Text.Trim());

                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_insertManualPoDet", param);

                if (Convert.ToBoolean(param[13].Value))
                {
                    //追加行：邮件通知
                    if (chkSubmit.Checked)
                    {
                        this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), "chenkaifeng@" + baseDomain.Domain[0], "", "订单追加了新行", "订单号：" + txtPoNbr.Text + "<br />行：" + txtDetLine.Text);
                    }

                    txtDetLine.Text = string.Empty;
                    txtDetCustPart.Text = string.Empty;
                    txtDetQad.Text = string.Empty;
                    txtDetQty.Text = string.Empty;
                    txtDetPrice.Text = string.Empty;
                    txtDetRemarks.Text = string.Empty;
                    txtSKU.Text = string.Empty;
                    trUpload.Visible = true;
                    //GetManualPoHrd();
                   
                }
                else
                {
                    ltlAlert.Text = "alert('Operation fails!Please try again!');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('Operation fails!Please try again!');";
                return;
            }

            BindGridView();
            BindUpload();
        }
        else
        {
            ltlAlert.Text = "alert('" + strMsg + "!');";
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManualPoHrd.aspx?rm=" + DateTime.Now.ToString());
    }
    protected void gvlist_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvlist.EditIndex = e.NewEditIndex;
        BindGridView();
    }
    protected void gvlist_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvlist.EditIndex = -1;
        BindGridView();
    }
    protected void gvlist_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string id = gvlist.DataKeys[e.RowIndex].Values["mpod_id"].ToString();
        TextBox txQty = hidAppr.Value == "1" ? (TextBox)gvlist.Rows[e.RowIndex].FindControl("txChkQtyEdit") : (TextBox)gvlist.Rows[e.RowIndex].FindControl("txQty");
        TextBox txPrice = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txPrice");
        TextBox txReqDate = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txReqDate");
        TextBox txDueDate = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txDueDate");
        TextBox txDesc = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txDesc");
        TextBox txRemarks = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txRemarks");
        TextBox txSKU = (TextBox)gvlist.Rows[e.RowIndex].FindControl("txSKU");
        CheckBox ckbSample = (CheckBox)gvlist.Rows[e.RowIndex].FindControl("ckbSample");
        if (txQty.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('Qty can not be empty!');";
            return;
        }
        else
        {
            try
            {
                int _qty = Convert.ToInt32(txQty.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Qty must be numeric!');";
                return;
            }
        }

        if (txPrice.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('Price can not be empty!');";
            return;
        }
        else
        {
            try
            {
                decimal _price = Convert.ToDecimal(txPrice.Text.Trim());
                if (_price < 0)
                {
                    ltlAlert.Text = "alert('Price must be more then 0!');";
                    return;
                }
            }
            catch
            {
                ltlAlert.Text = "alert('Price must be numeric!');";
                return;
            }
        }
        

        if (txReqDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txReqDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Req Date format is incorrect!');";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('Req Date can not be empty!');";
            return;     
        }

        if (txDueDate.Text.Trim().Length > 0)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(txDueDate.Text.Trim());
            }
            catch
            {
                ltlAlert.Text = "alert('Due Date format is incorrect!');";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('Due Date can not be empty!');";
            return;
        }

        if (txDesc.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('Description can not be empty!');";
            BindGridView();
            return;
        }

        try
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@qty", txQty.Text.Trim());
            param[2] = new SqlParameter("@price", txPrice.Text.Trim().Replace(",",""));
            param[3] = new SqlParameter("@reqDate", txReqDate.Text.Trim());
            param[4] = new SqlParameter("@dueDate", txDueDate.Text.Trim());
            param[5] = new SqlParameter("@rmks", txRemarks.Text.Trim());
            param[6] = new SqlParameter("@modifiedName", Session["uName"]);
            param[7] = new SqlParameter("@modifiedBy", Session["uID"]);
            param[8] = new SqlParameter("@retValue", SqlDbType.Int);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@SKU", txSKU.Text.Trim());
            param[10] = new SqlParameter("@sample", ckbSample.Checked);
            param[11] = new SqlParameter("@desc", txDesc.Text.Trim());
            param[12] = new SqlParameter("@approve", hidAppr.Value);

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateManualPoDet", param);
            int result = Convert.ToInt32(param[8].Value);
            if (result == 0)
            {
                ltlAlert.Text = "alert('Operation fails!Please try again!');";
                return;
            }
            else if (result==-1)
            {
                ltlAlert.Text = "alert('The qty is not consistent!Please enter again!');";
                return;
            }

            gvlist.EditIndex = -1;
            BindGridView();
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails!Please try again!');";
            return;
        }
    }
    protected void txtCust_TextChanged(object sender, EventArgs e)
    {
        //dropShipTo
        BindShipTo();
    }

    protected void BindShipTo()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@cust_code", txtCust.Text.Trim());
            param[1] = new SqlParameter("@plantCode", Session["PlantCode"].ToString());
            DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectShipTo", param);

            dropShipTo.DataSource = ds;
            dropShipTo.DataBind();
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails!Please try again!');";
            return;
        }
    
    }

    protected void BindChannel()
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@plantCode", Session["PlantCode"].ToString());

            DataSet ds = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_selectChannel", param);

            dropChannel.DataSource = ds;
            dropChannel.DataBind();
        }
        catch
        {
            ltlAlert.Text = "alert('Operation fails!Please try again!');";
            return;
        }

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(hidHrdID.Value) == 0)
        {
            ltlAlert.Text = "alert('You must get a po first!');";
            return;
        }
        else
        {
            if (fileAttachFile.FileName == null)
            {
                this.Alert("Please upload a file");
                return;
            }
            if (Path.GetFileName(fileAttachFile.FileName).IndexOf("#") > 0)
            {
                this.Alert("The file name contains illegal characters:#");
                return;
            }

            if (Path.GetFileName(fileAttachFile.FileName).IndexOf("%") > 0)
            {
                this.Alert("The file name contains illegal characters:%");
                return;
            }

            if (fileAttachFile.ContentLength > 1024 * 1024 * 100)
            {
                this.Alert("The file is larger than 100M, can not be uploaded！");
                return;
            }
        }

        //定义参数
        string _uID = Convert.ToString(Session["uID"]);
        string _uName = Convert.ToString(Session["eName"]) == "" ? Convert.ToString(Session["uName"]) : Convert.ToString(Session["eName"]);
        StringBuilder sb = new StringBuilder();

        string _fileName = Path.GetFileName(fileAttachFile.FileName);//文件名，包含后缀
        string _fileExtension = Path.GetExtension(fileAttachFile.FileName);//文件后缀
        string _targetFolder = Server.MapPath("/TecDocs/Po/");

        string _newFileName = DateTime.Now.ToFileTime().ToString() + _fileExtension;//合并两个路径为上传到服务器上的全路径

        try
        {
            if (!Directory.Exists(_targetFolder))
            {
                Directory.CreateDirectory(_targetFolder);
            }

            this.fileAttachFile.MoveTo(_targetFolder + "/" + _newFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);

            SqlParameter[] sqlParam = new SqlParameter[5];
            sqlParam[0] = new SqlParameter("@HrdId", hidHrdID.Value);
            sqlParam[1] = new SqlParameter("@Name", _fileName);
            sqlParam[2] = new SqlParameter("@PhysicalName", _newFileName);
            sqlParam[3] = new SqlParameter("@Uploader", _uName);
            sqlParam[4] = new SqlParameter("@Uid", _uID);

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_insertManualPoDocs", sqlParam);
            BindUpload();
        }
        catch
        {
            this.Alert("Failed to upload！");
            return;
        }
        BindGridView();
    }

    protected void gvUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //定义参数
        if (e.CommandName.ToString() == "View")
        {
            int intRow = Convert.ToInt32(e.CommandArgument.ToString());
            string strPath = gvUpload.DataKeys[intRow].Values["Path"].ToString().Trim();
            string strPhysicalName = gvUpload.DataKeys[intRow].Values["PhysicalName"].ToString();
            string fileName = gvUpload.DataKeys[intRow].Values["Name"].ToString();
            ltlAlert.Text = "var w=window.open('" + strPath + strPhysicalName + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

        }
    }

    protected void gvUpload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Visible = trUpload.Visible;
            if (gvUpload.DataKeys[e.Row.RowIndex].Values["CreatedBy"].ToString() != Session["uID"].ToString())
            {
                btnDelete.Visible = false;
            }
        }
    }

    protected void gvUpload_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //定义参数
        string strDocID = gvUpload.DataKeys[e.RowIndex].Values["DocId"].ToString();
        string strPhysicalName = gvUpload.DataKeys[e.RowIndex].Values["PhysicalName"].ToString();
        SqlParameter[] sqlParam = new SqlParameter[5];
        sqlParam[0] = new SqlParameter("@docId", strDocID);
        SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_deleteManualPoDocs", sqlParam);
        try
        {
            File.Delete(System.IO.Path.Combine(Server.MapPath("/TecDocs/Po/"), strPhysicalName));
        }
        catch
        {
            ;
        }

        BindUpload();
        BindGridView();
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        UpdateChkQty();
        string id = hidHrdID.Value;
        if (CheeckManualPoDocsExists(id))
        {
            int result = ModifyManualPoIsVerify(id);
            if (result == -1)
            {
                this.Alert("Verify and create must by different person!");
            }
            else if (result > 0)
            {
                this.Alert("The qty of line " + result.ToString() + " is not consistent!");
            }
            else if (result == 0)
            {
                GetManualPoHrd();
                BindGridView();
            }
            else
            {
                this.Alert("alert('Operation fails!Please try again!');");
            }
        }
        else
        {
            this.Alert("The order does not have documnet!");
        }
    }

    private bool CheeckManualPoDocsExists(string id)
    {
        try
        {
            string strName = "sp_edi_checkManualPoDocsExists";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);
            return Convert.ToBoolean(SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strName, param));

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    protected int ModifyManualPoIsVerify(string id)
    {
        try
        {
            string strName = "sp_edi_ModifyManualPoIsVerify";
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@reValue", SqlDbType.Int);
            param[1].Direction = ParameterDirection.Output;
            param[2] = new SqlParameter("@Uid", Session["uID"].ToString());
            param[3] = new SqlParameter("@Uname", Session["uName"].ToString());
            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, strName, param);
            return Convert.ToInt32(param[1].Value);
        }
        catch (Exception ex)
        {
            return -2;
        }
    }

    private void UpdateChkQty()
    {
        foreach (GridViewRow row in gvlist.Rows)
        {
            string id = gvlist.DataKeys[row.RowIndex].Values["mpod_id"].ToString();
            TextBox txQty = (TextBox)row.FindControl("txChkQty");
            if (txQty.Text.Trim().Length == 0)
            {
                break;
            }
            else
            {
                int _qty=0;
                if(!int.TryParse(txQty.Text.Trim(),out _qty))
                {
                    break;
                }
            }
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@qty", txQty.Text.Trim());
                param[2] = new SqlParameter("@modifiedName", Session["uName"]);
                param[3] = new SqlParameter("@modifiedBy", Session["uID"]);
                param[4] = new SqlParameter("@retValue", SqlDbType.Int);
                param[4].Direction = ParameterDirection.Output;
                param[5] = new SqlParameter("@approve", hidAppr.Value);
                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn_edi"), CommandType.StoredProcedure, "sp_edi_updateManualPoDet", param);
            }
            catch
            {
                
            }
        }
    }
}
