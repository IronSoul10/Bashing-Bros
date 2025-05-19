using UnityEngine;

public class CameraLook : MonoBehaviour
{

    [Header("Objetivo a seguir")]
    [SerializeField] public Transform target;

    [Header("Ajustes de Seguimiento")]
    [SerializeField] public Vector3 offset = new Vector3(4f, 1f, -10f); // Offset de la cámara
    [SerializeField] private float smoothSpeed = 5f; // Suavizado del movimiento

    [Header("Rotación")]
    [SerializeField] private bool enableRotation = false; // Habilitar/deshabilitar rotación
    [SerializeField] private float rotationAngle = 0f; // Ángulo de rotación en grados
    [SerializeField] private float rotationSmoothSpeed = 2f; // Suavizado de rotación


    [HideInInspector] public Vector3 currentOffset;
    private float currentRotation;

    private void Start()
    {
        currentOffset = offset;
        currentRotation = rotationAngle;
    }

    private void LateUpdate()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        Vector3 desiredPosition = target.position + currentOffset;
    
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Manejar la rotación
        if (enableRotation)
        {
            Quaternion desiredRotation = Quaternion.Euler(0f, 0f, currentRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSmoothSpeed * Time.deltaTime);
        }
    }
}


