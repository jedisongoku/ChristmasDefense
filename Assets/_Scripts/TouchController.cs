using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour {

    public static TouchController touchController;
    public static bool isMouseOnUI = false;

    public float swipeSpeed = 0.1f;
    public float smoothing = 1;
    public float swipeDelay = 0;
    public float zoomSpeed = 0.5f;
    public Transform CameraMoveLimitTop;
    public Transform CameraMoveLimitBottom;

    private float y_pos;

    private bool moveRight = true;
    private bool moveLeft = true;
    private bool moveTop = true;
    private bool moveBottom = true;
    private RaycastHit hit;



    void Start()
    {
        touchController = this;
        StartCoroutine(CameraSwipe());
    }


    void FixedUpdate()
    {
        if(!MouseController.isMouseOnUI)
        {

            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                if (Physics.Raycast(ray, out hit, 1000))
                {
                    //Debug.Log(hit.transform.name);
                    if (hit.transform.CompareTag("SpawnLocation"))
                    {
                        hit.transform.GetComponent<HeroSpawnManager>().OnTouched();
                        
                    }
                    else
                    {
                        //Invoke("HideHeroPanels", 0.1f);
                        MouseController.isMouseOnUI = false;
                        HideHeroPanels();
                        //GameHUDManager.gameHudManager.TutorialPhaseComplete(2);
                    }
                }
            }
            

        }
        else
        {
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                MouseController.isMouseOnUI = false;
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
        if(CameraMoveLimitTop != null)
        {
            if (transform.position.x > CameraMoveLimitTop.position.x)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(CameraMoveLimitTop.position.x - 0.1f, y_pos, transform.position.z), Time.deltaTime * smoothing);
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }

            if (transform.position.z > CameraMoveLimitTop.position.z)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y_pos, CameraMoveLimitTop.position.z - 0.1f), Time.deltaTime * smoothing);
                moveTop = false;
            }
            else
            {
                moveTop = true;
            }
            if (transform.position.x < CameraMoveLimitBottom.position.x)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(CameraMoveLimitBottom.position.x + 0.1f, y_pos, transform.position.z), Time.deltaTime * smoothing);
                moveLeft = false;
            }
            else
            {
                moveLeft = true;
            }

            if (transform.position.z < CameraMoveLimitBottom.position.z)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y_pos, CameraMoveLimitBottom.position.z + 0.1f), Time.deltaTime * smoothing);
                moveBottom = false;
            }
            else
            {
                moveBottom = true;
            }
        }

    }

    IEnumerator CameraSwipe()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && !MouseController.isMouseOnUI)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            if (transform.position.x < CameraMoveLimitTop.position.x && moveRight && touchDeltaPosition.x < 0)
            {
                y_pos = transform.position.y;
                transform.Translate(-touchDeltaPosition.x * swipeSpeed, 0, 0);
                transform.position = new Vector3(transform.position.x, y_pos, transform.position.z);
                //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y_pos, transform.position.z), Time.deltaTime * smoothing);
            }
            if (transform.position.z < CameraMoveLimitTop.position.z && moveTop && touchDeltaPosition.y < 0)
            {
                y_pos = transform.position.y;
                transform.Translate(0, -touchDeltaPosition.y * swipeSpeed, 0);
                transform.position = new Vector3(transform.position.x, y_pos, transform.position.z);
                //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y_pos, transform.position.z), Time.deltaTime * smoothing);
            }
            if (transform.position.x > CameraMoveLimitBottom.position.x && moveLeft && touchDeltaPosition.x > 0)
            {
                y_pos = transform.position.y;
                transform.Translate(-touchDeltaPosition.x * swipeSpeed, 0, 0);
                transform.position = new Vector3(transform.position.x, y_pos, transform.position.z);
                //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y_pos, transform.position.z), Time.deltaTime * smoothing);
            }
            if (transform.position.z > CameraMoveLimitBottom.position.z && moveBottom && touchDeltaPosition.y > 0)
            {
                y_pos = transform.position.y;
                transform.Translate(0, -touchDeltaPosition.y * swipeSpeed, 0);
                transform.position = new Vector3(transform.position.x, y_pos, transform.position.z);
                
            }


        }

        yield return new WaitForSeconds(0);

        StartCoroutine(CameraSwipe());


    }

    void HideHeroPanels()
    {
        GameHUDManager.gameHudManager.HideHeroes();
        GameHUDManager.gameHudManager.HideHeroInfo();
    }
}
