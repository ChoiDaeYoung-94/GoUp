using UnityEngine;
using System.Collections;

public class RandomGround : MonoBehaviour {

    private float a = 0;
    public static float Randomsu = 3;

    void OnEnable()
    {
        a = Random.Range(0.7f, 1.3f);
        this.transform.position = new Vector3(Random.Range(-1.37f, 1.37f), Randomsu, 0);
        Randomsu += a;
    }
}
