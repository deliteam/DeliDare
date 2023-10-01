using UnityEngine;

namespace DefaultNamespace
{
    public class SettingsScreen : MonoBehaviour
    {
        public static SettingsScreen Instance;

        public GameObject settingsPanel;

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
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }
        
        public void OnResumeButtonClicked()
        {
            settingsPanel.SetActive(false);
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