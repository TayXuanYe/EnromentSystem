using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class
/// </summary>
public class Class
{
    public Class()
    {

    }
    
    public Class(string classId, string classType, string classRoom, string lecturerId, int timeIndex)
    {
        this.classId = classId;
        this.classType = classType;
        this.classRoom = classRoom;
        this.lecturerId = lecturerId;
        this.timeIndex = timeIndex; 
    }
    public string classId;
    public string classType;
    public string classRoom;
    public string lecturerId;
    public int timeIndex;
}