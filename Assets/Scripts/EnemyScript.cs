using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public AudioSource hurtSound;

    private float moveSpeed = 0.3f;
    private float currentMoveSpeed;
    private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        currentMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * currentMoveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        hurtSound.Play();
    }

    public void SlowForDuration(float percent, float duration)
    {
        StartCoroutine(SlowAndWait(percent, duration));
    }

    private IEnumerator SlowAndWait(float percent, float duration)
    {
        currentMoveSpeed = currentMoveSpeed * (1.0f - percent);
        yield return new WaitForSeconds(duration);
        currentMoveSpeed = moveSpeed;
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }
}
