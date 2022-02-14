using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class Player : MapObject
    {
        public int _maxLevel = 200;
        public long _maxExp = 10000000;
        public AnimationCurve _expCurve;

        public ParticleSystem _ps_normal;
        BoxCollider2D _attackCol;

        public GameObject _hpBarObj;
        public Vector3 _hpBarOffset;

        [SerializeField] int _exp;
        public int Exp { get { return _exp; } }
        [SerializeField] int _level = 1;
        public int Level { get { return _level; } }
        int _needExp;

        protected override void Start()
        {
            base.Start();

            Init();

            _ps_normal = transform.Find("Sword_Effect").GetComponent<ParticleSystem>();
            _attackCol = transform.Find("Sword_Effect").GetComponent<BoxCollider2D>();
            _attackCol.enabled = false;
            _hpBarImg = _hpBarObj.transform.GetChild(0).GetComponent<Image>();

            // 공격 담당 코루틴
            StartCoroutine(_NormalAttack());
        }

        public void Init()
        {
            // 저장된 레벨 불러오기
            //LoadLevel();
            // 저장된 경험치 불러오기
            PlayerPrefs.SetInt("survivor_exp", 0);
            LoadExp();
        }

        void FixedUpdate()
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            _hpBarObj.transform.position = pos + _hpBarOffset;
        }

        IEnumerator _NormalAttack()
        {
            float maxTime = 1.0f;
            float lifeTime = _ps_normal.main.duration;

            while (gameObject.activeSelf)
            {
                // 이펙트 재생
                _ps_normal.Play();
                // 충돌체를 켜줌
                _attackCol.enabled = true;
                yield return new WaitForSeconds(lifeTime);
                // 충돌체를 꺼줌
                _attackCol.enabled = false;
                yield return new WaitForSeconds(maxTime - lifeTime);
            }

            yield return null;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            {
                return;
            }

            OnHit(collision.collider);
        }

        void LoadExp()
        {
            if (PlayerPrefs.HasKey("survivor_exp"))
            {
                int exp = PlayerPrefs.GetInt("survivor_exp");
                _exp = exp;
            }
        }

        void LoadLevel()
        {
            if (PlayerPrefs.HasKey("survivor_level"))
            {
                int level = PlayerPrefs.GetInt("survivor_level");
                _level = level;
            }
        }

        public void AddExp(int deltaExp)
        {
            _exp += deltaExp;

            PlayerPrefs.SetInt("survivor_exp", _exp);

            // 경험치 바 업데이트
            UIManager_Tutorial._inst.RefreshExpUI();
        }

        void LevelUp()
        {
            _level++;

            _exp = 0;
        }
    }
}