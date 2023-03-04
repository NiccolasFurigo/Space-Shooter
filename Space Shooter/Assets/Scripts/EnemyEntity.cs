using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] protected float velocity;
    [SerializeField] protected int life;
    protected float shotSpeed = -5f;
    protected float waitShot = 1f;
    [SerializeField] protected GameObject shot;
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
    //Destroying when exiting the screen
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
        }
    }
    //Destroying when touching the player
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player01"))
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            other.gameObject.GetComponent<PlayerController>().loseLife(1);
        }
    }
}
