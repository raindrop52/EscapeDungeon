using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeGame
{
    public enum UI_ID
    {
        INVALID = -1,
        LOBBY = 0,
        PLAYROOM,
        GAMEOVER,
        CAUTION,
        OPTION,
        GOAL,
        END
    }

    public class UIManager : MonoBehaviour
    {
        public static UIManager _inst;

        #region UI
        UI_ID _nowUI;
        public UI_ID NowUI
        { get { return _nowUI; } set { _nowUI = value; } }
        BaseUI[] _uiList;
        #endregion

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            _uiList = GetComponentsInChildren<BaseUI>(true);

            // 현재 UI를 설정 (초기 Lobby)
            _nowUI = UI_ID.LOBBY;

            foreach (BaseUI ui in _uiList)
            {
                // 초기화
                ui.Init();
                // ui 비활성화
                ui.gameObject.SetActive(false);
            }
            
            ChangeUI();
        }

        public void ChangeUI()
        {
            int showNo = -1;
            // 현재 UI를 가지고 변경
            switch (_nowUI)
            {
                case UI_ID.INVALID:
                    {
                        Debug.LogError("Error : 잘못된 접근입니다. (UI_ID : Invalid)");
                        break;
                    }

                case UI_ID.END:
                    {
                        Debug.LogError("Error : 잘못된 접근입니다. (UI_ID : End)");
                        break;
                    }

                case UI_ID.LOBBY:
                    {
                        for (int i = 0; i < _uiList.Length; i++)
                        {
                            if (_uiList[i] is LobbyUI)
                            {
                                showNo = i;
                                SoundManager._inst.OnPlayBgm(BGM_List.LOBBY);
                                break;
                            }
                        }

                        break;
                    }

                case UI_ID.PLAYROOM:
                    {
                        for (int i = 0; i < _uiList.Length; i++)
                        {
                            if (_uiList[i] is PlayRoomUI)
                            {
                                showNo = i;
                                // 체력바 초기화
                                ResetHitUI();
                                // 중독 초기화
                                ClearPoisonUI();
                                SoundManager._inst.OnPlayBgm(BGM_List.PLAYROOM);
                                break;
                            }
                        }

                        break;
                    }

                case UI_ID.GAMEOVER:
                    {
                        for (int i = 0; i < _uiList.Length; i++)
                        {
                            if (_uiList[i] is GameOverUI)
                            {
                                showNo = i;
                                SoundManager._inst.OnPlayBgm(BGM_List.GAMEOVER);
                                break;
                            }
                        }

                        break;
                    }

                case UI_ID.OPTION:
                    {
                        for (int i = 0; i < _uiList.Length; i++)
                        {
                            if (_uiList[i] is OptionUI)
                            {
                                showNo = i;
                                break;
                            }
                        }

                        break;
                    }
            }

            if(_nowUI > UI_ID.INVALID && _nowUI < UI_ID.END)
            {
                for (int i = (int)UI_ID.INVALID + 1; i < (int)UI_ID.END; i++)
                {
                    if (i == showNo)
                    {
                        if (_uiList[i].gameObject.activeSelf != true)
                            _uiList[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        if (_uiList[i].gameObject.activeSelf == true)
                            _uiList[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        public BaseUI GetUI(UI_ID id)
        {
            int uid = (int)id;

            return _uiList[uid];
        }

        #region PlayRoom
        
        public void RefreshHitUI()
        {
            if (_nowUI == UI_ID.PLAYROOM)
            {
                PlayRoomUI playroom = _uiList[(int)_nowUI] as PlayRoomUI;

                playroom.OnHit();
            }
        }

        public void ResetHitUI()
        {
            if(_nowUI == UI_ID.PLAYROOM)
            {
                PlayRoomUI playroom = _uiList[(int)_nowUI] as PlayRoomUI;

                playroom.OnHeal();
            }
        }

        public void ShowTextMessage(string text = "", bool forceHide = false)
        {
            if (_nowUI == UI_ID.PLAYROOM)
            {
                PlayRoomUI playroom = _uiList[(int)_nowUI] as PlayRoomUI;

                playroom.ShowTextMessage(text, forceHide);
            }
        }

        public bool CheckTalk()
        {
            bool result = false;

            if (_nowUI == UI_ID.PLAYROOM)
            {
                PlayRoomUI playroom = _uiList[(int)_nowUI] as PlayRoomUI;

                result = playroom.Talking;
            }

            return result;
        }

        // 중독 상태 표시
        public void SetPoisonUI(Poison_Type type, float holdingTime)
        {
            if (_nowUI == UI_ID.PLAYROOM)
            {
                PlayRoomUI playroom = _uiList[(int)_nowUI] as PlayRoomUI;

                playroom.OnPoisoning(type, holdingTime);
            }
        }

        // 중독 시간 초기화
        public void ResetPoisonTimeUI()
        {
            if (_nowUI == UI_ID.PLAYROOM)
            {
                PlayRoomUI playroom = _uiList[(int)_nowUI] as PlayRoomUI;

                playroom.OnPoisoningTimeReset();
            }
        }

        public void ClearPoisonUI()
        {
            if (_nowUI == UI_ID.PLAYROOM)
            {
                PlayRoomUI playroom = _uiList[(int)_nowUI] as PlayRoomUI;

                playroom.OnClearPoison();
            }
        }

        public bool IsTextPanel()
        {
            bool result = false;

            if (_nowUI == UI_ID.PLAYROOM)
            {
                PlayRoomUI playroom = _uiList[(int)_nowUI] as PlayRoomUI;

                result = playroom.IsTextPanel();
            }

            return result;
        }

        #endregion
    }
}