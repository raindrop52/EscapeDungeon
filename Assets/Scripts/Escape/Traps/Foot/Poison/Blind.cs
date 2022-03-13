using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Blind : Poison
    {
        float _originLight = 0.0f;
        [SerializeField] float _blindLight = 1.5f;
        PlayerLight _pLight;

        protected override void ExecutePoison()
        {
            if (_pLight == null)
                _pLight = _target.GetComponentInChildren<PlayerLight>();

            // 원래 시야 가져옴 ( 원본 값 저장 변수가 0 이하 일 때만 동작 )
            if (_originLight <= 0.0f)
                _originLight = _pLight.GetLightOuterRadius();
            // 플레이어에 중독 시야 설정
            _pLight.SetLightOuterRadius(_blindLight);

            base.ExecutePoison();
        }

        protected override void ClosePoison()
        {
            base.ClosePoison();

            if( _originLight > 0.0f)
            {
                _pLight.SetLightOuterRadius(_originLight);

                _originLight = 0.0f;
            }
        }
    }
}