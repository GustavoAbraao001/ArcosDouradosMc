using UnityEngine;

public class GrupoSelecao : MonoBehaviour
{
    [Header("Opções desta pergunta")]
    [SerializeField] private IconeSelecao[] opcoes;

    private IconeSelecao opcaoSelecionada;

    public IconeSelecao OpcaoSelecionada => opcaoSelecionada;

    private void Awake()
    {
        foreach (IconeSelecao opcao in opcoes)
        {
            if (opcao != null)
            {
                opcao.DefinirGrupo(this);
            }
        }
    }

    public void Selecionar(IconeSelecao novaOpcao)
    {
        if (novaOpcao == null)
            return;

        if (opcaoSelecionada != null &&
            opcaoSelecionada != novaOpcao)
        {
            opcaoSelecionada.Desselecionar();
        }

        opcaoSelecionada = novaOpcao;
    }
}