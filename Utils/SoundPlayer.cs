using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace RythmGame.Utils
{
    public static class SoundPlayer
    {
        private static float masterVolume;
        private static float musicVolume;
        private static float effectsVolume;

        public enum SoundEffects {
            StepHit = 0,
            TrackFailed = 1
        };

        private static Dictionary<SoundEffects, SoundEffect> soundEffects;
        private static Dictionary<string, Song> songs;

        public static void Initialize()
        {
            soundEffects = new Dictionary<SoundEffects, SoundEffect>();
            songs = new Dictionary<string, Song>();

            masterVolume = UserPrefs.Settings.MasterVolume;
            musicVolume = UserPrefs.Settings.MusicVolume;
            effectsVolume = UserPrefs.Settings.EffectsVolume;

            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = masterVolume * musicVolume;
        }

        public static void LoadContent()
        {
            AssetManager.GetSoundEffect("slimeSplash");
            soundEffects.Add(SoundEffects.StepHit, AssetManager.GetSoundEffect("slimeSplash"));
            soundEffects.Add(SoundEffects.TrackFailed, AssetManager.GetSoundEffect("rocketExplosionSound"));
            songs.Add("gods", AssetManager.GetSong("gods"));
        }

        public static void PlayEffect(SoundEffects effectName)
        {
            soundEffects[effectName].Play(masterVolume * effectsVolume, 0.0f, 0.0f);
        }

        public static void PlaySong(string songName)
        {
            MediaPlayer.Play(songs[songName]);
        }

        public static void StopSong()
        {
            MediaPlayer.Stop();
        }
    }
}
