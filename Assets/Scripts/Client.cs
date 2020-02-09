using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    private Button b_PYT; //ПУТ
    private int PYT_change_mod = 1;

    private Vector3 pos_PUT;

    private Vector3 moveDirection;//вектор передвижения

    private CharacterController controller;//ссылка на контроллер

    private float lastSynchronisationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private int syncCur_anim_PUT;
    //private int cur_anim = 0; //номер текущей анимации
    private PUT_controller but_contr;
    private T3_controller t3_contr;

    Vector3 syncPosition_Cube_PUT = Vector3.zero;
    Vector3 syncPosition_Cube_T3_1 = Vector3.zero;
    Vector3 syncPosition_Cube_T3_2 = Vector3.zero;
    Vector3 syncPosition_Cube_T3_3 = Vector3.zero;
    Vector3 syncPosition_Cube_T3_4 = Vector3.zero;
    Vector3 syncPosition_Cube_T3_5 = Vector3.zero;
    Vector3 syncPosition_Cube_T3_6 = Vector3.zero;
    Vector3 syncPosition_Cube_T3_VVD = Vector3.zero;
    Vector3 syncPosition_Cube_T3_SBR = Vector3.zero;

    private Vector3 syncStarPosition = Vector3.zero;//начальная позиция для интерполяции
    private Vector3 syncEndPosition = Vector3.zero;//конечная позиция для интерполяции
    private NetworkView networkview;

    private int ID;//0-сервер, 1-клиент
    private Dropdown dropdown;

    private Dropdown Dropdown_1;
    private Dropdown Dropdown_2;
    private Dropdown Dropdown_3;
    private Dropdown Dropdown_4;
    private Dropdown Dropdown_5;
    private Dropdown Dropdown_6;
    private Dropdown Dropdown_VVD;
    private Dropdown Dropdown_SBR;

    private GameObject Cube_PUT;

    private GameObject Cube_T3_VVD;
    private GameObject Cube_T3_1;
    private GameObject Cube_T3_2;
    private GameObject Cube_T3_3;
    private GameObject Cube_T3_4;
    private GameObject Cube_T3_5;
    private GameObject Cube_T3_6;
    private GameObject Cube_T3_SBR;

    private void Awake()
    {
        Cube_PUT = GameObject.Find("Cube(Clone)/Cube_PUT");

        Cube_T3_VVD = GameObject.Find("Cube(Clone)/Cube_T3_VVD");
        Cube_T3_1 = GameObject.Find("Cube(Clone)/Cube_T3_1");
        Cube_T3_2 = GameObject.Find("Cube(Clone)/Cube_T3_2");
        Cube_T3_3 = GameObject.Find("Cube(Clone)/Cube_T3_3");
        Cube_T3_4 = GameObject.Find("Cube(Clone)/Cube_T3_4");
        Cube_T3_5 = GameObject.Find("Cube(Clone)/Cube_T3_5");
        Cube_T3_6 = GameObject.Find("Cube(Clone)/Cube_T3_6"); ;
        Cube_T3_SBR = GameObject.Find("Cube(Clone)/Cube_T3_SBR");

        but_contr = GameObject.Find("Scene Manager").GetComponent<PUT_controller>();
        ID = GameObject.Find("Scene Manager").GetComponent<PUT_controller>().get_ID();
        networkview = transform.GetComponent<NetworkView>();

        t3_contr = GameObject.Find("Scene Manager").GetComponent<T3_controller>();

        dropdown = GameObject.Find("Canvas_server/Dropdown_put").GetComponent<Dropdown>();
        Dropdown_1 = GameObject.Find("Canvas_server/ТЗ/Dropdown_1").GetComponent<Dropdown>();
        Dropdown_2 = GameObject.Find("Canvas_server/ТЗ/Dropdown_2").GetComponent<Dropdown>();
        Dropdown_3 = GameObject.Find("Canvas_server/ТЗ/Dropdown_3").GetComponent<Dropdown>();
        Dropdown_4 = GameObject.Find("Canvas_server/ТЗ/Dropdown_4").GetComponent<Dropdown>();
        Dropdown_5 = GameObject.Find("Canvas_server/ТЗ/Dropdown_5").GetComponent<Dropdown>();
        Dropdown_6 = GameObject.Find("Canvas_server/ТЗ/Dropdown_6").GetComponent<Dropdown>();
        Dropdown_VVD = GameObject.Find("Canvas_server/ТЗ/Dropdown_VVD").GetComponent<Dropdown>();
        Dropdown_SBR = GameObject.Find("Canvas_server/ТЗ/Dropdown_SBR").GetComponent<Dropdown>();

        if (ID==0)
        {
            GameObject.Find("Canvas").SetActive(false);
        }
        if (ID==1)
        {
            b_PYT = GameObject.Find("PUT_Button").GetComponent<Button>();
            GameObject.Find("Canvas_server/Dropdown_put").SetActive(false); 
            GameObject.Find("Canvas_server/ТЗ").SetActive(false);
            GameObject.Find("Canvas_server/MTU").SetActive(false);
        }        
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    //вызывается с определенной частотой. Отвечает за сериализацию перемынных
    private void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        //если это наш персонаж
        if (stream.isWriting)
        {
            syncPosition_Cube_PUT = Cube_PUT.transform.position;
            syncPosition_Cube_T3_1 = Cube_T3_1.transform.position;
            syncPosition_Cube_T3_2 = Cube_T3_2.transform.position;
            syncPosition_Cube_T3_3 = Cube_T3_3.transform.position;
            syncPosition_Cube_T3_4 = Cube_T3_4.transform.position;
            syncPosition_Cube_T3_5 = Cube_T3_5.transform.position;
            syncPosition_Cube_T3_6 = Cube_T3_6.transform.position;
            syncPosition_Cube_T3_VVD = Cube_T3_VVD.transform.position;
            syncPosition_Cube_T3_SBR = Cube_T3_SBR.transform.position;

            stream.Serialize(ref syncPosition_Cube_PUT);
            stream.Serialize(ref syncPosition_Cube_T3_1);
            stream.Serialize(ref syncPosition_Cube_T3_2);
            stream.Serialize(ref syncPosition_Cube_T3_3);
            stream.Serialize(ref syncPosition_Cube_T3_4);
            stream.Serialize(ref syncPosition_Cube_T3_5);
            stream.Serialize(ref syncPosition_Cube_T3_6);
            stream.Serialize(ref syncPosition_Cube_T3_VVD);
            stream.Serialize(ref syncPosition_Cube_T3_SBR);
        }
        //если это другой игрок 
        else
        {
            stream.Serialize(ref syncPosition_Cube_PUT);
            stream.Serialize(ref syncPosition_Cube_T3_1);
            stream.Serialize(ref syncPosition_Cube_T3_2);
            stream.Serialize(ref syncPosition_Cube_T3_3);
            stream.Serialize(ref syncPosition_Cube_T3_4);
            stream.Serialize(ref syncPosition_Cube_T3_5);
            stream.Serialize(ref syncPosition_Cube_T3_6);
            stream.Serialize(ref syncPosition_Cube_T3_VVD);
            stream.Serialize(ref syncPosition_Cube_T3_SBR);


            //ПУТ
            if (Cube_PUT.transform.position.z!= syncPosition_Cube_PUT.z)
            {
                //если это телефон
                if (ID == 1)
                    but_contr.playAnim(TransformIntToANim((int)syncPosition_Cube_PUT.z));
                //если это комп
                else
                    dropdown.value = (int)syncPosition_Cube_PUT.z;
                Cube_PUT.transform.position = syncPosition_Cube_PUT;
            }

            //Т3
            //если это телефон
            if (ID == 1)
            {
                if (Cube_T3_VVD.transform.position.z != syncPosition_Cube_T3_VVD.z)
                {
                    t3_contr.VVD_click(((int)syncPosition_Cube_T3_VVD.z));
                }
                if (Cube_T3_SBR.transform.position.z != syncPosition_Cube_T3_SBR.z)
                {
                    t3_contr.SBR_click(((int)syncPosition_Cube_T3_SBR.z));
                }

                if (Cube_T3_1.transform.position.z != syncPosition_Cube_T3_1.z ||
                    Cube_T3_2.transform.position.z != syncPosition_Cube_T3_2.z ||
                    Cube_T3_3.transform.position.z != syncPosition_Cube_T3_3.z ||
                    Cube_T3_4.transform.position.z != syncPosition_Cube_T3_4.z ||
                    Cube_T3_5.transform.position.z != syncPosition_Cube_T3_5.z ||
                    Cube_T3_6.transform.position.z != syncPosition_Cube_T3_6.z)
                {
                    t3_contr.change_text_T3((int)syncPosition_Cube_T3_1.z, (int)syncPosition_Cube_T3_2.z,
                        (int)syncPosition_Cube_T3_3.z, (int)syncPosition_Cube_T3_4.z,
                        (int)syncPosition_Cube_T3_5.z, (int)syncPosition_Cube_T3_6.z);
                }
            }
            //если это комп
            else
            {
               
                
                Dropdown_1.value = (int)syncPosition_Cube_T3_1.z;
                Dropdown_2.value = (int)syncPosition_Cube_T3_2.z;
                Dropdown_3.value = (int)syncPosition_Cube_T3_3.z;
                Dropdown_4.value = (int)syncPosition_Cube_T3_4.z;
                Dropdown_5.value = (int)syncPosition_Cube_T3_5.z;
                Dropdown_6.value = (int)syncPosition_Cube_T3_6.z;
                Dropdown_VVD.value = (int)syncPosition_Cube_T3_VVD.z;
                Dropdown_SBR.value = (int)syncPosition_Cube_T3_SBR.z;
            }
            Cube_T3_1.transform.position = syncPosition_Cube_T3_1;
            Cube_T3_2.transform.position = syncPosition_Cube_T3_2;
            Cube_T3_3.transform.position = syncPosition_Cube_T3_3;
            Cube_T3_4.transform.position = syncPosition_Cube_T3_4;
            Cube_T3_5.transform.position = syncPosition_Cube_T3_5;
            Cube_T3_6.transform.position = syncPosition_Cube_T3_6;
            Cube_T3_VVD.transform.position = syncPosition_Cube_T3_VVD;
            Cube_T3_SBR.transform.position = syncPosition_Cube_T3_SBR;
        }
    }

    private string TransformIntToANim(int Cur_anim)
    {
        if (Cur_anim == 1)
        {
            return "b_move1";
        }
        if (Cur_anim == 2)
        {
            return "b_move2";
        }
        if (Cur_anim == 0)
        {
            return "b_move3";
        }
        return "0";
    }
   
}