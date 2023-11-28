using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Transform _playerTransform;
    [SerializeField] private LayerMask obstacleLayerMask; // Layer for walls
    [SerializeField] private float maxDistance = 5f; // Maximum distance for raycasting
    [SerializeField] private float offsetDistance = 0.2f; // Offset distance from the obstacle
    [SerializeField] private float returnSpeed; // Speed of the camera to return the default camera position

    private Vector3 _defaultCameraPosition; // Default position when no obstacles
    
    void Start()
    {
        _defaultCameraPosition = transform.localPosition;
        if (player != null)
        {
            _playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player GameObject undefined for " + nameof(CameraController));
        }
    }
    
    void Update()
    {
        // use raycasting to check if there's a wall in between the player and camera
        RaycastHit hit;
        if (Physics.Raycast(transform.position, _playerTransform.position - transform.position, out hit, maxDistance, obstacleLayerMask))
        {
            // Adjust camera position to avoid clipping
            transform.position = hit.point - (_playerTransform.position - transform.position).normalized * offsetDistance;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, _defaultCameraPosition, Time.deltaTime * returnSpeed);
        }
    }
}
