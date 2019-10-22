using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellListItem : MonoBehaviour
{
    public UnityEngine.UI.Text textItem;
    public int manaCost = 0;

    private string text;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("set text for " + textItem.text);
        text = textItem.text;
    }

    public string Text
    {
        get { return text; }
    }
}
