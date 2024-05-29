using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeFinish : MonoBehaviour
{
    public MazeGenerator mazeGenerator;
    public GameObject exitPrefab; // Çıkış prefabı referansı

    public void SetRandomExit()
    {
        int side = Random.Range(0, 4);

        int randomX = 0;
        int randomZ = 0;

        if (mazeGenerator != null && exitPrefab != null)
        {
            switch (side)
            {
                case 0: // Kuzey kenarı
                    randomX = Random.Range(0, mazeGenerator.mazeWidth);
                    randomZ = mazeGenerator.mazeDepth - 1;
                    break;
                case 1: // Güney kenarı
                    randomX = Random.Range(0, mazeGenerator.mazeWidth);
                    randomZ = 0;
                    break;
                case 2: // Doğu kenarı
                    randomX = mazeGenerator.mazeWidth - 1;
                    randomZ = Random.Range(0, mazeGenerator.mazeDepth);
                    break;
                case 3: // Batı kenarı
                    randomX = 0;
                    randomZ = Random.Range(0, mazeGenerator.mazeDepth);
                    break;
            }

            MazeCell exitCell = mazeGenerator.MazeGrid[randomX, randomZ];
            
            // Çıkış hücresinin bulunduğu kenara göre gerekli duvarı temizle
            switch (side)
            {
                case 0:
                    exitCell.ClearFrontWall();
                    break;
                case 1:
                    exitCell.ClearBackWall();
                    break;
                case 2:
                    exitCell.ClearRightWall();
                    break;
                case 3:
                    exitCell.ClearLeftWall();
                    break;
            }

            Instantiate(exitPrefab, exitCell.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("MazeGenerator or exitPrefab is not set");
        }
    }
}
