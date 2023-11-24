using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class WordHunt : MonoBehaviour {

    public static WordHunt instance;

    //public TextAsset theme;

    [SerializeField] private TextAsset _csvFile;



    private CanvasGroup canvas;

    public delegate void VisualEvents(RectTransform original, RectTransform final);
    public static event VisualEvents FoundWord;

    public delegate void Events();
    public static event Events Finish;

    private ObjectivesController objController;
    private string[,] lettersGrid;
    private Transform[,] lettersTransforms;
    private string alphabet = "abcdefghijklmnopqrstuvwxyz";

    

    [Header("Settings")]
    public bool invertedWordsAreValid;
    public bool diagonalWordsAreValid;

    [Header("Text Asset")]
    private List<string> eachLine;
    public String[] casewords;
    public string data_string;
    public bool filterBadWords;
    public TextAsset badWordsSource;
    [Space]

    [Header("List of Words")]
    public List<string> words = new List<string>();
    public List<string> insertedWords = new List<string>();
    [Header("Grid Settings")]
    public Vector2 gridSize;
    [Space]

    [Header("Cell Settings")]
    public Vector2 cellSize;
    public Vector2 cellSpacing;
    [Space]

    [Header("Public References")]
    public GameObject letterPrefab;
    public Transform gridTransform;
    [Space]

    [Header("Game Detection")]
    public string word;
    public Vector2 orig;
    public Vector2 dir;
    public bool activated;

    [HideInInspector]
    public List<Transform> highlightedObjects = new List<Transform>();

    private void Awake()
    {
        Time.timeScale = 1;
        objController = GameObject.Find("ObjetivosBG").GetComponent<ObjectivesController>();
        //wordsSource = theme;
        Setup();

        //canvas.alpha = 0;
        //canvas.blocksRaycasts = false;

        //miniMenu.DOMoveY(0, .6f).SetEase(Ease.OutBack);
        instance = this;
    }

    public void Setup(){

        Read();

        PrepareWords();

        InitializeGrid();

        InsertWordsOnGrid();

        RandomizeEmptyCells();

        //DisplaySelectedWords();

    }

    private void PrepareWords()
    {
        //Pegar lista de palavras
        words = eachLine[PlayerPrefs.GetInt("LoadCaseId", 0)].Split(';').ToList();
        objController.SetNumberOfWords(words.Count);


        //Filtrar palavrões e etc..
        if (filterBadWords)
        {
            List<string> badWords = badWordsSource.text.Split(',').ToList();
            for (int i = 0; i < badWords.Count(); i++)
            {
                if(words.Contains(badWords[i])){
                    words.Remove(badWords[i]);
                    print("palavra ofensiva <b>" + badWords[i] + "</b> <color=red> removida</color>");
                }
            }
        }

        //Randomizar palavras
        for (int i = 0; i < words.Count; i++)
        {
            string temp = words[i];

            System.Random rn = new System.Random();

            int randomIndex = rn.Next(words.Count());
            words[i] = words[randomIndex];
            words[randomIndex] = temp;
        }

        //Filtrar as palavras que cabem na grid
        int maxGridDimension = Mathf.Max((int)gridSize.x, (int)gridSize.y);

        //Que palavras da lista cabem no grid
        words = words.Where(x => x.Length <= maxGridDimension).ToList();
    }

    private void InitializeGrid()
    {

        //Inicializar o tamanho dos arrays bidimensionais
        lettersGrid = new string[(int)gridSize.x, (int)gridSize.y];
        lettersTransforms = new Transform[(int)gridSize.x, (int)gridSize.y];


        //Passar por todos os elementos x e y da grid
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {

                lettersGrid[x, y] = "";
             
                GameObject letter = Instantiate(letterPrefab, transform.GetChild(0));
               
                letter.name = x.ToString() + "-" + y.ToString();
             
                lettersTransforms[x, y] = letter.transform;
                
            }
        }
  
        ApplyGridSettings();
     
    }

    void ApplyGridSettings()
    {
        GridLayoutGroup gridLayout = gridTransform.GetComponent<GridLayoutGroup>();

        gridLayout.cellSize = cellSize;
        gridLayout.spacing = cellSpacing;

        int cellSizeX = (int)gridLayout.cellSize.x + (int)gridLayout.spacing.x;

        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(cellSizeX * gridSize.x, 0);


    }

    void InsertWordsOnGrid()
    {
        foreach (string word in words)
        {

            System.Random rn = new System.Random();

            bool inserted = false;
            int tryAmount = 0;

            do
            {
                int row;
                int safetyFlag = 0;
                do
                {
                    safetyFlag++;
                    row = rn.Next((int)gridSize.x);
                } while (row + word.Length > gridSize.x && row - word.Length < 0 && safetyFlag < 30); //garantir que as palavras grandes caibam na horizaontal
                
                int column = rn.Next((int)gridSize.y);

                int dirX = 0; int dirY = 0;

                while (dirX == 0 && dirY == 0)
                {
                    
                    if (invertedWordsAreValid)
                    {
                        dirX = rn.Next(3) - 1;
                        dirY = rn.Next(3) - 1;
                    }
                    else
                    {
                        dirX = rn.Next(2);
                        dirY = rn.Next(2);
                    }

                    if (diagonalWordsAreValid == false)
                    {
                        if (rn.Next(2) == 0)
                        {
                            dirX = 0;
                        }
                        else
                        {
                            dirY = 0;
                        }
                    }

                    if (word.Length > gridSize.y)
                    {
                        dirY = 0;
                    }
                }

                inserted = InsertWord(word, row, column, dirX, dirY);
                tryAmount++;

            } while (!inserted && tryAmount < 100);

            if (inserted) { 
                insertedWords.Add(word);
          
            }
        }
    }

    private bool InsertWord(string word, int row, int column, int dirX, int dirY)
    {

        if (!CanInsertWordOnGrid(word, row, column, dirX, dirY))
            return false;

        for (int i = 0; i < word.Length; i++)
        {
            lettersGrid[(i * dirX) + row, (i * dirY) + column] = word[i].ToString();
            Transform t = lettersTransforms[(i * dirX) + row, (i * dirY) + column];
            t.GetComponentInChildren<Text>().text = word[i].ToString().ToUpper();
            //t.GetComponent<Image>().color = Color.grey;
        }

        return true;
    }

    private bool CanInsertWordOnGrid(string word, int row, int column, int dirX, int dirY)
    {
        if (dirX > 0)
        {
            if (row + word.Length > gridSize.x)
            {
                return false;
            }
        }
        if (dirX < 0)
        {
            if (row - word.Length < 0)
            {
                return false;
            }
        }
        if (dirY > 0)
        {
            if (column + word.Length > gridSize.y)
            {
                return false;
            }
        }
        if (dirY < 0)
        {
            if (column - word.Length < 0)
            {
                return false;
            }
        }

        for (int i = 0; i < word.Length; i++)
        {
            string currentCharOnGrid = (lettersGrid[(i * dirX) + row, (i * dirY) + column]);
            string currentCharOnWord = (word[i].ToString());

            if (currentCharOnGrid != String.Empty && currentCharOnWord != currentCharOnGrid)
            {
                return false;
            }
        }

        return true;
    }

    private void RandomizeEmptyCells()
    {

        System.Random rn = new System.Random();

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (lettersGrid[x, y] == string.Empty)
                {
                    lettersGrid[x, y] = alphabet[rn.Next(alphabet.Length)].ToString();
                    lettersTransforms[x, y].GetComponentInChildren<Text>().text = lettersGrid[x, y].ToUpper();
                }
            }
        }
    }

    public void LetterClick(int x, int y, bool state)
    {
        activated = state;
        orig = state ? new Vector2(x, y) : orig;
        dir = state ? dir : new Vector2(-1, -1);

        if (!state)
        {
            ValidateWord();
        }

    }

    private void ValidateWord()
    {
        word = string.Empty;

        foreach (Transform t in highlightedObjects)
        {
            word += t.GetComponentInChildren<Text>().text.ToLower();
            
        }

        
        foreach(string w in insertedWords){
            print(w);
        }

        if (insertedWords.Contains(word) || insertedWords.Contains(Reverse(word)))
        {
            print("entrou");
            foreach (Transform h in highlightedObjects)
            {
                h.GetComponent<Image>().color = new Color32(128, 255, 128, 255);
                h.transform.DOPunchScale(-Vector3.one, 0.2f, 10, 1);
                h.GetComponent<LetterObjectScript>().hasPainted = true;
            }

            //Visual Event
            RectTransform r1 = highlightedObjects[0].GetComponent<RectTransform>();
            RectTransform r2 = highlightedObjects[highlightedObjects.Count() - 1].GetComponent<RectTransform>();
            FoundWord(r1, r2);

            objController.CountObjective(word);

            //ScrollViewWords.instance.CheckWord(word);

            insertedWords.Remove(word);
            insertedWords.Remove(Reverse(word));

            if(insertedWords.Count <= 0)
            {
                Finish();
            }
        }
        else {
            ClearWordSelection();
        }
    }

    public void LetterHover(int x, int y)
    {
        if (activated)
        {
            dir = new Vector2(x, y);
            if (IsLetterAligned(x, y))
            {
                HighlightSelectedLetters(x,y);
            }
        }
    }

    private void HighlightSelectedLetters(int x, int y)
    {

        ClearWordSelection();

        Color selectColor = new Color32(150,145,190,255);

        if (x == orig.x)
        {
            int min = (int)Math.Min(y, orig.y);
            int max = (int)Math.Max(y, orig.y);

            for (int i = min; i <= max; i++)
            {
                
              lettersTransforms[x, i].GetComponent<Image>().color = selectColor;
              highlightedObjects.Add(lettersTransforms[x, i]);
                
            }
        }
        else if (y == orig.y)
        {
            int min = (int)Math.Min(x, orig.x);
            int max = (int)Math.Max(x, orig.x);

            for (int i = min; i <= max; i++)
            {
                lettersTransforms[i, y].GetComponent<Image>().color = selectColor;
                highlightedObjects.Add(lettersTransforms[i, y]);
            }
        }
        else
        {

            // Increment according to direction (left and up decrement)
            int incX = (orig.x > x) ? -1 : 1;
            int incY = (orig.y > y) ? -1 : 1;
            int steps = (int)Math.Abs(orig.x - x);

            // Paints from (orig.x, orig.y) to (x, y)
            for (int i = 0, curX = (int)orig.x, curY = (int)orig.y; i <= steps; i++, curX += incX, curY += incY)
            {
                lettersTransforms[curX, curY].GetComponent<Image>().color = selectColor;
                highlightedObjects.Add(lettersTransforms[curX, curY]);
            }
        }

    }

    private void ClearWordSelection()
    {
        foreach (Transform  h in highlightedObjects)
        {

            if (h.GetComponent<LetterObjectScript>().hasPainted == true)
            {
                h.GetComponent<Image>().color = new Color32(128, 255, 128, 255);
                //h.GetComponent<bool>().hasPainted

            }
            else
            {
                h.GetComponent<Image>().color = Color.white;  

            }

        }

        highlightedObjects.Clear();
    }

    public bool IsLetterAligned(int x, int y)
    {
        return (orig.x == x || orig.y == y || Math.Abs(orig.x - x) == Math.Abs(orig.y - y));
    }



    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    void Read()
    {

        data_string = _csvFile.text;
        eachLine = new List<string>();
        eachLine.AddRange(data_string.Split("|"[0]));
        //casewords = eachLine[PlayerPrefs.GetInt("LoadCaseId", 0)].Split(';');

    }

}
