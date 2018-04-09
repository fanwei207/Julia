using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SampleDelivery
/// </summary>
[Serializable]
public class SampleDelivery
{
    private int id;
    public int ID
    {
        get
        {
            return id;
        }
    }

    private string no;
    public string No
    {
        get 
        {
            return no;
        }
    }

    private string receiver;
    public string Receiver
    {
        get
        {
            return receiver;
        }
        set
        {
            receiver = value;
        }
    }

    private string shipto;
    public string Shipto
    {
        get
        {
            return shipto;
        }
        set
        {
            shipto = value;
        }
    }

    private string remarks;
    public string Remarks
    {
        get
        {
            return remarks;
        }
        set
        {
            remarks = value;
        }
    }

    private int createdBy;
    public int CreatedBy
    {
        set
        {
            createdBy = value;
        }
        get
        {
            return createdBy;
        }
    }
    private string creator;
    public string Creator
    {
        get
        {
            return creator;
        }
        set
        {
            creator = value;
        }
    }

    private DateTime createdDate;
    public DateTime CreatedDate
    {
        get
        {
            return createdDate;
        }
        set
        {
            createdDate = value;
        }
    }

    private int sendedBy;
    public int SendedBy
    {
        set
        {
            sendedBy = value;
        }
        get
        {
            return sendedBy;
        }
    }
    private string sender;
    public string Sender
    {
        get 
        {
            return sender;
        }
        set
        {
            sender = value;
        }
    }

    private DateTime sendedDate;
    public DateTime SendedDate
    {
        get
        {
            return sendedDate;
        }
        set
        {
            sendedDate = value;
        }
    }

    private bool? checkResult;
    public bool? CheckResult
    {
        get
        {
            return checkResult;
        }
        set
        {
            checkResult = value;
        }
    }

    private bool isSended;
    public bool IsSended
    {
        get
        {
            return isSended;
        }
        set
        {
            isSended = value;
        }
    }

    private bool isCanceled;
    public bool IsCanceled
    {
        get
        {
            return isCanceled;
        }
        set
        {
            isCanceled = value;
        }
    }

    private int detId;
    public int DetId
    {
        set
        {
            detId = value;
        }
        get
        {
            return detId;
        }
    }

    private int mstrID;

    public int MstrID
    {
        get { return mstrID; }
        set { mstrID = value; }
    }

	public SampleDelivery()
	{
		
	}

    public SampleDelivery(int id)
    {
        this.id = id;
    }

    public SampleDelivery(int id, string no)
    {
        this.id = id;
        this.no = no;
    }
    
}