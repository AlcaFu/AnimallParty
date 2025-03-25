using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    float yPos;
    TileGenerator _tileGenerator;

    void Start()
    {
        yPos = transform.position.y;
        _tileGenerator = GameObject.Find("TileGenerator").GetComponent<TileGenerator>();
    }

    void Update()
    {
        if (transform.position.y < yPos - 10f)
        {
            _tileGenerator.GenerateTiles();
            Destroy(this.gameObject);
        }
    }
}
