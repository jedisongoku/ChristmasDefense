using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

    public static bool isMouseOnUI = false;

    public float speed = 0.1f;
    public float smoothing;
    public float zoomSpeed = 0.5f;
    public Transform[] CameraMoveLimitTop;
    public Transform[] CameraMoveLimitBottom;

    private float y_pos;

    private bool moveRight = true;
    private bool moveLeft = true;
    private bool moveTop = true;
    private bool moveBottom = true;
    private RaycastHit hit;



    void Start()
    {
        StartCoroutine(CameraSwipe());
    }


    void FixedUpdate()
    {

        if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag("SpawnLocation"))
                {
                    hit.transform.GetComponent<HeroSpawnManager>().OnTouched();
                }
                else
                {
                    //Invoke("HideHeroPanels", 0.1f);
                    MouseController.isMouseOnUI = false;
                    HideHeroPanels();
                    GameHUDManager.gameHudManager.TutorialPhaseComplete(2);
                }
            }
        }


        //PinchZoom
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnituteDiff = prevTouchDeltaMag - touchDeltaMag;

            Camera.main.fieldOfView += deltaMagnituteDiff * zoomSpeed;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 30, 90);
        }

        //Swipe Camera Move
        if (transform.position.x > CameraMoveLimitTop[GameManager.gameManager.level - 1].position.x)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(CameraMoveLimitTop[GameManager.gameManager.level - 1].position.x - 0.1f, y_pos, transform.position.z), Time.deltaTime * smoothing);
            moveRight = false;
        }
        else
        {
            moveRight = true;
        }

        if (transform.position.z > CameraMoveLimitTop[GameManager.gameManager.level - 1].position.z)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x , y_pos, CameraMoveLimitTop[GameManager.gameManager.level - 1].position.z - 0.1f), Time.deltaTime * smoothing);
            moveTop = false;
        }
        else
        {
            moveTop = true;
        }
        if (transform.position.x < CameraMoveLimitBottom[GameManager.gameManager.level - 1].position.x)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(CameraMoveLimitBottom[GameManager.gameManager.level - 1].position.x + 0.1f, y_pos, transform.position.z), Time.deltaTime * smoothing);
            moveLeft = false;
        }
        else
        {
            moveLeft = true;
        }

        if (transform.position.z < CameraMoveLimitBottom[GameManager.gameManager.level - 1].position.z)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y_pos, CameraMoveLimitBottom[GameManager.gameManager.level - 1].position.z + 0.1f), Time.deltaTime * smoothing);
            moveBottom = false;
        }
        else
        {
            moveBottom = true;
        }



    }

    IEnumerator CameraSwipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            if (transform.position.x < CameraMoveLimitTop[GameManager.gameManager.level - 1].position.x && moveRight && touchDeltaPosition.x < 0)
            {
                y_pos = transform.position.y;
                transform.Translate(-touchDeltaPosition.x * speed, 0, 0);
                transform.position = new Vector3(transform.position.x, y_pos, transform.position.z);
            }
            if (transform.position.z < CameraMoveLimitTop[GameManager.gameManager.level - 1].position.z && moveTop && touchDeltaPosition.y < 0)
            {
                y_pos = transform.position.y;
                transform.Translate(0, -touchDeltaPosition.y * speed, 0);
                transform.position = new Vector3(transform.position.x, y_pos, transform.position.z);
            }
            if (transform.position.x > CameraMoveLimitBottom[GameManager.gameManager.level - 1].position.x && moveLeft && touchDeltaPosition.x > 0)
            {
                y_pos = transform.position.y;
                transform.Translate(-touchDeltaPosition.x * speed, 0, 0);
                transform.position = new Vector3(transform.position.x, y_pos, transform.position.z);
            }
            if (transform.position.z > CameraMoveLimitBottom[GameManager.gameManager.level - 1].position.z && moveBottom && touchDeltaPosition.y > 0)
            {
                y_pos = transform.position.y;
                transform.Translate(0, -touchDeltaPosition.y * speed, 0);
                transform.position = new Vector3(transform.position.x, y_pos, transform.position.z);
            }


        }

        yield return new WaitForSeconds(0.08f);

        StartCoroutine(CameraSwipe());


    }

    void HideHeroPanels()
    {
        GameHUDManager.gameHudManager.HideHeroes();
        GameHUDManager.gameHudManager.HideHeroInfo();
    }
}
