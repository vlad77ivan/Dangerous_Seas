using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public int health = 100; 
    public float speed = 3f;
    public float sceneSpeed = 0.5f;
    public float rateOfFire = 5f;
    public int damage = 100;
    public int baseHealth = 100;

    [SerializeField] Projectile projectile;
    [SerializeField] Transform gunPoint;

    private Vector3 moveVec;
    private float nextTimeToFIre = 0f;

    void Update()
    {
        if(health <= 0)
        {
            GameObject hud = GameObject.FindWithTag("HUD");
            hud.GetComponent<HUDManager>().EnableMenu();

            Destroy(gameObject);
        }

        Movement();
    }

    private void Movement()
    {
        // Camera movement
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.z += Time.deltaTime * sceneSpeed;
        Camera.main.transform.position = cameraPos;

        // Player movement
        transform.position += moveVec * speed * Time.deltaTime + new Vector3(0, 0, Time.deltaTime * sceneSpeed);

        // Limit movement to viewport
        KeepInsideViewport();
    }

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        moveVec = new Vector3(inputVec.x, 0, inputVec.y);
    }

    private void KeepInsideViewport()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        float oldY = transform.position.y;

        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        
        Vector3 newPos = Camera.main.ViewportToWorldPoint(pos);
        transform.position = new Vector3(newPos.x, oldY, newPos.z);
    }

    public void OnShoot()
    {
        if(nextTimeToFIre < Time.time) 
        {
            Instantiate(projectile, gunPoint.position, Quaternion.Euler(new Vector3(90f, 0f, 0f)));

            nextTimeToFIre = Time.time + 1f / rateOfFire;
        }
    }

    public float GetRateOfFIre()
    {
        return rateOfFire;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetDamage()
    {
        return damage;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetBaseHealth()
    {
        return baseHealth;
    }

    public void DealDamage(int damage)
    {
        health -= damage;
    }

    public void AddBonus(string name)
    {
        switch (name) 
        {
            case "turbo": 
                speed *= 2;
                break;

            case "magazine":
                rateOfFire *= 2;
                break;

            default:
                break;
        }
    }

    public void AddHealth(int bonusHealth)
    {
        health += bonusHealth;

        health = Mathf.Clamp(health, 0, baseHealth);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Finish"))
        {
            GameObject hud = GameObject.FindWithTag("HUD");
            hud.GetComponent<HUDManager>().EnableMenu();

            Destroy(gameObject);

            return;
        }
       
        GameObject upgradesPanel = GameObject.FindWithTag("UpgradesPanel");

        upgradesPanel.GetComponent<UpgradesUI>().AddItem(collider.gameObject.tag);

        Destroy(collider.gameObject);
    }
}
