using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] protected float velocity;
    [SerializeField] protected int life;
    [SerializeField] protected GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loseLife(int damage)
    {
        life -= damage;
        if(life <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
