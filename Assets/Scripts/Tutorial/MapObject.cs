using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class MapObject : MonoBehaviour
    {
        Rigidbody2D _rigid;
        SpriteRenderer _render;

        void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _render = GetComponent<SpriteRenderer>();
        }

        
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Attack")
            {
                Debug.Log(collision.name + "���� �¾Ҵ�");

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
                }
            }
        }

        void StopForce()
        {
            if(_rigid != null)
                _rigid.velocity = Vector2.zero;
        }
    }
}