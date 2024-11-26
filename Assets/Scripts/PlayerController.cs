using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;

    public Vector2 playerDirection;

    //animação andando
    bool isWalking;
    Animator playerAnimator;

    //player olhando para a direita
    bool playerFaceingRight = true;

    int punchCount;
    float timeCross = 1f;

    bool comboControl;

    void Start()
    {
        //Obtem e inicializa a propriedades no rigidBody2D
        playerRigidBody = GetComponent<Rigidbody2D>();

        // Obtem e inicializa as propriedades do animator
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        UpdateAnimator();

        //Iniciar o temporizador
        

        // Quando clicar em alguma tecla
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (isWalking == false)
            {
                if (punchCount < 2)
                {
                    PlayerJab();
                    punchCount ++;
                    if (!comboControl)
                    {
                        StartCoroutine(CrossController());
                    }
                }
                else if (punchCount >= 2)
                {
                    PlayerCross();
                    punchCount = 0;
                }

            }
        }

        // Parando o temporizador
        StopCoroutine(CrossController());

    }

    // Fixed Upadte é utilizada para implementação de física no jogo
    // Por ter uma execução padronizada em diferentes dispositivos

    private void FixedUpdate()
    {
        //Verificar se o player está em movimento
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isWalking = true;

        }
        else
        {
            isWalking = false;
        }

        playerRigidBody.MovePosition(playerRigidBody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);
    }

    void PlayerMove()
    {
        //Pega a entrada do jogador, e cria um vector2 para usar no playerDirection
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Se o player for para a ESQUERDA e está olhando para a DIREITA
        if (playerDirection.x < 0 && playerFaceingRight == true)
        {
            Flip();
        }

        // Se o player vai para a DIREITA e está olhando para a ESQUERDA
        else if (playerDirection.x > 0 && playerFaceingRight == false)
        {
            Flip();
        }
    }

    void UpdateAnimator()
    {
        // Definir o valor do parâmetro do animator, igual á propriedade isWalking
        playerAnimator.SetBool("isWalking", isWalking);
    }

    void Flip()
    {
        //Vai girar o sprite do player em 180 graus no eixo Y
        //Inverte o valor da variável playerFacingRight
        playerFaceingRight = !playerFaceingRight;

        //Girar em 180 graus o sprite
        transform.Rotate(0, 180, 0);
    }

    void PlayerJab()
    {
        playerAnimator.SetTrigger("isJab");
    }

    void PlayerCross()
    {
        playerAnimator.SetTrigger("isCross");
    }

    IEnumerator CrossController()
    {
        comboControl = true;
        yield return new WaitForSeconds(timeCross);
        punchCount = 0;
        comboControl = false;
    }
}

