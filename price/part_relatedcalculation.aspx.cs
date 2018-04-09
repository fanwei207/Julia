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
using System.Text;
using System.Text.RegularExpressions;
using QCProgress;
using adamFuncs;

public partial class part_relatedcalculation : BasePage
{
    adamClass adam = new adamClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected  void txtpt_part_TextChanged(object sender, EventArgs e)
    {
        string partNo = txtpt_part.Text.Trim();
        DataTable dt = PartInfo.getPartDesc(partNo);
        if (dt.Rows.Count == 0)
        {
            txtnumber.Text = string.Empty;
            txtpt_desc1.Text = string.Empty;
            txtpt_desc2.Text = string.Empty;
            txtuintpt_ship_wt.Text = string.Empty;
            txtunitpt_net_wt.Text = string.Empty;
            txtunitpt_size.Text = string.Empty;
            txtsumpt_size.Text = string.Empty;
            txtsumpt_ship_wt.Text = string.Empty;
            txtsumpt_net_wt.Text = string.Empty;

            ltlAlert.Text = "alert('此物料号不存在');";
           return;
       
        }
     
        txtpt_desc1.Text = dt.Rows[1]["pt_desc1"].ToString();
        txtpt_desc2.Text = dt.Rows[1]["pt_desc2"].ToString();

        float  float_pt_size = Convert.ToSingle(dt.Rows[1]["pt_size"].ToString());
        float  float_pt_ship_wt = Convert.ToSingle(dt.Rows[1]["pt_ship_wt"].ToString());
        float  float_pt_net_et = Convert.ToSingle(dt.Rows[1]["pt_net_wt"].ToString());

        txtunitpt_size.Text = Convert.ToString((float)1 / float_pt_size);
        float unit_pt_size = Convert.ToSingle(txtunitpt_size.Text);
        txtuintpt_ship_wt.Text = Convert.ToString(unit_pt_size * float_pt_ship_wt);
        txtunitpt_net_wt.Text = Convert.ToString(unit_pt_size * float_pt_net_et);

        txtnumber.Text = string.Empty;
        txtsumpt_size.Text = string.Empty;
        txtsumpt_ship_wt.Text = string.Empty;
        txtsumpt_net_wt.Text = string.Empty;

    }

    protected void btnunit_cal_Click(object sender, EventArgs e)
    {
        if (txtnumber.Text == string.Empty)
        {
            ltlAlert.Text = "alert('物料数量输入不能为空')";
            return;
        }

        float float_txtnumber = Convert.ToSingle(txtnumber.Text.ToString());
        txtsumpt_size.Text = Convert.ToString(float_txtnumber * Convert.ToSingle(txtunitpt_size.Text.ToString()));
        txtsumpt_ship_wt.Text = Convert.ToString(float_txtnumber * Convert.ToSingle(txtuintpt_ship_wt.Text.ToString()));
        txtsumpt_net_wt.Text = Convert.ToString(float_txtnumber * Convert.ToSingle(txtunitpt_net_wt.Text.ToString()));

    }


    protected void btnbatch_cal_Click(object sender, EventArgs e)
    {
        if (excelfilepath.Value.Trim() == string.Empty)
        {
            ltlAlert.Text = "alert('请先输入需批量计算的xls文件')";
            return;
        }

        FileOperate _fileoperate = new FileOperate(excelfilepath, Server.MapPath("/import"), 8388608);
        _fileoperate.SectionLimit = Section.XLS;

        string strMsg = "";
        if (!_fileoperate.SaveFileToServer(ref strMsg))
        {
            ltlAlert.Text = "alert('" + strMsg + "')";
            return;
        }
        else
        {

            _fileoperate.ImportExcel( int.Parse(Session["uID"].ToString()), ref strMsg);
            if (strMsg != "")
            {
                ltlAlert.Text = "alert('" + strMsg + "')";
                return;
            }
            ltlAlert.Text = "alert('导入数据成功!')";
        }

    }


    protected void btn_exportexcel_Click(object sender, EventArgs e)
    {
        if (Session["uID"].ToString() == string.Empty)
        {
            //ltlAlert.Text = "alert('您登录的时间过长或许其它原因，请您先登录')";
            Response.Redirect("login.aspx"); 
        }

        ltlAlert.Text = "window.open('part_calculation_exportexcel.aspx', '_blank');";
    }
}