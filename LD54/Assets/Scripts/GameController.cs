using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        public string[] levelTexts;
        public string[] winTexts;
        
        public static GameController Instance;
        [SerializeField] private BoardController _boardController;
        [SerializeField] private ReferenceScreen _referenceScreen;
        [SerializeField] private TextMeshProUGUI _levelName;
        private int _currentLevel = 1;
        private void Awake()
        {
            Instance = this;
            GetLevel();
        }

        private void GetLevel()
        {
            Texture2D tex = Resources.Load<Texture2D>($"Levels/{_currentLevel}");
            if (tex == null)
            {
                _currentLevel = 1;
            }
            tex = Resources.Load<Texture2D>($"Levels/{_currentLevel}");
            _boardController.Setup(tex);
            _referenceScreen.SetReferenceImage(tex);
            MusicController.Instance.StartMusic(_currentLevel);
            _levelName.text = $"LEVEL: {levelTexts[_currentLevel - 1]}";
        }

        public string GetWinText()
        {
            return winTexts[_currentLevel - 1];
        }

        public void GetNextLevel()
        {
            _currentLevel++;
            GetLevel();
        }
    }
}