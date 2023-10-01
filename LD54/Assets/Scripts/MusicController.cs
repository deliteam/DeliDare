using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public enum SfxName
    {
        Win,
        Click,
        Drop
    }
    public class MusicController : MonoBehaviour
    {
        public static MusicController Instance;
        public AudioClip bg1;
        public AudioClip bg2;
        public AudioClip bg3;
        public AudioClip winSound;
        public AudioClip clickSound;
        public AudioClip dropSound;
        
        public AudioSource sfxSource;
        public AudioSource musicSouce;

        private float _currentMusicVolume = 1;
        private float _currentSfxVolume = 1;

        private void Awake()
        {
            Instance = this;
        }

        public bool IsMusicActive()
        {
            return !sfxSource.mute;
        }
        
        public void SetActiveMusic(bool isActive)
        {
            sfxSource.mute = !isActive;
            musicSouce.mute = !isActive;
        }
        
        public void SetSfxVolume(float musicVolume)
        {
            _currentSfxVolume = musicVolume;
            sfxSource.volume = musicVolume;
        }

        public void SetMusicVolume(float musicVolume)
        {
            _currentMusicVolume = musicVolume;
            musicSouce.volume = musicVolume;
        }

        public void StartMusic(int level)
        {
            AudioClip toPlay = null;

            if (level > 3)
            {
                level = Random.Range(1, 4);
            }

            toPlay = level switch
            {
                1 => bg1,
                2 => bg2,
                _ => bg3
            };

            musicSouce.clip = toPlay;
            musicSouce.loop = true; // Set this to true if you want the music to loop.
            musicSouce.Play();
        }

        public void StartSfx(SfxName sfxName)
        {
            AudioClip toPlay = null;
            toPlay = sfxName switch
            {
                SfxName.Win => winSound,
                SfxName.Click => clickSound,
                SfxName.Drop => dropSound,
            };
            sfxSource.clip = toPlay;
            sfxSource.Play();
        }
    }
}