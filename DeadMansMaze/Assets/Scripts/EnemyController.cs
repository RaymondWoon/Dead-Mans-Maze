using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private string behaviour;
    private Animator anim;
    private float xMove = 1f;
    private float yMove = 1f;
    private float rotationSpeed = 3f;
    private GameObject player;
    private int hp = 100;
    private int maxHp = 100;
    private NavMeshAgent agent;

    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 2;
        // Don’t update position automatically
        agent.updatePosition = false;
        behaviour = "chase";
    }

    public void SetBehaviour(string b)
    {
        behaviour = b;
        return;
    }

    public void AddHp(int s)
    {
        if (maxHp > hp + s)
        {
            hp = maxHp;
            return;
        }
        hp += s;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        Vector3 direction = player.transform.position - this.transform.position;//agent.nextPosition - this.transform.position;
        anim.SetBool("isMoving", true);//!agent.isStopped);
        anim.SetFloat("x", direction.x, 0.25f, Time.deltaTime);
        anim.SetFloat("y", direction.y, 0.25f, Time.deltaTime);
        */

        agent.SetDestination(player.transform.position);

        //Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        Vector3 worldDeltaPosition = player.transform.position - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = agent.remainingDistance > agent.stoppingDistance;// && velocity.magnitude > 0.5f;

        // Update animation parameters
        //Debug.Log(shouldMove);
        //Debug.Log(velocity);
        anim.SetBool("isMoving", shouldMove);
        anim.SetFloat("x", velocity.x, 0.25f, Time.deltaTime);
        anim.SetFloat("y", velocity.y, 0.25f, Time.deltaTime);
        //
        //GetComponent<LookAt>().lookAtTargetPosition = agent.steeringTarget + transform.forward;
    }

    void OnAnimatorMove()
    {
        // Update position to agent position
        transform.position = agent.nextPosition;
    }
}
