using UnityEngine;

namespace DefaultNamespace
{
    public class WinScreen : MonoBehaviour
    {
        public static WinScreen Instance;
        public GameObject winPanel;
        private void Awake()
        {
            Instance = this;
            winPanel.SetActive(false);
        }

        public bool IsWin()
        {
            return winPanel.activeSelf;
        }

        public void ShowWinScreen()
        {
            winPanel.SetActive(true);
            SettingsScreen.Instance.OnSettingsButtonClicked();
        }
        
        public void OnWinButtonClicked()
        {
            GameController.Instance.GetNextLevel();
            winPanel.SetActive(false);
        }
        
        public void OnRestartButtonClicked()
        {
            BoardController.Instance.ShuffleTiles();
            winPanel.SetActive(false);
        }
    }
}