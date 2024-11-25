using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;

    public Vector2 playerDirection;

    //anima��o andando
    bool isWalking;
    Animator playerAnimator;

    //player olhando para a direita
    bool playerFaceingRight = true;
    
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
       
    }

    // Fixed Upadte � utilizada para implementa��o de f�sica no jogo
    // Por ter uma execu��o padronizada em diferentes dispositivos

    private void FixedUpdate()
    {
        //Verificar se o player est� em movimento
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

        // Se o player for para a ESQUERDA e est� olhando para a DIREITA
        if (playerDirection.x < 0 && playerFaceingRight == true)
        {
            Flip();
        }

        // Se o player vai para a DIREITA e est� olhando para a ESQUERDA
        else if (playerDirection.x > 0 && playerFaceingRight == false)
        {
            Flip();
        }
    }

    void UpdateAnimator()
    {
        // Definir o valor do par�metro do animator, igual � propriedade isWalking
        playerAnimator.SetBool("isWalking", isWalking);
    }

    void Flip()
    {
        //Vai girar o sprite do player em 180 graus no eixo Y
        //Inverte o valor da vari�vel playerFacingRight
        playerFaceingRight = !playerFaceingRight;

        //Girar em 180 graus o sprite
        transform.Rotate(0, 180, 0);
    }
}

