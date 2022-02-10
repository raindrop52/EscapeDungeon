using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class MapObject : MonoBehaviour
    {
        Rigidbody2D _rigid;
        SpriteRenderer _render;
        Animator _anim;
        float _hp = 0.0f;
        float _maxHp = 10.0f;

        void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _render = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();

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

                    // �������� ��ġ
                    Transform attackerTrans = collision.transform.parent;
                    Vector2 attackerPos = attackerTrans.position;
                    Vector2 myPos = transform.position;

                    // �� ��ġ�� �����ں��� ���ʿ� ���� ��
                    if (myPos.x < attackerPos.x)
                        direction = -1.0f;
                    // �� ��ġ�� �����ں��� �����ʿ� ���� ��
                    else if (myPos.x >= attackerPos.x)
                        direction = 1.0f;

                    // ���⿡ �°� �и�����
                    _rigid.AddForce(new Vector2(power * direction, 0.0f));

                    Invoke("StopForce", 0.2f);

                    // ������ ó��
                    int damage = 10;
                    _hp -= damage;

                    // ������ �ؽ�Ʈ ����
                    GameObject damageTextObj = Instantiate(UIManager_Tutorial._inst._damageTextPrefab);
                    damageTextObj.transform.parent = UIManager_Tutorial._inst.transform;

                    Vector3 startPos = Camera.main.WorldToScreenPoint(transform.position);

                    damageTextObj.transform.position = startPos;

                    DamageText damageTxt = damageTextObj.GetComponent<DamageText>();
                    damageTxt.Owner = this;
                    damageTxt._damage = damage;

                    if(_hp <= 0.0f)
                    {
                        StartCoroutine(_Die());
                    }
                    
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