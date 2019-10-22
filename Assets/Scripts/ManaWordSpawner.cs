using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaWordSpawner : MonoBehaviour
{
    private string[] spookyWords = new string[] { "afraid", "alien", "afterlife", "autumn", "bizarre", "blood", "bones", "bat", "bogeyman",
        "broomstick", "cackle", "chilling", "cobweb", "costume", "creepy", "cadaver", "carve", "cauldron", "coffin", "cowboy", "candy", "casket",
        "cemetery", "clown", "corpse", "cowgirl", "crypt", "death", "devil", "demon", "dreadful", "eerie", "evil", "eyeballs", "eyepatch", "fangs", "fright",
        "frighten", "fear", "fog", "ghostly", "goblin", "grim", "gruesome", "ghastly", "ghoul", "goodies", "grave", "ghost", "gravestone", "gory", "haunt",
        "headstone", "halloween", "haunted", "hayride", "howl", "lantern", "mist", "moonlight", "mummy", "magic", "monster", "midnight", "morbid", "nightmare",
        "october", "otherworldly", "party", "phantom", "poltergeist", "pumpkin", "pirate", "potions", "phantasm", "scare", "scream", "skull", "spooky",
        "sweets", "scarecrow", "shadow", "spider", "spirit", "supernatural", "scary", "shadowy", "skeleton", "specter", "spook", "superstition", "treat",
        "thrilling", "tomb", "treat", "terrify", "tombstone", "unearthly", "vampire", "vanish", "warlock", "werewolf", "witch", "witchcraft", "zombie"};

    public GameObject manaWordPrefab;
    public UnityEngine.UI.Image manaWordSpawn;
    public UnityEngine.UI.Image manaWordEnd;

    private List<UnityEngine.UI.Text> wordList;

    // Start is called before the first frame update
    void Start()
    {
        wordList = new List<UnityEngine.UI.Text>();
        StartCoroutine("WaitAndSpawn");
        StartCoroutine("WaitAndCull");
    }

    public bool CheckForWord(string word)
    {
        for (int i = 0; i < wordList.Count; i++)
        {
            if (wordList[i].text.Equals(word.Substring(0, word.Length - 1)))
            {
                wordList[i].text = "";
                wordList.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    private IEnumerator WaitAndSpawn()
    {
        while (true)
        {
            GameObject manaWord = Instantiate(manaWordPrefab, manaWordSpawn.transform);
            UnityEngine.UI.Text manaWordText = manaWord.GetComponent<TextMoveScript>().manaWord;
            wordList.Add(manaWordText);
            manaWordText.text = spookyWords[Random.Range(0, spookyWords.Length)];
            yield return new WaitForSeconds(2.0f);
        }
    }

    private IEnumerator WaitAndCull()
    {
        while (true)
        {
            if (wordList.Count > 0 && wordList[0].rectTransform.position.x - manaWordEnd.rectTransform.position.x <= 0)
            {
                Destroy(wordList[0].gameObject);
                wordList.RemoveAt(0);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}
