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
            collision.transform.GetComponent<PlayerAction>().status.AddHp(-damage);
            Debug.Log("Player hp: " + collision.transform.GetComponent<PlayerAction>().status.GetCurrentHp().ToString());
        }
        if (collision.gameObject.tag.ToLower() == "enemy")
        {
            Debug.Log("Damaged enemy!");
            //collision.transform.GetComponent<EnemyController>().status.AddHp(-damage);
        }
        Destroy(gameObject);
    }
}
