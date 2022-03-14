using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class BaseUI : MonoBehaviour
    {
        public virtual void Init()
        {
            
        }

        public void OnShow(bool show)
        {
            if (gameObject.activeSelf != show)
            {
                gameObject.SetActive(show);
            }
        }

        protected virtual void OnEnable()
        {
            // 활성화 시
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
            }
        }

        protected virtual void SettingStage(bool stageClear = false)
        {
            if (Time.timeScale != 1)
            {
                Time.timeScale = 1;
            }

            // PlayUI 표시
            UIManager._inst.NowUI = UI_ID.PLAYROOM;
            UIManager._inst.ChangeUI();

            // PlayRoomUI 초기화(동작 관련)
            // 값 입력이 없는 경우 스테이지 저장된 스테이지로 설정
            if (stageClear == true)
            {
                StageManager._inst.SetStageLV(Stage_LV.RESTROOM);
            }

            // 스테이지 초기화
            StageManager._inst.StageInit();

            GameManager._inst.PlayInit();
        }
    }
}