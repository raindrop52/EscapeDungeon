using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace EscapeGame
{
    public class ControlUI : MonoBehaviour
    {
        [SerializeField] Joystick _joystick;

        public void Init()
        {
            // 초기 조이스틱 False로 설정
            if (_joystick != null)
                _joystick.gameObject.SetActive(false);

            OnShow(false);
        }

        public void OnShow(bool show)
        {
            gameObject.SetActive(show);
        }

        public void OnPointerDown()
        {
            _joystick.gameObject.SetActive(true);

            _joystick.transform.position = Input.mousePosition;

            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = Input.mousePosition;

            _joystick.OnPointerDown(data);
        }

        public void OnPointerUp()
        {
            _joystick.gameObject.SetActive(false);

            _joystick.OnPointerUp(null);
        }

        public void OnDrag()
        {
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = Input.mousePosition;

            _joystick.OnDrag(data);
        }

        public void OnPointerDownRight()
        {
            Player player = GameManager._inst._player;
            if(player != null)
            {
                if(Player_StateManager._inst.CurState == Player_State.TALK)
                {
                    if (player._btnDo == false)
                        player._btnDo = true;
                }
            }
        }
    }
}