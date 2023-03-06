using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyEntity
{
    //Variables
    private Rigidbody2D myRB;
    private bool positionPermission = true;
    private float shotDelay = 1f;
    private float waitShot2 = 1f;
    [SerializeField] private float horizontalLimit = 6f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private Transform shotPosition1;
    [SerializeField] private Transform shotPosition2;
    [SerializeField] private Transform shotPosition3;
    [SerializeField] private GameObject shot1;
    [SerializeField] private GameObject shot2;
    //Variables States
    private string state = "state1";
    [SerializeField] private string[] states;
    [SerializeField] private float waitStates = 10f;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //State Machine
        ChangeState();
        switch (state)
        {
            case "state1":
                State1();
                break;
            case "state2":
                State2();
                break;
            case "state3":
                State3();
                break;
        }
    }

    private void State1()
    {
        //Limiting the boss on the screen
        if ((transform.position.x >= horizontalLimit || transform.position.x <= -horizontalLimit) && positionPermission)
        {
            speed *= -1;
            positionPermission = false;
        } else if (transform.position.x < 2 && transform.position.x > -2)
        {
            positionPermission = true;
        }
        myRB.velocity = new Vector2(speed, 0f);
        //Shooting
        if (waitShot <= 0f)
        {
            Shot1();
            waitShot = shotDelay;
        } else
        {
            waitShot -= Time.deltaTime;
        }
        
    }

    private void State2() 
    {
        myRB.velocity = Vector2.zero;
        if (waitShot <= 0f)
        {
            Shot2();
            waitShot = shotDelay/2;
        } else
        {
            waitShot -= Time.deltaTime;
        }
    }

    private void State3() 
    {
        myRB.velocity = Vector2.zero;
        if (waitShot <= 0f)
        {
            Shot1();
            waitShot = shotDelay;
        } else
        {
            waitShot -= Time.deltaTime;
        }

        if (waitShot2 <= 0f)
        {
            Shot2();
            waitShot2 = shotDelay;
        }
        else
        {
            waitShot2 -= Time.deltaTime;
        }
    }

    private void Shot1()
    {
        //Creating the shot
        GameObject shot = Instantiate(shot1, shotPosition1.position, transform.rotation);
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shotSpeed);
        shot = Instantiate(shot1, shotPosition2.position, transform.rotation);
        shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shotSpeed);

    }

    private void Shot2()
    {
        var player = FindObjectOfType<PlayerController>();
        if (player)
        {
            //Creating the shot that follow
            var myShot = Instantiate(shot2, shotPosition3.position, transform.rotation);
            Vector2 direction = player.transform.position - myShot.transform.position;
            direction.Normalize();
            myShot.GetComponent<Rigidbody2D>().velocity = direction * (-shotSpeed);
            //Adjusting the angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            myShot.transform.rotation = Quaternion.Euler(0f, 0f, (angle + 90));
        }
    }

    private void ChangeState()
    {
        //Locking between states
        if(waitStates <= 0f)
        {
            int indiceState = Random.Range(0, states.Length);
            state = states[indiceState];
            waitStates = 7f;
        } else
        {
            waitStates -= Time.deltaTime;
        }
    }
}
