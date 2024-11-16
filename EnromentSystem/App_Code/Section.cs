using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Section
/// </summary>
public class Section
{
    public string name;
    public string sectionId;
    public string semester;
    public string courseId;
    public int maxEnrollAllow;
    public List<Class> lectureClass = new List<Class>();
    public List<Class> practicalClass = new List<Class>();

    public Section(string name, string semester, string courseId)
    {
        this.name = name.ToUpper();
        this.semester = semester.ToUpper();
        this.courseId = courseId.ToUpper();
        sectionId = GenerateSectionId(name,semester,courseId).ToUpper();
    }
    public Section(string name, string sectionId, string semester, string courseId, int maxEnrollAllow)
    {
        this.name = name.ToUpper();
        this.sectionId = sectionId.ToUpper();
        this.semester = semester.ToUpper();
        this.courseId = courseId.ToUpper();
        this.maxEnrollAllow = maxEnrollAllow;
    }

    private string GenerateSectionId(string name, string semester, string courseId)
    {
        return $"{courseId}-{semester}-{name}";
    }

    public void AddLectureClass(string classRoom, string lectureId, int timeIndex)
    {
        string classId = $"{sectionId}-{lectureClass.Count+1}".ToUpper();
        string classType = "LECTURE";
        lectureClass.Add(new Class(classId,classType,classRoom.ToUpper(),lectureId.ToUpper(),timeIndex));
    }
    
    public void AddPracticalClass(string classRoom, string lectureId, int timeIndex)
    {
        string classId = $"{sectionId}-{lectureClass.Count+1}".ToUpper();
        string classType = "PRACTICAL";
        practicalClass.Add(new Class(classId,classType,classRoom.ToUpper(),lectureId.ToUpper(),timeIndex));
    }

    public DataTable GetLectureClassTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("classRoom", typeof(string));
        table.Columns.Add("timeIndex", typeof(int));
        for (int i = 0; i < lectureClass.Count; i++)
        {
            string classRoom = lectureClass[i].classRoom;
            int timeIndex = lectureClass[i].timeIndex;
            table.Rows.Add(classRoom, timeIndex);
        }

        return table;
    }
    public DataTable GetPracticalClassTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("classRoom", typeof(string));
        table.Columns.Add("timeIndex", typeof(int));
        for (int i = 0; i < practicalClass.Count; i++)
        {
            string classRoom = practicalClass[i].classRoom;
            int timeIndex = practicalClass[i].timeIndex;
            table.Rows.Add(classRoom, timeIndex);
        }

        return table;
    }
}