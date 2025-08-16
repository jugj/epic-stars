using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spielerbewegung : MonoBehaviour
{
    public float Geschwindigkeit=5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        if(Input.GetKey("w")){
        transform.Translate(Vector2.up * Time.deltaTime * Geschwindigkeit);
        }

        if(Input.GetKey("a")){
        transform.Translate(Vector2.left * Time.deltaTime * Geschwindigkeit);
        }

        if(Input.GetKey("d")){
        transform.Translate(Vector2.right * Time.deltaTime * Geschwindigkeit);
        }

        if(Input.GetKey("s")){
        transform.Translate(Vector2.down * Time.deltaTime * Geschwindigkeit);
        }       
    }
}
