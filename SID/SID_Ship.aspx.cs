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
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using QADSID;


public partial class SID_SID_Ship : BasePage
{
    SID sid = new SID();

    protected void Page_Load(object sender, EventArgs e)
    {
        ltlAlert.Text = "";
        if (!IsPostBack)
        {
            //�������ά��
            if (this.Security["550000010"].isValid)
            {
                /*
                    ������û����г��˵�ά��Ȩ�ޣ�Ҳ��������ڵ�Ȩ�ޣ���ǰ�߽��ᱻ���ǣ��ȣ������ѱ��صĴ��ڿɱ༭״̬����Ҳ�޷�����ͷ��Ϣ����ֻ�ܽ����������
                 */

                //���ǹ���Ա�ģ����˵�ά���Ĺ���Ҳ���õ�
                if (Convert.ToInt32(Session["uID"]) != 13)
                {
                    btnSave.Visible = false;
                    btnAdd.Visible = false;
                    gvShip.Columns[15].Visible = false;
                }

                chkInspectDate.Checked = true;//Ϊ�棬��ʾ���û��в�����졢������ڵ�Ȩ�ޣ��ر����ѱ���ʱ����

                txtInspectDate.Enabled = true;
                txtInspectSite.Enabled = true;
                chkMianJian.Enabled = true;
                txt_InspMatchDate.Enabled = true;
                chkIsCabin.Enabled = true;
                btnSaveInsp.Visible = true;
                btnClearInsp.Visible = true;
            }

            if (rad2.Checked == false)
            {
                gvShip.Columns[24].Visible = false;
                gvShip.Columns[25].Visible = false;
            }
            //if (!this.Security["550000006"].isValid)
            //{
            //    gvShip.Columns[22].Visible = false;
            //}
            txtcreated.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now.AddMonths(-1));
            txtcreated1.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

            BindData();
        }
    }

    protected void BindData()
    {
        String strRad = "";

        if (rad1.Checked)
        {
            strRad = "1";
        }
        if (rad2.Checked)
        {
            strRad = "2";
        }
        if (rad3.Checked)
        {
            strRad = "3";
        }

       // DataTable dt = sid.GetShip();
       DataTable dt = sid.GetShip(txtPK.Text.Trim(), txtnbr.Text.Trim(), txtOutDate.Text.Trim(), txtVia.Text.Trim(), txtCtype.Text.Trim(), txtShipDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtdomain.Text.Trim(), txtcreated.Text.Trim(), txtcreated1.Text.Trim(),strRad);

        //if (dt.Rows.Count == 0)
        //{
        //    dt.Rows.Add(dt.NewRow());

        //}
        gvShip.DataSource = dt;
        gvShip.DataBind();


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtPK.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('ϵͳ���˵��ű������룡');";
            return;
        }
        if (txtnbr.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('���˵��ű������룡');";
            return;
        }
        if (txtVia.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('���䷽ʽ�������룡');";
            return;
        }
        if (txtCtype.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('װ�����ͱ������룡');";
            return;
        }
        if (txtsite.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('װ��ص�������룡');";
            return;
        }
        if (txtShipDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�������ڱ������룡');";
            return;
        }
        else
        {
            if (!sid.IsDate(txtShipDate.Text.Trim()))
            {
                ltlAlert.Text = "alert('��������Ӧ����2009-09-09��');";
                return;
            }
        }

        if (txtOutDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�������ڱ������룡');";
            return;
        }

        if (txtdomain.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('���ڹ�˾�������룡');";
            return;
        }
        /*
         *  Add By Shanzm 2013-01-29
         * 
         * �״�ά�����˵�ʱ���������Ϊ��
         * �ٴ�ά�����˵�ʱ������Ƿ����ģ���ʱ���ĳ������ڵĻ�����Ҫר������������
        */
        if (chkMianJian.Checked || txtInspectDate.Text.Trim().Length > 0)
        {
            if (txtShipDate.Text.Trim() != txtOldShipDate.Text.Trim())
            {
                ltlAlert.Text = "alert('�����ĳ������ڣ������������Ա���������ڡ�����ص㣡');";
                return;
            }
        }

        sid.UpdateShipMstr(Convert.ToInt32(Session["uID"]),Convert.ToInt32(lblSID.Text.Trim()), txtPK.Text.Trim(), txtnbr.Text.Trim(), txtOutDate.Text.Trim(), txtVia.Text.Trim(), txtCtype.Text.Trim(), txtShipDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtdomain.Text.Trim(), txtPKref.Text.Trim());
        lblSID.Text = "";
        txtPK.Text = "";
        txtnbr.Text = "";
        txtOutDate.Text = "";
        txtVia.Text = "";
        txtCtype.Text = "";
        txtShipDate.Text = "";
        txtshipto.Text = "";
        txtsite.Text = "";
        txtdomain.Text = "";
        txtPKref.Text = "";
        BindData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        BindData();

        txtPK.Text = "";
        txtnbr.Text = "";
        txtOutDate.Text = ""; 
        txtVia.Text = "";
        txtCtype.Text  = "";
        txtShipDate.Text = "";
        txtshipto.Text = "";
        txtsite.Text = "";
        txtdomain.Text = "";

    }

    protected void gvShip_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Detail1")
        {
          if (gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim() == "")
          {
              ltlAlert.Text = "alert('�����ǿ��У�');";
              return;
          }

          Response.Redirect("SID_shipdetail.aspx?DID=" + Server.UrlEncode(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim()) + "&RAD=" + rad2.Checked.ToString() + "&rm=" + DateTime.Now);
        }
        else if (e.CommandName.ToString() == "Del1")
        {
            if (this.Security["550000002"].isValid)
            {
                ltlAlert.Text = "alert('û��ɾ����Ȩ�ޣ�');";
                return;
            }


            Int32 IErr = 0;

            IErr = sid.DelShipData(Convert.ToString(Session["uID"]), gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim());
            if (IErr < 0)
            {
                ltlAlert.Text = "alert('ɾ��ʧ�ܣ�');";
                return;
            }
            else
            {
                BindData();
            }
        }
        else if (e.CommandName.ToString() == "Edit1")
        {
            if (this.Security["550000002"].isValid)
            {
                ltlAlert.Text = "alert('û��ά����Ȩ�ޣ�');";
                return;
            }

            int index = ((GridViewRow)((LinkButton)e.CommandSource).Parent.Parent).RowIndex;
            txtPK.Text = gvShip.Rows[index].Cells[0].Text.Trim();
            txtPKref.Text = gvShip.Rows[index].Cells[1].Text.Trim();
            
            if (gvShip.Rows[index].Cells[1].Text.Trim() == "&nbsp;")
            {
                txtPKref.Text = "";
            }

            txtnbr.Text = gvShip.Rows[index].Cells[2].Text.Trim();
            txtVia.Text = gvShip.Rows[index].Cells[3].Text.Trim();
            txtCtype.Text = Server.HtmlDecode( gvShip.Rows[index].Cells[4].Text.Trim());
            txtsite.Text = gvShip.Rows[index].Cells[5].Text.Trim();

            txtShipDate.Text = gvShip.Rows[index].Cells[6].Text.Trim();
            if (gvShip.Rows[index].Cells[6].Text.Trim() == "&nbsp;")
            {
                txtShipDate.Text = "";
            }

            txtOutDate.Text = gvShip.Rows[index].Cells[7].Text.Trim();
            if (gvShip.Rows[index].Cells[7].Text.Trim() == "&nbsp;")
            {
                txtOutDate.Text = "";
            }

            txtdomain.Text = gvShip.Rows[index].Cells[16].Text.Trim();
            txtshipto.Text = gvShip.Rows[index].Cells[17].Text.Trim();
            if (gvShip.Rows[index].Cells[16].Text.Trim() == "&nbsp;")
            {
                txtshipto.Text = "";
            }

            //Add By Shanzm 2013-01-29
            txtOldShipDate.Text = txtShipDate.Text;
            if (sid.IsDate(gvShip.Rows[index].Cells[8].Text.Trim()))
            {
                txtInspectDate.Text = gvShip.Rows[index].Cells[8].Text.Trim();
            }
            else
            {
                txtInspectDate.Text = "";
            }

            //Add By Liuqj 2013-09-05
            if (gvShip.Rows[index].Cells[9].Text.Trim() == "&nbsp;")
            {
                txtInspectSite.Text = "";
            }
            else
            {
                txtInspectSite.Text = gvShip.Rows[index].Cells[9].Text.Trim();
            }

            if (sid.IsDate(gvShip.Rows[index].Cells[10].Text.Trim()))
            {
                txt_InspMatchDate.Text = gvShip.Rows[index].Cells[10].Text.Trim();
            }
            else
            {
                txt_InspMatchDate.Text = "";
            }
            if (gvShip.Rows[index].Cells[11].Text.Trim() == "��")
            {
                chkMianJian.Checked = true;
            }
            else
            {
                chkMianJian.Checked = false;
            }
            if (gvShip.Rows[index].Cells[12].Text.Trim() == "��")
            {
                chkIsCabin.Checked = true;
            }
            else
            {
                chkIsCabin.Checked = false;
            }

            lblSID.Text = gvShip.DataKeys[index].Value.ToString().Trim();
        }
        else if (e.CommandName == "Confirm1")
        {
            sid.ConfirmShipInfo(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim(), "ORG1", Session["uID"].ToString());

            BindData();
        }
        else if (e.CommandName == "Confirm2")
        {
            sid.ConfirmShipInfo(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim(), "ORG2", Session["uID"].ToString());

            BindData();
        }
        else if (e.CommandName == "Confirm3")
        {
            string isqcstatus = "";
            int index = ((GridViewRow)((Button)e.CommandSource).Parent.Parent).RowIndex;
            if (gvShip.Rows[index].Cells[25].Text.Trim() == "����")
            {
                isqcstatus = "NQC";
            }
            else
            {
                isqcstatus = "ISQC";
            }
            sid.ConfirmShipInfo(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim(), isqcstatus, Session["uID"].ToString());

            BindData();
        }
        else if (e.CommandName == "ConfirmC")
        {
            int index = ((GridViewRow)((Button)e.CommandSource).Parent.Parent).RowIndex;
            string sidnbr = gvShip.Rows[index].Cells[2].Text.Trim();
            string domain = "";
            CheckBoxList GridView = (CheckBoxList)gvShip.Rows[index].Cells[18].FindControl("cklist_domain");
            for (int i = 0; i < GridView.Items.Count; i++)
            {
                if (GridView.Items[i].Selected == true)
                {
                    domain = domain + GridView.Items[i].Value + ";";
                }
            }
            if (string.IsNullOrEmpty(domain.Replace(";", "")))
            {
                this.Alert("����ѡ��һ����!");
                return;
            }
            check(sidnbr, domain);
            sid.ConfirmShipInfo(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim(), "ISCK", Session["uID"].ToString());
            BindData();
        }
        else if (e.CommandName == "ConfirmU")
        {
            int index = ((GridViewRow)((Button)e.CommandSource).Parent.Parent).RowIndex;
            string sidnbr = gvShip.Rows[index].Cells[2].Text.Trim();
            string domain = "";
            CheckBoxList GridView = (CheckBoxList)gvShip.Rows[index].Cells[18].FindControl("cklist_domain");
            for (int i = 0; i < GridView.Items.Count; i++)
            {
                if (GridView.Items[i].Selected == true)
                {
                    domain = domain + GridView.Items[i].Value + ";";
                }
            }
            update(sidnbr, domain);
            sid.ConfirmShipInfo(gvShip.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString().Trim(), "ISUP", Session["uID"].ToString());
            BindData();
        }
    }

    protected void gvShip_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvShip.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void gvShip_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            if (this.Security["550000003"].isValid)
            {
                e.Row.Cells[24].Enabled = false;
            }

            if (this.Security["550001000"].isValid)
            {
                e.Row.Cells[25].Enabled = false;
            }
            //Add By WLW 2014-05-12
            if (!this.Security["550000005"].isValid)
            {
                e.Row.Cells[25].Enabled = false;
            }
            if (!this.Security["550000004"].isValid)
            {
                e.Row.Cells[24].Enabled = false;
            }

            //Add By Shanzm 2013-04-01
            if (chkInspectDate.Checked)
            {
                LinkButton linkEdit = (LinkButton)e.Row.FindControl("LinkButton1");
                linkEdit.Enabled = true;
            }
        }

    }
    protected void rad1_CheckedChanged(object sender, EventArgs e)
    {
        gvShip.Columns[24].Visible = false;
        gvShip.Columns[25].Visible = false;

        BindData();
    }
    protected void rad2_CheckedChanged(object sender, EventArgs e)
    {
        if (rad2.Checked == true)
        {
            gvShip.Columns[24].Visible = true;
            gvShip.Columns[25].Visible = true;
        }

        BindData();
    }
    protected void rad3_CheckedChanged(object sender, EventArgs e)
    {
        gvShip.Columns[24].Visible = false;
        gvShip.Columns[25].Visible = false;

        BindData();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtPK.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('ϵͳ���˵��ű������룡');";
            return;
        }
        if (txtnbr.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('���˵��ű������룡');";
            return;
        }
        if (txtVia.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('���䷽ʽ�������룡');";
            return;
        }
        if (txtCtype.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('װ�����ͱ������룡');";
            return;
        }
        if (txtsite.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('װ��ص�������룡');";
            return;
        }
        if (txtShipDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�������ڱ������룡');";
            return;
        }
        else
        {
            if (!sid.IsDate(txtShipDate.Text.Trim()))
            {
                ltlAlert.Text = "alert('��������Ӧ����2009-09-09��');";
                return;
            }
        }

        if (txtOutDate.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('�������ڱ������룡');";
            return;
        }

        if (txtdomain.Text.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('���ڹ�˾�������룡');";
            return;
        }

        sid.UpdateShipMstr(Convert.ToInt32(Session["uID"]), 0, txtPK.Text.Trim(), txtnbr.Text.Trim(), txtOutDate.Text.Trim(), txtVia.Text.Trim(), txtCtype.Text.Trim(), txtShipDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtdomain.Text.Trim(), txtPKref.Text.Trim());
        lblSID.Text = "";
        txtPK.Text = "";
        txtnbr.Text = "";
        txtOutDate.Text = "";
        txtVia.Text = "";
        txtCtype.Text = "";
        txtShipDate.Text = "";
        txtshipto.Text = "";
        txtsite.Text = "";
        txtdomain.Text = "";
        txtPKref.Text = "";
        BindData();
    }
    protected void btnSaveInsp_Click(object sender, EventArgs e)
    {
        if (lblSID.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('����ָ��һ�����˵���');";
            return;
        }

        //��������ģ�������д�������ڣ����򣬱�������ڳ�������֮ǰ
        //���û��ѡ���������������д�ˣ����Զ���Ϊ�Ƿ�����
        if (txtInspectDate.Text.Trim() != string.Empty)
        {
            chkMianJian.Checked = false;
        }

        if (!chkMianJian.Checked)
        {
            if (txtInspectDate.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('�������Ĳ�Ʒ����������������ڣ�');";
                return;
            }
            else
            {
                DateTime ship_date = Convert.ToDateTime(txtShipDate.Text.Trim());
                DateTime insp_date = Convert.ToDateTime(txtInspectDate.Text.Trim());

                if (insp_date > ship_date)
                {
                    ltlAlert.Text = "alert('������ڲ������ڳ������ڣ�');";
                    return;
                }
            }

            if(txtInspectSite.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('�������Ĳ�Ʒ��������������ص㣡');";
                return;
            }
            if (txt_InspMatchDate.Text.Trim() == string.Empty)
            {
                ltlAlert.Text = "alert('�������Ĳ�Ʒ����������Ԥ�����ڣ�');";
                return;
            }
            //ȡ��Ԥ��������װ������,Ԥ����������װ������
            //else
            //{
            //    DateTime ship_date = Convert.ToDateTime(txtShipDate.Text.Trim());
            //    DateTime insp_matchdate = Convert.ToDateTime(txt_InspMatchDate.Text.Trim());

            //    if (insp_matchdate > ship_date)
            //    {
            //        ltlAlert.Text = "alert('Ԥ�����ڲ������ڳ������ڣ�');";
            //        return;
            //    }
            //}
            if (!sid.IsDate(txt_InspMatchDate.Text.Trim()))
            {
                ltlAlert.Text = "alert('Ԥ������Ӧ����2009-09-09��');";
                return;
            }
        }

        sid.UpdateInspectInfo(Convert.ToInt32(Session["uID"]), Session["uName"].ToString(), Convert.ToInt32(lblSID.Text.Trim()), txtInspectDate.Text.Trim(), txtInspectSite.Text.Trim(), chkMianJian.Checked, txt_InspMatchDate.Text.Trim(),chkIsCabin.Checked);
        lblSID.Text = "";
        txtPK.Text = "";
        txtnbr.Text = "";
        txtOutDate.Text = "";
        txtVia.Text = "";
        txtCtype.Text = "";
        txtShipDate.Text = "";
        txtshipto.Text = "";
        txtsite.Text = "";
        txtdomain.Text = "";
        txtPKref.Text = "";
        txtInspectDate.Text = "";
        txtInspectSite.Text = "";
        txt_InspMatchDate.Text = "";
        chkMianJian.Checked = false;
        chkIsCabin.Checked = false;
        BindData();
    }
    protected void btnClearInsp_Click(object sender, EventArgs e)
    {
        if (lblSID.Text.Trim().Length == 0)
        {
            ltlAlert.Text = "alert('����ָ��һ�����˵���');";
            return;
        }

        sid.ClearInspectInfo(Convert.ToInt32(Session["uID"]), Session["uName"].ToString(), Convert.ToInt32(lblSID.Text.Trim()));
        lblSID.Text = "";
        txtPK.Text = "";
        txtnbr.Text = "";
        txtOutDate.Text = "";
        txtVia.Text = "";
        txtCtype.Text = "";
        txtShipDate.Text = "";
        txtshipto.Text = "";
        txtsite.Text = "";
        txtdomain.Text = "";
        txtPKref.Text = "";
        txtInspectDate.Text = "";
        txtInspectSite.Text = "";
        txt_InspMatchDate.Text = "";
        chkMianJian.Checked = true;
        chkIsCabin.Checked = false;
        BindData();
    }
    protected void chkMianJian_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMianJian.Checked)
        {
            txtInspectDate.Text = string.Empty;
            txtInspectSite.Text = string.Empty;
            txt_InspMatchDate.Text = string.Empty;
            txtInspectDate.Enabled = false;
            txtInspectSite.Enabled = false;
            txt_InspMatchDate.Enabled = false;
        }
        else
        {
            txtInspectDate.Enabled = true;
            txtInspectSite.Enabled = true;
            txt_InspMatchDate.Enabled = true;
        }
    }
    protected void btn_exportexcel_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(txtnbr.Text))
        //{
        //    this.Alert("��������˵��ţ�");
        //    return;
        //}
        String strRad = "";

        if (rad1.Checked)
        {
            strRad = "1";
        }
        if (rad2.Checked)
        {
            strRad = "2";
        }
        if (rad3.Checked)
        {
            strRad = "3";
        }
        DataTable dt = sid.SidExportExcel(txtPK.Text.Trim(), txtnbr.Text.Trim(), txtOutDate.Text.Trim(), txtVia.Text.Trim(), txtCtype.Text.Trim(), txtShipDate.Text.Trim(), txtshipto.Text.Trim(), txtsite.Text.Trim(), txtdomain.Text.Trim(), txtcreated.Text.Trim(), txtcreated1.Text.Trim(), strRad);
        if (dt.Rows.Count <= 0)
        {
            this.Alert("������ѯ���ݣ�");
            return;
        }

        string title = "80^<b>���˵���</b>~^110^<b>ϵͳ���˵���</b>~^70^<b>��������</b>~^120^<b>��������</b>~^80^<b>װ��ص�</b>~^110^<b>���</b>~^110^<b>���ϱ���</b>~^200^<b>�ͻ�����</b>~^70^<b>��������</b>~^70^<b>����ֻ��</b>~^70^<b>����</b>~^150^<b>��ע</b>~^100^<b>���۵���</b>~^40^<b>�к�</b>~^100^<b>ATL������</b>~^100^<b>TCP������</b>~^120^<b>�깤����</b>~^120^<b>����ִ�����</b>~^";
        this.ExportExcel(title, dt, false);
    }
    protected void btn_exportexcelByTemp_Click(object sender, EventArgs e)
    {
        System.Data.DataTable dt = sid.GetSidNbrVar(txtnbr.Text.Trim(), false);
        if (dt == null)
        {
            this.Alert("�˳��˵��Ų�����!");
            return;
        }
        else if (dt.Rows.Count <= 0)
        {
            this.Alert("�˳��˵��Ų�����!");
            return;
        }
        string strFile = string.Empty;
        SIDExcel.SIDExcel excel = null;
        //����EXCEL

        //�������
        string strShipNo = txtnbr.Text.Trim();
        //strFile = "SID_Cust_Invoice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        strFile = "SID_" + strShipNo + ".xls";
        excel = new SIDExcel.SIDExcel(Server.MapPath("/docs/SID_Ship_Det.xls"), Server.MapPath("../Excel/") + strFile);
        excel.SidDetToExcelNewByNPOI("���˵�", strShipNo, Convert.ToInt32(Session["uID"]), Session["uName"].ToString(), Convert.ToInt32(Session["PlantCode"]));
        ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
    }

    protected void check(string sidnbr, string domain)
    {
        string strFile = string.Empty;
        string strFiles = string.Empty;
        SIDExcel.SIDExcel excel = null;
        //����EXCEL

        //�������
        string strShipNo = sidnbr; //txtnbr.Text.Trim();

        if (string.IsNullOrEmpty(strShipNo))
        {
            this.Alert("���˵�����Ϊ��!");
            return;
        }
        //strFile = "SID_Cust_Invoice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";


        System.Data.DataTable dt = sid.GetSidNbrVar(strShipNo, true);
        if (dt.Rows.Count <= 0)
        {
            this.Alert("�˳��˵��Ų�����!");
            return;
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //strFile = "SID_" + strShipNo + ".xls";
            strFile = dt.Rows[i]["sid_nbr"].ToString().Trim() + ".xls";

            excel = new SIDExcel.SIDExcel(Server.MapPath("/docs/SID_Ship_Det.xls"), Server.MapPath("../Excel/") + strFile);
            excel.SidDetToExcelNewByNPOI("���˵�", dt.Rows[i]["sid_nbr"].ToString(), Convert.ToInt32(Session["uID"]), Session["uName"].ToString(), Convert.ToInt32(Session["PlantCode"]));
            //ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            strFiles += strFile + ";";
        }
        System.Data.DataTable dt1 = sid.GetSidUpdateEmailAdr("CK", domain, Convert.ToInt32(Session["uID"]));
        if (dt1.Rows.Count <= 0)
        {
            this.Alert("��ȡ�ռ���Ա�������,����ϵ����Ա!");
            return;
        }

        string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
        string to = dt1.Rows[0]["sendto"].ToString();
        //return;
        string copy = "";
        string subject = "���˵�ȷ�ϳ�����Ϣ��ȷ��֪ͨ��" + "���˵���:" + sidnbr + "�汾:" + dt.Rows[0]["sid_var"].ToString();
        string body = "";

        #region дBody
        body += "<font style='font-size: 12px;'>���˵����ݡ�ȷ�ϡ�����֪ͨ��" + "</font><br />";
        body += "<font style='font-size: 12px;'>���˵���:" + strShipNo + "</font><br />";
        body += "<font style='font-size: 12px;'>����Ϊ���˵���ȷ�ϡ�������Ϣ����ο��������κ���������ϵ�ƻ���ȷ��!" + "</font><br />";
        body += "<br /><br />";
        body += "<font style='font-size: 12px;'>�������½ "+baseDomain.getPortalWebsite()+" </font><br />";
        #endregion

        string filepath = @Server.MapPath("../Excel/") + strFile;//@"F:\\TCP160607.xls";
        string path = @Server.MapPath("../Excel/");
        SendEmail(from, to, copy, subject, body, path, strFiles);
        if (System.IO.File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }
        int re = sid.SidVarSubmit(sidnbr, Convert.ToInt32(Session["Uid"].ToString().Trim()));
        if (re > 0)
        {
            this.Alert("�������ʧ��,����ϵ����Ա!");
            return;
        }
        this.Alert("������Ϣ��ȷ�ϡ��ɹ�,�ѷ����ʼ�֪ͨ��Ӧ��Ա!");
    }

    protected void update(string sidnbr, string domain)
    {
        string strFile = string.Empty;
        string strFiles = string.Empty;
        SIDExcel.SIDExcel excel = null;
        //����EXCEL

        //�������
        string strShipNo = sidnbr;//txtnbr.Text.Trim();

        if (string.IsNullOrEmpty(strShipNo))
        {
            this.Alert("���˵�����Ϊ��!");
            return;
        }
        //strFile = "SID_Cust_Invoice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";


        System.Data.DataTable dt = sid.GetSidNbrVar(strShipNo, false);
        if (dt == null)
        {
            this.Alert("�˳��˵��Ų�����!");
            return;
        }
        else if (dt.Rows.Count <= 0)
        {
            this.Alert("�˳��˵��Ų�����!");
            return;
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //strFile = "SID_" + strShipNo + ".xls";
            strFile = dt.Rows[i]["sid_nbr"].ToString().Trim() + ".xls";

            excel = new SIDExcel.SIDExcel(Server.MapPath("/docs/SID_Ship_Det.xls"), Server.MapPath("../Excel/") + strFile);
            excel.SidDetToExcelNewByNPOI("���˵�", dt.Rows[i]["sid_nbr"].ToString(), Convert.ToInt32(Session["uID"]), Session["uName"].ToString(), Convert.ToInt32(Session["PlantCode"]));
            //ltlAlert.Text = "window.open('/Excel/" + strFile + "','Excel','menubar=yes,scrollbars = yes,resizable=yes,width=800,height=500,top=0,left=0');";
            strFiles += strFile + ";";
        }

        System.Data.DataTable dt1 = sid.GetSidUpdateEmailAdr("UP", domain, Convert.ToInt32(Session["uID"]));

        string from = ConfigurationManager.AppSettings["AdminEmail"].ToString();
        string to = dt1.Rows[0]["sendto"].ToString();//"";
        string copy = "";
        string subject = "���˵��޸ĳ�����Ϣ���޸�֪ͨ��" + "���˵���:" + sidnbr + "�汾:" + dt.Rows[0]["sid_var"].ToString();
        string body = "";

        #region дBody
        body += "<font style='font-size: 12px;'>���˵����ݡ��޸ġ�����֪ͨ��" +"</font><br />";
        body += "<font style='font-size: 12px;'>���˵���:" + strShipNo + "</font><br />";
        body += "<font style='font-size: 12px;'>����Ϊ���˵����޸ġ��������Ϣ����ο��������κ���������ϵ�ƻ���ȷ��!" + "</font><br />";
        body += "<br /><br />";
        body += "<font style='font-size: 12px;'>�������½ "+baseDomain.getPortalWebsite()+" </font><br />";
        #endregion

        string filepath = @Server.MapPath("../Excel/") + strFile;//@"F:\\TCP160607.xls";
        string path = @Server.MapPath("../Excel/");
        SendEmail(from, to, copy, subject, body, path, strFiles);
        if (System.IO.File.Exists(filepath))
        {
            System.IO.File.Delete(filepath);
        }
        int re = sid.SidVarSubmit(sidnbr, Convert.ToInt32(Session["Uid"].ToString().Trim()));
        if (re > 0)
        {
            this.Alert("�������ʧ��,����ϵ����Ա!");
            return;
        }
        this.Alert("������Ϣ���޸ġ��ɹ�,�ѷ����ʼ�֪ͨ��Ӧ��Ա!");
    }
}
