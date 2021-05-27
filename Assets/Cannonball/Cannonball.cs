using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public int damage = 30;
    public float cannonballSpeed = 12f;
    public Rigidbody rb;
    private Vector3 direction;

    void Update()
    {
        if(!InsideViewPort())
        {
            Destroy(gameObject);
            return;
        }

        rb.velocity = direction * cannonballSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().DealDamage(damage);
        }

        Destroy(gameObject);
    }

    private bool InsideViewPort()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = pos.z > 0 && pos.x > 0 && pos.x < 1 && pos.y > 0 && pos.y < 1;

        return onScreen;
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }
}
