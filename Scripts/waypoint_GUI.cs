using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class waypoint_GUI : MonoBehaviour
{

    //public Image flecha;
    private GameController gamecontroller_aux;
    public GameObject ondas;
    private GameObject jugador;
    public Transform objetivo_llave_portal;
    public Transform objetivo_portal;

    [SerializeField]
    private Camera camera_aux;
    private float top_y;
    private float top_x;
    private float bot_y;
    private float bot_X;
    private float diference_x;
    private float diference_y;

    void Start()
    {
        gamecontroller_aux = FindObjectOfType<GameController>();
        if (gamecontroller_aux != null)
        {
            jugador = gamecontroller_aux.jugadoractual();
        }

        if (FindObjectOfType<Camera>() != null)
        {
            camera_aux = FindObjectOfType<Camera>();
            //Debug.Log(""+camera_aux.ViewportToWorldPoint(new Vector3(1, 1, -20)));
            Vector3 aux_max = camera_aux.ViewportToWorldPoint(new Vector3(1, 1, 20));
            Vector3 aux_min = camera_aux.ViewportToWorldPoint(new Vector3(0, 0, 20));
            diference_x = (aux_max.x - aux_min.x) / 2;
            diference_y = (aux_max.y - aux_min.y) / 2;
        }
    }



    void Update()
    {
        Transform a_usar;
        //Se verifica si primero esta disponible la llave antes que la salida para guiar
        if (objetivo_llave_portal != null && objetivo_llave_portal.gameObject.activeSelf && objetivo_llave_portal.transform.localScale.x > 0)
        {
            a_usar = objetivo_llave_portal;
        }
        else
        {
            if (objetivo_portal != null && objetivo_portal.gameObject.activeSelf)
            {
                a_usar = objetivo_portal;
            }
            else
            {
                a_usar = null;
            }
        }

        if (a_usar == null)
        {
            ondas.SetActive(false);
        }

        if (a_usar != null)
        {
            /*
            //Maximos y minimos de cada EJE con respecto a la pantalla (CODIGO QUE ANDA)
            
            float minimox=(jugador.transform.position.x)-21f;
            float maximox=(jugador.transform.position.x)+21f;

            float minimoy=(jugador.transform.position.y)-12f;
            float maximoy=(jugador.transform.position.y)+12f;
            */

            float minimox = (jugador.transform.position.x) - diference_x;
            float maximox = (jugador.transform.position.x) + diference_x;

            float minimoy = (jugador.transform.position.y) - diference_y;
            float maximoy = (jugador.transform.position.y) + diference_y;

            //Debug.Log("Screen Largo es : " + left.x +"  "+ right.x);

            Vector3 posi = a_usar.position;

            posi.x = Mathf.Clamp(posi.x, minimox, maximox);
            posi.y = Mathf.Clamp(posi.y, minimoy, maximoy);
            ondas.transform.position = new Vector3(posi.x, posi.y, 0f);
        }
    }
}    
