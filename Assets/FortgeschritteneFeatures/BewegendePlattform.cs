using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basierend auf diesen Tutorials: https://www.youtube.com/watch?v=GtX1p4cwYOc und https://www.youtube.com/watch?v=pWh5G17US5U

public class BewegendePlattform : MonoBehaviour
{
    public Transform[] zielPunkte;
    public int aktuellerPunkt = 0;
    public float geschwindigkeit = 2f;

    void Start() { 
        transform.position = zielPunkte[aktuellerPunkt].position;    
    }

    // Update is called once per frame
    void Update()
    {
        //bewege zum aktuellen zielpunkt
        transform.position = Vector2.MoveTowards(transform.position, zielPunkte[aktuellerPunkt].position, geschwindigkeit * Time.deltaTime);

        //Wenn wir beim Zielpunkt sind, wähle den nächsten in der Liste
        if (Vector2.Distance(transform.position, zielPunkte[aktuellerPunkt].position) <0.02f)
        {
            aktuellerPunkt += 1;

            //Wenn das der letzte war, gehe zurück zum ersten:
            if (aktuellerPunkt >= zielPunkte.Length)
            {
                aktuellerPunkt = 0;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y -0.5 > transform.position.y)
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null);
    }
}
