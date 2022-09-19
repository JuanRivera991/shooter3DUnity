using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public Enemy enemy;
    public float spawnTime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    void Spawn()
    {
        Instantiate(enemy, this.transform.position, this.transform.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
