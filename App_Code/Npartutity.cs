using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Web.Security;
using adamFuncs;
using CommClass;
using System.IO;

/// <summary>
/// Npartutity 的摘要说明
/// </summary>
public class Npartutity
{
    private adamClass adam = new adamClass();
    SqlParameter[] param = new SqlParameter[2];
   
	public Npartutity()
	{  
	}

    public int DelTmpFile(IList<string> delobj)
    {
        string delparam = "";
       
        if (delobj.Count == 0 || delobj == null)
            return -1;

        foreach(string i in delobj)
        {
            string Tsql = "select * from QadDoc..NpartDocuments where Guid='" + i + "'";
            try
            {
                DataTable ds = SqlHelper.ExecuteDataset(adam.dsnx(), CommandType.Text, Tsql).Tables[0];
                if (ds.Rows.Count> 0 && ds!=null)
                {
                    string filepath = ds.Rows[0]["path"].ToString();
                    string filename = ds.Rows[0]["filename"].ToString();
                    string accname  = ds.Rows[0]["accfilename"] == null ? "" : ds.Rows[0]["accfilename"].ToString();
            
                    if (!string.IsNullOrEmpty(filepath.Trim()))
                    {
                       if (!string.IsNullOrEmpty(filename))
                       {
                           if (File.Exists(HttpContext.Current.Server.MapPath(filepath + filename)))
                            {
                                File.Delete(HttpContext.Current.Server.MapPath(filepath + filename));
                            }
                       }

                       if (!string.IsNullOrEmpty(accname))
                       {
                           if (File.Exists(HttpContext.Current.Server.MapPath(filepath + accname)))
                           {
                               File.Delete(HttpContext.Current.Server.MapPath(filepath + accname));
                           }
                       }
                    }
                }
                delparam+=i + ";" ;
             }
             catch(Exception e)
             { return -2; }
           
          }

          param[0] = new SqlParameter("@idlst", delparam);
          param[1] = new SqlParameter("@flag", "1");
          try
          {
              SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_tmpfileaddordel", param);
          }
          catch
          { return 0;}

          return 1;
    }


    public int AddTmpFile(IList<string> delobj)
    {
        string addparam = "";
       
        if (delobj.Count == 0 || delobj == null)
            return -1;

        foreach (string i in delobj)
        {       
              addparam += i + ";";
        }
       
        param[0] = new SqlParameter("@idlst", addparam);
        param[1] = new SqlParameter("@flag", "0");
        try
        {
            SqlHelper.ExecuteNonQuery(adam.dsn0(), CommandType.StoredProcedure, "sp_tmpfileaddordel", param);
        }
        catch
        { return 0; }

        return 1;
    }



}