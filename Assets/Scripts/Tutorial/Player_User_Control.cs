using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Tutorial
{
    public class Player_User_Control : MonoBehaviour
    {
        Rigidbody2D _rigid;
        Animator _anim;
        SpriteRenderer _render;
        Player _player;
        [SerializeField] float _speedMultiplier = 1.0f;

        void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _render = GetComponent<SpriteRenderer>();
            _player = GetComponent<Player>();
        }

        void Update()
        {

        }

        private void FixedUpdate()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            Flip(h);

            Move(h, v);

            //float test = Mathf.PerlinNoise(h, v);
            //Debug.Log(string.Format("펄린 노이즈 Value : ({0}, {1}) ({2})", h, v, test));
        }

        void Flip(float h)
        {
            if (h < 0f && _render.flipX == false)
            {
                // 캐릭터 플립 처리
                _render.flipX = true;
                // 이펙트 플립 처리
                Vector3 scale = _player._ps_normal.transform.localScale;
                scale.x *= -1;
                ParticleSystem[] psList = GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem ps in psList)
                {
                    ps.transform.localScale = scale;
                }
            }
            else if (h > 0f && _render.flipX == true)
            {
                // 캐릭터 플립 처리
                _render.flipX = false;
                // 이펙트 플립 처리
                Vector3 scale = _player._ps_normal.transform.localScale;
                scale.x *= -1;

                ParticleSystem[] psList = GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem ps in psList)
                {
                    ps.transform.localScale = scale;
                }
            }
        }

        void Move(float h, float v)
        {
            if (h != 0f || v != 0f)
                _anim.SetFloat("Speed", 1.0f);
            else
                _anim.SetFloat("Speed", 0f);

            _rigid.velocity = new Vector2(h * _speedMultiplier, v * _speedMultiplier);
        }
    }
}