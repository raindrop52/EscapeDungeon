using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public class Trap : MonoBehaviour
    {
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        }
        
        public void PlaySFX(SFX_List sfxNo)
        {
            // Æ®·¦ ¹â´Â ¼Ò¸® ÇÃ·¹ÀÌ
            SoundManager._inst.OnPlaySfx(sfxNo);
        }
    }
}