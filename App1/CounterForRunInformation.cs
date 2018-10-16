using AudioToolbox;
using CoreLocation;
using MapKit;
using System;

namespace IntervallRun
{

    public class CounterForRunInformation
    {
        AudioManager audioManager;
        UserClient UserClient = new UserClient();
        private static int TIMES_FOR_FAST = 5;
        private static int TIMES_FOR_SLOW = 20;

        #region Public Methods
        public CounterForRunInformation()
        {
            audioManager = new AudioManager();
        }

        public void ControllTheRunnersSpeed(UIKit.UILabel LeftOnRunlabel, double maxValue, double minValue, double currentSpeed)
        {

            LeftOnRunlabel.Hidden = false;
            Console.WriteLine("This is the speed" + currentSpeed);

            if (maxValue < currentSpeed)
            {
                Console.WriteLine("You are runnig to fast, SLOW down. MAX Value" + maxValue);
                audioManager.PlaySound("moveSlower.mp3");

                int i = 0;
                while (i < TIMES_FOR_FAST)
                {
                    SystemSound.Vibrate.PlaySystemSound();
                    i++;
                }
            }
            else if (minValue > currentSpeed)
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
        public void MeterLeftToRun(MKMapView map, CLLocation pointA,
            UIKit.UILabel LeftOnRunlabel, double LengthOfTheRun)
        {
            CLLocation pointB = new CLLocation(map.UserLocation.Location.Coordinate.Latitude,
                                               map.UserLocation.Location.Coordinate.Longitude);

            var distanceToB = pointB.DistanceFrom(pointA);


            //The connection an the information to the server.
            string text = "You have left" + distanceToB;
            UserClient.Connect("10.0.1.5", text);



            Console.WriteLine("THIS IS THE Destination between A and B" + distanceToB.ToString());
            Console.WriteLine("INTERNET IS ON" + UserClient.CheckConnectivity());


            if (LengthOfTheRun <= distanceToB)
            {
                Console.WriteLine("END OF RUN");
                audioManager.PlaySound("Finished.mp3");
            }
            double length = Math.Round(distanceToB, 2);
            Console.WriteLine(LengthOfTheRun - distanceToB);
            LeftOnRunlabel.Text = "YOU HAVE RUN: " + length.ToString();
        }

        #endregion
    }
}