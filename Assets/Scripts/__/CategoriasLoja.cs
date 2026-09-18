using UnityEngine;
using UnityEngine.UI;

public class CategoriasLoja : MonoBehaviour
{
    [Header("Painéis")]
    [SerializeField] private GameObject[] paineis;

    [Header("Botões")]
    [SerializeField] private Button[] botoes;

    [Header("Cores")]
    [SerializeField] private Color corAtiva = Color.red;
    [SerializeField] private Color corInativa = Color.gray;

    private void Start()
    {
        AbrirCategoria(0);
    }

    public void AbrirCategoria(int indice)
    {
        // Liga apenas o painel selecionado
        for (int i = 0; i < paineis.Length; i++)
        {
            if (paineis[i] != null)
            {
                paineis[i].SetActive(i == indice);
            }
        }

        // Atualiza as cores
        for (int i = 0; i < botoes.Length; i++)
        {
            if (botoes[i] == null)
                continue;

            Color novaCor = (i == indice)
                ? corAtiva
                : corInativa;

            // Altera as cores do próprio Button
            ColorBlock cores = botoes[i].colors;

            cores.normalColor = novaCor;
            cores.selectedColor = novaCor;
            cores.highlightedColor = novaCor;

            // Deixa um pouco mais escuro apenas enquanto pressiona
            cores.pressedColor = novaCor * 0.8f;

            botoes[i].colors = cores;

            // Força a imagem para a cor correta imediatamente
            if (botoes[i].targetGraphic != null)
            {
                botoes[i].targetGraphic.color = novaCor;
            }
        }
    }
}