using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for RepAcademicTranscriptAction
/// </summary>
public class RepAcademicTranscriptAction
{
	public RepAcademicTranscriptAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable getStudentBasicInfo(string sid)
    {
        string query = " SELECT STUDENT.SID,SPROGRAM ,ADMINSEMETER,ADMINYEAR,SNAME,"+
                   " GRADUATIONSEMESTER,GRADUATIONYEAR,GRADUATIONDATE,MAJOR, " +
                   " COLLEGE.COLLEGENAME, DEPARTMENTINCOLLEGE.DEGREENAME,DEPNAME,REQCREDITHRS "+
                   " FROM STUDENT,DEPARTMENTINCOLLEGE,COLLEGE "+
                   " WHERE SID=? "+
                   " AND STUDENT.SPROGRAM = DEPARTMENTINCOLLEGE.DEPCODE "+
                   " AND DEPARTMENTINCOLLEGE.COLLEGECODE=COLLEGE.COLLEGECODE ";
         DatabaseHelper objhelper = new DatabaseHelper();
         objhelper.AddParameter("@SID", sid);
         DataSet ds = new DataSet();
         DataTable tb = new DataTable();   
         ds.Merge(objhelper.ExecuteDataSet(query));
         if (ds.Tables.Count > 0)
         {
             tb.Merge(ds.Tables[0]);
         }
         
         return tb;
    }


    public DataTable getStudentWaiver(string sid)
    {
        string query =" SELECT PUNINAME,EUCCODE, CREDIT, PUNICCODE, PUNICNAME, PUNICREDIT FROM COURSEWAIVER "+
                    " WHERE SID = ? ";
        DatabaseHelper objhelper = new DatabaseHelper();
        objhelper.AddParameter("@SID", sid);
        DataSet ds = new DataSet();
        DataTable tb = new DataTable();
        ds.Merge(objhelper.ExecuteDataSet(query));
        if (ds.Tables.Count > 0)
        {
            tb.Merge(ds.Tables[0]);
        }

        return tb;
    }


    public DataTable getStudentTransfer(string sid)
    {
        string query = " SELECT TRANSFERFROM,COURSECODE, EUCREDIT, CCODEPREUNI, CNAMEPREUNI, PUNICREDIT, GRADE, GP FROM TRANSFER " +
                    " WHERE SID = ? ";
        DatabaseHelper objhelper = new DatabaseHelper();
        objhelper.AddParameter("@SID", sid);
        DataSet ds = new DataSet();
        DataTable tb = new DataTable();
        ds.Merge(objhelper.ExecuteDataSet(query));
        if (ds.Tables.Count > 0)
        {
            tb.Merge(ds.Tables[0]);
        }

        return tb;
    }

    public DataTable getStudentCompletedCourses(string sid,int profiles)
    {
        string query = "";
        if (profiles == 1)
        {
            query = " SELECT  SEMESTER, YEAR,OFFEREDCOURSE.COURSECODE, OFFERERINGANDGRADE.GGRADE," +
                " CHOURS,GRADINGPOLICY.GRADEPOINT,CHANGEDCREDIT.FLAG " +
                " FROM OFFERERINGANDGRADE,REGISTATUS,OFFEREDCOURSE,CHANGEDCREDIT,GRADINGPOLICY " +
                " WHERE SID=? AND OFFERERINGANDGRADE.GGRADE IS NOT NULL AND " +
                " OFFERERINGANDGRADE.GGRADE !='W' " +
                " AND OFFERERINGANDGRADE.GGRADE != 'F' AND OFFERERINGANDGRADE.GGRADE != 'I' " +
                " AND OFFERERINGANDGRADE.REGKEY=REGISTATUS.REGKEY AND " +
                " (OFFERERINGANDGRADE.GGRADE = GRADINGPOLICY.GRADETYPE AND " +
                " GRADINGPOLICY.PROFILES=? ) AND CHANGEDCREDIT.COURSECODE = " +
                " OFFEREDCOURSE.COURSECODE AND " +
                " OFFEREDCOURSE.COURSEKEY=OFFERERINGANDGRADE.COURSEKEY " +
                " ORDER BY CHANGEDCREDIT.FLAG,OFFEREDCOURSE.COURSECODE,GRADINGPOLICY.GRADEPOINT ASC ";
        }
        else if (profiles == 2)
        {
            query = " SELECT  SEMESTER, YEAR,OFFEREDCOURSE.COURSECODE, OFFERERINGANDGRADE.GGRADE2 as GGRADE," +
               " CHOURS,GRADINGPOLICY.GRADEPOINT,CHANGEDCREDIT.FLAG " +
               " FROM OFFERERINGANDGRADE,REGISTATUS,OFFEREDCOURSE,CHANGEDCREDIT,GRADINGPOLICY " +
               " WHERE SID=? AND OFFERERINGANDGRADE.GGRADE2 IS NOT NULL AND " +
               " OFFERERINGANDGRADE.GGRADE2 !='W' " +
               " AND OFFERERINGANDGRADE.GGRADE2 != 'F' AND OFFERERINGANDGRADE.GGRADE2 != 'I' " +
               " AND OFFERERINGANDGRADE.REGKEY=REGISTATUS.REGKEY AND " +
               " (OFFERERINGANDGRADE.GGRADE = GRADINGPOLICY.GRADETYPE AND " +
               " GRADINGPOLICY.PROFILES=? ) AND CHANGEDCREDIT.COURSECODE = " +
               " OFFEREDCOURSE.COURSECODE AND " +
               " OFFEREDCOURSE.COURSEKEY=OFFERERINGANDGRADE.COURSEKEY " +
               " ORDER BY CHANGEDCREDIT.FLAG,OFFEREDCOURSE.COURSECODE,GRADINGPOLICY.GRADEPOINT ASC ";
        }
        DatabaseHelper objhelper = new DatabaseHelper();
        objhelper.AddParameter("@SID", sid);
        objhelper.AddParameter("@PROFILES", profiles);
        DataSet ds = new DataSet();
        DataTable tb = new DataTable();
        ds.Merge(objhelper.ExecuteDataSet(query));
        if (ds.Tables.Count > 0)
        {
            tb.Merge(ds.Tables[0]);
        }

        return tb;
    }
}
