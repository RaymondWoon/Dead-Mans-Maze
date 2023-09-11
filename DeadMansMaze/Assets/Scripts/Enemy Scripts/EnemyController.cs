using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //[Header("GameObject")]
    //[SerializeField] private GameObject _player;

    [Header("Speed")]
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float _runningSpeed;


    private GameObject _player;
    private Animator _anim;
    private NavMeshAgent _agent;

    private enum STATE
    {
        IDLE,
        WANDER,
        ATTACK,
        CHASE,
        DEAD
    }

    // Initial state
    private STATE _state = STATE.IDLE;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }

     // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case STATE.IDLE:
                if (IsPlayerVisible())
                {
                    _state = STATE.CHASE;
                }
                else if (Random.Range(0, 5000) < 5)
                {
                    _state = STATE.WANDER;
                }
                break;

            case STATE.WANDER:
                // Only if agent has no path or reach end of current path
                if (!_agent.hasPath)
                {
                    float newX = transform.position.x + Random.Range(-12, 12);
                    float newZ = transform.position.z + Random.Range(-12, 12);
                    //float newY = Terrain.activeTerrain.SampleHeight(new Vector3(newX, -0.1f, newZ));

                    Vector3 dest = new Vector3(newX, 0.0f, newZ);
                    _agent.SetDestination(dest);
                    _agent.stoppingDistance = 0;

                    ResetStates();
                    _agent.speed = _walkingSpeed;
                    _anim.SetBool("isWalking", true);
                }

                if (IsPlayerVisible())
                    _state = STATE.CHASE;
                else if (Random.Range(0, 5000) < 5)
                {
                    _state = STATE.IDLE;

                    ResetStates();
                    _agent.ResetPath();
                }
                break;

            case STATE.CHASE:
                _agent.SetDestination(_player.transform.position);
                _agent.stoppingDistance = 2.0f;

                ResetStates();
                _agent.speed = _runningSpeed;
                _anim.SetBool("isRunning", true);
                
                if (_agent.remainingDistance <= _agent.stoppingDistance && !_agent.pathPending)
                {
                    _state = STATE.ATTACK;
                }

                if (ForgetPlayer())
                {
                    _state = STATE.WANDER;
                    _agent.ResetPath();
                }
                break;

            case STATE.ATTACK:
                ResetStates();
                _anim.SetBool("isAttacking", true);

                // Set enemy to look at player
                transform.LookAt(_player.transform.position);

                if (DistanceToPlayer() > _agent.stoppingDistance + 1)
                    _state = STATE.CHASE;
                break;

            case STATE.DEAD:

                break;
        }
    }

    private void ResetStates()
    {
        _anim.SetBool("isWalking", false);
        _anim.SetBool("isAttacking", false);
        _anim.SetBool("isRunning", false);
        _anim.SetBool("isDead", false);
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(_player.transform.position, transform.position);
    }

    private bool IsPlayerVisible()
    {
        if (DistanceToPlayer() < 10)
            return true;

        return false;
    }

    private bool ForgetPlayer()
    {
        if (DistanceToPlayer() > 20)
            return true;

        return false;
    }
}
