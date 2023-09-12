using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectBulletCasing : MonoBehaviour
{
  [SerializeField] float DestroyInSeconds;

  Rigidbody rb;

  // Start is called before the first frame update
  void Start()
  {
    // Spawn bullet casing with ejection force
    rb = GetComponent<Rigidbody>();
    rb.AddRelativeForce(0.1f, -0.5f, 0, ForceMode.Impulse);
    Destroy(gameObject, DestroyInSeconds);
  }
}