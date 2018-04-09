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
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;

public partial class wsline_wl_woTrackReport2 : BasePage
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //��ʼ����ʾ����ǰ��
            txtWoCompDate1.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Today.AddDays(-2));
            txtWoCompDate2.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Today);

            BindPlants();
            BindData();
        }
    }

    protected void BindPlants()
    {
        string strSQL = "SELECT plantID,description From Plants where isAdmin=0 and plantID in(1, 2, 5, 8) order by plantID";
        dropPlants.Items.Clear();

        try
        {
            SqlDataReader reader = SqlHelper.ExecuteReader(chk.dsn0(), CommandType.Text, strSQL);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dropPlants.Items.Add(new ListItem(reader["description"].ToString(), reader["plantID"].ToString()));
                }
                reader.Close();
            }
        }
        catch
        { }
    }

    protected DataSet GetData()
    {
        try
        {
            string strPalntCode = dropPlants.SelectedIndex == -1 ? Session["plantCode"].ToString() : dropPlants.SelectedValue;

            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@nbr", txbNbr.Text.Trim());
            param[1] = new SqlParameter("@pcbBeginDate", pcbBeginDate.Text.Trim());
            param[2] = new SqlParameter("@pcbEndDate", pcbEndDate.Text.Trim());
            param[3] = new SqlParameter("@mgBeginDate", mgBeginDate.Text.Trim());
            param[4] = new SqlParameter("@mgEndDate", mgEndDate.Text.Trim());
            param[5] = new SqlParameter("@onlineBeginDate", onlineBeginDate.Text.Trim());
            param[6] = new SqlParameter("@onlineEndDate", onlineEndDate.Text.Trim());
            param[7] = new SqlParameter("@offlineBeginDate", offlineBeginDate.Text.Trim());
            param[8] = new SqlParameter("@offlineEndDate", offlineEndDate.Text.Trim());
            param[9] = new SqlParameter("@plantCode", strPalntCode);
            param[10] = new SqlParameter("@wo_due_date1", txtWoDueDate1.Text.Trim());
            param[11] = new SqlParameter("@wo_due_date2", txtWoDueDate2.Text.Trim());
            param[12] = new SqlParameter("@wo_comp_date1", txtWoCompDate1.Text.Trim());
            param[13] = new SqlParameter("@wo_comp_date2", txtWoCompDate2.Text.Trim());

            return SqlHelper.ExecuteDataset(chk.dsn0(), CommandType.StoredProcedure, "sp_wo2_selectWoTrackReport", param);
        }
        catch
        {
            return null;
        }
    }

    protected void BindData()
    {
        DataSet ds = this.GetData();

        gvWoTrackReports.DataSource = ds.Tables[0];
        gvWoTrackReports.DataBind();
        gvWoTrackReports.PageIndex = 0;

        if (ds != null)
        {
            ds.Dispose();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvWoTrackReports_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void gvWoTrackReports_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvWoTrackReports.PageIndex = e.NewPageIndex;
        BindData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataSet ds = this.GetData();

        if (ds.Tables[0].Rows.Count == 0)
        {
            ltlAlert.Text = "alert('û��Ҫ���������ݣ�')";
        }
        else
        {
            string ExTitle = "<b>��</b>~^<b>�ص�</b>~^<b>������</b>~^<b>����ID</b>~^<b>�´�����</b>~^<b>����״̬</b>~^��������</b>~^<b>������</b>~^<b>�깤��</b>~^<b>�����</b>~^<b>�������</b>~^<b>������</b>~^<b>��·�巢��</b>~^<b>��·�巢�ϻ㱨��</b>~^<b>ë�ܷ���</b>~^<b>ë�ܷ��ϻ㱨��</b>~^<b>����</b>~^<b>���߻㱨��</b>~^<b>�ʼ�����</b>~^<b>�ʼ�㱨��</b>~^<b>����</b>~^<b>���߻㱨��</b>~^";
            ExTitle += "<b>(1008A)���ƹ�׼��</b>~^<b>(1008B)���ƹ�׼��</b>~^<b>(1008C)���ƹ�׼��</b>~^<b>(1008D)���ƹ�׼��</b>~^<b>(1010A)���ƹ�</b>~^<b>(1010B)���ƹ�</b>~^<b>(1010C)���ƹ�</b>~^<b>(1010D)���ƹ�</b>~^<b>(1020A)��ͷ׼��</b>~^<b>(1020B)��ͷ׼��</b>~^<b>(1020C)��ͷ׼��</b>~^<b>(1020D)��ͷ׼��</b>~^<b>(1030A)��װ</b>~^<b>(1030B)��װ</b>~^<b>(1030C)��װ</b>~^<b>(1030D)��װ</b>~^<b>(1040A)����</b>~^<b>(1040B)����</b>~^<b>(1040C)����</b>~^<b>(1040D)����</b>~^<b>(1090A)��װ</b>~^<b>(1090B)��װ</b>~^<b>(1090C)��װ</b>~^<b>(1090D)��װ</b>~^<b>(2010A)������</b>~^<b>(2010B)������</b>~^<b>(2010C)������</b>~^<b>(2010D)������</b>~^<b>(2020A)ϴ����</b>~^<b>(2020B)ϴ����</b>~^<b>(2020C)ϴ����</b>~^<b>(2020D)ϴ����</b>~^<b>(2030A)Ϳ��</b>~^<b>(2030B)Ϳ��</b>~^<b>(2030C)Ϳ��</b>~^<b>(2030D)Ϳ��</b>~^<b>(2040A)��˿</b>~^<b>(2040B)��˿</b>~^<b>(2040C)��˿</b>~^<b>(2040D)��˿</b>~^<b>(2045A)�ֹ����</b>~^<b>(2045B)�ֹ����</b>~^<b>(2045C)�ֹ����</b>~^<b>(2045D)�ֹ����</b>~^<b>(2050A)�������</b>~^<b>(2050B)�������</b>~^<b>(2050C)�������</b>~^<b>(2050D)�������</b>~^<b>(2060A)�ƹ��ܼ�</b>~^<b>(2060B)�ƹ��ܼ�</b>~^<b>(2060C)�ƹ��ܼ�</b>~^<b>(2060D)�ƹ��ܼ�</b>~^<b>(3010A)ֱ������</b>~^<b>(3010B)ֱ������</b>~^<b>(3010C)ֱ������</b>~^<b>(3010D)ֱ������</b>~^<b>(3090A)ֱ�ܰ�װ</b>~^<b>(3090B)ֱ�ܰ�װ</b>~^<b>(3090C)ֱ�ܰ�װ</b>~^<b>(3090D)ֱ�ܰ�װ</b>~^<b>(4005A)���</b>~^<b>(4005B)���</b>~^<b>(4005C)���</b>~^<b>(4005D)���</b>~^<b>(4010A)���</b>~^<b>(4010B)���</b>~^<b>(4010C)���</b>~^<b>(4010D)���</b>~^<b>(4020A)���</b>~^<b>(4020B)���</b>~^<b>(4020C)���</b>~^<b>(4020D)���</b>~^<b>(4030A)���</b>~^<b>(4030B)���</b>~^<b>(4030C)���</b>~^<b>(4030D)���</b>~^<b>(4040A)�˻�</b>~^<b>(4040B)�˻�</b>~^<b>(4040C)�˻�</b>~^<b>(4040D)�˻�</b>~^<b>(4050A)���</b>~^<b>(4050B)���</b>~^<b>(4050C)���</b>~^<b>(4050D)���</b>~^<b>(4060A)����</b>~^<b>(4060B)����</b>~^<b>(4060C)����</b>~^<b>(4060D)����</b>~^<b>(4080A)����</b>~^<b>(4080B)����</b>~^<b>(4080C)����</b>~^<b>(4080D)����</b>~^<b>(5020A)���</b>~^<b>(5020B)���</b>~^<b>(5020C)���</b>~^<b>(5020D)���</b>~^<b>(5030A)����</b>~^<b>(5030B)����</b>~^<b>(5030C)����</b>~^<b>(5030D)����</b>~^<b>(5040A)�ܼ�</b>~^<b>(5040B)�ܼ�</b>~^<b>(5040C)�ܼ�</b>~^<b>(5040D)�ܼ�</b>~^<b>(6020A)����</b>~^<b>(6020B)����</b>~^<b>(6020C)����</b>~^<b>(6020D)����</b>~^<b>(6030A)��Ƭ��SMT��</b>~^<b>(6030B)��Ƭ��SMT��</b>~^<b>(6030C)��Ƭ��SMT��</b>~^<b>(6030D)��Ƭ��SMT��</b>~^<b>(6040A)������/��í��</b>~^<b>(6040B)������/��í��</b>~^<b>(6040C)������/��í��</b>~^<b>(6040D)������/��í��</b>~^<b>(6050A)���壨AI��</b>~^<b>(6050B)���壨AI��</b>~^<b>(6050C)���壨AI��</b>~^<b>(6050D)���壨AI��</b>~^<b>(6060A)�ֲ��</b>~^<b>(6060B)�ֲ��</b>~^<b>(6060C)�ֲ��</b>~^<b>(6060D)�ֲ��</b>~^<b>(6070A)�ܼ�</b>~^<b>(6070B)�ܼ�</b>~^<b>(6070C)�ܼ�</b>~^<b>(6070D)�ܼ�</b>~^<b>(7010A)���¶�</b>~^<b>(7010B)���¶�</b>~^<b>(7010C)���¶�</b>~^<b>(7010D)���¶�</b>~^<b>(7020A)���ߺ�</b>~^<b>(7020B)���ߺ�</b>~^<b>(7020C)���ߺ�</b>~^<b>(7020D)���ߺ�</b>~^<b>(7025A)��ӡ</b>~^<b>(7025B)��ӡ</b>~^<b>(7025C)��ӡ</b>~^<b>(7025D)��ӡ</b>~^";
            this.ExportExcel(ExTitle, ds.Tables[0], false);
        }
    }
}
