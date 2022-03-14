using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class Tutorial_Key : MonoBehaviour
    {
        [SerializeField] List<Sprite> _keyImgs;
        [SerializeField] Image _fxImg;
        Image _img;

        public void Init()
        {
            _img = GetComponent<Image>();

            FxShow(false);

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

            if (index == 0)
                FxShow(false);
            else if (index == 1)
                FxShow(true);
        }

        void FxShow(bool show)
        {
            if (_fxImg != null)
                _fxImg.gameObject.SetActive(show);
        }
    }
}