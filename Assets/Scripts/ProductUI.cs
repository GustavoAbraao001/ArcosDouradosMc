using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProductUI : MonoBehaviour
{
    [Header("Elementos Visuais do Card")]
    public TMP_Text textoNome;
    public TMP_Text textoPreco;
    public Image imagemIcone;

  
    public void ConfigurarCard(ProductData produto)
    {
        if (textoNome != null) textoNome.text = produto.nomeItem;
        if (textoPreco != null) textoPreco.text = "R$ " + produto.preco.ToString("F2");
        if (imagemIcone != null) imagemIcone.sprite = produto.imagemProduto; 
    }
}