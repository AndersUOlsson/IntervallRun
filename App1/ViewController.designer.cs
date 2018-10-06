// WARNING
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
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider destinationSlider { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel destinationSliderLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView map { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel maxSpeedLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStepper maxSpeedStepper { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel minSpeedLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIStepper minSpeedStepper { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton startButton { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (destinationSlider != null) {
                destinationSlider.Dispose ();
                destinationSlider = null;
            }

            if (destinationSliderLabel != null) {
                destinationSliderLabel.Dispose ();
                destinationSliderLabel = null;
            }

            if (map != null) {
                map.Dispose ();
                map = null;
            }

            if (maxSpeedLabel != null) {
                maxSpeedLabel.Dispose ();
                maxSpeedLabel = null;
            }

            if (maxSpeedStepper != null) {
                maxSpeedStepper.Dispose ();
                maxSpeedStepper = null;
            }

            if (minSpeedLabel != null) {
                minSpeedLabel.Dispose ();
                minSpeedLabel = null;
            }

            if (minSpeedStepper != null) {
                minSpeedStepper.Dispose ();
                minSpeedStepper = null;
            }

            if (startButton != null) {
                startButton.Dispose ();
                startButton = null;
            }
        }
    }
}