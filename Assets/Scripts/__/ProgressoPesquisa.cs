using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressoPesquisa : MonoBehaviour
{
    [Header("Barra de Progresso")]
    [SerializeField] private Slider barraProgresso;

    [Header("Botão de Enviar")]
    [SerializeField] private Button botaoEnviar;

    [Header("Animação")]
    [SerializeField] private float duracaoAnimacao = 0.4f;

    private bool selecionouCarinha = false;
    private bool selecionouAmbiente = false;

    private Coroutine animacaoAtual;

    private void Start()
    {
        if (barraProgresso != null)
        {
            barraProgresso.minValue = 0f;
            barraProgresso.maxValue = 1f;
            barraProgresso.value = 0f;
        }

        if (botaoEnviar != null)
        {
            botaoEnviar.interactable = false;
        }

        AtualizarPesquisa();
    }

    // FUNÇÃO DOS 3 EMOJIS
    public void SelecionouCarinha()
    {
        selecionouCarinha = true;

        Debug.Log("Carinha selecionada!");

        AtualizarPesquisa();
    }

    // FUNÇÃO DAS 4 OPÇÕES DE BAIXO
    public void SelecionouAmbiente()
    {
        selecionouAmbiente = true;

        Debug.Log("Opção selecionada!");

        AtualizarPesquisa();
    }

    // Mantém compatibilidade caso algum botão esteja
    // usando os nomes que usamos anteriormente
    public void SelecionarCarinha()
    {
        SelecionouCarinha();
    }

    public void SelecionarOpcao()
    {
        SelecionouAmbiente();
    }

    private void AtualizarPesquisa()
    {
        float destino;

        if (selecionouCarinha && selecionouAmbiente)
        {
            destino = 1f;

            if (botaoEnviar != null)
            {
                botaoEnviar.interactable = true;
            }
        }
        else if (selecionouCarinha || selecionouAmbiente)
        {
            destino = 0.5f;

            if (botaoEnviar != null)
            {
                botaoEnviar.interactable = false;
            }
        }
        else
        {
            destino = 0f;

            if (botaoEnviar != null)
            {
                botaoEnviar.interactable = false;
            }
        }

        if (barraProgresso == null)
        {
            Debug.LogWarning("Slider da barra não foi configurado!");
            return;
        }

        if (animacaoAtual != null)
        {
            StopCoroutine(animacaoAtual);
        }

        animacaoAtual = StartCoroutine(AnimarBarra(destino));
    }

    private IEnumerator AnimarBarra(float destino)
    {
        float inicio = barraProgresso.value;
        float tempo = 0f;

        while (tempo < duracaoAnimacao)
        {
            tempo += Time.deltaTime;

            float t = Mathf.Clamp01(
                tempo / duracaoAnimacao
            );

            barraProgresso.value = Mathf.Lerp(
                inicio,
                destino,
                t
            );

            yield return null;
        }

        barraProgresso.value = destino;
        animacaoAtual = null;
    }
}