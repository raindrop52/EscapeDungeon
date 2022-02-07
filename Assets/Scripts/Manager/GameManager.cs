using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum STAGE_LV
    {
        NONE,
        TRAP,
        DEFENSE,
        BOSS,

    }

    public enum ROOM
    {
        RESTROOM,
        DUNGEON,

    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager _inst;

        [Header("��������")]
        public STAGE_LV _stageLevel;        // ���� �������� ����
        [SerializeField] ROOM _room;        // ���� ������ ��
        public ROOM Room
        {
            get { return _room; }
        }
        public bool _clearRoom = false;     // �� Ŭ���� ����
                
        [Header("�� ��ȯ ���ø�")]
        [SerializeField] SpriteRenderer _loadingPanel;

        bool mapMove = false;
        public bool MapMoving
        {
            get { return mapMove; }
            set { mapMove = value; }
        }

        void Awake()
        {
            _inst = this;
        }

        void Start()
        {

        }


        void Update()
        {

        }

        public void StartMoveStage()
        {
            StartCoroutine(MoveStart());
        }

        IEnumerator MoveStart()
        {
            if (mapMove == false)
            {
                mapMove = true;

                // ȭ���� ����
                ShowLoading(true);

                StartCoroutine(MoveStage());

                yield return null;
            }
            else
            {
                Debug.Log("�̵� �Ұ�");
                yield return null;
            }
        }

        IEnumerator MoveStage()
        {
            // �� ��ȯ

            // ĳ���� ��ġ
            
            yield return new WaitForSeconds(1.0f);

            // ȭ�� ����
            StartCoroutine(MoveEnd());
        }

        IEnumerator MoveEnd()
        {
            mapMove = false;

            ShowLoading(false);

            yield return null;
        }

        void ShowLoading(bool show)
        {
            if(show)
            {
                StartCoroutine(_FadeOut(0.5f));
            }
            else
            {
                StartCoroutine(_FadeIn(1.0f));
            }
        }

        IEnumerator _FadeIn(float time)
        {
            Color color = _loadingPanel.color;

            while(color.a > 0f)
            {
                color.a -= Time.deltaTime / time;
                _loadingPanel.color = color;
                yield return null;
            }
        }

        IEnumerator _FadeOut(float time)
        {
            Color color = _loadingPanel.color;

            while (color.a <= 1.0f)
            {
                color.a += Time.deltaTime / time;
                _loadingPanel.color = color;
                yield return null;
            }
        }
    }
}