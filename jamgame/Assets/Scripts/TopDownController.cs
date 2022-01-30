using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour {
    int[,] tileGrid;
    Vector2 playerPosition = new Vector2(0,0);
    Vector2 gridSize = new Vector2(4,4);
    Vector2 topLeftPosition = new Vector2(-4, -18);
    void Start() {
        tileGrid = new int[2,3] {
            {0, -1, -50},
            {0, 0, -1}
        };
        DrawGrid();
        
    }
    

    public void Move(Vector2 moveDirection) {
        if (moveDirection.y > 0) {
            if (playerPosition.y != 0 && tileGrid[(int) playerPosition.x, (int) playerPosition.y-1] <= tileGrid[(int) playerPosition.x, (int) playerPosition.y]) {
                playerPosition.y++;
                transform.Translate(0, gridSize.y, 0);
            }
        } else if (moveDirection.y < 0) {


        } else if (moveDirection.x > 0) {

        } else if (moveDirection.x < 0) {

        }
    }

    void DrawGrid() {
        

        for (int i = 0; i < tileGrid.GetLength(0); i++) {
            for (int j = 0; j < tileGrid.GetLength(1); j++) {
                Vector2 positionofTile = new Vector2(topLeftPosition.x + gridSize.x*j, topLeftPosition.y + gridSize.y*i);
                DrawTiles(positionofTile, tileGrid[i,j]);
            }
        }
    }

    void DrawTiles(Vector2 position, int tileType) {

    }

    public void ConvertPosition() {
        float test = 20/(float)tileGrid.GetLength(1);
        for (int i = 1; i <= tileGrid.GetLength(1); i++) {
            if (transform.position.x < -10 + i*test) {
                playerPosition.x = i - 1;

                break;
            }
        }
        playerPosition.y = tileGrid.GetLength(0) - 1 - GetComponent<Player>().layer;
        transform.position = new Vector3(topLeftPosition.x + playerPosition.x*gridSize.x, topLeftPosition.y - playerPosition.y*gridSize.y, 0);

    }

    
}
