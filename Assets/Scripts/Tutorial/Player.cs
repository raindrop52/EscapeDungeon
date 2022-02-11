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

        protected override void Start()
        {
            base.Start();

            _ps_normal = transform.Find("Sword_Effect").GetComponent<ParticleSystem>();
            _attackCol = transform.Find("Sword_Effect").GetComponent<BoxCollider2D>();
            _attackCol.enabled = false;
            _hpBarImg = _hpBarObj.transform.GetChild(0).GetComponent<Image>();

            // 공격 담당 코루틴
            StartCoroutine(_NormalAttack());
        }

        protected override void Update()
        {
            base.Update();

            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            _hpBarObj.transform.position = pos + _hpBarOffset;
        }

        IEnumerator _NormalAttack()
        {
            float maxTime = 1.0f;
            float lifeTime = _ps_normal.main.duration;

            while (gameObject.activeSelf)
            {
                // 이펙트 재생
                _ps_normal.Play();
                // 충돌체를 켜줌
                _attackCol.enabled = true;
                yield return new WaitForSeconds(lifeTime);
                // 충돌체를 꺼줌
                _attackCol.enabled = false;
                yield return new WaitForSeconds(maxTime - lifeTime);
            }

            yield return null;
        }
    }
}