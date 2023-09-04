using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    #region Variables
    [Header("Targets")]
    protected List<Transform> keys;
    protected int targetIdx;

    [Header("Moving Variables")]
    protected float distanceToTarget;
    protected float checkTime;

    [Header("Objects")]
    protected GameObject player;
    protected UnityEngine.AI.NavMeshAgent agent;
    protected GameObject npcGO;
    protected NpcFSM FSM;
    #endregion


    // Base Method.
    public virtual void EnterState(NpcFSM npc)
    {
        keys = npc.keys;
        targetIdx = npc.targetIdx;

        distanceToTarget = npc.distanceToTarget;
        checkTime = npc.checkTime;

        player = npc.player;
        agent = npc.agent;
        npcGO = npc.npcGO;

        FSM = npc;
    }
}
