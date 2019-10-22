using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UnityEngine.UI.Image healthBar;
    public UnityEngine.UI.Image manaBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateMana(int mana)
    {
        manaBar.fillAmount = mana / (20.0f);
    }

    public void UpdateHealth(int health)
    {
        healthBar.fillAmount = health / (100.0f);
    }
}
