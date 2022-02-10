using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class DamageText : MonoBehaviour
    {
        public float _duration = 0.5f;      // ��� �ð�
        public float _offsetY = 0.2f;
        public int _damage = 0;

        Text _text;        
        MapObject _owner;       // ������ ����


        public MapObject Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                _owner = value;
            }
        }

        void Start()
        {
            _text = GetComponent<Text>();

            StartCoroutine(_Play());
        }

        IEnumerator _Play()
        {
            _text.text = _damage.ToString();

            StartCoroutine(_AlphaFx());
            StartCoroutine(_ScaleFx());
            StartCoroutine(_MoveFx());

            yield return null;
        }

        IEnumerator _AlphaFx()
        {
            float elapsed = 0.0f;
            float duration1 = _duration * 0.3f;
            float duration2 = _duration * 0.4f;
            float duration3 = _duration * 0.3f;

            while (elapsed <= duration1)
            {
                elapsed += Time.deltaTime;

                Color c = _text.color;

                c.a = Mathf.Lerp(0.0f, 1.0f, elapsed / duration1);

                _text.color = c;

                yield return null;
            }

            yield return new WaitForSeconds(duration2);

            elapsed = 0.0f;
            while (elapsed <= duration3)
            {
                elapsed += Time.deltaTime;

                Color c = _text.color;

                c.a = Mathf.Lerp(1.0f, 0.0f, elapsed / duration3);

                _text.color = c;

                yield return null;
            }

            Destroy(gameObject);
        }

        IEnumerator _ScaleFx()
        {
            float elapsed = 0.0f;
            float duration1 = _duration * 0.3f;
            float duration2 = _duration * 0.4f;
            float duration3 = _duration * 0.3f;
            float scalsSize = 1.25f;

            while (elapsed <= duration1)
            {
                elapsed += Time.deltaTime;

                Vector3 scale = transform.localScale;

                float s = Mathf.Lerp(1.0f, scalsSize, elapsed / duration1);

                transform.localScale = new Vector3(s, s, s);

                yield return null;
            }

            yield return new WaitForSeconds(duration2);

            elapsed = 0.0f;
            while (elapsed <= duration3)
            {
                elapsed += Time.deltaTime;

                Vector3 scale = transform.localScale;

                float s = Mathf.Lerp(scalsSize, 1.0f, elapsed / duration3);

                transform.localScale = new Vector3(s, s, s);

                yield return null;
            }
        }

        IEnumerator _MoveFx()
        {
            float elapsed = 0.0f;
            float duration1 = _duration * 0.5f;
            float duration2 = _duration * 0.3f;
            float duration3 = _duration * 0.2f;

            while (elapsed <= duration1)
            {
                elapsed += Time.deltaTime;

                if (_owner != null)
                {
                    // �������� ��ġ�� �׻� ����ٴϰ�
                    Vector3 curPos = Camera.main.WorldToScreenPoint(_owner.transform.position);

                    transform.position = new Vector3(curPos.x, transform.position.y, curPos.z);
                }

                Vector3 pos = transform.localPosition;

                float delta = Mathf.Lerp(0f, 2.0f, elapsed / duration1);

                transform.localPosition = new Vector3(pos.x, pos.y + delta + _offsetY, pos.z);

                yield return null;
            }

            yield return new WaitForSeconds(duration2 + duration3);
        }
    }
}