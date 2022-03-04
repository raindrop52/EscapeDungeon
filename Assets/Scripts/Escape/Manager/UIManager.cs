using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager _inst;

        GameObject _textReadPanel;

        #region UI
        public StatusUI _statusUI;
        public ControlUI _controlUI;
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
            _controlUI = transform.Find("ControlUI").GetComponentInChildren<ControlUI>();
            _controlUI.Init();

            // �κ� UI ǥ�� �� �κ� ������ ������ UI�� ��Ȱ��ȭ

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
    }
}