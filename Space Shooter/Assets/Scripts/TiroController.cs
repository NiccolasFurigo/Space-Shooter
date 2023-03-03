using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{
    //Variables
    private Rigidbody2D myRB;
    [SerializeField] private GameObject impact;
    [SerializeField] private float velocity = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //Catching the rigidbody
        myRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Destroying the shot
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyEntity>().loseLife(1);
        }

        if (collision.CompareTag("Player01"))
        {
            collision.GetComponent<PlayerController>().loseLife(1);
        }
        Destroy(gameObject);
        Instantiate(impact, transform.position, transform.rotation);
    }
}
