using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EscapeGame
{
    public enum SFX_List
    {
        HOLE,
        BOMB,
    }

    public enum BGM_List
    {
        LOBBY,
        PLAYROOM,
        GAMEOVER,
        GOAL,
    }

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager _inst;
        
        [SerializeField] AudioSource[] _sfxList;
        [SerializeField] AudioSource[] _bgmList;
        int _prevNo = -1;

        private void Awake()
        {
            _inst = this;
        }

        public void Init()
        {
            Transform trans = transform.Find("Sfx");
            _sfxList = trans.GetComponentsInChildren<AudioSource>();
            trans = transform.Find("Bgm");
            _bgmList = trans.GetComponentsInChildren<AudioSource>();
        }

        public void OnPlaySfx(SFX_List sfxNo)
        {
            int no = (int)sfxNo;
            if (_sfxList.Length > 0 && no < _sfxList.Length)
            {
                _sfxList[no].Play();
            }
        }

        public void OnStopSfx(SFX_List sfxNo)
        {
            int no = (int)sfxNo;
            if (_sfxList.Length > 0 && no < _sfxList.Length)
            {
                _sfxList[no].Stop();
            }
        }

        public void OnPlayBgm(BGM_List bgmNo)
        {
            int no = (int)bgmNo;

            if(_prevNo >= 0)
            {
                OnStopBgm();
            }

            _prevNo = no;

            if(no < _bgmList.Length)
                _bgmList[no].Play();
        }

        public void OnStopBgm()
        {
            int no = _prevNo;
            if (no < _bgmList.Length)
                _bgmList[no].Stop();
        }
    }
}