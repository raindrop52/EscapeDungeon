using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class Player : MapObject
    {
        const string SAVEDATA_KEY_LEVEL = "survivor_level";
        const string SAVEDATA_KEY_EXP = "survivor_exp";

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
            //PlayerPrefs.SetInt(SAVEDATA_KEY_LEVEL, 1);
            //PlayerPrefs.SetInt(SAVEDATA_KEY_EXP, 0);

            // 저장된 레벨 불러오기
            LoadLevel();
            // 저장된 경험치 불러오기
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
            if (PlayerPrefs.HasKey(SAVEDATA_KEY_EXP))
            {
                int exp = PlayerPrefs.GetInt(SAVEDATA_KEY_EXP);
                _exp = exp;
            }
        }

        void LoadLevel()
        {
            if (PlayerPrefs.HasKey(SAVEDATA_KEY_LEVEL))
            {
                int level = PlayerPrefs.GetInt(SAVEDATA_KEY_LEVEL);
                _level = level;
            }
        }

        public void AddExp(int deltaExp)
        {
            _exp += deltaExp;

            PlayerPrefs.SetInt(SAVEDATA_KEY_EXP, _exp);

            // 레벨업 체크
            CheckLevelUp();

            // 경험치 바 업데이트
            UIManager_Tutorial._inst.RefreshExpUI();
        }

        public void CalcLevelExp(out int curLvExp, out int nextLvExp)
        {
            float curLevel = _level;
            float nextLevel = curLevel + 1;

            curLvExp = (int)((float)_maxExp * _expCurve.Evaluate(curLevel / (float)_maxLevel));
            nextLvExp = (int)((float)_maxExp * _expCurve.Evaluate(nextLevel / (float)_maxLevel));

            float exp = _exp;     // 구간 exp
            float needExp = nextLvExp - curLvExp;
        }

        void CheckLevelUp()
        {
            int curLvExp = 0;       // 현재 레벨의 경험치
            int nextLvExp = 0;      // 다음 레벨의 경험치
            CalcLevelExp(out curLvExp, out nextLvExp);

            int exp = _exp;     // 구간에서 누적된 경험치
            int needExp = nextLvExp - curLvExp;     // 레벨업에 필요한 경험치

            CheckLevelUp(exp, needExp);
        }

        void CheckLevelUp(int exp, int needExp)
        {
            if (needExp <= exp)
            {
                int exceed = exp - needExp;     // 경험치 초과분

                // 레벨업
                _level++;
                PlayerPrefs.SetInt(SAVEDATA_KEY_LEVEL, _level);

                // 경험치 초과분을 다음 레벨 구간에 이어서 시작
                _exp = exceed;
                PlayerPrefs.SetInt(SAVEDATA_KEY_EXP, _exp);

                int curLvExp = 0;       // 현재 레벨의 경험치
                int nextLvExp = 0;      // 다음 레벨의 경험치
                CalcLevelExp(out curLvExp, out nextLvExp);

                int needExp2 = nextLvExp - curLvExp;
                CheckLevelUp(exceed, needExp2);
            }
        }
    }
}