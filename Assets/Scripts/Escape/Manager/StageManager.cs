using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EscapeGame
{
    public enum STATUS
    {
        NONE,
        WAIT,
        READY,
        START,
        END,
    }

    public class StageManager : MonoBehaviour
    {
        public static StageManager _inst;

        [Header("스테이지1 함정")]
        [SerializeField] GameObject _tileHidden;
        Arrow_Trap[] _arrowTrapArray;

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            ShowHiddenTile(true);

            // 화살 함정 Init
            _arrowTrapArray = GetComponentsInChildren<Arrow_Trap>();
            foreach(Arrow_Trap arTrap in _arrowTrapArray)
            {
                arTrap.Init();
            }
        }

        // 히든 타일 설정
        public void ShowHiddenTile(bool show)
        {
            _tileHidden.SetActive(show);
        }
    }
}