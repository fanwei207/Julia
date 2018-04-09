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
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;


public partial class new_MPview : BasePage
{
    adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataReader sqReader;
        string str = "SELECT AttContent,atttype,attname  FROM MPAttachFile Where Attid=" + Convert.ToString(Request["attid"]);
        sqReader= SqlHelper.ExecuteReader(adam.dsn0(), CommandType.Text, str);

        sqReader.Read ();
        
        Response.ContentType = Convert.ToString(sqReader["atttype"]);

        Response.BinaryWrite(getImage(sqReader,0));
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(Convert.ToString(sqReader["attname"])));
        Response.Flush();
        Response.Close();
        sqReader.Close();
    }


    public byte[] getImage(SqlDataReader sqlrd, int id)
    {
        int buffersize = 1000;
        byte[] outbyte = new byte[1000];
        //��������getbytes�����ص��ֽ���Ŀ
        long retval;
        //�����ʼλ��
        long startIndex = 0;
        startIndex = 0;
        //���ֽڶ���outbyte��������������õ��ֽ���
        retval = sqlrd.GetBytes(id, startIndex, outbyte, 0, buffersize);
        while (retval == buffersize)
        {
            //����ʼλ���������ó���һ������ȡ�Ļ�������ĩ�ˣ������������ֽ�����byte������
            startIndex += buffersize;
            retval = sqlrd.GetBytes(id, startIndex, outbyte, 0, buffersize);

        }
        return outbyte;
 
    }
}
