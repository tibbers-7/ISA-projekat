namespace BloodBankLibrary.Core.EmailSender
{
    public interface IEmailSendService
    {
        void SendEmail(Message message);
    }
}
