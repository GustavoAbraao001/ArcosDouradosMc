using System.Collections.Generic;
using UnityEngine;
    

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Configuração Global")]
    [Tooltip("Arraste TODAS as telas do app aqui (para desligar no início)")]
    public GameObject[] allScreens;

    [Header("Blocos de Perfil")]
    public GameObject painelFluxoColaborador;
    public GameObject painelFluxoGerente;

    [Tooltip("Tela de Login (Ponto de partida)")]
    public GameObject loginScreen;

    private Stack<GameObject> screenHistory = new Stack<GameObject>();

  
    private GameObject currentTab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
       
        foreach (GameObject screen in allScreens)
        {
            screen.SetActive(false);
        }

        
        if (loginScreen != null)
        {
            OpenScreen(loginScreen);
        }
    }

    

    public void EntrarComoColaborador(GameObject primeiraAbaColab)
    {
       
        if (painelFluxoGerente != null) painelFluxoGerente.SetActive(false);

    
        if (painelFluxoColaborador != null) painelFluxoColaborador.SetActive(true);

        ChangeTab(primeiraAbaColab);
    }

    public void EntrarComoGerente(GameObject primeiraAbaGerente)
    {
  
        if (painelFluxoColaborador != null) painelFluxoColaborador.SetActive(false);

        if (painelFluxoGerente != null) painelFluxoGerente.SetActive(true);

        ChangeTab(primeiraAbaGerente);
    }
    public void ChangeTab(GameObject newTab)
    {
        while (screenHistory.Count > 0)
        {
            screenHistory.Pop().SetActive(false);
        }

        if (currentTab != null)
        {
            currentTab.SetActive(false);
        }

       
        currentTab = newTab;
        currentTab.SetActive(true);
    }

  
    public void OpenScreen(GameObject screenToShow)
    {
        if (screenHistory.Count > 0)
        {
            screenHistory.Peek().SetActive(false);
        }
        else if (currentTab != null)
        {
           
            currentTab.SetActive(false);
        }

       
        screenHistory.Push(screenToShow);
        screenToShow.SetActive(true);

        MenuDisplay menuDaTela = screenToShow.GetComponentInChildren<MenuDisplay>();
        if (menuDaTela != null)
        {
            menuDaTela.AtualizarEExibirMenu();
        }
    }

  
    public void BackScreen()
    {
        if (screenHistory.Count > 0)
        {
          
            screenHistory.Pop().SetActive(false);

        
            if (screenHistory.Count > 0)
            {
                screenHistory.Peek().SetActive(true);
            }
           
            else if (currentTab != null)
            {
                currentTab.SetActive(true);
            }
        }
    }
}

