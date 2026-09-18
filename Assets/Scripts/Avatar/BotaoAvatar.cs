using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BotaoAvatar : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private int indiceAvatar;

    [Header("Painel de Avatares")]
    [SerializeField] private GameObject painelAvatares;

    [Header("Indicador de Seleção")]
    [SerializeField] private GameObject indicadorSelecionado;

    private Button botao;

    private void Awake()
    {
        botao = GetComponent<Button>();
    }

    private void Start()
    {
        botao.onClick.AddListener(Selecionar);

        if (AvatarManager.Instance != null)
        {
            AvatarManager.Instance.OnAvatarAlterado += AtualizarVisual;
            AtualizarVisual(AvatarManager.Instance.AvatarSelecionado);
        }
        else
        {
            Debug.LogWarning("AvatarManager não encontrado.");
        }
    }

    private void OnDestroy()
    {
        if (botao != null)
        {
            botao.onClick.RemoveListener(Selecionar);
        }

        if (AvatarManager.Instance != null)
        {
            AvatarManager.Instance.OnAvatarAlterado -= AtualizarVisual;
        }
    }

    private void Selecionar()
    {
        if (AvatarManager.Instance == null)
        {
            Debug.LogWarning("AvatarManager não encontrado.");
            return;
        }

        AvatarManager.Instance.SelecionarAvatar(indiceAvatar);

        // Fecha o painel após escolher o avatar
        if (painelAvatares != null)
        {
            painelAvatares.SetActive(false);
        }
    }

    private void AtualizarVisual(int indiceSelecionado)
    {
        if (indicadorSelecionado != null)
        {
            indicadorSelecionado.SetActive(
                indiceSelecionado == indiceAvatar
            );
        }
    }
}