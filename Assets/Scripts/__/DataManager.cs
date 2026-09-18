using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [Header("Configuração de Teste")]
    [Tooltip("Ative para apagar todos os PlayerPrefs quando o jogo iniciar.")]
    public bool zerarPlayerPrefsAoIniciar = false;

    [Header("Dados em Tempo Real")]
    public string nomeDoUsuario = "Visitante";
    public int totalXP = 0;
    public int tarefasConcluidas = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Só apaga os dados se vocês ativarem no Inspector
            if (zerarPlayerPrefsAoIniciar)
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();

                Debug.Log("PlayerPrefs zerados!");
            }

            CarregarDadosSalvos();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SalvarNomeUsuario(string nome)
    {
        nomeDoUsuario = nome;

        PlayerPrefs.SetString("NomeUsuarioBD", nome);
        PlayerPrefs.Save();
    }

    public void AdicionarXP(int quantidade)
    {
        totalXP += quantidade;
        tarefasConcluidas++;

        SalvarDados();

        Debug.Log(
            $"Ganhou {quantidade} XP | XP atual: {totalXP}"
        );
    }

    public bool GastarXP(int quantidade)
    {
        if (totalXP < quantidade)
        {
            Debug.Log(
                $"XP insuficiente! Você tem {totalXP} XP e precisa de {quantidade} XP."
            );

            return false;
        }

        totalXP -= quantidade;

        SalvarDados();

        Debug.Log(
            $"Gastou {quantidade} XP | XP restante: {totalXP}"
        );

        return true;
    }

    public int ObterXPAtual()
    {
        return totalXP;
    }

    private void SalvarDados()
    {
        PlayerPrefs.SetString(
            "NomeUsuarioBD",
            nomeDoUsuario
        );

        PlayerPrefs.SetInt(
            "TotalXPBD",
            totalXP
        );

        PlayerPrefs.SetInt(
            "TarefasBD",
            tarefasConcluidas
        );

        PlayerPrefs.Save();
    }

    private void CarregarDadosSalvos()
    {
        nomeDoUsuario = PlayerPrefs.GetString(
            "NomeUsuarioBD",
            "Colaborador"
        );

        totalXP = PlayerPrefs.GetInt(
            "TotalXPBD",
            0
        );

        tarefasConcluidas = PlayerPrefs.GetInt(
            "TarefasBD",
            0
        );
    }
}