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

        public static void ContinueSong()
        {
            MediaPlayer.Resume();
        }

        public static void LoadContent()
        {
            soundEffects = new Dictionary<SoundEffects, SoundEffect>();
            songs = new Dictionary<string, Song>();

            soundEffects.Add(SoundEffects.StepHit, AssetManager.GetSoundEffect("hitSound2"));
            soundEffects.Add(SoundEffects.TrackFailed, AssetManager.GetSoundEffect("gameOver"));
            songs.Add("gods", AssetManager.GetSong("gods"));
            songs.Add("riverFlowsInYou", AssetManager.GetSong("riverFlowsInYou"));
            songs.Add("mainMenuMusic", AssetManager.GetSong("mainMenuMusic"));

            masterVolume = UserPrefs.Settings.MasterVolume;
            musicVolume = UserPrefs.Settings.MusicVolume;
            effectsVolume = UserPrefs.Settings.EffectsVolume;

            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = masterVolume * musicVolume;
        }

        public static void PauseSong()
        {
            MediaPlayer.Pause();
        }

        public static void PlayEffect(SoundEffects effectName, float volumeModifier = 1f, float pitch = 0f, float pan = 1f)
        {
            soundEffects[effectName].Play(masterVolume * effectsVolume * volumeModifier, pitch, pan);
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
