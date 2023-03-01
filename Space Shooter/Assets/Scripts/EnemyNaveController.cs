using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNaveController : EnemyEntity
{
    private Rigidbody2D myRB;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myRB.velocity = new Vector2(0f, velocity);
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        bool visible = GetComponentInChildren<SpriteRenderer>().isVisible;

        if (visible)
        {
            //Wait Shot
            waitShot -= Time.deltaTime;
            if (waitShot <= 0)
            {
                //Creating the shot
                Instantiate(shot, transform.position, transform.rotation);
                //Restarting
                waitShot = Random.Range(1.5f, 2f);
            }
        }
    }
}
