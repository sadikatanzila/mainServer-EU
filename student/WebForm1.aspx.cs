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
using System.Security.Cryptography;
using System.IO;
using System.Text;


public partial class student_WebForm1 : System.Web.UI.Page
{
   

    string AESKey = "Key@AES1234";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
        txtEncryptedString.Text = AESEncryptionDecryption.Encrypt(txtInput.Text, AESKey);
    }

    protected void btnDecrypt_Click(object sender, EventArgs e)
    {
        txtDecryptedString.Text = AESEncryptionDecryption.Decrypt(txtEncryptedString.Text, AESKey);
    }
}
