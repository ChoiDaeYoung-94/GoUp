using UnityEngine;
using System.Collections;

public class RamdomBomb1 : MonoBehaviour {

    private float a = 0;
    public static float Randomsu = 30;

    void OnEnable()
    {
        a = Random.Range(5, 7);
        this.transform.position = new Vector3(Random.Range(-1.37f, 1.37f), Randomsu, 0);
        Randomsu += a;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }
    }
}
