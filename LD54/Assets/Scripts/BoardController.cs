using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

[Flags]
public enum AvailableMovements
{
    None = 0,
    Right = 1,
    Left = 2,
    Up = 4,
    Down = 8,
}

public class BoardController : MonoBehaviour
{
    public static BoardController Instance;
    private const int TilePixelSize = 256;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Sprite _maskSprite;
    [SerializeField] private TextMeshProUGUI _winPercentageText;
    private int _colCount;
    private int _rowCount;
    private Tile[,] _tiles;

    private bool _isActive = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Clear()
    {
        if (_tiles != null)
        {
            foreach (Tile tile in _tiles)
            {
                if (tile == null)
                {
                    continue;
                }

                tile.OnTilePosChange = null;
                Destroy(tile.gameObject);
            }

            _tiles = null;
        }
    }

    private bool IsActive()
    {
        return !SettingsScreen.Instance.IsPaused() || !WinScreen.Instance.IsWin();
    }

    public void Setup(Texture2D texture)
    {
        Clear();
        Tile.ClearStatic();
        if (texture.width % TilePixelSize != 0 || texture.height % TilePixelSize != 0)
        {
            Debug.LogError("Texture size not match");
            return;
        }

        CreateTiles(texture);
        ShuffleTiles();
        CheckWinCondition();
        _cameraController.SetCamera(_colCount,_rowCount);
    }

    private void OnTileIndexChange(Tile tile,int currentCol ,int currentRow,int col, int row)
    {
        Tile tempTile = _tiles[col, row];
        _tiles[col, row] = tile;
        _tiles[currentCol, currentRow] = tempTile;
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        int totalTiles = _tiles.Length - 1;
        int totalSolvedTiles = 0;

        foreach (var tile in _tiles)
        {
            if (tile == null)
            {
                continue;
            }
            if (tile.IsInPlace())
            {
                totalSolvedTiles++;
            }
        }

        float percentage = (float) totalSolvedTiles / totalTiles;
        _winPercentageText.text = $"Win Percentage: {percentage:0.00}";
        if (percentage > 0.4F)
        {
            WinScreen.Instance.ShowWinScreen();
        }
    }

    // remove flag
    // value &= ~MyEnum.Flag2; //value is now Flag1, Flag3    
    private AvailableMovements GetAvailableMovements(Tile tile)
    {
        AvailableMovements availableMovements = AvailableMovements.None;

        if (tile.CurrentRow > 0 && _tiles[tile.CurrentCol, tile.CurrentRow - 1] == null)
        {
            availableMovements |= AvailableMovements.Down;
        }

        if (tile.CurrentRow < _rowCount - 1 && _tiles[tile.CurrentCol, tile.CurrentRow + 1] == null)
        {
            availableMovements |= AvailableMovements.Up;
        }

        if (tile.CurrentCol > 0 && _tiles[tile.CurrentCol - 1, tile.CurrentRow] == null)
        {
            availableMovements |= AvailableMovements.Left;
        }

        if (tile.CurrentCol < _colCount - 1 && _tiles[tile.CurrentCol + 1, tile.CurrentRow] == null)
        {
            availableMovements |= AvailableMovements.Right;
        }
        Debug.Log(availableMovements.ToString());
        return availableMovements;
    }

    private void Update()
    {
        if (!IsActive())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, LayerMask.GetMask("Tile"));

            if (hit.collider != null)
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                tile.StartDrag(GetAvailableMovements(tile));
            }
        }
    }

    public void ShuffleTiles()
    {
        _tiles.Shuffle2D();
        for (int colIndex = 0; colIndex < _colCount; colIndex++)
        {
            for (int rowIndex = 0; rowIndex < _rowCount; rowIndex++)
            {
                Tile tile = _tiles[colIndex, rowIndex];
                if (tile == null)
                {
                    continue;
                }

                tile.SetCurrentIndex(colIndex, rowIndex);
                tile.transform.position = new Vector3(colIndex, rowIndex, 0);
            }
        }
    }

    private void CreateTiles(Texture2D texture)
    {
        _colCount = texture.width / TilePixelSize;
        _rowCount = texture.height / TilePixelSize;

        _tiles = new Tile[_colCount, _rowCount];

        for (int colIndex = 0; colIndex < _colCount; colIndex++)
        {
            for (int rowIndex = 0; rowIndex < _rowCount; rowIndex++)
            {
                if (colIndex == _colCount - 1 && rowIndex == 0)
                {
                    continue;
                }

                Color[] pixels = texture.GetPixels(colIndex * TilePixelSize, rowIndex * TilePixelSize, TilePixelSize,
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
                sr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                var mask = go.AddComponent<SpriteMask>();
                mask.sprite = _maskSprite;
                var tile = go.AddComponent<Tile>();
                tile.Initialize(colIndex, rowIndex);
                tile.SetCurrentIndex(colIndex, rowIndex);
                tile.OnTilePosChange += OnTileIndexChange;
                go.AddComponent<BoxCollider2D>();

                go.transform.position = new Vector3(colIndex, rowIndex, 0);
                go.layer = LayerMask.NameToLayer("Tile");

                _tiles[colIndex, rowIndex] = tile;
            }
        }
    }
}