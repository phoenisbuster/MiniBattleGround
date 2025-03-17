using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Cinemachine;

public class MapGen : MonoBehaviour
{
    [SerializeField] GameObject startPlat;  //5
    [SerializeField] GameObject norPlat;    //1
    [SerializeField] GameObject finishPlat; //6
    [SerializeField] GameObject Terrain;    //2
    [SerializeField] GameObject RampUp;     //none
    [SerializeField] GameObject RampDown;   //none
    [SerializeField] GameObject SpeedUp;    //4
    [SerializeField] GameObject SlowDown;   //3
    [SerializeField] GameObject CheckPointPlat;   //7
    [SerializeField] GameObject StartGate;
    [SerializeField] GameObject EndGate;
    [SerializeField] GameObject KillZone;
    [SerializeField] GameObject[] EnvironmentList;
    [SerializeField] GameObject player;
    [SerializeField] GameObject bot;
    [SerializeField] GameObject Coundown;
    [SerializeField] CinemachineVirtualCamera Cam;
    private Vector3 CamStartPos;
    private Vector3 CamEndPos;
    public List<Vector3> startCheckPoint;
    public static event Action startTime;
    private bool isBegin = false;
    public Transform parent;

    public int[,] MapMatrix = new int[,]
    {
        {1, 1, 2, 2, 2, 2, 2, 2},
        {1, 1, 2, 2, 2, 2, 2, 2},
        {0, 0, 0, 0, 0, 4, 2, 2},
        {0, 4, 0, 0, 0, 4, 5, 5},
        {0, 0, 4, 0, 0, 4, 2, 2},
        {0, 0, 0, 0, 0, 4, 6, 6},
        {0, 0, 0, 4, 0, 0, 7, 8},
        {0, 4, 0, 0, 0, 0, 8, 7},
        {0, 0, 4, 0, 0, 0, 3, 3},
        {0, 0, 0, 0, 0, 0, 3, 3},
    };

