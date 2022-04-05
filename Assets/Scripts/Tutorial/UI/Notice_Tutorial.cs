using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Notice_Tutorial : MonoBehaviour
    {
        bool _oneShow = false;

        public void Init()
        {
            _oneShow = false;

            OnShow(false);
        }

        public void OnShow(bool show)
        {
            if(show == true)
            {
                if(_oneShow == true)
                {
                    return;
                }
            }

            gameObject.SetActive(show);
        }

        public void OnTouchToStart()
        {
            _oneShow = true;

            OnShow(false);

            PlayRoomUI ui = UIManager._inst.GetUI(UI_ID.PLAYROOM) as PlayRoomUI;
            if(ui != null)
            {
                ui._statusUI.OnShow(true);
                ui._controlUI.OnShow(true);
            }

            GameObject go = GameManager._inst._player.gameObject;
            Player_Control pcCtrl = go.GetComponent<Player_Control>();
            if (pcCtrl != null)
            {
                pcCtrl.OnSwitchMove(true);
            }
        }
    }
}