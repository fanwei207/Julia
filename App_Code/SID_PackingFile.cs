using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using CommClass;
using System.Configuration;

/// <summary>
/// Summary description for Document
/// </summary>
public class SID_PackingFile
{

    public SID_PackingFile()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    string strConn = ConfigurationManager.AppSettings["SqlConn.Conn"];
    public static DataSet GetDocument(string shipno)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@shipno", shipno);
            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_sid_selectDocument", param);
        }
        catch
        {
            return null;
        }
    }
    public static DataSet GetDocumentById(string id)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);

            return SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_adm_selectDocumentById", param);
        }
        catch
        {
            return null;
        }
    }
    public static bool InsertDocument(string shipno,string name,string doctype, string ext, string path, int uID, string uName)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@shipno", shipno);
            param[1] = new SqlParameter("@name", name);
            param[2] = new SqlParameter("@doctype", doctype);
            param[3] = new SqlParameter("@ext", ext);
            param[4] = new SqlParameter("@path", path);
            param[5] = new SqlParameter("@uID", uID);
            param[6] = new SqlParameter("@uName", uName);

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_SID_insert_Document", param);

            return true;
        }
        catch
        {
            return false;
        }
    }
    public static bool DeleteDocument(string id, int uID, string uName)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@id", id);
            param[1] = new SqlParameter("@uID", uID);
            param[2] = new SqlParameter("@uName", uName);
            param[3] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[3].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_sid_deleteDocument", param);
            return Convert.ToBoolean(param[3].Value);
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// 验证后缀是否正确
    /// </summary>
    /// <param name="ext"></param>
    public static bool CheckDocument(string ext)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ext", ext);
            param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
            param[1].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_adm_chekcDocument", param);

            return Convert.ToBoolean(param[1].Value);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 获得文件的类型
    /// </summary>
    /// <param name="ext"></param>
    /// <returns></returns>
    public static string  getDocumentType()
    {
        try
        {

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@type", "1");
            SqlDataReader reader = SqlHelper.ExecuteReader(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_adm_getDocumentType", param);

            string type="文件格式：只能是";
            while (reader.Read())
            {
                type += ""+reader["doc_name"].ToString() + ",";

            }
            type = type.Remove(type.LastIndexOf(","), 1);
            return type;
           
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 获得文件的类型表
    /// </summary>
    /// <returns></returns>
    public static DataTable getDocType(string ext,string name)
    {
        try
        {
            if (ext == "" || ext == string.Empty)
            {
                ext = "0";
            }

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ext", ext);
            param[1] = new SqlParameter("@name", name);
           
            DataTable dt = SqlHelper.ExecuteDataset(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_adm_getDocumentType", param).Tables[0];


            return dt;

        }
        catch
        {
            return null;
        }
    }
    public static SqlDataReader getDocType(string id)
    {
        try
        {
            

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@id", id);
            

            return SqlHelper.ExecuteReader(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_adm_getDocumentType", param);


            

        }
        catch
        {
            return null;
        }
    }
    /// <summary>
    /// 新增文件类型
    /// </summary>
    /// <param name="ext"></param>
    /// <returns></returns>
    public static int InsertDocumentType(string ext,string name,string id,string cname)
    {
        try
        {
            int intext;
            if (Int32.TryParse(ext, out intext))
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@ext", intext.ToString());
                param[1] = new SqlParameter("@name", name);
                param[2] = new SqlParameter("@createBy", id);
                param[3] = new SqlParameter("@createName", cname);
                param[4] = new SqlParameter("@retValue", SqlDbType.Int);
                param[4].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_adm_insertDocumentType", param);

                return Convert.ToInt32(param[4].Value.ToString().Trim());
            }
            else
            {
                return 3;
            }
        }
        catch
        {
            return 0;
        }
    }
    /// <summary>
    /// 修改文档格式
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ext"></param>
    /// <param name="name"></param>
    /// <param name="mid"></param>
    /// <param name="mname"></param>
    /// <returns></returns>
    public static int UpdateDocumentType(string id, string ext, string name, string mid, string mname)
    {
        try
        {
            int intext;
            if (Int32.TryParse(ext, out intext))
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@ext", intext.ToString());
                param[2] = new SqlParameter("@name", name);
                param[3] = new SqlParameter("@modifyBy", mid);
                param[4] = new SqlParameter("@modifyName", mname);
                param[5] = new SqlParameter("@retValue", SqlDbType.Int);
                param[5].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_adm_updateDocumentType", param);

                return Convert.ToInt32(param[5].Value.ToString().Trim());
            }
            else
            {
                return 3;
            }
        }
        catch
        {
            return 0;
        }
    }
    /// <summary>
    /// 删除文件类型
    /// </summary>
    /// <param name="ext"></param>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <param name="cname"></param>
    /// <returns></returns>
    public static bool deleteDocumentType(string id)
    {
        try
        {

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@id", id);
                param[1] = new SqlParameter("@retValue", SqlDbType.Bit);
                param[1].Direction = ParameterDirection.Output;
                SqlHelper.ExecuteNonQuery(admClass.getConnectString("SqlConn.Conn"), CommandType.StoredProcedure, "sp_adm_deleteDocumentType", param);

                return Convert.ToBoolean(param[1].Value);
           
        }
        catch
        {
            return false;
        }
    }
    /// <summary>
    /// 通过流的方式获取文件的真正后缀。防止只改变文件的后缀
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns>后缀</returns>
    public static string GetFileExtension(string filepath)
    {
        System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
        string fileclass = string.Empty;
        //这里的位长要具体判断.
        byte buffer;
        try
        {
            buffer = r.ReadByte();
            fileclass = buffer.ToString();
            buffer = r.ReadByte();
            fileclass += buffer.ToString();

        }
        catch
        {

        }

        r.Close();
        fs.Close();

        return fileclass;
    }
}
