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
using CommClass;
using adamFuncs;

public partial class Performance_perf_Add : BasePage
{
    adamClass adam = new adamClass();
    adamClass chk = new adamClass();

    //public string _fpath
    //{
    //    get
    //    {
    //        return ViewState["fpath"].ToString();
    //    }
    //    set
    //    {
    //        ViewState["fpath"] = value;
    //    }
    //}
    //public string _fname
    //{
    //    get
    //    {
    //        return ViewState["fname"].ToString();
    //    }
    //    set
    //    {
    //        ViewState["fname"] = value;
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dd_type.SelectedIndex = 0;

            ListItem item = new ListItem("--", "-1");

            dd_type.Items.Add(item);

            string StrSql = "SELECT perf_type_id,perf_type From tcpc0.dbo.perf_type order by perf_type_id ";
            DataTable ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql).Tables[0];

            dropPlant.SelectedValue = Session["PlantCode"].ToString();

            getdept();
            foreach (DataRow row in ds.Rows)
            {
                item = new ListItem(row["perf_type"].ToString(), row["perf_type_id"].ToString());

                dd_type.Items.Add(item);

            }
            if(Request["type_id"] != null)
            {
                dd_type.SelectedValue = Request["type_id"].ToString();
                loadCause();
                dd_cause.SelectedValue = Request["cause_id"].ToString();
                 StrSql = "SELECT perf_mark_min= isnull(perf_mark_min,0),perf_mark_max=isnull(perf_mark_max,0) From tcpc0.dbo.perf_definition where perf_type =N'" + dd_type.SelectedItem.Text.Trim() + "' and perf_cause=N'" + dd_cause.SelectedItem.Text.Trim() + "'";

                DataTable dt = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql).Tables[0];

                foreach (DataRow row in dt.Rows)
                {

                    //txt_demp.Text = row["name"].ToString();
                    //txt_user.Text = row["userName"].ToString();

                    txb_ref.Text = row["perf_mark_min"].ToString() + "-" + row["perf_mark_max"].ToString();
                    txb_mark.Text = row["perf_mark_min"].ToString();

                }
            }
            

        }
    }
    protected void dd_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCause();
       string StrSql = "SELECT perf_mark_min= isnull(perf_mark_min,0),perf_mark_max=isnull(perf_mark_max,0) From tcpc0.dbo.perf_definition where perf_type =N'" + dd_type.SelectedItem.Text.Trim() + "' and perf_cause=N'" + dd_cause.SelectedItem.Text.Trim() + "'";

        DataTable dt = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql).Tables[0];

        foreach (DataRow row in dt.Rows)
        {

            //txt_demp.Text = row["name"].ToString();
            //txt_user.Text = row["userName"].ToString();

            txb_ref.Text = row["perf_mark_min"].ToString() + "-" + row["perf_mark_max"].ToString();
            txb_mark.Text = row["perf_mark_min"].ToString();

        }
    }
    public void loadCause()
    {
        string StrSql = "";
        if (txtsearch.Text == string.Empty)
        {
             StrSql = "SELECT perf_defi_id,perf_cause From tcpc0.dbo.perf_definition where perf_type_id ='" + dd_type.SelectedValue.ToString() + "' order by perf_type_id,perf_cause ";
        }
        else
        {
            StrSql = "SELECT perf_defi_id,perf_cause From tcpc0.dbo.perf_definition where perf_type_id ='" + dd_type.SelectedValue.ToString() + "' and perf_cause like N'%"+txtsearch.Text.Trim()+"%' order by perf_type_id,perf_cause ";
        }
        DataTable dt = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql).Tables[0];
        dd_cause.DataSource = dt;
        dd_cause.DataBind();
    }

    protected bool UpmessageFile(ref string _fpath,ref string _fname)
    {
        string strUserFileName = filename1.PostedFile.FileName;
        int flag = strUserFileName.LastIndexOf("\\");
        string fileName = strUserFileName.Substring(flag + 1);

        string catPath = "/TecDocs/perf/";
        string strCatFolder = Server.MapPath(catPath);
        
        string attachName = Path.GetFileNameWithoutExtension(filename1.PostedFile.FileName);
        string attachExtension = Path.GetExtension(filename1.PostedFile.FileName);
        string SaveFileName = System.IO.Path.Combine(Server.MapPath(catPath), DateTime.Now.ToFileTime().ToString() + "-" + attachName);//合并两个路径为上传到服务器上的全路径
        if (File.Exists(SaveFileName))
        {
            try
            {
                File.Delete(SaveFileName);
            }
            catch
            {
                ltlAlert.Text = "alert('fail to delete folder！')";

                return false;
            }
        }

        try
        {
            filename1.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            ltlAlert.Text = "alert('fail to save file')";

            return false;
        }

        string path = "/TecDocs/perf/";

        if (!Directory.Exists(Server.MapPath(path)))
        {
            Directory.CreateDirectory(Server.MapPath(path));
        }

        string docid = DateTime.Now.ToString("yyyyMMddhhmmssfff") + "-" + attachName + attachExtension;
        try
        {
            File.Move(SaveFileName, Server.MapPath(path + docid));
        }
        catch
        {
            ltlAlert.Text = "alert('fail to move file')";

            if (File.Exists(SaveFileName))
            {
                try
                {
                    File.Delete(SaveFileName);
                }
                catch
                {
                    ltlAlert.Text = "alert('fail to delete folder')";

                    return false;
                }
            }
            return false;
        }


        _fpath = strCatFolder + docid;
        _fname = fileName;
        return true;
       
    }

    protected void btn_next_Click(object sender, EventArgs e)
    {

        if (dd_cause.SelectedValue.ToString() == "")
        {
            this.Alert("原因不能为空");
            return;
        }
        if (dd_user.SelectedIndex<1)
        {
            this.Alert("责任人不能为空");
            return;
        }
        string StrSql = "perf_insert_markNew";
        string uno = txb_no.Text.Trim();
        string _fpath="";
        string _fname="";
       // string una = txt_user.Text.Trim();
        if (filename1.Value.Trim() != string.Empty)
        {
            UpmessageFile(ref _fpath , ref _fname);
          
        }
        else
        {
            _fpath = "";
            _fname = "";
        }
        //string ss = txt_user.Text.Trim();
        //int ind = ss.IndexOf("-");
        //if (ind > -1)
        //{
        //    uno = ss.Substring(0, ind);
        //    una = ss.Substring(ind + 1);
        //}

        SqlParameter[] param = new SqlParameter[20];
        param[0] = new SqlParameter("@userid", dd_user.SelectedValue);
        param[1] = new SqlParameter("@cause", dd_cause.SelectedItem.Text.Trim());
        param[2] = new SqlParameter("@mark", txb_mark.Text.Trim());
        param[3] = new SqlParameter("@note", txb_note.Text.Trim());
        param[4] = new SqlParameter("@createdby", Session["uID"].ToString());
        param[5] = new SqlParameter("@createdname", Session["UName"].ToString());
        // param[6] = new SqlParameter("@mid", userNo);
        param[7] = new SqlParameter("@type", dd_type.SelectedItem.Text.Trim());
        param[8] = new SqlParameter("@duty", dd_duty.SelectedValue.Trim());
        param[9] = new SqlParameter("@dept", dd_dept.SelectedItem.Text.Trim());
        param[10] = new SqlParameter("@userno", uno);
       // param[11] = new SqlParameter("@uname", una);
        param[12] = new SqlParameter("@fpath", _fpath);
        param[13] = new SqlParameter("@fname", _fname);
        param[14] = new SqlParameter("@managerEmial", SqlDbType.NVarChar, 100);
        
        param[14].Direction = ParameterDirection.Output;

        SqlHelper.ExecuteScalar(admClass.getConnectString("SqlConn.Conn" + dropPlant.SelectedValue.ToString().Trim()), CommandType.StoredProcedure, StrSql, param);

        #region 发送邮件
        string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
        string to =  param[14].Value.ToString();
        string copy = "";
        string subject = "考评违纪通知书";
        string body = "";
        #region 写Body
        body += "<font style='font-size: 12px;'>您的下属" + dd_user.SelectedItem.Text + "</font><br />";
        body += "<font style='font-size: 12px;'>有考评违纪行为</font><br />";
        body += "<font style='font-size: 12px;'>请到系统确认，并及时改进。</font><br />";
        body += "<br /><br />";
        body += "<font style='font-size: 12px;'>详情请登陆 "+baseDomain.getPortalWebsite()+" </font><br />";
        body += "<font style='font-size: 12px;'>For details please visit "+baseDomain.getPortalWebsite()+" </font>";
        #endregion
        if (!this.SendEmail(from, to, copy, subject, body))
        {
            this.ltlAlert.Text = "alert('邮件发送失败，保存成功');";
        }
        else
        {
            this.ltlAlert.Text = "alert('邮件发送成功，保存成功');";
        }
        #endregion

        
        //txt_user.Text = "";
        //txt_demp.Text = "";
        txb_note.Text = "";
        txb_no.Text = "";
        //txb_mark.Text = "";
        txb_comm.Text = "";
        //txb_ref.Text = "";
        dd_user.SelectedIndex = 0;


    }
    protected void btn_back_Click(object sender, EventArgs e)
    {
        Response.Redirect("perf_mstr.aspx");
    }
    protected void txb_no_TextChanged(object sender, EventArgs e)
    {

        string StrSql = "select userid,de.name,de.departmentID ,us.userName from tcpc0.dbo.Users us LEFT  JOIN tcpc" + dropPlant.SelectedValue + ".dbo.Departments de ON us.departmentID = de.departmentID  where roleID<>1 and plantCode = '" + dropPlant.SelectedValue + "' and isnull(isactive,0)=1 and isnull(deleted,0) =0 and (leaveDate is null or datediff(month,leaveDate,getdate()) <=1 ) and userno='" + txb_no.Text.Trim() + "' ";
        DataTable dt = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql).Tables[0];

        foreach (DataRow row in dt.Rows)
        {
            try
            {
                getdept();

                dd_dept.SelectedValue = row["departmentID"].ToString();
                loadUser();
                dd_user.SelectedValue = row["userid"].ToString();
            }
            catch (Exception)
            {
                
               
            }
          
           
            //txt_demp.Text = row["name"].ToString();
            //txt_user.Text = row["userName"].ToString();
            //lbluid.Text = row["userid"].ToString();


        }

        //reader = SqlHelper.ExecuteReader(chk.dsnx, CommandType.Text, StrSql)
        //If (reader.Read()) Then 
        //    dd_dept.SelectedValue = reader(1)

        //    loadUser()

        //    dd_user.SelectedValue = reader(0)
        //Else
        //    dd_dept.SelectedIndex = 0
        //    loadUser()
        //End If
        //reader.Close()
    }
    protected void dd_cause_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StrSql = "SELECT perf_mark_min= isnull(perf_mark_min,0),perf_mark_max=isnull(perf_mark_max,0) From tcpc0.dbo.perf_definition where perf_type =N'" + dd_type.SelectedItem.Text.Trim() + "' and perf_cause=N'" + dd_cause.SelectedItem.Text.Trim() + "'";

        DataTable dt = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql).Tables[0];

        foreach (DataRow row in dt.Rows)
        {

            //txt_demp.Text = row["name"].ToString();
            //txt_user.Text = row["userName"].ToString();

            txb_ref.Text = row["perf_mark_min"].ToString() + "-" + row["perf_mark_max"].ToString();
            txb_mark.Text = row["perf_mark_min"].ToString();

        }
    }
    protected void rdl_cause_SelectedIndexChanged(object sender, EventArgs e)
    {
        string StrSql = "SELECT perf_mark_min= isnull(perf_mark_min,0),perf_mark_max=isnull(perf_mark_max,0) From tcpc0.dbo.perf_definition where perf_type =N'" + dd_type.SelectedItem.Text.Trim() + "' and perf_cause=N'" + dd_cause.SelectedItem.Text.Trim() + "'";

        DataTable dt = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql).Tables[0];

        foreach (DataRow row in dt.Rows)
        {

            //txt_demp.Text = row["name"].ToString();
            //txt_user.Text = row["userName"].ToString();

            txb_ref.Text = row["perf_mark_min"].ToString() + "-" + row["perf_mark_max"].ToString();
            txb_mark.Text = row["perf_mark_min"].ToString();

        }
    }
    protected void txtsearch_TextChanged(object sender, EventArgs e)
    {
        loadCause();
    }
    protected void dropPlant_SelectedIndexChanged(object sender, EventArgs e)
    {
        getdept();
        dd_user.Items.Clear();
    }
    public void getdept()
    {
        dd_dept.Items.Clear();
        string StrSql = "SELECT departmentID,name From tcpc" + dropPlant .SelectedValue+ ".dbo.departments where isnull(active,0)=1 and isnull(isSalary,0)=1 order by departmentID ";
         DataSet ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql);
          ListItem ls = new ListItem("--");
                ls.Value = "0";
                    dd_dept.Items.Add(ls);
                    dd_dept.SelectedIndex = 0;
        
         foreach (DataRow row in ds.Tables[0].Rows)
         {

          
               ls = new ListItem(row["name"].ToString());
               ls.Value = row["departmentID"].ToString();
               dd_dept.Items.Add(ls);


         }
    }
    protected void dd_dept_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadUser();

    }
    public void loadUser()
    {
        if (dd_dept.SelectedIndex > 0)
        {
            dd_user.Items.Clear();
            string StrSql = "select userid,userno,username from tcpc0.dbo.Users where roleID<>1 and plantCode = '" + dropPlant.SelectedValue + "' and isnull(isactive,0)=1 and isnull(deleted,0) =0 and (leaveDate is null or datediff(month,leaveDate,getdate()) <=1 ) and departmentid='" + dd_dept.SelectedValue + "' order by userid";
           // string StrSql = "SELECT departmentID,name From tcpc" + dropPlant.SelectedValue + ".dbo.departments where isnull(active,0)=1 and isnull(isSalary,0)=1 order by departmentID ";
            DataSet ds = SqlHelper.ExecuteDataset(chk.dsnx(), CommandType.Text, StrSql);
            ListItem ls = new ListItem("--");
            ls.Value = "0";
            dd_user.Items.Add(ls);
            dd_user.SelectedIndex = 0;

            foreach (DataRow row in ds.Tables[0].Rows)
            {


                ls = new ListItem(row["userno"].ToString() +"-"+ row["username"].ToString());
                ls.Value = row["userid"].ToString();
                dd_user.Items.Add(ls);


            }
        }
    }
}