using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    public int damage = 25;
    public int speed = 10;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.ToLower() == "player")
        {
            int currHp = collision.transform.GetComponent<PlayerAction>().AddHp(-damage);
            Debug.Log("Player hp: " + currHp.ToString());
        }
        if (collision.gameObject.tag.ToLower() == "enemy")
        {
            collision.transform.GetComponent<EnemyControllerX>().AddHp(-damage);
            Debug.Log("Enemy HP: " + collision.transform.GetComponent<EnemyControllerX>().GetHp().ToString());
        }
        Destroy(gameObject);
    }
}
