using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcFSM : MonoBehaviour
{
    #region Variables
    public BaseState CurrentState;

    [Header("States")]
    public Chase sChase = new Chase();
    public Attack sAttack = new Attack();
    public Guard sGuard = new Guard();

    [Header("Targets")]
    public List<Transform> keys;
    public int targetIdx;

    [Header("Moving Variables")]
    public float distanceToTarget;
    public float checkTime;

    [Header("Objects")]
    public GameObject player;
    public NavMeshAgent agent;
    public GameObject npcGO;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        npcGO = this.gameObject;
        player = GameObject.FindWithTag("Player");

        MoveToState(sChase);
    }

    public void MoveToState(BaseState state)
    {
        CurrentState = state;
        CurrentState.EnterState(this);
    }
}
