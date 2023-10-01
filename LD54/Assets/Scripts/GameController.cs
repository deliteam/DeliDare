using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private BoardController _boardController;
        private int _currentLevel = 1;
        private void Awake()
        {
            GetLevel();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GetNextLevel();
            }
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
        }

        private void GetNextLevel()
        {
            _currentLevel++;
            GetLevel();
        }
    }
}