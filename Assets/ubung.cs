using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ubung : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      int lives = 0;
      if(lives==0){
       Debug.Log("Game Over");
      }
     for(int i = 1; i <= 10; i++){
        Debug.Log("Zahl" + i);
     }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
