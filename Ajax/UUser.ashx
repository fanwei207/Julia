<%@ WebHandler Language="C#" Class="UUser" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;
using System.Collections.Generic;

/// <summary>
/// User的定义
/// </summary>
public class UUserCell
{
    public string UserNo
    {
        get;
        set;
    }
    public string UserName
    {
        get;
        set;
    }
    public string Dept
    {
        get;
        set;
    }
    public string Post
    {
        get;
        set;
    }
    public string Domain
    {
        get;
        set;
    }
}
/// <summary>
/// 和User类似。只不过，UUser拉取的用户要区分域
/// </summary>
public class UUser : IHttpHandler, IRequiresSessionState
{
    adamClass adam = new adamClass();

    public void ProcessRequest (HttpContext context) {

        if (context.Request.Params["req"] == null) {
            return;
        }
        
        context.Response.ContentType = "text/html";

        JavaScriptSerializer json = new JavaScriptSerializer();
        
        string _user = context.Request.Params["req"].ToString();
        string _plantCode = context.Session["PlantCode"].ToString();

        string strSql = "Select Top 10 u.userNo, u.userName, deptName = Isnull(dept.name, N'--'), roleName = Isnull(roleName, N'--')";
        strSql += "     From tcpc0.dbo.Users u";
        strSql += "     Left Join tcpc" + _plantCode + ".dbo.Departments dept On dept.departmentId = u.departmentId";
        strSql += "     Left Join tcpc" + _plantCode + ".dbo.Roles rol On rol.id = u.roleID";
        strSql += "     Where plantCode = " + _plantCode;
        strSql += "         And leaveDate is null";
        strSql += "	        And userNo = '" + _user + "'";

        IList<UUserCell> list = new List<UUserCell>();
        
        try
        {            
            DataTable table = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql).Tables[0];

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    UUserCell item = new UUserCell();
                    item.UserNo = row["userNo"].ToString();
                    item.UserName = row["userName"].ToString();
                    item.Dept = row["deptName"].ToString();
                    item.Post = row["roleName"].ToString();

                    if (Convert.ToInt32(_plantCode) == 1)
                    {
                        item.Domain = "SZX";
                    }
                    else if (Convert.ToInt32(_plantCode) == 2)
                    {
                        item.Domain = "ZQL";
                    }
                    else if (Convert.ToInt32(_plantCode) == 5)
                    {
                        item.Domain = "YQL";
                    }
                    else if (Convert.ToInt32(_plantCode) == 8)
                    {
                        item.Domain = "HQL";
                    }
                    else if (Convert.ToInt32(_plantCode) == 11)
                    {
                        item.Domain = "TCB";
                    }
                    else if (Convert.ToInt32(_plantCode) == 98)
                    {
                        item.Domain = "TCP-EN";
                    }
                    else if (Convert.ToInt32(_plantCode) == 99)
                    {
                        item.Domain = "TCP-US";
                    }  
                                                            
                    list.Add(item);
                }
            }
            else
            {
                UUserCell item = new UUserCell();
                item.UserNo = "无数据";
                item.UserName = "";
                item.Dept = "";
                item.Post = "";
                item.Domain = "";

                list.Add(item);
            }
        }
        catch
        {
            UUserCell item = new UUserCell();
            item.UserNo = "无数据";
            item.UserName = "";
            item.Dept = "";
            item.Post = "";
            item.Domain = "";

            list.Add(item);
        }

        context.Response.Write(json.Serialize(list));
        context.Response.End();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }
}