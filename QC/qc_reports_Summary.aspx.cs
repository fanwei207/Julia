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

public partial class qc_reports_Summary : BasePage
{
    QCExcel qc = new QCExcel();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void btn_btn_CFLLineSamplingreport_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (txtStdDate12.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate12.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���ڸ�ʽ����ȷ!');Form1.txtStdDate12.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ����ʼ����!');Form1.txtStdDate12.focus();";
            return;
        }

        if (txtEndDate12.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate12.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ!');Form1.txtEndDate12.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ���������!');Form1.txtEndDate12.focus();";
            return;
        }
        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/sp_QC_Report_LineSampling_Incoming.xls");
        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>��������</b>~^<b>��������</b>~^<b>�ߺ�</b>~^<b>�߳�</b>~^<b>������</b>~^<b>�ϸ�����</b>~^<b>���ϸ�����</b>~^<b>һ�γ�����ν�����</b>~^<b>������</b>~^<b>��ȱ����</b>~^<b>�ϸ���</b>~^";

        DataTable DT = qc.SelectLineSamplingIncoming(Convert.ToInt32(Session["uID"]), strStdDate, strEndDate, "CFL��Ʒ", Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("��������Ϊ��!");
        }
    }
    protected void btn_LEDLineSamplingreport_Click(object sender, EventArgs e)
    {
        string strStdDate = DateTime.Now.ToShortDateString();
        string strEndDate = DateTime.Now.AddDays(1).ToShortDateString();

        if (txtStdDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate.Text.Trim());
                strStdDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���ڸ�ʽ����ȷ!');Form1.txtStdDate.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ����ʼ����!');Form1.txtStdDate.focus();";
            return;
        }

        if (txtEndDate.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate.Text.Trim());
                strEndDate = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ!');Form1.txtEndDate.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ���������!');Form1.txtStdDate.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/sp_QC_Report_LineSampling_Incoming.xls");
        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>��������</b>~^<b>��������</b>~^<b>�ߺ�</b>~^<b>�߳�</b>~^<b>������</b>~^<b>�ϸ�����</b>~^<b>���ϸ�����</b>~^<b>һ�γ�����ν�����</b>~^<b>������</b>~^<b>��ȱ����</b>~^<b>�ϸ���</b>~^";

        DataTable DT = qc.SelectLineSamplingIncoming(Convert.ToInt32(Session["uID"]), strStdDate, strEndDate, "LED��Ʒ", Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("��������Ϊ��!");
        }
    }
    protected void btn_CFPLampSamplingreport_Click(object sender, EventArgs e)
    {
        string txtStdDate2 = DateTime.Now.ToShortDateString();
        string txtEndDate2 = DateTime.Now.AddDays(1).ToShortDateString();

        if (txtStdDate22.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate22.Text.Trim());
                txtStdDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���ڸ�ʽ����ȷ!');Form1.txtStdDate22.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ����ʼ����!');Form1.txtStdDate22.focus();";
            return;
        }

        if (txtEndDate22.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate22.Text.Trim());
                txtEndDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ!');Form1.txtEndDate22.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ���������!');Form1.txtEndDate22.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        //string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>��������</b>~^<b>������</b>~^<b>�ϸ�����</b>~^<b>���ϸ�����</b>~^<b>һ�γ�����ν�����</b>~^<b>Ͷ����</b>~^<b>��������</b>~^<b>ȱ����</b>~^<b>һ�γ��ϸ���</b>~^<b>(101)��˿��¶���ƹ����ܸǡ��ܿ����ܸ�֮�䣩</b>~^<b>(102)�ƿǡ���������</b>~^<b>(103)��ͷ������Ť��������Ҫ��</b>~^<b>(104)ͭͷ���䡢δ���</b>~^<b>(105)ȱ��</b>~^<b>(106)��װ�ڲ�Ʒ����</b>~^<b>(107)���ְ�װӡˢ�ͺ����Ʒ�ͺŲ�ƥ��</b>~^<b>(108)BOM�汾�˶ԣ�model/PCB Versions/˿ӡ/��װ marking������ʾ���</b>~^<b>(109)��������ȱ��</b>~^<b>����ȱ��PPM</b>~^<b>(201)�Ʋ���</b>~^<b>(202)�ƹܲ����ԡ���˸</b>~^<b>(203)�Ƽ��γߴ糬����Χ</b>~^<b>(204)�����ɶ�/����</b>~^<b>(205)��ͷ���ر��Σ����ܳ�����Ӧͨ��</b>~^<b>(206)��ͷ�޶�������ͷ��Ƭ�ɶ�</b>~^<b>(207)�ƿǿ��ѡ���������������ࣩ</b>~^<b>(208)����ӡˢ�ؼ���ʶ���壨��ѹ�����ʡ�UL��ʶ�ȣ�</b>~^<b>(209)��װӡˢ�����������ϵƱ��ѹ�����ʵ�</b>~^<b>(210)���ֻ����ܰ�װ���г���</b>~^<b>(211)��˿��¶</b>~^<b>(212)����ͽ��������</b>~^<b>(213)��������</b>~^<b>(214)�ƹ�Ӧ��/©��</b>~^<b>(215)����Ӧ��</b>~^<b>(216)��������ȱ��</b>~^<b>(217)����������4��1Сʱѹ���ϸ�</b>~^<b>����ȱ��PPM</b>~^<b>(301)����ʱ�ƹ�������覴ã�������Ʒ��</b>~^<b>(302)�ƹ����ر��Σ�������Ʒ��</b>~^<b>(303)��ͷδš�����м�϶��>0.5mm��</b>~^<b>(304)(304)����������˵��ϵĲ�һ��</b>~^<b>(305)��װ��/��/�����������벻������ɨ�����</b>~^<b>(306)��������</b>~^<b>(307)�ƹ��ɶ����ѽ�</b>~^<b>(308)����������Ҫȱ��</b>~^<b>��Ҫȱ��PPM</b>~^<b>(401)�ƹ��ᣨ������Ʒ��</b>~^<b>(402)�ƹܿ���������΢�ѷۡ�ɫ�ߡ��۵㡢��˿�ȣ�������Ʒ��</b>~^<b>(403)(403)�ƿ�©��ճ�������������������</b>~^<b>(404)�ƽ��಻�������Գ��ߣ���2mm����ȱ��</b>~^<b>(405)�ܷ⿪�ѣ�����ë���뻮��</b>~^<b>(406)ֽ�а�װ��Ʒ���������ֽ�в���������ë�ߺ�ӡˢ����</b>~^<b>(407)����������Բ���</b>~^<b>(408)���ܰ�װ��������</b>~^<b>(409)��װ��ʽ����ȷ����ͷ�ġ����������/���ס��а��©��</b>~^<b>(410)��װ����ӡˢģ���������©���ʺа�װ����������ɫ��</b>~^<b>(411)��������������©��������λ�ò��Ի��ѽ�</b>~^<b>(412)���䳱ʪ�����Ρ��𻵡����̵�����</b>~^<b>(413)�������ƴ�Ҫȱ��</b>~^<b>��Ҫȱ��PPM</b>~^";

        DataTable DT = qc.SelectCFLLampSamplingIncoming(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "CFL��Ʒ", false, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("��������Ϊ��!");
        }
    }
    protected void btn_LEDLampSampling_Click(object sender, EventArgs e)
    {
        string txtStdDate2 = DateTime.Now.ToShortDateString();
        string txtEndDate2 = DateTime.Now.AddDays(1).ToShortDateString();

        if (txtStdDate21.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate21.Text.Trim());
                txtStdDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���ڸ�ʽ����ȷ!');Form1.txtStdDate21.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ����ʼ����!');Form1.txtStdDate21.focus();";
            return;
        }

        if (txtEndDate21.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate21.Text.Trim());
                txtEndDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ!');Form1.txtEndDate21.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ���������!');Form1.txtEndDate21.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>��������</b>~^<b>������</b>~^<b>�ϸ�����</b>~^<b>���ϸ�����</b>~^<b>һ�γ�����ν�����</b>~^<b>Ͷ����</b>~^<b>��������</b>~^<b>ȱ����</b>~^<b>һ�γ��ϸ���</b>~^<b>(101)��˿��¶��ָ�ܿ����ͷ֮�䣩</b>~^<b>(102)�ƿǡ����֡�ɢ������͸��������</b>~^<b>(103)��ͷ������Ť������еǿ�ȣ�������Ҫ��ÿ����6ֻ AC=0��</b>~^<b>(104)ͭͷ���䡢δ��ס�ͭͷ���㴦�в�������ͷ�޶�������í��</b>~^<b>(105)��Ʒ��ѹ�ⲻͨ����ÿ����6ֻ AC=0��</b>~^<b>(106)��װ�ڲ�Ʒ���������ְ�װӡˢ�ͺ����Ʒ�ͺŲ�ƥ��</b>~^<b>(107)ˤ�����鲻�ϸ񣨵�һ����������</b>~^<b>(108)ȱ��</b>~^<b>(109)BOM�汾�˶ԣ�model/PCB Versions/LED versions/˿ӡ/��װ marking������ʾ���</b>~^<b>(110)��������ˮ��Ʒ��ˮ����ÿ����20ֻ AC=0����ͨ��</b>~^<b>PPM1</b>~^<b>(201)�Ʋ���������˸,���ַ���ܲ���(��ѹ����)��������ɫ��ɫ��</b>~^<b>(202)͸�����ѡ����ػ��ۻ�͸�����䲻ͬ����͸����һ������ʱ����ȡ6ֻ��Ʒ���ڰ����ڣ����ѹ�µ�����������</b>~^<b>(203)�������ѡ�  ��δ�����</b>~^<b>(204)ͭͷ���ر��Σ�����,����ͨ����Ӧͨ��</b>~^<b>(205)��ͷ��Ƭ�ɶ���ƫ����©��������í���ɶ�</b>~^<b>(206)�ƿǿ��ѡ����������� (ҡ��ʱ�������������</b>~^<b>(207)����ӡˢ�ؼ���ʶ���� (�ͺš���ѹ�����ʡ�UL��CE��ʶ���ͻ�LOG��ʶ��) ����ӡˢ��</b>~^<b>(208)��װӡˢ����չ���Ʒ��̮������</b>~^<b>(209)���ֻ����ܰ�װ���г���</b>~^<b>(210)����ͽ�������⣨��������</b>~^<b>(211)���ʡ��������������쳣</b>~^<b>(212)���ܲ��Բ�ͨ����ÿ����6ֻ��AC=0)</b>~^<b>(213)������ܼ��������Լ�鲻ͨ����ÿ����2ֻ��AC=0)</b>~^<b>(214)���ù�����ѹ/�����¶����鲻ͨ�����²�Ʒ��һ������</b>~^<b>(215)���γߴ粻���ϼ���Ҫ��ÿ����6ֻ��AC=0)</b>~^<b>(216)��������ȱ��</b>~^<b>PPM</b>~^<b>(301)����ʱ��������覴ã�������Ʒ��������ɫ��ߵ��</b>~^<b>(302)�ܼ���ɢ������͸�����������ر��Ρ��ѽ����ܼ���ɫ��ߵ��</b>~^<b>(303)��ͷδš�����м�϶����0.5mm) ���ܼ���ɢ����������֮���м�϶����1.0mm)</b>~^<b>(304)����������˵��ϵĲ�һ��</b>~^<b>(305)��װ��/��/�����������벻������ɨ����󣬰�װ������б</b>~^<b>(306)������������ë�̡�ͭͷ��ȱ�𡢽��忪�ѣ�ҡ������������</b>~^<b>(307)ɢ�����ܼ������ֵ��ᡢ�ߵ㡢���ۡ��ѷ۵���1�����������2mm��С��2mm�ĵ���ߵ㡢���ۡ��ѷ�2��</b>~^<b>(308)�ʺд���PETĤճ�᲻�ι̡��ѽ����ѵ�,չ���Ʒ���ѽ�������</b>~^<b>(309)������Ҫȱ��</b>~^<b>PPM2</b>~^<b>(401)ɢ���������֡�͸���ᣬ�ܼ���ɢ������������ϴ�λ</b>~^<b>(402)ɢ��������������΢ɫ�ߡ��۵㡢���ס����ۡ�ȱ�����᲻����</b>~^<b>(403)�ƿ�©��ճ�ϼ�����������������</b>~^<b>(404)����ӡˢһ���ʶ����</b>~^<b>(405)�ܷ⿪�ѣ�����ë���뻮��</b>~^<b>(406)ֽ�а�װ��Ʒ���������ֽ�в���������ë�ߺ�ӡˢ����</b>~^<b>(407)ɢ�����ܼ������ֵ��ᡢ�ѷۡ��ߵ㡢���۵���1�����������1mm��С��1mm�ĵ��ᡢ�ߵ㡢���ۡ��ѷ���</b>~^<b>(408)���ܰ�װ��������</b>~^<b>(409)��װ��ʽ����ȷ����ͷ�ġ����������/���ס��а��©�� ����װ��������</b>~^<b>(410)��װ����ӡˢģ���������©;��ɫ��װ����������ɫ���ӡ��ɫ</b>~^<b>(411)�⡢��������������©��������λ�ò��Ի��ѽ�����Ʒ�����²���</b>~^<b>(412)���䳱ʪ�����Ρ��𻵣����̵�����</b>~^<b>(413)������Ҫȱ��</b>~^<b>PPM3</b>~^";

        DataTable DT = qc.SelectLampSamplingIncoming(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "LED��Ʒ", false, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("��������Ϊ��!");
        }
    }
    protected void btn_TcpCFPLampSamplingreport_Click(object sender, EventArgs e)
    {
        string txtStdDate2 = DateTime.Now.ToShortDateString();
        string txtEndDate2 = DateTime.Now.AddDays(1).ToShortDateString();
        if (txtStdDate31.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate31.Text.Trim());
                txtStdDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���ڸ�ʽ����ȷ!');Form1.txtStdDate31.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ����ʼ����!');Form1.txtStdDate31.focus();";
            return;
        }
        if (txtEndDate31.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate31.Text.Trim());
                txtEndDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ!');Form1.txtEndDate31.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ���������!');Form1.txtEndDate31.focus();";
            return;
        }
        int uID = int.Parse(Session["uID"].ToString());
        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        //string title = "<b>��������</b>~^<b>������</b>~^<b>�ϸ�����</b>~^<b>���ϸ�����</b>~^<b>һ�γ�����ν�����</b>~^<b>Ͷ����</b>~^<b>��������</b>~^<b>ȱ����</b>~^<b>һ�γ��ϸ���</b>~^<b>(101)��˿��¶��ָ�ܿ����ͷ֮�䣩</b>~^<b>(102)�ƿǡ����֡�ɢ������͸��������</b>~^<b>(103)��ͷ������Ť������еǿ�ȣ�������Ҫ��ÿ����6ֻ AC=0��</b>~^<b>(104)ͭͷ���䡢δ��ס�ͭͷ���㴦�в�������ͷ�޶�������í��</b>~^<b>(105)��Ʒ��ѹ�ⲻͨ����ÿ����6ֻ AC=0��</b>~^<b>(106)��װ�ڲ�Ʒ���������ְ�װӡˢ�ͺ����Ʒ�ͺŲ�ƥ��</b>~^<b>(107)ˤ�����鲻�ϸ񣨵�һ����������</b>~^<b>(108)ȱ��</b>~^<b>(109)BOM�汾�˶ԣ�model/PCB Versions/LED versions/˿ӡ/��װ marking������ʾ���</b>~^<b>(110)��������ˮ��Ʒ��ˮ����ÿ����20ֻ AC=0����ͨ��</b>~^<b>PPM1</b>~^<b>(201)�Ʋ���������˸,���ַ���ܲ���(��ѹ����)��������ɫ��ɫ��</b>~^<b>(202)͸�����ѡ����ػ��ۻ�͸�����䲻ͬ����͸����һ������ʱ����ȡ6ֻ��Ʒ���ڰ����ڣ����ѹ�µ�����������</b>~^<b>(203)�������ѡ�  ��δ�����</b>~^<b>(204)ͭͷ���ر��Σ�����,����ͨ����Ӧͨ��</b>~^<b>(205)��ͷ��Ƭ�ɶ���ƫ����©��������í���ɶ�</b>~^<b>(206)�ƿǿ��ѡ����������� (ҡ��ʱ�������������</b>~^<b>(207)����ӡˢ�ؼ���ʶ���� (�ͺš���ѹ�����ʡ�UL��CE��ʶ���ͻ�LOG��ʶ��) ����ӡˢ��</b>~^<b>(208)��װӡˢ����չ���Ʒ��̮������</b>~^<b>(209)���ֻ����ܰ�װ���г���</b>~^<b>(210)����ͽ�������⣨��������</b>~^<b>(211)���ʡ��������������쳣</b>~^<b>(212)���ܲ��Բ�ͨ����ÿ����6ֻ��AC=0)</b>~^<b>(213)������ܼ��������Լ�鲻ͨ����ÿ����2ֻ��AC=0)</b>~^<b>(214)���ù�����ѹ/�����¶����鲻ͨ�����²�Ʒ��һ������</b>~^<b>(215)���γߴ粻���ϼ���Ҫ��ÿ����6ֻ��AC=0)</b>~^<b>(216)��������ȱ��</b>~^<b>PPM</b>~^<b>(301)����ʱ��������覴ã�������Ʒ��������ɫ��ߵ��</b>~^<b>(302)�ܼ���ɢ������͸�����������ر��Ρ��ѽ����ܼ���ɫ��ߵ��</b>~^<b>(303)��ͷδš�����м�϶����0.5mm) ���ܼ���ɢ����������֮���м�϶����1.0mm)</b>~^<b>(304)����������˵��ϵĲ�һ��</b>~^<b>(305)��װ��/��/�����������벻������ɨ����󣬰�װ������б</b>~^<b>(306)������������ë�̡�ͭͷ��ȱ�𡢽��忪�ѣ�ҡ������������</b>~^<b>(307)ɢ�����ܼ������ֵ��ᡢ�ߵ㡢���ۡ��ѷ۵���1�����������2mm��С��2mm�ĵ���ߵ㡢���ۡ��ѷ�2��</b>~^<b>(308)�ʺд���PETĤճ�᲻�ι̡��ѽ����ѵ�,չ���Ʒ���ѽ�������</b>~^<b>(309)������Ҫȱ��</b>~^<b>PPM2</b>~^<b>(401)ɢ���������֡�͸���ᣬ�ܼ���ɢ������������ϴ�λ</b>~^<b>(402)ɢ��������������΢ɫ�ߡ��۵㡢���ס����ۡ�ȱ�����᲻����</b>~^<b>(403)�ƿ�©��ճ�ϼ�����������������</b>~^<b>(404)����ӡˢһ���ʶ����</b>~^<b>(405)�ܷ⿪�ѣ�����ë���뻮��</b>~^<b>(406)ֽ�а�װ��Ʒ���������ֽ�в���������ë�ߺ�ӡˢ����</b>~^<b>(407)ɢ�����ܼ������ֵ��ᡢ�ѷۡ��ߵ㡢���۵���1�����������1mm��С��1mm�ĵ��ᡢ�ߵ㡢���ۡ��ѷ���</b>~^<b>(408)���ܰ�װ��������</b>~^<b>(409)��װ��ʽ����ȷ����ͷ�ġ����������/���ס��а��©�� ����װ��������</b>~^<b>(410)��װ����ӡˢģ���������©;��ɫ��װ����������ɫ���ӡ��ɫ</b>~^<b>(411)�⡢��������������©��������λ�ò��Ի��ѽ�����Ʒ�����²���</b>~^<b>(412)���䳱ʪ�����Ρ��𻵣����̵�����</b>~^<b>(413)������Ҫȱ��</b>~^<b>PPM3</b>~^";
        string title = "<b>��������</b>~^<b>������</b>~^<b>�ϸ�����</b>~^<b>���ϸ�����</b>~^<b>һ�γ�����ν�����</b>~^<b>Ͷ����</b>~^<b>��������</b>~^<b>ȱ����</b>~^<b>һ�γ��ϸ���</b>~^<b>(101)��˿��¶���ƹ����ܸǡ��ܿ����ܸ�֮�䣩</b>~^<b>(102)�ƿǡ���������</b>~^<b>(103)��ͷ������Ť��������Ҫ��</b>~^<b>(104)ͭͷ���䡢δ���</b>~^<b>(105)ȱ��</b>~^<b>(106)��װ�ڲ�Ʒ����</b>~^<b>(107)���ְ�װӡˢ�ͺ����Ʒ�ͺŲ�ƥ��</b>~^<b>(108)BOM�汾�˶ԣ�model/PCB Versions/˿ӡ/��װ marking������ʾ���</b>~^<b>(109)��������ȱ��</b>~^<b>����ȱ��PPM</b>~^<b>(201)�Ʋ���</b>~^<b>(202)�ƹܲ����ԡ���˸</b>~^<b>(203)�Ƽ��γߴ糬����Χ</b>~^<b>(204)�����ɶ�/����</b>~^<b>(205)��ͷ���ر��Σ����ܳ�����Ӧͨ��</b>~^<b>(206)��ͷ�޶�������ͷ��Ƭ�ɶ�</b>~^<b>(207)�ƿǿ��ѡ���������������ࣩ</b>~^<b>(208)����ӡˢ�ؼ���ʶ���壨��ѹ�����ʡ�UL��ʶ�ȣ�</b>~^<b>(209)��װӡˢ�����������ϵƱ��ѹ�����ʵ�</b>~^<b>(210)���ֻ����ܰ�װ���г���</b>~^<b>(211)��˿��¶</b>~^<b>(212)����ͽ��������</b>~^<b>(213)��������</b>~^<b>(214)�ƹ�Ӧ��/©��</b>~^<b>(215)����Ӧ��</b>~^<b>(216)��������ȱ��</b>~^<b>(217)����������4��1Сʱѹ���ϸ�</b>~^<b>����ȱ��PPM</b>~^<b>(301)����ʱ�ƹ�������覴ã�������Ʒ��</b>~^<b>(302)�ƹ����ر��Σ�������Ʒ��</b>~^<b>(303)��ͷδš�����м�϶��>0.5mm��</b>~^<b>(304)(304)����������˵��ϵĲ�һ��</b>~^<b>(305)��װ��/��/�����������벻������ɨ�����</b>~^<b>(306)��������</b>~^<b>(307)�ƹ��ɶ����ѽ�</b>~^<b>(308)����������Ҫȱ��</b>~^<b>��Ҫȱ��PPM</b>~^<b>(401)�ƹ��ᣨ������Ʒ��</b>~^<b>(402)�ƹܿ���������΢�ѷۡ�ɫ�ߡ��۵㡢��˿�ȣ�������Ʒ��</b>~^<b>(403)(403)�ƿ�©��ճ�������������������</b>~^<b>(404)�ƽ��಻�������Գ��ߣ���2mm����ȱ��</b>~^<b>(405)�ܷ⿪�ѣ�����ë���뻮��</b>~^<b>(406)ֽ�а�װ��Ʒ���������ֽ�в���������ë�ߺ�ӡˢ����</b>~^<b>(407)����������Բ���</b>~^<b>(408)���ܰ�װ��������</b>~^<b>(409)��װ��ʽ����ȷ����ͷ�ġ����������/���ס��а��©��</b>~^<b>(410)��װ����ӡˢģ���������©���ʺа�װ����������ɫ��</b>~^<b>(411)��������������©��������λ�ò��Ի��ѽ�</b>~^<b>(412)���䳱ʪ�����Ρ��𻵡����̵�����</b>~^<b>(413)�������ƴ�Ҫȱ��</b>~^<b>��Ҫȱ��PPM</b>~^";

        DataTable DT = qc.SelectCFLLampSamplingIncoming(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "CFL��Ʒ", true, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("��������Ϊ��!");
        }
    }
    protected void btn_TcpLEDLampSampling_Click(object sender, EventArgs e)
    {
        string txtStdDate2 = DateTime.Now.ToShortDateString();
        string txtEndDate2 = DateTime.Now.AddDays(1).ToShortDateString();
        if (txtStdDate32.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate32.Text.Trim());
                txtStdDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���ڸ�ʽ����ȷ!');Form1.txtStdDate32.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ����ʼ����!');Form1.txtStdDate32.focus();";
            return;
        }
        if (txtEndDate32.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate32.Text.Trim());
                txtEndDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ!');Form1.txtEndDate32.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ���������!');Form1.txtEndDate32.focus();";
            return;
        }
        int uID = int.Parse(Session["uID"].ToString());
        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        //string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>��������</b>~^<b>������</b>~^<b>�ϸ�����</b>~^<b>���ϸ�����</b>~^<b>һ�γ�����ν�����</b>~^<b>Ͷ����</b>~^<b>��������</b>~^<b>ȱ����</b>~^<b>һ�γ��ϸ���</b>~^<b>(101)��˿��¶��ָ�ܿ����ͷ֮�䣩</b>~^<b>(102)�ƿǡ����֡�ɢ������͸��������</b>~^<b>(103)��ͷ������Ť������еǿ�ȣ�������Ҫ��ÿ����6ֻ AC=0��</b>~^<b>(104)ͭͷ���䡢δ��ס�ͭͷ���㴦�в�������ͷ�޶�������í��</b>~^<b>(105)��Ʒ��ѹ�ⲻͨ����ÿ����6ֻ AC=0��</b>~^<b>(106)��װ�ڲ�Ʒ���������ְ�װӡˢ�ͺ����Ʒ�ͺŲ�ƥ��</b>~^<b>(107)ˤ�����鲻�ϸ񣨵�һ����������</b>~^<b>(108)ȱ��</b>~^<b>(109)BOM�汾�˶ԣ�model/PCB Versions/LED versions/˿ӡ/��װ marking������ʾ���</b>~^<b>(110)��������ˮ��Ʒ��ˮ����ÿ����20ֻ AC=0����ͨ��</b>~^<b>PPM1</b>~^<b>(201)�Ʋ���������˸,���ַ���ܲ���(��ѹ����)��������ɫ��ɫ��</b>~^<b>(202)͸�����ѡ����ػ��ۻ�͸�����䲻ͬ����͸����һ������ʱ����ȡ6ֻ��Ʒ���ڰ����ڣ����ѹ�µ�����������</b>~^<b>(203)�������ѡ�  ��δ�����</b>~^<b>(204)ͭͷ���ر��Σ�����,����ͨ����Ӧͨ��</b>~^<b>(205)��ͷ��Ƭ�ɶ���ƫ����©��������í���ɶ�</b>~^<b>(206)�ƿǿ��ѡ����������� (ҡ��ʱ�������������</b>~^<b>(207)����ӡˢ�ؼ���ʶ���� (�ͺš���ѹ�����ʡ�UL��CE��ʶ���ͻ�LOG��ʶ��) ����ӡˢ��</b>~^<b>(208)��װӡˢ����չ���Ʒ��̮������</b>~^<b>(209)���ֻ����ܰ�װ���г���</b>~^<b>(210)����ͽ�������⣨��������</b>~^<b>(211)���ʡ��������������쳣</b>~^<b>(212)���ܲ��Բ�ͨ����ÿ����6ֻ��AC=0)</b>~^<b>(213)������ܼ��������Լ�鲻ͨ����ÿ����2ֻ��AC=0)</b>~^<b>(214)���ù�����ѹ/�����¶����鲻ͨ�����²�Ʒ��һ������</b>~^<b>(215)���γߴ粻���ϼ���Ҫ��ÿ����6ֻ��AC=0)</b>~^<b>(216)��������ȱ��</b>~^<b>PPM</b>~^<b>(301)����ʱ��������覴ã�������Ʒ��������ɫ��ߵ��</b>~^<b>(302)�ܼ���ɢ������͸�����������ر��Ρ��ѽ����ܼ���ɫ��ߵ��</b>~^<b>(303)��ͷδš�����м�϶����0.5mm) ���ܼ���ɢ����������֮���м�϶����1.0mm)</b>~^<b>(304)����������˵��ϵĲ�һ��</b>~^<b>(305)��װ��/��/�����������벻������ɨ����󣬰�װ������б</b>~^<b>(306)������������ë�̡�ͭͷ��ȱ�𡢽��忪�ѣ�ҡ������������</b>~^<b>(307)ɢ�����ܼ������ֵ��ᡢ�ߵ㡢���ۡ��ѷ۵���1�����������2mm��С��2mm�ĵ���ߵ㡢���ۡ��ѷ�2��</b>~^<b>(308)�ʺд���PETĤճ�᲻�ι̡��ѽ����ѵ�,չ���Ʒ���ѽ�������</b>~^<b>(309)������Ҫȱ��</b>~^<b>PPM2</b>~^<b>(401)ɢ���������֡�͸���ᣬ�ܼ���ɢ������������ϴ�λ</b>~^<b>(402)ɢ��������������΢ɫ�ߡ��۵㡢���ס����ۡ�ȱ�����᲻����</b>~^<b>(403)�ƿ�©��ճ�ϼ�����������������</b>~^<b>(404)����ӡˢһ���ʶ����</b>~^<b>(405)�ܷ⿪�ѣ�����ë���뻮��</b>~^<b>(406)ֽ�а�װ��Ʒ���������ֽ�в���������ë�ߺ�ӡˢ����</b>~^<b>(407)ɢ�����ܼ������ֵ��ᡢ�ѷۡ��ߵ㡢���۵���1�����������1mm��С��1mm�ĵ��ᡢ�ߵ㡢���ۡ��ѷ���</b>~^<b>(408)���ܰ�װ��������</b>~^<b>(409)��װ��ʽ����ȷ����ͷ�ġ����������/���ס��а��©�� ����װ��������</b>~^<b>(410)��װ����ӡˢģ���������©;��ɫ��װ����������ɫ���ӡ��ɫ</b>~^<b>(411)�⡢��������������©��������λ�ò��Ի��ѽ�����Ʒ�����²���</b>~^<b>(412)���䳱ʪ�����Ρ��𻵣����̵�����</b>~^<b>(413)������Ҫȱ��</b>~^<b>PPM3</b>~^";

        DataTable DT = qc.SelectLampSamplingIncoming(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "LED��Ʒ", true, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
                            
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("��������Ϊ��!");
        }
    }
 
   protected void btn_DSD_Click(object sender, EventArgs e)
    {
        string txtStdDate2 = DateTime.Now.ToShortDateString();
        string txtEndDate2 = DateTime.Now.AddDays(1).ToShortDateString();

        if (txtStdDate23.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate23.Text.Trim());
                txtStdDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���ڸ�ʽ����ȷ!');Form1.txtStdDate23.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ����ʼ����!');Form1.txtStdDate23.focus();";
            return;
        }

        if (txtEndDate23.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate23.Text.Trim());
                txtEndDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ!');Form1.txtEndDate23.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ���������!');Form1.txtEndDate23.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        string strImport = "qc_report_daily_DSD_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>��������</b>~^<b>������</b>~^<b>�ϸ�����</b>~^<b>���ϸ�����</b>~^<b>һ�γ�����ν�����</b>~^<b>Ͷ����</b>~^<b>��������</b>~^<b>ȱ����</b>~^<b>һ�γ��ϸ���</b>~^"
                        + "<b>(101)��˿��¶���ƿǡ����֣���ͷ������</b>~^<b>(102)��ͷŤ������ (ÿ����6ֻ��</b>~^<b>(103)��ѹ���� (ÿ����6ֻ��</b>~^<b>(104)��װ�ڲ�Ʒ���������ְ�װӡˢ�ͺ����Ʒ�ͺŲ�ƥ��</b>~^"
                        +"<b>(105)ˤ�����飨��һ��������Ʒ��1����֤��</b>~^<b>(106)��������ȱ��</b>~^<b>����ȱ��|����ȱ��PPMֵ</b>~^<b>(201)��Ʒ����</b>~^<b>(202)��Ʒ���䣬�Ѻ�</b>~^<b>(203)�����ܲ��Բ�ͨ����ÿ����2ֻ��AC=0)</b>~^"
                        +"<b>(204)���γߴ缰���� ��ÿ����6ֻ��</b>~^<b>(205)�����ܲ��Բ�ͨ����ÿ����6ֻ��AC=0)</b>~^<b>(206)ө��濪�ز��ԣ�ÿ����6ֻ��AC=0)</b>~^<b>(207)��ͷ���ر��Σ�����,����ͨ����Ӧ��ͷ�棨ÿ����6ֻ��AC=0)</b>~^"
                        +"<b>(208)��������ȱ��</b>~^<b>����ȱ��PPMֵ</b>~^<b>(301)��Ʒ�ɶ�������</b>~^<b>(302)��Ʒ���ˣ��������ۣ�ɫ��</b>~^<b>(303)��Ʒ������������</b>~^<b>(304)��Ʒ�����Ա��Σ�ƽ���ȷ���</b>~^<b>(305)����ӡˢ�ؼ���ʶ�����ӡˢ��</b>~^"
                        +"<b>(306)��װ,˵����ӡˢ����</b>~^<b>(307)������</b>~^<b>(308)������Ҫȱ��</b>~^<b>��Ҫȱ��PPMֵ</b>~^<b>(401)��Ʒ��Ӱ�������</b>~^<b>(402)��Ʒ�������ɫ����࣬����</b>~^<b>(403)����ӡˢһ���ʶ����</b>~^<b>(404)��װ��������ް��룬��λ������</b>~^"
                        +"<b>(405)��װ��/��/�����������벻������ɨ�����</b>~^<b>(406)���䣬������������</b>~^<b>(407)���������ŵȳ�����Ϣ</b>~^<b>(408)������Ҫȱ��</b>~^<b>��Ҫȱ��PPMֵ</b>~^";
        DataTable DT = qc.SelectDSD(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "��˿��", false, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("��������Ϊ��!");
        }
    }

  protected void btn_TcpDSD_Click(object sender, EventArgs e)
    {
        string txtStdDate2 = DateTime.Now.ToShortDateString();
        string txtEndDate2 = DateTime.Now.AddDays(1).ToShortDateString();
        if (txtStdDate33.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate33.Text.Trim());
                txtStdDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���ڸ�ʽ����ȷ!');Form1.txtStdDate33.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ����ʼ����!');Form1.txtStdDate33.focus();";
            return;
        }
        if (txtEndDate33.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate33.Text.Trim());
                txtEndDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ!');Form1.txtEndDate33.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ���������!');Form1.txtEndDate33.focus();";
            return;
        }
        int uID = int.Parse(Session["uID"].ToString());
        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        //string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        string strImport = "qc_report_daily_DSD_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>��������</b>~^<b>������</b>~^<b>�ϸ�����</b>~^<b>���ϸ�����</b>~^<b>һ�γ�����ν�����</b>~^<b>Ͷ����</b>~^<b>��������</b>~^<b>ȱ����</b>~^<b>һ�γ��ϸ���</b>~^"
                        + "<b>(101)��˿��¶���ƿǡ����֣���ͷ������</b>~^<b>(102)��ͷŤ������ (ÿ����6ֻ��</b>~^<b>(103)��ѹ���� (ÿ����6ֻ��</b>~^<b>(104)��װ�ڲ�Ʒ���������ְ�װӡˢ�ͺ����Ʒ�ͺŲ�ƥ��</b>~^"
                        + "<b>(105)ˤ�����飨��һ��������Ʒ��1����֤��</b>~^<b>(106)��������ȱ��</b>~^<b>����ȱ��|����ȱ��PPMֵ</b>~^<b>(201)��Ʒ����</b>~^<b>(202)��Ʒ���䣬�Ѻ�</b>~^<b>(203)�����ܲ��Բ�ͨ����ÿ����2ֻ��AC=0)</b>~^"
                        + "<b>(204)���γߴ缰���� ��ÿ����6ֻ��</b>~^<b>(205)�����ܲ��Բ�ͨ����ÿ����6ֻ��AC=0)</b>~^<b>(206)ө��濪�ز��ԣ�ÿ����6ֻ��AC=0)</b>~^<b>(207)��ͷ���ر��Σ�����,����ͨ����Ӧ��ͷ�棨ÿ����6ֻ��AC=0)</b>~^"
                        + "<b>(208)��������ȱ��</b>~^<b>����ȱ��PPMֵ</b>~^<b>(301)��Ʒ�ɶ�������</b>~^<b>(302)��Ʒ���ˣ��������ۣ�ɫ��</b>~^<b>(303)��Ʒ������������</b>~^<b>(304)��Ʒ�����Ա��Σ�ƽ���ȷ���</b>~^<b>(305)����ӡˢ�ؼ���ʶ�����ӡˢ��</b>~^"
                        + "<b>(306)��װ,˵����ӡˢ����</b>~^<b>(307)������</b>~^<b>(308)������Ҫȱ��</b>~^<b>��Ҫȱ��PPMֵ</b>~^<b>(401)��Ʒ��Ӱ�������</b>~^<b>(402)��Ʒ�������ɫ����࣬����</b>~^<b>(403)����ӡˢһ���ʶ����</b>~^<b>(404)��װ��������ް��룬��λ������</b>~^"
                        + "<b>(405)��װ��/��/�����������벻������ɨ�����</b>~^<b>(406)���䣬������������</b>~^<b>(407)���������ŵȳ�����Ϣ</b>~^<b>(408)������Ҫȱ��</b>~^<b>��Ҫȱ��PPMֵ</b>~^";

        DataTable DT = qc.SelectDSD(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "��˿��", true, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("��������Ϊ��!");
        }
    }

       protected void btn_T8_Click(object sender, EventArgs e)
    {
        string txtStdDate2 = DateTime.Now.ToShortDateString();
        string txtEndDate2 = DateTime.Now.AddDays(1).ToShortDateString();
        if (txtStdDate24.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate24.Text.Trim());
                txtStdDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���ڸ�ʽ����ȷ!');Form1.txtStdDate24.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ����ʼ����!');Form1.txtStdDate24.focus();";
            return;
        }
        if (txtEndDate24.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate24.Text.Trim());
                txtEndDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ!');Form1.txtEndDate24.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ���������!');Form1.txtEndDate24.focus();";
            return;
        }
        int uID = int.Parse(Session["uID"].ToString());
        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        //string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        string strImport = "qc_report_daily_T8_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>��������</b>~^<b>������</b>~^<b>�ϸ�����</b>~^<b>���ϸ�����</b>~^<b>һ�γ�����ν�����</b>~^<b>Ͷ����</b>~^<b>������</b>~^<b>��ȱ����</b>~^<b>�ϸ���</b>~^"
            + "<b>�ؼ�ȱ��|(101)��װ���г��ӡ���Ƭ���������</b>~^<b>�ؼ�ȱ��|(102)�ƿǡ��ƹ�����</b>~^<b>�ؼ�ȱ��|(103)��װȱ��</b>~^<b>�ؼ�ȱ��|(104)��ͷŤ������6֧AC=0</b>~^<b>�ؼ�ȱ��|(105)��ͷ����</b>~^<b>�ؼ�ȱ��|(106)��ѹ���ԣ�����ѹҪ��Ĳ�Ʒ��6֧ AC=0</b>~^<b>�ؼ�ȱ��|(107)��װ���������ְ�װӡˢ�ͺ����Ʒ�ͺŲ�ƥ��</b>~^<b>�ؼ�ȱ��|�ؼ�ȱ��PPMֵ</b>~^<b>��Ҫȱ��|(201)���γߴ�6֧ AC=0</b>~^<b>��Ҫȱ��|(202)�ƿ�©��ճ�ϼ�����������</b>~^<b>��Ҫȱ��|(203)�Ʋ������ֲ�����������˸��������ɫ��ɫ��</b>~^<b>��Ҫȱ��|(204)�������ͷ���ܼ�����ʶ����һ�£����ŷ���</b>~^<b>��Ҫȱ��|(205)�ƹܿ��ѡ�����</b>~^"
            + "<b>��Ҫȱ��|(206)��������������ҡ��ʱ������</b>~^<b>��Ҫȱ��|(207)��δ�����</b>~^<b>��Ҫȱ��|(208)����ӡˢ�ؼ���ʶ���壨����ʶ�𣩣��ͺš���ѹ�����ʡ�UL�����ͻ�LOGO��ʶ�ȣ���ӡˢ��</b>~^<b>��Ҫȱ��|(209)��װӡˢ����������ɨ�����</b>~^<b>��Ҫȱ��|(210)������6֧��AC=1</b>~^"
            + "<b>��Ҫȱ��|(211)��ű���</b>~^<b>��Ҫȱ��|(212)������Ų���ͬһƽ�治��ͨ���໥ƽ�еļ���оߣ������棩</b>~^<b>��Ҫȱ��|(213)δ���룬��δ�嵽λ��ɽӴ�����</b>~^<b>��Ҫȱ��|(214)��ͷ����ñ����װ��б���޷�ͨ������</b>~^<b>��Ҫȱ��|��Ҫȱ��PPMֵ</b>~^"
            + "<b>��Ҫȱ��|(301)LED�����������ȣ��м��жϽ���չ����ȵ��ڹ��չ涨��Χ</b>~^<b>��Ҫȱ��|(302)LED�������������90�㶨λ��ƫ�빤�չ涨��Χ</b>~^<b>��Ҫȱ��|(303)Ϳ�۲����������������ڲ����ϣ����ֲ�©Ϳ�ۡ��ƽŻ�����©������©��</b>~^"
            + "<b>��Ҫȱ��|(304)��ͷ�ܼ����ر��Σ���ȱ�����ƣ��ƿ�δ¶�����Ŀ���</b>~^<b>��Ҫȱ��|(305)����������ǽ������ҡ������������</b>~^<b>��Ҫȱ��|(306)����������˵��ϵĲ�һ��</b>~^<b>��Ҫȱ��|(307)��װ��/��/�����������벻������ɨ�費������װ������б</b>~^"
            + "<b>��Ҫȱ��|��Ҫȱ��PPMֵ</b>~^<b>��ȱ��|(401)�ƹ�A���Сݧ�2.0mm��ɫ�ߡ��ߵ㡢��ʯ���ڵ�ȣ���10CM�ڧ�0.5-��2.0mm����2��������0.5mm����.�ƹ�A������ͷ�˲�15mm������ķ۵㲻��Ҫ��B��10CM�ڧ�1.0-��2.0</b>~^<b>��ȱ��|(402)A��۲�ݧ�2.0mm�۵㡢���ۡ���Ȳ�������10CM�ڧ�0.5-��2.0mm����2��������0.5mm����.B��10CM�ڧ�1.0-��2.0mm����3��������1.0mm����</b>~^"
            + "<b>��ȱ��|(403)��ͷA��ڵ㡢�׵�����3����0.5-1mm֮�䣬���벻С��300mm����1.0-1.5mm��������1��������0.5�ĵ㲻�ƣ�B��׵㲻�ƣ������ڧ�1.5mm�ڵ㲻�ƣ����ڧ�1.5mm�ڵ㲻����2��</b>~^<b>��ȱ��|(404)A���ܼ����ƹܲ��˻��ۡ�30CM������˻��ۣ�30CM����2����B������3�������Ȳ���</b>~^<b>��ȱ��|(405)A��ƹ����߳��ȡ�30CM����30CM����2����B�治��</b>~^"
            + "<b>��ȱ��|(406)�����Сݧ�2.0mm���ۡ�������0.5-��0.2mm����3��������0.5mm����</b>~^<b>��ȱ��|(407)���������ɶ�������</b>~^<b>��ȱ��|(408)����ӡˢһ���ʶ���壨����ʶ�𣩣����������λ������</b>~^<b>��ȱ��|(409)ֽ�ж˿ڰ��ݡ�����</b>~^"
            + "<b>��ȱ��|(410)��װ��ʽ����ȷ����װ��������</b>~^<b>��ȱ��|(411)ӡˢģ�����壨����ʶ�𣩻���©����</b>~^<b>��ȱ��|(412)��ɫ��װ����������ɫ���ӡ��ɫ</b>~^<b>��ȱ��|(413)��������������©��������λ�ò��Ի��ѽ�����Ʒ�����²���</b>~^<b>��ȱ��|(414)��װ�䳱ʪ�����Ρ��𻵵�</b>~^<b>��ȱ��|��ȱ��PPMֵ</b>~^";

        DataTable DT = qc.SelectT8(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "LED T8ֱ�ܵ�", false, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        //DataTable DT = qc.SelectDSD(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "��˿��", false, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("��������Ϊ��!");
        }
    }

   protected void btn_TcpT8_Click(object sender, EventArgs e)
    {
        string txtStdDate2 = DateTime.Now.ToShortDateString();
        string txtEndDate2 = DateTime.Now.AddDays(1).ToShortDateString();
        if (txtStdDate34.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtStdDate34.Text.Trim());
                txtStdDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('��ʼ���ڸ�ʽ����ȷ!');Form1.txtStdDate34.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ����ʼ����!');Form1.txtStdDate34.focus();";
            return;
        }
        if (txtEndDate34.Text.Trim() != string.Empty)
        {
            try
            {
                DateTime _datetime = DateTime.Parse(txtEndDate34.Text.Trim());
                txtEndDate2 = _datetime.ToShortDateString();
            }
            catch
            {
                ltlAlert.Text = "alert('�������ڸ�ʽ����ȷ!');Form1.txtEndDate34.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('��ѡ���������!');Form1.txtEndDate34.focus();";
            return;
        }
        int uID = int.Parse(Session["uID"].ToString());
        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        //string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        string strImport = "qc_report_daily_T8_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>��������</b>~^<b>������</b>~^<b>�ϸ�����</b>~^<b>���ϸ�����</b>~^<b>һ�γ�����ν�����</b>~^<b>Ͷ����</b>~^<b>������</b>~^<b>��ȱ����</b>~^<b>�ϸ���</b>~^"
             + "<b>�ؼ�ȱ��|(101)��װ���г��ӡ���Ƭ���������</b>~^<b>�ؼ�ȱ��|(102)�ƿǡ��ƹ�����</b>~^<b>�ؼ�ȱ��|(103)��װȱ��</b>~^<b>�ؼ�ȱ��|(104)��ͷŤ������6֧AC=0</b>~^<b>�ؼ�ȱ��|(105)��ͷ����</b>~^<b>�ؼ�ȱ��|(106)��ѹ���ԣ�����ѹҪ��Ĳ�Ʒ��6֧ AC=0</b>~^<b>�ؼ�ȱ��|(107)��װ���������ְ�װӡˢ�ͺ����Ʒ�ͺŲ�ƥ��</b>~^<b>�ؼ�ȱ��|�ؼ�ȱ��PPMֵ</b>~^<b>��Ҫȱ��|(201)���γߴ�6֧ AC=0</b>~^<b>��Ҫȱ��|(202)�ƿ�©��ճ�ϼ�����������</b>~^<b>��Ҫȱ��|(203)�Ʋ������ֲ�����������˸��������ɫ��ɫ��</b>~^<b>��Ҫȱ��|(204)�������ͷ���ܼ�����ʶ����һ�£����ŷ���</b>~^<b>��Ҫȱ��|(205)�ƹܿ��ѡ�����</b>~^"
             + "<b>��Ҫȱ��|(206)��������������ҡ��ʱ������</b>~^<b>��Ҫȱ��|(207)��δ�����</b>~^<b>��Ҫȱ��|(208)����ӡˢ�ؼ���ʶ���壨����ʶ�𣩣��ͺš���ѹ�����ʡ�UL�����ͻ�LOGO��ʶ�ȣ���ӡˢ��</b>~^<b>��Ҫȱ��|(209)��װӡˢ����������ɨ�����</b>~^<b>��Ҫȱ��|(210)������6֧��AC=1</b>~^"
             + "<b>��Ҫȱ��|(211)��ű���</b>~^<b>��Ҫȱ��|(212)������Ų���ͬһƽ�治��ͨ���໥ƽ�еļ���оߣ������棩</b>~^<b>��Ҫȱ��|(213)δ���룬��δ�嵽λ��ɽӴ�����</b>~^<b>��Ҫȱ��|(214)��ͷ����ñ����װ��б���޷�ͨ������</b>~^<b>��Ҫȱ��|��Ҫȱ��PPMֵ</b>~^"
             + "<b>��Ҫȱ��|(301)LED�����������ȣ��м��жϽ���չ����ȵ��ڹ��չ涨��Χ</b>~^<b>��Ҫȱ��|(302)LED�������������90�㶨λ��ƫ�빤�չ涨��Χ</b>~^<b>��Ҫȱ��|(303)Ϳ�۲����������������ڲ����ϣ����ֲ�©Ϳ�ۡ��ƽŻ�����©������©��</b>~^"
             + "<b>��Ҫȱ��|(304)��ͷ�ܼ����ر��Σ���ȱ�����ƣ��ƿ�δ¶�����Ŀ���</b>~^<b>��Ҫȱ��|(305)����������ǽ������ҡ������������</b>~^<b>��Ҫȱ��|(306)����������˵��ϵĲ�һ��</b>~^<b>��Ҫȱ��|(307)��װ��/��/�����������벻������ɨ�費������װ������б</b>~^"
             + "<b>��Ҫȱ��|��Ҫȱ��PPMֵ</b>~^<b>��ȱ��|(401)�ƹ�A���Сݧ�2.0mm��ɫ�ߡ��ߵ㡢��ʯ���ڵ�ȣ���10CM�ڧ�0.5-��2.0mm����2��������0.5mm����.�ƹ�A������ͷ�˲�15mm������ķ۵㲻��Ҫ��B��10CM�ڧ�1.0-��2.0</b>~^<b>��ȱ��|(402)A��۲�ݧ�2.0mm�۵㡢���ۡ���Ȳ�������10CM�ڧ�0.5-��2.0mm����2��������0.5mm����.B��10CM�ڧ�1.0-��2.0mm����3��������1.0mm����</b>~^"
             + "<b>��ȱ��|(403)��ͷA��ڵ㡢�׵�����3����0.5-1mm֮�䣬���벻С��300mm����1.0-1.5mm��������1��������0.5�ĵ㲻�ƣ�B��׵㲻�ƣ������ڧ�1.5mm�ڵ㲻�ƣ����ڧ�1.5mm�ڵ㲻����2��</b>~^<b>��ȱ��|(404)A���ܼ����ƹܲ��˻��ۡ�30CM������˻��ۣ�30CM����2����B������3�������Ȳ���</b>~^<b>��ȱ��|(405)A��ƹ����߳��ȡ�30CM����30CM����2����B�治��</b>~^"
             + "<b>��ȱ��|(406)�����Сݧ�2.0mm���ۡ�������0.5-��0.2mm����3��������0.5mm����</b>~^<b>��ȱ��|(407)���������ɶ�������</b>~^<b>��ȱ��|(408)����ӡˢһ���ʶ���壨����ʶ�𣩣����������λ������</b>~^<b>��ȱ��|(409)ֽ�ж˿ڰ��ݡ�����</b>~^"
             + "<b>��ȱ��|(410)��װ��ʽ����ȷ����װ��������</b>~^<b>��ȱ��|(411)ӡˢģ�����壨����ʶ�𣩻���©����</b>~^<b>��ȱ��|(412)��ɫ��װ����������ɫ���ӡ��ɫ</b>~^<b>��ȱ��|(413)��������������©��������λ�ò��Ի��ѽ�����Ʒ�����²���</b>~^<b>��ȱ��|(414)��װ�䳱ʪ�����Ρ��𻵵�</b>~^<b>��ȱ��|��ȱ��PPMֵ</b>~^";

        DataTable DT = qc.SelectT8(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "LED T8ֱ�ܵ�", true, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("��������Ϊ��!");
        }
    }
       
}
