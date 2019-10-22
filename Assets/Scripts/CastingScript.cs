using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingScript : MonoBehaviour
{
    enum Spells { none, fireball, swamp, wall };

    public MobSpawner mobSpawner;
    public UIManager uiManager;
    public TypingScript typingScript;

    public GameObject player;

    public GameObject aoeIndicator;
    public LineRenderer lineIndicator;
    public GameObject boxIndicator;

    public GameObject fireExplosion;
    public GameObject fireBall;
    public GameObject swampPrefab;
    public GameObject wallPrefab;

    private bool aoeActive;
    private bool lineActive;

    private int layerMask;

    private int mana;

    private Spells activeSpell;

    // Start is called before the first frame update
    void Start()
    {
        activeSpell = Spells.none;
        layerMask = 1 << 8;
        mana = 20;
        aoeIndicator.SetActive(false);
        boxIndicator.SetActive(false);
        lineIndicator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeSpell == Spells.fireball)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layerMask))
                aoeIndicator.transform.position = new Vector3(hit.point.x, aoeIndicator.transform.position.y, hit.point.z);

            if (Input.GetMouseButtonDown(0))
            {
                mobSpawner.DamageInRadius(aoeIndicator.transform.position, 2.0f);
                Instantiate(fireExplosion, aoeIndicator.transform.position, Quaternion.identity);

                mana -= 20;
                uiManager.UpdateMana(mana);

                aoeIndicator.SetActive(false);
                activeSpell = Spells.none;
                typingScript.FinishedCasting();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                aoeIndicator.SetActive(false);
                activeSpell = Spells.none;
                typingScript.FinishedCasting();
            }
        }

        if (activeSpell == Spells.swamp)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layerMask))
                aoeIndicator.transform.position = new Vector3(hit.point.x, aoeIndicator.transform.position.y, hit.point.z);

            if (Input.GetMouseButtonDown(0))
            {
                GameObject swamp = Instantiate(swampPrefab, aoeIndicator.transform.position, Quaternion.identity);
                Destroy(swamp, 8.0f);

                mana -= 8;
                uiManager.UpdateMana(mana);

                aoeIndicator.SetActive(false);
                activeSpell = Spells.none;
                typingScript.FinishedCasting();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                aoeIndicator.SetActive(false);
                activeSpell = Spells.none;
                typingScript.FinishedCasting();
            }
        }

        if (activeSpell == Spells.wall)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layerMask))
                boxIndicator.transform.position = new Vector3(hit.point.x, boxIndicator.transform.position.y, hit.point.z);

            if (Input.GetMouseButtonDown(0))
            {
                GameObject wall = Instantiate(wallPrefab, boxIndicator.transform.position, wallPrefab.transform.rotation);
                Destroy(wall, 5.0f);

                mana -= 14;
                uiManager.UpdateMana(mana);

                boxIndicator.SetActive(false);
                activeSpell = Spells.none;
                typingScript.FinishedCasting();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                boxIndicator.SetActive(false);
                activeSpell = Spells.none;
                typingScript.FinishedCasting();
            }
        }

        if (lineActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, layerMask))
            {
                lineIndicator.SetPosition(0, new Vector3(hit.point.x, lineIndicator.GetPosition(0).y, hit.point.z));
            }

            if (Input.GetMouseButtonDown(0))
            {
                mobSpawner.SpawnFireBolt(lineIndicator.GetPosition(1), lineIndicator.GetPosition(0));

                mana -= 6;
                uiManager.UpdateMana(mana);

                lineIndicator.enabled = false;
                lineActive = false;
                typingScript.FinishedCasting();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                lineIndicator.enabled = false;
                lineActive = false;
                typingScript.FinishedCasting();
            }
        }
    }

    //TODO - put radius size as argument
    public void CastFireBall()
    {
        if (mana >= 20)
        {
            aoeIndicator.SetActive(true);
            aoeIndicator.transform.localScale = new Vector3(4f, 4f, 1f);
            activeSpell = Spells.fireball;
        }
    }

    public void CastFireBolt()
    {
        if (mana >= 6)
        {
            lineIndicator.enabled = true;
            lineActive = true;
        }
    }

    public void CastSwamp()
    {
        if (mana >= 8)
        {
            aoeIndicator.SetActive(true);
            aoeIndicator.transform.localScale = new Vector3(2f, 3f, 1f);
            activeSpell = Spells.swamp;
        }
    }

    public void CastWall()
    {
        if (mana >= 14)
        {
            boxIndicator.SetActive(true);
            activeSpell = Spells.wall;
        }
    }

    public void AddMana(int amt)
    {
        mana += amt;
        if (mana > 20)
            mana = 20;
        uiManager.UpdateMana(mana);
    }

    public int Mana
    {
        get { return mana; }
    }
}
