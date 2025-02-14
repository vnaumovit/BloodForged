using UnityEngine;

public class WalkingDto : MonoBehaviour {
    public float walkingDistanceMax = 4f;
    public float walkingDistanceMin = 2f;
    public float walkingTimeMax = 5f;
    public float walkingTime {get; set;}
    public float nextCheckDirectionTime {get; set;}
}