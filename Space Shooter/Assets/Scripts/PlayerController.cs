using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    private Rigidbody2D myRB;
    private float velocity = 5f;
    private int life = 3;
    [SerializeField] private GameObject shot;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform shotPosition;
    // Start is called before the first frame update
    void Start()
    {
        //Catching the rigidbody
        myRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Taking the input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 myVelocity= new Vector2(horizontal, vertical);
        //Limiting diagonal speed
        myVelocity.Normalize();
        //Passing the input to Rigidbody
        myRB.velocity = myVelocity * velocity;

        //Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(shot, shotPosition.position, transform.rotation);
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
