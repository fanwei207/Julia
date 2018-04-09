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
using System.IO;
using adamFuncs;
using MinorP;
using Wage;
using System.Web.Mail;

public partial class new_MinorPOrder : BasePage
{
    adamClass adam = new adamClass();
    MinorPurchase mp = new MinorPurchase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        { 

            //ExportExcel();
            
            DropTypedatabind();
            DropDeptdatabind();

            InforDatabind(Convert.ToInt32(Request["Appid"]));  //申请信息赋值

            btnSave.Attributes.Add("onclick", "return confirm('确定提交给下一个审批者吗？');");

            btnFinish.Attributes.Add("onclick", "return confirm('确定审批完成，并可以采购申请物件？');");

        }
    }

    /// <summary>
    ///  初始绑定部门数据
    /// </summary>
    protected void DropDeptdatabind()
    {
        try
        {
            ListItem item;
            item = new ListItem("--", "0");
            dropDept.Items.Add(item);

            DataTable dtDropDept = HR.GetDepartment(Convert.ToInt32(Session["Plantcode"]));
            if (dtDropDept.Rows.Count > 0)
            {
                for (int i = 0; i < dtDropDept.Rows.Count; i++)
                {
                    item = new ListItem(dtDropDept.Rows[i].ItemArray[1].ToString(), dtDropDept.Rows[i].ItemArray[0].ToString());
                    dropDept.Items.Add(item);
                }
            }
            dropDept.SelectedIndex = 0;
        }
        catch
        {

        }
    }
    /// <summary>
    ///  初始绑定物品分类数据
    /// </summary>
    protected void DropTypedatabind()
    {
        try
        {
            ListItem item;
            item = new ListItem("--", "0");
            dropType.Items.Add(item);

            DataTable dtType = mp.MinorPType("");
            if (dtType.Rows.Count > 0)
            {
                for (int i = 0; i < dtType.Rows.Count; i++)
                {
                    item = new ListItem(dtType.Rows[i].ItemArray[1].ToString(), dtType.Rows[i].ItemArray[0].ToString());
                    dropType.Items.Add(item);
                }
            }
            dropType.SelectedIndex = 0;
        
        }
        catch
        {

        }
    }

    /// <summary>
    /// 初始申请信息，是否为新申请或是查看审批
    /// </summary>
    /// <param name="intAppid"></param>
    protected void InforDatabind(int intAppid)
    {
        if (intAppid != 0)
        {
            tbHeader1.Visible = false;
            tbHeader2.Visible = true;

            lblApid.Text = intAppid.ToString();
            DataTable dtInfor = mp.MinorPList(Convert.ToInt32(Session["Plantcode"]), 0, intAppid);
            if (dtInfor.Rows.Count > 0)
            {
                lblApper.Text = dtInfor.Rows[0].ItemArray[3].ToString();
                lblDept.Text = dtInfor.Rows[0].ItemArray[4].ToString();
                lblType.Text = dtInfor.Rows[0].ItemArray[6].ToString();
                lblQuantity.Text = dtInfor.Rows[0].ItemArray[7].ToString();
                lblprice.Text = dtInfor.Rows[0].ItemArray[9].ToString();
                lblPart.Text = dtInfor.Rows[0].ItemArray[5].ToString();
                lblSP.Text = dtInfor.Rows[0].ItemArray[10].ToString();

                lbltotal.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtInfor.Rows[0].ItemArray[7]) * Convert.ToDecimal(dtInfor.Rows[0].ItemArray[9].ToString()),3));

                if (dtInfor.Rows[0].ItemArray[1].ToString() != "A" || dtInfor.Rows[0].ItemArray[12].ToString() != Convert.ToString(Session["uName"]))
                {
                    btnAhSave.Visible = false;
                    btnSave.Visible = false;
                    btnFinish.Visible = false;
                    tbHeader3.Visible = false;
                }

                
            }
            else
            {
                lblApper.Text = "";
                lblDept.Text = "";
                lblType.Text = "";
                lblQuantity.Text = "";
                lblprice.Text = "";
                lblPart.Text = "";
                lblSP.Text = "";
                lblApid.Text = intAppid.ToString();


            }

        }
      
    }

    /// <summary>
    /// 返回申请查看界面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("/new/MinorPurchase.aspx");

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Check approve
        if (txtApplication.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须选择审批者！'); ";
            txtApplication.Focus();
            return;
        }

        if (txtEmail.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('必须填写自己的邮箱地址！'); ";
            txtEmail.Focus();
            return;
        }
        else
        {
            if (baseDomain.checkDomainOR(txtEmail.Text.Trim()))
            {
                ltlAlert.Text = "alert('邮箱地址填写不正确！'); ";
                txtEmail.Focus();
                return;
            }
        }


        // Application check
        if (Convert.ToInt32(Request["Appid"]) == 0)
        {
            if (dropDept.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('必须选择部门！'); ";
                dropDept.Focus();
                return;
            }

            if (dropType.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('必须选择零件分类！'); ";
                dropType.Focus();
                return;
            }

            if (txtQuantity.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('必须填写数量！'); ";
                txtQuantity.Focus();
                return;
            }
            else
            {
                try
                {
                    decimal decQ = Convert.ToDecimal(txtQuantity.Text.Trim());
                }
                catch
                {
                    ltlAlert.Text = "alert('数量填写不正确，请重新输入！'); ";
                    txtQuantity.Focus();
                    return;
                }
            }

            if (txtPrice.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('必须填写单价！'); ";
                txtPrice.Focus();
                return;
            }
            else
            {
                try
                {
                    decimal decP = Convert.ToDecimal(txtPrice.Text.Trim());
                }
                catch
                {
                    ltlAlert.Text = "alert('单价填写不正确，请重新输入！'); ";
                    txtPrice.Focus();
                    return;
                }
            }

            if (txtPart.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('必须填写零件！'); ";
                txtPart.Focus();
                return;
            }

            if (txtSP.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('必须填写供应商！'); ";
                txtPart.Focus();
                return;
            }
                
        }

        //Save the application 
        //--- lblApid.Text == "0"   New Apply    == "1" Exist Apply
        int intflag;
        if (Convert.ToInt32(Request["Appid"]) == 0)
        {
            intflag = mp.SaveApplication(Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropType.SelectedValue), Convert.ToDecimal(txtQuantity.Text.Trim()), Convert.ToDecimal(txtPrice.Text.Trim()), txtPart.Text.Trim(), txtSP.Text.Trim(), Convert.ToInt32(Session["Plantcode"]), Convert.ToInt32(Session["uid"]), Convert.ToInt32(txtuid.Text.Trim()), txtComments.Text.Trim(), Convert.ToInt32(Request["Appid"]), 0);
        }
        else
            intflag = mp.SaveApplication(0, 0, 0, 0, "", "", Convert.ToInt32(Session["Plantcode"]), Convert.ToInt32(Session["uid"]), Convert.ToInt32(txtuid.Text.Trim()), txtComments.Text.Trim(), Convert.ToInt32(Request["Appid"]), 0);

        if (intflag < 0)
        {
            ltlAlert.Text = "alert(' 提交失败，请重新操作！'); ";
            return;
        }
        else
        {
            MailSend(txtComments.Text.Trim(),txtEmail.Text.Trim(),Convert.ToInt32(Session["uid"]));
            ltlAlert.Text = "alert('提交成功！'); ";
            Response.Redirect("/new/MinorPurchase.aspx");
        }




    }

    /// <summary>
    ///  点击表示申请审批结束，可以打印采购
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFinish_Click(object sender, EventArgs e)
    {
        // Application check
        if (Convert.ToInt32(Request["Appid"]) == 0)
        {
            if (dropDept.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('必须选择部门！'); ";
                dropDept.Focus();
                return;
            }

            if (dropType.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('必须选择零件分类！'); ";
                dropType.Focus();
                return;
            }

            if (txtQuantity.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('必须填写数量！'); ";
                txtQuantity.Focus();
                return;
            }
            else
            {
                try
                {
                    decimal decQ = Convert.ToDecimal(txtQuantity.Text.Trim());
                }
                catch
                {
                    ltlAlert.Text = "alert('数量填写不正确，请重新输入！'); ";
                    txtQuantity.Focus();
                    return;
                }
            }

            if (txtPrice.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('必须填写单价！'); ";
                txtPrice.Focus();
                return;
            }
            else
            {
                try
                {
                    decimal decP = Convert.ToDecimal(txtPrice.Text.Trim());
                }
                catch
                {
                    ltlAlert.Text = "alert('单价填写不正确，请重新输入！'); ";
                    txtPrice.Focus();
                    return;
                }
            }

            if (txtPart.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('必须填写零件！'); ";
                txtPart.Focus();
                return;
            }

            if (txtSP.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('必须填写供应商！'); ";
                txtPart.Focus();
                return;
            }
        }


        //Save the application 
        //--- lblApid.Text == "0"   New Apply    == "1" Exist Apply
        int intflag;
        if (Convert.ToInt32(Request["Appid"]) == 0)
        {
            intflag = mp.SaveApplication(Convert.ToInt32(dropDept.SelectedValue), Convert.ToInt32(dropType.SelectedValue), Convert.ToDecimal(txtQuantity.Text.Trim()), Convert.ToDecimal(txtPrice.Text.Trim()), txtPart.Text.Trim(), txtSP.Text.Trim(), Convert.ToInt32(Session["Plantcode"]), Convert.ToInt32(Session["uid"]), 0, txtComments.Text.Trim(), Convert.ToInt32(Request["Appid"]), 1);
        }
        else
            intflag = mp.SaveApplication(0, 0, 0, 0, "", "", Convert.ToInt32(Session["Plantcode"]), Convert.ToInt32(Session["uid"]), 0, txtComments.Text.Trim(), Convert.ToInt32(Request["Appid"]), 1);

        if (intflag< 0)
        {
            ltlAlert.Text = "alert(' 操作失败，请重新操作！'); ";
            return;
        }
        else
        {
           
            ltlAlert.Text = "alert('操作成功！'); ";
            Response.Redirect("/new/MinorPurchase.aspx");
        }


    }


    protected void btnChuser_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = " window.open('MPApplicationUserCh.aspx','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300');";
    }


    protected void btnAhSave_Click(object sender, EventArgs e)
    {
        if (filename.Value.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('请选择上传文件 ！'); ";
            filename.Focus();
            return;
        }

        string strFile;
        int intfold;

        strFile = filename.PostedFile.FileName;
        intfold  =strFile.LastIndexOf ("\\");
        strFile = strFile.Substring(intfold+1);

        if (strFile.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('请选择导入文件.');";
            return;
        }

        Stream stImagin;
        int intLength;
        string strType;

      



        stImagin = filename.PostedFile.InputStream;
        intLength = filename.PostedFile.ContentLength;
        strType = filename.PostedFile.ContentType;

        byte[] byImagin = new byte[intLength];

        stImagin.Read(byImagin, 0, intLength);

        if (mp.SaveAttched(strFile, byImagin, strType, Convert.ToInt32(Session["uid"]), Convert.ToString(Session["uname"]), lblApid.Text.Trim().Length == 0 ? 0 : Convert.ToInt32(lblApid.Text.Trim())) < 0)
        {
            ltlAlert.Text = "alert('上传文件失败.');";
            return;
        }
        else
        {
             ltlAlert.Text = "alert('上传文件成功.');";
             gvAttached.DataBind();
        }
        

    }



    private void MailSend(string strCom,string strEmail,int intUid)
    {
        MailMessage MM = new MailMessage();
        string strUserID = lblApid.Text.Trim();

        string dmail = mp.UserEmail(intUid);

        if (dmail.Trim().Length > 0)
        {
            MM.To = dmail;
            MM.From = strEmail;
            MM.Subject = "请查看你需要审核的零星采购申请";
            MM.Body = strCom;
            BasePage.SSendEmail(MM.From, MM.To, MM.Cc, MM.Subject, MM.Body);
            //SmtpMail.SmtpServer = Convert.ToString (ConfigurationManager.AppSettings["mailServer"]);
            //SmtpMail.Send(MM);
        }

    }

    
    protected void gvAttached_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "1")
        {
            ltlAlert.Text = " window.open('MPview.aspx?attid=" + gvAttached.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString() + "','','menubar=No,scrollbars = No,resizable=No,width=500,height=500,top=200,left=300'); ";
        }
        else
        {
            if (e.CommandName.ToString() == "2")
            {
                if (Convert.ToInt32(Session["uid"]) != Convert.ToInt32(gvAttached.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[5].Text))
                {
                    ltlAlert.Text = "alert('没有权限删除此文件！.');";
                    return;
                }

                if (mp.DelAttached(Convert.ToInt32(gvAttached.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString()))<0)
                {
                   ltlAlert.Text = "alert('删除上传文件失败！.');";
                    return;
                }
                else
                {
                    ltlAlert.Text = "alert('删除成功！.');";
                    gvAttached.DataBind();
                }
            }
        }
    }
}
