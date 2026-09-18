using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AbrirPainelAvatar : MonoBehaviour
{
    [Header("Painel de escolha de avatar")]
    [SerializeField] private GameObject painelAvatares;

    private Button botao;

    private void Awake()
    {
        botao = GetComponent<Button>();
    }

    private void Start()
    {
        botao.onClick.AddListener(AbrirPainel);
    }

    private void OnDestroy()
    {
        if (botao != null)
        {
            botao.onClick.RemoveListener(AbrirPainel);
        }
    }

    private void AbrirPainel()
    {
        if (painelAvatares != null)
        {
            painelAvatares.SetActive(true);
        }
    }
}