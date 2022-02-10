using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class MapObject : MonoBehaviour
    {
        Rigidbody2D _rigid;
        SpriteRenderer _render;
        float _hp;
        float _maxHp;

        void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _render = GetComponent<SpriteRenderer>();

            _hp = _maxHp;
        }

        
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Attack")
            {
                if(_rigid != null && _render != null)
                {
                    float direction = 1.0f;
                    float power = 200.0f;

                    // 공격자의 위치
                    Transform attackerTrans = collision.transform.parent;
                    Vector2 attackerPos = attackerTrans.position;
                    Vector2 myPos = transform.position;

                    // 내 위치가 공격자보다 왼쪽에 있을 때
                    if (myPos.x < attackerPos.x)
                        direction = -1.0f;
                    // 내 위치가 공격자보다 오른쪽에 있을 때
                    else if (myPos.x >= attackerPos.x)
                        direction = 1.0f;

                    // 방향에 맞게 밀리도록
                    _rigid.AddForce(new Vector2(power * direction, 0.0f));

                    Invoke("StopForce", 0.2f);

                    // 데미지 처리
                    int damage = 10;
                    _hp -= damage;

                    // 데미지 텍스트 연출
                    GameObject damageTextObj = Instantiate(UIManager_Tutorial._inst._damageTextPrefab);
                    damageTextObj.transform.parent = UIManager_Tutorial._inst.transform;

                    Vector3 startPos = Camera.main.WorldToScreenPoint(transform.position);

                    damageTextObj.transform.position = startPos;

                    DamageText damageTxt = damageTextObj.GetComponent<DamageText>();
                    damageTxt.Owner = this;
                    damageTxt._damage = damage;

                    if(_hp <= 0.0f)
                    {
                        Die();
                    }
                    
                }
            }
        }

        void Die()
        {
            
        }

        void StopForce()
        {
            if(_rigid != null)
                _rigid.velocity = Vector2.zero;
        }
    }
}