<%@ WebHandler Language="C#" Class="User" %>

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
public class UserCell
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
/// 和UserDomain类似。只不过，User拉取的用户不区分域
/// </summary>
public class User : IHttpHandler, IRequiresSessionState
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

        string strSql = "Select Top 10 u.userNo, u.userName, deptName = Isnull(dept.deptName, N'--'), roleName = Isnull(roleName, N'--'), domain = deptDomain";
        strSql += "     From tcpc0.dbo.Users u";
        strSql += "     Left Join(";
        strSql += "                 Select deptID = departmentID, deptName = Name, deptDomain = 'PML', deptPlant = 1";
        strSql += "                 From tcpc1.dbo.Departments";
        strSql += "                 Where isSalary = 1";
        strSql += "                 Union";
        strSql += "                 Select departmentID, Name, deptDomain = 'PML', 2";
        strSql += "                 From tcpc2.dbo.Departments";
        strSql += "                 Where isSalary = 1";
        strSql += "                 Union";
        strSql += "                 Select departmentID, Name, deptDomain = 'PML', 3";
        strSql += "                 From tcpc3.dbo.Departments";
        strSql += "                 Where isSalary = 1";
        strSql += "              ) dept On dept.deptID = u.departmentId And dept.deptPlant = u.plantCode";
        strSql += "     Left Join (";
        strSql += "                 Select roleID, roleName, roleDomain = 'PML', rolePlant = 1";
        strSql += "                 From tcpc1.dbo.Roles";
        strSql += "                 Union";
        strSql += "                 Select roleID, roleName, roleDomain = 'PML', 2";
        strSql += "                 From tcpc2.dbo.Roles";
        strSql += "                 Union";
        strSql += "                 Select roleID, roleName, roleDomain = 'PML', 3";
        strSql += "                 From tcpc3.dbo.Roles";
        strSql += "               ) rol On rol.roleID = u.roleID And rol.rolePlant = u.plantCode";
        strSql += "     Where (userNo Like '" + _user + "%' Or userName Like N'" + _user + "%')";
        strSql += "         And leaveDate is null order by u.userno";
        
        IList<UserCell> list = new List<UserCell>();
        
        try
        {            
            DataTable table = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, strSql).Tables[0];

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    UserCell item = new UserCell();
                    item.UserNo = row["userNo"].ToString();
                    item.UserName = row["userName"].ToString();
                    item.Dept = row["deptName"].ToString();
                    item.Post = row["roleName"].ToString();
                    item.Domain = row["domain"].ToString();

                    list.Add(item);
                }
            }
            else
            {
                UserCell item = new UserCell();
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
            UserCell item = new UserCell();
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