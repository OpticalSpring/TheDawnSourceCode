
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraSystem : MonoBehaviour
{
    public static PlayerCameraSystem instance;
    private void Awake()
    {
        instance = this;
    }

    public enum CameraState
    {
        GamePlay,
        Cinematic
    }
    public CameraState CamState;
    public GameObject camTarget;
    Vector3 followPos;
    public float followSpeed;
    public float rotationSpeed;
    public float maxDistance;
    public Vector2 correctionPos;
    public Vector2 correctionPosOld;
    public float fov;

    public float camDistance;
    GameObject mainCamera;
    GameObject cameraPivot;
    GameObject rotationPivot;
    public Vector3 rotateValue;
    public GameObject rayHitPoint;

    [System.Serializable]
    public struct CloseStauts
    {
        public Vector2 correctionPos;
        public float maxDistance;
        public int fov;
    }
    public CloseStauts CloseOnSet;
    public CloseStauts CloseOutSet;
    public Vector2 distanceMaxRange;

    // Start is called before the first frame update
    void Start()
    {

        mainCamera = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        cameraPivot = gameObject.transform.GetChild(0).gameObject;
        rotationPivot = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (CamState == CameraState.Cinematic)
        {
            return;
        }
        if (GameManager.instance.timeStopState == true)
        {
            return;
        }
        FollowCam();
        RotateCam();
        CheckDistance();
        SynchronizePivot();
        CheckRayDestination();
    }

    //카메라가 타겟을 따라간다.
    void FollowCam()
    {

        followPos = camTarget.transform.position;
        gameObject.transform.position = followPos;
        //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, followPos, followSpeed * Time.fixedDeltaTime);
    }

    //카메라를 회전시킨다.
    void RotateCam()
    {
        Quaternion targetRotation;

        rotateValue.y += Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
        rotateValue.x -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;
        rotateValue.x = Mathf.Clamp(rotateValue.x, -70f, 70f);
        targetRotation = Quaternion.Euler(rotateValue);

        cameraPivot.transform.rotation = targetRotation;
    }

    //Pivot간의 회전축 동기화
    void SynchronizePivot()
    {
        Vector3 synchronization = Vector3.zero;
        synchronization.y = cameraPivot.transform.eulerAngles.y;
        rotationPivot.transform.eulerAngles = synchronization;
    }

    //카메라가 주변 지형지물에 걸리는지 체크한다.
    void CheckDistance()
    {
        RaycastHit rayHit;

        Debug.DrawRay(cameraPivot.transform.position + new Vector3(0, correctionPos.y, 0), -mainCamera.transform.forward * maxDistance, Color.red);
        int mask = 1 << 2 | 1 << 8 | 1 << 9 | 1 << 10;
        mask = ~mask;
        if (Physics.Raycast(cameraPivot.transform.position + new Vector3(0, correctionPos.y, 0), -mainCamera.transform.forward, out rayHit, maxDistance, mask))
        {
            Vector3 hitPoint = rayHit.point;
            
            camDistance = Vector3.Distance(hitPoint, cameraPivot.transform.position + new Vector3(0, correctionPos.y, 0)) - 0.5f;
            
            if(maxDistance > 6)
            {
                SetMaxDistance(Time.fixedDeltaTime);
            }
        }
        else
        {
            camDistance = maxDistance - 0.5f;
        }

        Vector3 localPos = new Vector3(correctionPos.x, correctionPos.y, -camDistance);
        mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, localPos, Time.fixedDeltaTime * 10f);
        mainCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(mainCamera.GetComponent<Camera>().fieldOfView, fov, Time.fixedDeltaTime * 10);
    }

    void CheckRayDestination()
    {
        RaycastHit rayHit;
        int mask = 1 << 2 | 1 << 8 | 1 << 10;
        mask = ~mask;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rayHit, 5000f, mask))
        {
            rayHitPoint.transform.position = rayHit.point;
        }
    }


    public void Shaking(float _amount, float _duration)
    {
        StartCoroutine(Shake(_amount, _duration));
    }
    public IEnumerator Shake(float _amount, float _duration)
    {
        Vector3 originPos = mainCamera.transform.localPosition;
        float timer = 0;
        while (timer <= _duration)
        {
            mainCamera.transform.localPosition += (Vector3)Random.insideUnitCircle * _amount;

            timer += Time.fixedDeltaTime;
            yield return null;
        }
        mainCamera.transform.localPosition = originPos;

    }


    public void CloseOnCam()
    {
        correctionPos = CloseOnSet.correctionPos;
        maxDistance = CloseOnSet.maxDistance;
        fov = CloseOnSet.fov;
        rotationSpeed = PlayerPrefs.GetInt("GamePlay_2");
    }

    public void CloseOutCam()
    {
        correctionPos = CloseOutSet.correctionPos;
        maxDistance = CloseOutSet.maxDistance;
        fov = CloseOutSet.fov;
        rotationSpeed = PlayerPrefs.GetInt("GamePlay_1");
    }
    public float MaxPosY;
    public float plusY;
    public void SetMaxDistance(float scrollValue)
    {
        if (scrollValue < 0)
        {

            if (CloseOutSet.maxDistance < distanceMaxRange.y)
            {
                CloseOutSet.maxDistance -= scrollValue * 5f;
                CloseOutSet.correctionPos.y -= scrollValue * plusY;
            }
            else
            {
                CloseOutSet.maxDistance = distanceMaxRange.y;
                
            }
        }
        else
        {

            if (CloseOutSet.maxDistance > distanceMaxRange.x)
            {
                CloseOutSet.maxDistance -= scrollValue * 5f;
                CloseOutSet.correctionPos.y -= scrollValue * plusY;
            }
            else
            {
                CloseOutSet.maxDistance = distanceMaxRange.x;
                
            }
        }
        if(CloseOutSet.correctionPos.y < 0)
        {
            CloseOutSet.correctionPos.y = 0;
        }
        else if (CloseOutSet.correctionPos.y > MaxPosY)
        {
            CloseOutSet.correctionPos.y = MaxPosY;
        }
    }


    public void SetBossBattle()
    {
        MaxPosY = 4;
        plusY = 2;
        distanceMaxRange.y = 12;
    }
}
