using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

public class UITransitionManager : MonoBehaviour
{
    public static UITransitionManager Instance;

    [Header("Painel de Transição")]
    [SerializeField] private CanvasGroup painelFade;

    [Header("Configurações")]
    [SerializeField] private float duracaoSaida = 0.25f;
    [SerializeField] private float duracaoEntrada = 0.30f;

    [Range(0f, 1f)]
    [SerializeField] private float alphaMaximo = 0.65f;

    [Header("Carregamento")]
    [SerializeField] private float atrasoMinimo = 0.08f;

    private bool estaTransicionando = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += QuandoCenaCarregada;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PrepararFadeInicial();
    }

    private void PrepararFadeInicial()
    {
        if (painelFade == null)
            return;

        painelFade.alpha = alphaMaximo;
        painelFade.blocksRaycasts = true;
        painelFade.interactable = true;

        painelFade
            .DOFade(0f, duracaoEntrada)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                painelFade.blocksRaycasts = false;
                painelFade.interactable = false;
            });
    }

    public void CarregarCenaComTransicao(string nomeDaCena)
    {
        if (estaTransicionando)
            return;

        if (string.IsNullOrEmpty(nomeDaCena))
        {
            Debug.LogWarning("Nome da cena não foi informado.");
            return;
        }

        if (SceneManager.GetActiveScene().name == nomeDaCena)
            return;

        StartCoroutine(TransicionarParaCena(nomeDaCena));
    }

    private IEnumerator TransicionarParaCena(string nomeDaCena)
    {
        estaTransicionando = true;

        if (painelFade == null)
        {
            SceneManager.LoadScene(nomeDaCena);
            yield break;
        }

        painelFade.DOKill();

        painelFade.blocksRaycasts = true;
        painelFade.interactable = true;

        // Começa a carregar a próxima cena IMEDIATAMENTE.
        AsyncOperation carregamento =
            SceneManager.LoadSceneAsync(nomeDaCena);

        if (carregamento == null)
        {
            Debug.LogError(
                "Não foi possível carregar a cena: " +
                nomeDaCena
            );

            estaTransicionando = false;
            yield break;
        }

        // Não ativa a nova cena ainda.
        carregamento.allowSceneActivation = false;

        // Fade começa ao mesmo tempo em que a cena carrega.
        Tween fadeOut = painelFade
            .DOFade(alphaMaximo, duracaoSaida)
            .SetEase(Ease.InOutSine);

        // Dá um pequeno tempo para o fade começar visualmente.
        yield return new WaitForSecondsRealtime(atrasoMinimo);

        // Espera a cena estar praticamente pronta.
        while (carregamento.progress < 0.9f)
        {
            yield return null;
        }

        // Espera o fade terminar.
        yield return fadeOut.WaitForCompletion();

        // Agora a cena pode ser ativada.
        carregamento.allowSceneActivation = true;

        // Aguarda a Unity realmente concluir a troca.
        while (!carregamento.isDone)
        {
            yield return null;
        }
    }

    private void QuandoCenaCarregada(
        Scene cena,
        LoadSceneMode modo)
    {
        estaTransicionando = false;

        if (painelFade == null)
            return;

        painelFade.DOKill();

        painelFade.alpha = alphaMaximo;

        painelFade.blocksRaycasts = true;
        painelFade.interactable = true;

        painelFade
            .DOFade(0f, duracaoEntrada)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                painelFade.blocksRaycasts = false;
                painelFade.interactable = false;
            });
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= QuandoCenaCarregada;
        }
    }
}