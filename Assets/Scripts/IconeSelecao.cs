using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class IconeSelecao : MonoBehaviour
{
    [Header("Identificação")]
    [SerializeField] private int indice;

    [Header("Borda de Seleção")]
    [SerializeField] private GameObject bordaSelecionada;

    [Header("Animação")]
    [SerializeField] private bool usarAnimacao = true;

    [SerializeField] private float escalaInicial = 0.85f;

    [SerializeField] private float duracaoAnimacao = 0.15f;

    [Header("Som")]
    [SerializeField] private bool tocarSom = true;

    [SerializeField] private AudioClip somSelecionado;

    private Button botao;
    private RectTransform rectBorda;

    private GrupoSelecao grupo;

    public int Indice => indice;

    private void Awake()
    {
        botao = GetComponent<Button>();

        if (bordaSelecionada != null)
        {
            rectBorda =
                bordaSelecionada.GetComponent<RectTransform>();
        }
    }

    private void Start()
    {
        botao.onClick.AddListener(Selecionar);

        Desselecionar();
    }

    private void OnDestroy()
    {
        if (botao != null)
        {
            botao.onClick.RemoveListener(Selecionar);
        }

        if (rectBorda != null)
        {
            rectBorda.DOKill();
        }
    }

    public void DefinirGrupo(GrupoSelecao novoGrupo)
    {
        grupo = novoGrupo;
    }

    private void Selecionar()
    {
        if (grupo != null)
        {
            grupo.Selecionar(this);
        }

        MostrarBorda();

        if (tocarSom &&
            UISoundManager.Instance != null)
        {
            if (somSelecionado != null)
            {
                UISoundManager.Instance.TocarSom(
                    somSelecionado
                );
            }
            else
            {
                UISoundManager.Instance.TocarClique();
            }
        }

        Debug.Log(
            "Ícone selecionado: " + indice
        );
    }

    public void Desselecionar()
    {
        if (bordaSelecionada == null)
            return;

        if (rectBorda != null)
        {
            rectBorda.DOKill();
            rectBorda.localScale = Vector3.one;
        }

        bordaSelecionada.SetActive(false);
    }

    private void MostrarBorda()
    {
        if (bordaSelecionada == null)
            return;

        bordaSelecionada.SetActive(true);

        if (rectBorda == null)
            return;

        rectBorda.DOKill();

        if (usarAnimacao)
        {
            rectBorda.localScale =
                Vector3.one * escalaInicial;

            rectBorda
                .DOScale(
                    Vector3.one,
                    duracaoAnimacao
                )
                .SetEase(Ease.OutBack);
        }
        else
        {
            rectBorda.localScale =
                Vector3.one;
        }
    }
}