using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TopDownController : MonoBehaviour {
    int[,] tileGrid;
    Vector2 playerPosition = new Vector2(0,0);
    Vector2 gridSize = new Vector2(4,4);
    Vector2 topLeftPosition = new Vector2(-4, -18);
    void Start() {
 
        //coordinates are in y, x!!!
        tileGrid = new int[2,3] {
            {0, -1, -50},
            {-6, 0, -1}
        };
       
    }
   
 
    public void Move(Vector2 moveDirection) {
        if (moveDirection.y > 0) {
            if (playerPosition.y != 0 && tileGrid[(int) playerPosition.x, (int) playerPosition.y-1] <= tileGrid[(int) playerPosition.x, (int) playerPosition.y]) {
                playerPosition.y++;
                transform.Translate(0, gridSize.y, 0);
            } else {
                print(playerPosition.x + ", " + playerPosition.y);
            }
        } else if (moveDirection.y < 0) {
            if (playerPosition.y != tileGrid.GetLength(1) - 1 && tileGrid[(int)playerPosition.x, (int) playerPosition.y+1] <= tileGrid[(int) playerPosition.x, (int) playerPosition.y]) {
                playerPosition.y--;
                transform.Translate(0, -gridSize.y, 0);
            } else {
                print(playerPosition.x + ", " + playerPosition.y);
            }
 
 
        } else if (moveDirection.x > 0) {
 
        } else if (moveDirection.x < 0) {
 
        }
    }
 
 
 
    public void ConvertPosition() {
        float test = 20/(float)tileGrid.GetLength(1);
        for (int i = 1; i <= tileGrid.GetLength(1); i++) {
            if (transform.position.x < -10 + i*test) {
                playerPosition.x = i - 1;
 
                break;
            }
        }
        playerPosition.y = tileGrid.GetLength(0) - GetComponent<Player>().layer;
        print(playerPosition.x + "," + playerPosition.y + "," + tileGrid[1,0]);
        transform.position = new Vector3(topLeftPosition.x + playerPosition.x*gridSize.x, topLeftPosition.y - playerPosition.y*gridSize.y, 0);
 
    }
 
   
}
