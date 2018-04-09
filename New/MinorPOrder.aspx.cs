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

            InforDatabind(Convert.ToInt32(Request["Appid"]));  //������Ϣ��ֵ

            btnSave.Attributes.Add("onclick", "return confirm('ȷ���ύ����һ����������');");

            btnFinish.Attributes.Add("onclick", "return confirm('ȷ��������ɣ������Բɹ����������');");

        }
    }

    /// <summary>
    ///  ��ʼ�󶨲�������
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
    ///  ��ʼ����Ʒ��������
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
    /// ��ʼ������Ϣ���Ƿ�Ϊ��������ǲ鿴����
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
    /// ��������鿴����
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
            ltlAlert.Text = "alert('����ѡ�������ߣ�'); ";
            txtApplication.Focus();
            return;
        }

        if (txtEmail.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('������д�Լ��������ַ��'); ";
            txtEmail.Focus();
            return;
        }
        else
        {
            if (baseDomain.checkDomainOR(txtEmail.Text.Trim()))
            {
                ltlAlert.Text = "alert('�����ַ��д����ȷ��'); ";
                txtEmail.Focus();
                return;
            }
        }


        // Application check
        if (Convert.ToInt32(Request["Appid"]) == 0)
        {
            if (dropDept.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('����ѡ���ţ�'); ";
                dropDept.Focus();
                return;
            }

            if (dropType.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('����ѡ��������࣡'); ";
                dropType.Focus();
                return;
            }

            if (txtQuantity.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('������д������'); ";
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
                    ltlAlert.Text = "alert('������д����ȷ�����������룡'); ";
                    txtQuantity.Focus();
                    return;
                }
            }

            if (txtPrice.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('������д���ۣ�'); ";
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
                    ltlAlert.Text = "alert('������д����ȷ�����������룡'); ";
                    txtPrice.Focus();
                    return;
                }
            }

            if (txtPart.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('������д�����'); ";
                txtPart.Focus();
                return;
            }

            if (txtSP.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('������д��Ӧ�̣�'); ";
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
            ltlAlert.Text = "alert(' �ύʧ�ܣ������²�����'); ";
            return;
        }
        else
        {
            MailSend(txtComments.Text.Trim(),txtEmail.Text.Trim(),Convert.ToInt32(Session["uid"]));
            ltlAlert.Text = "alert('�ύ�ɹ���'); ";
            Response.Redirect("/new/MinorPurchase.aspx");
        }




    }

    /// <summary>
    ///  �����ʾ�����������������Դ�ӡ�ɹ�
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
                ltlAlert.Text = "alert('����ѡ���ţ�'); ";
                dropDept.Focus();
                return;
            }

            if (dropType.SelectedIndex == 0)
            {
                ltlAlert.Text = "alert('����ѡ��������࣡'); ";
                dropType.Focus();
                return;
            }

            if (txtQuantity.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('������д������'); ";
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
                    ltlAlert.Text = "alert('������д����ȷ�����������룡'); ";
                    txtQuantity.Focus();
                    return;
                }
            }

            if (txtPrice.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('������д���ۣ�'); ";
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
                    ltlAlert.Text = "alert('������д����ȷ�����������룡'); ";
                    txtPrice.Focus();
                    return;
                }
            }

            if (txtPart.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('������д�����'); ";
                txtPart.Focus();
                return;
            }

            if (txtSP.Text.Trim().Length == 0)
            {
                ltlAlert.Text = "alert('������д��Ӧ�̣�'); ";
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
            ltlAlert.Text = "alert(' ����ʧ�ܣ������²�����'); ";
            return;
        }
        else
        {
           
            ltlAlert.Text = "alert('�����ɹ���'); ";
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
            ltlAlert.Text = "alert('��ѡ���ϴ��ļ� ��'); ";
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
            ltlAlert.Text = "alert('��ѡ�����ļ�.');";
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
            ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
            return;
        }
        else
        {
             ltlAlert.Text = "alert('�ϴ��ļ��ɹ�.');";
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
            MM.Subject = "��鿴����Ҫ��˵����ǲɹ�����";
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
                    ltlAlert.Text = "alert('û��Ȩ��ɾ�����ļ���.');";
                    return;
                }

                if (mp.DelAttached(Convert.ToInt32(gvAttached.DataKeys[Convert.ToInt32(e.CommandArgument.ToString())].Value.ToString()))<0)
                {
                   ltlAlert.Text = "alert('ɾ���ϴ��ļ�ʧ�ܣ�.');";
                    return;
                }
                else
                {
                    ltlAlert.Text = "alert('ɾ���ɹ���.');";
                    gvAttached.DataBind();
                }
            }
        }
    }
}
