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
        //用来持有getbytes所返回的字节数目
        long retval;
        //输出起始位置
        long startIndex = 0;
        startIndex = 0;
        //将字节读入outbyte【】并返回所获得的字节数
        retval = sqlrd.GetBytes(id, startIndex, outbyte, 0, buffersize);
        while (retval == buffersize)
        {
            //将起始位置重新设置成上一次所该取的缓存区的末端，并将继续将字节如若byte【】中
            startIndex += buffersize;
            retval = sqlrd.GetBytes(id, startIndex, outbyte, 0, buffersize);

        }
        return outbyte;
 
    }
}
