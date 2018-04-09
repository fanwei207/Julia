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
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate12.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate12.focus();";
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
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate12.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtEndDate12.focus();";
            return;
        }
        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/sp_QC_Report_LineSampling_Incoming.xls");
        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>检验日期</b>~^<b>生产车间</b>~^<b>线号</b>~^<b>线长</b>~^<b>总批次</b>~^<b>合格批次</b>~^<b>不合格批次</b>~^<b>一次抽检批次接收率</b>~^<b>抽样数</b>~^<b>总缺陷数</b>~^<b>合格率</b>~^";

        DataTable DT = qc.SelectLineSamplingIncoming(Convert.ToInt32(Session["uID"]), strStdDate, strEndDate, "CFL成品", Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("导出数据为空!");
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
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate.focus();";
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
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtStdDate.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/sp_QC_Report_LineSampling_Incoming.xls");
        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>检验日期</b>~^<b>生产车间</b>~^<b>线号</b>~^<b>线长</b>~^<b>总批次</b>~^<b>合格批次</b>~^<b>不合格批次</b>~^<b>一次抽检批次接收率</b>~^<b>抽样数</b>~^<b>总缺陷数</b>~^<b>合格率</b>~^";

        DataTable DT = qc.SelectLineSamplingIncoming(Convert.ToInt32(Session["uID"]), strStdDate, strEndDate, "LED成品", Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("导出数据为空!");
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
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate22.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate22.focus();";
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
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate22.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtEndDate22.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        //string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>检验日期</b>~^<b>总批次</b>~^<b>合格批次</b>~^<b>不合格批次</b>~^<b>一次抽检批次接收率</b>~^<b>投入数</b>~^<b>抽样总数</b>~^<b>缺陷数</b>~^<b>一次抽检合格率</b>~^<b>(101)导丝外露（灯管与塑盖、塑壳与塑盖之间）</b>~^<b>(102)灯壳、灯罩脱落</b>~^<b>(103)灯头、灯罩扭力不符合要求</b>~^<b>(104)铜头脱落、未打孔</b>~^<b>(105)缺料</b>~^<b>(106)包装内产品混淆</b>~^<b>(107)各种包装印刷型号与产品型号不匹配</b>~^<b>(108)BOM版本核对（model/PCB Versions/丝印/包装 marking（含警示语））</b>~^<b>(109)其它致命缺陷</b>~^<b>致命缺陷PPM</b>~^<b>(201)灯不亮</b>~^<b>(202)灯管不启辉、闪烁</b>~^<b>(203)灯几何尺寸超出范围</b>~^<b>(204)灯罩松动/破碎</b>~^<b>(205)灯头严重变形，不能超过相应通规</b>~^<b>(206)灯头无顶锡、灯头焊片松动</b>~^<b>(207)灯壳开裂、壳内有异物（金属类）</b>~^<b>(208)壳体印刷关键标识不清（电压、功率、UL标识等）</b>~^<b>(209)包装印刷、标贴不符合灯标电压、功率等</b>~^<b>(210)灯罩或吸塑包装内有虫子</b>~^<b>(211)导丝外露</b>~^<b>(212)调光和交流声检测</b>~^<b>(213)电流测试</b>~^<b>(214)灯管应裂/漏气</b>~^<b>(215)灯罩应裂</b>~^<b>(216)其它严重缺陷</b>~^<b>(217)本身重量的4倍1小时压力合格</b>~^<b>严重缺陷PPM</b>~^<b>(301)点亮时灯管有明显瑕疵（极限样品）</b>~^<b>(302)灯管严重变形（极限样品）</b>~^<b>(303)灯头未拧紧、有间隙（>0.5mm）</b>~^<b>(304)(304)订单号与出运单上的不一致</b>~^<b>(305)包装盒/箱/标贴的条形码不完整、扫描错误</b>~^<b>(306)焊锡不良</b>~^<b>(307)灯管松动、脱胶</b>~^<b>(308)其它整灯主要缺陷</b>~^<b>主要缺陷PPM</b>~^<b>(401)灯管歪（极限样品）</b>~^<b>(402)灯管可视面有轻微脱粉、色斑、污点、拉丝等（极限样品）</b>~^<b>(403)(403)灯壳漏打粘结剂、整灯有明显脏污</b>~^<b>(404)灯胶泥不可有明显超高（≤2mm）、缺胶</b>~^<b>(405)塑封开裂，明显毛刺与划痕</b>~^<b>(406)纸盒包装产品需封盒完好且纸盒不可有明显毛边和印刷不清</b>~^<b>(407)吊卡打孔明显不正</b>~^<b>(408)吸塑包装内有异物</b>~^<b>(409)包装方式不正确，灯头衬、保护瓦楞层/衬套、夹板等漏放</b>~^<b>(410)包装外箱印刷模糊不清或遗漏；彩盒包装材料有明显色差</b>~^<b>(411)外箱上有箱贴遗漏、错贴、位置不对或脱胶</b>~^<b>(412)外箱潮湿、变形、损坏、托盘地座损坏</b>~^<b>(413)其它整灯次要缺陷</b>~^<b>次要缺陷PPM</b>~^";

        DataTable DT = qc.SelectCFLLampSamplingIncoming(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "CFL成品", false, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("导出数据为空!");
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
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate21.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate21.focus();";
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
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate21.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtEndDate21.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>检验日期</b>~^<b>总批次</b>~^<b>合格批次</b>~^<b>不合格批次</b>~^<b>一次抽检批次接收率</b>~^<b>投入数</b>~^<b>抽样总数</b>~^<b>缺陷数</b>~^<b>一次抽检合格率</b>~^<b>(101)导丝外露（指塑壳与灯头之间）</b>~^<b>(102)灯壳、灯罩、散热器、透镜等脱落</b>~^<b>(103)灯头、灯罩扭力（机械强度）不符合要求（每批抽6只 AC=0）</b>~^<b>(104)铜头脱落、未打孔、铜头焊点处有残锡、灯头无顶锡、无铆钉</b>~^<b>(105)产品耐压测不通过（每批抽6只 AC=0）</b>~^<b>(106)包装内产品混淆、各种包装印刷型号与产品型号不匹配</b>~^<b>(107)摔箱试验不合格（第一次生产做）</b>~^<b>(108)缺灯</b>~^<b>(109)BOM版本核对（model/PCB Versions/LED versions/丝印/包装 marking（含警示语））</b>~^<b>(110)其它（防水产品防水试验每批抽20只 AC=0）不通过</b>~^<b>PPM1</b>~^<b>(201)灯不亮、灯闪烁,部分发光管不亮(低压测试)，发光颜色有色差</b>~^<b>(202)透镜破裂、严重划痕或透镜脱落不同厂家透镜第一次生产时，抽取6只样品，在暗室内，额定电压下点亮，将光线</b>~^<b>(203)灯罩破裂、  胶未干溢出</b>~^<b>(204)铜头严重变形，开裂,不能通过相应通规</b>~^<b>(205)灯头焊片松动、偏心线漏焊、中心铆钉松动</b>~^<b>(206)灯壳开裂、壳内有异物 (摇晃时有响声，金属物）</b>~^<b>(207)壳体印刷关键标识不清 (型号、电压、功率、UL、CE标识及客户LOG标识等) ，或印刷错</b>~^<b>(208)包装印刷错误，展柜产品无坍塌现象</b>~^<b>(209)灯罩或吸塑包装内有虫子</b>~^<b>(210)调光和交流声检测（按封样）</b>~^<b>(211)功率、功率因数测试异常</b>~^<b>(212)性能测试不通过（每批抽6只，AC=0)</b>~^<b>(213)光电性能及寿命测试检查不通过（每批抽2只，AC=0)</b>~^<b>(214)适用工作电压/工作温度试验不通过（新产品第一次做）</b>~^<b>(215)外形尺寸不符合技术要求（每批抽6只，AC=0)</b>~^<b>(216)其它严重缺陷</b>~^<b>PPM</b>~^<b>(301)点亮时灯有明显瑕疵（极限样品）或如有色差、斑点等</b>~^<b>(302)塑件、散热器、透镜、灯罩严重变形、脱胶或塑件有色差、斑点等</b>~^<b>(303)灯头未拧紧、有间隙（＞0.5mm) 、塑件与散热器及灯罩之间有间隙（＞1.0mm)</b>~^<b>(304)订单号与出运单上的不一致</b>~^<b>(305)包装盒/箱/标贴的条形码不完整、扫描错误，包装灯体歪斜</b>~^<b>(306)焊锡不良、有毛刺、铜头有缺损、胶体开裂，摇动灯内有响声</b>~^<b>(307)散热器塑件、灯罩掉漆、斑点、划痕、脱粉等有1处、面积大于2mm或小于2mm的掉漆斑点、划痕、脱粉2处</b>~^<b>(308)彩盒窗口PET膜粘结不牢固、脱胶开裂等,展柜产品无脱胶，变形</b>~^<b>(309)其它主要缺陷</b>~^<b>PPM2</b>~^<b>(401)散热器、灯罩、透镜歪，塑件、散热器、灯罩配合错位</b>~^<b>(402)散热器可视面有轻微色斑、污点、气孔、划痕、缺损、喷漆不均等</b>~^<b>(403)灯壳漏打粘合剂、整灯有明显脏污</b>~^<b>(404)壳体印刷一般标识不清</b>~^<b>(405)塑封开裂，明显毛刺与划痕</b>~^<b>(406)纸盒包装产品需封盒完好且纸盒不可有明显毛边和印刷不清</b>~^<b>(407)散热器塑件、灯罩掉漆、脱粉、斑点、划痕等有1处、面积大于1mm或小于1mm的掉漆、斑点、划痕、脱粉有</b>~^<b>(408)吸塑包装内有异物</b>~^<b>(409)包装方式不正确；灯头衬、保护瓦楞层/衬套、夹板等漏放 ，包装材料破损</b>~^<b>(410)包装外箱印刷模糊不清或遗漏;彩色包装材料有明显色差；彩印脱色</b>~^<b>(411)外、中箱上有箱贴遗漏、错贴、位置不对或脱胶、产品周期章不符</b>~^<b>(412)外箱潮湿、变形、损坏，托盘底座损坏</b>~^<b>(413)其它次要缺陷</b>~^<b>PPM3</b>~^";

        DataTable DT = qc.SelectLampSamplingIncoming(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "LED成品", false, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("导出数据为空!");
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
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate31.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate31.focus();";
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
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate31.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtEndDate31.focus();";
            return;
        }
        int uID = int.Parse(Session["uID"].ToString());
        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        //string title = "<b>检验日期</b>~^<b>总批次</b>~^<b>合格批次</b>~^<b>不合格批次</b>~^<b>一次抽检批次接收率</b>~^<b>投入数</b>~^<b>抽样总数</b>~^<b>缺陷数</b>~^<b>一次抽检合格率</b>~^<b>(101)导丝外露（指塑壳与灯头之间）</b>~^<b>(102)灯壳、灯罩、散热器、透镜等脱落</b>~^<b>(103)灯头、灯罩扭力（机械强度）不符合要求（每批抽6只 AC=0）</b>~^<b>(104)铜头脱落、未打孔、铜头焊点处有残锡、灯头无顶锡、无铆钉</b>~^<b>(105)产品耐压测不通过（每批抽6只 AC=0）</b>~^<b>(106)包装内产品混淆、各种包装印刷型号与产品型号不匹配</b>~^<b>(107)摔箱试验不合格（第一次生产做）</b>~^<b>(108)缺灯</b>~^<b>(109)BOM版本核对（model/PCB Versions/LED versions/丝印/包装 marking（含警示语））</b>~^<b>(110)其它（防水产品防水试验每批抽20只 AC=0）不通过</b>~^<b>PPM1</b>~^<b>(201)灯不亮、灯闪烁,部分发光管不亮(低压测试)，发光颜色有色差</b>~^<b>(202)透镜破裂、严重划痕或透镜脱落不同厂家透镜第一次生产时，抽取6只样品，在暗室内，额定电压下点亮，将光线</b>~^<b>(203)灯罩破裂、  胶未干溢出</b>~^<b>(204)铜头严重变形，开裂,不能通过相应通规</b>~^<b>(205)灯头焊片松动、偏心线漏焊、中心铆钉松动</b>~^<b>(206)灯壳开裂、壳内有异物 (摇晃时有响声，金属物）</b>~^<b>(207)壳体印刷关键标识不清 (型号、电压、功率、UL、CE标识及客户LOG标识等) ，或印刷错</b>~^<b>(208)包装印刷错误，展柜产品无坍塌现象</b>~^<b>(209)灯罩或吸塑包装内有虫子</b>~^<b>(210)调光和交流声检测（按封样）</b>~^<b>(211)功率、功率因数测试异常</b>~^<b>(212)性能测试不通过（每批抽6只，AC=0)</b>~^<b>(213)光电性能及寿命测试检查不通过（每批抽2只，AC=0)</b>~^<b>(214)适用工作电压/工作温度试验不通过（新产品第一次做）</b>~^<b>(215)外形尺寸不符合技术要求（每批抽6只，AC=0)</b>~^<b>(216)其它严重缺陷</b>~^<b>PPM</b>~^<b>(301)点亮时灯有明显瑕疵（极限样品）或如有色差、斑点等</b>~^<b>(302)塑件、散热器、透镜、灯罩严重变形、脱胶或塑件有色差、斑点等</b>~^<b>(303)灯头未拧紧、有间隙（＞0.5mm) 、塑件与散热器及灯罩之间有间隙（＞1.0mm)</b>~^<b>(304)订单号与出运单上的不一致</b>~^<b>(305)包装盒/箱/标贴的条形码不完整、扫描错误，包装灯体歪斜</b>~^<b>(306)焊锡不良、有毛刺、铜头有缺损、胶体开裂，摇动灯内有响声</b>~^<b>(307)散热器塑件、灯罩掉漆、斑点、划痕、脱粉等有1处、面积大于2mm或小于2mm的掉漆斑点、划痕、脱粉2处</b>~^<b>(308)彩盒窗口PET膜粘结不牢固、脱胶开裂等,展柜产品无脱胶，变形</b>~^<b>(309)其它主要缺陷</b>~^<b>PPM2</b>~^<b>(401)散热器、灯罩、透镜歪，塑件、散热器、灯罩配合错位</b>~^<b>(402)散热器可视面有轻微色斑、污点、气孔、划痕、缺损、喷漆不均等</b>~^<b>(403)灯壳漏打粘合剂、整灯有明显脏污</b>~^<b>(404)壳体印刷一般标识不清</b>~^<b>(405)塑封开裂，明显毛刺与划痕</b>~^<b>(406)纸盒包装产品需封盒完好且纸盒不可有明显毛边和印刷不清</b>~^<b>(407)散热器塑件、灯罩掉漆、脱粉、斑点、划痕等有1处、面积大于1mm或小于1mm的掉漆、斑点、划痕、脱粉有</b>~^<b>(408)吸塑包装内有异物</b>~^<b>(409)包装方式不正确；灯头衬、保护瓦楞层/衬套、夹板等漏放 ，包装材料破损</b>~^<b>(410)包装外箱印刷模糊不清或遗漏;彩色包装材料有明显色差；彩印脱色</b>~^<b>(411)外、中箱上有箱贴遗漏、错贴、位置不对或脱胶、产品周期章不符</b>~^<b>(412)外箱潮湿、变形、损坏，托盘底座损坏</b>~^<b>(413)其它次要缺陷</b>~^<b>PPM3</b>~^";
        string title = "<b>检验日期</b>~^<b>总批次</b>~^<b>合格批次</b>~^<b>不合格批次</b>~^<b>一次抽检批次接收率</b>~^<b>投入数</b>~^<b>抽样总数</b>~^<b>缺陷数</b>~^<b>一次抽检合格率</b>~^<b>(101)导丝外露（灯管与塑盖、塑壳与塑盖之间）</b>~^<b>(102)灯壳、灯罩脱落</b>~^<b>(103)灯头、灯罩扭力不符合要求</b>~^<b>(104)铜头脱落、未打孔</b>~^<b>(105)缺料</b>~^<b>(106)包装内产品混淆</b>~^<b>(107)各种包装印刷型号与产品型号不匹配</b>~^<b>(108)BOM版本核对（model/PCB Versions/丝印/包装 marking（含警示语））</b>~^<b>(109)其它致命缺陷</b>~^<b>致命缺陷PPM</b>~^<b>(201)灯不亮</b>~^<b>(202)灯管不启辉、闪烁</b>~^<b>(203)灯几何尺寸超出范围</b>~^<b>(204)灯罩松动/破碎</b>~^<b>(205)灯头严重变形，不能超过相应通规</b>~^<b>(206)灯头无顶锡、灯头焊片松动</b>~^<b>(207)灯壳开裂、壳内有异物（金属类）</b>~^<b>(208)壳体印刷关键标识不清（电压、功率、UL标识等）</b>~^<b>(209)包装印刷、标贴不符合灯标电压、功率等</b>~^<b>(210)灯罩或吸塑包装内有虫子</b>~^<b>(211)导丝外露</b>~^<b>(212)调光和交流声检测</b>~^<b>(213)电流测试</b>~^<b>(214)灯管应裂/漏气</b>~^<b>(215)灯罩应裂</b>~^<b>(216)其它严重缺陷</b>~^<b>(217)本身重量的4倍1小时压力合格</b>~^<b>严重缺陷PPM</b>~^<b>(301)点亮时灯管有明显瑕疵（极限样品）</b>~^<b>(302)灯管严重变形（极限样品）</b>~^<b>(303)灯头未拧紧、有间隙（>0.5mm）</b>~^<b>(304)(304)订单号与出运单上的不一致</b>~^<b>(305)包装盒/箱/标贴的条形码不完整、扫描错误</b>~^<b>(306)焊锡不良</b>~^<b>(307)灯管松动、脱胶</b>~^<b>(308)其它整灯主要缺陷</b>~^<b>主要缺陷PPM</b>~^<b>(401)灯管歪（极限样品）</b>~^<b>(402)灯管可视面有轻微脱粉、色斑、污点、拉丝等（极限样品）</b>~^<b>(403)(403)灯壳漏打粘结剂、整灯有明显脏污</b>~^<b>(404)灯胶泥不可有明显超高（≤2mm）、缺胶</b>~^<b>(405)塑封开裂，明显毛刺与划痕</b>~^<b>(406)纸盒包装产品需封盒完好且纸盒不可有明显毛边和印刷不清</b>~^<b>(407)吊卡打孔明显不正</b>~^<b>(408)吸塑包装内有异物</b>~^<b>(409)包装方式不正确，灯头衬、保护瓦楞层/衬套、夹板等漏放</b>~^<b>(410)包装外箱印刷模糊不清或遗漏；彩盒包装材料有明显色差</b>~^<b>(411)外箱上有箱贴遗漏、错贴、位置不对或脱胶</b>~^<b>(412)外箱潮湿、变形、损坏、托盘地座损坏</b>~^<b>(413)其它整灯次要缺陷</b>~^<b>次要缺陷PPM</b>~^";

        DataTable DT = qc.SelectCFLLampSamplingIncoming(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "CFL成品", true, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("导出数据为空!");
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
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate32.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate32.focus();";
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
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate32.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtEndDate32.focus();";
            return;
        }
        int uID = int.Parse(Session["uID"].ToString());
        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        //string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>检验日期</b>~^<b>总批次</b>~^<b>合格批次</b>~^<b>不合格批次</b>~^<b>一次抽检批次接收率</b>~^<b>投入数</b>~^<b>抽样总数</b>~^<b>缺陷数</b>~^<b>一次抽检合格率</b>~^<b>(101)导丝外露（指塑壳与灯头之间）</b>~^<b>(102)灯壳、灯罩、散热器、透镜等脱落</b>~^<b>(103)灯头、灯罩扭力（机械强度）不符合要求（每批抽6只 AC=0）</b>~^<b>(104)铜头脱落、未打孔、铜头焊点处有残锡、灯头无顶锡、无铆钉</b>~^<b>(105)产品耐压测不通过（每批抽6只 AC=0）</b>~^<b>(106)包装内产品混淆、各种包装印刷型号与产品型号不匹配</b>~^<b>(107)摔箱试验不合格（第一次生产做）</b>~^<b>(108)缺灯</b>~^<b>(109)BOM版本核对（model/PCB Versions/LED versions/丝印/包装 marking（含警示语））</b>~^<b>(110)其它（防水产品防水试验每批抽20只 AC=0）不通过</b>~^<b>PPM1</b>~^<b>(201)灯不亮、灯闪烁,部分发光管不亮(低压测试)，发光颜色有色差</b>~^<b>(202)透镜破裂、严重划痕或透镜脱落不同厂家透镜第一次生产时，抽取6只样品，在暗室内，额定电压下点亮，将光线</b>~^<b>(203)灯罩破裂、  胶未干溢出</b>~^<b>(204)铜头严重变形，开裂,不能通过相应通规</b>~^<b>(205)灯头焊片松动、偏心线漏焊、中心铆钉松动</b>~^<b>(206)灯壳开裂、壳内有异物 (摇晃时有响声，金属物）</b>~^<b>(207)壳体印刷关键标识不清 (型号、电压、功率、UL、CE标识及客户LOG标识等) ，或印刷错</b>~^<b>(208)包装印刷错误，展柜产品无坍塌现象</b>~^<b>(209)灯罩或吸塑包装内有虫子</b>~^<b>(210)调光和交流声检测（按封样）</b>~^<b>(211)功率、功率因数测试异常</b>~^<b>(212)性能测试不通过（每批抽6只，AC=0)</b>~^<b>(213)光电性能及寿命测试检查不通过（每批抽2只，AC=0)</b>~^<b>(214)适用工作电压/工作温度试验不通过（新产品第一次做）</b>~^<b>(215)外形尺寸不符合技术要求（每批抽6只，AC=0)</b>~^<b>(216)其它严重缺陷</b>~^<b>PPM</b>~^<b>(301)点亮时灯有明显瑕疵（极限样品）或如有色差、斑点等</b>~^<b>(302)塑件、散热器、透镜、灯罩严重变形、脱胶或塑件有色差、斑点等</b>~^<b>(303)灯头未拧紧、有间隙（＞0.5mm) 、塑件与散热器及灯罩之间有间隙（＞1.0mm)</b>~^<b>(304)订单号与出运单上的不一致</b>~^<b>(305)包装盒/箱/标贴的条形码不完整、扫描错误，包装灯体歪斜</b>~^<b>(306)焊锡不良、有毛刺、铜头有缺损、胶体开裂，摇动灯内有响声</b>~^<b>(307)散热器塑件、灯罩掉漆、斑点、划痕、脱粉等有1处、面积大于2mm或小于2mm的掉漆斑点、划痕、脱粉2处</b>~^<b>(308)彩盒窗口PET膜粘结不牢固、脱胶开裂等,展柜产品无脱胶，变形</b>~^<b>(309)其它主要缺陷</b>~^<b>PPM2</b>~^<b>(401)散热器、灯罩、透镜歪，塑件、散热器、灯罩配合错位</b>~^<b>(402)散热器可视面有轻微色斑、污点、气孔、划痕、缺损、喷漆不均等</b>~^<b>(403)灯壳漏打粘合剂、整灯有明显脏污</b>~^<b>(404)壳体印刷一般标识不清</b>~^<b>(405)塑封开裂，明显毛刺与划痕</b>~^<b>(406)纸盒包装产品需封盒完好且纸盒不可有明显毛边和印刷不清</b>~^<b>(407)散热器塑件、灯罩掉漆、脱粉、斑点、划痕等有1处、面积大于1mm或小于1mm的掉漆、斑点、划痕、脱粉有</b>~^<b>(408)吸塑包装内有异物</b>~^<b>(409)包装方式不正确；灯头衬、保护瓦楞层/衬套、夹板等漏放 ，包装材料破损</b>~^<b>(410)包装外箱印刷模糊不清或遗漏;彩色包装材料有明显色差；彩印脱色</b>~^<b>(411)外、中箱上有箱贴遗漏、错贴、位置不对或脱胶、产品周期章不符</b>~^<b>(412)外箱潮湿、变形、损坏，托盘底座损坏</b>~^<b>(413)其它次要缺陷</b>~^<b>PPM3</b>~^";

        DataTable DT = qc.SelectLampSamplingIncoming(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "LED成品", true, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
                            
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("导出数据为空!");
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
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate23.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate23.focus();";
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
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate23.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtEndDate23.focus();";
            return;
        }


        int uID = int.Parse(Session["uID"].ToString());

        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        string strImport = "qc_report_daily_DSD_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>检验日期</b>~^<b>总批次</b>~^<b>合格批次</b>~^<b>不合格批次</b>~^<b>一次抽检批次接收率</b>~^<b>投入数</b>~^<b>抽样总数</b>~^<b>缺陷数</b>~^<b>一次抽检合格率</b>~^"
                        + "<b>(101)导丝外露，灯壳、灯罩，灯头等脱落</b>~^<b>(102)灯头扭力测试 (每批抽6只）</b>~^<b>(103)耐压测试 (每批抽6只）</b>~^<b>(104)包装内产品混淆、各种包装印刷型号与产品型号不匹配</b>~^"
                        +"<b>(105)摔箱试验（第一次生产产品做1箱验证）</b>~^<b>(106)其它致命缺陷</b>~^<b>致命缺陷|致命缺陷PPM值</b>~^<b>(201)产品不亮</b>~^<b>(202)产品脱落，裂痕</b>~^<b>(203)光性能测试不通过（每批抽2只，AC=0)</b>~^"
                        +"<b>(204)外形尺寸及重量 （每批抽6只）</b>~^<b>(205)电性能测试不通过（每批抽6只，AC=0)</b>~^<b>(206)萤火虫开关测试（每批抽6只，AC=0)</b>~^<b>(207)灯头严重变形，开裂,不能通过相应灯头规（每批抽6只，AC=0)</b>~^"
                        +"<b>(208)其它严重缺陷</b>~^<b>严重缺陷PPM值</b>~^<b>(301)产品松动，开裂</b>~^<b>(302)产品擦伤，可视脏污，色斑</b>~^<b>(303)产品玻璃上有异物</b>~^<b>(304)产品无明显变形，平滑度符合</b>~^<b>(305)壳体印刷关键标识不清或印刷错</b>~^"
                        +"<b>(306)包装,说明书印刷错误</b>~^<b>(307)灯罩歪</b>~^<b>(308)其它主要缺陷</b>~^<b>主要缺陷PPM值</b>~^<b>(401)产品无影响的破损</b>~^<b>(402)产品外观上有色差，灯脏，气泡</b>~^<b>(403)壳体印刷一般标识不清</b>~^<b>(404)包装外箱标贴无剥离，错位，卷缩</b>~^"
                        +"<b>(405)包装盒/箱/标贴的条形码不完整、扫描错误</b>~^<b>(406)外箱，内箱破损，脏污</b>~^<b>(407)外箱无批号等出货信息</b>~^<b>(408)其它次要缺陷</b>~^<b>次要缺陷PPM值</b>~^";
        DataTable DT = qc.SelectDSD(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "灯丝灯", false, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("导出数据为空!");
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
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate33.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate33.focus();";
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
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate33.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtEndDate33.focus();";
            return;
        }
        int uID = int.Parse(Session["uID"].ToString());
        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        //string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        string strImport = "qc_report_daily_DSD_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>检验日期</b>~^<b>总批次</b>~^<b>合格批次</b>~^<b>不合格批次</b>~^<b>一次抽检批次接收率</b>~^<b>投入数</b>~^<b>抽样总数</b>~^<b>缺陷数</b>~^<b>一次抽检合格率</b>~^"
                        + "<b>(101)导丝外露，灯壳、灯罩，灯头等脱落</b>~^<b>(102)灯头扭力测试 (每批抽6只）</b>~^<b>(103)耐压测试 (每批抽6只）</b>~^<b>(104)包装内产品混淆、各种包装印刷型号与产品型号不匹配</b>~^"
                        + "<b>(105)摔箱试验（第一次生产产品做1箱验证）</b>~^<b>(106)其它致命缺陷</b>~^<b>致命缺陷|致命缺陷PPM值</b>~^<b>(201)产品不亮</b>~^<b>(202)产品脱落，裂痕</b>~^<b>(203)光性能测试不通过（每批抽2只，AC=0)</b>~^"
                        + "<b>(204)外形尺寸及重量 （每批抽6只）</b>~^<b>(205)电性能测试不通过（每批抽6只，AC=0)</b>~^<b>(206)萤火虫开关测试（每批抽6只，AC=0)</b>~^<b>(207)灯头严重变形，开裂,不能通过相应灯头规（每批抽6只，AC=0)</b>~^"
                        + "<b>(208)其它严重缺陷</b>~^<b>严重缺陷PPM值</b>~^<b>(301)产品松动，开裂</b>~^<b>(302)产品擦伤，可视脏污，色斑</b>~^<b>(303)产品玻璃上有异物</b>~^<b>(304)产品无明显变形，平滑度符合</b>~^<b>(305)壳体印刷关键标识不清或印刷错</b>~^"
                        + "<b>(306)包装,说明书印刷错误</b>~^<b>(307)灯罩歪</b>~^<b>(308)其它主要缺陷</b>~^<b>主要缺陷PPM值</b>~^<b>(401)产品无影响的破损</b>~^<b>(402)产品外观上有色差，灯脏，气泡</b>~^<b>(403)壳体印刷一般标识不清</b>~^<b>(404)包装外箱标贴无剥离，错位，卷缩</b>~^"
                        + "<b>(405)包装盒/箱/标贴的条形码不完整、扫描错误</b>~^<b>(406)外箱，内箱破损，脏污</b>~^<b>(407)外箱无批号等出货信息</b>~^<b>(408)其它次要缺陷</b>~^<b>次要缺陷PPM值</b>~^";

        DataTable DT = qc.SelectDSD(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "灯丝灯", true, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("导出数据为空!");
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
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate24.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate24.focus();";
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
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate24.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtEndDate24.focus();";
            return;
        }
        int uID = int.Parse(Session["uID"].ToString());
        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        //string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        string strImport = "qc_report_daily_T8_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>检验日期</b>~^<b>总批次</b>~^<b>合格批次</b>~^<b>不合格批次</b>~^<b>一次抽检批次接收率</b>~^<b>投入数</b>~^<b>抽样数</b>~^<b>总缺陷数</b>~^<b>合格率</b>~^"
            + "<b>关键缺陷|(101)包装内有虫子、刀片、针等利器</b>~^<b>关键缺陷|(102)灯壳、灯管松脱</b>~^<b>关键缺陷|(103)包装缺灯</b>~^<b>关键缺陷|(104)灯头扭力测试6支AC=0</b>~^<b>关键缺陷|(105)灯头脱落</b>~^<b>关键缺陷|(106)耐压测试（有耐压要求的产品）6支 AC=0</b>~^<b>关键缺陷|(107)包装混淆、各种包装印刷型号与产品型号不匹配</b>~^<b>关键缺陷|关键缺陷PPM值</b>~^<b>重要缺陷|(201)外形尺寸6支 AC=0</b>~^<b>重要缺陷|(202)灯壳漏打粘合剂、灯条浮起</b>~^<b>重要缺陷|(203)灯不亮及局部不亮、灯闪烁、发光颜色有色差</b>~^<b>重要缺陷|(204)灯条与灯头（塑件）标识方向不一致，安放方向反</b>~^<b>重要缺陷|(205)灯管开裂、破损</b>~^"
            + "<b>重要缺陷|(206)壳内有异物（金属物）摇晃时有响声</b>~^<b>重要缺陷|(207)胶未干溢出</b>~^<b>重要缺陷|(208)壳体印刷关键标识不清（不可识别）（型号、电压、功率、UL、及客户LOGO标识等）或印刷错</b>~^<b>重要缺陷|(209)包装印刷错误；条形码扫描错误</b>~^<b>重要缺陷|(210)光电参数6支，AC=1</b>~^"
            + "<b>重要缺陷|(211)针脚变形</b>~^<b>重要缺陷|(212)两端针脚不在同一平面不能通过相互平行的检验夹具（或量规）</b>~^<b>重要缺陷|(213)未冲针，或未冲到位造成接触不良</b>~^<b>重要缺陷|(214)灯头（灯帽）安装歪斜，无法通过量规</b>~^<b>重要缺陷|重要缺陷PPM值</b>~^"
            + "<b>主要缺陷|(301)LED灯条胶不均匀，中间有断胶，展开宽度低于工艺规定范围</b>~^<b>主要缺陷|(302)LED灯条不在针脚面90°定位，偏离工艺规定范围</b>~^<b>主要缺陷|(303)涂粉不均（能清晰看到内部材料），局部漏涂粉。灯脚护套遗漏，标贴漏贴</b>~^"
            + "<b>主要缺陷|(304)灯头塑件严重变形；有缺损、裂纹，灯壳未露金属的开裂</b>~^<b>主要缺陷|(305)壳内有异物（非金属物），摇动灯内有响声</b>~^<b>主要缺陷|(306)订单号与出运单上的不一致</b>~^<b>主要缺陷|(307)包装盒/箱/标贴的条形码不完整、扫描不出，包装灯体歪斜</b>~^"
            + "<b>主要缺陷|主要缺陷PPM值</b>~^<b>轻缺陷|(401)灯管A面有≥Ф2.0mm的色斑、斑点、结石、黑点等，或10CM内Ф0.5-Ф2.0mm允许2个，＜Ф0.5mm不计.灯管A面距离堵头端部15mm内区域的粉点不作要求；B面10CM内Ф1.0-Ф2.0</b>~^<b>轻缺陷|(402)A面粉层≥Ф2.0mm粉点、掉粉、厚度不均；或10CM内Ф0.5-Ф2.0mm允许2个，＜Ф0.5mm不计.B面10CM内Ф1.0-Ф2.0mm允许3个，＜Ф1.0mm不计</b>~^"
            + "<b>轻缺陷|(403)灯头A面黑点、白点允许3个Ф0.5-1mm之间，距离不小于300mm，Ф1.0-1.5mm点允许有1个，＜Ф0.5的点不计；B面白点不计，不大于Ф1.5mm黑点不计，大于Ф1.5mm黑点不多于2点</b>~^<b>轻缺陷|(404)A面塑件、灯管擦伤划痕≥30CM；或擦伤划痕＜30CM超过2条。B面允许3条，长度不计</b>~^<b>轻缺陷|(405)A面灯管气线长度≥30CM，或＜30CM超过2条，B面不计</b>~^"
            + "<b>轻缺陷|(406)灯体有≥Ф2.0mm脏污、胶，或Ф0.5-Ф0.2mm超过3个，＜Ф0.5mm不计</b>~^<b>轻缺陷|(407)灯条安放松动、浮起</b>~^<b>轻缺陷|(408)壳体印刷一般标识不清（不可识别）；灯体标贴错位、皱褶</b>~^<b>轻缺陷|(409)纸盒端口凹陷、开裂</b>~^"
            + "<b>轻缺陷|(410)包装方式不正确；包装材料破损</b>~^<b>轻缺陷|(411)印刷模糊不清（不可识别）或遗漏文字</b>~^<b>轻缺陷|(412)彩色包装材料有明显色差、彩印脱色</b>~^<b>轻缺陷|(413)外箱上有箱贴遗漏、错贴、位置不对或脱胶、产品周期章不符</b>~^<b>轻缺陷|(414)包装箱潮湿、变形、损坏等</b>~^<b>轻缺陷|轻缺陷PPM值</b>~^";

        DataTable DT = qc.SelectT8(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "LED T8直管灯", false, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        //DataTable DT = qc.SelectDSD(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "灯丝灯", false, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("导出数据为空!");
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
                ltlAlert.Text = "alert('起始日期格式不正确!');Form1.txtStdDate34.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择起始日期!');Form1.txtStdDate34.focus();";
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
                ltlAlert.Text = "alert('结束日期格式不正确!');Form1.txtEndDate34.focus();";
                return;
            }
        }
        else
        {
            ltlAlert.Text = "alert('请选择结束日期!');Form1.txtEndDate34.focus();";
            return;
        }
        int uID = int.Parse(Session["uID"].ToString());
        string strFloder = Server.MapPath("/docs/qc_report_daily_incoming.xls");
        //string strImport = "qc_report_daily_incoming_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";

        string strImport = "qc_report_daily_T8_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string title = "<b>检验日期</b>~^<b>总批次</b>~^<b>合格批次</b>~^<b>不合格批次</b>~^<b>一次抽检批次接收率</b>~^<b>投入数</b>~^<b>抽样数</b>~^<b>总缺陷数</b>~^<b>合格率</b>~^"
             + "<b>关键缺陷|(101)包装内有虫子、刀片、针等利器</b>~^<b>关键缺陷|(102)灯壳、灯管松脱</b>~^<b>关键缺陷|(103)包装缺灯</b>~^<b>关键缺陷|(104)灯头扭力测试6支AC=0</b>~^<b>关键缺陷|(105)灯头脱落</b>~^<b>关键缺陷|(106)耐压测试（有耐压要求的产品）6支 AC=0</b>~^<b>关键缺陷|(107)包装混淆、各种包装印刷型号与产品型号不匹配</b>~^<b>关键缺陷|关键缺陷PPM值</b>~^<b>重要缺陷|(201)外形尺寸6支 AC=0</b>~^<b>重要缺陷|(202)灯壳漏打粘合剂、灯条浮起</b>~^<b>重要缺陷|(203)灯不亮及局部不亮、灯闪烁、发光颜色有色差</b>~^<b>重要缺陷|(204)灯条与灯头（塑件）标识方向不一致，安放方向反</b>~^<b>重要缺陷|(205)灯管开裂、破损</b>~^"
             + "<b>重要缺陷|(206)壳内有异物（金属物）摇晃时有响声</b>~^<b>重要缺陷|(207)胶未干溢出</b>~^<b>重要缺陷|(208)壳体印刷关键标识不清（不可识别）（型号、电压、功率、UL、及客户LOGO标识等）或印刷错</b>~^<b>重要缺陷|(209)包装印刷错误；条形码扫描错误</b>~^<b>重要缺陷|(210)光电参数6支，AC=1</b>~^"
             + "<b>重要缺陷|(211)针脚变形</b>~^<b>重要缺陷|(212)两端针脚不在同一平面不能通过相互平行的检验夹具（或量规）</b>~^<b>重要缺陷|(213)未冲针，或未冲到位造成接触不良</b>~^<b>重要缺陷|(214)灯头（灯帽）安装歪斜，无法通过量规</b>~^<b>重要缺陷|重要缺陷PPM值</b>~^"
             + "<b>主要缺陷|(301)LED灯条胶不均匀，中间有断胶，展开宽度低于工艺规定范围</b>~^<b>主要缺陷|(302)LED灯条不在针脚面90°定位，偏离工艺规定范围</b>~^<b>主要缺陷|(303)涂粉不均（能清晰看到内部材料），局部漏涂粉。灯脚护套遗漏，标贴漏贴</b>~^"
             + "<b>主要缺陷|(304)灯头塑件严重变形；有缺损、裂纹，灯壳未露金属的开裂</b>~^<b>主要缺陷|(305)壳内有异物（非金属物），摇动灯内有响声</b>~^<b>主要缺陷|(306)订单号与出运单上的不一致</b>~^<b>主要缺陷|(307)包装盒/箱/标贴的条形码不完整、扫描不出，包装灯体歪斜</b>~^"
             + "<b>主要缺陷|主要缺陷PPM值</b>~^<b>轻缺陷|(401)灯管A面有≥Ф2.0mm的色斑、斑点、结石、黑点等，或10CM内Ф0.5-Ф2.0mm允许2个，＜Ф0.5mm不计.灯管A面距离堵头端部15mm内区域的粉点不作要求；B面10CM内Ф1.0-Ф2.0</b>~^<b>轻缺陷|(402)A面粉层≥Ф2.0mm粉点、掉粉、厚度不均；或10CM内Ф0.5-Ф2.0mm允许2个，＜Ф0.5mm不计.B面10CM内Ф1.0-Ф2.0mm允许3个，＜Ф1.0mm不计</b>~^"
             + "<b>轻缺陷|(403)灯头A面黑点、白点允许3个Ф0.5-1mm之间，距离不小于300mm，Ф1.0-1.5mm点允许有1个，＜Ф0.5的点不计；B面白点不计，不大于Ф1.5mm黑点不计，大于Ф1.5mm黑点不多于2点</b>~^<b>轻缺陷|(404)A面塑件、灯管擦伤划痕≥30CM；或擦伤划痕＜30CM超过2条。B面允许3条，长度不计</b>~^<b>轻缺陷|(405)A面灯管气线长度≥30CM，或＜30CM超过2条，B面不计</b>~^"
             + "<b>轻缺陷|(406)灯体有≥Ф2.0mm脏污、胶，或Ф0.5-Ф0.2mm超过3个，＜Ф0.5mm不计</b>~^<b>轻缺陷|(407)灯条安放松动、浮起</b>~^<b>轻缺陷|(408)壳体印刷一般标识不清（不可识别）；灯体标贴错位、皱褶</b>~^<b>轻缺陷|(409)纸盒端口凹陷、开裂</b>~^"
             + "<b>轻缺陷|(410)包装方式不正确；包装材料破损</b>~^<b>轻缺陷|(411)印刷模糊不清（不可识别）或遗漏文字</b>~^<b>轻缺陷|(412)彩色包装材料有明显色差、彩印脱色</b>~^<b>轻缺陷|(413)外箱上有箱贴遗漏、错贴、位置不对或脱胶、产品周期章不符</b>~^<b>轻缺陷|(414)包装箱潮湿、变形、损坏等</b>~^<b>轻缺陷|轻缺陷PPM值</b>~^";

        DataTable DT = qc.SelectT8(Convert.ToInt32(Session["uID"]), txtStdDate2, txtEndDate2, "LED T8直管灯", true, Session["plantcode"].ToString()).Tables[0];///_qcexcel.SelectChouJianIncoming(13,"","").table[0];
        if (DT.Rows.Count > 0)
        {
            ExportExcel2007(title, DT, false);
        }
        else
        {
            this.Alert("导出数据为空!");
        }
    }
       
}
