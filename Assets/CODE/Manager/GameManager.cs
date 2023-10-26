using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private bool PlayerMode;
    [SerializeField] private bool CamMode;
    [SerializeField] private bool CamAroundMode;
    private Vector3 CemaraPosVec;
    private Vector3 CemaraRotVec;
    bool once, once1;
    Player PL;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        CemaraPosVec = new Vector3(0, 4.45f, -8);
        CemaraRotVec = new Vector3(18.3f, 0, 0);
    }

    void Start()
    {
        PL = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        ModeChanger();
        CameraSetParent();
    }

    private void CameraSetParent()
    {
        if(PlayerMode && !once)
        {
            once = true;
            once1 = false;
            Camera.main.transform.SetParent(GameObject.Find("Player").transform);
            Camera.main.transform.position = PL.transform.position;
            Camera.main.transform.rotation = PL.transform.rotation;
            Camera.main.transform.localPosition = new Vector3(0, 3.9f, -7);
            Camera.main.transform.eulerAngles = new Vector3(20,0,0); 
        }
        else if(CamMode && !once1)
        {
            once1 = true;
            once = false;
            Camera.main.transform.SetParent(GameObject.Find("Manager/CameraManager").transform);
        }
    }

    private void ModeChanger()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            F_CamModeChanger(0);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            F_CamModeChanger(1);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            F_CamModeChanger(2);
        }

    }
    public void F_CamModeChanger(int value)
    {
        switch (value)
        {
            case 0:
                PlayerMode = true;
                CamMode = false;
                CamAroundMode = false;
                break;

            case 1:
                PlayerMode = false;
                CamMode = true;
                CamAroundMode = false;
                break;

            case 2:
                PlayerMode = false;
                CamMode = false;
                CamAroundMode = true;
                break;
        }

    }

    public bool F_CamModeChaker(int value)
    {
        switch (value)
        {
            case 0:
                return PlayerMode;

            case 1:
                return CamMode;

            case 2:
                return CamAroundMode;
                

           default: return false;
        }

    }
}
