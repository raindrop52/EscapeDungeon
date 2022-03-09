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
        [Header("��������1 ����")]
        [SerializeField] List<GameObject> _tilesGo;
        [SerializeField] Arrow_Trap _arrowTrap;
        [SerializeField] Trap_Talk _talkBreakWall;

        Arrow_Trap[] _arrowTrapArray;

        public override void Init()
        {
            base.Init();

            // ȭ�� ���� Init
            _arrowTrap.Init();
        }

        public override void StageStart()
        {
            // Ÿ�� Ȱ��ȭ �ʱ�ȭ
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
            // ���� �ʱ�ȭ
            if(_talkBreakWall != null)
            {
                Talk_Break talkBreak = _talkBreakWall as Talk_Break;
                if(talkBreak != null)
                {
                    talkBreak.OnShow();
                }
            }
        }

        // ���� Ÿ�� ����
        public void ShowHiddenTile(bool show)
        {
            _tilesGo[(int)Stage_1_Tiles.HIDDEN].SetActive(show);
        }
    }
}