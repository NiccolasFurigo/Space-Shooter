using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variáveis
    private Rigidbody2D meuRB;
    private float velocidade = 5f;
    [SerializeField] private GameObject tiro;
    // Start is called before the first frame update
    void Start()
    {
        //Pegando o Componente Rigidbody
        meuRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pegando o Input Horizontal e Vertical
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 minhaVelocidade = new Vector2(horizontal, vertical);
        //Limitando a velocidade na diagonal
        minhaVelocidade.Normalize();
        //Passando o Input para o Rigidbody
        meuRB.velocity = minhaVelocidade * velocidade;

        //Atirando
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(tiro, transform.position, transform.rotation);
        }
    }
}
