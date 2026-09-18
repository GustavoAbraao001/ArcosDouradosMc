using UnityEngine;

public class SistemaDeXP : MonoBehaviour
{
    [Header("Configuração da Tarefa")]
    public int quantidadeDeXP = 50;

    public void ConcluirTarefaEGanharXP()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogWarning("DataManager não encontrado.");
            return;
        }

        DataManager.Instance.AdicionarXP(
            quantidadeDeXP
        );

        Debug.Log(
            "Tarefa concluída! XP atual: " +
            DataManager.Instance.totalXP
        );

        AtualizarInterface();
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