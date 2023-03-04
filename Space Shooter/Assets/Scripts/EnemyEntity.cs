using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] protected float velocity;
    [SerializeField] protected int life;
    [SerializeField] protected float shotSpeed = -5f;
    [SerializeField] protected float waitShot = 1f;
    [SerializeField] protected GameObject shot;
    [SerializeField] protected GameObject explosion;
    [SerializeField] protected int points = 10;
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
        if (transform.position.y < 5f)
        {
            life -= damage;
            if (life <= 0)
            {
                Destroy(gameObject);
                Instantiate(explosion, transform.position, transform.rotation);

                var generator = FindObjectOfType<GameController>();
                generator.DecreaseAmout();
                generator.EarnPoints(points);

            }
        }
    }
    //Destroying when exiting the screen
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
            var generator = FindObjectOfType<GameController>();
            generator.DecreaseAmout();
        }
    }
    //Destroying when touching the player
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player01"))
        {
            Destroy(gameObject);
            var generator = FindObjectOfType<GameController>();
            generator.DecreaseAmout();
            Instantiate(explosion, transform.position, transform.rotation);
            other.gameObject.GetComponent<PlayerController>().loseLife(1);
        }
    }
}
