using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoxxiProjectile : MonoBehaviour
{

    [SerializeField] private float speed = 70f;
    private Vector3 velocity;

    void Update()
    {
        velocity = Vector3.forward * speed * Time.deltaTime;
        transform.Translate(velocity);

    }
    private void OnColissionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}

