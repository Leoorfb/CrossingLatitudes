using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private string cenaCarregar;
    public void Jogar()
    {
        SceneManager.LoadScene(cenaCarregar);
    }
    public void SairJogo()
    {
        Application.Quit();
        Debug.Log("Saindo do jogo");
    }
    
    public void TryAgain()
    {

    }

}
