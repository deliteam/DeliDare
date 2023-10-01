using System;
using UnityEngine;

namespace DefaultNamespace
{
    public enum DragDecision
    {
        None,
        Left,
        Right,
        Up,
        Down
    }

    public class Tile : MonoBehaviour
    {
        private static int staticId = 0;
        private Vector3 _lastInputPos;
        private Vector3 _startPos;
        private bool _isDragging;
        private DragDecision _dragDecision = DragDecision.None;
        private AvailableMovements _availableMovements;
        public int Id { private set; get; }
        public int CurrentCol { private set; get; }
        public int CurrentRow { private set; get; }
        private int _realCol;
        private int _realRow;
        public Action<Tile, int, int,int,int> OnTilePosChange;

        public bool IsInPlace()
        {
            if (CurrentCol == _realCol && _realRow == CurrentRow)
            {
                return true;
            }

            return false;
        }

        public void Initialize(int realCol, int realRow)
        {
            _realCol = realCol;
            _realRow = realRow;
            Id = staticId;
            staticId++;
        }

        public void SetCurrentIndex(int currentCol, int currentRow)
        {
            CurrentCol = currentCol;
            CurrentRow = currentRow;
        }

        public void StartDrag(AvailableMovements availableMovements)
        {
            _availableMovements = availableMovements;
            _isDragging = true;
            _lastInputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _startPos = transform.position;
        }

        public static void ClearStatic()
        {
            staticId = 0;
        }

        private void Update()
        {
            if (!_isDragging)
            {
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector3 pos = transform.position;
                int col = Mathf.RoundToInt(pos.x);
                int row = Mathf.RoundToInt(pos.y);
                transform.position = new Vector3(col, row, pos.z);
                OnTilePosChange?.Invoke(this,CurrentCol,CurrentRow,col,row);
                SetCurrentIndex(col,row);
                _isDragging = false;
            }

            if (_availableMovements == AvailableMovements.None)
            {
                return;
            }

            if (Input.GetMouseButton(0))
            {
                // Update the object's position based on the mouse position
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 deltaPos = mousePosition - _lastInputPos;
                
                //Clamp mousePos
                if (_availableMovements == AvailableMovements.Down)
                {
                    deltaPos.y = Mathf.Clamp(deltaPos.y, -1, 0);
                    deltaPos.x = 0;
                }
                else if (_availableMovements == AvailableMovements.Up)
                {
                    deltaPos.y = Mathf.Clamp(deltaPos.y, 0, 1);
                    deltaPos.x = 0;
                }
                else if (_availableMovements == AvailableMovements.Left)
                {
                    deltaPos.y = 0;
                    deltaPos.x = Mathf.Clamp(deltaPos.x, -1, 0);
                }
                else if (_availableMovements == AvailableMovements.Right)
                {
                    deltaPos.y = 0;
                    deltaPos.x = Mathf.Clamp(deltaPos.x, 0, 1);
                }

                transform.position = new Vector3(_startPos.x + deltaPos.x, _startPos.y + deltaPos.y,
                    _startPos.z);
            }
        }
    }
}