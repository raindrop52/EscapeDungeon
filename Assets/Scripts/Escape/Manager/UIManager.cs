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
                // �ʱ�ȭ �� Ȱ��ȭ ������ ��� ��Ȱ��ȭ ó��
                ShowTextPanel(false);
            }

            if(_messageBox != null)
            {
                _messageBox.OnShow(false);
            }

            // UI �ʱ�ȭ
            // �κ� UI �ʱ�ȭ

            // �÷��� UI �ʱ�ȭ
            _statusUI = transform.Find("StatusUI").GetComponentInChildren<StatusUI>();
            _statusUI.Init();

            // �κ� UI ǥ�� �� �κ� ������ ������ UI�� ��Ȱ��ȭ

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

        // UI�� ���� �ؽ�Ʈ ǥ��
        public void ShowTextMessage(string text = "", bool forceHide = false)
        {
            // ���� ����
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
                    // show = �ؽ�Ʈ â ǥ�� ����
                    bool isShow = _textReadPanel.activeSelf;

                    // UI �ؽ�Ʈ â Ȱ��ȭ(�����ִ� ����)
                    if (isShow == false)
                    {
                        _talking = true;

                        ShowTextPanel(true);

                        WriteTyping typing = _textReadPanel.GetComponent<WriteTyping>();
                        typing.m_Message = text;

                        typing.Init(delegate ()
                        {
                            // �ؽ�Ʈ �޽��� â �ݱ�
                            _talking = false;
                            ShowTextPanel(false);
                        });
                    }
                }
            }
        }

        public void CooltimeButton(float cooltime, GameObject buttonGo)
        {
            // ���� ��ư ������Ʈ ��������
            Button pressButton = buttonGo.GetComponent<Button>();
            // ��ư ��Ÿ�� �ڷ�ƾ ����
            StartCoroutine(_CooltimerButton(cooltime, pressButton));
        }

        IEnumerator _CooltimerButton(float cooltime, Button button)
        {
            // ��ư Disable�ϱ�
            if (button.interactable != false)
                button.interactable = false;

            float time = 0.0f;
            // �ڽİ�ü�� ��Ÿ�� �ؽ�Ʈ ��������
            Text cooltimeTxt = button.GetComponentInChildren<Text>();
            // ��Ÿ�� ����ϸ鼭 �ؽ�Ʈ ����
            while(time < cooltime)
            {
                time += Time.fixedDeltaTime;

                string text = string.Format("{0}", (int)(cooltime - time + 1));
                cooltimeTxt.text = text;

                yield return null;
            }

            // ��Ÿ�� ���� ��
            if(time >= cooltime)
            {
                cooltimeTxt.text = "";
            }

            // ��Ÿ�� ���� �� ��ư Disable ����
            if (button.interactable == false)
                button.interactable = true;
        }
    }
}