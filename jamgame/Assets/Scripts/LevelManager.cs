using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    [SerializeField] string newLevel;
    void update() {
        print("1");
        OnTriggerEnter2D(GetComponent<BoxCollider2D>());
    }

    void OnTriggerEnter2D(Collider2D other) {
        print("hi");
        if(other.CompareTag("Player")) SceneManager.LoadScene(newLevel);
    }

}
