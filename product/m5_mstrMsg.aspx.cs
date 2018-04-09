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

public partial class product_m5_mstrMsg : BasePage
{


    public String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 
            labNo.Text = Request["no"].ToString();
            txtEffDate.Text = Request["effdate"].ToString();
            labUExcutor.Text = Request["uexcutor"].ToString();
            hidEffDate.Value = Request["effdate"].ToString();

            #region 绑定Level
            ddlLevel.DataSource = this.GetDDLLevel();
            ddlLevel.DataBind();
            #endregion



            this.GetLevelAndModel();
            

           
        }
    }
    private void GetLevelAndModel()
    {
        ddlMarket.DataSource = this.getMarket();
        ddlMarket.DataBind();
        #region 获取Level和modelNo.
        SqlDataReader sdr = this.GetLevelAndModel(labNo.Text);
        while (sdr.Read())
        {
            ddlLevel.SelectedValue = sdr["m5_level"].ToString();
            //hidLevel.Value = sdr["m5_level"].ToString();
            txtModelNo.Text = sdr["m5_modelNumber"].ToString();
           // hidModelNo.Value = sdr["m5_modelNumber"].ToString();
            ddlMarket.SelectedValue = sdr["m5_market"].ToString();
            hidMarket.Value = sdr["m5_market"].ToString();
            chklAboutBoom.SelectedValue = sdr["m5_AboutBoom"].ToString();
        }
        sdr.Close();
        #endregion
            
    }

    private DataTable getMarket()
    {
        try
        {
            string sqlstr = "sp_m5_GetDdlMarketing";

            return  SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr).Tables[0];
        }
        catch
        {
            return null;
        }
    }
    private SqlDataReader GetLevelAndModel(string no)
    {
        try
        {
            string sqlstr = "sp_m5_selectLevelAndModelByNo";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@no",no)
            
            };

            return   SqlHelper.ExecuteReader(strConn,CommandType.StoredProcedure,sqlstr,param);

         
        }
        catch
        {
            return null;
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

    public  bool CheckUser(string userNo, string domain)
    {
        try
        {
            string strName = "sp_m5_checkUser";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@userNo", userNo);
            param[1] = new SqlParameter("@domain", domain);
            param[2] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            return Convert.ToBoolean(param[2].Value);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
       
       
        //if (!hidLevel.Value.Equals(ddlLevel.SelectedValue) || !hidModelNo.Value.Equals(txtModelNo.Text.Trim()) || !hidMarket.Value.Equals(ddlMarket.SelectedValue))
        //{
        //    if (this.SaveLevelAndModel())
        //    {
        //        this.ltlAlert.Text = "alert('Seve Level and Model and Market success!');";
        //        this.GetLevelAndModel();
        //        return;
        //    }
        //    else
        //    {
        //        this.ltlAlert.Text = "alert('Seve Level and Model failed!Please contact the administrator ！');";
        //        return;
        //    }
            
        //}

        this.SaveLevelAndModel();

        //bool msgChange = false;
        if (!string.IsNullOrEmpty(txtEffDate.Text))
        {
            if (!this.IsDate(txtEffDate.Text))
            {
                this.ltlAlert.Text = "alert('Effect Date is not true');";
                return;
            }
           
        }
         //验证人员填空是否为空
        //if (labUExcutor.Text.Equals(string.Empty))
        //{
        //    if (string.IsNullOrEmpty(txtUserNo.Text) || string.IsNullOrEmpty(txtUserDomain.Text) || txtUserNo.Text.Equals("Please Enter Excutor："))
        //    {
        //        this.Alert("Excutor is Empty!Please enter");
        //        return;
        //    }
        //    //else
        //    //{
        //    //    if (!CheckUser(txtUserNo.Text, txtUserDomain.Text))
        //    //    {
        //    //        this.Alert("Excutor is error!Please enter");
        //    //        return;
        //    //    }

        //    //}
        //}
        //if (string.IsNullOrEmpty(txtMsg.Text) )
        //{
        //    ltlAlert.Text = "alert('Message can not Empty');";
        //    return;
        //}

        if (fileAgree.PostedFile.FileName.Length > 0)
        {
            if (txtMsg.Text == string.Empty)
            {
                ltlAlert.Text = "alert('Message can not Empty');";
                return;
            }
          
        }


        //if (!msgChange )
        //{
        //    ltlAlert.Text = "alert('Haven't change,It's need not saving');";
        //    return;
        //}

        String strFileName = "";//文件名
        String strCateFolder = "/TecDocs/ECN/";
        String strExtension = "";//文件后缀
        String strSaveFileName = "";//储存名
        if (fileAgree.PostedFile != null)
        {
            #region 上传文档例行处理
            if (fileAgree.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('Upload the maximum space is 8 MB!');";
                return;
            }

            strFileName = Path.GetFileNameWithoutExtension(fileAgree.PostedFile.FileName);
            strExtension = Path.GetExtension(fileAgree.PostedFile.FileName);
            strSaveFileName = DateTime.Now.ToFileTime().ToString();

            try
            {
                fileAgree.PostedFile.SaveAs(Server.MapPath(strCateFolder) + "\\" + strSaveFileName + strExtension);
            }
            catch
            {
                ltlAlert.Text = "alert('Attachment upload failed.');";
                return;
            }
            #endregion
        }
        #region 操作数据库
        try
        {
            string strName = "sp_m5_insertMstrMsg";
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@no", labNo.Text);
            param[1] = new SqlParameter("@mas", txtMsg.Text);
            param[2] = new SqlParameter("@type", Request["type"].ToString());
            param[3] = new SqlParameter("@effDate", txtEffDate.Text);
            param[4] = new SqlParameter("@fileName", strFileName + strExtension);
            param[5] = new SqlParameter("@filePath", strCateFolder + strSaveFileName + strExtension);
            param[6] = new SqlParameter("@uID", Session["uID"].ToString());
            param[7] = new SqlParameter("@uName", Session["uName"].ToString());
            param[8] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@domain", txtUserDomain.Text);
            param[10] = new SqlParameter("@userNo", txtUserNo.Text);
            param[11] = new SqlParameter("@aboutBoom", chklAboutBoom.SelectedValue);
            

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[8].Value))
            {
                this.Alert("Save failed！Please contact the administrator ！");
            }
            else
            {
               
                this.ltlAlert.Text = " alert('Success'); var loc=$('body', parent.document).find('#ifrm_560000440')[0].contentWindow.location; loc.replace(loc.href);$.loading('none');$('BODY', parent.parent.parent.document).find('#j-modal-dialog').remove();";
                   
            }
        }
        catch (Exception ex)
        {
            this.Alert("Database operation failed ！Please contact the administrator！");
        }
        #endregion
    }

    private bool SaveLevelAndModel()
    {
        try
        {
            string sqlstr = "sp_m5_updateLevelAndModel";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@no",labNo.Text),
                new SqlParameter("@uID",Session["uID"].ToString()),
                new SqlParameter("@level",ddlLevel.SelectedValue),
                new SqlParameter("@modelNo",txtModelNo.Text.Trim()),
                new SqlParameter("@market",ddlMarket.SelectedValue)
            };

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param));

        }
        catch
        {
            return false;
        }
    }
    //protected void dropDomain_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (dropDomain.SelectedIndex == 0)
    //    {
    //        dropDept.Items.Clear();
    //        dropDept.Items.Insert(0, new ListItem("--Please choose department--", "0"));

    //        dropUser.Items.Clear();
    //        dropUser.Items.Insert(0, new ListItem("--Please choose person--", "0"));
    //    }
    //    else
    //    {
    //        dropDept.Items.Clear();

    //        string strSql = "Select departmentID, name From tcpc" + dropDomain.SelectedValue + ".dbo.Departments Where active  = 1 Order By name";

    //        dropDept.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql);
    //        dropDept.DataBind();
    //        dropDept.Items.Insert(0, new ListItem("--Please choose department--", "0"));

    //        dropUser.Items.Clear();
    //        dropUser.Items.Insert(0, new ListItem("--Please choose person--", "0"));
    //    }

    //    //BindEffect();
    //    //BindValid();
    //}
    //protected void dropDept_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (dropDept.SelectedIndex == 0)
    //    {
    //        dropUser.Items.Clear();
    //        dropUser.Items.Insert(0, new ListItem("--Please choose person--", "0"));
    //    }
    //    else
    //    {
    //        dropUser.Items.Clear();

    //        string strSql = "Select userID, userName = N'(' + userNo + N')' + userName From tcpc0.dbo.Users Where leaveDate Is Null And PlantCode = " + dropDomain.SelectedValue + " And departmentID  = " + dropDept.SelectedValue + " Order By userNo";

    //        dropUser.DataSource = SqlHelper.ExecuteDataset(strConn, CommandType.Text, strSql);
    //        dropUser.DataBind();
    //        dropUser.Items.Insert(0, new ListItem("--Please choose person--", "0"));
    //    }

    //    //BindEffect();
    //    //BindValid();
    //}
    //protected void ddlMarket_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (!hidMarket.Value.ToString().Equals(ddlMarket.SelectedValue))
    //    {
    //        btnSave.Attributes.Add("onclick", "return confirm('The market be change! Do you want change the market?')");
    //    }
    //    else
    //    {
    //        btnSave.Attributes.Remove("onclick");
    //    }
    //}
    protected void btnChoseExcutor_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "var w=window.open('m5_chosePerson.aspx','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); w.focus();";
    }
}