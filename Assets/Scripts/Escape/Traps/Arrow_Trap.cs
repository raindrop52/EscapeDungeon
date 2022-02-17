using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Arrow_Trap : Trap
    {
        bool _shot = false;
        public bool IsShot
        { get { return _shot; } set { _shot = value; } }
        Shooter[] _shooters;
        [SerializeField] float _secOnFireShooter = 1.0f;

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
        }

        public void OnShot()
        {
            StartCoroutine(_OnShot());
        }

        IEnumerator _OnShot()
        {
            while(_shot)
            {
                foreach(Shooter shooter in _shooters)
                {
                    shooter.OnShot();

                    yield return new WaitForSeconds(_secOnFireShooter);
                }
            }
        }
    }
}