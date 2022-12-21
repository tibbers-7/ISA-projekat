﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankLibrary.Core.EmailSender
{
    public interface IEmailSendService
    {
        void SendEmail(Message message);
    }
}
