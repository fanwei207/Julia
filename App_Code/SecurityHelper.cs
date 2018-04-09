using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.ApplicationBlocks.Data;
using System.Collections;
using System.Runtime.Serialization;
using System.Collections.Generic;

/// <summary>
/// 菜单权限类
/// </summary>
[Serializable]
public class Security
{
    private string _id;
    /// <summary>
    /// 菜单或权限ID
    /// </summary>
    public string ID
    {
        get { return this._id; }
        set { this._id = value; }
    }

    private string _name;
    /// <summary>
    /// 菜单或权限名称
    /// </summary>
    public string Name
    {
        get { return this._name; }
        set { this._name = value; }
    }

    private bool _valid;
    /// <summary>
    /// 是否验证通过
    /// </summary>
    public bool isValid
    {
        get { return this._valid; }
        set { this._valid = value; }
    }

    public Security(string id, string name)
    {
        this._id = id;
        this._name = name;
        this._valid = false;
    }

    public Security(string id, bool valid)
    {
        this._id = id;
        this._name = string.Empty;
        this._valid = valid;
    }
}

/// <summary>
/// 菜单权限操作类
/// </summary>
[Serializable]
public class SecurityHelper
{
    private Hashtable _ht;
    /// <summary>
    /// 用于存放权限
    /// </summary>
    public Hashtable Hashtable
    {
        get { return this._ht; }
        set { this._ht = value; }
    }

    /// <summary>
    /// 注册权限列表
    /// </summary>
    private IList<Security> _list = new List<Security>();

    public SecurityHelper(Hashtable ht)
    {
        this._ht = ht;
        //this._list = new 
    }

    /// <summary>
    /// 索引
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Security this[string id]
    {
        get
        {
            if (this._ht.Contains(id))
            {
                return new Security(this._ht[id].ToString(), true);
            }
            else
            {
                return new Security(id, false);
            }
        }
    }

    /// <summary>
    /// 跨页面（非子父关系）调用权限时，要先在OnPreInit事件中注册
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public IList<Security> Register(string id, string name)
    {
        this._list.Add(new Security(id, name));

        return this._list;
    }

    /// <summary>
    /// 重载。将Security列表拼接成字符串，以便做自权限认证
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string str = string.Empty;
        
        if (this._list.Count > 0)
        {
            foreach (Security secu in this._list)
            {
                str += secu.ID + ";";
            }
        }

        return str;
    }
}
