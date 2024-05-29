using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    public MazeCell mazeCellPrefab;
    public int mazeWidth = 10;
    public int mazeDepth = 10;
    public GameObject entrancePrefab; // Giriş prefabı
    public GameObject exitPrefab; // Çıkış prefabı

    public MazeCell[,] MazeGrid;
    private MazeCell StartCell; // Başlangıç hücresi
    private MazeFinish mazeFinish; // MazeFinish referansı

    public void Start()
    {
        // Zorluk seviyesini belirleme
        int selectedDifficulty = PlayerPrefs.GetInt("SelectedDifficulty", 0); // 0: Kolay (Varsayılan)
    
        // Zorluk seviyesine göre mazeWidth ve mazeDepth değerlerini ayarlama
        switch (selectedDifficulty)
        {
            case 0: // Kolay
                mazeWidth = 10;
                mazeDepth = 10;
                break;
            case 1: // Orta
                mazeWidth = 15;
                mazeDepth = 15;
                break;
            case 2: // Zor
                mazeWidth = 20;
                mazeDepth = 20;
                break;
            default: // Varsayılan olarak kolay seviyesini kullan
                mazeWidth = 10;
                mazeDepth = 10;
                break;
        }
        // MazeFinish bileşenini ekleme ve referansı başlatma
        mazeFinish = gameObject.AddComponent<MazeFinish>();
        mazeFinish.mazeGenerator = this;
        mazeFinish.exitPrefab = exitPrefab; // exitPrefab referansını MazeFinish'e atama

        StartCoroutine(GenerateMazeCoroutine());
    }

    public IEnumerator GenerateMazeCoroutine()
    {
        MazeGrid = new MazeCell[mazeWidth, mazeDepth];

        // Labirent hücrelerini oluşturma
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int z = 0; z < mazeDepth; z++)
            {
                MazeGrid[x, z] = Instantiate(mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
            }
        }

        // Labirenti oluşturma
        yield return StartCoroutine(GenerateMaze(null, MazeGrid[0, 0]));

        // Başlangıç noktasını ilk hücre olarak belirleme
        StartCell = MazeGrid[0, 0];

        // Başlangıç noktasına prefab ekleme
        if (entrancePrefab != null && StartCell != null)
        {
            Instantiate(entrancePrefab, StartCell.transform.position, Quaternion.identity);
        }

        // MazeFinish sınıfının çıkış noktasını ayarlamasını sağlama
        if (mazeFinish != null)
        {
            mazeFinish.SetRandomExit();
        }
        else
        {
            Debug.LogError("MazeFinish is not set");
        }
    }

    public IEnumerator GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        if (previousCell != null)
        {
            ClearWalls(previousCell, currentCell);
        }

        var unvisitedCells = GetUnvisitedCells(currentCell).ToList();

        while (unvisitedCells.Count > 0)
        {
            var nextCell = unvisitedCells[Random.Range(0, unvisitedCells.Count)];
            yield return StartCoroutine(GenerateMaze(currentCell, nextCell));
            unvisitedCells = GetUnvisitedCells(currentCell).ToList();
        }
    }

    public IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 1 < mazeWidth && !MazeGrid[x + 1, z].IsVisited)
        {
            yield return MazeGrid[x + 1, z];
        }

        if (x - 1 >= 0 && !MazeGrid[x - 1, z].IsVisited)
        {
            yield return MazeGrid[x - 1, z];
        }

        if (z + 1 < mazeDepth && !MazeGrid[x, z + 1].IsVisited)
        {
            yield return MazeGrid[x, z + 1];
        }

        if (z - 1 >= 0 && !MazeGrid[x, z - 1].IsVisited)
        {
            yield return MazeGrid[x, z - 1];
        }
    }

    public void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null || currentCell == null)
        {
            return;
        }

        int prevX = (int)previousCell.transform.position.x;
        int prevZ = (int)previousCell.transform.position.z;
        int currX = (int)currentCell.transform.position.x;
        int currZ = (int)currentCell.transform.position.z;

        if (prevX < currX)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
        }
        else if (prevX > currX)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
        }
        else if (prevZ < currZ)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
        }
        else if (prevZ > currZ)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
        }
    }
}
