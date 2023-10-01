using UnityEngine;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        public void SetCamera(int columnCount, int rowCount)
        {
            transform.position = new Vector3((columnCount -1)/2f,(rowCount -1)/2f,-10);
            _camera.orthographicSize = (columnCount / 2f) * 1.2f;
        }
    }
}