using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform endPoint;
    public Slider progressSlider;

    private float startX;
    private float totalDistance;

    void Start()
    {
        startX = player.position.x;

        totalDistance = Vector3.Distance(player.position, endPoint.position);
    }

    void Update()
    {
        float currentDistance = player.position.x - startX;

        float progress = currentDistance / totalDistance;

        progressSlider.value = progress;
    }
}