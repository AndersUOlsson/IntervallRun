
using AudioToolbox;
using CoreLocation;
using MapKit;
using System;
using UIKit;

namespace IntervallRun
{
    public partial class ViewController : UIViewController
    {
        CLLocationManager locationManager = new CLLocationManager();
        SystemSound systemSound = new SystemSound(1003);
        CLLocation location = new CLLocation();

        //public static CLLocationManager Manager { get; set; }

        public double MaxValue { get; private set; }
        public double MinValue { get; private set; }
        private bool StartFlag { get; set; }
        public double Speed { get; set; }
        private double MaxMinPerKm { get; set; }
        private double MinMinPerKm { get; set; }
        public double LengthOfTheRun { get; private set; }
        private double StartLocation { get; set; }
        private bool flagForRun { get; set; }
     
        CLLocation pointA;



        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad();
            map = new MKMapView(View.Bounds)
            {
                AutoresizingMask = UIViewAutoresizing.FlexibleDimensions
            };

            //This LoadView function fix the resizing of the map problem, when zoomed in on user.
            LoadView();
            View.AddSubview(map);

            //Take a look at UIKit.UIView.SizeThatFits
            locationManager.RequestWhenInUseAuthorization();
            locationManager.RequestAlwaysAuthorization();
            map.ShowsUserLocation = true;
     



            map.DidUpdateUserLocation += (s, ergs) =>
            {
                if (map.UserLocation != null)
                {
                          
                    CLLocationCoordinate2D coords = map.UserLocation.Coordinate;
                    MKCoordinateSpan span = new MKCoordinateSpan(0.01, 0.01);
                    map.Region = new MKCoordinateRegion(coords, span);
                    Speed = map.UserLocation.Location.Speed;


                    if(!StartFlag)
                    {
                        pointA = new CLLocation(map.UserLocation.Location.Coordinate.Latitude, map.UserLocation.Location.Coordinate.Longitude);
                    }
                         
                    if (StartFlag)
                    {
                        StartLocation = map.UserLocation.Location.Coordinate.Latitude;
                       
                        Console.WriteLine(StartLocation);
                        MeterLeftToRun(pointA);
                        ControllTheRunnersSpeed(MaxValue, MinValue, Speed);
                    }
                }
            };

            //Initialize max speed stepper, this is config for setting max speed on run.
            maxSpeedStepper.MaximumValue = 150.0;
            maxSpeedStepper.MinimumValue = 0.0;
            maxSpeedStepper.Value = 3.9 * 10;
            maxSpeedStepper.ValueChanged += (sender, args) =>
            {
                MaxValue = maxSpeedStepper.Value / 10.0;
                maxSpeedLabel.Text = MaxValue.ToString();
            };

            //Initialize max speed stepper, this is config for setting max speed on run.
            minSpeedStepper.MaximumValue = 150.0;
            minSpeedStepper.MinimumValue = 0.0;
            minSpeedStepper.Value = 3.9 * 10;
            minSpeedStepper.ValueChanged += (sender, args) =>
            {
                MinValue = minSpeedStepper.Value / 10.0;
                minSpeedLabel.Text = MinValue.ToString();
            };


            destinationSlider.ValueChanged += (sender, args) =>
            {
                LengthOfTheRun = (int)destinationSlider.Value;
                destinationSliderLabel.Text = LengthOfTheRun.ToString();
            };

            flagForRun = true;
            StartFlag = false;
            startButton.TouchUpInside += (object sender, System.EventArgs e) =>
            {
                StartFlag = !StartFlag;
                Console.WriteLine("You have pressed the button");
            };
        }

        private void ControllTheRunnersSpeed(double maxValue, double minValue, double currentSpeed)
        {
            Console.WriteLine("This is the speed" + currentSpeed);

            if (maxValue > currentSpeed)
            {
                Console.WriteLine("You are runnig to fast, SLOW down. MAX Value" + maxValue);
            }
            else if(minValue < currentSpeed)
            {
                Console.WriteLine("You are running to slow, SPEED up, MIN Value" + minValue);
            }
            else
            {
                Console.WriteLine("You are running in a perfect speed");
            }
        }


        //Showed calculate how long you have run and how long you have left.
        private void MeterLeftToRun(CLLocation pointA)
        {
            CLLocation pointB = new CLLocation(map.UserLocation.Location.Coordinate.Latitude, map.UserLocation.Location.Coordinate.Longitude);
            var distanceToB = pointB.DistanceFrom(pointA);
            Console.WriteLine("THIS IS THE Destination between A and B" + distanceToB.ToString());

            if(LengthOfTheRun <= distanceToB)
            {
                Console.WriteLine("END OF RUN");
            }

            Console.WriteLine(LengthOfTheRun - distanceToB);
            
        }
    }
}