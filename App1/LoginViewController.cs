using Foundation;
using System;
using UIKit;

namespace IntervallRun
{
    public partial class LoginViewController : UIViewController
    {
        public LoginViewController (IntPtr handle) : base (handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            LoginInButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                ViewController controller = this.Storyboard.InstantiateViewController("ViewController") as ViewController;
                this.NavigationController.PushViewController(controller, true);
            };

            RegistrateButton.TouchUpInside += (object sender, EventArgs e) =>
            {
                RegistrationViewController controller = this.Storyboard.InstantiateViewController("RegistrationViewController") as RegistrationViewController;
                this.NavigationController.PushViewController(controller, true);
            };

        }

    }
}