using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;

    public void UpdateHealthBar(float health, float maxHealth) {
    healthBarImage.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1f);
    }
    
}
