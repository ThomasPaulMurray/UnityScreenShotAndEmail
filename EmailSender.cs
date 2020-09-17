using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.UI;

public class EmailSender : MonoBehaviour
{
    public InputField EnteredEmail;
    public void SendPhoto()
    {
        var client = new SmtpClient("Smtp Host Here", portHere)
        {
            Credentials = new NetworkCredential("email address here", "password here"),
            EnableSsl = true
        };
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        Camera.main.targetTexture = rt;
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        Camera.main.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();


        Attachment att = new Attachment(new MemoryStream(bytes), "Photo.png");
        MailMessage mailMessage = new MailMessage("wmail address", EnteredEmail.text, "subject test", "body test");

        mailMessage.Attachments.Add(att);

        client.Send(mailMessage);
    }
}
