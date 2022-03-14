using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class Tutorial_Key : MonoBehaviour
    {
        [SerializeField] List<Sprite> _keyImgs;
        
        Image _img;
        
        public void Init()
        {
            _img = GetComponent<Image>();
            OnShow(false);
        }

        public void OnShow(bool show)
        {
            gameObject.SetActive(show);
        }

        public void ChangeImg(int i)
        {
            int index = i % _keyImgs.Count;

            _img.sprite = _keyImgs[index];
        }
    }
}