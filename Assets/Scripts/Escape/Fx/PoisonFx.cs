using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class PoisonFx : MonoBehaviour
    {
        ParticleSystem _ps;

        public void Init()
        {
            _ps = GetComponent<ParticleSystem>();
            _ps.gameObject.SetActive(false);
        }

        public void PlayParticle()
        {
            if(_ps != null)
            {
                _ps.gameObject.SetActive(true);
                _ps.Play();
            }
        }

        public void StopParticle()
        {
            if (_ps != null)
            {
                _ps.Stop();
                _ps.gameObject.SetActive(false);
            }
        }
    }
}