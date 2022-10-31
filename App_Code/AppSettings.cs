using System;
using System.Collections.Generic;
using System.Web;

public class AppSettings
{
    private static string _CourseOfferingOpenedDepList;
    public static string CourseOfferingOpenedDepList
    {
        get { return _CourseOfferingOpenedDepList; }
        set { _CourseOfferingOpenedDepList = value; }
    }
    //public static string CourseOfferingOpenedDepList { get; set; }

    public AppSettings()
    {

    }
}
