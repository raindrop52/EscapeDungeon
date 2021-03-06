using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class PoisonImg : MonoBehaviour
    {
        float _time = 0.0f;
        float _maxTime = 0.0f;
        Image _img;
        Text _text;
        bool _reset = false;
        public bool Reset
        { set { _reset = value; } }

        public void Init()
        {
            _img = GetComponent<Image>();
            _text = GetComponentInChildren<Text>();
        }

        public void SetImage(Sprite sprite)
        {
            _img.sprite = sprite;
        }

        public void OnTimer(float maxTime)
        {
            _maxTime = maxTime;
            StartCoroutine(_OnTimer());
        }

        public void Disappear()
        {
            Destroy(gameObject);
        }

        IEnumerator _OnTimer()
        {
            _time = 0.0f;

            while(_time < _maxTime)
            {
                _time += Time.fixedUnscaledDeltaTime;

                if(_reset == true)
                {
                    _reset = false;
                    _time = 0.0f;
                    continue;
                }

                if(_img != null)
                {
                    _img.fillAmount = (float)((_maxTime - _time) / _maxTime);
                }

                if(_text != null)
                {
                    _text.text = string.Format("{0}", (int)(_maxTime - _time));
                }

                yield return null;
            }

            // 이미지가 안 보이게 된 경우 (쿨타임이 다 찬 경우)
            if(_img.fillAmount <= 0.0f)
            {
                // 오브젝트 제거
                Disappear();
            }
        }
    }
}