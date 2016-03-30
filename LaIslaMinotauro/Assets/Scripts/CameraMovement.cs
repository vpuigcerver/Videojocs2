using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

    public static CameraMovement Instance;
    public Transform TargetLookAt;

    public float Distance = 5f;
    public float DistanceMin = 3f;
    public float DistanceMax = 10f;
    public float DistanceSmooth = 0.05f;
    public float DistanceResumeSmooth = 1f;
    public float X_MouseSensivity = 5f;
    public float Y_MouseSensivity = 5f;
    public float MouseWheelSensivity = 5f;
    public float X_Smooth = 0.001f;
    public float Y_Smooth = 0.01f;
    public float Y_MinLimit = -40f;
    public float Y_MaxLimit = 80f;
    public float OcclusionDistanceStep = 0.5f;
    public int MaxOcclusionChecks = 10;


    private float mouseX = 0f;
    private float mouseY = 0f;
    private float velX = 0f;
    private float velY = 0f;
    private float velZ = 0f;
    private float velDistance = 0f;
    private float startDistance = 0f;
    private float desiredDistance = 0f;
    private Vector3 desiredPosition = Vector3.zero;
    private Vector3 position = Vector3.zero;
    private float distanceSmooth = 0f;
    private float preOccludedDistante = 0;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Distance = Mathf.Clamp(Distance, DistanceMin, DistanceMax);
        startDistance = Distance;
        Reset();
    }

    void LateUpdate()
    {
        if (TargetLookAt == null)
            return;

        HandlePlayerInput();

        var count = 0;
        do
        {
            CalculateDesiredPosition();
            count++;
        } while (CheckIfOccluded(count));
        UpdatePosition();
    }
    
    public static void UseExistingOrCreateNewMainCamera()
    {
        GameObject tempCamera;
        GameObject targetLookAt;
        CameraMovement myCamera;

        if(Camera.main != null)
        {
            tempCamera = Camera.main.gameObject;
        }
        else
        {
            tempCamera = new GameObject("Main Camera");
            tempCamera.AddComponent<Camera>();
            tempCamera.tag = "MainCamera";
        }

        tempCamera.AddComponent<CameraMovement>();
        myCamera = tempCamera.GetComponent<CameraMovement>() as CameraMovement;

        targetLookAt = GameObject.Find("targetLookAt") as GameObject;
        if(targetLookAt == null)
        {
            targetLookAt = new GameObject("targetLookAt");
            targetLookAt.transform.position = Vector3.zero;
        }
        myCamera.TargetLookAt = targetLookAt.transform;
    }

    void HandlePlayerInput()
    {
        var deadZone = 0.01f;
        if (Input.GetMouseButton(1))
        {
            // El botón es presionado y obtiene los inputs
            mouseX += Input.GetAxis("Mouse X") * X_MouseSensivity;
            mouseY -= Input.GetAxis("Mouse Y") * Y_MouseSensivity;
        }
        mouseY = Helper.ClampAngle(mouseY, Y_MinLimit, Y_MaxLimit);

        if(Input.GetAxis("Mouse ScrollWheel") < -deadZone || 
            Input.GetAxis("Mouse ScrollWheel") > deadZone)
        {
            desiredDistance = Mathf.Clamp(
                Distance - Input.GetAxis("Mouse ScrollWheel") * X_MouseSensivity,
                DistanceMin, DistanceMax);
            preOccludedDistante = desiredDistance;
            distanceSmooth = DistanceSmooth;
        }
    }

    void CalculateDesiredPosition()
    {
        // Evaluar la distancia
        ResetDesireDistance();
        Distance = Mathf.SmoothDamp(Distance, desiredDistance, ref velDistance, distanceSmooth);

        // Calcular la posicion
        desiredPosition = CalculatePosition(mouseY, mouseX, Distance); //offset

    }

    Vector3 CalculatePosition(float rotationX, float rotationY, float distance)
    {
        Vector3 direction = new Vector3(0, 0, -distance); //detras del personaje
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        return TargetLookAt.position + rotation * direction;
    }

    bool CheckIfOccluded(int count)
    {
        var isOccluded = false;
        var nearestDistance = CheckCameraPoints(TargetLookAt.position, desiredPosition);
        if (nearestDistance != -1)
        {
            if(count< MaxOcclusionChecks)
            {
                isOccluded = true;
                Distance -= OcclusionDistanceStep;

                //Evitar que la distancia sea menos que la distancia evita errores visuales raros y saltos de imagen
                if (Distance < 0.25f) 
                    Distance = 0.25f;
            }
            else
            {
                Distance = nearestDistance - Camera.main.nearClipPlane;
            }
            desiredDistance = Distance;
            distanceSmooth = DistanceResumeSmooth;
        }
        return isOccluded;
    }

    float CheckCameraPoints(Vector3 from, Vector3 to)
    {
        var nearetsDistance = -1f;

        RaycastHit hitInfo;

        Helper.ClipPlanePoints clipPlanePoints = Helper.ClipPlaneAtNear(to);

        if (Physics.Linecast(from, clipPlanePoints.UpperLeft, out hitInfo) && hitInfo.collider.tag != "Player")
            nearetsDistance = hitInfo.distance;

        if (Physics.Linecast(from, clipPlanePoints.LowerLeft, out hitInfo) && hitInfo.collider.tag != "Player")
            if(hitInfo.distance < nearetsDistance || nearetsDistance == -1)
                nearetsDistance = hitInfo.distance;
        if (Physics.Linecast(from, clipPlanePoints.UpperRight, out hitInfo) && hitInfo.collider.tag != "Player")
            if (hitInfo.distance < nearetsDistance || nearetsDistance == -1)
                nearetsDistance = hitInfo.distance;
        if (Physics.Linecast(from, clipPlanePoints.LowerRight, out hitInfo) && hitInfo.collider.tag != "Player")
            if (hitInfo.distance < nearetsDistance || nearetsDistance == -1)
                nearetsDistance = hitInfo.distance;
        if (Physics.Linecast(from, to + transform.forward * -Camera.main.nearClipPlane, out hitInfo) && hitInfo.collider.tag != "Player")
            if (hitInfo.distance < nearetsDistance || nearetsDistance == -1)
                nearetsDistance = hitInfo.distance;


        return nearetsDistance;
    }
    
    void ResetDesireDistance()
    {
        if(desiredDistance < preOccludedDistante)
        {
            var pos = CalculatePosition(mouseY, mouseX, preOccludedDistante);

            var nearestDistance = CheckCameraPoints(TargetLookAt.position, pos);

            //no encuentra colision o es más grande que antes de hacer la oclusion
            if (nearestDistance == -1 || nearestDistance > preOccludedDistante)
            {
                desiredDistance = preOccludedDistante;
            }

        }
    }

    void UpdatePosition()
    {
        var posX = Mathf.SmoothDamp(position.x, desiredPosition.x, ref velX, X_Smooth);
        var posY = Mathf.SmoothDamp(position.y, desiredPosition.y, ref velY, Y_Smooth);
        var posZ = Mathf.SmoothDamp(position.z, desiredPosition.z, ref velZ, X_Smooth);
        position = new Vector3(posX, posY, posZ);

        transform.position = position;

        transform.LookAt(TargetLookAt);
    }

    public void Reset()
    {
        mouseX = 0;
        mouseY = 10;
        Distance = startDistance;
        desiredDistance = Distance;
        preOccludedDistante = Distance;
    }
}