using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : EnemyEntity
{
    //Variables
    private Rigidbody2D myRB;
    private float waitShot;

    [SerializeField] private GameObject shot; 
    [SerializeField] private Transform shotPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        //Catching the rigidbody
        myRB = GetComponent<Rigidbody2D>();
        myRB.velocity = new Vector2(0f, velocity);
        //Random shot wait
        waitShot = Random.Range(0.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //Checking the sprite render
        bool visible = GetComponentInChildren<SpriteRenderer>().isVisible;

        if (visible)
        {
            //Wait Shot
            waitShot -= Time.deltaTime;
            if (waitShot <= 0)
            {
                //Creating the shot
                Instantiate(shot, shotPosition.position, transform.rotation);
                //Restarting
                waitShot = Random.Range(1.5f, 2f);
            }
        }
    }
    
}
