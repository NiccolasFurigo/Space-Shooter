using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    private Rigidbody2D myRB;
    private float velocity = 5f;
    private int life = 3;
    private float shotSpeed = 10f;
    [SerializeField] private GameObject shot;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform shotPosition;
    [SerializeField] private float xLimit;
    [SerializeField] private float yLimit;
    // Start is called before the first frame update
    void Start()
    {
        //Catching the rigidbody
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shooting();
    }

    private void Move()
    {
        //Taking the input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 myVelocity = new Vector2(horizontal, vertical);
        //Limiting diagonal speed
        myVelocity.Normalize();
        //Passing the input to Rigidbody
        myRB.velocity = myVelocity * velocity;

        //Limiting the player on the screen
        float myX = Mathf.Clamp(transform.position.x, -xLimit, xLimit);
        float myY = Mathf.Clamp(transform.position.y, -yLimit, yLimit);
        transform.position = new Vector3(myX, myY, transform.position.z);
    }

    private void Shooting()
    {
        //Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            var myShot = Instantiate(shot, shotPosition.position, transform.rotation);
            myShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shotSpeed);
        }
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
