using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notification
{
    [Serializable]
    class Notification
    {
        private static int gID = 10000;
        private MailAddress mFromMail = new MailAddress("");
        private String mFromMailPWD = "PWD";
        private MailMessage mMailItem;
        int mID { get; set; }
        LinkedList <MailAddress> mToMail { get; set; }
        String mContent { get; set; }
        DateTime mOrderDateTime { get; set; }
        DateTime mNotificationDateTime { get; set; }
        bool mIsIntervall { get; set; }
        DateTime mInterval { get; set; }

        public Notification(String pToMail, String pContent, DateTime pOrderDateTime, DateTime pNotificationDateTime, String pSubject)
        {
            mToMail = new LinkedList<MailAddress>();
            mToMail.AddLast(new MailAddress(pToMail));
            mContent = pContent;
            mOrderDateTime = pOrderDateTime;
            mNotificationDateTime = pNotificationDateTime;
            mMailItem = new MailMessage();
            mMailItem.From = mFromMail;
            mMailItem.Subject = pSubject;

            mID = gID++;
        }

        public Notification(String pToMail, String pContent, DateTime pOrderDateTime, DateTime pNotificationDateTime, String pSubject ,bool pIsIntervall, DateTime pIntervall)
        {
            new Notification(pToMail, pContent, pOrderDateTime, pNotificationDateTime, pSubject);
            this.mIsIntervall = pIsIntervall;
            this.mInterval = pIntervall;
        }

        public void sendMail()
        {
            for (int i = 0; i < mToMail.Count; i++ )
                mMailItem.To.Add(mToMail.ElementAt(1));

            mMailItem.Body = mContent;
            //mMailItem.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.live.com", 25);
            try
            {
                client.Credentials = new System.Net.NetworkCredential("koopakiller@live.de", "Passwort");//Anmeldedaten für den SMTP Server 

                client.EnableSsl = true; 

                client.Send(mMailItem); //Senden 

                Console.WriteLine("E-Mail wurde versendet"); 
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler beim Senden der E-Mail\n\n{0}", e.Message);
            }
        }

        public void doWork()
        {
            if (DateTime.Now == mNotificationDateTime)
                sendMail();
        }
    }
}
