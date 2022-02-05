using MimeKit;
using System;

namespace TFG_Web.Interface
{
    public interface ISMTPServices
    {
        void SMPTServer(MimeMessage message);
    }
}
