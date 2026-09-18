using UnityEngine;



[CreateAssetMenu(fileName = "NovoProduto", menuName = "McDonalds/ProdutoData")]
public class ProductData : ScriptableObject
{
    [Header("Informações do Item")]
    public string nomeItem;
    [TextArea] public string descricao;
    public float preco;
    public Sprite imagemProduto;

    [Header("Categoria")]
    public TipoCategoria categoria;

    public enum TipoCategoria
    {
        Lanche,
        Acompanhamento,
        Bebida,
        RecompensaMcRewards
    }
}

