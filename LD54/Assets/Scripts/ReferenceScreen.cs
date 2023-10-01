using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ReferenceScreen : MonoBehaviour
    {
        [SerializeField] private Image _refImage;

        private void Update()
        {
            if (Input.GetKey(KeyCode.C))
            {
                _refImage.gameObject.SetActive(true);
            }
            else
            {
                _refImage.gameObject.SetActive(false);
            }
        }

        public void SetReferenceImage(Texture2D texture)
        {
            _refImage.sprite = Sprite.Create(texture,new Rect(0,0,texture.width,texture.height), new Vector2(0.5f,0.5f),100);
        }
    }
}