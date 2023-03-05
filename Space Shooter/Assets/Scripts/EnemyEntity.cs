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
    [SerializeField] protected GameObject powerUp;
    [SerializeField] protected float itemRate = 0.9f;
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
                generator.EarnPoints(points);
                DropItem();

            }
        }
    }
    //Destroying when exiting the screen
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Destroyer"))
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

    private void OnDestroy()
    {
        var generator = FindObjectOfType<GameController>();
        if (generator)
        {
            generator.DecreaseAmout();
        }
    }

    public void DropItem()
    {
        float chance = Random.Range(0f, 1f);

        if (chance > itemRate)
        {
            //Creating PowerUp
            GameObject pU = Instantiate(powerUp, transform.position, transform.rotation);
            Destroy(pU, 3f);
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            pU.GetComponent<Rigidbody2D>().velocity = dir;
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
