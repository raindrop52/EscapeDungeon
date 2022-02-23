using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class Meteor : MonoBehaviour
    {
        // 파티클의 배열
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
                // 스크립트 상에서 박스 콜라이더 만들어 자식객체로 생성해주기
                GameObject go = new GameObject("col");
                go.transform.parent = transform;
                go.tag = "Attack";
                BoxCollider2D col = go.AddComponent<BoxCollider2D>();
                col.size = new Vector2(0.5f, 0.5f);
                col.isTrigger = true;

                _colList.Add(col);
            }

            // 조건이 실패할 경우 에러메시지 표시
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