using UnityEngine;
using TMPro; 
using UnityEngine.UI; 

public class AtualizadorDePerfil : MonoBehaviour
{
    [Header("Textos da Interface")]
    public TextMeshProUGUI textoNomeUsuario; 
    public TextMeshProUGUI textoXP;          

    private void OnEnable()
    {
       
        if (DataManager.Instance != null)
        {
            if (textoNomeUsuario != null)
                textoNomeUsuario.text = "Olá, " + DataManager.Instance.nomeDoUsuario;

            if (textoXP != null)
                textoXP.text = "XP: " + DataManager.Instance.totalXP.ToString();
        }
    }
}