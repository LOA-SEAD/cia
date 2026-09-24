public class Dados
{
    public static GameConfig config;
}

[System.Serializable]
public class Caso
{
    public string tamanho;

    public string detalhes;

    public string links;

    public string[] frases;

    public string[] palavras;

    public string[] tamanhoGrid;
}

[System.Serializable]
public class GameConfig {
    public int nroCasosPrincipais;

    public Caso[] casos;
}