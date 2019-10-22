using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public UIManager uiManager;
    public List<GameObject> enemyPrefabs;
    public List<Transform> spawnerLocations;

    public GameObject endLocation;
    public AudioSource catMeow;

    public GameObject fireBall;

    private List<EnemyScript> enemies;
    private List<ProjectileScript> projectiles;

    private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<EnemyScript>();
        projectiles = new List<ProjectileScript>();
        StartCoroutine("WaitAndSpawn");
        StartCoroutine("WaitAndCull");
    }

    void Update()
    {
        ProjectileScript p;
        EnemyScript e;

        for (int i = 0; i < projectiles.Count; i++)
        {
            p = projectiles[i];
            for (int j = 0; j < enemies.Count; j++)
            {   
                e = enemies[j];
                if (Vector2.Distance(new Vector2(p.gameObject.transform.position.x,
                    p.gameObject.transform.position.z), new Vector2(e.gameObject.transform.position.x,
                    e.gameObject.transform.position.z)) <= 0.5f)
                {
                    e.TakeDamage(100);
                    if (e.Health <= 0)
                    {
                        enemies.RemoveAt(j);
                        Destroy(e.gameObject);
                        j--;
                    }
                    projectiles.RemoveAt(i);
                    Destroy(p.gameObject);
                    i--;
                }
            }
        }
    }

    private IEnumerator WaitAndSpawn()
    {
        while (true)
        {
            enemies.Add(Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)],
                spawnerLocations[Random.Range(0, spawnerLocations.Count)].transform.position,
                Quaternion.identity).GetComponent<EnemyScript>());
            yield return new WaitForSeconds(Random.Range(2.0f, 4.0f));
        }
    }

    private IEnumerator WaitAndCull()
    {
        while (true)
        {
            if (enemies.Count > 0 && (enemies[0].transform.position.x <= endLocation.transform.position.x))
            {
                EnemyScript e = enemies[0];
                enemies.RemoveAt(0);
                Destroy(e.gameObject);

                health -= 10;
                if (health <= 0)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
                }
                else
                {
                    uiManager.UpdateHealth(health);
                    catMeow.Play();
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void DamageInRadius(Vector3 position, float radius)
    {
        EnemyScript e;

        for (int i = 0; i < enemies.Count; i++)
        {
            e = enemies[i];
            if (Vector3.Distance(position, e.transform.position) <= radius)
            {
                e.TakeDamage(100);
                if (e.Health <= 0)
                {
                    enemies.RemoveAt(i);
                    Destroy(e.gameObject);
                    i--;
                }
            }
        }
    }

    public void SpawnFireBolt(Vector3 spawn, Vector3 end)
    {
        GameObject go = Instantiate(fireBall, spawn, Quaternion.identity);
        go.transform.LookAt(end, Vector3.up);

        projectiles.Add(go.GetComponent<ProjectileScript>());
    }
}
