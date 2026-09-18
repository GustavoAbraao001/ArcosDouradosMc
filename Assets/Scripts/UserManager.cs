using TMPro;
using UnityEngine;


 
public class UserManager : MonoBehaviour
{
    public static UserManager Instance;

    [Header("Dados do Colaborador")]
    public string nomeColaborador = "Gustavo";
    public int nivelAtual = 3;
    public int xpAtual = 450;
    public int xpParaProximoNivel = 1000;
    public int diasDeFoguinho = 5;

    [Header("Referências Visuais (UI)")]
    public TMP_Text textoNivel;
    public TMP_Text textoXp;
    public TMP_Text textoFoguinho;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        AtualizarInterfaceUI();
    }

    
    public void AdicionarXp(int quantidadeXp)
    {
        xpAtual += quantidadeXp;

    
        if (xpAtual >= xpParaProximoNivel)
        {
            xpAtual -= xpParaProximoNivel;
            nivelAtual++;
        }

        AtualizarInterfaceUI();
    }

   
    private void AtualizarInterfaceUI()
    {
        if (textoNivel != null) textoNivel.text = "Nível " + nivelAtual;
        if (textoXp != null) textoXp.text = xpAtual + " / " + xpParaProximoNivel + " XP";
        if (textoFoguinho != null) textoFoguinho.text = diasDeFoguinho.ToString() + " dias";
    }
}

