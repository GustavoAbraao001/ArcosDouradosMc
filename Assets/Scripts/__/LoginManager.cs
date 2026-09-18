using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public enum TipoDeConta { Colaborador, Gerente }

    [Header("Quem está fazendo o Login?")]
    public TipoDeConta tipoDeConta;

    [Header("Campos de Entrada (Login)")]
    public TMP_InputField campoNome;
    public TMP_InputField campoTPM;

    [Header("Troca de Cena")]
    public string nomeDaProximaCena;

    private void Awake()
    {

        PlayerPrefs.DeleteAll();
    }

    public void ConfirmarLogin()
    {
        string nomeDigitado = (campoNome != null && !string.IsNullOrEmpty(campoNome.text)) ? campoNome.text : "Usuário";
        string tpmDigitado = (campoTPM != null && !string.IsNullOrEmpty(campoTPM.text)) ? campoTPM.text : "0000";

        
        string prefixo = (tipoDeConta == TipoDeConta.Colaborador) ? "Colab_" : "Gerente_";

        PlayerPrefs.SetString(prefixo + "Nome", nomeDigitado);
        PlayerPrefs.SetString(prefixo + "TPM", tpmDigitado);
        PlayerPrefs.Save();

        if (!string.IsNullOrEmpty(nomeDaProximaCena))
        {
            SceneManager.LoadScene(nomeDaProximaCena);
        }
    }
}