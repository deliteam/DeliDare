using UnityEngine;

namespace DefaultNamespace
{
    public class Tile: MonoBehaviour
    {
        private Vector3 offset;
        private bool _isDragging;
        public void Initialize()
        {
            
        }

        public void StartDrag()
        {
            _isDragging = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        private void Update()
        {
            if (!_isDragging)
            {
                return;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
            }
            if (Input.GetMouseButton(0))
            {
                // Update the object's position based on the mouse position
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
            }
        }

    }
}