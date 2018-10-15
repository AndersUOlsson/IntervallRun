
using AudioToolbox;
using CoreLocation;
using MapKit;
using System;
using System.Net;
using System.Net.Sockets;
using UIKit;
using System.IO;
using System.Threading;
using Plugin.Connectivity;

namespace IntervallRun
{
    public partial class ViewController : UIViewController
    {
        CLLocationManager locationManager = new CLLocationManager();
        SystemSound systemSound = new SystemSound(1003);
        CLLocation location = new CLLocation();
        CLLocation pointA;

        public double MaxValue { get; private set; }
        public double MinValue { get; private set; }
        private bool StartFlag { get; set; }
        public double Speed { get; set; }
        private double MaxMinPerKm { get; set; }
        private double MinMinPerKm { get; set; }
        public double LengthOfTheRun { get; private set; }
        private double StartLocation { get; set; }
        private bool FlagForRun { get; set; }
        public object Main { get; private set; }

        SystemSound soundTooSlow = new SystemSound(1100);
        SystemSound soundTooFast = new SystemSound(1003);
        public static readonly SystemSound Vibrate;
        private static int TIMES_FOR_FAST = 5;
        private static int TIMES_FOR_SLOW = 20;
        AudioManager audioManager;
        TcpClient sock = new TcpClient();

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
            audioManager = new AudioManager();

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
                  
                    if (!StartFlag)
                    {
                        pointA = new CLLocation(map.UserLocation.Location.Coordinate.Latitude, 
                                                map.UserLocation.Location.Coordinate.Longitude);

                        LeftOnRunlabel.Hidden = true;
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

            FlagForRun = true;
            StartFlag = false;
            startButton.TouchUpInside += (object sender, System.EventArgs e) =>
            {
                StartFlag = !StartFlag;
            };
        }

        private void ControllTheRunnersSpeed(double maxValue, double minValue, double currentSpeed)
        {

            LeftOnRunlabel.Hidden = false;
            Console.WriteLine("This is the speed" + currentSpeed);

            if (maxValue < currentSpeed)
            {
                Console.WriteLine("You are runnig to fast, SLOW down. MAX Value" + maxValue);
                audioManager.PlaySound("moveSlower.mp3");

                int i = 0;
                while(i < TIMES_FOR_FAST)
                {
                    SystemSound.Vibrate.PlaySystemSound();
                    i++;
                }
            }
            else if(minValue > currentSpeed)
            {
                Console.WriteLine("You are running to slow, SPEED up, MIN Value" + minValue);
                audioManager.PlaySound("moveFaster.mp3");

                int i = 0;
                while (i < TIMES_FOR_SLOW)
                {
                    SystemSound.Vibrate.PlaySystemSound();
                    i++;
                }
            }
            else
            {
                Console.WriteLine("You are running in a perfect speed");
            }
        }

        //Showed calculate how long you have run and how long you have left.
        private void MeterLeftToRun(CLLocation pointA)
        {
            CLLocation pointB = new CLLocation(map.UserLocation.Location.Coordinate.Latitude,
                                               map.UserLocation.Location.Coordinate.Longitude);

            var distanceToB = pointB.DistanceFrom(pointA);


            //The connection an the information to the server.
            string text = "You have left" + distanceToB;
            Connect("10.0.1.5",  text);



            Console.WriteLine("THIS IS THE Destination between A and B" + distanceToB.ToString());
            Console.WriteLine("INTERNET IS ON" + CheckConnectivity());
            if (LengthOfTheRun <= distanceToB)
            {
                Console.WriteLine("END OF RUN");
                audioManager.PlaySound("Finished.mp3");
            }
            double length = Math.Round(distanceToB, 2);
            Console.WriteLine(LengthOfTheRun - distanceToB);
            LeftOnRunlabel.Text = "YOU HAVE RUN: " + length.ToString();
        }


        //Connecting to the server. The host machine that runs the emulator.
        public static void Connect(String server, String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 8001;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                Stream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", message);

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);

                // Close everything.
                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }


            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }

        private bool CheckConnectivity()
        {
            var isConnected = CrossConnectivity.Current.IsConnected;
            return isConnected;
        }
    }
}