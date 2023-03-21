using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKBlog.Models
{
    public class Emails
    {
        public string Email { get; set; } = string.Empty;
        public int OTP { get; set; }
        public DateTime Date { get; set; }
        public Emails(string email, int otp, DateTime dt)
        {
            Email = email;
            OTP = otp;
            Date = dt;
        }
    }
}
