using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class Poison : MonoBehaviour
    {
        float _time = 0.0f;
        public float UsingTime
        { get { return _time; } set { _time = value; } }
        public float _poisonTime = 3.0f;
        public PoisonFx _fx;
        bool _isPosioning = false;
        protected Player _target;
        Callback _cb;

        public virtual void OnPoison(Player player, Callback cb)
        {
            _cb = cb;

            _target = player;

            ExecutePoison();
        }

        protected virtual void ExecutePoison()
        {
            if (_isPosioning == false)
            {
                _isPosioning = true;
                // �ߵ� �ڷ�ƾ ����
                StartCoroutine(_Poisoning());
            }
        }

        protected virtual void ClosePoison()
        {
            // �ߵ� ���� ���� �� ���� ���� ����
            if (_isPosioning == true)
                _isPosioning = false;
        }

        IEnumerator _Poisoning()
        {
            _time = 0.0f;

            // �ߵ� ���� �ð�
            while (_time < _poisonTime)
            {
                if (GameManager._inst.Die == true)
                    break;

                _time += Time.fixedDeltaTime;

                yield return null;
            }

            ClosePoison();

            if (_cb != null)
                _cb();
        }
    }
}