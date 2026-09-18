using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ExibirInfoUsuario : MonoBehaviour
{
    public enum OQueExibir
    {
        ApenasNome,
        ApenasTPM,
        NomeETPMJuntos,
        ApenasXP
    }

    public enum DeQuem
    {
        Colaborador,
        Gerente
    }

    [Header("De quem é a informação?")]
    public DeQuem puxarDadosDe;

    [Header("O que exibir?")]
    public OQueExibir modoDeExibicao;

    [Header("Personalização (Opcional)")]
    public string textoAntes = "";
    public string textoDepois = "";

    private void OnEnable()
    {
        AtualizarTexto();
    }

    public void AtualizarTexto()
    {
        string prefixo =
            (puxarDadosDe == DeQuem.Colaborador)
            ? "Colab_"
            : "Gerente_";

        string nome = PlayerPrefs.GetString(
            prefixo + "Nome",
            "Usuário"
        );

        string tpm = PlayerPrefs.GetString(
            prefixo + "TPM",
            "0000"
        );

        int xp = 0;

        // XP do colaborador vem do DataManager
        if (puxarDadosDe == DeQuem.Colaborador)
        {
            if (DataManager.Instance != null)
            {
                xp = DataManager.Instance.totalXP;
            }
            else
            {
                // Segurança caso o DataManager ainda não exista
                xp = PlayerPrefs.GetInt("TotalXPBD", 0);
            }
        }
        else
        {
            // Caso vocês usem XP do gerente futuramente
            xp = PlayerPrefs.GetInt("Gerente_XP", 0);
        }

        string textoFinal = "";

        switch (modoDeExibicao)
        {
            case OQueExibir.ApenasNome:

                textoFinal =
                    $"{textoAntes}{nome}{textoDepois}";

                break;


            case OQueExibir.ApenasTPM:

                textoFinal =
                    $"{textoAntes}{tpm}{textoDepois}";

                break;


            case OQueExibir.NomeETPMJuntos:

                textoFinal =
                    $"{textoAntes}{nome} | TPM: {tpm}{textoDepois}";

                break;


            case OQueExibir.ApenasXP:

                textoFinal =
                    $"{textoAntes}{xp}{textoDepois}";

                break;
        }

        TMP_Text componenteTMP =
            GetComponent<TMP_Text>();

        if (componenteTMP != null)
        {
            componenteTMP.text = textoFinal;
            return;
        }

        Text componenteTextNativo =
            GetComponent<Text>();

        if (componenteTextNativo != null)
        {
            componenteTextNativo.text = textoFinal;
        }
    }
}