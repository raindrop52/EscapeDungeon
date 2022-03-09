using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EscapeGame
{
    public enum Stage_LV
    { 
        INVALID = -1,
        RESTROOM = 0,
        LV1,
        END,
    }

    public class StageManager : MonoBehaviour
    {
        public static StageManager _inst;

        const string SAVEDATA_KEY_STAGE = "stage_level";
        const string SAVEDATA_KEY_POS_X = "stage_spawn_pos_x";
        const string SAVEDATA_KEY_POS_Y = "stage_spawn_pos_y";

        Stage_Base[] _stageArray;
        int _stageLV = -1;
        public int StageLV
        { get { return _stageLV; }  set { _stageLV = value; } }

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            Vector3 spawnPos = Vector3.zero;

            // 스테이지 키 정보 체크 ( 초기 정보 없을 시 스테이지 레벨 0으로 설정 및 키 생성 )
            if (PlayerPrefs.HasKey(SAVEDATA_KEY_STAGE) == false)
            {
                SetStageLV(Stage_LV.RESTROOM);
            }
            else
            {
                // 키가 있는 경우 스테이지 정보를 가져옴
                _stageLV = PlayerPrefs.GetInt(SAVEDATA_KEY_STAGE);

                if(PlayerPrefs.HasKey(SAVEDATA_KEY_POS_X) == true && PlayerPrefs.HasKey(SAVEDATA_KEY_POS_Y) == true)
                {
                    float x, y;
                    x = PlayerPrefs.GetFloat(SAVEDATA_KEY_POS_X);
                    y = PlayerPrefs.GetFloat(SAVEDATA_KEY_POS_Y);

                    spawnPos = new Vector3(x, y, 0f);
                }
            }

            if(spawnPos != Vector3.zero)
            {
                GameManager._inst.SetSpawnPos(spawnPos);
            }

            _stageArray = transform.GetComponentsInChildren<Stage_Base>(true);
            foreach (Stage_Base stage in _stageArray)
            {
                stage.OnShow(false);
            }
        }

        public void SetStagePos(Vector3 pos)
        {
            PlayerPrefs.SetFloat(SAVEDATA_KEY_POS_X, pos.x);
            PlayerPrefs.SetFloat(SAVEDATA_KEY_POS_Y, pos.y);
        }

        public void SetStageLV(Stage_LV lv)
        {
            _stageLV = (int)lv;
            PlayerPrefs.SetInt(SAVEDATA_KEY_STAGE, _stageLV);
        }

        public void StageLevelUP()
        {
            // 현재 스테이지 숨김
            StageEnd();
            // 스테이지 레벨업
            _stageLV++;
            PlayerPrefs.SetInt(SAVEDATA_KEY_STAGE, _stageLV);
            // 스테이지 할당
            StageInit();
        }

        public void StageInit()
        {
            Stage_Base baseStage = _stageArray[_stageLV];

            switch (_stageLV)
            {
                case 0:
                    {
                        RestRoom room = baseStage as RestRoom;
                        room.Init();

                        break;
                    }

                case 1:
                    {
                        Stage_1 lv1 = baseStage as Stage_1;

                        lv1.Init();

                        break;
                    }
                case 2:
                    {
                        break;
                    }
            }

            StageStart();
        }

        public void StageStart()
        {
            Stage_Base baseStage = _stageArray[_stageLV];

            switch (_stageLV)
            {
                case 1:
                    {
                        Stage_1 lv1 = baseStage as Stage_1;

                        lv1.StageStart();

                        break;
                    }
                case 2:
                    {
                        break;
                    }
            }
        }

        public void StageEnd()
        {
            _stageArray[_stageLV].OnShow(false);
        }

        public void DoStageEvent(int order)
        {
            Stage_Base baseStage = _stageArray[_stageLV];

            switch (_stageLV)
            {
                case 1:
                    {
                        Stage_1 lv1 = baseStage as Stage_1;

                        // 타일 숨기기
                        if(order == 0)
                            lv1.ShowHiddenTile(false);

                        break;
                    }
                case 2:
                    {
                        break;
                    }
            }
        }
    }
}