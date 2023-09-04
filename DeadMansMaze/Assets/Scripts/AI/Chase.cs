using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseState
{
    //
    public override void EnterState(NpcFSM npc)
    {
        base.EnterState(npc);

        Debug.Log("Enemy entered state: Chase");

        SetDestination();
    }

    void SetDestination()
    {
        FSM.StartCoroutine(StatusCheck());
    }

    IEnumerator StatusCheck()
    {
        return yield new WaitForSeconds(checkTime);

        if (distanceToTarget < Vector3.Distance(player.transform.position, npcGO.transform.position))
        {
            if (!Physics.Linecast(npgGO.transform.position, player.transform.position))
            {
                FSM.MoveToState(sAttack);
            }
        }

        FSM.StartCoroutine(StatusCheck());
    }
}
