using UnityEngine;
using System;

public class AvatarManager : MonoBehaviour
{
    public static AvatarManager Instance;

    [Header("Avatares do Perfil")]
    [SerializeField] private Sprite[] avataresPerfil;

    [Header("Avatares da Home")]
    [SerializeField] private Sprite[] avataresHome;

    private const string CHAVE_AVATAR = "Colab_AvatarIndex";

    private int avatarSelecionado = 0;

    public event Action<int> OnAvatarAlterado;

    public int AvatarSelecionado => avatarSelecionado;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            CarregarAvatar();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CarregarAvatar()
    {
        avatarSelecionado = PlayerPrefs.GetInt(CHAVE_AVATAR, 0);

        if (avataresPerfil == null || avataresPerfil.Length == 0)
        {
            Debug.LogWarning("Nenhum avatar de Perfil foi configurado.");
            avatarSelecionado = 0;
            return;
        }

        if (avatarSelecionado < 0 || avatarSelecionado >= avataresPerfil.Length)
        {
            avatarSelecionado = 0;

            PlayerPrefs.SetInt(CHAVE_AVATAR, avatarSelecionado);
            PlayerPrefs.Save();
        }
    }

    public void SelecionarAvatar(int indice)
    {
        if (avataresPerfil == null || indice < 0 || indice >= avataresPerfil.Length)
        {
            Debug.LogWarning("Índice de avatar inválido: " + indice);
            return;
        }

        avatarSelecionado = indice;

        PlayerPrefs.SetInt(CHAVE_AVATAR, avatarSelecionado);
        PlayerPrefs.Save();

        OnAvatarAlterado?.Invoke(avatarSelecionado);

        Debug.Log("Avatar selecionado: " + avatarSelecionado);
    }

    public Sprite ObterAvatarPerfil()
    {
        if (avataresPerfil == null || avataresPerfil.Length == 0)
            return null;

        if (avatarSelecionado < 0 || avatarSelecionado >= avataresPerfil.Length)
            return null;

        return avataresPerfil[avatarSelecionado];
    }

    public Sprite ObterAvatarHome()
    {
        if (avataresHome == null || avataresHome.Length == 0)
            return null;

        if (avatarSelecionado < 0 || avatarSelecionado >= avataresHome.Length)
            return null;

        return avataresHome[avatarSelecionado];
    }
}