    public GameMap MapFromServer;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        startCheckPoint = new List<Vector3>();
        StartCoroutine(Spawn());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && !isBegin)
        {
            SpawnBot();
        }
    }

    public void UnlockPlayerMovement()
    {
        GameObject.FindGameObjectWithTag("LocalPlayer").transform.GetChild(0).GetComponent<CharacterControls>().canMove = true;
    }
    public IEnumerator Spawn()
    {
        //SpwanObjectWithinMap(MapFromServer.Map);
        //SpawnPlayer();
        yield return new WaitForSeconds(1f);
        CameraTransition();        
        yield return new WaitForSeconds(3.2f);
        CountDown();
    }

    public void SpwanObjectWithinMap(Google.Protobuf.Collections.RepeatedField<GameMap.Types.Grid> GameMap)
    {
        float lengthX = 5f;
        float lengthZ = 5f;
        int len0 = GameMap.Count;//MapMatrix.GetLength(0);
        int len1 = GameMap[0].Object.Count;//MapMatrix.GetLength(1);
        int i,j = 0;
        int noOfStart = 0;
        int noOfEnd = 0;
        // bool increasePosStartgate = false;
        // bool decreasePosStartgate = false;
        // bool increasePosEndGate = false;
        // bool decreasePosEndGate = false;
        GameObject mapType = null;
        Vector3 pos = new Vector3(0,0,0);
        Quaternion rotate = Quaternion.Euler(0, 0, 0);
        Quaternion rotateStartGate = Quaternion.identity;
        Quaternion rotateEndGate = Quaternion.identity;
        Vector3 posStartGate = new Vector3(0, 0.5f, 0);
        Vector3 posEndGate = new Vector3(0, 0.5f, 0);

        for (i = 0; i < len0; i++)
        {
            for (j = 0; j < len1; j++)
            {
                rotate = Quaternion.Euler(0, 90, 0);
                switch ((int)GameMap[i].Object[j])
                {
                    case 5:
                        mapType = startPlat;
                        pos.x = lengthX;
                        pos.z = lengthZ;

                        Instantiate(mapType, pos, Quaternion.Euler(0, 0, 0)).transform.SetParent(parent);
                        startCheckPoint.Add(new Vector3(pos.x/2, pos.y, pos.z/2));
                        if (i + 1 < len0 && (int)GameMap[i+1].Object[j] != 0 && (int)GameMap[i+1].Object[j] != 5)
                        {
                            posStartGate.x += lengthX;
                            posStartGate.z += lengthZ;
                            rotateStartGate.y = -90;
                            noOfStart += 1;
                            //increasePosStartgate = true;
                            //Debug.Log("1");
                        }
                        if (i - 1 > 0 && (int)GameMap[i-1].Object[j] != 0 && (int)GameMap[i-1].Object[j] != 5)
                        {
                            posStartGate.x += lengthX;
                            posStartGate.z += lengthZ;
                            rotateStartGate.y = 90;
                            noOfStart += 1;
                            //decreasePosStartgate = true;
                            //Debug.Log("2");
                        }
                        if (j + 1 < len1 && (int)GameMap[i].Object[j+1] != 0 && (int)GameMap[i].Object[j+1] != 5)
                        {
                            posStartGate.x += lengthX;
                            posStartGate.z += lengthZ;
                            rotateStartGate.y = 180;
                            noOfStart += 1;
                            //Debug.Log("3");
                        }
                        if (j - 1 >= 0 && (int)GameMap[i].Object[j-1] != 0 && (int)GameMap[i].Object[j-1] != 5)
                        {
                            posStartGate.x += lengthX;
                            posStartGate.z += lengthZ;
                            rotateStartGate.y = 0;
                            noOfStart += 1;
                            //Debug.Log("4");
                        }

                        lengthZ += startPlat.transform.localScale.z;
                        break;
                    case 1:
                        mapType = norPlat;
                        pos.x = lengthX;
                        pos.z = lengthZ;

                        Instantiate(mapType, pos, rotate).transform.SetParent(parent);
                        lengthZ += norPlat.transform.localScale.z;
                        break;
                    case 6:
                        mapType = finishPlat;
                        pos.x = lengthX;
                        pos.z = lengthZ;

                        Instantiate(mapType, pos, rotate).transform.SetParent(parent);
                        if (i + 1 < len0 && (int)GameMap[i+1].Object[j] != 0 && (int)GameMap[i+1].Object[j]!= 6)
                        {
                            posEndGate.x += lengthX;
                            posEndGate.z += lengthZ;
                            rotateEndGate.y = 90;
                            noOfEnd += 1;
                            //increasePosEndGate = true;
                            //Debug.Log("5");
                        }
                        if (i - 1 > 0 && (int)GameMap[i-1].Object[j] != 0 && (int)GameMap[i-1].Object[j] != 6)
                        {
                            posEndGate.x += lengthX;
                            posEndGate.z += lengthZ;
                            rotateEndGate.y = -90;
                            noOfEnd += 1;
                            //decreasePosEndGate = true;
                            //Debug.Log("6");
                        }
                        if (j + 1 < len1 && (int)GameMap[i].Object[j+1] != 0 && (int)GameMap[i].Object[j+1] != 6)
                        {
                            posEndGate.x += lengthX;
                            posEndGate.z += lengthZ;
                            rotateEndGate.y = 0;
                            noOfEnd += 1;
                            //Debug.Log("7");
                        }
                        if (j - 1 >= 0 && (int)GameMap[i].Object[j-1] != 0 && (int)GameMap[i].Object[j-1] != 6)
                        {
                            posEndGate.x += lengthX;
                            posEndGate.z += lengthZ;
                            rotateEndGate.y = 180;
                            noOfEnd += 1;
                            //Debug.Log("8");
                        }
                        

                        lengthZ += finishPlat.transform.localScale.z;
                        break;
                    case 2:
                        //int index = UnityEngine.Random.Range(0, EnvironmentList.Length);
                        mapType = Terrain;
                        pos.x = lengthX;
                        pos.z = lengthZ;

                        Instantiate(mapType, pos, rotate).transform.SetParent(parent);

                        lengthZ += Terrain.transform.localScale.z;
                        break;
                    case 8:
                        mapType = RampUp;
                        if (i + 1 < len0 && (int)GameMap[i+1].Object[j] == 1)
                        {
                            rotate.y = 90;
                        }
                        if (i - 1 > 0 && (int)GameMap[i-1].Object[j] == 1)
                        {
                            rotate.y = 90;
                        }
                        if (j + 1 < len1 && (int)GameMap[i].Object[j+1] == 1)
                        {
                            rotate.y = 0;
                        }
                        if (j - 1 > 0 && (int)GameMap[i].Object[j-1] == 1)
                        {
                            rotate.y = 0;
                        }
                        pos.x = lengthX;
                        pos.z = lengthZ;
                        Instantiate(mapType, pos, Quaternion.Euler(0, rotate.y, 0)).transform.SetParent(parent);

                        lengthZ += SpeedUp.transform.localScale.z;
                        break;
                    case 9:
                        mapType = RampDown;
                        if (i + 1 < len0 && (int)GameMap[i+1].Object[j] == 1)
                        {
                            rotate.y = 90;
                        }
                        if (i - 1 > 0 && (int)GameMap[i-1].Object[j] == 1)
                        {
                            rotate.y = 90;
                        }
                        if (j + 1 < len1 && (int)GameMap[i].Object[j+1] == 1)
                        {
                            rotate.y = 0;
                        }
                        if (j - 1 > 0 && (int)GameMap[i].Object[j-1] == 1)
                        {
                            rotate.y = 0;
                        }
                        pos.x = lengthX;
                        pos.z = lengthZ;

                        Instantiate(mapType, pos, Quaternion.Euler(0, rotate.y, 0)).transform.SetParent(parent);

                        lengthZ += SpeedUp.transform.localScale.z;
                        break;
                    case 4:
                        mapType = SpeedUp;
                        pos.x = lengthX;
                        pos.z = lengthZ;

                        Instantiate(mapType, pos, rotate).transform.SetParent(parent);

                        lengthZ += SpeedUp.transform.localScale.z;
                        break;
                    case 3:
                        mapType = SlowDown;
                        pos.x = lengthX;
                        pos.z = lengthZ;

                        Instantiate(mapType, pos, rotate).transform.SetParent(parent);

                        lengthZ += SlowDown.transform.localScale.z;
                        break;
                    case 7:
                        mapType = CheckPointPlat;
                        pos.x = lengthX;
                        pos.z = lengthZ;
                        
                        Instantiate(mapType, new Vector3(pos.x, 1, pos.z), rotate).transform.SetParent(parent);
                        lengthZ += norPlat.transform.localScale.z;
                        break;
                    default:
                        // int index = UnityEngine.Random.Range(0, EnvironmentList.Length);
                        // Instantiate(EnvironmentList[index], pos, rotate).transform.SetParent(parent);
                        lengthZ += 10;
                        break;
                }
            }
            lengthZ = 5f;
            lengthX += startPlat.transform.localScale.x;
        }
        float scale = noOfStart > 0? startPlat.transform.localScale.x * 0.08f * noOfStart : 1;
        StartGate.transform.localScale = new Vector3(scale, scale, 1);
        // if(increasePosStartgate)
        //     posStartGate.x =  (posStartGate.x + startPlat.transform.localScale.x)/2;
        // else if(decreasePosStartgate)
        //     posStartGate.x =  (posStartGate.x - startPlat.transform.localScale.x)/2;
        // else
        posStartGate.x /= 2;
        posStartGate.z /= 2;        
        Instantiate(StartGate, posStartGate, Quaternion.Euler(0, rotateStartGate.y, 0)).transform.SetParent(parent);
        CamStartPos = new Vector3(posStartGate.x - 10, 50, posStartGate.z-10);


        scale = noOfEnd > 0? finishPlat.transform.localScale.x * 0.08f * noOfEnd : 1;
        EndGate.transform.localScale = new Vector3(scale, scale, 1);
        // if(increasePosEndGate)
        //     posEndGate.x =  (posEndGate.x + finishPlat.transform.localScale.x)/2;
        // else if(decreasePosEndGate)
        //     posEndGate.x =  (posEndGate.x - finishPlat.transform.localScale.x)/2;
        // else
        posEndGate.x /= 2;
        posEndGate.z /= 2;
        Instantiate(EndGate, posEndGate, Quaternion.Euler(0, rotateEndGate.y, 0)).transform.SetParent(parent);  
        CamEndPos = new Vector3(posEndGate.x - 10, 50, posEndGate.z-10);
        Instantiate(KillZone, Vector3.down * 25, Quaternion.identity).transform.SetParent(parent);   
    }

    public void CameraTransition()
    {
        Cam.gameObject.SetActive(true);
        Cam.transform.position = CamStartPos;
        Cam.transform.DOMove(CamEndPos, 3f).SetEase(Ease.InOutSine).SetLink(Cam.gameObject).OnComplete(()=>{
            Cam.gameObject.SetActive(false);
            //Camera.main.transform.localRotation = Quaternion.identity;
        });
    }

    public GameObject SpawnPlayer(int ID, GameObject playerPrefab, float x = 0, float y = 0, float facing = 0, string name = "This player do not even have A NAME")
    {
        Vector3 pos;
        int index = UnityEngine.Random.Range(0, startCheckPoint.Count);
        // pos.x = startCheckPoint.Count > 0? startCheckPoint[index].x : 0;
        // pos.z = startCheckPoint.Count > 0? startCheckPoint[index].z : 0;
        pos.x = y;
        pos.z = x;
        pos.y = player.transform.localScale.y + startPlat.transform.localScale.y/2;
        playerPrefab.transform.GetChild(0).rotation = Quaternion.Euler(0, facing, 0);
        var playerRenturn = Instantiate(playerPrefab, pos, Quaternion.identity);
        //GameObject.FindGameObjectWithTag("LocalPlayer").transform.GetChild(0).GetComponent<CharacterControls>().canMove = false;
        playerRenturn.transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = "" + name;
        return playerRenturn;
    }

    public void CountDown()
    {
        Coundown.gameObject.SetActive(true);
        Camera.main.transform.localRotation = Quaternion.identity;
        DOVirtual.Int(1, 1, 0, v =>
        {
            Coundown.GetComponentInChildren<TMP_Text>().text = v==0? "Start" : "" + v;
        }).OnComplete(()=>
        {            
            Debug.Log("MATCH START");
            Coundown.gameObject.SetActive(false);
            GameObject temp = GameObject.FindGameObjectWithTag("StartGate");
            temp.transform.GetChild(0).gameObject.SetActive(false);
            startTime?.Invoke();
            isBegin = true;
            Debug.Log("MATCH START: " + isBegin);
        }).SetLink(Coundown).OnStart(()=>{
            GameObject.FindGameObjectWithTag("LocalPlayer").transform.GetChild(0).GetComponent<CharacterControls>().canMove = true;
        });
    }

    public void SpawnBot()
    {
        Vector3 pos;
        int index = UnityEngine.Random.Range(0, startCheckPoint.Count);
        pos.x = startCheckPoint.Count > 0? startCheckPoint[index].x : 15;
        pos.z = startCheckPoint.Count > 0? startCheckPoint[index].z : 15;
        Debug.Log("Bot Create");
        pos.y = bot.transform.localScale.y + startPlat.transform.localScale.y/2;
        Instantiate(bot, pos, Quaternion.identity);
    }
}
