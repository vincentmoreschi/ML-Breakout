using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    #region Singleton
    public static LevelController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public List<int[,]> levelsData { get; private set; }  // To be loaded from Resources/levels.txt
    public List<Brick> bricks { get; private set; }  // List of all instantiated bricks
    public Sprite[] brickSprites;  // { 1 hp brick, 2 hp brick, 3 hp brick }
    public Brick brickPrefab;

    public int currentLevel;

    private GameObject _bricksContainer;  // Container to hold instantiated bricks

    private int _levelRows = 15;
    private int _levelCols = 12;

    private Vector3 _initialBrickPosition = new Vector3(-1.99f, 3.377f, 0f);  // Top-left brick position
    private float _shift = 0.365f;  // Brick width + padding

    // Start is called before the first frame update
    void Start()
    {
        _bricksContainer = new GameObject("Bricks Container");

        levelsData = LoadLevelsData();
        GenerateLevel(currentLevel);
    }

    /// <summary>
    /// Generate the current level.
    /// </summary>
    /// <param name="currentLevel">Current level in the game.</param>
    private void GenerateLevel(int currentLevel)
    {
        int[,] levelData = levelsData[currentLevel - 1];

        bricks = new List<Brick>();

        float currentX = _initialBrickPosition.x;
        float currentY = _initialBrickPosition.y;

        for (int i = 0; i < levelData.GetLength(0); i++)
        {
            for (int j = 0; j < levelData.GetLength(1); j++)
            {
                int brickHp = levelData[i, j];
                if (brickHp > 0)
                {
                    Brick brick = Instantiate(brickPrefab, new Vector3(currentX, currentY, _initialBrickPosition.z), Quaternion.identity) as Brick;
                    brick.Init(_bricksContainer.transform, brickSprites[brickHp - 1], brickHp);

                    bricks.Add(brick);
                }

                currentX += _shift;
            }

            currentX = _initialBrickPosition.x;
            currentY -= _shift;
        }
    }

    /// <summary>
    /// Load levels data from text file.
    /// 
    /// Within the text file:
    /// The end of a level is signified by "---".
    /// Each level must consist of _levelRows rows and _levelCols columns, which identify brick positions. These positions are separated by commas.
    /// Integers represent bricks and their associated hp.
    ///   0: No brick
    ///   1: Brick with 1 hp
    ///   2: Brick with 2 hp
    ///   3: Brick with 3 hp
    /// </summary>
    /// <returns>List of levels data</returns>
    private List<int[,]> LoadLevelsData()
    {
        List<int[,]> levelsData = new List<int[,]>();

        TextAsset levelsAsset = Resources.Load("levels") as TextAsset;
        string[] lines = levelsAsset.text.TrimEnd().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        int[,] curLevel = new int[_levelRows, _levelCols];
        int matrixRow = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            if (string.Equals(line, "---"))  // If the current level has been fully parsed
            {
                levelsData.Add(curLevel);

                curLevel = new int[_levelRows, _levelCols];
                matrixRow = 0;
            }
            else
            {
                string[] lineBricks = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < lineBricks.Length; j++)
                {
                    curLevel[matrixRow, j] = int.Parse(lineBricks[j]);
                }

                matrixRow++;
            }
        }

        return levelsData;
    }
}