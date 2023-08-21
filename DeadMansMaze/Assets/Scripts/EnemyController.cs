using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        behaviour = "chase";
    }

    public void SetBehaviour(string b)
    {
        behaviour = b;
        return;
    }

    public void SetHp(int newHp)
    {
        if (newHp < hp)
        {
            hp = newHp;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (behaviour)
        {
            case "chase":

                if (Vector3.Distance(player.transform.position, this.transform.position) < 3)
                {
                    anim.SetBool("isMoving", false);
                    break;
                }
                Vector3 direction = player.transform.position - this.transform.position;
                float angle = Vector3.Angle(direction, this.transform.forward);

                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                        Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                anim.SetBool("isMoving", true);
                anim.SetFloat("x", direction.x, 0.25f, Time.deltaTime);
                anim.SetFloat("y", direction.z, 0.25f, Time.deltaTime);

                break;
            case "defend":
                break;
            case "random":
                break;
        }



        // When the player has all the keys, all enemies change their behaviour to chase.
        /* if(player.status.inventory.GetNumberOfKeyItems("key") = gamePrefs.numberOfKeys)
        {
            SetBehaviour("chase");
            return;
        }
        */
        // When the character is under attack, it changes its behaviour to defensive.
        /* if (hp < maxHp)
        { 
            behaviour = "defend";
            return;
        }
        */

    }
}
