using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class SettingsScreen : MonoBehaviour
    {
        public static SettingsScreen Instance;

        public GameObject settingsPanel;
        public Sprite soundOffIcon;
        public Sprite soundOnIcon;
        public Image soundImage;
        
        public bool IsPaused()
        {
            return settingsPanel.activeSelf;
        }

        private void Awake()
        {
            Instance = this;
            settingsPanel.SetActive(false);
        }

        public void OnSettingsButtonClicked()
        {
            if (WinScreen.Instance.IsWin())
            {
                settingsPanel.SetActive(false);
                return;
            }

            if (MusicController.Instance.IsMusicActive())
            {
                soundImage.sprite = soundOnIcon;
            }
            else
            {
                soundImage.sprite = soundOffIcon;
            }
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
        
        public void OnResumeButtonClicked()
        {
            settingsPanel.SetActive(false);
        }
        public void OnMusicButtonClicked()
        {
            MusicController.Instance.SetActiveMusic(!MusicController.Instance.IsMusicActive());
            if (MusicController.Instance.IsMusicActive())
            {
                soundImage.sprite = soundOnIcon;
            }
            else
            {
                soundImage.sprite = soundOffIcon;
            }
        }
        public void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        public void OnRestartButtonClicked()
        {
            BoardController.Instance.ShuffleTiles();
            settingsPanel.SetActive(false);
        }
    }
}