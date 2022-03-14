using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeGame
{
    public enum Portal_Type
    {
        PORTAL_GO,
        PORTAL_END,
    }

    public class Portal : MonoBehaviour
    {
        public Portal_Type _type;
        public Portal _moveTarget;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_moveTarget == null)
            {
                if (_type == Portal_Type.PORTAL_END)
                    return;
            }

            if (collision.CompareTag("Player") && collision.gameObject.layer == LayerMask.NameToLayer("Portal"))
            {
                Debug.Log("플레이어 진입");

                if (_type == Portal_Type.PORTAL_GO)
                {
                    if(_moveTarget == null)
                    {
                        // TODO : 임시
                        // 게임 클리어
                        GoalUI goalUi = UIManager._inst.GetUI(UI_ID.GOAL) as GoalUI;
                        if(goalUi != null)
                        {
                            SoundManager._inst.OnPlayBgm(BGM_List.GOAL);
                            goalUi.OnShow(true);
                        }
                    }
                    else
                        StartCoroutine(_DoWarp(collision));
                }
            }
        }

        IEnumerator _DoWarp(Collider2D collision)
        {
            GameManager._inst.StartMoveStage();

            yield return new WaitForSeconds(0.5f);

            Player player = collision.transform.parent.GetComponent<Player>();
            player.ChangePlayerPos(_moveTarget.transform.position);
        }
    }
}