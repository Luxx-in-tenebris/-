using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PUT_controller : MonoBehaviour {

    private Button b_PYT; //ПУТ
    private int PYT_change_mod=1;
    public Animator anim;

    public int ID;//0-сервер, 1-клиент

    private GameObject Cube_PUT;

    private int cur_anim; //номер текущей анимации


    // Use this for initialization
    void Start ()
    {
        Cube_PUT = GameObject.Find("Cube(Clone)/Cube_PUT");
        anim = GameObject.Find("PUT").GetComponent<Animator>();
        b_PYT =GameObject.Find("PUT_Button").GetComponent<Button>();

       // cur_anim = 0;
        b_PYT.onClick.AddListener(Change_PYT_onClick);
        //GameObject.Find("Canvas_server/Dropdown").GetComponent<Dropdown>().onValueChanged.AddListener(change_Cube_pos_by_Dropdown());
        //char str = s_const__PYT[0][1];
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!Cube_PUT)
            Cube_PUT = GameObject.Find("Cube(Clone)/Cube_PUT");
    }

    void Change_PYT_onClick()//переключение
    {
        if(PYT_change_mod ==1)
        {
            PYT_change_mod = 2;//переключение на следующий режим

            anim.SetTrigger("b_move1");
            anim.ResetTrigger("b_move2");
            anim.ResetTrigger("b_move3");
            cur_anim = 1;
            Cube_PUT.transform.position = new Vector3(0, 0, 1);
        }
        else
        {
            if (PYT_change_mod == 2)
            {
                PYT_change_mod = 0;//переключение на следующий режим
               anim.SetTrigger("b_move2");
                anim.ResetTrigger("b_move1");
                anim.ResetTrigger("b_move3");
                cur_anim = 2;
                Cube_PUT.transform.position = new Vector3(0, 0, 2);
            }
            else
            {
                if (PYT_change_mod == 0)
                {
                    PYT_change_mod = 1;//переключение на следующий режим
                    anim.SetTrigger("b_move3");
                    anim.ResetTrigger("b_move1");
                    anim.ResetTrigger("b_move2");
                    cur_anim = 0;
                    Cube_PUT.transform.position = new Vector3(0, 0, 0);
                }
            }
        }
    }

    public void playAnim(string trigger)
    {
        anim.SetTrigger(trigger);

        if(trigger== "b_move1")
        {
            anim.ResetTrigger("b_move2");
            anim.ResetTrigger("b_move3");
        }
        if (trigger == "b_move2")
        {
            anim.ResetTrigger("b_move1");
            anim.ResetTrigger("b_move3");
        }
        if (trigger == "b_move3")
        {
            anim.ResetTrigger("b_move2");
            anim.ResetTrigger("b_move1");
        }
    }

  
    public int get_ID()
    {
        return ID;
    }
    public void set_ID(int id)
    {
        ID = id;

    }

    public void change_Cube_pos_by_Dropdown()
    {
       int value =GameObject.Find("Canvas_server/Dropdown_put").GetComponent<Dropdown>().value;
        if (value == 1)
        {
            Cube_PUT.transform.position = new Vector3(0, 0, 1);
        }
        if (value == 2)
        {
            Cube_PUT.transform.position = new Vector3(0, 0, 2);
        }
        if (value == 0)
        {
            Cube_PUT.transform.position = new Vector3(0, 0, 0);
        }
    }
}