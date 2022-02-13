using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EscapeGame
{
    public enum SFX_List
    {
        HOLE,
        

    }

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager _inst;
        
        [SerializeField] List<AudioSource> _sfxList;

        private void Awake()
        {
            _inst = this;
        }

        void Start()
        {
            AudioSource[] soundList = GetComponentsInChildren<AudioSource>();
            _sfxList = soundList.ToList();
        }

        
        void Update()
        {

        }

        public void OnPlaySfx(SFX_List sfxNo)
        {
            int no = (int)sfxNo;
            _sfxList[no].Play();
        }
    }
}