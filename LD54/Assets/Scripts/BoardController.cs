using System;
using DefaultNamespace;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    private const int TilePixelSize = 64;
    [SerializeField] private Texture2D _texture;
    private int _colCount;
    private int _rowCount;
    private Tile[,] _tiles;

    private void Awake()
    {
        if (_texture.width % TilePixelSize != 0 || _texture.height % TilePixelSize != 0)
        {
            Debug.LogError("Texture size not match");
            return;
        }

        CreateTiles();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, LayerMask.GetMask("Tile"));

            if (hit.collider != null)
            {
                hit.collider.GetComponent<Tile>().StartDrag();
            }
        }
    }

    private void CreateTiles()
    {
        _colCount = _texture.width / TilePixelSize;
        _rowCount = _texture.height / TilePixelSize;

        _tiles = new Tile[_colCount, _rowCount];

        for (int colIndex = 0; colIndex < _colCount; colIndex++)
        {
            for (int rowIndex = 0; rowIndex < _rowCount; rowIndex++)
            {
                Color[] pixels = _texture.GetPixels(colIndex * TilePixelSize, rowIndex * TilePixelSize, TilePixelSize,
                    TilePixelSize);
                var tileTexture = new Texture2D(TilePixelSize, TilePixelSize);
                tileTexture.SetPixels(0, 0, TilePixelSize, TilePixelSize, pixels);
                tileTexture.Apply();

                Sprite sprite = Sprite.Create(
                    tileTexture,
                    new Rect(0.0f, 0.0f, TilePixelSize, TilePixelSize),
                    new Vector2(0.5f, 0.5f),
                    TilePixelSize
                );

                GameObject go = new GameObject($"{colIndex} - {rowIndex}");
                var sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = sprite;

                var tile = go.AddComponent<Tile>();
                tile.Initialize();

                go.AddComponent<BoxCollider2D>();

                go.transform.position = new Vector3(colIndex, rowIndex, 0);
                go.layer = LayerMask.NameToLayer("Tile");

                _tiles[colIndex, rowIndex] = tile;
            }
        }
    }
}