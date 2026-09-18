using UnityEngine;

public class ResgatarRecompensa : MonoBehaviour
{
    [Header("Configuração da Recompensa")]
    [SerializeField] private int custoXP = 100;

    public void Resgatar()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogWarning("DataManager não encontrado.");
            return;
        }

        bool conseguiuResgatar =
            DataManager.Instance.GastarXP(custoXP);

        if (conseguiuResgatar)
        {
            Debug.Log(
                "Recompensa resgatada com sucesso!"
            );

            AtualizarInterface();
        }
        else
        {
            Debug.Log(
                "XP insuficiente para essa recompensa."
            );
        }
    }

    private void AtualizarInterface()
    {
        ExibirInfoUsuario[] textosNaTela =
            FindObjectsOfType<ExibirInfoUsuario>();

        foreach (ExibirInfoUsuario texto in textosNaTela)
        {
            texto.AtualizarTexto();
        }
    }
}