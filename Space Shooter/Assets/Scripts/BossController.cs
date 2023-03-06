using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyEntity
{
    [SerializeField] private Rigidbody2D myRB;
    [SerializeField] private float horizontalLimit = 6f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private string state = "state1";
    [SerializeField] private Transform shotPosition1;
    [SerializeField] private Transform shotPosition2;
    [SerializeField] private Transform shotPosition3;
    [SerializeField] private GameObject shot1;
    [SerializeField] private GameObject shot2;
    [SerializeField] private bool positionPermission = true;
    [SerializeField] private float shotDelay = 1f;
    [SerializeField] private float waitShot2 = 1f;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if ((transform.position.x >= horizontalLimit || transform.position.x <= -horizontalLimit) && positionPermission)
        {
            speed *= -1;
            positionPermission = false;
        } else if (transform.position.x < 2 && transform.position.x > -2)
        {
            positionPermission = true;
        }
        myRB.velocity = new Vector2(speed, 0f);

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
}
