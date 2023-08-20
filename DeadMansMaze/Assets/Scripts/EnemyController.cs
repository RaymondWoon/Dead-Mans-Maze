using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator anim;
    float xMove = 1f;
    float yMove = 1f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //HideCursor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetBool("isMoving", true);

        anim.SetFloat("x", xMove, 0.25f, Time.deltaTime);
        anim.SetFloat("y", yMove, 0.25f, Time.deltaTime);
    }
}
