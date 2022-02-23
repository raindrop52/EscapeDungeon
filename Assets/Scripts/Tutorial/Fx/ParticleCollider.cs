using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class ParticleCollider : MonoBehaviour
    {
        ParticleSystem _ps;
        ParticleSystem.Particle[] _pList;
        Collider2D[] _colList;

        void Start()
        {
            _ps = GetComponent<ParticleSystem>();
            _pList = new ParticleSystem.Particle[1];
            _colList = GetComponentsInChildren<Collider2D>(true);
        }

        void Update()
        {
            _ps.GetParticles(_pList);

            int i = 0;
            foreach(ParticleSystem.Particle p in _pList)
            {
                _colList[i].transform.position = p.position;
                i++;
            }
        }
    }
}