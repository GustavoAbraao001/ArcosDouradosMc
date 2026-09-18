using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuNavegacao : MonoBehaviour
{
    [Header("Cores dos Botões")]
    public Color corAtiva = Color.red;       
    public Color corInativa = Color.white;   

    [Header("Imagens dos Botões (Backgrounds)")]
    public Image imgBotaoHome;
    public Image imgBotaoCardapio;
    public Image imgBotaoPerfil;

    [Header("Nomes Exatos das Cenas")]
    public string cenaHome = "Cena_Home";
    public string cenaCardapio = "Cena_Cardapio";
    public string cenaPerfil = "Cena_Perfil";

    private void Start()
    {
        AtualizarCoresDoMenu();
    }

    public void IrParaCena(string nomeDaCena)
    {
        if (SceneManager.GetActiveScene().name != nomeDaCena)
        {
            SceneManager.LoadScene(nomeDaCena);
        }
    }

    private void AtualizarCoresDoMenu()
    {
        string cenaAtual = SceneManager.GetActiveScene().name;

        if (imgBotaoHome != null) imgBotaoHome.color = corInativa;
        if (imgBotaoCardapio != null) imgBotaoCardapio.color = corInativa;
        if (imgBotaoPerfil != null) imgBotaoPerfil.color = corInativa;

        if (cenaAtual == cenaHome && imgBotaoHome != null) imgBotaoHome.color = corAtiva;
        else if (cenaAtual == cenaCardapio && imgBotaoCardapio != null) imgBotaoCardapio.color = corAtiva;
        else if (cenaAtual == cenaPerfil && imgBotaoPerfil != null) imgBotaoPerfil.color = corAtiva;
    }
}