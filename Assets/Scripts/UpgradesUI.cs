using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesUI : MonoBehaviour
{
    public Image upgrade_1;
    public Image upgrade_2;
    public Image upgrade_3;

    public int itemHealth = 20;

    public Sprite turbo;
    public Sprite magazine;

    private Dictionary<string, Sprite> items = new Dictionary<string, Sprite>();

    void Awake()
    {
        upgrade_1.enabled = false;
        upgrade_2.enabled = false;
        upgrade_3.enabled = false;

        items.Add("turbo", turbo);
        items.Add("magazine", magazine);
    }

    void Update()
    {
        
    }

    public void AddItem(string name)
    {
        if(upgrade_1.enabled && (upgrade_1.sprite == items[name]))
        {
            if (upgrade_2.enabled)
            {
                if(upgrade_3.enabled)
                {
                    return;
                }

                upgrade_3.sprite = items[name];
                upgrade_3.enabled = true;

                StartCoroutine(ItemsCombo(name));
            }
            else
            {
                upgrade_2.sprite = items[name];
                upgrade_2.enabled = true;
            }
        } 
        else
        {
            if (upgrade_1.enabled) 
            {
                int health = itemHealth;

                if (upgrade_2.enabled) 
                {
                    health *= 2;
                }

                GameObject player = GameObject.FindWithTag("Player");
                player.GetComponent<PlayerController>().AddHealth(health);
            }


            upgrade_1.enabled = true;
            upgrade_2.enabled = false;
            upgrade_3.enabled = false;

            upgrade_1.sprite = items[name];
        }
    }

    IEnumerator ItemsCombo(string name)
    {
        yield return new WaitForSeconds(1.5f);

        upgrade_1.enabled = false;
        upgrade_2.enabled = false;
        upgrade_3.enabled = false;

        upgrade_1.sprite = null;

        GameObject player = GameObject.FindWithTag("Player");
        try
        {
            player.GetComponent<PlayerController>().AddBonus(name);
        }
        catch (System.Exception)
        {
        }
    }
}
