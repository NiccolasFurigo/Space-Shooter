using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : MonoBehaviour
{
    //Variáveis
    private Rigidbody2D meuRB;
    private float velocidade = -3f;
    private float esperaTiro;

    [SerializeField] private GameObject tiro;
    [SerializeField] private Transform posicaoTiro;
    // Start is called before the first frame update
    void Start()
    {
        //Pegando o Rigidbody
        meuRB = GetComponent<Rigidbody2D>();
        //Aplicando uma velocidade vertical
        meuRB.velocity = new Vector2(0f, velocidade);
        //Deixando a espera do primeiro tiro aleatória
        esperaTiro = Random.Range(0.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //Checando se o sprite render está visível
        bool visivel = GetComponentInChildren<SpriteRenderer>().isVisible;

        if (visivel)
        {
            //Diminuindo a espera do tiro
            esperaTiro -= Time.deltaTime;
            //Atirando caso o valor seja menor que 0
            if (esperaTiro <= 0)
            {
                //Criando o tiro na minha posição
                Instantiate(tiro, posicaoTiro.position, transform.rotation);
                //Reiniciando o tempo de espera
                esperaTiro = Random.Range(1.5f, 2f);
            }
        }
    }
}
