using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SampleDeliveryDocImport
/// </summary>
[Serializable]
public class SampleDeliveryDocImport
{
    private int id;
    public int Id
    {
        get
        {
            return id;
        }
    }

    private int detId;
    public int DetId
    {
        get
        {
            return detId;
        }
    }

    private string virtualFileName;
    public string VirtualFileName
    {
        get
        {
            return virtualFileName;
        }
        set
        {
            virtualFileName = value;
        }
    }

    private string fileName;
    public string FileName
    {
        get
        {
            return fileName;
        }
        set
        {
            fileName = value;
        }
    }

    private string fileDescription;
    public string FileDescription
    {
        get
        {
            return fileDescription;
        }
        set
        {
            fileDescription = value;
        }
    }

    private string filePath;
    public string FilePath
    {
        get
        {
            return filePath;
        }
        set
        {
            filePath = value;
        }
    }

    private int uploadBy;
    public int UploadBy
    {
        get
        {
            return uploadBy;
        }
        set
        {
            uploadBy = value;
        }
    }

    private string uploader;
    public string Uploader
    {
        get
        {
            return uploader;
        }
        set
        {
            uploader = value;
        }
    }

    private DateTime uploadDate;
    public DateTime UploadDate
    {
        get
        {
            return uploadDate;
        }
        set
        {
            uploadDate = value;
        }
    }

	public SampleDeliveryDocImport()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public SampleDeliveryDocImport(int id)
    {
        this.id = id;
    }

    public SampleDeliveryDocImport(int id, int detId)
    {
        this.id = id;
        this.detId = detId;
    }
}