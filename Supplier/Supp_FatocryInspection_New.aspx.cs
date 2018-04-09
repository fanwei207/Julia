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

public partial class Supplier_Supp_FatocryInspection_New : BasePage
{
    public String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnDone.Visible = this.Security["591100008"].isValid;
            getType();
            hidsuppno.Value = "";
            if (!string.IsNullOrEmpty(Request.QueryString["suppno"]))
            {
                hidsuppno.Value = Request.QueryString["suppno"].ToString();
                try
                {
                  
                    txtvent.Enabled = false;
                        txtname.Text = Request.QueryString["name"].ToString();
                        txtlevel.Text = Request.QueryString["type"].ToString();
                        txtAddress.Text = Request.QueryString["Address"].ToString();
                        txtTelephone.Text = Request.QueryString["Telephone"].ToString();
                        txtFax.Text = Request.QueryString["Fax"].ToString();
                        lblNo.Text = this.GetNo();
                        bandgv();
                        string strName = "sp_FI_saveFIMstr";
                        SqlParameter[] param = new SqlParameter[22];
                        param[0] = new SqlParameter("@no", lblNo.Text);
                        param[1] = new SqlParameter("@Name", txtname.Text.Trim());
                        param[2] = new SqlParameter("@vent", txtvent.Text.Trim());
                        param[3] = new SqlParameter("@Date", txtStdDate.Text.Trim());
                        param[4] = new SqlParameter("@Service", txtService.Text.Trim());
                        param[5] = new SqlParameter("@Address", txtAddress.Text.Trim());
                        param[6] = new SqlParameter("@LicenseNO", txtLicenseNO.Text.Trim());
                        param[7] = new SqlParameter("@LegalPerson", txtLegalPerson.Text.Trim());
                        param[8] = new SqlParameter("@Business", txtBusiness.Text.Trim());
                        param[9] = new SqlParameter("@Quality", txtQuality.Text.Trim());
                        param[10] = new SqlParameter("@Telephone", txtTelephone.Text.Trim());
                        param[11] = new SqlParameter("@retValue", SqlDbType.Bit);
                        param[11].Direction = ParameterDirection.Output;
                        param[12] = new SqlParameter("@Fax", txtFax.Text.Trim());
                        param[13] = new SqlParameter("@number", txtnumber.Text.Trim());
                        param[14] = new SqlParameter("@Technology", txtTechnology.Text.Trim());
                        param[15] = new SqlParameter("@uID", Session["uID"].ToString());
                        param[16] = new SqlParameter("@uName", Session["uName"].ToString());
                        param[17] = new SqlParameter("@type", radType.SelectedItem.Text.Trim());
                        param[18] = new SqlParameter("@level", txtlevel.Text.Trim());
                        param[19] = new SqlParameter("@departmentID", chkSelect());
                        param[20] = new SqlParameter("@suppid", hidsuppno.Value);

                        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

                        if (!Convert.ToBoolean(param[11].Value))
                        {
                            this.Alert("新建数据失败，请手动填写！");
                        }

                }
                catch (Exception ex)
                {
                    ;
                }

                
            }
            else
            {
                if (string.IsNullOrEmpty(Request.QueryString["FI_id"]))
                {
                    // gv1.Visible = false;
                    lblNo.Text = this.GetNo();
                    bandgv();
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["comm"]))
                    {
                        if (Request.QueryString["comm"] == "1")
                        {
                            btnDone.Visible = true;
                        }
                        else
                        {
                            btnDone.Visible = false;
                        }
                       
                    }
                    hidFIid.Value = Request.QueryString["FI_id"].ToString();
                    gv1.Visible = true;
                    btnback.Visible = true;
                    btnDone.Text = "保存";
                    binddetil(hidFIid.Value);
                    bandgv(hidFIid.Value);
                }
            }
          
           

        }
    }
    public void bandgv(string id)
    {
        gv1.DataSource = getdepartment(id);
        gv1.DataBind();
    }
    public void bandgv()
    {
        gv1.DataSource = getdepartment();
        gv1.DataBind();
    }
    public DataTable getdepartment(string FI_id)
    {
        try
        {
            string strName = "sp_FI_selectFIdepartment";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@fi_id", FI_id);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public void getType()
    {
        try
        {
            string strName = "sp_FI_selectFIType";
           
            radType.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName).Tables[0];
            radType.DataBind();
            radType.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            
        }
    }
    public DataTable getdepartment()
    {
        try
        {
            string strName = "sp_FI_selectFIdepartmentNew";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@type", txtlevel.Text);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public void binddetil(string FI_id)
    {
        try
        {
            string strName = "sp_FI_selectFIMstr";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@fi_id", FI_id);
            DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
            txtname.Text = dt.Rows[0]["FI_Name"].ToString();
            txtAddress.Text = dt.Rows[0]["FI_Address"].ToString();
            txtStdDate.Text = dt.Rows[0]["FI_Date"].ToString();
            txtService.Text = dt.Rows[0]["FI_Service"].ToString();
            txtLicenseNO.Text = dt.Rows[0]["FI_LicenseNO"].ToString();
            txtLegalPerson.Text = dt.Rows[0]["FI_LegalPerson"].ToString();
            txtBusiness.Text = dt.Rows[0]["FI_Business"].ToString();
            txtQuality.Text = dt.Rows[0]["FI_Quality"].ToString();
            txtTelephone.Text = dt.Rows[0]["FI_Telephone"].ToString();
            txtFax.Text = dt.Rows[0]["FI_Fax"].ToString();
            txtnumber.Text = dt.Rows[0]["FI_number"].ToString();
            txtTechnology.Text = dt.Rows[0]["FI_Technology"].ToString();
            txtlevel.Text = dt.Rows[0]["FI_level"].ToString();
            txtvent.Text = dt.Rows[0]["FI_Vent"].ToString();
            lblNo.Text = dt.Rows[0]["FI_NO"].ToString();
            string uID = dt.Rows[0]["FI_createBy"].ToString();
            if (uID != Session["uID"].ToString())
            {
                btnDone.Visible = false;
            }

        }
        catch (Exception ex)
        {

        }
    }
    public String GetNo()
    {
        try
        {
            string strName = "sp_FI_selectFINo";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@retValue", SqlDbType.VarChar, 30);
            param[0].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
            return param[0].Value.ToString();

        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        if (radType.SelectedItem.Text == string.Empty)
        {
            this.Alert("类型不能为空!");
        }
        int number;
        if (txtnumber.Text != string.Empty)
        {
            try
            {
                number = Convert.ToInt32(txtnumber.Text);
            }
            catch (Exception)
            {
                this.Alert("人员人数必须是数字!");
            }
        }
        try
        {
            string strName = "sp_FI_saveFIMstr";
            SqlParameter[] param = new SqlParameter[22];
            param[0] = new SqlParameter("@no", lblNo.Text);
            param[1] = new SqlParameter("@Name", txtname.Text.Trim());
            param[2] = new SqlParameter("@vent", txtvent.Text.Trim());
            param[3] = new SqlParameter("@Date", txtStdDate.Text.Trim());
            param[4] = new SqlParameter("@Service", txtService.Text.Trim());
            param[5] = new SqlParameter("@Address", txtAddress.Text.Trim());
            param[6] = new SqlParameter("@LicenseNO", txtLicenseNO.Text.Trim());
            param[7] = new SqlParameter("@LegalPerson", txtLegalPerson.Text.Trim());
            param[8] = new SqlParameter("@Business", txtBusiness.Text.Trim());
            param[9] = new SqlParameter("@Quality", txtQuality.Text.Trim());
            param[10] = new SqlParameter("@Telephone", txtTelephone.Text.Trim());
            param[11] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[11].Direction = ParameterDirection.Output;
            param[12] = new SqlParameter("@Fax", txtFax.Text.Trim());
            param[13] = new SqlParameter("@number", txtnumber.Text.Trim());
            param[14] = new SqlParameter("@Technology", txtTechnology.Text.Trim());
            param[15] = new SqlParameter("@uID", Session["uID"].ToString());
            param[16] = new SqlParameter("@uName", Session["uName"].ToString());
            param[17] = new SqlParameter("@type", radType.SelectedItem.Text.Trim());
            param[18] = new SqlParameter("@level", txtlevel.Text.Trim());
            param[19] = new SqlParameter("@departmentID", chkSelect());
            param[20] = new SqlParameter("@suppid", hidsuppno.Value);
            
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[11].Value))
            {
                this.Alert("submit failed！");
            }
            else
            {
               
                string strName1 = "sp_FI_SetEmailbegin";
                SqlParameter[] param1 = new SqlParameter[2];
                param1[0] = new SqlParameter("@departmentID", chkSelect());
                param1[1] = new SqlParameter("@no", lblNo.Text);
                DataTable dt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName1, param1).Tables[0];

                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    dt.Rows[i]["email"].ToString();

                    string strapp = "有新的验厂单需要你指派验厂人员<br/> 网址：<a href ="+baseDomain.getPortalWebsite()+"/Supplier/Supp_FatocryInspection_New.aspx?FI_id=" + dt.Rows[i]["Fi_id"].ToString() + "&comm=0> "+baseDomain.getPortalWebsite()+"/Supplier/Supp_FatocryInspection_New.aspx?FI_id=" + dt.Rows[i]["Fi_id"].ToString() + "<a>";
                    this.SendEmail(ConfigurationManager.AppSettings["AdminEmail"].ToString(), dt.Rows[i]["email"].ToString(), "", "有新的验厂单需要你指派验厂人员", strapp);
               
                }
                this.ltlAlert.Text = "alert('success！');";
               

                Response.Redirect("Supp_FactoryInspection_mstr.aspx?no=" + lblNo.Text);

              

            }
        }
        catch (Exception ex)
        {
            this.Alert("Save failed！");
        }

    }
    protected string chkSelect()
    {
        //定义参数
        string strSelect = "";

        //判断是否有选择
        for (int i = 0; i < gv1.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)gv1.Rows[i].FindControl("chk_Select");
            if (cb.Checked)
            {
                strSelect = strSelect + gv1.DataKeys[i].Values["departmentID"].ToString() + ",";
            }
        }
        return strSelect;
    }
    protected void gv1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Reviewer")
        {
            //判断用户是否具备工作流模板删、改的权限

            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string Uid = gv1.DataKeys[index].Values["Uid"].ToString();
            string UName = gv1.DataKeys[index].Values["UName"].ToString();
            if (Session["uID"].ToString() != Uid && Session["uID"].ToString() != "13")
            {
                this.Alert("你没有权限进行此项操作，请联系部门管理员-" + UName);
                return;
            }
            CheckBox cb = (CheckBox)gv1.Rows[index].FindControl("chk_Select");
            if (!cb.Checked)
            {
                this.Alert("此部门未勾选验厂");
                return;
            }
            string departmentID = gv1.DataKeys[index].Values["departmentID"].ToString();
            string plantID = gv1.DataKeys[index].Values["plantID"].ToString();
            Response.Redirect("FI_department_person.aspx?department_id=" + e.CommandArgument.ToString().Trim() + "&FI_id=" + hidFIid.Value + "&departmentID=" + departmentID + "&plantID=" + plantID + "&rm=" + DateTime.Now, true);

        }
    }
    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((CheckBox)e.Row.Cells[0].FindControl("chk_Select")).Enabled = btnDone.Visible;
            int index = e.Row.RowIndex;
            string status = gv1.DataKeys[index].Values["cbkcheck"].ToString().Trim();
            ((CheckBox)e.Row.Cells[0].FindControl("chk_Select")).Checked = Convert.ToBoolean(status);
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Supp_FactoryInspection_mstr.aspx?no=" + lblNo.Text);
    }
}