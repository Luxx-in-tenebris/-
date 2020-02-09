using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T3_controller : MonoBehaviour {

    public Text T3_text;
    public Button[] b = new Button[10];//кнопки с цифрами

    private Animator anim;
    private Button b_start;
    private Button b_reset;
    private string str_before_enter;


    private bool can_reset = false;
    private int count_of_touches = 0;//сколько раз нажали на кнопку ВВД
    private bool can_start = false;
    private int nubmer_to_write = -1; //на какое место сейчас пишется цифра(-1 --это типо обнуление переменной)
    private bool endWrite = false; //конец ввода

    private GameObject Cube_T3_VVD;
    private GameObject Cube_T3_1;
    private GameObject Cube_T3_2;
    private GameObject Cube_T3_3;
    private GameObject Cube_T3_4;
    private GameObject Cube_T3_5;
    private GameObject Cube_T3_6;
    private GameObject Cube_T3_SBR;

    private Dropdown Dropdown_1;
    private Dropdown Dropdown_2;
    private Dropdown Dropdown_3;
    private Dropdown Dropdown_4;
    private Dropdown Dropdown_5;
    private Dropdown Dropdown_6;
    private Dropdown Dropdown_VVD;
    private Dropdown Dropdown_SBR;

    int num_in1 = 0;
    int num_in2 = 0;

    private void Awake()
    {
        if (GameObject.Find("Scene Manager").GetComponent<PUT_controller>().get_ID() == 0)
        {
            Dropdown_1 = GameObject.Find("Canvas_server/ТЗ/Dropdown_1").GetComponent<Dropdown>();
            Dropdown_2 = GameObject.Find("Canvas_server/ТЗ/Dropdown_2").GetComponent<Dropdown>();
            Dropdown_3 = GameObject.Find("Canvas_server/ТЗ/Dropdown_3").GetComponent<Dropdown>();
            Dropdown_4 = GameObject.Find("Canvas_server/ТЗ/Dropdown_4").GetComponent<Dropdown>();
            Dropdown_5 = GameObject.Find("Canvas_server/ТЗ/Dropdown_5").GetComponent<Dropdown>();
            Dropdown_6 = GameObject.Find("Canvas_server/ТЗ/Dropdown_6").GetComponent<Dropdown>();
            Dropdown_VVD = GameObject.Find("Canvas_server/ТЗ/Dropdown_VVD").GetComponent<Dropdown>();
            Dropdown_SBR = GameObject.Find("Canvas_server/ТЗ/Dropdown_SBR").GetComponent<Dropdown>();
        }

        //Cube_T3_VVD = GameObject.Find("Cube(Clone)/Cube_T3_VVD");
        //Cube_T3_1 = GameObject.Find("Cube(Clone)/Cube_T3_1");
        //Cube_T3_2 = GameObject.Find("Cube(Clone)/Cube_T3_2");
        //Cube_T3_3 = GameObject.Find("Cube(Clone)/Cube_T3_3");
        //Cube_T3_4 = GameObject.Find("Cube(Clone)/Cube_T3_4");
        //Cube_T3_5 = GameObject.Find("Cube(Clone)/Cube_T3_5");
        //Cube_T3_6 = GameObject.Find("Cube(Clone)/Cube_T3_6"); ;
        //Cube_T3_SBR = GameObject.Find("Cube(Clone)/Cube_T3_SBR");

        anim = GameObject.Find("T3").GetComponent<Animator>();
        b_start = GameObject.Find("ВВД").GetComponent<Button>();
        b_reset = GameObject.Find("СБР").GetComponent<Button>();
    }

    // Use this for initialization
    void Start ()
    {

        b_start.onClick.AddListener(access_to_write);
        b_reset.onClick.AddListener(reset);
        //кнопки с цифрами
        b[0].onClick.AddListener(() => set_writing_number('0'));
        b[1].onClick.AddListener(() => set_writing_number('1'));
        b[2].onClick.AddListener(() => set_writing_number('2'));
        b[3].onClick.AddListener(() => set_writing_number('3'));
        b[4].onClick.AddListener(() => set_writing_number('4'));
        b[5].onClick.AddListener(() => set_writing_number('5'));
        b[6].onClick.AddListener(() => set_writing_number('6'));
        b[7].onClick.AddListener(() => set_writing_number('7'));
        b[8].onClick.AddListener(() => set_writing_number('8'));
        b[9].onClick.AddListener(() => set_writing_number('9'));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!Cube_T3_VVD)
        {
            Cube_T3_VVD = GameObject.Find("Cube(Clone)/Cube_T3_VVD");
        }
        if (!Cube_T3_1)
        {
            Cube_T3_1=GameObject.Find("Cube(Clone)/Cube_T3_1");
        }
        if (!Cube_T3_2)
        {
            Cube_T3_2=GameObject.Find("Cube(Clone)/Cube_T3_2");
        }
        if (!Cube_T3_3)
        {
            Cube_T3_3=GameObject.Find("Cube(Clone)/Cube_T3_3");
        }
        if (!Cube_T3_4)
        {
            Cube_T3_4=GameObject.Find("Cube(Clone)/Cube_T3_4");
        }
        if (!Cube_T3_5)
        {
            Cube_T3_5=GameObject.Find("Cube(Clone)/Cube_T3_5");
        }
        if (!Cube_T3_6)
        {
            Cube_T3_6=GameObject.Find("Cube(Clone)/Cube_T3_6");
        }
        if (!Cube_T3_SBR)
        {
            Cube_T3_SBR = GameObject.Find("Cube(Clone)/Cube_T3_SBR");
        }
        if (GameObject.Find("Scene Manager").GetComponent<PUT_controller>().get_ID() == 0)
        {
            if (Dropdown_VVD.value==0 || Dropdown_VVD.value ==2)
            {
                num_in1++;
                if(num_in1==1)
                {
                    Dropdown_1.interactable = false;
                    Dropdown_2.interactable = false;
                    Dropdown_3.interactable = false;
                    Dropdown_4.interactable = false;
                    Dropdown_5.interactable = false;
                    Dropdown_6.interactable = false;
                    Dropdown_SBR.interactable = false;
                    Cube_T3_VVD.transform.position = new Vector3(0, 0, Dropdown_VVD.value);
                    Cube_T3_SBR.transform.position = new Vector3(0, 0, 0);
                }
                num_in2 = 0;
            }
            else
            {
                num_in2++;
                if(num_in2==1)
                {
                    Dropdown_1.interactable = true;
                    Dropdown_2.interactable = true;
                    Dropdown_3.interactable = true;
                    Dropdown_4.interactable = true;
                    Dropdown_5.interactable = true;
                    Dropdown_6.interactable = true;
                    Dropdown_SBR.interactable = true;
                    Cube_T3_VVD.transform.position = new Vector3(0, 0, 1);
                }
                num_in1 = 0;
            }



        }
    }
    void set_writing_number(char i)
    {
        T3_text.color = Color.green;
        if (can_start==true && endWrite==false)
        {
            Set_number_of_place(int.Parse(""+i));
            Start_writing(i);

        }
        

    }
    
    void access_to_write()
    {
        count_of_touches++;
        
        if (count_of_touches==1)
        {
            Cube_T3_VVD.transform.position = new Vector3(0, 0, 1);
            Cube_T3_SBR.transform.position = new Vector3(0, 0, 0);

            T3_text.color = Color.yellow; 
            can_reset = true;
            can_start = true;
            endWrite = false;
            anim.ResetTrigger("end");
            anim.SetTrigger("1");

            str_before_enter = T3_text.text;
        }
        else
        {
            if(count_of_touches == 2)
            {
                Cube_T3_SBR.transform.position = new Vector3(0, 0, 0);
                Cube_T3_VVD.transform.position = new Vector3(0, 0, 2);
                T3_text.color = Color.green;
                can_reset = false;
                can_start = false;
                endWrite = true;
                nubmer_to_write = -1;
                anim.SetTrigger("end");
                count_of_touches = 0;
            }
        }
    }
    void Set_number_of_place(int number)
    {
        switch (nubmer_to_write)
        {
            case -1:
                nubmer_to_write = 0;
                Cube_T3_1.transform.position = new Vector3(0, 0, number);
                anim.ResetTrigger("1");
                anim.SetTrigger("end");
                anim.SetTrigger("2");
                break;
            case 0:
                nubmer_to_write = 1;
                Cube_T3_2.transform.position = new Vector3(0, 0, number);
                anim.ResetTrigger("2");
                anim.SetTrigger("end");
                anim.SetTrigger("3");
                break;
            case 1:
                nubmer_to_write = 5;
                Cube_T3_3.transform.position = new Vector3(0, 0, number);
                anim.ResetTrigger("3");
                anim.SetTrigger("end");
                anim.SetTrigger("4");
                break;
            case 5:
                nubmer_to_write = 6;
                Cube_T3_4.transform.position = new Vector3(0, 0, number);
                anim.ResetTrigger("4");
                anim.SetTrigger("end");
                anim.SetTrigger("5");
                break;
            case 6:
                nubmer_to_write = 10;
                Cube_T3_5.transform.position = new Vector3(0, 0, number);
                anim.ResetTrigger("5");
                anim.SetTrigger("end");
                anim.SetTrigger("6");
                break;
            case 10:
                nubmer_to_write = 11;
                Cube_T3_6.transform.position = new Vector3(0, 0, number);
                anim.ResetTrigger("6");
                anim.SetTrigger("end");
                anim.SetTrigger("1");
                break;
            case 11:
                nubmer_to_write = 0;
                Cube_T3_1.transform.position = new Vector3(0, 0, number);
                anim.ResetTrigger("1");
                anim.SetTrigger("end");
                anim.SetTrigger("2");

                break;

        } 
    }
    void reset()
    {
        if(can_reset == true)
        {
            Cube_T3_SBR.transform.position = new Vector3(0, 0, 1);
            Cube_T3_VVD.transform.position = new Vector3(0, 0, 0);
            T3_text.text = str_before_enter;
            Cube_T3_1.transform.position = new Vector3(0, 0, int.Parse(""+str_before_enter[0]));
            Cube_T3_2.transform.position = new Vector3(0, 0, int.Parse("" + str_before_enter[1]));
            Cube_T3_3.transform.position = new Vector3(0, 0, int.Parse("" + str_before_enter[5]));
            Cube_T3_4.transform.position = new Vector3(0, 0, int.Parse("" + str_before_enter[6]));
            Cube_T3_5.transform.position = new Vector3(0, 0, int.Parse("" + str_before_enter[10]));
            Cube_T3_6.transform.position = new Vector3(0, 0, int.Parse("" + str_before_enter[11]));
            can_start = false;
            endWrite = true;
            nubmer_to_write = -1;
            anim.SetTrigger("end");
            count_of_touches = 0;
            can_reset = false;
        }
    }
    void Start_writing(char number)
    {
        
        char[] text_of_T3 = new char[12+1];
        //инициализируем массив char
        for (int i=0;i<T3_text.text.Length;i++)
        {
            text_of_T3[i] = T3_text.text[i];
        }
        text_of_T3[nubmer_to_write] = number;
        T3_text.text = "";
        //обновляем текст ТЗ
        for (int i = 0; i < 12; i++)
        {
            T3_text.text += text_of_T3[i];
            Debug.Log(text_of_T3[i]);
        }
      
    }
    public void playAnim(string trigger)
    {
        anim.SetTrigger(trigger);

        if (trigger == "1")
        {
            anim.ResetTrigger("2");
            anim.ResetTrigger("3");
            anim.ResetTrigger("4");
            anim.ResetTrigger("5");
            anim.ResetTrigger("6");
        }
        if (trigger == "2")
        {
            anim.ResetTrigger("1");
            anim.ResetTrigger("3");
            anim.ResetTrigger("4");
            anim.ResetTrigger("5");
            anim.ResetTrigger("6");
        }
        if (trigger == "3")
        {
            anim.ResetTrigger("2");
            anim.ResetTrigger("1");
            anim.ResetTrigger("4");
            anim.ResetTrigger("5");
            anim.ResetTrigger("6");
        }
        if (trigger == "4")
        {
            anim.ResetTrigger("2");
            anim.ResetTrigger("3");
            anim.ResetTrigger("1");
            anim.ResetTrigger("5");
            anim.ResetTrigger("6");
        }
        if (trigger == "5")
        {
            anim.ResetTrigger("2");
            anim.ResetTrigger("3");
            anim.ResetTrigger("4");
            anim.ResetTrigger("1");
            anim.ResetTrigger("6");
        }
        if (trigger == "6")
        {
            anim.ResetTrigger("2");
            anim.ResetTrigger("3");
            anim.ResetTrigger("4");
            anim.ResetTrigger("5");
            anim.ResetTrigger("1");
        }
    }
    //==обработчики дропдаунов=================================================================================================
    public void change1_Cube_T3_1_pos_by_Dropdown()
    {
        Cube_T3_1.transform.position = new Vector3(0, 0, Dropdown_1.value);
    }
    public void change1_Cube_T3_2_pos_by_Dropdown()
    {
        Cube_T3_2.transform.position = new Vector3(0, 0, Dropdown_2.value);
    }
    public void change1_Cube_T3_3_pos_by_Dropdown()
    {
        Cube_T3_3.transform.position = new Vector3(0, 0, Dropdown_3.value);
    }
    public void change1_Cube_T3_4_pos_by_Dropdown()
    {
        Cube_T3_4.transform.position = new Vector3(0, 0, Dropdown_4.value);
    }
    public void change1_Cube_T3_5_pos_by_Dropdown()
    {
        Cube_T3_5.transform.position = new Vector3(0, 0, Dropdown_5.value);
    }
    public void change1_Cube_T3_6_pos_by_Dropdown()
    {
        Cube_T3_6.transform.position = new Vector3(0, 0, Dropdown_6.value);
    }
    public void change1_Cube_T3_SBR_pos_by_Dropdown()
    {
        Cube_T3_SBR.transform.position = new Vector3(0, 0, Dropdown_SBR.value);
        if (Dropdown_SBR.value == 1)
        {
            Dropdown_1.interactable = false;
            Dropdown_2.interactable = false;
            Dropdown_3.interactable = false;
            Dropdown_4.interactable = false;
            Dropdown_5.interactable = false;
            Dropdown_6.interactable = false;
            Dropdown_SBR.interactable = false;
            //Dropdown_VVD.value = 0;
            //Cube_T3_VVD.transform.position = new Vector3(0, 0, 0);
            //Dropdown_SBR.value = 0;
        }
    }

    //==изменение чисел================================================================================================= 
    public void change_text_T3(int z1, int z2, int z3, int z4, int z5, int z6)
    {
        T3_text.text = "" + z1 + z2 + " : " + z3 + z4 + " : " + z5 + z6;
    }
    public void VVD_click(int i)
    {
        if(i==1&& can_start==false)
            access_to_write();
        if(i==2&& can_start == true)
            access_to_write();
    }
    public void SBR_click(int i)
    {
        if(i==1)
            reset();
    }
}


