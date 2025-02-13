using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject bigTilePrefab;

    public float xDiff = 2.3f;
    public float yDiff = 1f;
    public float yDiffBig = 1.5f;

    private float xPos = 0f;
    private float yPos = 0f;

    private string smallTag = "smallTile";
    private string bigTile  = "bigTile";

    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            GenerateTiles();
        }
    }

    public void GenerateTiles()
    {
        int random = Random.Range(0, 5);

        if (random <= 2 )
        {
            GenerateSmallTiles();
        }
        else
        {
            GenerateBigTiles();
        }
    }

    void GenerateSmallTiles()
    {
        xPos += xDiff;
        yPos += yDiff;

        tilePrefab.tag = smallTag;
        Instantiate(tilePrefab, new Vector2(xPos, yPos), tilePrefab.transform.rotation);
    }

    void GenerateBigTiles()
    {
        xPos += xDiff;
        yPos += yDiffBig;
        bigTilePrefab.tag = bigTile;
        Instantiate(bigTilePrefab, new Vector2(xPos, yPos), bigTilePrefab.transform.rotation);
    }
}
