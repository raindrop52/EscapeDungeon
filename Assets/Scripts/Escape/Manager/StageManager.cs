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
            _stageArray = transform.GetComponentsInChildren<Stage_Base>(true);
            foreach(Stage_Base stage in _stageArray)
            {
                stage.OnShow(false);
            }

            _stageLV = (int)Stage_LV.RESTROOM;
        }
        
        public void StageInit()
        {
            Stage_Base baseStage = _stageArray[_stageLV];

            switch (_stageLV)
            {
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

        public void DoStageEvent(int order)
        {
            Stage_Base baseStage = _stageArray[_stageLV];

            switch (_stageLV)
            {
                case 1:
                    {
                        Stage_1 lv1 = baseStage as Stage_1;

                        // Å¸ÀÏ ¼û±â±â
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