using System.Collections.Generic;
using UnityEngine;


public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Banco de Dados Local (Mock)")]
    public List<ProductData> listaDeProdutos; 

    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
  

   
    public List<ProductData> ObterProdutosPorCategoria(ProductData.TipoCategoria categoriaDesejada)
    {
        List<ProductData> filtrados = new List<ProductData>();

        foreach (var produto in listaDeProdutos)
        {
            if (produto.categoria == categoriaDesejada)
            {
                filtrados.Add(produto);
            }
        }

        return filtrados;
    }
}

