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
using QCProgress;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.SqlClient;

public partial class QC_qc_flux_mstr : BasePage
{
    QC oqc = new QC();
    GridViewNullData ogv = new GridViewNullData();

     protected override void OnInit(EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Security.Register("100105140", "�༭��ϢȨ��");
        }

        base.OnInit(e);
    }

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dropStatu.SelectedIndex = 1;

            GridViewBind();

            checkAuth();
        }
        else
        {
            ogv.ResetGridView(gvFluxMstr);
        }
    }

    private void checkAuth()
    {
        if (!this.Security["100105140"].isValid)
        {
            btnAdd.Visible = false;
            trupload1.Visible = false;
            trupload2.Visible = false;
            trupload3.Visible = false;
            gvFluxMstr.Columns[21].Visible = false;
            gvFluxMstr.Columns[22].Visible = false;
        }
    }
    /// <summary>
    /// ����txtID���°�ͷ��
    /// </summary>
    private void bindHard()
    {
        SqlDataReader sdr = oqc.SelectFlux(txtID.Text.Trim());
        if (sdr.Read())
        {
            txtProduct.Text = sdr["fl_product"].ToString();
            txtQad.Text = sdr["fl_qad"].ToString();
            txtOldPro.Text = sdr["fl_oldpro"].ToString();
            txtOldQad.Text = sdr["fl_oldqad"].ToString();
            txtPo.Text = sdr["fl_po"].ToString();
            txtCustomer.Text = sdr["fl_cust"].ToString();
            txtWo.Text = sdr["fl_wo"].ToString();
            txtProvider.Text = sdr["fl_provider"].ToString();
            txtLamp.Text = sdr["fl_lamp"].ToString();
            txtSeries.Text = sdr["fl_series"].ToString();
            txtPeriod.Text = sdr["fl_period"].ToString();
            txtVersion.Text = sdr["fl_version"].ToString();
            txtDept.Text = sdr["fl_dept"].ToString();
            txtTemp.Text = sdr["fl_temp"].ToString();
            txtAging.Text = sdr["fl_aging"].ToString();
            txtDate.Text = sdr["fl_date"].ToString();
            txtTester.Text = sdr["fl_tester"].ToString();
            dropStatu.SelectedValue = sdr["fl_statu"].ToString();
            ddlType.SelectedValue = sdr["fl_type"].ToString();
            txtRemarks.Text = sdr["fl_rmks"].ToString();

           
        }

        sdr.Dispose();
        sdr.Close();

        btnAdd.Text = "����";
        btnClear.Text = "ȡ��";
    }

    private void GridViewBind() 
    {
        DataTable table = oqc.SelectFlux(txtProduct.Text.Trim(), txtQad.Text.Trim(), txtOldPro.Text.Trim(), txtOldQad.Text.Trim(), txtPo.Text.Trim(), txtCustomer.Text.Trim(), txtWo.Text.Trim(), txtProvider.Text.Trim(),
                                         txtLamp.Text.Trim(), txtSeries.Text.Trim(), txtPeriod.Text.Trim(), txtVersion.Text.Trim(), txtDept.Text.Trim(), txtTemp.Text.Trim(),
                                         txtAging.Text.Trim(), txtDate.Text.Trim(),txtDate1.Text.Trim(), txtTester.Text.Trim(), txtRemarks.Text.Trim(), dropStatu.SelectedIndex == 0 ? string.Empty : dropStatu.SelectedValue
                                         ,  ddlType.SelectedValue);

        ogv.GridViewDataBind(gvFluxMstr, table);

        table.Dispose();
    }
    
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        txtID.Text = string.Empty;
        btnAdd.Text = "���";
        btnClear.Text = "���";

        GridViewBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtProduct.Text.Trim() == string.Empty) 
        {
            ltlAlert.Text = "alert('��Ʒ�ͺŲ���Ϊ��');Form1.txtProduct.focus();";
            return;
        }

        if (txtQad.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('QAD�Ų���Ϊ��');Form1.txtQad.focus();";
            return;
        }

        if (txtOldPro.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('��ƺŲ���Ϊ��');Form1.txtOldPro.focus();";
            return;
        }

        if (txtOldQad.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('���QAD�Ų���Ϊ��');Form1.txtOldQad.focus();";
            return;
        }

        if (txtPo.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�����Ų���Ϊ��');Form1.txtPo.focus();";
            return;
        }

        if (txtCustomer.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�ͻ�����Ϊ��');Form1.txtCustomer.focus();";
            return;
        }

        if (txtWo.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�ӹ����Ų���Ϊ��');Form1.txtWo.focus();";
            return;
        }

        if (txtProvider.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('��Ӧ�̲���Ϊ��');Form1.txtProvider.focus();";
            return;
        }

        //if (txtLamp.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('�ƹܺŲ���Ϊ��');Form1.txtLamp.focus();";
        //    return;
        //}

        //if (txtSeries.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('���κŲ���Ϊ��');Form1.txtSeries.focus();";
        //    return;
        //}

        if (txtPeriod.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�������ڲ���Ϊ��');Form1.txtPeriod.focus();";
            return;
        }

        if (txtVersion.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('���Ӱ�Ų���Ϊ��');Form1.txtVersion.focus();";
            return;
        }

        if (txtDept.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�Ͳⲿ�Ų���Ϊ��');Form1.txtDept.focus();";
            return;
        }

        //if (txtTemp.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('��ʱ�ϻ�̨�Ų���Ϊ��');Form1.txtTemp.focus();";
        //    return;
        //}

        //if (txtAging.Text.Trim() == string.Empty)
        //{
        //    ltlAlert.Text = "alert('�����ϻ�̨�Ų���Ϊ��');Form1.txtAging.focus();";
        //    return;
        //}

        if (txtDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�������ڲ���Ϊ��');Form1.txtDate.focus();";
            return;
        }
        else 
        {
            try
            {
                DateTime dd = DateTime.Parse(txtDate.Text.Trim());
            }
            catch 
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ');Form1.txtDate.focus();";
                return;
            }
        }

        int nPlantid = int.Parse(Session["PlantCode"].ToString());

        if (txtTester.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('����Ա����Ϊ��');Form1.txtTester.focus();";
            return;
        }
        else if (!oqc.CheckUser(nPlantid, txtTester.Text.Trim())) 
        {
            ltlAlert.Text = "alert('����Ա������');Form1.txtTester.focus();";
            return;
        }

        if (dropStatu.SelectedIndex == 0) 
        {
            ltlAlert.Text = "alert('��ѡ��һ��״̬');Form1.dropStatu.focus();";
            return;
        }

        if (ddlType.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('��ѡ��һ������');Form1.ddlType.focus();";
            return;
        }

        FuncErrType error = FuncErrType.�����ɹ�;
        int uID = int.Parse(Session["uID"].ToString());
        int nID = 0;

        if (txtID.Text.Trim() == string.Empty)
        {
            error = oqc.InsertFlux(txtProduct.Text.Trim(), txtQad.Text.Trim(), txtOldPro.Text.Trim(), txtOldQad.Text.Trim(), txtPo.Text.Trim(), txtCustomer.Text.Trim(), txtWo.Text.Trim(), 
                                   txtProvider.Text.Trim(),txtLamp.Text.Trim(), txtSeries.Text.Trim(), txtPeriod.Text.Trim(), txtVersion.Text.Trim(), txtDept.Text.Trim(), txtTemp.Text.Trim(),
                                   txtAging.Text.Trim(), txtDate.Text.Trim(), txtTester.Text.Trim(), dropStatu.SelectedValue, txtRemarks.Text.Trim(), uID, ref nID, ddlType.SelectedValue);

            if (error == FuncErrType.�����ɹ�) 
            {
                txtID.Text = nID == 0 ? string.Empty : nID.ToString();

                if (txtFile.Name != string.Empty)
                    this.btnUploadTxt_ServerClick();
                if (excelFile.Name != string.Empty)
                    this.btnUploadExcel_ServerClick();
                if (filename.Name != string.Empty)
                    this.btnUplodeFile_Click(this, new EventArgs());
                    
            }
        }
        else 
        {
            int ID = int.Parse(txtID.Text.Trim());

            error = oqc.UpdateFlux(ID, txtProduct.Text.Trim(), txtQad.Text.Trim(), txtOldPro.Text.Trim(), txtOldQad.Text.Trim(), txtPo.Text.Trim(), txtCustomer.Text.Trim(), txtWo.Text.Trim(), 
                                   txtProvider.Text.Trim(), txtLamp.Text.Trim(), txtSeries.Text.Trim(), txtPeriod.Text.Trim(), txtVersion.Text.Trim(), txtDept.Text.Trim(), txtTemp.Text.Trim(),
                                   txtAging.Text.Trim(), txtDate.Text.Trim(), txtTester.Text.Trim(), dropStatu.SelectedValue, txtRemarks.Text.Trim(), uID,ddlType.SelectedValue);
        }

        if (error != FuncErrType.�����ɹ�)
            ltlAlert.Text = "alert('����ʧ��');";
        else
        {
            ltlAlert.Text = "alert('�����ɹ�');";
            ClearInfo();
        }

        GridViewBind();
    }
    protected void ClearInfo() 
    {
        txtID.Text = string.Empty;
        txtProduct.Text = string.Empty;
        txtQad.Text = string.Empty;
        txtOldPro.Text = string.Empty;
        txtOldQad.Text = string.Empty;
        txtPo.Text = string.Empty;
        txtCustomer.Text = string.Empty;
        txtWo.Text = string.Empty;
        txtProvider.Text = string.Empty;
        txtLamp.Text = string.Empty;
        txtSeries.Text = string.Empty;
        txtPeriod.Text = string.Empty;
        txtVersion.Text = string.Empty;
        txtDept.Text = string.Empty;
        txtTemp.Text = string.Empty;
        txtAging.Text = string.Empty;
        txtDate.Text = string.Empty;
        txtTester.Text = string.Empty;
        dropStatu.SelectedIndex = 0;
        txtRemarks.Text = string.Empty;
        ddlType.SelectedIndex = 0;
        txtDate1.Text = string.Empty;

        txtFile.Name = string.Empty;
        excelFile.Name = string.Empty;

        btnAdd.Text = "���";
        btnClear.Text = "���";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ClearInfo();

        GridViewBind();
    }

    /// <summary>
    /// ���ڱ����õ��ϴ�����
    /// </summary>
    protected void btnUploadTxt_ServerClick()
    {
        if (txtID.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('��ѡ��һ���¼,Ȼ�����ϴ�');";
            return;
        }

        if (txtHour.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('����ʱ�䲻��Ϊ��');";
            return;
        }
        else
        {
            if (!Regex.IsMatch(txtHour.Text.Trim(), @"^\d+$"))
            {
                ltlAlert.Text = "alert('����ʱ���ʽ����');";
                return;
            }
        }
        if (dropWay.SelectedIndex == 0)
        {
            ltlAlert.Text = "alert('��ѡ��һ���ȼ��ʽ');";
            return;
        }

        FileOperate fop = new FileOperate(txtFile, Server.MapPath("/import"), 8388608);
        fop.SectionLimit = Section.TXT;
        string strMsg = "";
        if (!fop.SaveFileToServer(ref strMsg))
        {
            ltlAlert.Text = "alert('" + strMsg + "')";
            return;
        }
        else
        {
            int id = int.Parse(txtID.Text.Trim());

            fop.FluxImportTxt(id, txtHour.Text.Trim(), dropWay.SelectedValue, int.Parse(Session["uID"].ToString()), ref strMsg);
            if (strMsg != "")
            {
                ltlAlert.Text = "alert('" + strMsg + "')";
                return;
            }
            oqc.updateQCFluxStatusByID(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            ltlAlert.Text = "alert('�������ݳɹ�!')";
        }
    }

    /// <summary>
    /// �ϴ�����ķ���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUploadTxt_ServerClick(object sender, EventArgs e)
    {
        if (txtID.Text.Trim() == string.Empty) 
        {
            ltlAlert.Text = "alert('��ѡ��һ���¼,Ȼ�����ϴ�');";
            return;
        }

        if (txtHour.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('����ʱ�䲻��Ϊ��');";
            return;
        }
        else 
        {
            if (!Regex.IsMatch(txtHour.Text.Trim(), @"^\d+$"))
            {
                ltlAlert.Text = "alert('����ʱ���ʽ����');";
                return;
            }
        }
        if (dropWay.SelectedIndex == 0) 
        {
            ltlAlert.Text = "alert('��ѡ��һ���ȼ��ʽ');";
            return;
        }

        FileOperate fop = new FileOperate(txtFile, Server.MapPath("/import"), 8388608);
        fop.SectionLimit = Section.TXT;
        string strMsg = "";
        if (!fop.SaveFileToServer(ref strMsg))
        {
            ltlAlert.Text = "alert('" + strMsg + "')";
            return;
        }
        else
        {
            int id = int.Parse(txtID.Text.Trim());

            fop.FluxImportTxt(id, txtHour.Text.Trim(), dropWay.SelectedValue, int.Parse(Session["uID"].ToString()), ref strMsg);
            if (strMsg != "")
            {
                ltlAlert.Text = "alert('" + strMsg + "')";
                return;
            }
            oqc.updateQCFluxStatusByID(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
            bindHard();
            GridViewBind();
     
            ltlAlert.Text = "alert('�������ݳɹ�!')";
        }
    }

    /// <summary>
    /// �����õ��ϴ�excel�ķ���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUploadExcel_ServerClick()
    {
        if (txtID.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('��ѡ��һ���¼,Ȼ�����ϴ�');";
            return;
        }

        string strFileName = "";
        string strCatFolder = "";
        string strUserFileName = "";
        int intLastBackslash = 0;

        string strUID = Convert.ToString(Session["uID"]);
        string struName = Convert.ToString(Session["uName"]);

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
                return;
            }
        }

        strUserFileName = excelFile.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('��ѡ�����ļ�.');";
            return;
        }
        strUserFileName = strFileName;

        int i = 0;
        while (i < 1000)
        {
            strFileName = strCatFolder + "\\f" + i.ToString() + strUserFileName;
            if (!File.Exists(strFileName))
            {
                break;
            }
            i += 1;
        }


        if (excelFile.PostedFile != null)
        {
            if (excelFile.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('�ϴ����ļ����Ϊ 8 MB!');";
                return;
            }

            try
            {
                excelFile.PostedFile.SaveAs(strFileName);//�ϴ� �ļ�
            }
            catch
            {
                ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                try
                {


                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('�����ļ�������Excel��ʽ'" + ex.Message.ToString() + "'.');";
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
        }



        FileOperate fop = new FileOperate(excelFile, Server.MapPath("/import"), 8388608);
        fop.SectionLimit = Section.XLS;
        string strMsg = "";
        if (!fop.SaveFileToServer(ref strMsg))
        {
            ltlAlert.Text = "alert('" + strMsg + "')";
            return;
        }
        else
        {
            int id = int.Parse(txtID.Text.Trim());
            fop.FluxImportExcel(id, int.Parse(Session["uID"].ToString()), ref strMsg);
            if (strMsg != "")
            {
                ltlAlert.Text = "alert('" + strMsg + "')";
                return;
            }

            oqc.updateQCFluxStatusByID(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
           
            ltlAlert.Text = "alert('�������ݳɹ�!')";
        }
    }

    /// <summary>
    /// ʵ���ϴ�excel�ļ��ķ���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUploadExcel_ServerClick(object sender, EventArgs e)
    {
        if (txtID.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('��ѡ��һ���¼,Ȼ�����ϴ�');";
            return;
        }

        string strFileName = "";
        string strCatFolder = "";
        string strUserFileName = "";
        int intLastBackslash = 0;

        string strUID = Convert.ToString(Session["uID"]);
        string struName = Convert.ToString(Session["uName"]);

        strCatFolder = Server.MapPath("/import");
        if (!Directory.Exists(strCatFolder))
        {
            try
            {
                Directory.CreateDirectory(strCatFolder);
            }
            catch
            {
                ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
                return;
            }
        }

        strUserFileName = excelFile.PostedFile.FileName;
        intLastBackslash = strUserFileName.LastIndexOf("\\");
        strFileName = strUserFileName.Substring(intLastBackslash + 1);
        if (strFileName.Trim().Length <= 0)
        {
            ltlAlert.Text = "alert('��ѡ�����ļ�.');";
            return;
        }
        strUserFileName = strFileName;

        int i = 0;
        while (i < 1000)
        {
            strFileName = strCatFolder + "\\f" + i.ToString() + strUserFileName;
            if (!File.Exists(strFileName))
            {
                break;
            }
            i += 1;
        }


        if (excelFile.PostedFile != null)
        {
            if (excelFile.PostedFile.ContentLength > 8388608)
            {
                ltlAlert.Text = "alert('�ϴ����ļ����Ϊ 8 MB!');";
                return;
            }

            try
            {
                excelFile.PostedFile.SaveAs(strFileName);//�ϴ� �ļ�
            }
            catch
            {
                ltlAlert.Text = "alert('�ϴ��ļ�ʧ��.');";
                return;
            }

            if (File.Exists(strFileName))
            {
                try
                {
                    
                    
                }
                catch (Exception ex)
                {
                    ltlAlert.Text = "alert('�����ļ�������Excel��ʽ'" + ex.Message.ToString() + "'.');";
                }
                finally
                {
                    if (File.Exists(strFileName))
                    {
                        File.Delete(strFileName);
                    }
                }
            }
        }



        FileOperate fop = new FileOperate(excelFile, Server.MapPath("/import"), 8388608);
        fop.SectionLimit = Section.XLS;
        string strMsg = "";
        if (!fop.SaveFileToServer(ref strMsg))
        {
            ltlAlert.Text = "alert('" + strMsg + "')";
            return;
        }
        else
        {
            int id = int.Parse(txtID.Text.Trim());
            fop.FluxImportExcel(id, int.Parse(Session["uID"].ToString()), ref strMsg);
            if (strMsg != "")
            {
                ltlAlert.Text = "alert('" + strMsg + "')";
                return;
            }

            oqc.updateQCFluxStatusByID(txtID.Text.Trim(),Session["uID"].ToString(),Session["uName"].ToString());
            bindHard();
            GridViewBind();
            ltlAlert.Text = "alert('�������ݳɹ�!')";
        }
    }


    protected void gvFluxMstr_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Cust_Edit") 
        {
            int index = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent)).RowIndex;

            txtID.Text = e.CommandArgument.ToString();
            txtProduct.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[0].Text.Trim());
            txtQad.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[1].Text.Trim());
            txtOldPro.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[2].Text.Trim());
            txtOldQad.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[3].Text.Trim());
            txtPo.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[4].Text.Trim());
            txtCustomer.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[5].Text.Trim());
            txtWo.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[6].Text.Trim());
            txtProvider.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[7].Text.Trim());
            txtLamp.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[8].Text.Trim());
            txtSeries.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[9].Text.Trim());
            txtPeriod.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[10].Text.Trim());
            txtVersion.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[11].Text.Trim());
            txtDept.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[12].Text.Trim());
            txtTemp.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[13].Text.Trim());
            txtAging.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[14].Text.Trim());
            txtDate.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[15].Text.Trim());
            txtTester.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[16].Text.Trim());
            dropStatu.SelectedValue = gvFluxMstr.Rows[index].Cells[17].Text.Trim();
            ddlType.SelectedValue = gvFluxMstr.DataKeys[index].Values["fl_type"].ToString();
            txtRemarks.Text = changeEmpty(gvFluxMstr.Rows[index].Cells[19].Text.Trim());

            btnAdd.Text = "����";
            btnClear.Text = "ȡ��";
        }
    }

    private string changeEmpty(string enter)
    {
        if(enter.Equals("&nbsp;"))
        {
            return "";
        }
        else
        {
            return enter;
        }
    }
    protected void gvFluxMstr_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = int.Parse(gvFluxMstr.DataKeys[e.RowIndex].Values["fl_id"].ToString());

        if (oqc.DeleteFlux(ID))
        {
            ltlAlert.Text = "alert('ɾ���ɹ�');";

            ClearInfo();
            GridViewBind();
        }
        else
            ltlAlert.Text = "alert('ɾ��ʧ��');";
    }
    protected void gvFluxMstr_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton link = (LinkButton)e.Row.Cells[19].FindControl("linkDetail");
            link.Attributes["onclick"] = "window.open('qc_flux_det.aspx?id=" + gvFluxMstr.DataKeys[e.Row.RowIndex].Values["fl_id"].ToString().Trim() + "','','menubar=no,scrollbars=no,resizable=no,width=1000,height=500,top=0,left=0');";

            string type = gvFluxMstr.DataKeys[e.Row.RowIndex].Values["fl_type"].ToString();
        }
    }
    protected void gvFluxMstr_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFluxMstr.PageIndex = e.NewPageIndex;

        GridViewBind();
    }
    protected void btnTemplate_Click(object sender, EventArgs e)
    {
        string title = "30^<b>���</b>~^200^<b>��Ʒ�ͺ�</b>~^120^<b>��������</b>~^100^<b>��ȼ��ʽ</b>~^80^<b>x</b>~^80^<b>y</b>~^80^<b>u</b>~^80^<b>v</b>~^80^<b>ɫ��(K)</b>~^80^<b>ɫ�ݲ�(SDCM)</b>~^80^<b>��ͨ��(lm)</b>~^80^<b>��Ч(lm/W)</b>~^80^<b>���书��(W)</b>~^80^<b>U(V)</b>~^80^<b>I(A)</b>~^80^<b>P(W)</b>~^80^<b>PF</b>~^80^<b>��ɫ��(%)</b>~^80^<b>ɫ����(%)</b>~^80^<b>������(nm)</b>~^80^<b>��ֵ����(nm)</b>~^80^<b>Ra</b>~^80^<b>R9</b>~^80^<b>DUV</b>~^";

        string[] titleSub = title.Split(new char[] { '~' });

        DataTable dtExcel = new DataTable("temp");
        DataColumn col;

        foreach (string colName in titleSub)
        {
            col = new DataColumn();
            col.DataType = System.Type.GetType("System.String");
            col.ColumnName = colName;
            dtExcel.Columns.Add(col);
        }

        ExportExcel(title, dtExcel, false);
    }

    /// <summary>
    /// ���ڱ����õ��ϴ������ķ���
    /// </summary>
    protected void btnUplodeFile_Click()
    {

        if (txtID.Text.Equals(string.Empty))
        {
            ltlAlert.Text = "alert('����ѡ��һ����Ʒ��');";
            return;
        }
        else
        {

            string path = "/TecDocs/QC/";
            string fileName = string.Empty;//ԭ�ļ���
            string filePate = string.Empty;//�ļ�·��+�ļ������洢�ģ�
            if (string.Empty.Equals(filename.PostedFile.FileName))
            {
                ltlAlert.Text = "alert('�ϴ�·������Ϊ��');";
                return;

            }
            else
            {
                if (!ImportFile(ref fileName, ref filePate, path))
                {
                    ltlAlert.Text = "alert('�ϴ��ļ�ʧ�ܣ�����ϵ����Ա');";
                    return;
                }
                //else 
                //{
                //    ltlAlert.Text = "alert('�ϴ��ļ��ɹ���');";
                //}
            }

            if (oqc.importFluxBasisFile(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString(), fileName, filePate))
            {
                ltlAlert.Text = "alert('�ϴ��ļ��ɹ���');";
                oqc.updateQCFluxStatusByID(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
                return;
            }
            else
            {

                ltlAlert.Text = "alert('�ϴ��ļ�ʧ�ܣ�����ϵ����Ա��');";
                return;
            }
        }
    }


    /// <summary>
    /// �ϴ������ķ���
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUplodeFile_Click(object sender, EventArgs e)
    {

        if (txtID.Text.Equals(string.Empty))
        {
            ltlAlert.Text = "alert('����ѡ��һ����Ʒ��');";
            return;
        }
        else
        { 
            
            string path = "/TecDocs/QC/";
            string fileName = string.Empty;//ԭ�ļ���
            string filePate = string.Empty;//�ļ�·��+�ļ������洢�ģ�
            if (string.Empty.Equals(filename.PostedFile.FileName))
            {
                ltlAlert.Text = "alert('�ϴ�·������Ϊ��');";
                return;

            }
            else
            {
                if (!ImportFile(ref fileName, ref filePate, path))
                {
                    ltlAlert.Text = "alert('�ϴ��ļ�ʧ�ܣ�����ϵ����Ա');";
                    return;
                }
                //else 
                //{
                //    ltlAlert.Text = "alert('�ϴ��ļ��ɹ���');";
                //}
            }

            if (oqc.importFluxBasisFile(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString(), fileName, filePate))
            {
                ltlAlert.Text = "alert('�ϴ��ļ��ɹ���');";
                oqc.updateQCFluxStatusByID(txtID.Text.Trim(), Session["uID"].ToString(), Session["uName"].ToString());
                bindHard();
                GridViewBind();
                return;
            }
            else
            {

                ltlAlert.Text = "alert('�ϴ��ļ�ʧ�ܣ�����ϵ����Ա��');";
                return;
            }
        }
    }

    protected bool ImportFile(ref string _fileName, ref string _filePath, string path)
    {
        string attachName = Path.GetFileNameWithoutExtension(filename.PostedFile.FileName);
        string newFileName = DateTime.Now.ToFileTime().ToString();

        string attachExtension = Path.GetExtension(filename.PostedFile.FileName);
        string SaveFileName = System.IO.Path.Combine(Server.MapPath("../import/"), newFileName + attachExtension);//�ϲ�����·��Ϊ�ϴ����������ϵ�ȫ·��

        if (File.Exists(SaveFileName))
        {
            try
            {
                File.Delete(SaveFileName);
            }
            catch
            {
                return false;
            }
        }

        try
        {
            filename.PostedFile.SaveAs(SaveFileName);
        }
        catch
        {
            return false;
        }



        if (!Directory.Exists(Server.MapPath(path)))
        {
            Directory.CreateDirectory(Server.MapPath(path));
        }

        path += newFileName + attachExtension;

        try
        {
            File.Move(SaveFileName, Server.MapPath(path));
        }
        catch
        {
            return false;
        }

        _fileName = attachName + attachExtension;
        _filePath = path;

        return true;
    }
}
