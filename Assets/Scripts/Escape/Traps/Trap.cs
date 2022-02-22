using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum Talk_ID
    {
        NONE = 0,           // 상황 발생 X
        BREAK,              // 오브젝트 파괴 상황
        MOVE,               // 대상 이동 상황
        EXCUTE,             // 함정 발동 상황
    }

    public class Trap : MonoBehaviour
    {
        protected virtual void OnDrawGizmos()
        {
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        }
        
        public void PlaySFX(SFX_List sfxNo)
        {
            // 트랩 밟는 소리 플레이
            SoundManager._inst.OnPlaySfx(sfxNo);
        }
    }
}