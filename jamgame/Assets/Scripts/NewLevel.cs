using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevel : MonoBehaviour {
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField] string newLevel;

    void Update()
    {
        print("hi");
    }
    void OnTriggerEnter2D(Collider2D other) {
        print("hi");
        if(other.CompareTag("Player")) SceneManager.LoadScene(newLevel);
    }
}
