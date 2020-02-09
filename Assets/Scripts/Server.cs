using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Server : MonoBehaviour
{ 

    public GameObject PlayerPrefab;
    public string ip;//ip для созадания или подключения к серверу
    public string port;
    public bool connected; // статус подключения
    private GameObject go; //объект для ссылки на игрока
    public bool visible = false; // статус показа меню

    public Button b_Сonnect;
    public Button b_Server;
    public Button b_Quit;
    public Button b_Disconnect;

    public InputField f_IP;
    public InputField f_Port;

    public Text Connetions;

    public GameObject background;

    private void Start()
    {
        background.SetActive(true);
        Connetions.gameObject.SetActive(false);
        b_Disconnect.gameObject.SetActive(false);
        b_Quit.gameObject.SetActive(false);
        b_Сonnect.gameObject.SetActive(false);
        b_Server.gameObject.SetActive(false);
        f_IP.gameObject.SetActive(false);
        f_Port.gameObject.SetActive(false);

        f_IP.onValueChange.AddListener(new UnityEngine.Events.UnityAction<string>(IP));
        f_Port.onValueChange.AddListener(new UnityEngine.Events.UnityAction<string>(Port));

    }

    // На каждый кадр
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            visible = !visible;

        // Если мы на сервере
        if (connected)
        {
            if (visible)
            {
                Connetions.gameObject.SetActive(true);
                Connetions.text = "Присоединились: " + Network.connections.Length;
                b_Disconnect.gameObject.SetActive(true);
                b_Quit.gameObject.SetActive(true);
                b_Сonnect.gameObject.SetActive(false);
                b_Server.gameObject.SetActive(false);
                f_IP.gameObject.SetActive(false);
                f_Port.gameObject.SetActive(false);
            }
            else
            {
                Connetions.gameObject.SetActive(false);
                b_Disconnect.gameObject.SetActive(false);
                b_Quit.gameObject.SetActive(false);
                b_Сonnect.gameObject.SetActive(false);
                b_Server.gameObject.SetActive(false);
                f_IP.gameObject.SetActive(false);
                f_Port.gameObject.SetActive(false);
            }
            //Если мы в главном меню
        }
        else
        {
            Connetions.gameObject.SetActive(false);
            b_Disconnect.gameObject.SetActive(false);
            b_Quit.gameObject.SetActive(true);
            b_Сonnect.gameObject.SetActive(true);
            b_Server.gameObject.SetActive(true);
            f_IP.gameObject.SetActive(true);
            f_Port.gameObject.SetActive(true);
        }
    }




    //изменение IP
    public void IP(string ip1)
    {
        ip = ip1;
        Debug.Log(ip);
    }
    //изменение Port
    public void Port(string port1)
    {
        port = port1;
    }
    public void DisConnect()
    {
        Network.Disconnect(200);
    }

    //конпка присоединиться
    public void Connect()
    {
        Network.Connect(ip, int.Parse(port));
    }
    //кнопка Создать сервер
    public void InitializeServer()
    {
        Network.InitializeServer(2, int.Parse(port), false);
    }
    public void Quit()
    {
        Application.Quit();
    }
    // Вызывается когда мы подключились к серверу
    void OnConnectedToServer()
    {
        GameObject.Find("Scene Manager").GetComponent<PUT_controller>().set_ID(1);
        CreatePlayer();
    }

    // Когда мы создали сервер
    void OnServerInitialized()
    {
        GameObject.Find("Scene Manager").GetComponent<PUT_controller>().set_ID(0);
        Create_player_Server();
    }

    void Create_player_Server()
    {
        background.SetActive(true);
        background.transform.position = new Vector3(0, 3, -1180.51f);
        Connetions.gameObject.SetActive(false);
        b_Disconnect.gameObject.SetActive(false);
        b_Quit.gameObject.SetActive(false);
        b_Сonnect.gameObject.SetActive(false);
        b_Server.gameObject.SetActive(false);
        f_IP.gameObject.SetActive(false);
        f_Port.gameObject.SetActive(false);
        connected = true;
        go = (GameObject)Network.Instantiate(PlayerPrefab, new Vector3(0,0,0), transform.rotation, 1);
    }
    void CreatePlayer()
    {
        background.SetActive(false);
        Connetions.gameObject.SetActive(false);
        b_Disconnect.gameObject.SetActive(false);
        b_Quit.gameObject.SetActive(false);
        b_Сonnect.gameObject.SetActive(false);
        b_Server.gameObject.SetActive(false);
        f_IP.gameObject.SetActive(false);
        f_Port.gameObject.SetActive(false);
        connected = true;
        go = (GameObject)Network.Instantiate(PlayerPrefab, new Vector3(0, 0, 0), transform.rotation, 1);

    }

    // При отключении от сервера
    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        connected = false;
        Application.LoadLevel(Application.loadedLevel);
    }

    // Вызывается каждый раз когда игрок отсоеденяется от сервера
    void OnPlayerDisconnected(NetworkPlayer pl)
    {
        Network.RemoveRPCs(pl);
        Network.DestroyPlayerObjects(pl);
    }
}