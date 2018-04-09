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
using System.Web.UI.WebControls.Expressions;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Data.SqlClient;
using System.Web.Mail;
using System.Text;
using Microsoft.Web.UI.WebControls;
using System.IO;

public partial class HR_app_newpage : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        adamClass chk = new adamClass();
        if (!IsPostBack)
        {
            txtAppDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtAppDate.Enabled = false;            
        }
    }
    public string GetConnString()
    {
        string conn = string.Empty;
        #region
        //if (Request.UserHostAddress.Contains("10.3.0.") || Request.UserHostAddress.Contains("10.3.1.") || Request.UserHostAddress.Contains("10.3.2.") || Request.UserHostAddress.Contains("10.3.3.") || Request.UserHostAddress.Contains("10.3.4.") || Request.UserHostAddress.Contains("10.3.5."))
        //{
        //    return conn = "SqlConn.Conn1";
        //}
        //else if (Request.UserHostAddress.Contains("10.3.10.") || Request.UserHostAddress.Contains("10.3.20."))
        //{
        //    return conn = "SqlConn.Conn2";
        //}
        //else if (Request.UserHostAddress.Contains("10.3.30.") || Request.UserHostAddress.Contains("10.3.40."))
        //{
        //    return conn = "SqlConn.Conn5";
        //}
        //else if (Request.UserHostAddress.Contains("10.3.50."))
        //{
        //    return conn = "SqlConn.Conn8";
        //}
        //else if (Request.UserHostAddress.Contains("10.3.70."))
        //{
        //    return conn = "SqlConn.Conn10";
        //}
        //else if (Request.UserHostAddress.Contains("10.1."))
        //{
        //    return conn = "SqlConn.Conn99";
        //}
        //else if (Request.UserHostAddress.Contains("10.3.60."))
        //{
        //    return conn = "SqlConn.Conn11";
        //}
        //else
        //{
        //    return conn = "SqlConn.Conn";
        //}
        #endregion
        return conn = "SqlConn.Conn";
        
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region 基本信息

        #region 应聘职位
        string proc = string.Empty;
        if (txtAppProcess.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写应聘职位!')";
            return;
        }
        else 
        {
            proc = txtAppProcess.Text;
        }
        #endregion
        #region 应聘日期
        string appdate = string.Empty;
        if (txtAppDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写应聘日期!')";
            return;
        }
        else
        {
            appdate = txtAppDate.Text;
        }
        #endregion
        #region 可到职日期
        string arrdate = string.Empty;
        if (txtArriveDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写可到职日期!')";
            return;
        }
        else
        {
            arrdate = txtArriveDate.Text;
        }
        #endregion
        #region 应聘渠道
        string apptype=string.Empty;
        if (rbAppType1.Checked)
        {
            apptype = rbAppType1.Text;
        }
        else if (rbAppType2.Checked)
        {
            apptype = rbAppType2.Text;
        }
        else if (rbAppType3.Checked)
        {
            apptype = rbAppType3.Text;
        }
        else if (rbAppType4.Checked)
        {
            apptype = rbAppType4.Text;
        }
        else if (rbAppType5.Checked)
        {
            apptype = rbAppType5.Text;
        }

        if (apptype == string.Empty)
        {
            ltlAlert.Text = "alert('请选择应聘渠道!')";
            return;
        }
        #endregion
        #region 姓名
        string name = string.Empty;
        if (txtName.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写姓名!')";
            return;
        }
        else
        {
            name = txtName.Text;
        }
        #endregion
        #region 出生年月
        string birthday = string.Empty;
        if (txtBirthDate.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写出生年月!')";
            return;
        }
        else
        {
            birthday = txtBirthDate.Text;
        }
        #endregion
        #region 籍贯
        string place = string.Empty;
        if (txtPlace.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写籍贯!')";
            return;
        }
        else
        {
            place = txtPlace.Text;
        }
        #endregion
        #region 民族
        string nation = string.Empty;
        if (txtNation.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写民族!')";
            return;
        }
        else
        {
            nation = txtNation.Text;
        }
        #endregion
        #region 身份证号码
        string ic = string.Empty;
        if (txtIC.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写身份证号码!')";
            return;
        }
        else
        {
            ic = txtIC.Text;
            if (ic.Length == 18)
            {
                string icbirth = ic.Substring(6, 8);
                DateTime birth = Convert.ToDateTime(txtBirthDate.Text);
                string str = string.Format("{0:yyyyMMdd}", birth);
                if (str != icbirth)
                {
                    ltlAlert.Text = "alert('出生年月有误!')";
                    return;
                }
            }
            else
            {
                string icbirth = ic.Substring(6, 6);
                DateTime birth = Convert.ToDateTime(txtBirthDate.Text);
                string str = string.Format("{0:yyMMdd}", birth);
                if (str != icbirth)
                {
                    ltlAlert.Text = "alert('出生年月有误!')";
                    return;
                }
            }
        }
        #endregion
        #region 性别
        string sex = string.Empty;
        if (txtSex.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写性别!')";
            return;
        }
        else
        {
            if (txtSex.Text != "男" && txtSex.Text != "女")
            {
                ltlAlert.Text = "alert('性别只能是男或女!')";
                return;
            }
            else
            {
                sex = txtSex.Text;
                string icard = txtIC.Text;
                if (icard.Length == 18)
                {
                    int icsex = Convert.ToInt32(icard.Substring(16, 1));
                    if (icsex % 2 == 0)
                    {
                        string isex = "女";
                        if (isex != txtSex.Text)
                        {
                            ltlAlert.Text = "alert('性别输入错误!')";
                            return;
                        }
                    }
                    else
                    {
                        string isex = "男";
                        if (isex != txtSex.Text)
                        {
                            ltlAlert.Text = "alert('性别输入错误!')";
                            return;
                        }
                    }
                }
                else
                {
                    int icsex1 = Convert.ToInt32(icard.Substring(12, 3));
                    if (icsex1 % 2 == 0)
                    {
                        string isex = "女";
                        if (isex != txtSex.Text)
                        {
                            ltlAlert.Text = "alert('性别输入错误!')";
                            return;
                        }
                    }
                    else
                    {
                        string isex = "男";
                        if (isex != txtSex.Text)
                        {
                            ltlAlert.Text = "alert('性别输入错误!')";
                            return;
                        }
                    }
                }
            }
        }
        #endregion
        #region 婚姻状况
        string marr = string.Empty;
        if (rbMarr1.Checked)
        {
            marr = rbMarr1.Text;
        }
        else if (rbMarr2.Checked)
        {
            marr = rbMarr2.Text; 
        }
        else if (rbMarr3.Checked)
        {
            marr = rbMarr3.Text; 
        }
        if (marr == string.Empty)
        {
            ltlAlert.Text = "alert('请选择婚姻状况!')";
            return;
        }
        #endregion
        #region 联系电话
        string phone = string.Empty;
        if (txtPhone.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写联系电话!')";
            return;
        }
        else
        {
            phone = txtPhone.Text;
        }
        #endregion
        #region 通信地址
        string address = string.Empty;
        if (txtAddress.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写通信地址!')";
            return;
        }
        else
        {
            address = txtAddress.Text;
        }
        #endregion
        #region 外语种类
        string language = string.Empty;
        if (txtLanguage.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填外语种类!')";
            return;
        }
        else
        {
            language = txtLanguage.Text;
        }
        #endregion
        #region 外语熟练程度
        string laguLevel = string.Empty;
        if (rbLagu1.Checked)
        {
            laguLevel = rbLagu1.Text;
        }
        else if (rbLagu2.Checked)
        {
            laguLevel = rbLagu2.Text;
        }
        else if (rbLagu3.Checked)
        {
            laguLevel = rbLagu3.Text;
        }
        if (laguLevel == string.Empty)
        {
            ltlAlert.Text = "alert('请选择外语熟练程度!')";
            return;
        }
        #endregion
        #region 计算机证书
        string computer = string.Empty;
        if (txtComputer.Text == string.Empty)
        {
            ltlAlert.Text = "alert('请填写计算机证书!')";
            return;
        }
        else
        {
            computer = txtComputer.Text;
        }
        #endregion
        #region 计算机熟练程度
        string computerLevel = string.Empty;
        if (rbComputer1.Checked)
        {
            computerLevel = rbComputer1.Text;
        }
        else if (rbComputer2.Checked)
        {
            computerLevel = rbComputer2.Text;
        }
        else if (rbComputer3.Checked)
        {
            computerLevel = rbComputer3.Text;
        }
        if (computerLevel == string.Empty)
        {
            ltlAlert.Text = "alert('请选择计算机熟练程度!')";
            return;
        }
        #endregion

        #endregion
        #region 工作经历

        #region 工作经历一
        string SStime1 = txtSStime1.Text;
        string Depart1 = txtDepart1.Text;
        string Proc1 = txtProc1.Text;
        string LeaveReason1 = txtLeaveReason1.Text;
        string LeaveMoney1 = txtLeaveMoney1.Text;
        string References1 = txtReferences1.Text;
        string ConNum1 = txtConNum1.Text;
        if (SStime1 == string.Empty && Depart1 == string.Empty && Proc1 == string.Empty && LeaveReason1 == string.Empty && LeaveMoney1 == string.Empty && References1 == string.Empty && ConNum1 == string.Empty)
        {
            ltlAlert.Text = "alert('请至少填写一次工作经历!')";
            return;
        }
        else if (SStime1 == string.Empty || Depart1 == string.Empty || Proc1 == string.Empty || LeaveReason1 == string.Empty || LeaveMoney1 == string.Empty || References1 == string.Empty || ConNum1 == string.Empty)
        {
            ltlAlert.Text = "alert('请填写完整第一行工作经历!')";
            return;
        }
        #endregion
        #region 工作经历二
        string SStime2 = txtSStime2.Text;
        string Depart2 = txtDepart2.Text;
        string Proc2 = txtProc2.Text;
        string LeaveReason2 = txtLeaveReason2.Text;
        string LeaveMoney2 = txtLeaveMoney2.Text;
        string References2 = txtReferences2.Text;
        string ConNum2 = txtConNum2.Text;
        if (SStime2 != string.Empty || Depart2 != string.Empty || Proc2 != string.Empty || LeaveReason2 != string.Empty || LeaveMoney2 != string.Empty || References2 != string.Empty || ConNum2 != string.Empty)
        {
            if (SStime2 == string.Empty || Depart2 == string.Empty || Proc2 == string.Empty || LeaveReason2 == string.Empty || LeaveMoney2 == string.Empty || References2 == string.Empty || ConNum2 == string.Empty)
            {
                ltlAlert.Text = "alert('请填写完整第二行工作经历!')";
                return;
            }
        }
        #endregion
        #region 工作经历三
        string SStime3 = txtSStime3.Text;
        string Depart3 = txtDepart3.Text;
        string Proc3 = txtProc3.Text;
        string LeaveReason3 = txtLeaveReason3.Text;
        string LeaveMoney3 = txtLeaveMoney3.Text;
        string References3 = txtReferences3.Text;
        string ConNum3 = txtConNum3.Text;
        if (SStime3 != string.Empty || Depart3 != string.Empty || Proc3 != string.Empty || LeaveReason3 != string.Empty || LeaveMoney3 != string.Empty || References3 != string.Empty || ConNum3 != string.Empty)
        {
            if (SStime3 == string.Empty || Depart3 == string.Empty || Proc3 == string.Empty || LeaveReason3 == string.Empty || LeaveMoney3 == string.Empty || References3 == string.Empty || ConNum3 == string.Empty)
            {
                ltlAlert.Text = "alert('请填写完整第三行工作经历!')";
                return;
            }
        }
        #endregion
        #region 工作经历四
        string SStime4 = txtSStime4.Text;
        string Depart4 = txtDepart4.Text;
        string Proc4 = txtProc4.Text;
        string LeaveReason4 = txtLeaveReason4.Text;
        string LeaveMoney4 = txtLeaveMoney4.Text;
        string References4 = txtReferences4.Text;
        string ConNum4 = txtConNum4.Text;
        if (SStime4 != string.Empty || Depart4 != string.Empty || Proc4 != string.Empty || LeaveReason4 != string.Empty || LeaveMoney4 != string.Empty || References4 != string.Empty || ConNum4 != string.Empty)
        {
            if (SStime4 == string.Empty || Depart4 == string.Empty || Proc4 == string.Empty || LeaveReason4 == string.Empty || LeaveMoney4 == string.Empty || References4 == string.Empty || ConNum4 == string.Empty)
            {
                ltlAlert.Text = "alert('请填写完整第四行工作经历!')";
                return;
            }
        }
        #endregion

        #endregion
        #region 教育背景

        #region 教育背景一
        string ESStime1 = txtESStime1.Text;
        string GradSchool1 = txtGradSchool1.Text;
        string Profess1 = txtProfess1.Text;
        string Degree1 = txtDegree1.Text;
        string Examtype1 = string.Empty;
        if (rbExamtype1.Checked)
        {
            Examtype1 = rbExamtype1.Text;
        }
        else if (rbExamtype2.Checked)
        {
            Examtype1 = rbExamtype2.Text;
        }
        if (ESStime1 == string.Empty && GradSchool1 == string.Empty && Profess1 == string.Empty && Degree1 == string.Empty)
        {
            ltlAlert.Text = "alert('请至少填写一次教育背景!')";
            return;
        }
        else if (ESStime1 == string.Empty || GradSchool1 == string.Empty || Profess1 == string.Empty || Degree1 == string.Empty || Examtype1 == string.Empty)
        {
            ltlAlert.Text = "alert('请填写完整第一行教育背景!')";
            return;
        }
        #endregion
        #region 教育背景二
        string ESStime2 = txtESStime2.Text;
        string GradSchool2 = txtGradSchool2.Text;
        string Profess2 = txtProfess2.Text;
        string Degree2 = txtDegree2.Text;
        string Examtype2 = string.Empty;
        if (rbExamtype3.Checked)
        {
            Examtype2 = rbExamtype3.Text;
        }
        else if (rbExamtype4.Checked)
        {
            Examtype2 = rbExamtype4.Text;
        }
        if (ESStime2 != string.Empty || GradSchool2 != string.Empty || Profess2 != string.Empty || Degree2 != string.Empty || Examtype2 != string.Empty)
        {
            if (ESStime2 == string.Empty || GradSchool2 == string.Empty || Profess2 == string.Empty || Degree2 == string.Empty || Examtype2 == string.Empty)
            {
                ltlAlert.Text = "alert('请填写完整第二行教育背景!')";
                return;
            }
        }
        #endregion
        #region 教育背景三
        string ESStime3 = txtESStime3.Text;
        string GradSchool3 = txtGradSchool3.Text;
        string Profess3 = txtProfess3.Text;
        string Degree3 = txtDegree3.Text;
        string Examtype3 = string.Empty;
        if (rbExamtype5.Checked)
        {
            Examtype3 = rbExamtype5.Text;
        }
        else if (rbExamtype6.Checked)
        {
            Examtype3 = rbExamtype6.Text;
        }
        if (ESStime3 != string.Empty || GradSchool3 != string.Empty || Profess3 != string.Empty || Degree3 != string.Empty || Examtype3 != string.Empty)
        {
            if (ESStime3 == string.Empty || GradSchool3 == string.Empty || Profess3 == string.Empty || Degree3 == string.Empty || Examtype3 == string.Empty)
            {
                ltlAlert.Text = "alert('请填写完整第三行教育背景!')";
                return;
            }
        }
        #endregion
        #region 教育背景四
        string ESStime4 = txtESStime4.Text;
        string GradSchool4 = txtGradSchool4.Text;
        string Profess4 = txtProfess4.Text;
        string Degree4 = txtDegree4.Text;
        string Examtype4 = string.Empty;
        if (rbExamtype7.Checked)
        {
            Examtype4 = rbExamtype7.Text;
        }
        else if (rbExamtype8.Checked)
        {
            Examtype4 = rbExamtype8.Text;
        }
        if (ESStime4 != string.Empty || GradSchool4 != string.Empty || Profess4 != string.Empty || Degree4 != string.Empty || Examtype4 != string.Empty)
        {
            if (ESStime4 == string.Empty || GradSchool4 == string.Empty || Profess4 == string.Empty || Degree4 == string.Empty || Examtype4 == string.Empty)
            {
                ltlAlert.Text = "alert('请填写完整第四行教育背景!')";
                return;
            }
        }
        #endregion

        #endregion
        #region 其他说明

        #region 工作状态
        string WorkStatus = string.Empty;
        if (rbWorkStatus1.Checked)
        {
            WorkStatus = rbWorkStatus1.Text;
        }
        else if (rbWorkStatus2.Checked)
        {
            WorkStatus = rbWorkStatus2.Text;
        }
        else if (rbWorkStatus3.Checked)
        {
            WorkStatus = rbWorkStatus3.Text;
        }
        if (WorkStatus == string.Empty)
        {
            ltlAlert.Text = "alert('请选择工作状态!')";
            return;
        }
        #endregion
        #region 能否提供离职证明
        string LevelCer = string.Empty;
        if (rbLevelCer1.Checked)
        {
            LevelCer = rbLevelCer1.Text;
        }
        else if (rbLevelCer2.Checked)
        {
            LevelCer = rbLevelCer2.Text;
        }
        if (LevelCer == string.Empty)
        {
            ltlAlert.Text = "alert('请选择是否能够提供离职证明!')";
            return;
        }
        #endregion
        #region 能否提供有效证明文件
        string ValidDoc = string.Empty; 
        if (rbValidDoc1.Checked)
        {
            ValidDoc = rbValidDoc1.Text;
        }
        else if (rbValidDoc2.Checked)
        {
            ValidDoc = rbValidDoc2.Text;
        }
        if (ValidDoc == string.Empty)
        {
            ltlAlert.Text = "alert('请选择是否能够提供有效证明文件!')";
            return;
        }
        #endregion
        #region 是否有亲友在本公司任职
        string ship = string.Empty;
        string ShipName = string.Empty;
        string Ships = string.Empty;
        string ShipDept = string.Empty;
        string ShipProc = string.Empty;
        if (rbShip1.Checked)
        {
            ship = rbShip1.Text;
        }
        if (rbShip2.Checked)
        {
            ship = rbShip2.Text;
            ShipName = txtShipName.Text;
            Ships = txtShip.Text;
            ShipDept = txtShipDept.Text;
            ShipProc = txtShipProc.Text;
        }
        if (ship == string.Empty)
        {
            ltlAlert.Text = "alert('请选择是否有亲友在本公司任职!')";
            return;
        }
        else if (rbShip2.Checked)
        {
            if (txtShipName.Text == string.Empty || txtShip.Text == string.Empty)
            {
                ltlAlert.Text = "alert('请填写亲友姓名以及之间的关系!')";
                return;
            }
        }
        #endregion
        #region 是否有疾病/职业病史
        string Dise = string.Empty;
        string DiseText = string.Empty;
        if (rbDise1.Checked)
        {
            Dise = rbDise1.Text;
        }
        if (rbDise2.Checked)
        {
            Dise = rbDise2.Text;
            DiseText = txtDisease.Text;
        }
        if (Dise == string.Empty)
        {
            ltlAlert.Text = "alert('请选择是否有疾病/职业病史!')";
            return;
        }
        else if (rbDise2.Checked)
        {
            if (txtDisease.Text == string.Empty)
            {
                ltlAlert.Text = "alert('请填写疾病/职业病史!')";
                return;
            }
        }
        #endregion
        #region 是否曾签署有效地“竞争行业禁止协议”
        string Protocol = string.Empty;
        if(rbProtocol1.Checked)
        {
            Protocol = rbProtocol1.Text;
        }
        else if (rbProtocol2.Checked)
        {
            Protocol = rbProtocol2.Text;
        }
        if (Protocol == string.Empty)
        {
            ltlAlert.Text = "alert('请选择是否曾签署有效的“竞争行业禁止协议”!')";
            return;
        }
        #endregion
        #region 是否曾犯刑案
        string Crime = string.Empty;
        string CrimeText = string.Empty;
        if (rbCrime1.Checked)
        {
            Crime = rbCrime1.Text;
        }
        else if (rbCrime2.Checked)
        {
            Crime = rbCrime2.Text;
            CrimeText = txtCrime.Text;
        }
        if (Crime == string.Empty)
        {
            ltlAlert.Text = "alert('请选择是否曾犯刑案!')";
            return;
        }
        else if (rbCrime2.Checked)
        {
            if (txtCrime.Text == string.Empty)
            {
                ltlAlert.Text = "alert('请填写刑案说明!')";
                return;
            }
        }
        #endregion
        #region 健康状况
        string Health = string.Empty;
        if (rbHealth1.Checked)
        {
            Health = rbHealth1.Text;
        }
        else if (rbHealth2.Checked)
        {
            Health = rbHealth2.Text;
        }
        else if (rbHealth3.Checked)
        {
            Health = rbHealth3.Text;
        }
        if (Health == string.Empty)
        {
            ltlAlert.Text = "alert('请选择健康状况!')";
            return;
        }
        #endregion
        
        #endregion

        if (addApplyInformation(proc,appdate,arrdate,apptype,name,birthday,place,nation,ic,sex,marr,phone,address,language,laguLevel,computer,computerLevel))
        {
            if (addUserWorkExp(name, ic, SStime1, Depart1, Proc1, LeaveReason1, LeaveMoney1, References1, ConNum1, SStime2, Depart2, Proc2, LeaveReason2, LeaveMoney2, References2, ConNum2, SStime3, Depart3, Proc3, LeaveReason3, LeaveMoney3, References3, ConNum3, SStime4, Depart4, Proc4, LeaveReason4, LeaveMoney4, References4, ConNum4))
            { 
                if(addUserEduBG(name, ic,ESStime1, GradSchool1,Profess1,Degree1,Examtype1,ESStime2,GradSchool2,Profess2,Degree2,Examtype2,ESStime3,GradSchool3,Profess3,Degree3,Examtype3,ESStime4,GradSchool4,Profess4,Degree4,Examtype4))
                {
                    if(addUserOther(name,ic,WorkStatus,LevelCer,ValidDoc,ship,ShipName,Ships,ShipDept,ShipProc,Dise,DiseText,Protocol,Crime,CrimeText,Health))
                    {
                        ltlAlert.Text = "alert('应聘登记表提交成功，请等待安排笔试或面试!')";
                        return;
                    }
                    else
                    {
                        ltlAlert.Text = "alert('其他说明提交失败!')";
                        return;
                    }
                }
                else
                {
                    ltlAlert.Text = "alert('教育背景提交失败!')";
                    return;
                }
            }
            else 
            {
                ltlAlert.Text = "alert('工作经历提交失败!')";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('基本信息提交失败!')";
            return;
        }
    }    

    #region 添加基本信息
    public bool addApplyInformation(string proc, string appdate, string arrdate, string apptype, string name, string birthday, string place, string nation
                                    ,string ic,string sex,string marr,string phone,string address,string language,string laguLevel,string computer
                                    , string computerLevel)
    {
        //GetConnString();
        string str = GetConnString().ToString();
        string conn = System.Configuration.ConfigurationManager.AppSettings[str];
        SqlParameter[] param = new SqlParameter[17];
        param[0] = new SqlParameter("@proc", proc);
        param[1] = new SqlParameter("@appdate", appdate);
        param[2] = new SqlParameter("@arrdate", arrdate);
        param[3] = new SqlParameter("@apptype", apptype);
        param[4] = new SqlParameter("@name", name);
        param[5] = new SqlParameter("@birthday", birthday);
        param[6] = new SqlParameter("@place", place);
        param[7] = new SqlParameter("@nation", nation);
        param[8] = new SqlParameter("@ic", ic);
        param[9] = new SqlParameter("@sex", sex);
        param[10] = new SqlParameter("@marr", marr);
        param[11] = new SqlParameter("@phone", phone);
        param[12] = new SqlParameter("@address", address);
        param[13] = new SqlParameter("@language", language);
        param[14] = new SqlParameter("@laguLevel", laguLevel);
        param[15] = new SqlParameter("@computer", computer);
        param[16] = new SqlParameter("@computerLevel", computerLevel);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_addApplyInformation", param));
    }
    #endregion

    #region 添加工作经历
    public bool addUserWorkExp(string name,string ic,string SStime1,string Depart1,string Proc1,string LeaveReason1,string LeaveMoney1,string References1
                            ,string ConNum1,string SStime2,string Depart2,string Proc2,string LeaveReason2,string LeaveMoney2,string References2
                            ,string ConNum2,string SStime3,string Depart3,string Proc3,string LeaveReason3,string LeaveMoney3,string References3
                            ,string ConNum3,string SStime4,string Depart4,string Proc4,string LeaveReason4,string LeaveMoney4,string References4
                            ,string ConNum4)
    {
        string str = GetConnString().ToString();
        string conn = System.Configuration.ConfigurationManager.AppSettings[str];
        SqlParameter[] param = new SqlParameter[30];
        param[0] = new SqlParameter("@name", name);
        param[1] = new SqlParameter("@ic", ic);
        param[2] = new SqlParameter("@SStime1", SStime1);
        param[3] = new SqlParameter("@Depart1", Depart1);
        param[4] = new SqlParameter("@Proc1", Proc1);
        param[5] = new SqlParameter("@LeaveReason1", LeaveReason1);
        param[6] = new SqlParameter("@LeaveMoney1", LeaveMoney1);
        param[7] = new SqlParameter("@References1", References1);
        param[8] = new SqlParameter("@ConNum1", ConNum1);
        param[9] = new SqlParameter("@SStime2", SStime2);
        param[10] = new SqlParameter("@Depart2", Depart2);
        param[11] = new SqlParameter("@Proc2", Proc2);
        param[12] = new SqlParameter("@LeaveReason2", LeaveReason2);
        param[13] = new SqlParameter("@LeaveMoney2", LeaveMoney2);
        param[14] = new SqlParameter("@References2", References2);
        param[15] = new SqlParameter("@ConNum2", ConNum2);
        param[16] = new SqlParameter("@SStime3", SStime3);
        param[17] = new SqlParameter("@Depart3", Depart3);
        param[18] = new SqlParameter("@Proc3", Proc3);
        param[19] = new SqlParameter("@LeaveReason3", LeaveReason3);
        param[20] = new SqlParameter("@LeaveMoney3", LeaveMoney3);
        param[21] = new SqlParameter("@References3", References3);
        param[22] = new SqlParameter("@ConNum3", ConNum3);
        param[23] = new SqlParameter("@SStime4", SStime4);
        param[24] = new SqlParameter("@Depart4", Depart4);
        param[25] = new SqlParameter("@Proc4", Proc4);
        param[26] = new SqlParameter("@LeaveReason4", LeaveReason4);
        param[27] = new SqlParameter("@LeaveMoney4", LeaveMoney4);
        param[28] = new SqlParameter("@References4", References4);
        param[29] = new SqlParameter("@ConNum4", ConNum4);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_addUserWorkExp", param));
    }
    #endregion

    #region 添加教育背景
    public bool addUserEduBG(string name, string ic,string ESStime1,string GradSchool1,string Profess1,string Degree1,string Examtype1
                        ,string ESStime2,string GradSchool2,string Profess2,string Degree2,string Examtype2,string ESStime3
                        ,string GradSchool3,string Profess3,string Degree3,string Examtype3,string ESStime4,string GradSchool4
                        , string Profess4, string Degree4, string Examtype4)
    {
        string str = GetConnString().ToString();
        string conn = System.Configuration.ConfigurationManager.AppSettings[str];
        SqlParameter[] param = new SqlParameter[22];
        param[0] = new SqlParameter("@name", name);
        param[1] = new SqlParameter("@ic", ic);
        param[2] = new SqlParameter("@ESStime1", ESStime1);
        param[3] = new SqlParameter("@GradSchool1", GradSchool1);
        param[4] = new SqlParameter("@Profess1", Profess1);
        param[5] = new SqlParameter("@Degree1", Degree1);
        param[6] = new SqlParameter("@Examtype1", Examtype1);
        param[7] = new SqlParameter("@ESStime2", ESStime2);
        param[8] = new SqlParameter("@GradSchool2", GradSchool2);
        param[9] = new SqlParameter("@Profess2", Profess2);
        param[10] = new SqlParameter("@Degree2", Degree2);
        param[11] = new SqlParameter("@Examtype2", Examtype2);
        param[12] = new SqlParameter("@ESStime3", ESStime3);
        param[13] = new SqlParameter("@GradSchool3", GradSchool3);
        param[14] = new SqlParameter("@Profess3", Profess3);
        param[15] = new SqlParameter("@Degree3", Degree3);
        param[16] = new SqlParameter("@Examtype3", Examtype3);
        param[17] = new SqlParameter("@ESStime4", ESStime4);
        param[18] = new SqlParameter("@GradSchool4", GradSchool4);
        param[19] = new SqlParameter("@Profess4", Profess4);
        param[20] = new SqlParameter("@Degree4", Degree4);
        param[21] = new SqlParameter("@Examtype4", Examtype4);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_addUserEduBG", param));
    }
    #endregion 

    #region 添加其他说明
    public bool addUserOther(string name, string ic,string WorkStatus,string LevelCer,string ValidDoc,string ship,string ShipName,string Ships
                            , string ShipDept, string ShipProc, string Dise, string DiseText, string Protocol, string Crime, string CrimeText, string Health)
    {
        string str = GetConnString().ToString();
        string conn = System.Configuration.ConfigurationManager.AppSettings[str];
        SqlParameter[] param = new SqlParameter[16];
        param[0] = new SqlParameter("@name", name);
        param[1] = new SqlParameter("@ic", ic);
        param[2] = new SqlParameter("@WorkStatus", WorkStatus);
        param[3] = new SqlParameter("@LevelCer", LevelCer);
        param[4] = new SqlParameter("@ValidDoc", ValidDoc);
        param[5] = new SqlParameter("@ship", ship);
        param[6] = new SqlParameter("@ShipName", ShipName);

        param[7] = new SqlParameter("@Ships", Ships);
        param[8] = new SqlParameter("@ShipDept", ShipDept);
        param[9] = new SqlParameter("@ShipProc", ShipProc);
        param[10] = new SqlParameter("@Dise", Dise);
        param[11] = new SqlParameter("@DiseText", DiseText);
        param[12] = new SqlParameter("@Protocol", Protocol);
        param[13] = new SqlParameter("@Crime", Crime);
        param[14] = new SqlParameter("@CrimeText", CrimeText);
        param[15] = new SqlParameter("@Health", Health);

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(chk.dsn0(), CommandType.StoredProcedure, "sp_app_addUserOther", param));
    }
    #endregion
}