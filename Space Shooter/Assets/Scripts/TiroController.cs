using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{
    //Variables
    private Rigidbody2D myRB;
    [SerializeField] private float velocity = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //Catching the rigidbody
        myRB = GetComponent<Rigidbody2D>();
        //Applying speed
        myRB.velocity = new Vector2(0f, velocity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Destroying the shot
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
