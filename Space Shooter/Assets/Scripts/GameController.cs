using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private int point = 0;
    private int levelBase = 100;
    [SerializeField] private int level = 1;
    private float waitEnemy = 0f;
    [SerializeField] private float waitTime = 5f;
    [SerializeField] private int amoutEnemy = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       GenerateEnemy();
    }

    public void EarnPoints(int point)
    {
        this.point += point;
        if(this.point > levelBase * level)
        {
            level++;
        }
    }

    public void DecreaseAmout()
    {
        amoutEnemy--;
    }

    void GenerateEnemy()
    {
        if(waitEnemy > 0)
        {
            waitEnemy -= Time.deltaTime;
        }

        if (waitEnemy <= 0f && amoutEnemy <= 0)
        {
            // Created Waves
            int amout = level * 4;
            while(amoutEnemy < amout)
            {
                //Creating enemies based on level
                GameObject enemyCreated;
                float chance = Random.Range(0f, level);
                if (chance > 2f)
                {
                    enemyCreated = enemies[1];
                }
                else
                {
                    enemyCreated = enemies[0];
                }

                Vector3 position = new Vector3(Random.Range(-8f, 8f), Random.Range(6f, 15f), 0f);
                Instantiate(enemyCreated, position, transform.rotation);
                amoutEnemy++;
                waitEnemy = waitTime;
            }
        }
        
    }
}
