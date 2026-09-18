using UnityEngine;
using TMPro;
using DG.Tweening;

public class GraficoHorizontal : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private RectTransform barra;
    [SerializeField] private TMP_Text textoPorcentagem;

    [Header("Valor")]
    [Range(0f, 100f)]
    [SerializeField] private float porcentagem = 75f;

    [Header("Animação")]
    [SerializeField] private float duracao = 1f;

    [SerializeField] private Ease ease = Ease.OutCubic;

    [Header("Cores")]
    [SerializeField] private Color corVermelha = new Color(0.9f, 0.2f, 0.2f);
    [SerializeField] private Color corAmarela = new Color(1f, 0.75f, 0.1f);
    [SerializeField] private Color corVerde = new Color(0.2f, 0.75f, 0.3f);

    private float larguraMaxima;

    private void Awake()
    {
        if (barra == null)
            return;

        larguraMaxima = barra.sizeDelta.x;
    }

    private void Start()
    {
        AnimarGrafico();
    }

    public void AnimarGrafico()
    {
        if (barra == null)
            return;

        porcentagem = Mathf.Clamp(porcentagem, 0f, 100f);

        float larguraFinal =
            larguraMaxima * (porcentagem / 100f);

        // Começa zerado
        barra.sizeDelta =
            new Vector2(
                0f,
                barra.sizeDelta.y
            );

        // Atualiza a cor
        Color cor = ObterCor(porcentagem);

        barra.GetComponent<UnityEngine.UI.Image>().color = cor;

        // Texto começa em 0
        if (textoPorcentagem != null)
        {
            textoPorcentagem.text = "0%";
        }

        float valorAtual = 0f;

        DOTween.To(
            () => valorAtual,
            valor =>
            {
                valorAtual = valor;

                // Cresce a barra
                barra.sizeDelta = new Vector2(
                    valor,
                    barra.sizeDelta.y
                );

                // Atualiza o número
                if (textoPorcentagem != null)
                {
                    textoPorcentagem.text =
                        Mathf.RoundToInt(
                            valor / larguraMaxima * 100f
                        ) + "%";
                }
            },
            larguraFinal,
            duracao
        )
        .SetEase(ease);
    }

    private Color ObterCor(float valor)
    {
        if (valor <= 30f)
            return corVermelha;

        if (valor <= 70f)
            return corAmarela;

        return corVerde;
    }
}