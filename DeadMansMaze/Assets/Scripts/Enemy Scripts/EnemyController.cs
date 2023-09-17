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

    [Header("Range")]
    [SerializeField] private float _chaseDistance = 10.0f;
    [SerializeField] private float _forgetDistance = 15.0f;

    [Header("Stats")]
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip _alert;
    [SerializeField] private AudioClip _walking;
    [SerializeField] private AudioClip _running;
    [SerializeField] private AudioClip _attack;
    [SerializeField] private AudioClip[] _strikes;
    [SerializeField] private AudioClip _pain;
    [SerializeField] private AudioClip _die;

    // Components
    private GameObject _player;
    private Animator _anim;
    private NavMeshAgent _agent;
    private AudioSource _audioSource;

    // Enemy states
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
        // Initialize components
        _player = GameObject.FindWithTag("Player");
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
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
                
                if (_agent.remainingDistance <= _agent.stoppingDistance + 1 && !_agent.pathPending)
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

                //_audioSource.clip = _attack;
                //_audioSource.Play();

                if (DistanceToPlayer() > _agent.stoppingDistance + 1)
                    _state = STATE.CHASE;
                break;

            case STATE.DEAD:

                break;
        }
    }

    // Set all anim bool states to false
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

    // Check if the player is within visible range
    private bool IsPlayerVisible()
    {
        if (DistanceToPlayer() < _chaseDistance)
            return true;

        return false;
    }

    // Check if player is out of range
    private bool ForgetPlayer()
    {
        if (DistanceToPlayer() > _forgetDistance)
            return true;

        return false;
    }

    // apply damage to player
    private void DamagePlayer()
    {
        if (_state == STATE.ATTACK)
        {
            _player.GetComponent<PlayerAction>().TakeHit(_damage);
            //PlayStrikeAudio();
        }
    }

    public void DamageEnemy(int Damage)
    {
        _health = _health - Damage;

        if (_health <= 0)
        {
            _anim.SetBool("isDead", true);
        }
    }

    //private void PlayStrikeAudio()
    //{
    //    _audioSource.clip = _strikes[Random.Range(0, _strikes.Length - 1)];
    //    _audioSource.Play();
    //}
}