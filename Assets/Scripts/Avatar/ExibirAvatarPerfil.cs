using UnityEngine;
using UnityEngine.UI;

public class ExibirAvatarPerfil : MonoBehaviour
{
    [Header("Imagem do Avatar")]
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
        {
            return;
        }

        if (inscritoNoEvento)
        {
            return;
        }

        AvatarManager.Instance.OnAvatarAlterado += AvatarMudou;

        inscritoNoEvento = true;
    }

    private void RemoverEvento()
    {
        if (!inscritoNoEvento)
        {
            return;
        }

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
        {
            return;
        }

        if (imagemAvatar == null)
        {
            Debug.LogWarning(
                "Imagem do avatar não foi configurada em " + gameObject.name
            );

            return;
        }

        Sprite avatarAtual = AvatarManager.Instance.ObterAvatarPerfil();

        if (avatarAtual != null)
        {
            imagemAvatar.sprite = avatarAtual;
        }
    }
}