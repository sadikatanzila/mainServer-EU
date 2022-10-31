using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;


public partial class student_AdmitCard_Default : System.Web.UI.Page
{

    staff_webService obj_staff = new staff_webService();
    string AESKey = "Key@AES1234";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEncrypt_Click(object sender, EventArgs e)
    {

        txtEncryptedString.Text = obj_staff.Encrypt(txtInput.Text, AESKey);
    }

    protected void btnDecrypt_Click(object sender, EventArgs e)
    {
        txtDecryptedString.Text = obj_staff.Decrypt(txtEncryptedString.Text, AESKey);
    }
}
