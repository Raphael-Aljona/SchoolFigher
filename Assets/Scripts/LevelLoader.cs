using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            //Mudar cenas
            //SceneManager.LoadScene("Fase1");

            //Chamar Corotina
            StartCoroutine(CarregarFase("Fase1"));
        };

    }

    // Corotina - Coroutine
    IEnumerator CarregarFase(string nomeFase)
    {
        //Iniciar a animação
        transition.SetTrigger("Start");

        //Esperar o tempo da animação
        yield return new WaitForSeconds(transitionTime);

        //Carregar a cena
        SceneManager.LoadScene(nomeFase);
    }

}
