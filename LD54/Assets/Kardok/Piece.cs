using UnityEngine;
using UnityEngine.EventSystems;

public class Piece : MonoBehaviour
{
    public int x, y;
    public bool emptyTile,extraTile;
    public int ArtID, pieceID;
    Puzzle puzzle;
    private void Awake()
    {
        puzzle = GetComponentInParent<Puzzle>();
        x = (int)transform.localPosition.x;
        y = (int)transform.localPosition.y;
    }
    void OnMouseDown()
    {
        if (!puzzle.isMoving)
        {
            if (!emptyTile&&!extraTile)
            {
                puzzle.targetTile = this;
                if (puzzle.emptyTile != null)
                {
                    puzzle.TryToMove();
                }
            }
            else if (extraTile)
            {
                if (!emptyTile)
                {
                    for (int i = 0; i < puzzle.emptyPieces.Count; i++)
                    {
                        if (puzzle.emptyPieces[i].x == puzzle.enterPoint.localPosition.x && puzzle.emptyPieces[i].y == puzzle.enterPoint.localPosition.y)
                        {
                            puzzle.emptyPieces[i].transform.SetParent(this.transform.parent);
                            puzzle.emptyPieces[i].transform.localPosition = transform.localPosition;
                            puzzle.emptyPieces[i].extraTile = true;
                            transform.SetParent(puzzle.transform);
                            transform.localPosition = new Vector2(puzzle.enterPoint.localPosition.x, puzzle.enterPoint.localPosition.y);
                            puzzle.emptyPieces.Remove(puzzle.emptyPieces[i]);
                            puzzle.pieces.Add(this);
                            x = (int)puzzle.enterPoint.localPosition.x;
                            y = (int)puzzle.enterPoint.localPosition.y;
                            this.extraTile = false;
                        }
                    }
                }
                else
                {
                    if (puzzle.targetTile!=null&&puzzle.targetTile.x == puzzle.enterPoint.localPosition.x && puzzle.targetTile.y == puzzle.enterPoint.localPosition.y)
                    {
                        puzzle.targetTile.transform.SetParent(this.transform.parent);
                        puzzle.targetTile.transform.localPosition = transform.localPosition;
                        puzzle.targetTile.extraTile = true;
                        transform.SetParent(puzzle.transform);
                        puzzle.emptyPieces.Add(this);
                        transform.localPosition = new Vector2(puzzle.enterPoint.localPosition.x, puzzle.enterPoint.localPosition.y);
                        puzzle.pieces.Remove(puzzle.targetTile);
                        x = (int)puzzle.enterPoint.localPosition.x;
                        y = (int)puzzle.enterPoint.localPosition.y;
                        this.extraTile = false;
                    }
                }
                puzzle.emptyTile = null;
                puzzle.targetTile = null;
                //puzzle.CheckWin();
            }
            else
            {
                puzzle.emptyTile = this;
                if (puzzle.targetTile != null)
                {
                    puzzle.TryToMove();
                }
            }
        }
    }
}