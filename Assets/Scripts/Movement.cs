using UnityEngine;

public class Movement : MonoBehaviour
{
    float hMove, vMove;

    float hMoveMouse, vMoveMouse;

    public float moveSpeed = 2f;

    public float turnedSpeedCameraH = 360f;  //скорось вращения камеры по оси Х

    public float turnedSpeedCameraV = 240f;  //скорось вращения камеры по оси У

    float limitCamraX = 35f;  //переменная для ограничения поворота камеры по оси Х(верх-вниз)

    float currentRotationCamX = 0;  //переменная в которой хранится текущее врщение камеры по оси Х

    CharacterController objectCam;

    float rayDistance = 2.5f;  //дистанция на которую будет пускаться луч из камеры

    public GameObject[] MiscsLable;
    public GameObject[] Miscs;

    public Material[] selectMisc;
    public Material[] mainMaterialMisc;

    public GameObject showInfoText;
    public GameObject hideInfoText;

    private void Start()
    {
        objectCam = GetComponent<CharacterController>();  //нахожу компонент на объекте
    }

    private void Update()
    {
        //MouseLock();

        MovmentCamera();

        RotationCamera();

        CheckMisc();
    }

    void MovmentCamera()
    {
        hMove = Input.GetAxisRaw("Horizontal");
        vMove = Input.GetAxisRaw("Vertical");

        Vector3 move = (transform.right * hMove + transform.forward * vMove).normalized;

        objectCam.Move(move * moveSpeed * Time.deltaTime);
    }

    void RotationCamera()
    {
        //тут задаю переменным значение движения мышки по осям и домножаю на скорость передвижения:
        hMoveMouse = Input.GetAxis("Mouse X") * turnedSpeedCameraH * Time.deltaTime;
        vMoveMouse = Input.GetAxis("Mouse Y") * turnedSpeedCameraV * Time.deltaTime;

        if (hMoveMouse != 0)
        {
            transform.RotateAround(transform.position, Vector3.up, hMoveMouse);  //вращение камеры вокруг самой себя по оси Х
        }

        if (vMoveMouse != 0)
        {
            currentRotationCamX -= vMoveMouse;  //задаю текущее движение по оси У и инвертирую его
            currentRotationCamX = Mathf.Clamp(currentRotationCamX, -limitCamraX, limitCamraX);  //ограничиваю поворот камеры по оси Х
            float rotationY = transform.localEulerAngles.y;  //локальная переменная изменения вращения камеры по оси У
            transform.localEulerAngles = new Vector3(currentRotationCamX, rotationY, 0);  //возвращаю новые значения поворота камеры по осям
        }
    }

