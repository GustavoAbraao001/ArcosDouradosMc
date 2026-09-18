using UnityEngine;
using UnityEngine.UI;

public class ExibirAvatarHome : MonoBehaviour
{
    [Header("Imagem do Avatar na Home")]
    [SerializeField] private Image imagemAvatar;

    private bool inscritoNoEvento = false;

    private void OnEnable()
    {
        AtualizarAvatar();
        InscreverEvento();
    }

    private void Start()
    {
        AtualizarAvatar();
        InscreverEvento();
    }

    private void OnDisable()
    {
        RemoverEvento();
    }

    private void OnDestroy()
    {
        RemoverEvento();
    }

    private void InscreverEvento()
    {
        if (AvatarManager.Instance == null)
            return;

        if (inscritoNoEvento)
            return;

        AvatarManager.Instance.OnAvatarAlterado += AvatarMudou;

        inscritoNoEvento = true;
    }

    private void RemoverEvento()
    {
        if (!inscritoNoEvento)
            return;

        if (AvatarManager.Instance != null)
        {
            AvatarManager.Instance.OnAvatarAlterado -= AvatarMudou;
        }

        inscritoNoEvento = false;
    }

    private void AvatarMudou(int novoIndice)
    {
        AtualizarAvatar();
    }

    private void AtualizarAvatar()
    {
        if (AvatarManager.Instance == null)
            return;

        if (imagemAvatar == null)
        {
            Debug.LogWarning("Imagem do avatar da Home não configurada.");
            return;
        }

        Sprite avatarAtual = AvatarManager.Instance.ObterAvatarHome();

        if (avatarAtual != null)
        {
            imagemAvatar.sprite = avatarAtual;
        }
    }
}