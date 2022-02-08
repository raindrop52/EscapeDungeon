using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace EscapeGame
{
    public class Player_User_Control : MonoBehaviour
    {
        Rigidbody2D _rigid;
        Animator _anim;
        SpriteRenderer _render;
        [SerializeField] float _speedMultiplier = 1.0f;

        void Start()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _render = GetComponent<SpriteRenderer>();
        }

        void Update()
        {

        }

        private void FixedUpdate()
        {
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            Move(h, v);
        }

        void Move(float h, float v)
        {
            if (h < 0)
                _render.flipX = true;
            else if (h > 0)
                _render.flipX = false;

            if (h != 0f || v != 0f)
                _anim.SetFloat("Speed", 1.0f);
            else
                _anim.SetFloat("Speed", 0f);

            _rigid.velocity = new Vector2(h * _speedMultiplier, v * _speedMultiplier);
        }
    }
}