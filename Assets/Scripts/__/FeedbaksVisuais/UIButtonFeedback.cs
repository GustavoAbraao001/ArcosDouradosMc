using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(RectTransform))]
public class UIButtonFeedback : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    [Header("========================================")]
    [Header("ESCALA")]
    [Header("========================================")]

    [SerializeField] private float escalaNormal = 1f;
    [SerializeField] private float escalaHover = 1.05f;
    [SerializeField] private float escalaPressionada = 0.95f;

    [SerializeField] private float duracaoEscala = 0.12f;


    [Header("========================================")]
    [Header("MOVIMENTO")]
    [Header("========================================")]

    [SerializeField] private bool usarMovimentoHover = false;

    [SerializeField] private float movimentoYHover = 4f;

    [SerializeField] private float duracaoMovimento = 0.15f;


    [Header("========================================")]
    [Header("ROTAÇÃO")]
    [Header("========================================")]

    [SerializeField] private bool usarRotacaoHover = false;

    [SerializeField] private float rotacaoHover = 2f;

    [SerializeField] private float duracaoRotacao = 0.15f;


    [Header("========================================")]
    [Header("COR")]
    [Header("========================================")]

    [SerializeField] private bool alterarCor = false;

    [SerializeField] private Image imagemBotao;

    [SerializeField] private Color corNormal = Color.white;

    [SerializeField] private Color corHover = Color.white;

    [SerializeField] private float duracaoCor = 0.12f;


    [Header("========================================")]
    [Header("TRANSPARÊNCIA")]
    [Header("========================================")]

    [SerializeField] private bool usarAlphaHover = false;

    [Range(0f, 1f)]
    [SerializeField] private float alphaNormal = 1f;

    [Range(0f, 1f)]
    [SerializeField] private float alphaHover = 1f;


    [Header("========================================")]
    [Header("CLICK / PUNCH")]
    [Header("========================================")]

    [SerializeField] private bool usarPunch = true;

    [SerializeField] private float intensidadePunch = 0.06f;

    [SerializeField] private float duracaoPunch = 0.18f;


    [Header("========================================")]
    [Header("SONS")]
    [Header("========================================")]

    [SerializeField] private bool tocarSomHover = true;

    [SerializeField] private bool tocarSomClique = true;

    [SerializeField] private AudioClip somHoverPersonalizado;

    [SerializeField] private AudioClip somCliquePersonalizado;


    private Button botao;
    private RectTransform rectTransform;

    private Vector2 posicaoInicial;
    private float rotacaoInicial;

    private bool estaSobreOBotao;


    private void Awake()
    {
        botao = GetComponent<Button>();

        rectTransform = GetComponent<RectTransform>();

        posicaoInicial = rectTransform.anchoredPosition;

        rotacaoInicial = rectTransform.localEulerAngles.z;

        if (imagemBotao == null)
        {
            imagemBotao = GetComponent<Image>();
        }
    }


    private void Start()
    {
        rectTransform.localScale = Vector3.one * escalaNormal;

        rectTransform.anchoredPosition = posicaoInicial;

        rectTransform.localEulerAngles =
            new Vector3(0f, 0f, rotacaoInicial);


        if (alterarCor && imagemBotao != null)
        {
            imagemBotao.color = corNormal;
        }


        if (botao != null)
        {
            botao.onClick.AddListener(QuandoClicar);
        }
    }


    private void OnDestroy()
    {
        if (botao != null)
        {
            botao.onClick.RemoveListener(QuandoClicar);
        }

        rectTransform.DOKill();

        if (imagemBotao != null)
        {
            imagemBotao.DOKill();
        }
    }


    // ============================================================
    // HOVER
    // ============================================================

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!botao.interactable)
            return;


        estaSobreOBotao = true;


        // ESCALA
        rectTransform
            .DOScale(Vector3.one * escalaHover, duracaoEscala)
            .SetEase(Ease.OutBack);


        // MOVIMENTO
        if (usarMovimentoHover)
        {
            rectTransform
                .DOAnchorPos(
                    posicaoInicial +
                    Vector2.up * movimentoYHover,
                    duracaoMovimento
                )
                .SetEase(Ease.OutQuad);
        }


        // ROTAÇÃO
        if (usarRotacaoHover)
        {
            rectTransform
                .DORotate(
                    new Vector3(
                        0f,
                        0f,
                        rotacaoInicial + rotacaoHover
                    ),
                    duracaoRotacao
                )
                .SetEase(Ease.OutQuad);
        }


        // COR
        if (alterarCor && imagemBotao != null)
        {
            imagemBotao.DOKill();

            imagemBotao
                .DOColor(corHover, duracaoCor)
                .SetEase(Ease.OutQuad);
        }


        // ALPHA
        if (usarAlphaHover && imagemBotao != null)
        {
            Color corAtual = imagemBotao.color;

            imagemBotao
                .DOFade(alphaHover, duracaoCor)
                .SetEase(Ease.OutQuad);
        }


        // SOM
        if (tocarSomHover && UISoundManager.Instance != null)
        {
            if (somHoverPersonalizado != null)
            {
                UISoundManager.Instance.TocarSom(
                    somHoverPersonalizado
                );
            }
            else
            {
                UISoundManager.Instance.TocarHover();
            }
        }
    }


    // ============================================================
    // SAIR DO HOVER
    // ============================================================

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!botao.interactable)
            return;


        estaSobreOBotao = false;


        // ESCALA
        rectTransform
            .DOScale(Vector3.one * escalaNormal, duracaoEscala)
            .SetEase(Ease.OutQuad);


        // MOVIMENTO
        if (usarMovimentoHover)
        {
            rectTransform
                .DOAnchorPos(
                    posicaoInicial,
                    duracaoMovimento
                )
                .SetEase(Ease.OutQuad);
        }


        // ROTAÇÃO
        if (usarRotacaoHover)
        {
            rectTransform
                .DORotate(
                    new Vector3(
                        0f,
                        0f,
                        rotacaoInicial
                    ),
                    duracaoRotacao
                )
                .SetEase(Ease.OutQuad);
        }


        // COR
        if (alterarCor && imagemBotao != null)
        {
            imagemBotao
                .DOColor(corNormal, duracaoCor)
                .SetEase(Ease.OutQuad);
        }


        // ALPHA
        if (usarAlphaHover && imagemBotao != null)
        {
            imagemBotao
                .DOFade(alphaNormal, duracaoCor)
                .SetEase(Ease.OutQuad);
        }
    }


    // ============================================================
    // PRESSIONAR
    // ============================================================

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!botao.interactable)
            return;


        rectTransform
            .DOScale(
                Vector3.one * escalaPressionada,
                0.08f
            )
            .SetEase(Ease.OutQuad);
    }


    // ============================================================
    // SOLTAR
    // ============================================================

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!botao.interactable)
            return;


        if (estaSobreOBotao)
        {
            rectTransform
                .DOScale(
                    Vector3.one * escalaHover,
                    0.08f
                )
                .SetEase(Ease.OutQuad);
        }
        else
        {
            rectTransform
                .DOScale(
                    Vector3.one * escalaNormal,
                    0.08f
                )
                .SetEase(Ease.OutQuad);
        }
    }


    // ============================================================
    // CLICK
    // ============================================================

    private void QuandoClicar()
    {
        // SOM
        if (tocarSomClique && UISoundManager.Instance != null)
        {
            if (somCliquePersonalizado != null)
            {
                UISoundManager.Instance.TocarSom(
                    somCliquePersonalizado
                );
            }
            else
            {
                UISoundManager.Instance.TocarClique();
            }
        }


        // PUNCH
        if (usarPunch)
        {
            rectTransform.DOKill();

            rectTransform
                .DOPunchScale(
                    Vector3.one * intensidadePunch,
                    duracaoPunch,
                    6,
                    0.8f
                );
        }
    }
}