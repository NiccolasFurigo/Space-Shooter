using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int point = 0;
    private int levelBase = 100;
    private float waitEnemy = 0f;
    private bool animetionBoss = false;
    [SerializeField] private GameObject BossStart;
    [SerializeField] private int level = 1;
    [SerializeField] private float waitTime = 5f;
    [SerializeField] private int amoutEnemy = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (level < 5)
        {
            GenerateEnemy();
        } else
        {
            CreatingBoss();
        }
    }

    public void EarnPoints(int point)
    {
        this.point += point;
        if(this.point > levelBase * level)
        {
            level++;
        }
    }

    private void CreatingBoss()
    {
        if (amoutEnemy <= 0 && waitTime > 0)
        {
            waitTime -= Time.deltaTime;
            if (!animetionBoss && waitTime <= 0)
            {
                GameObject animBoss = Instantiate(BossStart, Vector3.zero, transform.rotation);
                Destroy(animBoss, 5f);
                animetionBoss = true;
            }
        }
    }

    public void DecreaseAmout()
    {
        amoutEnemy--;
    }

    private bool PositionCheck(Vector3 position, Vector3 size)
    {
        Collider2D hit = Physics2D.OverlapBox(position, size, 0f);
        if(hit != null)
        {
            return true;
        }
        else 
        { 
            return false; 
        }
    }

    private void GenerateEnemy()
    {
        if(waitEnemy > 0)
        {
            waitEnemy -= Time.deltaTime;
        }

        if (waitEnemy <= 0f && amoutEnemy <= 0)
        {
            // Created Waves
            int amout = level * 4;
            int attempt = 0;
            while(amoutEnemy < amout)
            {
                attempt++;
                if(attempt > 200)
                {
                    break;
                }

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
                bool collision = PositionCheck(position, enemyCreated.transform.localScale);
                if (collision) { continue; }
                Instantiate(enemyCreated, position, transform.rotation);
                amoutEnemy++;
                waitEnemy = waitTime;
            }
        }
        
    }
}
