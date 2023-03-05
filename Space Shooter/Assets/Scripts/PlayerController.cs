using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variables
    private Rigidbody2D myRB;
    private float velocity = 5f;
    private int life = 3;
    private float shotSpeed = 10f;
    private GameObject myShield;
    private float shieldTimer = 0f;
    private int amountShiled = 3;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject shot;
    [SerializeField] private GameObject shot2;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Transform shotPosition;
    [SerializeField] private float xLimit;
    [SerializeField] private float yLimit;
    [SerializeField] private int shotLevel = 1;
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
        Shield();
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

    private void Shield()
    {
        if (Input.GetButtonDown("Shield"))
        {
            if (!myShield && amountShiled > 0)
            {
                myShield = Instantiate(shield, transform.position, transform.rotation);
                amountShiled--;
            }
        }

        if (myShield) {
            shieldTimer += Time.deltaTime;
            myShield.transform.position = transform.position;

            if(shieldTimer > 6f)
            {
                Destroy(myShield);
                shieldTimer = 0f;
            }
        }
    }

    private void Shooting()
    {
        //Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            switch (shotLevel) {
                case 1:
                    CreatingShot(shot, shotPosition.position);
                    break;
                case 2:
                    Shot2();
                    break;
                case 3:
                    CreatingShot(shot, shotPosition.position);
                    Shot2();
                    break;
            }
        }
    }

    private void Shot2() {
        Vector3 position = new Vector3(transform.position.x - 0.45f, transform.position.y + 0.1f, 0f);
        CreatingShot(shot2, position);
        position = new Vector3(transform.position.x + 0.45f, transform.position.y + 0.1f, 0f);
        CreatingShot(shot2, position);
    }

    private void CreatingShot(GameObject shot, Vector3 position)
    {
        GameObject myShot = Instantiate(shot, position, transform.rotation);
        myShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, shotSpeed);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            if (shotLevel < 3)
            {
                shotLevel++;
                Destroy(other.gameObject);
            }
        }
    }
}
