using AVFoundation;
using Foundation;

namespace IntervallRun
{
    public class AudioManager
    {
        #region Private Variables
        private AVAudioPlayer backgroundMusic;
        private AVAudioPlayer soundEffect;
        private string backgroundSong = "";
        private AVAudioPlayer audio = null;
        #endregion


        #region Computed Properties
        public bool MusicOn { get; set; } = true;
        public float MusicVolume { get; set; } = 0.5f;
        #endregion

        #region Constructor
        public AudioManager()
        {
            ActivateAudioSession();
        }
        #endregion

        #region Public Methods
        public void ActivateAudioSession()
        {
            var session = AVAudioSession.SharedInstance();
            session.SetCategory(AVAudioSessionCategory.Ambient);
            session.SetActive(true);
        }

        public void DeactivateAudioSession()
        {
            var session = AVAudioSession.SharedInstance();
            session.SetActive(false);
        }

        public void ReactivateAudioSession()
        {
            var session = AVAudioSession.SharedInstance();
            session.SetActive(true);
        }

        public void PlaySound(string fileName)
        {
           
            NSUrl songUrl;

            //Music enabled?
            if (!MusicOn)
                return;
            if(audio != null)
            {
                audio.Stop();
                audio.Dispose();
            }

            songUrl = new NSUrl("Sounds/" + fileName);
            NSError err;
            audio = new AVAudioPlayer(songUrl, "mp3", out err)
            {
                Volume = MusicVolume
            };
            audio.FinishedPlaying += delegate 
            {
                audio = null;
            };
            audio.NumberOfLoops = 0;
            audio.Play();
        }

        #endregion
    }
}