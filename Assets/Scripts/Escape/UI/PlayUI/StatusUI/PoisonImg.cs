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

        public void Init()
        {
            _img = GetComponent<Image>();
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

        public void OnShow(bool show)
        {
            gameObject.SetActive(show);
        }

        IEnumerator _OnTimer()
        {
            _time = 0.0f;

            while(_time < _maxTime)
            {
                _time += Time.fixedDeltaTime;

                if(_img != null)
                {
                    _img.fillAmount = (float)((_maxTime - _time) / _maxTime);
                }

                yield return null;
            }

            // �̹����� �� ���̰� �� ��� (��Ÿ���� �� �� ���)
            if(_img.fillAmount <= 0.0f)
            {
                // ������Ʈ ��Ȱ��ȭ
                OnShow(false);
            }
        }
    }
}