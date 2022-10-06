using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;

namespace Rgs.Dms.Integration.MailTools
{
    public static class TestConnectionMailKit
    {
        public static void connection(CancellationToken cancellationToken = default) 
        {
			using (var client = new ImapClient())
			{
				client.CheckCertificateRevocation = false;
				client.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
				client.Connect("mail.it.rgs.ru", 143, SecureSocketOptions.Auto, cancellationToken);

				client.Authenticate("mktickets_test@it.rgs.ru", "ue3Tie^Teig,uh1O");

				// The Inbox folder is always available on all IMAP servers...
				var inbox = client.Inbox;
				inbox.Open(FolderAccess.ReadOnly);

				Console.WriteLine("Total messages: {0}", inbox.Count);
				Console.WriteLine("Recent messages: {0}", inbox.Recent);

				for (int i = 0; i < inbox.Count; i++)
				{
					var message = inbox.GetMessage(i);
					Console.WriteLine("Subject: {0}", message.Subject);
				}

				client.Disconnect(true);
			}
		}
    }
}