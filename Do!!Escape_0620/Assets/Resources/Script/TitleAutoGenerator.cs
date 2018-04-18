using UnityEngine;
using System.Collections;

public class TitleAutoGenerator : MonoBehaviour {
    public GameObject[] Blocks;
    public Transform[] Position;
    float Gene_tick=0;
    float now_tick=0;
    
	// Use this for initialization
	void Start () {
        Gene_tick = Random.Range(5, 8);

    }
	
	// Update is called once per frame
	void Update () {
        now_tick += Time.deltaTime;
        if (Gene_tick <= now_tick)
        {
            now_tick = 0;
            Instantiate(Blocks[Random.Range(0, Blocks.Length)], Position[Random.Range(0, Position.Length)].position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

            Gene_tick= Random.Range(1, 5);
        }


    }
}
