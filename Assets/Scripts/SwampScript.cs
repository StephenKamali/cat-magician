using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyScript e = other.GetComponent<EnemyScript>();
        if (e != null)
        {
            e.SlowForDuration(0.6f, 5.0f);
        }
    }
}
