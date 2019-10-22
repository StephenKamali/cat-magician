using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingScript : MonoBehaviour
{
    public CastingScript castingScript;
    public ManaWordSpawner manaWordSpawner;
    public UnityEngine.UI.InputField textBox;

    public AudioSource manaSound;
    public AudioSource pageTurn;
    
    public UnityEngine.UI.Text elementFireText;
    public UnityEngine.UI.Text elementWaterText;
    public UnityEngine.UI.Text elementEarthText;

    public GameObject fireSpellPage;
    public GameObject waterSpellPage;
    public GameObject earthSpellPage;

    public List<SpellListItem> fireSpellsText;
    public List<SpellListItem> waterSpellsText;
    public List<SpellListItem> earthSpellsText;

    private bool fireActive;
    private bool waterActive;
    private bool earthActive;

    private string currentText;
    private Color defaultColor;

    private bool jankyUnityWorkaround;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = elementFireText.color;

        //Clear and select textbox right at beginning of game
        textBox.text = "";
        textBox.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentText = textBox.text.ToLower();
            if (currentText.Equals("fire ball\n"))
            {
                castingScript.CastFireBall();
            }
            else if (currentText.Equals("fire bolt\n")) {
                castingScript.CastFireBolt();
            }
            else if (currentText.Equals("earth swamp\n"))
            {
                castingScript.CastSwamp();
            }
            else if (currentText.Equals("earth wall\n"))
            {
                castingScript.CastWall();
            }
            else if (manaWordSpawner.CheckForWord(currentText)) {
                castingScript.AddMana(5);
                manaSound.Play();
            }
            textBox.text = "";
        }
    }

    public void TextChanged()
    {
        //TODO - could make this more efficient - keep a substring of x length, and don't update it unless Length changes to less than
        currentText = textBox.text.ToLower();

        if (currentText.Length == "fire".Length)
        {
            if (currentText.Substring(0, 4).Equals("fire"))
            {
                pageTurn.Play();
                elementFireText.color = Color.red;
                fireActive = true;
            }
        }
        else if (currentText.Length < "fire".Length)
        {
            elementFireText.color = defaultColor;
            fireActive = false;
        }

        if (currentText.Length == "water".Length)
        {
            if (currentText.Substring(0, 5).Equals("water"))
            {
                pageTurn.Play();
                elementWaterText.color = Color.blue;
                waterActive = true;
            }

            if (currentText.Substring(0, 5).Equals("earth"))
            {
                pageTurn.Play();
                elementEarthText.color = Color.green;
                earthActive = true;
            }
        }
        else if (currentText.Length < "water".Length)
        {
            elementWaterText.color = defaultColor;
            elementEarthText.color = defaultColor;
            waterActive = false;
            earthActive = false;
        }

        if (fireActive)
        {
            fireSpellPage.SetActive(true);
            HandleSpellPage(fireSpellsText, "fire", Color.red);
        }
        else
        {
            fireSpellPage.SetActive(false);
        }

        if (waterActive)
        {
            waterSpellPage.SetActive(true);
            HandleSpellPage(waterSpellsText, "water", Color.blue);
        }
        else
        {
            waterSpellPage.SetActive(false);
        }

        if (earthActive)
        {
            earthSpellPage.SetActive(true);
            HandleSpellPage(earthSpellsText, "earth", Color.green);
        }
        else
        {
            earthSpellPage.SetActive(false);
        }
    }

    public void TextFinished()
    {
        //textBox.Select();
    }

    public void FinishedCasting()
    {
        textBox.Select();
    }

    private void HandleSpellPage(List<SpellListItem> spellItems, string element, Color activeColor)
    {
        foreach (SpellListItem spell in spellItems)
        {
            if (castingScript.Mana >= spell.manaCost)
            {
                if (currentText.Length == (element.Length + spell.Text.Length + 1))
                {
                    if (spell.Text.Equals(currentText.Substring(element.Length + 1, spell.Text.Length)))
                    {
                        spell.textItem.color = activeColor;
                    }
                }
                else
                {
                    spell.textItem.color = defaultColor;
                }
            }
            else
            {
                //Not enough mana
                spell.textItem.color = Color.gray;
            }
        }
    }
}
