using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class Player : MapObject
    {
        public ParticleSystem _ps_normal;
        BoxCollider2D _attackCol;

        public GameObject _hpBarObj;
        public Vector3 _hpBarOffset;

        [SerializeField] int _exp;

        protected override void Start()
        {
            base.Start();

            // ����� ����ġ �ҷ�����
            LoadExp();

            _ps_normal = transform.Find("Sword_Effect").GetComponent<ParticleSystem>();
            _attackCol = transform.Find("Sword_Effect").GetComponent<BoxCollider2D>();
            _attackCol.enabled = false;
            _hpBarImg = _hpBarObj.transform.GetChild(0).GetComponent<Image>();

            // ���� ��� �ڷ�ƾ
            StartCoroutine(_NormalAttack());
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            _hpBarObj.transform.position = pos + _hpBarOffset;
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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            {
                return;
            }

            OnHit(collision.collider);
        }

        void LoadExp()
        {
            if (PlayerPrefs.HasKey("survivor_exp"))
            {
                int exp = PlayerPrefs.GetInt("survivor_exp");
                _exp = exp;
            }
        }

        public void AddExp(int deltaExp)
        {
            _exp += deltaExp;

            PlayerPrefs.SetInt("survivor_exp", _exp);
        }
    }
}