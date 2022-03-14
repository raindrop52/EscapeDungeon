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
            // Ȱ��ȭ ��
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

            // PlayUI ǥ��
            UIManager._inst.NowUI = UI_ID.PLAYROOM;
            UIManager._inst.ChangeUI();

            // PlayRoomUI �ʱ�ȭ(���� ����)
            // �� �Է��� ���� ��� �������� ����� ���������� ����
            if (stageClear == true)
            {
                StageManager._inst.SetStageLV(Stage_LV.RESTROOM);
            }

            // �������� �ʱ�ȭ
            StageManager._inst.StageInit();

            GameManager._inst.PlayInit();
        }
    }
}