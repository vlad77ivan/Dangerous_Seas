using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{

    [SerializeField] Slider healthbar;
    [SerializeField] PlayerController player;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI playerStatsText;
    public GameObject menu;

    private int score = 0;
    private int baseHealth;

    private string statsSpeed = "Boat speed: ";
    private string statsRateOfFire = "Rate of fire: ";
    private string statsDamage = "Damage: ";

    void Awake()
    {
        baseHealth = player.GetBaseHealth();
    }

    void Update()
    {
        scoreText.SetText(score.ToString());
        playerStatsText.SetText(statsSpeed + player.GetSpeed().ToString() + "\n" 
            + statsRateOfFire + player.GetRateOfFIre().ToString() + "\n"
            + statsDamage + player.GetDamage().ToString());

        healthbar.value = 1f * player.GetHealth() / baseHealth * 100;
    }

    public void AddPoints(int points)
    {
        score += points;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void EnableMenu()
    {
        menu.SetActive(true);
    }
}
