using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TopDownController : MonoBehaviour {
    public float[,] tileGrid;
    float[,] swapGrid;
    Vector2 playerPosition = new Vector2(0,0);
    Vector2 gridSize = new Vector2(4,4);
    Vector2 topLeftPosition = new Vector2(-4, -18);

    public float tileHeightScale = 1;
    void Start() {
 
        //coordinates are in y, x!!!
        tileGrid = new float[3,3] {
            {0, 0, -1},
            {0, 5, -2},
            {1, 5, 0}
        };
    //    tileGrid = new float[3,20] {
    //        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
    //        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    //    };
    }



    
   
 
    public void Move(Vector2 moveDirection) {
        
        // print(playerPosition.x + ", "+  playerPosition.y + ","+tileGrid[(int)playerPosition.y, (int) playerPosition.x]);
        if (moveDirection.y > 0) {
            if (playerPosition.y != 0 && tileGrid[(int) playerPosition.y-1, (int) playerPosition.x] <= tileGrid[(int) playerPosition.y, (int) playerPosition.x]) {
                playerPosition.y--;
                transform.Translate(0, gridSize.y, 0);
            }
        } else if (moveDirection.y < 0) {
            if (playerPosition.y != tileGrid.GetLength(0) - 1 && tileGrid[(int)playerPosition.y+1, (int) playerPosition.x] <= tileGrid[(int) playerPosition.y, (int) playerPosition.x]) {
                playerPosition.y++;
                transform.Translate(0, -gridSize.y, 0);
            }
 
        } else if (moveDirection.x > 0) {
            if (playerPosition.x != tileGrid.GetLength(1)-1 && tileGrid[(int)playerPosition.y, (int) playerPosition.x + 1] <= tileGrid[(int) playerPosition.y, (int) playerPosition.x]) {
                playerPosition.x++;
                transform.Translate(gridSize.x, 0, 0);
            }
 
        } else if (moveDirection.x < 0) {
            if (playerPosition.x != 0 && tileGrid[(int)playerPosition.y, (int) playerPosition.x - 1] <= tileGrid[(int) playerPosition.y, (int) playerPosition.x]) {
                playerPosition.x--;
                transform.Translate(-gridSize.x, 0, 0);
            }
        }
    }
 
 
 
    public void ConvertPosition() {
        float sliceLength = 20/(float)tileGrid.GetLength(1);
        for (int i = 1; i < tileGrid.GetLength(1) + 1; i++) {
            if (transform.position.x < -10 + i*sliceLength) {
                playerPosition.x = i - 1;
 
                break;
            }
        }
        playerPosition.y = GetComponent<Player>().layer;
        // print(playerPosition.x + "," + playerPosition.y + "," + tileGrid[(int) playerPosition.y, (int) playerPosition.x]);
        transform.position = new Vector3(topLeftPosition.x + playerPosition.x*gridSize.x, topLeftPosition.y - playerPosition.y*gridSize.y, 0);
 
    }

    public int ConvertBack() {
        
        transform.position = new Vector3(-9 + playerPosition.x * 18f/(tileGrid.GetLength(1)-1f), (float)0.75-3 + tileGrid[(int)playerPosition.y, (int)playerPosition.x]*tileHeightScale, 0);
        return (int)playerPosition.y;
    }
 
   
}
