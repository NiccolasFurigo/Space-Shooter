using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroController : MonoBehaviour
{
    //Variáveis
    private Rigidbody2D meuRB;
    private float velocidade = 10f;
    // Start is called before the first frame update
    void Start()
    {
        //Pegando meu Rigidbody
        meuRB= GetComponent<Rigidbody2D>();

        //Aplicando a velocidade para cima
        meuRB.velocity = new Vector2(0f, velocidade);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
