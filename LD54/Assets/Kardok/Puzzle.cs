using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Puzzle : MonoBehaviour
{
    public Piece emptyTile,targetTile;
    public Transform enterPoint;
    public bool isMoving;
    public List<Piece> pieces,emptyPieces;
    private void Start()
    {
        pieces.AddRange(GetComponentsInChildren<Piece>());
        for(int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].emptyTile)
            {
                emptyPieces.Add(pieces[i]);
                pieces.Remove(pieces[i]);
                i--;
            }else if (pieces[i].extraTile)
            {
                pieces.Remove(pieces[i]);
                i--;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isMoving)
        {
            emptyTile.transform.localPosition = Vector2.MoveTowards(emptyTile.transform.localPosition,new Vector2 (emptyTile.x, emptyTile.y),Time.fixedDeltaTime);
            targetTile.transform.localPosition = Vector2.MoveTowards(targetTile.transform.localPosition,new Vector2 (targetTile.x, targetTile.y),Time.fixedDeltaTime);
            if (emptyTile.transform.localPosition == new Vector3(emptyTile.x, emptyTile.y)&& targetTile.transform.localPosition == new Vector3(targetTile.x, targetTile.y))
            {
                StartCoroutine(MoveCoolDown());
            }
        }
    }
    public void TryToMove()
    {
        if (emptyTile.x ==  targetTile.x + 1&& emptyTile.y== targetTile.y)
        {
            MoveTiles(emptyTile, targetTile);
        }
        else if(emptyTile.x ==  targetTile.x - 1 && emptyTile.y ==  targetTile.y)
        {
            MoveTiles(emptyTile, targetTile);
        }
        else if (emptyTile.y ==  targetTile.y + 1 && emptyTile.x ==  targetTile.x)
        {
            MoveTiles(emptyTile, targetTile);
        }
        else if (emptyTile.y ==  targetTile.y - 1 && emptyTile.x ==  targetTile.x)
        {
            MoveTiles(emptyTile, targetTile);
        }
    }
    void MoveTiles(Piece piece, Piece target)
    {
        int pieceX = piece.x;
        int pieceY = piece.y;
        piece.x =  target.x;
        piece.y =  target.y;
        target.x = pieceX;
        target.y = pieceY;
        StartCoroutine(MoveWait());
    }
    IEnumerator MoveWait()
    {
        yield return new WaitForSeconds(.5f);
        isMoving = true;
    }
    IEnumerator MoveCoolDown()
    {
        yield return new WaitForSeconds(.2f);
        emptyTile = null;
        targetTile = null;
        isMoving = false;
    }
}