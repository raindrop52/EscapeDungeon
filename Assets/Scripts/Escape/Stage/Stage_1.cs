using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EscapeGame
{
    public enum Stage_1_Tiles
    { 
        INVALID = -1,
        FLOOR = 0,
        WALL,
        DOOR,
        HIDDEN,
        TRAP_HOLE,
        TRAP_WALL_LEFT,
        TRAP_WALL_RIGHT,
        END
    }

    public class Stage_1 : Stage_Base
    {
        [Header("스테이지1 함정")]
        [SerializeField] List<GameObject> _tilesGo;
        [SerializeField] Arrow_Trap _arrowTrap;
        [SerializeField] Trap_Talk _talkBreakWall;

        Arrow_Trap[] _arrowTrapArray;

        public override void Init()
        {
            base.Init();

            // 화살 함정 Init
            _arrowTrap.Init();
        }

        public override void StageStart()
        {
            // 타일 활성화 초기화
            for (int i = (int)Stage_1_Tiles.INVALID + 1; i < (int)Stage_1_Tiles.END; i++)
            {
                if ( i <= (int)Stage_1_Tiles.HIDDEN)
                {
                    _tilesGo[i].SetActive(true);
                }
                else
                {
                    _tilesGo[i].SetActive(false);
                }
            }
            // 함정 초기화
            if(_talkBreakWall != null)
            {
                Talk_Break talkBreak = _talkBreakWall as Talk_Break;
                if(talkBreak != null)
                {
                    talkBreak.OnShow();
                }
            }
        }

        // 히든 타일 설정
        public void ShowHiddenTile(bool show)
        {
            _tilesGo[(int)Stage_1_Tiles.HIDDEN].SetActive(show);
        }
    }
}