    //метод который будет блокировать курсор в центре экрана:
    private void MouseLock()
    {
        //если нажат Escape, курсор становится видимым, и его можно свободно перемещать по экрану:
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        //eсли же нажата левая кнопка мыши - курсор закрепляется в центре экрана, и становится невидимым:
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void CheckMisc()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            #region активирую UI надписи показа инфы об объекте:
            if (hit.transform.tag == "MiscCube" && MiscsLable[0].activeInHierarchy == false)
            {
                hideInfoText.SetActive(false);
                showInfoText.SetActive(true);
            }
            else if (hit.transform.tag == "MiscCube" && MiscsLable[0].activeInHierarchy == true)
            {
                showInfoText.SetActive(false);
                hideInfoText.SetActive(true);
            }
            else if (hit.transform.tag == "MiscCapsule" && MiscsLable[1].activeInHierarchy == false)
            {
                hideInfoText.SetActive(false);
                showInfoText.SetActive(true);
            }
            else if (hit.transform.tag == "MiscCapsule" && MiscsLable[1].activeInHierarchy == true)
            {
                showInfoText.SetActive(false);
                hideInfoText.SetActive(true);
            }
            else if (hit.transform.tag == "MiscCylinder" && MiscsLable[2].activeInHierarchy == false)
            {
                hideInfoText.SetActive(false);
                showInfoText.SetActive(true);
            }
            else if (hit.transform.tag == "MiscCylinder" && MiscsLable[2].activeInHierarchy == true)
            {
                showInfoText.SetActive(false);
                hideInfoText.SetActive(true);
            }
            if (hit.transform.tag == "MiscCubeT" && MiscsLable[3].activeInHierarchy == false)
            {
                hideInfoText.SetActive(false);
                showInfoText.SetActive(true);
            }
            else if (hit.transform.tag == "MiscCubeT" && MiscsLable[3].activeInHierarchy == true)
            {
                showInfoText.SetActive(false);
                hideInfoText.SetActive(true);
            }
            else if (hit.transform.tag == "MiscCapsuleT" && MiscsLable[4].activeInHierarchy == false)
            {
                hideInfoText.SetActive(false);
                showInfoText.SetActive(true);
            }
            else if (hit.transform.tag == "MiscCapsuleT" && MiscsLable[4].activeInHierarchy == true)
            {
                showInfoText.SetActive(false);
                hideInfoText.SetActive(true);
            }
            else if (hit.transform.tag == "MiscCylinderT" && MiscsLable[5].activeInHierarchy == false)
            {
                hideInfoText.SetActive(false);
                showInfoText.SetActive(true);
            }
            else if (hit.transform.tag == "MiscCylinderT" && MiscsLable[5].activeInHierarchy == true)
            {
                showInfoText.SetActive(false);
                hideInfoText.SetActive(true);
            }
            #endregion

            #region при подходе к предметам реализую их активацию на нажатие кнопки Е:
            if (hit.transform.tag == "MiscCube" && Input.GetKeyDown(KeyCode.E) && MiscsLable[0].activeInHierarchy == false)
            {
                MiscsLable[0].SetActive(true);
                Miscs[0].GetComponent<Renderer>().material = selectMisc[0];  //меняю материал при выборе предмета
            }             //чтобы деактивировать предметы необходимо опять подойти к нему и нажать кнопку Е:
            else if (hit.transform.tag == "MiscCube" && Input.GetKeyDown(KeyCode.E) && MiscsLable[0].activeInHierarchy == true)
            {
                MiscsLable[0].SetActive(false);
                Miscs[0].GetComponent<Renderer>().material = mainMaterialMisc[0];
            }

            if (hit.transform.tag == "MiscCapsule" && Input.GetKeyDown(KeyCode.E) && MiscsLable[1].activeInHierarchy == false)
            {
                MiscsLable[1].SetActive(true);
                Miscs[1].GetComponent<Renderer>().material = selectMisc[1];
            }
            else if (hit.transform.tag == "MiscCapsule" && Input.GetKeyDown(KeyCode.E) && MiscsLable[1].activeInHierarchy == true)
            {
                MiscsLable[1].SetActive(false);
                Miscs[1].GetComponent<Renderer>().material = mainMaterialMisc[1];
            }

            if (hit.transform.tag == "MiscCylinder" && Input.GetKeyDown(KeyCode.E) && MiscsLable[2].activeInHierarchy == false)
            {
                MiscsLable[2].SetActive(true);
                Miscs[2].GetComponent<Renderer>().material = selectMisc[2];
            }
            else if (hit.transform.tag == "MiscCylinder" && Input.GetKeyDown(KeyCode.E) && MiscsLable[2].activeInHierarchy == true)
            {
                MiscsLable[2].SetActive(false);
                Miscs[2].GetComponent<Renderer>().material = mainMaterialMisc[2];
            }
            #endregion

            #region при подходе к маленьким предметам реализую их активацию на нажатие кнопки Е:
            if (hit.transform.tag == "MiscCubeT" && Input.GetKeyDown(KeyCode.E) && MiscsLable[3].activeInHierarchy == false)
            {
                hit.transform.gameObject.GetComponent<Animator>().SetTrigger("ShowCube");
                MiscsLable[3].SetActive(true);
                Miscs[3].GetComponent<Renderer>().material = selectMisc[0];  //меняю материал при выборе предмета
            }             //чтобы деактивировать предметы необходимо опять подойти к нему и нажать кнопку Е:
            else if (hit.transform.tag == "MiscCubeT" && Input.GetKeyDown(KeyCode.E) && MiscsLable[3].activeInHierarchy == true)
            {
                hit.transform.gameObject.GetComponent<Animator>().SetTrigger("StopRotateCube");
                MiscsLable[3].SetActive(false);
                Miscs[3].GetComponent<Renderer>().material = mainMaterialMisc[0];
            }

            if (hit.transform.tag == "MiscCapsuleT" && Input.GetKeyDown(KeyCode.E) && MiscsLable[4].activeInHierarchy == false)
            {
                hit.transform.gameObject.GetComponent<Animator>().SetTrigger("ShowCapsule");
                MiscsLable[4].SetActive(true);
                Miscs[4].GetComponent<Renderer>().material = selectMisc[1];
            }
            else if (hit.transform.tag == "MiscCapsuleT" && Input.GetKeyDown(KeyCode.E) && MiscsLable[4].activeInHierarchy == true)
            {
                hit.transform.gameObject.GetComponent<Animator>().SetTrigger("StopRotateCapsule");
                MiscsLable[4].SetActive(false);
                Miscs[4].GetComponent<Renderer>().material = mainMaterialMisc[1];
            }

            if (hit.transform.tag == "MiscCylinderT" && Input.GetKeyDown(KeyCode.E) && MiscsLable[5].activeInHierarchy == false)
            {
                hit.transform.gameObject.GetComponent<Animator>().SetTrigger("ShowCylinder");
                MiscsLable[5].SetActive(true);
                Miscs[5].GetComponent<Renderer>().material = selectMisc[2];
            }
            else if (hit.transform.tag == "MiscCylinderT" && Input.GetKeyDown(KeyCode.E) && MiscsLable[5].activeInHierarchy == true)
            {
                hit.transform.gameObject.GetComponent<Animator>().SetTrigger("StopRotateCylinder");
                MiscsLable[5].SetActive(false);
                Miscs[5].GetComponent<Renderer>().material = mainMaterialMisc[2];
            }
            #endregion
        }
        else  //если луч не с чем не пересёкся, то дезактивирую надписи:
        {
            hideInfoText.SetActive(false);
            showInfoText.SetActive(false);
        }
    }
}