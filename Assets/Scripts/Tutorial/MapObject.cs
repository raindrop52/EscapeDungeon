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
        bool _die = false;

        protected virtual void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _render = GetComponent<SpriteRenderer>();
            _anim = GetComponent<Animator>();
            
            _hp = _maxHp;

            if (_hpBarImg != null)
                _hpBarImg.fillAmount = _hp / _maxHp;
        }


        protected virtual void FixedUpdate()
        {
            
        }

        protected void OnHit(Collider2D collision)
        {
            MapObject attacker = null;
            Transform parent = collision.transform.parent;
            if (parent != null)
            {
                attacker = parent.GetComponent<MapObject>();
            }
            else
            {
                //attacker = GetComponent<MapObject>();
                attacker = collision.GetComponent<MapObject>();
            }

            if (_rigid != null && attacker != null)
            {
                float direction = 1.0f;
                float power = 200.0f;

                // �������� ��ġ
                Vector3 attackerPos = attacker.transform.position;
                Vector3 myPos = transform.position;

                // �� ��ġ�� �����ں��� ���ʿ� ���� ��
                if (myPos.x < attackerPos.x)
                    direction = -1.0f;
                // �� ��ġ�� �����ں��� �����ʿ� ���� ��
                else if (myPos.x >= attackerPos.x)
                    direction = 1.0f;

                // ������ ó��
                int damage = 10;
                _hp -= damage;

                if (attacker is Player)
                {
                    // ���⿡ �°� �и�����
                    _rigid.AddForce(new Vector2(power * direction, 0.0f));

                    Invoke("StopForce", 0.2f);
                }
                else
                {
                    if (_hpBarImg != null)
                        _hpBarImg.fillAmount = _hp / _maxHp;
                }

                // ������ �ؽ�Ʈ ����
                GameObject damageTextObj = Instantiate(UIManager_Tutorial._inst._damageTextPrefab);
                damageTextObj.transform.parent = UIManager_Tutorial._inst.transform;

                Vector3 startPos = Camera.main.WorldToScreenPoint(transform.position);

                damageTextObj.transform.position = startPos;

                DamageText damageTxt = damageTextObj.GetComponent<DamageText>();
                damageTxt.Owner = this;
                damageTxt._damage = damage;

                if (_hp <= 0.0f)
                {
                    if(_die == false)
                        StartCoroutine(_Die());
                }
            }
        }

        IEnumerator _Die()
        {
            _die = true;

            float duration = 0.3f;
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

            if(this is Player)
            {
                // ���� ���� â ǥ��
            }
            else
            {
                // ����ġ ������ ���
                DropItem();

                Destroy(gameObject);
            }
        }

        void StopForce()
        {
            if(_rigid != null)
                _rigid.velocity = Vector2.zero;
        }

        void DropItem()
        {
            GameObject gem = Instantiate(GameManager._inst._expGemPrefab);
            gem.transform.position = transform.position;
            gem.transform.localScale = new Vector3(2f, 2f, 2f);
        }
    }
}