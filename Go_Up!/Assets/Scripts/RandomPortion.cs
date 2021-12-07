using UnityEngine;
using System.Collections;

public class RandomPortion : MonoBehaviour {

    private float a = 0;
    public static float Randomsu = 25;

    void OnEnable()
    {
        a = Random.Range(10, 14);
        this.transform.position = new Vector3(Random.Range(-1.37f, 1.37f), Randomsu, 0);
        Randomsu += a;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
