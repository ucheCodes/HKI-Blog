namespace HKBlog.Controllers
{
    using Newtonsoft.Json;
    using HKBlog.Database;
    using HKBlog.Models;
    using HKBlog.MailService;
    using System.Collections;
    using System.ComponentModel;
    using HKBlog.Controllers;

    public class UserController : IUserController
    {
        private readonly IDatabase database;
        private readonly IMailService ms;
        public UserController(IDatabase database,IMailService ms)
        {
            this.database = database;
            this.ms = ms;
        }
        public async Task<bool> AddUser(User user)
        {
            string id = JsonConvert.SerializeObject(user.Id);
            user.Email = user.Email.ToLower();
            var serialized = JsonConvert.SerializeObject(user);
            var res = await database.Create("Users", id, serialized);
            return res;
        }
        public async Task<List<User>> ReadAllUsers()
        {
            List<User> users = new List<User>();
            var data = (await database.ReadAll("Users")).ToList();
            if (data.Count > 0)
            {
                foreach (var d in data)
                {
                    var des = JsonConvert.DeserializeObject<User>(d.Value);
                    users.Add(des ?? new User());
                }
            }
            return users;
        }
        public async void SendUsersPassword(string email)
        {
            var users = (await ReadAllUsers()).ToList<User>();
            if (users != null && users.Count > 0)
            {
                var isUser = users.Find(x => x.Email.Equals(email));
                if (isUser != null)
                {
                    await ms.Send("peters.soft.network@gmail.com", email, "Password retrieval from HKI",
                     $"<p>kindly login on to the site with your registered password {isUser.Password}");
                }
            }
        }
        public async Task<bool> IsEmailOTP(int otp, string email)
        {
            var d = await database.Read("Emails", JsonConvert.SerializeObject(email));
            if (!string.IsNullOrEmpty(d.Value))
            {
                var data = JsonConvert.DeserializeObject<Emails>(d.Value);
                if (data != null && data.Email.Equals(email) && data.OTP.Equals(otp))
                {
                    var time = DateTime.Now.Subtract(data.Date).TotalMinutes;
                    if (time <= 10 && data.OTP == otp)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public async Task<bool> IsEmailRegistered(string email)
        {
            var users = (await ReadAllUsers()).ToList<User>();
            if (users != null && users.Count > 0)
            {
                var isReg = users.Any(x => x.Email.Equals(email));
                if (!isReg)
                {
                    GetAndMailOTP(email);
                    return false;
                }
                return true;
            }
            GetAndMailOTP(email);
            return false;
        }
        public async void GetAndMailOTP(string email)
        {
            bool sendMail = true;
            Random rnd = new Random();
            int code = rnd.Next(1000, 9999);
            var allMails = (await database.ReadAll("Emails")).ToList();
            if (allMails != null && allMails.Count > 0)
            {
                foreach (var m in allMails)
                {
                    var mail = JsonConvert.DeserializeObject<Emails>(m.Value);
                    if (mail != null && mail.Email.Equals(email) && DateTime.Now.Subtract(mail.Date).TotalMinutes <= 10)
                    {
                        sendMail = false;
                    }
                }
            }
            if (sendMail)
            {
                await ms.Send("peters.soft.network@gmail.com", email, "PSN Form Validation Bot",
                    $"<p>kindly complete your registration on the site with this unique authentication code {code} " +
                    $"<em>The One Time Pin(OTP) code expires in 10 minutes.</em></p>");
                //save emails and OTPS sent differently
                Emails em = new Emails(email, code, DateTime.Now);
                var serialized = JsonConvert.SerializeObject(em);
                var mails = await database.Create("Emails", JsonConvert.SerializeObject(email), serialized);
            }
        }
        public async Task<User> LoginUser(string email, string password)
        {
            var users = await ReadAllUsers();
            var user = users.Find(u => u.Email.ToLower().Equals(email) && u.Password.Equals(password));
            if (user != null)
            {
                return user;
            }
            return  new User();
        }
    }
}