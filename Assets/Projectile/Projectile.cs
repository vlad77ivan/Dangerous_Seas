using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 12f;
    public Rigidbody rb;
    
    void Update()
    {
        if(!InsideViewPort())
        {
            Destroy(gameObject);
            return;
        }

        rb.velocity = new Vector3(0f, 0f, 1f) * projectileSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private bool InsideViewPort()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = pos.z > 0 && pos.x > 0 && pos.x < 1 && pos.y > 0 && pos.y < 1;

        return onScreen;
    }
}
