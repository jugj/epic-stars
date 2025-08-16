using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seilbahn : MonoBehaviour
{
    public Animator animator;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.SetBool("Winning", true);
            StartCoroutine(player.death());
            player.lastcheckpoint = new Vector3(-6,0,0);
        }
    }
}