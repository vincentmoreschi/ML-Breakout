using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    public static LevelManager Instance { get; private set; }

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

    public int initialLevel;

    public Sprite[] brickSprites;  // { 1 hp brick, 2 hp brick, 3 hp brick }
    public Color[] brickColors;
    public Brick brickPrefab;

    public string levelsHpFile;
    public string levelsColorsFile;

    private List<int[,]> _levelsHpData;  // Brick hp data for all levels
    private List<int[,]> _levelsColorsData;  // Brick color data for all levels

    private int _levelRows = 15;
    private int _levelCols = 12;
    private Vector3 _initialBrickPosition = new Vector3(-1.99f, 3.377f, 0f);  // Top-left brick position in an environment centered on (0, 0, 0)
    private float _shift = 0.365f;  // Brick width + padding

    // Start is called before the first frame update
    void Start()
    {
        _levelsHpData = LoadLevelsHpData();
        _levelsColorsData = LoadLevelsColorsData();

        foreach (Player player in GameManager.Instance.players)
        {
            if (player != null)
            {
                GenerateLevel(player, player.currentLevel);
            }
        }
    }

    private void OnEnable()
    {
        Brick.OnBrickDestruction += LevelCompletion;
    }

    private void OnDisable()
    {
        // Necessary for re-loading game scenes
        Brick.OnBrickDestruction -= LevelCompletion;
    }

    private void LevelCompletion(Brick brick)
    {
        Player player = brick.player;

        if (CheckLevelCompletion(player))
        {
            if (CheckFinalLevel(player))
            {
                BallManager.Instance.DestroyBalls(player);

                UIManager.Instance.UpdateFinalScoreText(player);
                player.victoryScreen.SetActive(true);
            }
            else
            {
                // TODO: level change visual?
                player.currentLevel++;

                GenerateLevel(player, player.currentLevel);
                UIManager.Instance.UpdateLevelText(player);
                BallManager.Instance.ResetBall(player);
                //player.paddle.ResetPosition();

                player.gameStarted = false;
            }
        }
    }

    public bool CheckLevelCompletion(Player player)
    {
        return player.RemainingBricks == 0;
    }
    public bool CheckFinalLevel(Player player)
    {
        return player.currentLevel == _levelsHpData.Count;
    }

    /// <summary>
    /// Destroy all bricks in the current level.
    /// </summary>
    public void ClearLevel(Player player)
    {
        // Destroy all current bricks
        for (int i = 0; i < player.bricksContainer.transform.childCount; i++)
        {
            Destroy(player.bricksContainer.transform.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Reset the current level to level 1 and update the UI.
    /// </summary>
    public void ResetLevels(Player player)
    {
        ClearLevel(player);

        player.currentLevel = 1;
        GenerateLevel(player, player.currentLevel);

        UIManager.Instance.UpdateLevelText(player);
    }

    /// <summary>
    /// Generate the current level.
    /// </summary>
    /// <param name="currentLevel">Current level in the game.</param>
    public void GenerateLevel(Player player, int currentLevel)
    {
        int[,] levelHpData = _levelsHpData[currentLevel - 1];
        int[,] levelColorData = _levelsColorsData[currentLevel - 1];

        player.RemainingBricks = 0;

        // Local position is affected by parent position
        float currentX = player.transform.position.x + _initialBrickPosition.x;
        float currentY = _initialBrickPosition.y;

        for (int i = 0; i < levelHpData.GetLength(0); i++)
        {
            for (int j = 0; j < levelHpData.GetLength(1); j++)
            {
                int brickHp = levelHpData[i, j];
                int brickColor = levelColorData[i, j];

                if (brickHp > 0)
                {
                    Brick brick = Instantiate(brickPrefab, new Vector3(currentX, currentY, _initialBrickPosition.z), Quaternion.identity) as Brick;
                    brick.Init(player, player.bricksContainer.transform, brickSprites[brickHp - 1], brickColors[brickColor - 1], brickHp);

                    player.RemainingBricks++;
                }

                currentX += _shift;
            }

            currentX = player.transform.position.x + _initialBrickPosition.x;
            currentY -= _shift;
        }
    }

    /// <summary>
    /// Load levels data (hitpoints) from text file using the LoadLevelsData method.
    /// 
    /// Note that the hitpoints data must match the colors data, i.e. all bricks present in
    /// one file must be present in the other file.
    /// 
    /// Integers in the text file represent bricks and their associated hitpoints.
    ///   0: No brick
    ///   1: Brick with 1 hp
    ///   2: Brick with 2 hp
    ///   3: Brick with 3 hp
    /// </summary>
    /// <returns>List of levels data (hitpoints)</returns>
    private List<int[,]> LoadLevelsHpData()
    {
        return LoadLevelsData(levelsHpFile);
    }

    /// <summary>
    /// Load levels data (colors) from text file using the LoadLevelsData method.
    /// 
    /// Note that the colors data must match the hitpoints data, i.e. all bricks present in
    /// one file must be present in the other file.
    /// 
    /// Integers in the text file represent bricks and their associated color.
    ///   0: No brick
    ///   1: Brick with color brickColors[0]
    ///   ...
    ///   5: Brick with color brickColors[4]
    /// </summary>
    /// <returns>List of levels data (colors)</returns>
    private List<int[,]> LoadLevelsColorsData()
    {
        return LoadLevelsData(levelsColorsFile);
    }

    /// <summary>
    /// Load levels data from text file.
    /// 
    /// Within the text file:
    /// The end of a level is signified by "---".
    /// Each level must consist of _levelRows rows and _levelCols columns, which identify brick positions. These positions are separated by commas.
    /// </summary>
    /// <param name="fileName">The file to be loaded.</param>
    /// <returns>List of levels data</returns>
    private List<int[,]> LoadLevelsData(string fileName)
    {
        List<int[,]> levelsData = new List<int[,]>();

        TextAsset levelsAsset = Resources.Load(fileName) as TextAsset;
        string[] lines = levelsAsset.text.TrimEnd().Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

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
