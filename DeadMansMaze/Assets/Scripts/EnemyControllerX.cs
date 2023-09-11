using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerX : MonoBehaviour
{
    private string behaviour;
    private Animator anim;
    private GameObject player;
    private int hp = 500;
    private int maxHp = 500;
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

    public int GetHp()
    {
        return hp;
    }

    public void AddHp(int s)
    {
        if (hp + s <= 0)
        {
            die();
            return;
        }
        if (hp + s > maxHp)
        {
            hp = maxHp;
            return;
        }
        hp += s;
    }

    private void die()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        agent.SetDestination(player.transform.position);

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

        bool shouldMove = agent.remainingDistance > agent.stoppingDistance;

        // Update animation parameters
        anim.SetBool("isMoving", shouldMove);
        anim.SetFloat("x", velocity.x, 0.25f, Time.deltaTime);
        anim.SetFloat("y", velocity.y, 0.25f, Time.deltaTime);
        //
        // GetComponent<LookAt>().lookAtTargetPosition = agent.steeringTarget + transform.forward;
    }

    void OnAnimatorMove()
    {
        // Update position to agent position
        transform.position = agent.nextPosition;
    }
}
