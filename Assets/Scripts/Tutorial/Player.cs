using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class Player : MonoBehaviour
    {
        public ParticleSystem _ps_normal;
        BoxCollider2D _attackCol;

        void Start()
        {
            _ps_normal = transform.Find("Sword_Effect").GetComponent<ParticleSystem>();
            _attackCol = transform.Find("Sword_Effect").GetComponent<BoxCollider2D>();
            _attackCol.enabled = false;

            // ���� ��� �ڷ�ƾ
            StartCoroutine(_NormalAttack());
        }

        IEnumerator _NormalAttack()
        {
            float maxTime = 1.0f;
            float lifeTime = _ps_normal.main.duration;

            while (gameObject.activeSelf)
            {
                // ����Ʈ ���
                _ps_normal.Play();
                // �浹ü�� ����
                _attackCol.enabled = true;
                yield return new WaitForSeconds(lifeTime);
                // �浹ü�� ����
                _attackCol.enabled = false;
                yield return new WaitForSeconds(maxTime - lifeTime);
            }

            yield return null;
        }
    }
}