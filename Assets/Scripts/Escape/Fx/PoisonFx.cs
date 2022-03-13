using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class PoisonFx : MonoBehaviour
    {
        ParticleSystem _ps;

        public void Init(bool sticky = false)
        {
            _ps = GetComponent<ParticleSystem>();
        }

        public void PlayParticle()
        {
            _ps.Play();
        }

        public void StopParticle()
        {
            _ps.Stop();
        }
    }
}