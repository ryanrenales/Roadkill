using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProperties : MonoBehaviour
{

    public enum Weight { Light, Medium, Heavy } // three weights
    public Weight weight; // declare weight variable
    public int score; // score variable
    public float speedMultiplier; // speed multiplier variable

    // Start
    void Start()
    {
        // switch case for light, medium, or heavy weight
        // each weight assigns a score value for that weight, and a speed multiplier to decrease player speed
        switch (weight) 
        {
            case Weight.Light:
                score = 10;
                speedMultiplier = 0.95f;
                break;
            case Weight.Medium:
                score = 30;
                speedMultiplier = 0.8f;
                break;
            case Weight.Heavy:
                score = 100;
                speedMultiplier = 0.45f;
                break;
        }
    }

}
