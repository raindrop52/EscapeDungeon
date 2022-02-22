using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum Talk_ID
    {
        NONE = 0,           // ��Ȳ �߻� X
        BREAK,              // ������Ʈ �ı� ��Ȳ
        MOVE,               // ��� �̵� ��Ȳ
        EXCUTE,             // ���� �ߵ� ��Ȳ
    }

    public class Trap : MonoBehaviour
    {
        protected virtual void OnDrawGizmos()
        {
            Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
        }
        
        public void PlaySFX(SFX_List sfxNo)
        {
            // Ʈ�� ��� �Ҹ� �÷���
            SoundManager._inst.OnPlaySfx(sfxNo);
        }
    }
}