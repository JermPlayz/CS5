using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image healthBar;

    public void SetHP(float currentHP)
    {
        healthBar.fillAmount = currentHP;
    }
}