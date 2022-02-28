using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Arrow_Trap : Trap_Foot
    {
        bool _shot = false;
        public bool IsShot
        { get { return _shot; } }
        bool _shooting = false;
        public bool IsShooting
        { get { return _shooting; } }
        Shooter[] _shooters;
        [SerializeField] float _secOnFireShooter = 1.0f;

        protected override void ExecuteTrap(GameObject playerObj)
        {
            if (_shot == false)
            {
                _shot = true;
            }
        }

        public void Init()
        {
            // 화살 발사 오브젝트 초기화
            _shooters = GetComponentsInChildren<Shooter>();
            if(_shooters != null)
            {
                foreach (Shooter shooter in _shooters)
                {
                    shooter.Init();
                }
            }

            OnShot();
        }

        public void OnShot()
        {
            StartCoroutine(_OnShot());
        }

        IEnumerator _OnShot()
        {
            while(true)
            {
                if(_shot)
                {
                    if (_shooting)
                    {
                        foreach (Shooter shooter in _shooters)
                        {
                            shooter.OnShot();

                            yield return new WaitForSeconds(_secOnFireShooter);
                        }
                    }
                    else
                        yield return null;
                }                
                else
                {
                    yield return null;
                }
            }
        }

        protected override void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if (_shooting == false)
                    _shooting = true;
            }
        }

        protected override void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if (_shooting == true)
                    _shooting = false;

                if (_shot == true)
                    _shot = false;
            }
        }
    }
}