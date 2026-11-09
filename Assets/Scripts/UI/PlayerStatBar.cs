using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image powerImage;


    private void Update()
    {
        if(healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime * 0.5f;
        }
    }

    /// <summary>
    /// Receives the updated health percentage and updates the health bar accordingly.
    /// </summary>
    /// <param name="percentage">percentage: Current/Max</param>
    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount = percentage;
    }
}
