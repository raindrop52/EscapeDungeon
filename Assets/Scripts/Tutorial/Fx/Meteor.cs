using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class Meteor : MonoBehaviour
    {
        // ��ƼŬ�� �迭
        List<ParticleSystem.Particle> _pList = new List<ParticleSystem.Particle>();
        ParticleSystem[] _psList;
        List<BoxCollider2D> _colList = new List<BoxCollider2D>();

        void Start()
        {
            _psList = GetComponentsInChildren<ParticleSystem>();
        }

        
        void Update()
        {
            _pList.Clear();
            _colList.Clear();

            int count = 0;
            foreach (ParticleSystem ps in _psList)
            {
                count = ps.particleCount;
                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[count];
                ps.GetParticles(particles);

                _pList.AddRange(particles);
            }

            foreach (ParticleSystem.Particle p in _pList)
            {
                // ��ũ��Ʈ �󿡼� �ڽ� �ݶ��̴� ����� �ڽİ�ü�� �������ֱ�
                GameObject go = new GameObject("col");
                go.transform.parent = transform;
                go.tag = "Attack";
                BoxCollider2D col = go.AddComponent<BoxCollider2D>();
                col.size = new Vector2(0.5f, 0.5f);
                col.isTrigger = true;

                _colList.Add(col);
            }

            // ������ ������ ��� �����޽��� ǥ��
            Debug.Assert(_pList.Count == _colList.Count);

            int i = 0;
            foreach (ParticleSystem.Particle p in _pList)
            {
                _colList[i].transform.position = p.position;
                i++;
            }
        }
    }
}