using UnityEngine;

public class GerenciadorDePaineis : MonoBehaviour
{
    [Header("Painéis das abas")]
    [SerializeField] private GameObject[] paineis;

    [Header("Aba que abre primeiro")]
    [SerializeField] private int painelInicial = 0;

    private int painelAtual = -1;

    private void Start()
    {
        AbrirPainel(painelInicial);
    }

    public void AbrirPainel(int indice)
    {
        if (indice < 0 || indice >= paineis.Length)
        {
            Debug.LogWarning(
                "Índice de painel inválido: " + indice
            );

            return;
        }

        for (int i = 0; i < paineis.Length; i++)
        {
            if (paineis[i] != null)
            {
                paineis[i].SetActive(i == indice);
            }
        }

        painelAtual = indice;
    }

    public int ObterPainelAtual()
    {
        return painelAtual;
    }
}