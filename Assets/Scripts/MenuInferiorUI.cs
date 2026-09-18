using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuInferiorUI : MonoBehaviour
{
    [System.Serializable]
    public class ItemMenu
    {
        [Header("Cena")]
        public string nomeDaCena;

        [Header("Visual")]
        public RectTransform botao;
        public GameObject painelBorda;
    }

    [Header("Itens do Menu")]
    [SerializeField] private ItemMenu[] itensMenu;

    [Header("Animação")]
    [SerializeField] private float duracaoAnimacao = 0.2f;
    [SerializeField] private float escalaInicial = 0.8f;
    [SerializeField] private float deslocamentoAtivo = 2f;

    private void Awake()
    {
        SceneManager.sceneLoaded += QuandoCenaCarregada;
    }

    private void Start()
    {
        AtualizarMenu(false);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= QuandoCenaCarregada;
    }

    private void QuandoCenaCarregada(
        Scene cena,
        LoadSceneMode modo)
    {
        AtualizarMenu(true);
    }

    public void AtualizarMenu(bool animar)
    {
        string cenaAtual = SceneManager.GetActiveScene().name;

        for (int i = 0; i < itensMenu.Length; i++)
        {
            ItemMenu item = itensMenu[i];

            if (item == null)
                continue;

            bool selecionado =
                item.nomeDaCena == cenaAtual;

            // ==============================
            // PAINEL DA BORDA
            // ==============================

            if (item.painelBorda != null)
            {
                item.painelBorda.SetActive(selecionado);
            }

            // ==============================
            // ANIMAÇÃO DO BOTÃO
            // ==============================

            if (item.botao != null)
            {
                item.botao.DOKill();

                Vector2 posicaoOriginal =
                    item.botao.anchoredPosition;

                if (selecionado && animar)
                {
                    item.botao
                        .DOAnchorPos(
                            posicaoOriginal +
                            Vector2.up * deslocamentoAtivo,
                            duracaoAnimacao
                        )
                        .SetEase(Ease.OutQuad);
                }
                else
                {
                    item.botao
                        .DOAnchorPos(
                            posicaoOriginal,
                            duracaoAnimacao
                        )
                        .SetEase(Ease.OutQuad);
                }
            }

            // ==============================
            // ANIMAÇÃO DA BORDA
            // ==============================

            if (selecionado &&
                item.painelBorda != null)
            {
                RectTransform borda =
                    item.painelBorda.GetComponent<RectTransform>();

                if (borda != null)
                {
                    borda.DOKill();

                    if (animar)
                    {
                        borda.localScale =
                            Vector3.one * escalaInicial;

                        borda
                            .DOScale(
                                Vector3.one,
                                duracaoAnimacao
                            )
                            .SetEase(Ease.OutBack);
                    }
                    else
                    {
                        borda.localScale =
                            Vector3.one;
                    }
                }
            }
        }
    }
}