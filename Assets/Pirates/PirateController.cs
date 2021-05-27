using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{
    public float rateOfFire = 5f;
    public int points = 100;

    [SerializeField] Cannonball cannonball;
    [SerializeField] Transform gunPoint;

    private float nextTimeToFIre = 0f;

    void Update()
    {
        if(InsideViewPort())
        {
            Attack();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("PlayerProjectile"))
        {
            GameObject hud = GameObject.FindWithTag("HUD");
            hud.GetComponent<HUDManager>().AddPoints(points);

            Destroy(gameObject);
        }
    }

    private bool InsideViewPort()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = pos.z > 0 && pos.x > 0 && pos.x < 1 && pos.y > 0 && pos.y < 1;

        return onScreen;
    }

    private void Attack()
    {
        if(nextTimeToFIre < Time.time)
        {
            Vector3 direction = (GameObject.Find("Player").transform.position - transform.position).normalized;
            direction.y = gunPoint.transform.position.y;

            Cannonball cb = Instantiate(cannonball, gunPoint.position, Quaternion.Euler(new Vector3(90f, 0f, 180f)));
            cb.SetDirection(direction);

            nextTimeToFIre = Time.time + 1f / rateOfFire;
        }
    }
}
