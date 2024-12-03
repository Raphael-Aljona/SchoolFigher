using NUnit.Framework.Constraints;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMeleeController : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;

    public bool isDead;

    // Variavel que controla o lado que o inimigo est�
    public bool facingRight;
    public bool previousDirectionRight;

    // Variavel para armazenar posi��o do Player
    Transform target;

    // Variavel de movimenta��o
    float enemySpeed = 0.3f;
    float currentSpeed;

    bool isWalking;

    float horizontalForce;
    float verticalForce;

    //Variavel que vamos usar para controlar
    float walkTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Buscar o player e armazenar sua posi��o
        target = FindAnyObjectByType<PlayerController>().transform;

        //Iniciar a velocidade do inimigo
        currentSpeed = enemySpeed;

    }


    void Update()
    {
        //Verificar se o player est� para direit ou para a esquerda
        //E com isso determinar o lado que o inimigo ficar� virado
        if (target.position.x < this.transform.position.x)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }

        // Se facingRight for TRUE, vamos virar o inimigo em 180 graus no eixo Y,
        // Sen�o vmaos virar o inimigo para a esquerda
        // Se o Player � direita e a dire��o anterior n�o era direita
        if (facingRight && !previousDirectionRight)
        {
            this.transform.Rotate(0, 180, 0);
            previousDirectionRight = true;
        }

        // Se o Player n�o � direita e a dire��o anterior era direita
        if (!facingRight && previousDirectionRight)
        {
            this.transform.Rotate(0, -180, 0);
            previousDirectionRight = false;
        }

        // Iniciar o timer do caminhar do inimigo
        walkTimer += Time.deltaTime;

        //Gerenciar a anima��o do inimigo
        if (horizontalForce == 0 && verticalForce == 0)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }

        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        //Movimenta��o

        // Variavel para armazenar a distancia entre o inimigo e o player
        Vector3 targetDistance = target.position - this.transform.position;

        // Determina se a for�a horizontal deve ser negativa
        horizontalForce = targetDistance.x / Mathf.Abs(targetDistance.x);

        //Entre 1 e 2 segundos ser� feita uma definic�o de dire��o vertical
        if (walkTimer >= Random.Range(1f, 2f))
        {
            verticalForce = Random.Range(-1, 2);

            // Zera o timer de movimenta��o para andar verticalmente novamente a cada +- 1seg
            walkTimer = 0;
        }

        // Caso esteja pto do player, parar a movimenta��o
        if (Mathf.Abs(targetDistance.x) < 0.4)
        {
            horizontalForce = 0;
        }

        // Aplica velocidade no inimigo fazendo o movimentar 
        rb.linearVelocity = new Vector2(horizontalForce * currentSpeed, verticalForce * currentSpeed);
    }

    void UpdateAnimator()
    {
        animator.SetBool("isWalking", isWalking);
    }
}
