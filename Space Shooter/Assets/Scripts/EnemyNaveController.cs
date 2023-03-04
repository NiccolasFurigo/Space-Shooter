using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNaveController : EnemyEntity
{
    private Rigidbody2D myRB;
    [SerializeField] private Transform shotPosition;
    private float yMax = 2.5f;
    private bool permissionMove = true;
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

        if (transform.position.y < yMax && permissionMove)
        {
            if(transform.position.x < 0)
            {
                myRB.velocity = new Vector2(-velocity, velocity);
                permissionMove = false;
            } 
            else
            {
                myRB.velocity = new Vector2(velocity, velocity);
                permissionMove = false;
            }
        }
    }

    private void Shooting()
    {
        bool visible = GetComponentInChildren<SpriteRenderer>().isVisible;

        if (visible)
        {
            var player = FindObjectOfType<PlayerController>();
            if (player)
            {
                //Wait Shot
                waitShot -= Time.deltaTime;
                if (waitShot <= 0)
                {
                    //Creating the shot that follow
                    var myShot = Instantiate(shot, shotPosition.position, transform.rotation);

                    Vector2 direction = player.transform.position - myShot.transform.position;
                    direction.Normalize();
                    myShot.GetComponent<Rigidbody2D>().velocity = direction * (-shotSpeed);
                    //Adjusting the angle
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    myShot.transform.rotation = Quaternion.Euler(0f, 0f, (angle + 90));


                    //Restarting
                    waitShot = Random.Range(1.5f, 3f);
                }
            }
        }
    }
}
