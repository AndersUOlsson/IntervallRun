﻿// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace IntervallRun
{
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton LoginInButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton RegistrateButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (LoginInButton != null) {
                LoginInButton.Dispose ();
                LoginInButton = null;
            }

            if (RegistrateButton != null) {
                RegistrateButton.Dispose ();
                RegistrateButton = null;
            }
        }
    }
}