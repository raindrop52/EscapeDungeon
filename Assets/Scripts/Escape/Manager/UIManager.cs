using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace EscapeGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager _inst;

        public Joystick _joystick;
        GameObject _textReadPanel;

        #region UI
        public StatusUI _statusUI;
        public MessageBox _messageBox;
        #endregion

        bool _talking = false;
        public bool Talking
        { get { return _talking; } }

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            _textReadPanel = GetComponentInChildren<WriteTyping>(true).gameObject;
            if(_textReadPanel.activeSelf)
            {
                // 초기화 시 활성화 상태인 경우 비활성화 처리
                ShowTextPanel(false);
            }

            if(_messageBox != null)
            {
                _messageBox.OnShow(false);
            }

            // UI 초기화
            // 로비 UI 초기화

            // 플레이 UI 초기화
            _statusUI = transform.Find("StatusUI").GetComponentInChildren<StatusUI>();
            _statusUI.Init();

            // 로비 UI 표시 및 로비를 제외한 나머지 UI는 비활성화

        }

        public void OnPointerDown()
        {
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = Input.mousePosition;

            _joystick.OnPointerDown(data);
        }

        public void OnPointerUp()
        {
            _joystick.OnPointerUp(null);
        }

        public void OnDrag()
        {
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = Input.mousePosition;

            _joystick.OnDrag(data);
        }

        public bool IsTextPanel()
        {
            return _textReadPanel.activeSelf;
        }

        void ShowTextPanel(bool show)
        {
            _textReadPanel.SetActive(show);
        }

        // UI에 지정 텍스트 표시
        public void ShowTextMessage(string text = "", bool forceHide = false)
        {
            // 강제 숨김
            if(forceHide)
            {
                if (_talking == true)
                    _talking = false;

                ShowTextPanel(false);
            }
            else
            {
                if (_textReadPanel != null)
                {
                    // show = 텍스트 창 표시 상태
                    bool isShow = _textReadPanel.activeSelf;

                    // UI 텍스트 창 활성화(꺼져있는 상태)
                    if (isShow == false)
                    {
                        _talking = true;

                        ShowTextPanel(true);

                        WriteTyping typing = _textReadPanel.GetComponent<WriteTyping>();
                        typing.m_Message = text;

                        typing.Init(delegate ()
                        {
                            // 텍스트 메시지 창 닫기
                            _talking = false;
                            ShowTextPanel(false);
                        });
                    }
                }
            }
        }

        public void CooltimeButton(float cooltime, GameObject buttonGo)
        {
            // 눌린 버튼 컴포넌트 가져오기
            Button pressButton = buttonGo.GetComponent<Button>();
            // 버튼 쿨타임 코루틴 동작
            StartCoroutine(_CooltimerButton(cooltime, pressButton));
        }

        IEnumerator _CooltimerButton(float cooltime, Button button)
        {
            // 버튼 Disable하기
            if (button.interactable != false)
                button.interactable = false;

            float time = 0.0f;
            // 자식객체의 쿨타임 텍스트 가져오기
            Text cooltimeTxt = button.GetComponentInChildren<Text>();
            // 쿨타임 대기하면서 텍스트 수정
            while(time < cooltime)
            {
                time += Time.fixedDeltaTime;

                string text = string.Format("{0}", (int)(cooltime - time + 1));
                cooltimeTxt.text = text;

                yield return null;
            }

            // 쿨타임 종료 후
            if(time >= cooltime)
            {
                cooltimeTxt.text = "";
            }

            // 쿨타임 종료 후 버튼 Disable 해제
            if (button.interactable == false)
                button.interactable = true;
        }
    }
}