using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class MapObject : MonoBehaviour
    {
        Rigidbody2D _rigid;
        SpriteRenderer _render;
        Animator _anim;
        public float _hp = 0.0f;
        public float _maxHp = 10.0f;
        public Image _hpBarImg;

        protected virtual void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _render = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();
            
            _hp = _maxHp;

            if (_hpBarImg != null)
                _hpBarImg.fillAmount = _hp / _maxHp;
        }

        
        protected virtual void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag != "Attack")
                return;

            MapObject attacker = null;            
            Transform parent = collision.transform.parent;
            if (parent != null)
            {
                attacker = parent.GetComponent<MapObject>();
            }

            if (_rigid != null && attacker != null)
            {
                float direction = 1.0f;
                float power = 200.0f;

                // 공격자의 위치
                Vector3 attackerPos = attacker.transform.position;
                Vector3 myPos = transform.position;

                // 내 위치가 공격자보다 왼쪽에 있을 때
                if (myPos.x < attackerPos.x)
                    direction = -1.0f;
                // 내 위치가 공격자보다 오른쪽에 있을 때
                else if (myPos.x >= attackerPos.x)
                    direction = 1.0f;

                // 데미지 처리
                int damage = 10;
                _hp -= damage;

                if (attacker is Player)
                {
                    // 방향에 맞게 밀리도록
                    _rigid.AddForce(new Vector2(power * direction, 0.0f));

                    Invoke("StopForce", 0.2f);
                }
                else
                {
                    if (_hpBarImg != null)
                        _hpBarImg.fillAmount = _hp / _maxHp;
                }

                // 데미지 텍스트 연출
                GameObject damageTextObj = Instantiate(UIManager_Tutorial._inst._damageTextPrefab);
                damageTextObj.transform.parent = UIManager_Tutorial._inst.transform;

                Vector3 startPos = Camera.main.WorldToScreenPoint(transform.position);

                damageTextObj.transform.position = startPos;

                DamageText damageTxt = damageTextObj.GetComponent<DamageText>();
                damageTxt.Owner = this;
                damageTxt._damage = damage;

                if (_hp <= 0.0f)
                {
                    StartCoroutine(_Die());
                }
            }
        }

        IEnumerator _Die()
        {
            float duration = 0.5f;
            float elapsed = 0.0f;

            _anim.SetBool("Die", true);

            while(elapsed <= duration)
            {
                elapsed += Time.deltaTime;

                Color c = _render.color;

                c.r = Mathf.PingPong(Time.time * 15f, 1.0f);
                c.g = 0f;
                c.b = 0f;

                _render.color = c;

                yield return null;
            }

            Destroy(gameObject);
        }

        void StopForce()
        {
            if(_rigid != null)
                _rigid.velocity = Vector2.zero;
        }
    }
}