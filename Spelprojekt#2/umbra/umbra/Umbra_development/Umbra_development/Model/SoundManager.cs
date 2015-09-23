    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Timers;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Media;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    namespace Umbra_development.Model
{


      public  class SoundManager
        {
            Dictionary<int, SoundEffect> soundCollection = new Dictionary<int, SoundEffect>();
            Dictionary<int, SoundEffect> soundsLevel1 = new Dictionary<int, SoundEffect>();
            Dictionary<int, SoundEffect> soundsLevel2 = new Dictionary<int, SoundEffect>();
            Dictionary<int, SoundEffect> soundsLevel3 = new Dictionary<int, SoundEffect>();
            Random rng = new Random();
            private float tenSec = 10;
            private float s_timer = 0;
            private bool timerActive = false;
            SoundEffectInstance windSound;
            SoundEffectInstance rainSound;
            SoundEffectInstance heartBeatSound;
            SoundEffectInstance currentRandomSound;
            SoundEffectInstance sewerDrop;

            public SoundManager(ContentManager a_cManager)
            {
                //LoadSoundContent(a_cManager, soundCollection);
                LoadSoundContent(a_cManager, soundsLevel1, soundsLevel2, soundsLevel3);
                windSound = a_cManager.Load<SoundEffect>("Wind").CreateInstance();
                rainSound = a_cManager.Load<SoundEffect>("RainThunder").CreateInstance();
                heartBeatSound = a_cManager.Load<SoundEffect>("heartBeat").CreateInstance();
                sewerDrop = a_cManager.Load<SoundEffect>("sewerDrop").CreateInstance();
                windSound.IsLooped = true;
                rainSound.IsLooped = true;
                sewerDrop.IsLooped = true;
            }
          //LADDAR FILER OCH PLACAERAR DEM BEROENDE PÅ VILKEN BANA
            internal void LoadSoundContent(ContentManager a_cManager, Dictionary<int, SoundEffect> a_soundsLevel1, Dictionary<int, SoundEffect> a_soundsLevel2, Dictionary<int, SoundEffect> a_soundsLevel3)
            {
                DirectoryInfo dir_level1 = new DirectoryInfo(a_cManager.RootDirectory + "/Sounds_Level1");
                DirectoryInfo dir_level2 = new DirectoryInfo(a_cManager.RootDirectory + "/Sounds_Level2");
                DirectoryInfo dir_level3 = new DirectoryInfo(a_cManager.RootDirectory + "/Sounds_Level3");

                FileInfo[] level1_files = dir_level1.GetFiles();
                FileInfo[] level2_files = dir_level2.GetFiles();
                FileInfo[] level3_files = dir_level3.GetFiles();
                string soundFolder_1 = "Sounds_Level1/";
                string soundFolder_2 = "Sounds_Level2/";
                string soundFolder_3 = "Sounds_Level3/";
                LoadSoundFiles(a_cManager, a_soundsLevel1, level1_files, soundFolder_1);
                LoadSoundFiles(a_cManager, a_soundsLevel2, level2_files, soundFolder_2);
                LoadSoundFiles(a_cManager, a_soundsLevel3, level3_files, soundFolder_3);

            }

            public static Dictionary<int, SoundEffect> LoadSoundFiles(ContentManager a_cManager, Dictionary<int, SoundEffect> a_soundCollection, FileInfo[] a_files, string a_soundFolder)
            {
                SoundEffect soundE;
                int soundId = 0;
                foreach (FileInfo file in a_files)
                {
                    string soundFile = Path.GetFileNameWithoutExtension(file.Name);
                    soundE = a_cManager.Load<SoundEffect>(a_soundFolder + soundFile);
                    a_soundCollection.Add(soundId, soundE);
                    soundId++;
                }
                return a_soundCollection;
            }

            public Dictionary<int, SoundEffect> SoundCollection
            {
                get { return soundCollection; }
                set { soundCollection = value; }
            }

            public Dictionary<int, SoundEffect> GetSoundCollection(int a_level)
            {
                switch (a_level)
                {
                    case 1:
                        SoundCollection = soundsLevel1;
                        break;
                    case 2:
                        SoundCollection = soundsLevel2;
                        break;
                    case 3:
                        SoundCollection = soundsLevel3;
                        break;
                    default:
                        break;
                }

                return SoundCollection;
            }
          //SPELAR UPP LJUDEN BEROENDE PÅ VILKEN BANA
            internal void PlayBackgroundSound(int a_level)
            {
                switch (a_level)
                {
                    case 1:
                        rainSound.Play();
                        rainSound.Volume = 0.4f;
                        break;
                    case 2:
                        sewerDrop.Play();
                        sewerDrop.Volume = 0.4f;
                        break;
                    case 3:
                        windSound.Play();
                        windSound.Volume = 0.4f;
                        break;
                    default:
                        return;
                }
                
                
            }
          //KOLLOAR NÄR HJÄRTLJUDEN SKALL SPELAS UPP
            internal void PlayHeartBeat()
            {
                heartBeatSound.Play();
                if (GetCurrentRandomSound() != null)
                {
                    GetCurrentRandomSound().Volume = 0.2f;
                }
            }
          //NÄR HJÄRTLJUDET SKALL STOPPAS
            internal void StopHeartBeat()
            {
                heartBeatSound.Stop();
            }
          //STOPPAR LJUD
            internal void StopSound()
            {
                windSound.Stop();
                rainSound.Stop();
                sewerDrop.Stop();
                if (GetCurrentRandomSound() != null)
                {
                    GetCurrentRandomSound().Stop();
                }
            }

            public SoundEffectInstance CurrentRandomSound
            {
                get { return currentRandomSound; }
                set { currentRandomSound = value; }
            }

            public SoundEffectInstance GetCurrentRandomSound()
            {
                return CurrentRandomSound;
            }


            internal void PlayRandomSound(Dictionary<int, SoundEffect> a_soundCollection)
            {
                int soundNumber = rng.Next(a_soundCollection.Count);
                foreach (KeyValuePair<int, SoundEffect> sound in a_soundCollection)
                {
                    if (sound.Key != soundNumber)
                    {
                        continue;
                    }
                    currentRandomSound = a_soundCollection[sound.Key].CreateInstance();
                    currentRandomSound.Play();
                    currentRandomSound.Volume = 0.8f;
                }
            }


            internal void StartSoundTimer()
            {
                timerActive = true;
            }

            internal void CountSoundTimer(float a_elapsedTime)
            {
                if (timerActive)
                {
                    s_timer += a_elapsedTime;
                }
            }

            internal void StopSoundTimer()
            {
                timerActive = false;
                tenSec = 10;
                s_timer = 0;
            }

            public float GetSoundTimer()
            {
                return s_timer;
            }

            
            internal bool SoundTimer(float s_timer)
            {
                int plusSec;
                if (s_timer > tenSec)
                {
                    plusSec = rng.Next(10, 20);
                    tenSec = s_timer + plusSec;
                    return true;
                }
                return false;
            }
        }
}
