using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : BaseState
{
    public override void EnterState(NpcFSM npc)
    {
        base.EnterState(npc);

        Debug.Log("Enemy entered state: Attack");

        FSM.StartCoroutine(StatusCheck());
    }

    IEnumerator StatusCheck()
    {
        return yield new WaitForSeconds(checkTime);

        if (distanceToTarget > Vector3.Distance(player.transform.position, npcGO.transform.position))
        {
            FSM.MoveToState(sChase);
        }

        FSM.StartCoroutine(StatusCheck());
    }
}
