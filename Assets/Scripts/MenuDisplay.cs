using UnityEngine;

public class MenuDisplay : MonoBehaviour
{
    [Header("Configurações de Exibição")]
    public GameObject prefabCard;
    public Transform contentParent;
    public ProductData.TipoCategoria categoriaDesejada;

    public void AtualizarEExibirMenu()
    {
        if (prefabCard == null || contentParent == null)
        {
            Debug.LogWarning("Faltam referências no MenuDisplay de " + gameObject.name);
            return;
        }

        foreach (Transform filho in contentParent)
        {
            Destroy(filho.gameObject);
        }

        if (MenuManager.Instance != null)
        {
            var produtos = MenuManager.Instance.ObterProdutosPorCategoria(categoriaDesejada);

            foreach (var produto in produtos)
            {
                GameObject novoCard = Instantiate(prefabCard, contentParent);
                ProductUI ui = novoCard.GetComponent<ProductUI>();

                if (ui != null)
                {
                    ui.ConfigurarCard(produto);
                }
            }
        }
    }
}

