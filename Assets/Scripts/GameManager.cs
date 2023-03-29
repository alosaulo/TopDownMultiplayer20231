using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject prefabSlime;
    
    public List<Transform> Spawns;
    
    public float tempoDeSpawn;

    public List<PlayerController> Players;

    public static GameManager instance;

    void Awake() { 
        instance= this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spawnar();
    }

    bool spawnar = false;
    float contaSpawn;
    void Spawnar() {
        if (spawnar == false)
        {
            contaSpawn += Time.deltaTime;
            if (contaSpawn >= tempoDeSpawn)
            {
                contaSpawn = 0;
                spawnar = true;
            }
        }
        else 
        {
            int spawnAleatorio = Random.Range(0, Spawns.Count);
            Instantiate(prefabSlime, Spawns[spawnAleatorio].transform.position,Quaternion.identity);
            spawnar = false;
        }
    }

}
