using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class BotaoNavegacies : MonoBehaviour
{
    [Header("NAVEGAÇÃO")]
    [Tooltip("Nome exato da cena que este botão deve abrir")]
    public string nomeDaCenaDestino;


    [Header("CORES")]
    public Color corAtiva = Color.red;
    public Color corInativa = Color.white;


    [Header("EFEITO DO PANEL")]
    [SerializeField] private bool usarEfeitoPanel = true;

    [Tooltip("Nome do objeto filho usado como efeito")]
    [SerializeField] private string nomeDoPanel = "PrefabBorda";


    [Header("ANIMAÇÃO")]
    [SerializeField] private float escalaInicial = 0.45f;
    [SerializeField] private float escalaAntesDoFade = 1.35f;
    [SerializeField] private float escalaFinal = 1.75f;

    [SerializeField] private float duracaoCrescimento = 0.35f;
    [SerializeField] private float duracaoFade = 0.30f;

    [Range(0f, 1f)]
    [SerializeField] private float alphaInicial = 0f;

    [Range(0f, 1f)]
    [SerializeField] private float alphaVisivel = 1f;


    [Header("CURVAS")]
    [SerializeField] private Ease easeCrescimento = Ease.OutBack;
    [SerializeField] private Ease easeFade = Ease.InQuad;


    private Button meuBotao;
    private Image minhaImagem;

    private GameObject panelEfeito;
    private RectTransform rectPanel;
    private CanvasGroup canvasGroupPanel;


    private void Awake()
    {
        meuBotao = GetComponent<Button>();
        minhaImagem = GetComponent<Image>();

        EncontrarPanel();
    }


    private void Start()
    {
        AtualizarCor();

        meuBotao.onClick.AddListener(IrParaCena);

        bool estaNaCenaAtual =
            SceneManager.GetActiveScene().name ==
            nomeDaCenaDestino;

        if (usarEfeitoPanel)
        {
            if (estaNaCenaAtual)
                ExecutarEfeitoPanel();
            else
                DesativarPanel();
        }
    }


    private void OnDestroy()
    {
        if (meuBotao != null)
            meuBotao.onClick.RemoveListener(IrParaCena);

        if (rectPanel != null)
            rectPanel.DOKill();

        if (canvasGroupPanel != null)
            canvasGroupPanel.DOKill();
    }


    // ============================================================
    // ENCONTRA O PANEL AUTOMATICAMENTE
    // ============================================================

    private void EncontrarPanel()
    {
        Transform encontrado =
            transform.Find(nomeDoPanel);

        if (encontrado == null)
        {
            Debug.LogWarning(
                $"[{gameObject.name}] Não encontrei um filho chamado '{nomeDoPanel}'."
            );

            return;
        }

        panelEfeito = encontrado.gameObject;

        rectPanel =
            panelEfeito.GetComponent<RectTransform>();

        canvasGroupPanel =
            panelEfeito.GetComponent<CanvasGroup>();


        if (canvasGroupPanel == null)
        {
            canvasGroupPanel =
                panelEfeito.AddComponent<CanvasGroup>();
        }
    }


    // ============================================================
    // COR DO BOTÃO
    // ============================================================

    private void AtualizarCor()
    {
        if (SceneManager.GetActiveScene().name ==
            nomeDaCenaDestino)
        {
            minhaImagem.color = corAtiva;
        }
        else
        {
            minhaImagem.color = corInativa;
        }
    }


    // ============================================================
    // EFEITO
    // ============================================================

    private void ExecutarEfeitoPanel()
    {
        if (panelEfeito == null ||
            rectPanel == null)
            return;


        panelEfeito.SetActive(true);

        rectPanel.DOKill();
        canvasGroupPanel.DOKill();


        rectPanel.localScale =
            Vector3.one * escalaInicial;

        canvasGroupPanel.alpha =
            alphaInicial;


        Sequence sequencia =
            DOTween.Sequence();


        // CRESCE + APARECE
        sequencia.Append(
            rectPanel
                .DOScale(
                    Vector3.one * escalaAntesDoFade,
                    duracaoCrescimento
                )
                .SetEase(easeCrescimento)
        );


        sequencia.Join(
            canvasGroupPanel
                .DOFade(
                    alphaVisivel,
                    duracaoCrescimento
                )
                .SetEase(Ease.OutQuad)
        );


        // CONTINUA CRESCENDO + DESAPARECE
        sequencia.Append(
            rectPanel
                .DOScale(
                    Vector3.one * escalaFinal,
                    duracaoFade
                )
                .SetEase(easeFade)
        );


        sequencia.Join(
            canvasGroupPanel
                .DOFade(
                    0f,
                    duracaoFade
                )
                .SetEase(easeFade)
        );


        // FINAL
        sequencia.OnComplete(() =>
        {
            panelEfeito.SetActive(false);
        });
    }


    // ============================================================
    // DESATIVAR
    // ============================================================

    private void DesativarPanel()
    {
        if (panelEfeito == null)
            return;

        rectPanel.DOKill();
        canvasGroupPanel.DOKill();

        panelEfeito.SetActive(false);
    }


    // ============================================================
    // NAVEGAÇÃO
    // ============================================================

    private void IrParaCena()
    {
        string cenaAtual =
            SceneManager.GetActiveScene().name;


        if (cenaAtual == nomeDaCenaDestino)
            return;


        if (UITransitionManager.Instance != null)
        {
            UITransitionManager.Instance
                .CarregarCenaComTransicao(
                    nomeDaCenaDestino
                );
        }
        else
        {
            SceneManager.LoadScene(
                nomeDaCenaDestino
            );
        }
    }
}