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
/// Summary description for cls_message
/// </summary>
public class cls_message
{
    public cls_message()
    {
    }

    public string getMessage(int msg_id)
    {
        string msg = "";

        switch (msg_id)
        {
            case 1: msg = "No data found!";
                break;
            case 2: msg = "Saved successfully";
                break;
            case 3: msg = "Fail to add";
                break;
            case 4: msg = "No assignment uploaded";
                break;
            case 5: msg = "Already exists";
                break;
            case 6: msg = "Partialy or fully fail to delete.";
                break;
            case 7: msg = "No teacher allocated yet.";
                break;
            case 8: msg = "Image uploaded successfully.";
                break;
            case 9: msg = "You are not registered for this semester.";
                break;
            case 10: msg = "Already course offering process is closed and for the next semester look at notice board.";
                break;
            case 11: msg = "Course offering date not yet declare for this semester.";
                break;
            case 12: msg = "Please select (tick) only one course at a time, otherwise only first tick will considered. ";
                break;
            case 13: msg = "You are not a registered student.";
                break;
            case 14: msg = "Fail to save";
                break;
            case 15: msg = "Your advisor not yet approved your courses, you may contact with your advisor.";
                break;
            case 16: msg = "You have not choosen any course yet.";
                break;
            case 17: msg = "Already course offering process is closed. for more information conatct with IT Department ";
                break;
               
            case 18: msg = "you have not clear your accounce and library, please contact with these departments";
                break;
            case 19: msg = "Please Clear your dues & Contact with Account Office";
                break;
            case 20: msg = "you have not clear your library , please contact with library";
                break;
            case 21: msg = "Deleted successfully";
                break;
            case 22: msg = "Sorry can not save, please evaluate for each argument";
                break;
            case 23: msg = "You have approved the grade(s), so you can't change the grade again";
                break;
            case 24: msg = "Exam-controller has approved the grade(s), so you can't change the grade again";
                break;
            case 25: msg = "You are a dropout student.\n Please contact with registry department.";
                break;
			case 26: msg = "You cannot do advising as your six year’s period to complete graduation is over.\n Please contact Registrar’s Office for further information.";
                break;
            case 27: msg = "You cannot do advising as your CGPA has fallen below 2.5.\n Please contact your Course Advisor for further information.";
                break;
            case 28: msg = "This Teacher has been booked in this same day, in same time slot by another Course";
                break;



        }

        return msg;
    }
}
