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
using System.Drawing;
using System.Text;

public partial class Supplier_FI_view : BasePage
{
    public string EffectTR = string.Empty;//第一个标签：部门确认显示的tr
    public string Effectscript = string.Empty;
    public String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnsave.Visible = this.Security["591100007"].isValid;
            hidsuppno.Value = "";
            if (!string.IsNullOrEmpty(Request.QueryString["suppno"]))
            {
                hidsuppno.Value = Request.QueryString["suppno"].ToString();
                try
                {
                    string strName = "sp_FI_checksuppNo";
                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter("@suppno", hidsuppno.Value);
                    DataTable dtt = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName, param).Tables[0];
                    if (dtt.Rows.Count > 0)
                    {
                        lblNo.Text = dtt.Rows[0]["FI_NO"].ToString();
                        hidFIid.Value = dtt.Rows[0]["FI_id"].ToString();
                        binddetil(hidFIid.Value);
                        Boundcheck(hidFIid.Value);
                        BindData(hidFIid.Value);
                    }
                }
                catch (Exception ex)
                {
                    ;
                }
                btnBack.Visible = false;
            }
           
            if (!string.IsNullOrEmpty(Request.QueryString["FI_id"]))
            {
                hidFIid.Value = Request.QueryString["FI_id"].ToString();
                binddetil(hidFIid.Value);
                Boundcheck(hidFIid.Value);
                BindData(hidFIid.Value);
            }
        }
    }
    protected void BindData(string id)
    {
        //定义参数
        DataSet ds = SelectFIfile(id);
        gv.DataSource = ds.Tables[0];
        gv.DataBind();
    }
    public DataSet SelectFIfile(string id)
    {
        try
        {
            string strSql = "sp_FI_Selectfile";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@FI_id", id);
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, sqlParam);
        }
        catch (Exception ex)
        {
            //throw ex;
            return null;
        }
    }
    public void Boundcheck(string FI_id)
    {
        try
        {
            string strName = "sp_FI_selectFIcheck";

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@fi_id", FI_id);
            int i = 0;
            string name;
            string url;
            Effectscript += " <script language=\"JavaScript\" type=\"text/javascript\">";
            Effectscript += " $(function () {";
            SqlDataReader read = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, strName, param);
            while (read.Read())
            {
                i += 1;
                string depart = read["departmentName"].ToString();
                string desc = read["FI_desc"].ToString();
                string use = read["FI_use"].ToString();
                string remark = read["FI_remark"].ToString();
                string createname = read["FI_createname"].ToString();
                string FI_Member = read["FI_Member"].ToString().Replace("<br>", ",");
                string department_id = read["department_id"].ToString();
                name = "cob" + i.ToString();
                EffectTR += " <tr id=\"" + name + "\"><td class=\"FixedGridLeftbody\"></td>";
                EffectTR += "<td colspan =\"1\" rowspan=\"4\"  style=\"text-align:center;\"  >";
                EffectTR += "<p style=\"font-size:15px;font-weight:600;\" >" + depart + "评价</p>";
                EffectTR += "   </td>";
                EffectTR += "  <td colspan =\"7\" style=\"text-align:left; vertical-align:top; height:140px\"> <br/> &nbsp;&nbsp;";
                EffectTR += desc;
                EffectTR += "   </td>";
                EffectTR += "     <td class=\"FixedGridRightbody\"></td>";
                EffectTR += "  </tr>";
                EffectTR += "   <tr>";
                EffectTR += "     <td class=\"FixedGridLeftbody\"></td>";
                EffectTR += "      <td colspan =\"7\" style=\"text-align:left;\">";
                EffectTR += "    结论：是否可以使用     " + use + " ";
                EffectTR += "  </td>";
                EffectTR += "   <td class=\"FixedGridRightbody\"></td>";
                EffectTR += "  </tr>";
                EffectTR += "  <tr>";
                EffectTR += "   <td class=\"FixedGridLeftbody\"></td>";
                EffectTR += "     <td colspan =\"1\" style=\"text-align:left;\">";
                EffectTR += "       备注";
                EffectTR += "      </td>";
                EffectTR += "       <td colspan =\"6\" style=\"text-align:left;\">";
                EffectTR += remark;
                EffectTR += "       </td>";
                EffectTR += "      <td class=\"FixedGridRightbody\"></td>";
                EffectTR += "     </tr>";
                EffectTR += "      <tr>";
                EffectTR += "       <td class=\"FixedGridLeftbody\"></td>";
                EffectTR += "       <td colspan =\"7\" style=\"text-align:left;\">";
                EffectTR += "        评价人     " + createname;
                EffectTR += "      </td>";
                EffectTR += "       <td class=\"FixedGridRightbody\"></td>";
                EffectTR += "    </tr>";
                Effectscript += "  $(\"#" + name + "\").dblclick(function(){ ";
                if (lblstatus.Text == "进行中")
                {
                    string strName1 = "sp_FI_selectFIcheckbyuidDepart";
                    SqlParameter[] param1 = new SqlParameter[3];
                    param1[0] = new SqlParameter("@fi_id", FI_id);
                    param1[1] = new SqlParameter("@uid", Session["uID"].ToString());
                    param1[2] = new SqlParameter("@department_id", department_id);
                    DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strName1, param1);
                    if (ds.Tables.Count == 0)
                    {
                        if (FI_Member == string.Empty || FI_Member == "添加")
                        {
                            Effectscript += " alert(\"请先确认验厂人员" + "\");";
                        }
                        else
                        {
                            Effectscript += " alert(\"请联系验厂人" + FI_Member + "\");";
                            //return false;
                        }
                    }
                    else
                    {
                        url = "/Supplier/FI_check.aspx?FI_id=" + FI_id + "&fi_no=" + Request.QueryString["fi_no"].ToString() + "&rt=" + DateTime.Now.ToFileTime().ToString();
                        Effectscript += " var _src = \"" + url + "\";";
                        Effectscript += " $.window(\"Message\", \"80%\", \"80%\", _src, \"\", true);";
                    }
                }
                else
                {
                    Effectscript += " alert(\"验厂单" + lblstatus.Text + "\");";
                }
                Effectscript += " });";
            }
            read.Close();
            Effectscript += " })";
            Effectscript += "</script>";
        }
        catch (Exception ex)
        {

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
            lblname.Text = dt.Rows[0]["FI_Name"].ToString();
            lblAddress.Text = dt.Rows[0]["FI_Address"].ToString();
            lbldate.Text = dt.Rows[0]["FI_Date"].ToString();
            lblserver.Text = dt.Rows[0]["FI_Service"].ToString();
            lblLicenseNO.Text = dt.Rows[0]["FI_LicenseNO"].ToString();
            lblLegalPerson.Text = dt.Rows[0]["FI_LegalPerson"].ToString();
            lblBusiness.Text = dt.Rows[0]["FI_Business"].ToString();
            lblQuality.Text = dt.Rows[0]["FI_Quality"].ToString();
            lblTelephone.Text = dt.Rows[0]["FI_Telephone"].ToString();
            lblFax.Text = dt.Rows[0]["FI_Fax"].ToString();
            lblnumber.Text = dt.Rows[0]["FI_number"].ToString();
            lblTechnology.Text = dt.Rows[0]["FI_Technology"].ToString();
            lblNo.Text = dt.Rows[0]["FI_NO"].ToString();
            rbtncheck.SelectedValue = dt.Rows[0]["FI_checkuse"].ToString();
            rbtncheckper.SelectedValue = dt.Rows[0]["FI_checkuseperfer"].ToString();
            rbtnfinuse.SelectedValue = dt.Rows[0]["FI_finuse"].ToString();
            lblcheck.Text = dt.Rows[0]["FI_agreename"].ToString();
            string create = dt.Rows[0]["FI_createby"].ToString();
            if (create != Session["uID"].ToString())
            {
                btnsave.Visible = false;
                btnupdate.Visible = false;
            }
            lblstatus.Text = dt.Rows[0]["FI_Status"].ToString();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Supp_FactoryInspection_mstr.aspx?no=" + Request.QueryString["fi_no"].ToString());
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        //if (lblstatus.Text != "进行中")
        //{
        //    this.Alert("验厂单" + lblstatus.Text);
        //    return;
        //}
        if (fileExcute.Value == string.Empty)
        {
            ltlAlert.Text = "alert('请选择上传文件');";
            Boundcheck(hidFIid.Value);
            return;
        }
        //if (chkAgree.Checked)
        //{
        String strFileName = "";//文件名
        //String strCateFolder = "/TecDocs/FI/";
        String strCateFolder = "/TecDocs/ECN/";
       
        String strExtension = "";//文件后缀
        String strSaveFileName = "";//储存名
        if (fileExcute.PostedFile != null)
        {
            #region 上传文档例行处理
            if (fileExcute.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('The maximum file upload is 8 MB');";
                return;
            }
            strFileName = Path.GetFileNameWithoutExtension(fileExcute.PostedFile.FileName);
            strExtension = Path.GetExtension(fileExcute.PostedFile.FileName);
            strSaveFileName = DateTime.Now.ToFileTime().ToString();
            try
            {
                fileExcute.PostedFile.SaveAs(Server.MapPath(strCateFolder) + "\\" + strSaveFileName + strExtension);
            }
            catch
            {
                ltlAlert.Text = "alert('" + Server.MapPath(strCateFolder) + "\\" + strSaveFileName + strExtension + "');";
                //ltlAlert.Text = "alert('Attachment upload failed');";
                return;
            }
            #endregion
        }
        #region 操作数据库
        try
        {
            string strName = "sp_FI_savefile";
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@FI_id", hidFIid.Value);
            param[1] = new SqlParameter("@fileName", strFileName + strExtension);
            param[2] = new SqlParameter("@filePath", strCateFolder + strSaveFileName + strExtension);
            param[3] = new SqlParameter("@uID", Session["uID"].ToString());
            param[4] = new SqlParameter("@uName", Session["uName"].ToString());
            param[5] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[5].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[5].Value))
            {
                this.Alert("fail! Please contact the administrator");
            }
            else
            {
                this.Alert("上传成功");

            }
        }
        catch (Exception ex)
        {
            this.Alert("fail! Please contact the administrator");
        }
        #endregion
        binddetil(hidFIid.Value);
        Boundcheck(hidFIid.Value);
        BindData(hidFIid.Value);

    }
    protected void gv_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "show")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
            string filePath = gv.DataKeys[index].Values["FI_path"].ToString();
            try
            {
                filePath = Server.MapPath(filePath);
            }
            catch (Exception)
            {

                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }

            if (!File.Exists(@filePath))
            {
                ltlAlert.Text = "alert('文件已移除或不存在！')";
                return;
            }
            int i = filePath.IndexOf("TecDocs");
            filePath = filePath.Substring(i - 1);
            filePath = filePath.Replace("\\", "/");
            ltlAlert.Text = "var w=window.open('" + filePath + "','DocView','menubar=No,scrollbars = No,resizable=No,width=800,height=600,top=0,left=0'); w.focus();";

        }
        if (e.CommandName == "delete")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;
           
            string id = gv.DataKeys[index].Values["fileid"].ToString();
            string strName = "sp_FI_deletefile";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
        }
        binddetil(hidFIid.Value);
        Boundcheck(hidFIid.Value);
        BindData(hidFIid.Value);
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int index = e.Row.RowIndex;
            if (btnsave.Visible || lblstatus.Text != "进行中")
            {

                e.Row.Cells[2].Text = string.Empty;

            }
          
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (lblstatus.Text != "进行中")
        {
            this.Alert("验厂单" + lblstatus.Text);
            return;
        }
        string inn = rbtncheck.SelectedValue;
        try
        {
            string strName = "sp_FI_savefinally";
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@FI_id", hidFIid.Value);
            if (rbtncheck.SelectedValue == "")
            {
                param[1] = new SqlParameter("@FI_checkuse", System.DBNull.Value);
            }
            else
            {
                param[1] = new SqlParameter("@FI_checkuse", rbtncheck.SelectedValue);
            }
            if (rbtncheckper.SelectedValue == "")
            {
                param[2] = new SqlParameter("@FI_checkuseperfer", System.DBNull.Value);
            }
            else
            {
                param[2] = new SqlParameter("@FI_checkuseperfer", rbtncheckper.SelectedValue);
            }
            if (rbtnfinuse.SelectedValue == "")
            {
                param[3] = new SqlParameter("@FI_finuse", System.DBNull.Value);
            }
            else
            {
                param[3] = new SqlParameter("@FI_finuse", rbtnfinuse.SelectedValue);
            }
            param[4] = new SqlParameter("@uID", Session["uID"].ToString());
            param[5] = new SqlParameter("@uName", Session["uName"].ToString());
            param[6] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[6].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);
            if (!Convert.ToBoolean(param[6].Value))
            {
                this.Alert("fail! Please contact the administrator");
            }
            else
            {
                this.Alert("保存成功");
            }
        }
        catch (Exception ex)
        {
            this.Alert("fail! Please contact the administrator");
        }
        binddetil(hidFIid.Value);
        Boundcheck(hidFIid.Value);
        BindData(hidFIid.Value);
    }
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}