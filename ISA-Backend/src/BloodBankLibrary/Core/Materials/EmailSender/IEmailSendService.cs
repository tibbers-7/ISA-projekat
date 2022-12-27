﻿namespace BloodBankLibrary.Core.EmailSender
{
    public interface IEmailSendService
    {
        void SendEmail(Message message);
        void SendWithQR(Message message,byte[] qr,string path);
    }
}
