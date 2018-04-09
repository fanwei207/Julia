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


public partial class m5_new : BasePage
{
    public String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string _nbr = this.GetNo();
            #region 获取ddlMarketing
            this.GetDdlMarketing();
            #endregion
            #region 单号获取
            if (!string.IsNullOrEmpty(_nbr))
            {
                lblNo.Text = _nbr;
            }
            else
            {
                btnDone.Enabled = false;
                this.Alert("获取编号失败!请联系管理员！");
            }
            #endregion
            #region 绑定dropProject
            radProject.Items.Clear();
            radProject.DataSource = this.GetProjects();
            radProject.DataBind();
            #endregion

            #region 绑定Level
            ddlLevel.DataSource = this.GetDDLLevel();
            ddlLevel.DataBind();
            ddlLevel.SelectedValue = "3";
            #endregion

            btnDone.Attributes.Add("onclick", "return confirm('Are you sure to submit?')");
        }
    }

    private DataTable GetDDLLevel()
    {
        try
        {
            string sqlstr = "SELECT soque_degreeName,soque_did FROM dbo.soque_degree";

            return SqlHelper.ExecuteDataset(strConn, CommandType.Text, sqlstr).Tables[0];
        }
        catch
        {
            return null;
        }
    }

    private void GetDdlMarketing()
    {
        try 
        {
            string sqlstr = "sp_m5_GetDdlMarketing";

             SqlDataReader sdr =  SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, sqlstr);
             ddlMarketing.Items.Add(new ListItem("Please choose one marketing", "0"));
             while (sdr.Read())
             {
                 ddlMarketing.Items.Add(new ListItem(sdr["m5mk_name"].ToString(), sdr["m5mk_ID"].ToString()));
             
             }
             sdr.Dispose();
             sdr.Close();
           

        }
        catch
        {
        
        }
    }
    public String GetNo()
    {
        try
        {
            string strName = "sp_m5_selectM5No";

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
    public DataTable GetProjects()
    {
        try
        {
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, "sp_m5_selectProject").Tables[0];
        }
        catch
        {
            return null;
        }

    }

    protected bool UploadDescFile(string strCateFolder, string strDescSaveFileName, string strDescExtension)
    {
        if (fileDesc.PostedFile.FileName != string.Empty)
        {
            if (fileDesc.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('The maximum file upload is 8 MB!');";
                return false;
            }

            Int32 bytes = fileDesc.PostedFile.ContentLength;

            string _logicalPath = Server.MapPath("/TecDocs/ECN/");

            if (fileDesc.PostedFile.ContentLength > 0)
            {
                try
                {
                    fileDesc.PostedFile.SaveAs(Server.MapPath(strCateFolder) + "\\" + strDescSaveFileName+strDescExtension);
                }
                catch
                {
                    ltlAlert.Text = "alert('File upload failed!');";
                    return false;
                }
            }
            else
            {
                this.Alert("File is requied!");
                return false;
            }
        }
        return true;
    }

    protected bool UploadReasonFile(string strCateFolder, string strReasonSaveFileName,string strReasonExtension)
    {
        if (fileReason.PostedFile.FileName != string.Empty)
        {
            if (fileReason.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('The maximum file upload is 8 MB!');";
                return false;
            }
            Int32 bytes = fileReason.PostedFile.ContentLength;
            string _logicalPath = Server.MapPath("/TecDocs/ECN/");
            if (fileReason.PostedFile.ContentLength > 0)
            {
                try
                {
                    fileReason.PostedFile.SaveAs(Server.MapPath(strCateFolder) + "\\" + strReasonSaveFileName + strReasonExtension);
                }
                catch
                {
                    ltlAlert.Text = "alert('File upload failed!');";
                    return false;
                }
            }
            else
            {
                this.Alert("File is requied!");
                return false;
            }
        }
        return true;
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        if (radProject.SelectedIndex < 0)
        {
            this.Alert("Type is requied！");
            return;
        }

        if (string.IsNullOrEmpty(txtDesc.Text))
        {
            this.Alert("Content is requied！");
            return;
        }

        if (string.IsNullOrEmpty(txtReason.Text))
        {
            this.Alert("Reason is requied！");
            return;
        }

        if ("0".Equals(ddlMarketing.SelectedItem.Value))
        {
            this.Alert("Please choose one marketing!");
            return; 
        }

        if (!"06553FC5-982B-4C6A-8A35-83DEC9887C32".Equals(ddlMarketing.SelectedValue.ToString()) && txtModelNo.Text.ToString().Trim().Equals(string.Empty))
        {
            this.Alert("Please enter ModelNo!");
            return; 
        }

        #region  上传附件验证

        string strDescName = "";//文件名
        string strCateFolder = "/TecDocs/ECN/";
        string strDescExtension = "";//文件后缀
        string strDescSaveFileName = "";//储存名

        if (fileDesc.PostedFile.FileName != string.Empty)
        {
            strDescName = Path.GetFileNameWithoutExtension(fileDesc.PostedFile.FileName);
            strDescExtension = Path.GetExtension(fileDesc.PostedFile.FileName);
            strDescSaveFileName = DateTime.Now.ToFileTime().ToString();
            if (!UploadDescFile(strCateFolder, strDescSaveFileName, strDescExtension))
            {
                this.Alert("File upload failed！");
                return;
            }
        }

        string strReasonName = "";//文件名
        string strReasonExtension = "";//文件后缀
        string strReasonSaveFileName = "";//储存名
        if (fileReason.PostedFile.FileName != string.Empty)
        {
            strReasonName = Path.GetFileNameWithoutExtension(fileReason.PostedFile.FileName);
            strReasonExtension = Path.GetExtension(fileReason.PostedFile.FileName);
            strReasonSaveFileName = DateTime.Now.AddTicks(1).ToFileTime().ToString();

            if (!UploadReasonFile(strCateFolder, strReasonSaveFileName, strReasonExtension))
            {
                this.Alert("File upload failed！");
                return;
            }
        }
        #endregion

        try
        {
            string strName = "sp_m5_saveM5Mstr";
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@no", lblNo.Text);
            param[1] = new SqlParameter("@project", radProject.SelectedValue);
            param[2] = new SqlParameter("@desc", txtDesc.Text.Trim());
            param[3] = new SqlParameter("@desc_file", strDescName + strDescExtension);
            param[4] = new SqlParameter("@desc_path", strCateFolder + strDescSaveFileName + strDescExtension);
            param[5] = new SqlParameter("@reason", txtReason.Text.Trim());
            param[6] = new SqlParameter("@reason_file", strReasonName + strReasonExtension);
            param[7] = new SqlParameter("@reason_path", strCateFolder + strReasonSaveFileName + strReasonExtension);
            param[8] = new SqlParameter("@market", ddlMarketing.SelectedItem.Value.Trim());
            param[9] = new SqlParameter("@uID", Session["uID"].ToString());
            param[10] = new SqlParameter("@uName", Session["uName"].ToString());
            param[11] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[11].Direction = ParameterDirection.Output;
            param[12] = new SqlParameter("@level", ddlLevel.SelectedItem.Value);
            param[13] = new SqlParameter("@modelNo", txtModelNo.Text.Trim().ToString());

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[11].Value))
            {
                this.Alert("submit failed！");
            }
            else
            {
                this.ltlAlert.Text = "alert('success！');";
                Response.Redirect("/product/m5_mstr.aspx?no=" + lblNo.Text);
            }
        }
        catch (Exception ex)
        {
            this.Alert("submit failed！");
        }
    }
}