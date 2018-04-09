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

public partial class m5_new : BasePage
{
    public String strConn = ConfigurationSettings.AppSettings["SqlConn.Conn"];
    public string EffectTR = string.Empty;//第一个标签：部门确认显示的tr
    public string ValidTR = string.Empty;//第一个标签：验证显示的tr

    private string  DESC
    {
        get
        {

            return ViewState["DESC"].ToString();
        }
        set
        {
            ViewState["DESC"] = value;
        }
    }

    private string REASON
    {
        get
        {

            return ViewState["REASON"].ToString();
        }
        set
        {
            ViewState["REASON"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region 记录当前选中的Tab
            try
            {
                hidTabIndex.Value = Request.QueryString["index"];
            }
            catch (Exception)
            {

            }
            #endregion
            #region 单号获取
            if (Request.QueryString["no"] != null || Request.QueryString["no"] == string.Empty)
            {
                lblNo.Text = Request.QueryString["no"];
            }
            else
            {
                btnDone.Enabled = false;
                this.Alert("Params Error!");
            }
            #endregion
            #region 绑定dropProject
            radProject.Items.Clear();
            radProject.DataSource = this.GetProjects();
            radProject.DataBind();
            #endregion
            //设定一个标志，

            string chenge = this.Security["560000461"].isValid ? "1" : "0";
            if(checkEffectHaveDisagree(Request.QueryString["no"], Session["uID"].ToString())=="1" || chenge == "1")
            {
                chenge = "1";
            }

            hidReasonChange.Value = chenge;

            #region 是否有权限去点击同意
            hidAgreeAuth.Value = CheckNotisAuth(Session["uID"].ToString(), lblNo.Text).ToString();
            #endregion
            #region 获取项目号
            DataTable dtRDW = GetProjectCode(lblNo.Text);
            if (dtRDW.Rows.Count > 0)
            {
                hlRDW.Text = dtRDW.Rows[0][0].ToString();
                if (!string.Empty.Equals(hlRDW.Text))
                {
                    hlRDW.NavigateUrl = "/RDW/RDW_DetailList.aspx?mid=" + dtRDW.Rows[0][1].ToString() + "&fr=" + lblNo.Text + "&ecnCode=" + lblNo.Text;

                }
            }
            #endregion
            #region 将申请的原因和申请的详情初始化

            txtDesc.InnerHtml = SelectMsgAndFile(lblNo.Text, "desc");
            txtReason.InnerHtml = SelectMsgAndFile(lblNo.Text, "reason");
            #endregion 

            #region 初始化 申请单
            DataTable _mstrM5 = this.GetM5MstrByNo(lblNo.Text);
            foreach (DataRow row in _mstrM5.Rows)
            {
                #region 初始化非btn控件
                try
                {
                    radProject.Items.FindByValue(row["m5_project"].ToString()).Selected = true;
                }
                catch { }

                DESC = row["m5_desc"].ToString();
                REASON = row["m5_reason"].ToString();
                //txtDesc.Text = row["m5_desc"].ToString();
                //if (string.IsNullOrEmpty(row["m5_desc_file"].ToString()))
                //{
                //    hlinkDesc.Visible = false;
                //}
                //else
                //{
                //    hlinkDesc.Text = "Attachment:" + row["m5_desc_file"].ToString();
                //    hlinkDesc.NavigateUrl = row["m5_desc_path"].ToString();
                //}

                //txtReason.Text = row["m5_reason"].ToString();
                //if (string.IsNullOrEmpty(row["m5_reason_file"].ToString()))
                //{
                //    hlinkReason.Visible = false;
                //}
                //else
                //{
                //    hlinkReason.Text = "Attachment:" + row["m5_reason_file"].ToString();
                //    hlinkReason.NavigateUrl = row["m5_reason_path"].ToString();
                //}

                txtApprMsg.Text = row["m5_appr_msg"].ToString();

                if (string.IsNullOrEmpty(row["m5_appr_file"].ToString()))
                {
                    hlinkManager.Visible = false;
                }
                else
                {
                    hlinkManager.Text = "Attachment:" + row["m5_appr_file"].ToString();
                    hlinkManager.NavigateUrl = row["m5_appr_path"].ToString();
                }
                lbLevel.Text = row["soque_degreeName"].ToString();
                lbModel.Text = row["m5_modelNumber"].ToString();

                //  txtValidMsg.Text = row["m5_agree_msg"].ToString();

                //  if (string.IsNullOrEmpty(row["m5_agree_file"].ToString()))
                // {
                //     hlinkValid.Visible = false;
                // }
                //else
                // {
                //     hlinkValid.Text = "Attachment:" + row["m5_agree_file"].ToString();
                //      hlinkValid.NavigateUrl = row["m5_agree_path"].ToString();
                //  }
                tdAgreeMsg.InnerHtml = SelectMsgAndFile(lblNo.Text, "agree");


                //获取是否同意变更留言及附件
                //tdAgreeMsg.InnerHtml = SelectMsgAndFile(lblNo.Text, "agree");
                lbAboutBoom.Text = row["m5_AboutBoom"].ToString();

                if (row["m5_AboutBoom"].ToString().Equals("keep same"))
                {
                    lbAboutBoom.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lbAboutBoom.ForeColor = System.Drawing.Color.Red;
                }

                lblCreateName.Text = row["m5_createName"].ToString() + "-" + row["m5_createNameEn"].ToString();
                hidCreateBy.Value = row["m5_createBy"].ToString();

                lblMarket.Text = row["m5_market"].ToString();

                lbisChangeSafety.Text = row["m5_isAboutSafety"].ToString();
                
                
                //chkIsExcute.Checked = Convert.ToBoolean(row["m5_isExcuted"]);
                //txtExcuteMsg.Text = row["m5_excute_msg"].ToString();
                //获取生效日期
                if (row["m5_effDate"].ToString() == "1900-01-01")
                {
                    labEffDate.Text = "";
                }
                else
                {
                    labEffDate.Text = row["m5_effDate"].ToString();
                }
                //获取执行人
                DataTable dt = GetExecutor(lblNo.Text);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow rw in dt.Rows)
                    {
                        labUExcutor.Text = rw["m5xu_userName"].ToString() + " - " + rw["englishName"].ToString();
                        if (rw["email"].ToString() != "")
                        {
                            hidUExcutorEmail.Value = ";" + rw["email"];
                        }
                    }
                    // labUExcutor.Text = labUExcutor.Text.Substring(5);

                }
                if (string.IsNullOrEmpty(row["m5_excute_file"].ToString()))
                {
                    // hlinkExcute.Visible = false;
                }
                else
                {
                    //hlinkExcute.Text = "Attachment:" + row["m5_excute_file"].ToString();
                    // hlinkExcute.NavigateUrl = row["m5_excute_file"].ToString();
                }

                //创建人的Email
                hidEmailAddress.Value = string.IsNullOrEmpty(row["email"].ToString()) ? "" : (row["email"].ToString() + ";");
                #endregion

                btnDone.Enabled = this.Security["560000461"].isValid ;
                btnNotDone.Enabled = this.Security["560000461"].isValid ;

                #region m5_isApproved为真表示同意，为假表示拒绝；前台区分不出Null
                if (!string.IsNullOrEmpty(row["m5_apprBy"].ToString()))
                {
                    chkIsApprove.Checked = true;
                    btnDone.Enabled = false;
                    btnNotDone.Enabled = false;
                    txtApprMsg.ReadOnly = false;

                    txtApprMsg.Text = row["m5_appr_msg"].ToString();
                    txtApprMsg.BackColor = Color.White;
                    txtApprMsg.ReadOnly = true;

                    hlinkManager.Text = "Attachment:" + row["m5_appr_file"].ToString();
                    hlinkManager.NavigateUrl = row["m5_appr_path"].ToString();
                    fileManager.Visible = false;

                    if (Convert.ToBoolean(row["m5_isApproved"]))
                    {
                        hidAppr.Value = "1"; //主管已签字 同意
                        btnDone.Text = "Agree（By " + row["m5_apprNameEn"].ToString() + "）";
                        btnNotDone.Visible = false;
                    }
                    else
                    {
                        hidAppr.Value = "2"; //主管已签字 拒绝
                        btnNotDone.Text = "Disagree（By " + row["m5_apprNameEn"].ToString() + "）";
                        btnDone.Visible = false;
                    }
                    //if (!(chkAgree.Checked || chkNotAgree.Checked) && !(chkValid.Checked || chkNotValid.Checked) && this.Security["560000463"].isValid && CheckEffectFinished(lblNo.Text))
                    //{
                    //    btnValid.Enabled = true;
                    //    btnNotValid.Enabled = true;
                    //}
                    if ((!(chkValid.Checked || chkNotValid.Checked) && Convert.ToBoolean(hidAgreeAuth.Value) 
                        && CheckEffectFinished(lblNo.Text, true)) || this.Security["560000467"].isValid)
                    {
                        btnValid.Enabled = true;

                    }
                    if ((!(chkValid.Checked || chkNotValid.Checked) && Convert.ToBoolean(hidAgreeAuth.Value) 
                        && CheckEffectFinished(lblNo.Text, false)) || this.Security["560000467"].isValid)
                    {

                        btnNotValid.Enabled = true;
                    }
                }
                else
                {
                    hlinkManager.Visible = false;
                    /*
                     * 主管尚未审核的情况下，验证、变更都要禁用
                     * 需要验证:当任意部门有留言的时候，激活
                     * 同意变更：需要验证时，当任意部门有意见时，激活；当不需要检验时，直接激活
                     * 同时还要考虑权限
                     */
                    btnValid.Enabled = false;
                    btnNotValid.Enabled = false;

                    // btnAgree.Enabled = false;
                    // btnNotAgree.Enabled = false;
                    hidAppr.Value = "0"; //主管未签字
                }
                #endregion

                #region 是否需要验证
                if (!string.IsNullOrEmpty(row["m5_agreeBy"].ToString()))
                {
                    chkValid.Checked = Convert.ToBoolean(row["m5_isAgreed"]);
                    chkNotValid.Checked = !Convert.ToBoolean(row["m5_isAgreed"]);
                    btnValid.Enabled = false;
                    btnNotValid.Enabled = false;
                    //txtValidMsg.ReadOnly = false;

                    // txtValidMsg.Text = row["m5_agree_msg"].ToString();
                    //txtValidMsg.BackColor = Color.White;
                    // txtValidMsg.ReadOnly = true;

                    // hlinkValid.Text = "Attachment:" + row["m5_agree_file"].ToString();
                    //hlinkValid.NavigateUrl = row["m5_agree_path"].ToString();
                    // fileValid.Visible = false;

                    if (Convert.ToBoolean(row["m5_isAgreed"]))
                    {
                        hidNeed.Value = "2";//需要验证
                        btnValid.Text = "Agree（By " + row["m5_agreeNameEn"].ToString() + "）";
                        btnNotValid.Visible = false;

                        //不能在这里放开btnAgree，因为必须要有留言
                    }
                    else
                    {
                        hidNeed.Value = "1";//不需要验证
                        btnNotValid.Text = "Disagree（By " + row["m5_agreeNameEn"].ToString() + "）";
                        btnValid.Visible = false;

                        //btnAgree.Enabled = true;
                        //btnNotAgree.Enabled = true;
                    }
                    //if (!(chkAgree.Checked || chkNotAgree.Checked) && this.Security["560000462"].isValid && CheckValidFinished(lblNo.Text))
                    //{
                    //    hidNeed.Value = "3";//需要验证 并且在det中已经有记录
                    //    btnAgree.Enabled = true;
                    //    btnNotAgree.Enabled = true;
                    //}

                    if (this.Security["560000462"].isValid && CheckValidFinished(lblNo.Text))
                    {
                        hidNeed.Value = "3";//需要验证 并且在det中已经有记录
                        //btnAgree.Enabled = true;
                        //btnNotAgree.Enabled = true;
                    }
                }
                else
                {
                    //hlinkValid.Visible = false;
                    //尚未决定是否验证时，禁用
                    // btnAgree.Enabled = false;
                    //btnNotAgree.Enabled = false;
                    hidNeed.Value = "0"; //还未确定是否进行验证
                }
                #endregion

                #region 是否同意变更：m5_agreeName没有记录，表示未操作，否则就是有结果
                if (!string.IsNullOrEmpty(row["m5_agreeBy"].ToString()))
                {
                    //chkAgree.Checked = Convert.ToBoolean(row["m5_isAgreed"]);
                    //chkNotAgree.Checked = !Convert.ToBoolean(row["m5_isAgreed"]);
                    hidAgree.Value = "1"; //是否同意变更已确认
                    //btnAgree.Enabled = false;
                    //btnNotAgree.Enabled = false;

                    if (Convert.ToBoolean(row["m5_isAgreed"]))
                    {
                        labEffDate.Text = row["m5_effDate"].ToString();
                        //txtEffDate.Text = row["m5_effDate"].ToString();
                        //btnAgree.Text = "YES（By " + row["m5_agreeNameEn"].ToString() + "）";
                        //btnNotAgree.Visible = false;
                    }
                    else
                    {
                        //btnNotAgree.Text = "NO（By " + row["m5_agreeNameEn"].ToString() + "）";
                        //btnAgree.Visible = false;
                    }

                    //lblExcutor.Visible = false;
                    //dropDomain.Visible = false;
                    //dropDept.Visible = false;
                    //dropUser.Visible = false;
                }
                else
                {
                    //hlinkAgree.Visible = false;
                }
                #endregion

                #region 没有同意变更的、已经有留言的，都无法撰写执行情况
                if (Convert.ToBoolean(row["m5_isAgreed"]))
                {
                    btnExcute.Enabled = true;
                    if (Convert.ToBoolean(row["m5_isExcuted"]))
                    {
                        btnExcute.Text = "SAVED（By " + row["m5_excuteNameEn"].ToString() + "）";
                        btnExcute.Enabled = false;
                        txtExcuteMsg.BackColor = Color.White;
                        txtExcuteMsg.ReadOnly = true;
                    }

                    //    if (Convert.ToBoolean(row["m5_isPowerExcuted"]))
                    //    {
                    //        btnPowerExecute.Text = "SAVED（By " + row["m5_PowerExcutedNameEn"].ToString() + "）";
                    //        btnPowerExecute.Enabled = false;
                    //        txtExcuteMsg.BackColor = Color.White;
                    //        txtExcuteMsg.ReadOnly = true;
                    //    }
                }
                #endregion
            }
            #endregion
            #region 填写可同意人
            tdAgree.InnerHtml = this.getAgreeAuth();
            #endregion


            BindEffect();
            BindValid();

            #region 注册btn客户端提示
            btnDone.Attributes.Add("onclick", "return confirm('Once click, you can't change!')");
            btnNotDone.Attributes.Add("onclick", "return confirm('Once click, you can't change it!')");

            btnValid.Attributes.Add("onclick", "return confirm('Once click, you can't change it!')");
            btnNotValid.Attributes.Add("onclick", "return confirm('Once click, you can't change it!')");

            //btnAgree.Attributes.Add("onclick", "return confirm('Once click, the system sends e-mail, and can not change!')");
            //btnNotAgree.Attributes.Add("onclick", "return confirm('Once click, the system sends e-mail, and can not change!')");
            #endregion

            #region 试流单结果
            //string ECNstatus = getECNstatus(lblNo.Text);
            //if (ECNstatus == "1")
            //{
            //    labProd.Text = "Trial flow is through";
            //}
            //else
            //{
            //    labProd.Text = "Trial flow is not through";
            //}
            #endregion
        }
    }

    private string checkEffectHaveDisagree(string no, string uID)
    {
        try
        {
            string sqlstr = "sp_m5_checkEffectHaveDisagree";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@no",no)
                , new SqlParameter("@uID",uID)
            };

            return SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param).ToString();

        }
        catch
        {
            return null;
        }
    }

    private string getAgreeAuth()
    {
        try
        {
            string sqlstr = "sp_m5_selectAgreeAuthByNo";

            SqlParameter[] param = new SqlParameter[]{
               new  SqlParameter("@NO",lblNo.Text)
            };

            SqlDataReader sdr = SqlHelper.ExecuteReader(strConn, CommandType.StoredProcedure, sqlstr, param);
            StringBuilder sb = new StringBuilder();

            while (sdr.Read())
            {
                sb.Append(sdr["m5aa_uName"].ToString());
                sb.Append("--");
                sb.Append(sdr["m5aa_uNameEn"].ToString());
                sb.Append("<br/>");
            }

            return sb.ToString();

        }
        catch
        {
            return null;
        }
    }

    private DataTable GetProjectCode(string No)
    {
        try
        {
            string sqlstr = "sp_m5_selectProjectCode";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@No",No)
            };

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, sqlstr, param).Tables[0];

        }
        catch
        {
            return null;
        }
    }

    private bool CheckNotisAuth(string uID, string no)
    {
        try
        {
            string sqlstr = "sp_m5_CheckNotisAuth";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@uID",uID)
                , new SqlParameter("@no",no)
            };

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param));

        }
        catch
        {
            return false;

        }
    }
    private string getECNstatus(string no)
    {
        string sql = "select isnull(m5_ECNstatus,0) m5_ECNstatus from tcpc0.dbo.m5_mstr where m5_no = '" + no + "'";
        return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.Text, sql));
    }
    private string SelectMsgAndFile(string no, string type)
    {
        string str = "sp_m5_selectMsgAndFile";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@type", type);
        return Convert.ToString(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, str, param));
    }
    public DataTable GetM5MstrByNo(string no)
    {
        try
        {
            string strSql = "sp_m5_selectM5MstrByNo";
            SqlParameter param = new SqlParameter("@no", no);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    public DataTable GetEffectUser(string effID)
    {
        try
        {
            string strSql = "sp_m5_selectM5EffectUser";
            SqlParameter param = new SqlParameter("@effectID", effID);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    public DataTable GetValidUser(string validID)
    {
        try
        {
            string strSql = "sp_m5_selectM5ValidUser";
            SqlParameter param = new SqlParameter("@validID", validID);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    public DataTable GetEffectDetail(string no, string effID)
    {
        try
        {
            string strSql = "sp_m5_selectM5EffectDet";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@no", no);
            param[1] = new SqlParameter("@effectID", effID);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    public DataTable GetValidDetail(string no, string validID)
    {
        try
        {
            string strSql = "sp_m5_selectM5ValidDet";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@no", no);
            param[1] = new SqlParameter("@validID", validID);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    public void BindEffect()
    {
        gv1.DataSource = this.GetM5MstrEffect();
        gv1.DataBind();
    }
    public void BindValid()
    {
        //#region 设置chkAgree、chkNotAgree是否可选
        //if (chkIsAgree.Checked)
        //{
        //    chkAgree.Enabled = false;
        //    chkNotAgree.Enabled = false;
        //}
        //else
        //{
        //    if (chkValid.Checked || chkNotValid.Checked)
        //    {
        //        chkAgree.Enabled = true;
        //        chkNotAgree.Enabled = true;
        //    }
        //    else
        //    {
        //        chkAgree.Enabled = false;
        //        chkNotAgree.Enabled = false;
        //    }
        //}
        //#endregion

        //gv2.DataSource = this.GetM5MstrValid();
        //gv2.DataBind();
    }
    public DataTable GetM5MstrEffect()
    {
        try
        {
            string strSql = "sp_m5_selectM5Effect";

            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@_no",lblNo.Text)
            };
            DataSet ds = SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return ds.Tables[1];
            }


        }
        catch
        {
            return null;
        }

    }
    public DataTable GetM5MstrValid()
    {
        try
        {
            string strSql = "sp_m5_selectM5Valid";
            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql).Tables[0];
        }
        catch
        {
            return null;
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="no"></param>
    /// <param name="flag">标志位true是agree钮 false是disagree钮</param>
    /// <returns></returns>
    public bool CheckEffectFinished(string no, bool flag)
    {
        string strSql = "sp_m5_checkEffectFinished";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@no", no);
        param[1] = new SqlParameter("@flag", flag);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, param));
    }
    public bool CheckValidFinished(string no)
    {
        string strSql = "sp_m5_checkValidFinished";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@no", no);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, strSql, param));
    }
    /// <summary>
    /// 验证是否指定了执行人
    /// </summary>
    /// <returns></returns>
    public bool CheckExecutorSpecified(String no)
    {
        return true;
        //Marked By Shanzm 2015-06-08:user\dept改用drop之后，舍弃
        //try
        //{
        //    string strSql = "sp_m5_checkExecutorSpecified";
        //    SqlParameter[] param = new SqlParameter[4];
        //    param[0] = new SqlParameter("@no", no);
        //    param[1] = new SqlParameter("@plantCode", dropDomain.SelectedValue);
        //    param[2] = new SqlParameter("@userNo", dropUser.SelectedValue);
        //    param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
        //    param[3].Direction = ParameterDirection.Output;

        //    SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strSql, param);

        //    return Convert.ToBoolean(param[3].Value);
        //}
        //catch
        //{
        //    return false;
        //}
    }
    /// <summary>
    /// 获取ECN执行人
    /// </summary>
    /// <param name="no"></param>
    /// <returns></returns>
    public DataTable GetExecutor(String no)
    {
        try
        {
            string strSql = "sp_m5_selectM5ExecutorByNo";
            SqlParameter param = new SqlParameter("@no", no);

            return SqlHelper.ExecuteDataset(strConn, CommandType.StoredProcedure, strSql, param).Tables[0];
        }
        catch
        {
            return null;
        }

    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        if (hidCreateBy.Value.Equals(Session["uID"].ToString()))
        {
            this.Alert("fail! The ECN is applied with you.You can not approval");
            return;
        }


        hidAppr.Value = "1"; //主管签字通过
        String strFileName = "";//文件名
        String strCateFolder = "/TecDocs/ECN/";
        String strExtension = "";//文件后缀
        String strSaveFileName = "";//储存名
        if (fileManager.PostedFile.FileName != "")
        {
            #region 上传文档例行处理
            if (fileManager.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('The maximum file upload is 8 MB');";
                return;
            }

            strFileName = Path.GetFileNameWithoutExtension(fileManager.PostedFile.FileName);
            strExtension = Path.GetExtension(fileManager.PostedFile.FileName);
            strSaveFileName = DateTime.Now.ToFileTime().ToString();

            try
            {
                fileManager.PostedFile.SaveAs(Server.MapPath(strCateFolder) + "\\" + strSaveFileName + strExtension);
            }
            catch
            {
                ltlAlert.Text = "alert('Attachment upload failed');";
                return;
            }
            #endregion
        }

        try
        {
            string strName = "sp_m5_approveM5Mstr";
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@no", lblNo.Text);
            param[1] = new SqlParameter("@approve", true);
            param[2] = new SqlParameter("@apprMsg", txtApprMsg.Text.Trim());
            param[3] = new SqlParameter("@fileName", strFileName + strExtension);
            param[4] = new SqlParameter("@filePath", strCateFolder + strSaveFileName + strExtension);
            param[5] = new SqlParameter("@uID", Session["uID"].ToString());
            param[6] = new SqlParameter("@uName", Session["uName"].ToString());
            param[7] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[7].Direction = ParameterDirection.Output;
            param[8] = new SqlParameter("@Email", SqlDbType.NVarChar,2000);
            param[8].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[7].Value))
            {
                this.Alert("fail! Please contact the administrator");
            }
            else
            {
                btnDone.Text = "Agree（By " + Session["eName"].ToString() + "）";
                btnDone.Enabled = false;
                btnNotDone.Visible = false;
                chkIsApprove.Checked = true;

                txtApprMsg.BackColor = Color.White;
                txtApprMsg.ReadOnly = true;

                fileManager.Visible = false;
                hlinkManager.Visible = true;
                if (!string.IsNullOrEmpty(strFileName))
                {
                    hlinkManager.Text = "Attachment:" + strFileName + strExtension;
                    hlinkManager.NavigateUrl = strCateFolder + strSaveFileName + strExtension;
                }

                #region 发送邮件
                string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                string to = param[8].Value.ToString();
                string copy = hidEmailAddress.Value.Substring(0,hidEmailAddress.Value.IndexOf(';'));
                string subject = "TCP ECN (产品变更通知书) Need your approval (需要您审批)";
                string body = "";
                #region 写Body
                body += "<font style='font-size: 12px;'>编号No：" + lblNo.Text + "</font><br />";
                body += "<font style='font-size: 12px;'>申请人Applicant：" + lblCreateName.Text + "</font><br />";
                body += "<font style='font-size: 12px;'>项目Type：" + radProject.SelectedItem.Text + "</font><br />";
                // body += "<font style='font-size: 12px;'>是否同意变更Agree?： " + btnNotAgree.Text + "</font><br />";
                // body += "<font style='font-size: 12px;'>生效日期Excutive Date：" + labEffDate.Text + "</font><br />";
                body += "<font style='font-size: 12px;'>申请内容Contents：" + DESC + "</font><br />";
                body += "<font style='font-size: 12px;'>申请理由Reasons：" + REASON + "</font><br />";
                // body += "<font style='font-size: 12px;'>执行人Executed By：" + labUExcutor.Text + "</font><br />";
                body += "<br /><br />";
                body += "<font style='font-size: 12px;'>详情请登陆 "+baseDomain.getPortalWebsite()+" </font><br />";
                body += "<font style='font-size: 12px;'>For details please visit "+baseDomain.getPortalWebsite()+" </font>";
                #endregion
                if (!this.SendEmail(from, to, copy, subject, body))
                {
                    this.ltlAlert.Text = "alert('Email sending failure');";
                }
                else
                {
                    this.ltlAlert.Text = "alert('Email sending');";
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            this.Alert("fail! Please contact the administrator");
        }

        BindEffect();
        BindValid();
    }
    protected void btnNotDone_Click(object sender, EventArgs e)
    {

        if (hidCreateBy.Value.Equals(Session["uID"].ToString()))
        {
            this.Alert("fail! The ECN is applied with you.You can not approval");
            return;
        }


        hidAppr.Value = "2"; //主管签字不通过
        String strFileName = "";//文件名
        String strCateFolder = "/TecDocs/ECN/";
        String strExtension = "";//文件后缀
        String strSaveFileName = "";//储存名
        if (fileManager.PostedFile != null)
        {
            #region 上传文档例行处理
            if (fileManager.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('The maximum file upload is 8 MB');";
                return;
            }

            strFileName = Path.GetFileNameWithoutExtension(fileManager.PostedFile.FileName);
            strExtension = Path.GetExtension(fileManager.PostedFile.FileName);
            strSaveFileName = DateTime.Now.ToFileTime().ToString();

            try
            {
                fileManager.PostedFile.SaveAs(Server.MapPath(strCateFolder) + "\\" + strSaveFileName + strExtension);
            }
            catch
            {
                ltlAlert.Text = "alert('Attachment upload failed');";
                return;
            }
            #endregion
        }

        try
        {
            string strName = "sp_m5_approveM5Mstr";
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@no", lblNo.Text);
            param[1] = new SqlParameter("@approve", false);
            param[2] = new SqlParameter("@apprMsg", txtApprMsg.Text.Trim());
            param[3] = new SqlParameter("@fileName", strFileName + strExtension);
            param[4] = new SqlParameter("@filePath", strCateFolder + strSaveFileName + strExtension);
            param[5] = new SqlParameter("@uID", Session["uID"].ToString());
            param[6] = new SqlParameter("@uName", Session["uName"].ToString());
            param[7] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[7].Direction = ParameterDirection.Output;
            param[8] = new SqlParameter("@Email", SqlDbType.NVarChar, 2000);
            param[8].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

            if (!Convert.ToBoolean(param[7].Value))
            {
                this.Alert("fail! Please contact the administrator");
            }
            else
            {
                btnNotDone.Text = "Disagree（By " + Session["eName"].ToString() + "）";
                btnNotDone.Enabled = false;
                btnDone.Visible = false;
                chkIsApprove.Checked = false;

                txtApprMsg.BackColor = Color.White;
                txtApprMsg.ReadOnly = true;

                fileManager.Visible = false;
                hlinkManager.Visible = true;
                if (!string.IsNullOrEmpty(strFileName))
                {
                    hlinkManager.Text = "Attachment:" + strFileName + strExtension;
                    hlinkManager.NavigateUrl = strCateFolder + strSaveFileName + strExtension;
                }

                #region 发送邮件
                string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                string to = hidEmailAddress.Value;
                string copy = "";
                string subject = "TCP ECN (产品变更通知书)";
                string body = "";
                #region 写Body
                body += "<font style='font-size: 12px;'>编号No：" + lblNo.Text + "</font><br />";
                body += "<font style='font-size: 12px;'>申请人Applicant：" + lblCreateName.Text + "</font><br />";
                body += "<font style='font-size: 12px;'>项目Type：" + radProject.SelectedItem.Text + "</font><br />";
                // body += "<font style='font-size: 12px;'>是否同意变更Agree?： " + btnNotAgree.Text + "</font><br />";
                // body += "<font style='font-size: 12px;'>生效日期Excutive Date：" + labEffDate.Text + "</font><br />";
                body += "<font style='font-size: 12px;'>申请内容Contents：" + DESC + "</font><br />";
                body += "<font style='font-size: 12px;'>申请理由Reasons：" + REASON + "</font><br />";
                body += "<font style='font-size: 12px;'>您的申请被部门经理"+Session["uName"] +"拒绝，请及时处理</font><br />";
                body += "<font style='font-size: 12px;'>Your application has been refused to department manager" + Session["eName"].ToString() + ", please timely processing！</font><br />";
                // body += "<font style='font-size: 12px;'>执行人Executed By：" + labUExcutor.Text + "</font><br />";
                body += "<br /><br />";
                body += "<font style='font-size: 12px;'>详情请登陆 "+baseDomain.getPortalWebsite()+" </font><br />";
                body += "<font style='font-size: 12px;'>For details please visit "+baseDomain.getPortalWebsite()+" </font>";
                #endregion
                if (!this.SendEmail(from, to, copy, subject, body))
                {
                    this.ltlAlert.Text = "alert('Email sending failure');";
                }
                else
                {
                    this.ltlAlert.Text = "alert('Email sending');";
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            this.Alert("fail! Please contact the administrator");
        }

        BindEffect();
        BindValid();
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        this.Redirect("m5_mstr.aspx");
    }
    protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView rowView = (DataRowView)e.Row.DataItem;

            string _no = lblNo.Text;
            string _effectID = rowView["m5e_id"].ToString();

            EffectTR += "<tr><td class=\"FixedGridHeight\" style=\"height:50px;\"></td>";
            EffectTR += " <td class=\"FixedGridLeft\" style=\"word-break:break-all; word-wrap:break-word;\" colspan=\"4\">" + rowView["m5e_descEn"].ToString() + "</td>";
            #region 绑定留言，同时设置EffectTR
            EffectTR += " <td colspan=\"13\" style=\"text-align: left;\">";
            foreach (DataRow row in this.GetEffectDetail(_no, _effectID).Rows)
            {
                e.Row.Cells[1].Text += string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(row["createDate"])) +
                    "&nbsp;&nbsp;&nbsp;&nbsp;" + row["createName"].ToString()
                    + "&nbsp;&nbsp;&nbsp;&nbsp;"
                    + (string.Empty.Equals(row["m5ed_isValid"].ToString()) ? "" : (Convert.ToBoolean(row["m5ed_isValid"]) ? "<span style='color:green'> Agree</span>" : "<span style='color:red'> Disagree</span>")) + "<br />";
                EffectTR += string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(row["createDate"])) + "&nbsp;&nbsp;&nbsp;&nbsp;" + row["createName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;"
                    + (string.Empty.Equals(row["m5ed_isValid"].ToString()) ? "" : (Convert.ToBoolean(row["m5ed_isValid"]) ? "<span style='color:green'> Agree</span>" : "<span style='color:red'> Disagree</span>")) + "<br />";

                if (!string.IsNullOrEmpty(row["m5ed_isAboutSafety"].ToString()))
                {
                    EffectTR += " Impact Safety : " + (Convert.ToBoolean(row["m5ed_isAboutSafety"]) ? "<span  style='color:red'> Yes</span>" : "<span  style='color:green'> No</span>") + "<br />";
                }

                e.Row.Cells[1].Text += "&nbsp;&nbsp;&nbsp;&nbsp;" + row["m5ed_msg"].ToString() + "<br />";
                EffectTR += "&nbsp;&nbsp;&nbsp;&nbsp;" + row["m5ed_msg"].ToString() + "<br />";

                if (!string.IsNullOrEmpty(row["m5ed_fileName"].ToString()))
                {
                    e.Row.Cells[1].Text += "Attachment:<a href='" + row["m5ed_filePath"].ToString() + "' target='_blank'>" + row["m5ed_fileName"].ToString() + "</a>" + "<br />";
                    EffectTR += "Attachment:<a href='" + row["m5ed_filePath"].ToString() + "' target='_blank'>" + row["m5ed_fileName"].ToString() + "</a>" + "<br />";
                }

                e.Row.Cells[1].Text += "<br />";
                EffectTR += "<br />";
            }
            EffectTR += " </td>";
            #endregion
            #region 绑定责任人，同时设置EffectTR
            EffectTR += " <td class=\"FixedGridRight\">";
            foreach (DataRow row in this.GetEffectUser(_effectID).Rows)
            {
                //Email
                if (!string.IsNullOrEmpty(row["email"].ToString()))
                {
                    hidEmailAddress.Value = hidEmailAddress.Value.Replace(row["email"].ToString() + ";", "");
                    hidEmailAddress.Value += row["email"].ToString() + ";";
                }

                e.Row.Cells[2].Text += row["m5eu_userName"].ToString()+"<br />";
                EffectTR += row["m5eu_userName"].ToString() + "<br />";
                //e.Row.Cells[2].Text += row["m5eu_userName"].ToString() + "-" + row["englishName"].ToString() + "<br />";
                //EffectTR += row["m5eu_userName"].ToString() + " - " + row["englishName"].ToString() + "<br />";
            }
            EffectTR += " </td>";
            #endregion
            EffectTR += "</tr>";
        }
    }
    //protected void gv2_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataRowView rowView = (DataRowView)e.Row.DataItem;

    //        string _no = lblNo.Text;
    //        string _validID = rowView["m5v_id"].ToString();

    //        ValidTR += "<tr><td class=\"FixedGridHeight\" style=\"height:50px;\"></td>";
    //        ValidTR += "<td class=\"FixedGridLeft\" style=\"text-align: center;\" colspan=\"4\">" + rowView["m5v_descEn"].ToString() + "</td>";
    //        ValidTR += "<td colspan=\"13\" style=\"text-align: left;\">";
    //        #region 绑定留言
    //        foreach (DataRow row in this.GetValidDetail(_no, _validID).Rows)
    //        {
    //            //如果有验证意见，则无法更改
    //            btnValid.Enabled = false;
    //            btnNotValid.Enabled = false;

    //            e.Row.Cells[1].Text += string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(row["createDate"])) + "&nbsp;&nbsp;&nbsp;&nbsp;" + row["createName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + (Convert.ToBoolean(row["m5vd_isValid"]) ? " YES" : "NO") + "<br />";
    //            ValidTR += string.Format("{0:yyyy-MM-dd HH:mm}", Convert.ToDateTime(row["createDate"])) + "&nbsp;&nbsp;&nbsp;&nbsp;" + row["createName"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + (Convert.ToBoolean(row["m5vd_isValid"]) ? " YES" : "NO") + "<br />";

    //            e.Row.Cells[1].Text += "&nbsp;&nbsp;&nbsp;&nbsp;" + row["m5vd_msg"].ToString() + "<br />";
    //            ValidTR += "&nbsp;&nbsp;&nbsp;&nbsp;" + row["m5vd_msg"].ToString() + "<br />";

    //            if (!string.IsNullOrEmpty(row["m5vd_fileName"].ToString()))
    //            {
    //                e.Row.Cells[1].Text += "Attachment:<a href='" + row["m5vd_filePath"].ToString() + "' target='_blank'>" + row["m5vd_fileName"].ToString() + "</a>" + "<br />";
    //                ValidTR += "Attachment:<a href='" + row["m5vd_filePath"].ToString() + "' target='_blank'>" + row["m5vd_fileName"].ToString() + "</a>" + "<br />";
    //            }

    //            e.Row.Cells[1].Text += "<br />";
    //            ValidTR += "<br />";
    //        }
    //        ValidTR += " </td>";
    //        #endregion
    //        #region 绑定责任人
    //        ValidTR += "<td class=\"FixedGridRight\">";
    //        foreach (DataRow row in this.GetValidUser(_validID).Rows)
    //        {
    //            if (!string.IsNullOrEmpty(row["email"].ToString()))
    //            {
    //                hidEmailAddress.Value = hidEmailAddress.Value.Replace(row["email"].ToString() + ";", "");
    //                hidEmailAddress.Value += row["email"].ToString() + ";";
    //            }

    //            e.Row.Cells[2].Text += row["m5vu_userName"].ToString() + " - " + row["englishName"].ToString() + "<br />";
    //            ValidTR += row["m5vu_userName"].ToString() + "-" + row["englishName"].ToString() + "<br />";
    //        }
    //        ValidTR += " </td>";
    //        #endregion
    //        ValidTR += "</tr>";
    //    }
    //}
    protected void btnValid_Click(object sender, EventArgs e)
    {
        if (chkIsApprove.Checked)
        {
            if (labUExcutor.Text == "")
            {
                this.ltlAlert.Text = "alert('The specified executed by');";
                return;
            }
            else
            {
                if (CheckHaveMassageByNo(lblNo.Text))
                {
                    #region 操作数据库
                    try
                    {
                        string strName = "sp_m5_agreeM5Mstr";
                        SqlParameter[] param = new SqlParameter[7];
                        param[0] = new SqlParameter("@no", lblNo.Text);
                        param[1] = new SqlParameter("@agree", true);
                        //param[2] = new SqlParameter("@notAgree", false);
                        // param[3] = new SqlParameter("@effDate", labEffDate.Text);
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
                            chkValid.Checked = true;

                            btnValid.Text = "Agree（By " + Session["eName"].ToString() + "）";
                            btnValid.Enabled = false;
                            //btnValid.Visible = false;
                            btnNotValid.Visible = false;

                            //lblExcutor.Visible = false;

                            #region 发送邮件
                            string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                            string to = hidEmailAddress.Value + hidUExcutorEmail.Value;
                            string copy = "";
                            string subject = "TCP ECN (产品变更通知书)";
                            string body = "";
                            #region 写Body
                            body += "<font style='font-size: 12px;'>编号No：" + lblNo.Text + "</font><br />";
                            body += "<font style='font-size: 12px;'>申请人Applicant：" + lblCreateName.Text + "</font><br />";
                            body += "<font style='font-size: 12px;'>项目Type：" + radProject.SelectedItem.Text + "</font><br />";
                            body += "<font style='font-size: 12px;'>是否同意变更Agree?： " + btnValid.Text + "</font><br />";
                            body += "<font style='font-size: 12px;'>执行日期Excutive Date：" + labEffDate.Text + "</font><br />";
                            body += "<font style='font-size: 12px;'>申请内容Contents：" + DESC + "</font><br />";
                            body += "<font style='font-size: 12px;'>申请理由Reasons：" + REASON + "</font><br />";
                            body += "<font style='font-size: 12px;'>执行人Executed By：" + labUExcutor.Text + "</font><br />";
                            body += "<br /><br />";
                            body += "<font style='font-size: 12px;'>详情请登陆 <a href="+baseDomain.getPortalWebsite()+"/product/m5_detail.aspx?no=" + lblNo.Text + " >"+baseDomain.getPortalWebsite()+"/product/m5_detail.aspx?no=" + lblNo.Text + "</a> </font><br />";
                            body += "<font style='font-size: 12px;'>For details please visit  <a href="+baseDomain.getPortalWebsite()+"/product/m5_detail.aspx?no=" + lblNo.Text + " >"+baseDomain.getPortalWebsite()+"/product/m5_detail.aspx?no=" + lblNo.Text + "</a>  </font>";
                            #endregion
                            if (!this.SendEmail(from, to, copy, subject, body))
                            {
                                this.ltlAlert.Text = "alert('Email sending failure');";
                            }
                            else
                            {
                                this.ltlAlert.Text = "alert('Email sending');";
                            }
                            #endregion


                            hidAgree.Value = "1";
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Alert("fail! Please contact the administrator");
                        return;
                    }
                    #endregion
                }
                else
                {
                    this.Alert("IF you want enter the button , need lever a message!");
                }
            }

        }
        else
        {
            this.ltlAlert.Text = "alert('fail! Please contact the administrator');";
        }

        BindEffect();
        BindValid();

        //if (chkIsApprove.Checked)
        //{
        //    hidNeed.Value = "2";//需要验证
        //    String strFileName = "";//文件名
        //    String strCateFolder = "/TecDocs/ECN/";
        //    String strExtension = "";//文件后缀
        //    String strSaveFileName = "";//储存名
        //    if (fileValid.PostedFile != null )
        //    {
        //        #region 上传文档例行处理
        //        if (fileValid.PostedFile.ContentLength > 8388608)
        //        {
        //            ltlAlert.Text = "alert('The maximum file upload is 8 MB');";
        //            return;
        //        }

        //        strFileName = Path.GetFileNameWithoutExtension(fileValid.PostedFile.FileName);
        //        strExtension = Path.GetExtension(fileValid.PostedFile.FileName);
        //        strSaveFileName = DateTime.Now.ToFileTime().ToString();

        //        try
        //        {
        //            fileValid.PostedFile.SaveAs(Server.MapPath(strCateFolder) + "\\" + strSaveFileName + strExtension);
        //        }
        //        catch
        //        {
        //            ltlAlert.Text = "alert('Attachment upload failed');";
        //            return;
        //        }
        //        #endregion
        //    }

        //    #region 操作数据库
        //    try
        //    {
        //        string strName = "sp_m5_needValid";
        //        SqlParameter[] param = new SqlParameter[9];
        //        param[0] = new SqlParameter("@no", lblNo.Text);
        //        param[1] = new SqlParameter("@valid", true);
        //       // param[2] = new SqlParameter("@notValid", false);
        //        param[3] = new SqlParameter("@validMsg", txtValidMsg.Text.Trim());
        //        param[4] = new SqlParameter("@fileName", strFileName + strExtension);
        //        param[5] = new SqlParameter("@filePath", strCateFolder + strSaveFileName + strExtension);
        //        param[6] = new SqlParameter("@uID", Session["uID"].ToString());
        //        param[7] = new SqlParameter("@uName", Session["uName"].ToString());
        //        param[8] = new SqlParameter("@retValue", SqlDbType.Bit);
        //        param[8].Direction = ParameterDirection.Output;

        //        SqlHelper.ExecuteNonQuery(strConn, CommandType.StoredProcedure, strName, param);

        //        if (!Convert.ToBoolean(param[8].Value))
        //        {
        //            chkValid.Checked = !chkValid.Checked;
        //            this.Alert("fail! Please contact the administrator");
        //        }
        //        else
        //        {
        //            chkValid.Checked = true;
        //            btnValid.Text = "Agree（By " + Session["eName"].ToString() + "）";
        //            btnValid.Enabled = false;
        //            btnNotValid.Visible = false;

        //           // btnProdApp.Visible = true;

        //            txtValidMsg.BackColor = Color.White;
        //            txtValidMsg.ReadOnly = true;

        //            fileValid.Visible = false;
        //            hlinkValid.Visible = true;
        //            if (!string.IsNullOrEmpty(strFileName))
        //            {
        //                hlinkValid.Text = "Attachment:" + strFileName + strExtension;
        //                hlinkValid.NavigateUrl = strCateFolder + strSaveFileName + strExtension;
        //            }
        //            #region 发送邮件
        //            string from = "";
        //            string to = hidEmailAddress.Value;
        //            string copy = "";
        //            string subject = "TCP ECN 【ECN表单已经通过验证】";
        //            string body = "";
        //            #region 写Body
        //            body += "<font style='font-size: 12px;'>您好：</font><br />";
        //            body += "<font style='font-size: 12px;'>　　您创建的编号No：" + lblNo.Text + "的ECN表单已经通过验证，</font><br />";
        //            body += "<font style='font-size: 12px;'>　　项目Type：" + radProject.SelectedItem.Text + "</font><br />";
        //            body += "<font style='font-size: 12px;'>　　申请内容Contents：" + txtDesc.Text + "</font><br />";
        //            body += "<font style='font-size: 12px;'>　　申请理由Reasons：" + txtReason.Text + "</font><br />";
        //            body += "<br /><br />";
        //            body += "<font style='font-size: 12px;'>详情请登陆 "+baseDomain.getPortalWebsite()+" </font><br />";
        //            body += "<font style='font-size: 12px;'>For details please visit "+baseDomain.getPortalWebsite()+" </font>";
        //            #endregion
        //            if (!this.SendEmail(from, to, copy, subject, body))
        //            {
        //                this.ltlAlert.Text = "alert('Email sending failure');";
        //            }
        //            else
        //            {
        //                this.ltlAlert.Text = "alert('Email sending');";
        //            }
        //            #endregion
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Alert("fail! Please contact the administrator");
        //    }
        //    #endregion
        //}
        //else
        //{
        //    this.ltlAlert.Text = "alert('fail! Please contact the administrator');";
        //}

        //BindEffect();
        //BindValid();
    }

    private bool CheckHaveMassageByNo(string No)
    {
        try
        {
            string sqlstr = "sp_m5_CheckHaveMassageByNo";

            SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@no",No)

             };



            return  Convert.ToBoolean( SqlHelper.ExecuteScalar(strConn, CommandType.StoredProcedure, sqlstr, param));
        }
        catch
        {
            return false;

        }
    }
    protected void btnNotValid_Click(object sender, EventArgs e)
    {
        if (chkIsApprove.Checked)
        {
            if (CheckHaveMassageByNo(lblNo.Text))
            {
                #region 操作数据库
                try
                {
                    string strName = "sp_m5_agreeM5Mstr";
                    SqlParameter[] param = new SqlParameter[7];
                    param[0] = new SqlParameter("@no", lblNo.Text);
                    param[1] = new SqlParameter("@agree", false);
                    // param[2] = new SqlParameter("@notAgree", true);
                    // param[3] = new SqlParameter("@effDate", string.Empty);
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

                        chkNotValid.Checked = true;
                        btnNotValid.Text = "Disagree（By " + Session["eName"].ToString() + "）";
                        btnNotValid.Enabled = false;
                        btnValid.Visible = false;

                        #region 发送邮件
                        string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                        string to = hidEmailAddress.Value;
                        string copy = "";
                        string subject = "TCP ECN (产品变更通知书)";
                        string body = "";
                        #region 写Body
                        body += "<font style='font-size: 12px;'>编号No：" + lblNo.Text + "</font><br />";
                        body += "<font style='font-size: 12px;'>申请人Applicant：" + lblCreateName.Text + "</font><br />";
                        body += "<font style='font-size: 12px;'>项目Type：" + radProject.SelectedItem.Text + "</font><br />";
                        body += "<font style='font-size: 12px;'>是否同意变更Agree？： " + btnNotValid.Text + "</font><br />";
                        body += "<font style='font-size: 12px;'>执行日期Excutive Date：" + labEffDate.Text + "</font><br />";
                        body += "<font style='font-size: 12px;'>申请内容Contents：" + DESC + "</font><br />";
                        body += "<font style='font-size: 12px;'>申请理由Reasons：" + REASON + "</font><br />";
                        body += "<font style='font-size: 12px;'>执行人Executed By：" + labUExcutor.Text + "</font><br />";
                        body += "<br /><br />";
                        body += "<font style='font-size: 12px;'>详情请登陆 <a href="+baseDomain.getPortalWebsite()+"/product/m5_detail.aspx?no=" + lblNo.Text + " >"+baseDomain.getPortalWebsite()+"/product/m5_detail.aspx?no=" + lblNo.Text + "</a> </font><br />";
                        body += "<font style='font-size: 12px;'>For details please visit  <a href="+baseDomain.getPortalWebsite()+"/product/m5_detail.aspx?no=" + lblNo.Text + " >"+baseDomain.getPortalWebsite()+"/product/m5_detail.aspx?no=" + lblNo.Text + "</a>  </font>";
                        #endregion
                        if (!this.SendEmail(from, to, copy, subject, body))
                        {
                            this.ltlAlert.Text = "alert('Email sending failure');";
                        }
                        else
                        {
                            this.ltlAlert.Text = "alert('Email sending');";
                        }
                        #endregion
                        hidAgree.Value = "1";
                    }

                }
                catch (Exception ex)
                {
                    this.Alert("fail! Please contact the administrator");
                }
                #endregion
            }
            else
            {
                this.Alert("IF you want enter the button , need lever a message!");
            }
        }
        else
        {
            this.ltlAlert.Text = "alert('fail! Please contact the administrator');";
        }

        BindEffect();
        BindValid();

       
    }
    /// <summary>
    /// 功能修改，先全都注释，后面重新写
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAgree_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnNotAgree_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnExcute_Click(object sender, EventArgs e)
    {
        //if (chkAgree.Checked)
        //{
        String strFileName = "";//文件名
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
                ltlAlert.Text = "alert('Attachment upload failed');";
                return;
            }
            #endregion
        }

        if (string.IsNullOrEmpty(txtExcuteMsg.Text))
        {
            this.Alert("Message can not be empty");
            return;
        }

        #region 操作数据库
        try
        {
            string strName = "sp_m5_saveExecutor";
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@no", lblNo.Text);
            param[1] = new SqlParameter("@excuteMsg", txtExcuteMsg.Text.Trim());
            param[2] = new SqlParameter("@fileName", strFileName + strExtension);
            param[3] = new SqlParameter("@filePath", strCateFolder + strSaveFileName + strExtension);
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
                btnExcute.Text = "SAVED";
                btnExcute.Enabled = false;

                chkIsExcute.Checked = true;

                txtExcuteMsg.BackColor = Color.White;
                txtExcuteMsg.ReadOnly = true;

                fileExcute.Visible = false;
                hlinkExcute.Visible = true;
                if (!string.IsNullOrEmpty(strFileName))
                {
                    hlinkExcute.Text = "Attachment:" + strFileName + strExtension;
                    hlinkExcute.NavigateUrl = strCateFolder + strSaveFileName + strExtension;
                }


              
            }
        }
        catch (Exception ex)
        {
            this.Alert("fail! Please contact the administrator");
        }
        #endregion
       

        BindEffect();
        BindValid();
    }
    
}