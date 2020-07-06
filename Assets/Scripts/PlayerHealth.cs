using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth;
    [SerializeField] float maxHealth = 100;
    [SerializeField] Slider slider;
    [SerializeField] Image healthBarFillColor;
    [SerializeField] Color maxHealthColor;
    [SerializeField] Color minHealthColor;

    public void Start()
    {
        playerHealth = maxHealth;
    }


    public void ReducePlayerHealth(float damage)
    {
        playerHealth -= damage;
        float healthPre = (playerHealth / maxHealth) * 100;
        slider.value = healthPre;
        healthBarFillColor.color = Color.Lerp(minHealthColor, maxHealthColor, healthPre / 100);
        if (playerHealth == 0)
            {

                GetComponent<DeathHandler>().DeathHandle();

            } 
    }
   

    
    
}