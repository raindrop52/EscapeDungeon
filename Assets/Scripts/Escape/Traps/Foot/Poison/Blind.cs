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

            // ���� �þ� ������ ( ���� �� ���� ������ 0 ���� �� ���� ���� )
            if (_originLight <= 0.0f)
                _originLight = _pLight.GetLightOuterRadius();
            // �÷��̾ �ߵ� �þ� ����
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