using Foundation;
using System;
using UIKit;

namespace IntervallRun
{
    public partial class RegistrationViewController : UIViewController
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Mail { get; set; }
        public string regInfo { get; set; }
        public RegistrationViewController(IntPtr handle) : base(handle)
        {
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            

            RegButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                Name = NameText.Text;
                Age = AgeText.Text;
                Mail = MailText.Text;
                SetregistrationInformation(Name, Age, Mail);
                UserClient.Connect("10.0.1.5", GetregistrationInformation());
            };
        }

        public string GetregistrationInformation()
        {
            return regInfo;
        }

        public void SetregistrationInformation(string name, string age, string mail)
        {
            regInfo = "1" + " "  + name + " " + age + " " + mail.ToString();
        }

    }
}
