#pragma warning disable 0618

using UnityEngine;
using System.Collections;

public class ReStart : MonoBehaviour {

    public string Name;

    void OnMouseDown()
    {
        RandomGround.Randomsu = 3;
        RandomGround_50.Randomsu = 20;
        RandomGround_100.Randomsu = 55;
        CameraController.a = 0.25f;
        RandomPortion.Randomsu = 25;
        RandomBomb.Randomsu = 10;
        RamdomBomb1.Randomsu = 30;
        Application.LoadLevel(Name);
    }
}
