using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	//VARIABLE STATICA PARA OPCIONES
	public static GameController master_control;
	private audiocontroller audiocontroller_aux;
	private Adcaller adlocal;
	private changer_fades cambiador_fades;
	private jugador_control_animacion jugador_anim_aux;
	private GameObject creador_aux_luz;
	private GameObject creador_aux_camara;
	//VARIABLES CONFIG INICIAL Y MENU
	private bool direcciontactil = true;
	private bool musica = true;
	private bool sonido = true;
	private bool tutorial;
	private bool continuar_bool = false;
	//LINK A GAME OBJECTS NESECARIOS PARA CADA CREACION
	public GameObject jugador_obj; //JUGADOR
	public Light luz_jugador; //LUZ JUGADOR
	public GameObject MenuCanvas;

	public GameObject MenuCanvas_Menu;
	//////////////////////////////////////////////
	//VARIABLES LOCALES DE USO GENERAL MIENTRAS JUEGO
	private int escenactual;
	private int nivelactual;
	private int puntaje;
	private puntitoscrip puntos_encript;
	private vidasscrip vidas_encript;
	//private int vidas;
	private bool dead;
	private bool control;
	private bool controles_en_evento;
	private bool pause;
	private int videovistos;
	//VARIABLES CANVAS
	public Text puntos_numero;
	//VARIABLES AUXILIARES DE CONTROL DE NIVELES
	GameObject nivelactual_gameobject;
	GameObject actualnivel_aux;

	//VARIABLES DE CONFIG
	string max_puntaje;
	int sentido_move;
	int musica_onoff;
	int sonido_onoff;



	string vidas_continuar;
	string puntaje_continuar;
	int nivel_continuar;

	void Awake()
	{
		if (master_control == null)
		{
			DontDestroyOnLoad(gameObject);
			master_control = this;
		}
		else
		{
			if (master_control != null)
			{
				if (master_control != this)
				{
					if (SceneManager.GetActiveScene().name == "Menu")
					{
						Resources.UnloadUnusedAssets();
						master_control.jugador_obj = null;
						master_control.luz_jugador = null;
						master_control.MenuCanvas = null;
						master_control.puntos_numero = null;
					}
					if (SceneManager.GetActiveScene().name == "Juego" || SceneManager.GetActiveScene().name == "ZONE_JOBS") //BORRAR desp de pruebas
					{
						Resources.UnloadUnusedAssets();
						if (master_control.MenuCanvas == null)
						{
							master_control.MenuCanvas = MenuCanvas;
						}
						if (master_control.puntos_numero == null)
						{
							master_control.puntos_numero = puntos_numero;
						}
					}
					Destroy(gameObject);
				}
			}
			else Destroy(gameObject);
		}
		max_puntaje = PlayerPrefs.GetString("hiscore", "v");
		sentido_move = PlayerPrefs.GetInt("movimiento", 1);
		musica_onoff = PlayerPrefs.GetInt("musica", 1);
		sonido_onoff = PlayerPrefs.GetInt("sonido", 1);

		//valores para continuar
		vidas_continuar = PlayerPrefs.GetString("continue_vidas", "z");
		puntaje_continuar = PlayerPrefs.GetString("continue_puntaje", "v");
		nivel_continuar = PlayerPrefs.GetInt("nivel_actual", 1);
	}




	////////START///////
	void Start()
	{
		audiocontroller_aux = FindObjectOfType<audiocontroller>();
		adlocal = GetComponent<Adcaller>();
		cambiador_fades = FindObjectOfType<changer_fades>();

		//Componenetes Iniciales del juego
		puntos_encript = gameObject.AddComponent(typeof(puntitoscrip)) as puntitoscrip;
		vidas_encript = gameObject.AddComponent(typeof(vidasscrip)) as vidasscrip;

		StartCoroutine(Menu());
	}

	private IEnumerator Menu()
	{
		//Re obtengo el "telon" en cada escena
		cambiador_fades = FindObjectOfType<changer_fades>();

		if (SceneManager.GetActiveScene().name == "Menu")
		{
			//Musica MENU
			audiocontroller_aux.reproducir("Menu_Intro");

			//Buscando CANVAS de MENU
			MenuCanvas_Menu = GameObject.Find("Panel_Principal_Canvas");
			max_puntaje = PlayerPrefs.GetString("hiscore", "v");

			//Controlo HISCORE
			if (max_puntaje != "v" && MenuCanvas_Menu != null)
			{
				MenuCanvas_Menu.gameObject.transform.GetChild(4).gameObject.SetActive(true);
				GameObject aux_puntajemaximo = MenuCanvas_Menu.gameObject.transform.GetChild(4).GetChild(1).gameObject; //UI donde defino el puntaje maximo actual
				aux_puntajemaximo.GetComponent<Text>().text = "" + puntos_encript.decript_publico(max_puntaje);
			}
			else
			{
				if (max_puntaje == "v" && MenuCanvas_Menu != null)
				{
					MenuCanvas_Menu.gameObject.transform.GetChild(4).gameObject.SetActive(false);
					GameObject aux_puntajemaximo = MenuCanvas_Menu.gameObject.transform.GetChild(4).GetChild(1).gameObject; //UI donde defino el puntaje maximo actual
					aux_puntajemaximo.GetComponent<Text>().text = "";
				}
			}



			//Controlo Vidas para Continuar partida
			string aux_local = PlayerPrefs.GetString("continue_vidas", "z");
			if (aux_local != "z")
			{
				//MenuCanvas_Menu.gameObject.transform.GetChild(5).gameObject.SetActive(true);
				MenuCanvas_Menu.gameObject.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
			}
			else
			{
				MenuCanvas_Menu.gameObject.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
			}

		}

		//Mientras estoy en el menu realizar...
		while (SceneManager.GetActiveScene().name == "Menu")
		{
			yield return null;
		}

		StartCoroutine(PartidaInicial());

	}

	private IEnumerator PartidaInicial()
	{
		if (SceneManager.GetActiveScene().name == "Juego" || SceneManager.GetActiveScene().name == "ZONE_JOBS") //BORRAR desp de pruebas
		{
			//Debug.Log("Estoy en el Juego");

			//Re obtengo el "telon" en cada escena
			cambiador_fades = FindObjectOfType<changer_fades>();

			if (continuar_bool == false)
			{
				creacionelementosprimarios();
				iniciojuego();
			}
			while (SceneManager.GetActiveScene().name == "Juego" || SceneManager.GetActiveScene().name == "ZONE_JOBS") //BORRAR desp de pruebas)
			{
				if (Input.GetKeyDown(KeyCode.Escape) && !dead)
				{
					pause_game();
					//guardadovidasypuntajeactual();
				}
				yield return null;
			}
		}
		if (SceneManager.GetActiveScene().name == "Menu")
		{
			StartCoroutine(Menu());
			continuar_bool = false;
		}
	}
	void OnDisable() //CUANDO SE CIERRA LA APP
	{
		//PlayerPrefs.SetInt("Puntaje_Maximo", puntaje);
	}
	void OnEnable() //CUANDO SE ABRE LA APP
	{
		//int puntaje=PlayerPrefs.GetInt("Puntaje_Maximo");
	}


	public void botoncontinue()
	{
		continuar_bool = true;
	}

	private void creacionelementosprimarios()
	{
		//IMPORTADORES AUXILIARES
		GameObject importador_aux;
		GameObject creador_aux;

		//Sintaxis para CREAR nuevo objeto, no se usa NEW
		//puntos_encript = gameObject.AddComponent(typeof(puntitoscrip))as puntitoscrip;
		//vidas_encript = gameObject.AddComponent(typeof(vidasscrip))as vidasscrip;

		importador_aux = Resources.Load("PrefabIniciales/Jugador/Z-Jugador") as GameObject;
		creador_aux = Instantiate(importador_aux, Vector3.zero, Quaternion.identity);
		creador_aux.transform.position = new Vector3(0, 0, 0);
		creador_aux.transform.Rotate(0, -90, 0, Space.Self);
		creador_aux.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
		jugador_obj = creador_aux;
		////Creacion LUZ JUGADOR
		importador_aux = Resources.Load("PrefabIniciales/Luz_Jugador") as GameObject;
		creador_aux_luz = Instantiate(importador_aux, Vector3.zero, Quaternion.identity);
		creador_aux_luz.transform.position = new Vector3(0, 0, -14);
		creador_aux_luz.transform.Rotate(0, 0, 0, Space.Self);
		creador_aux_luz.transform.localScale = new Vector3(1, 1, 1);
		luz_jugador = creador_aux_luz.GetComponent<Light>();
		////Creacion PARTICULAS EN EL ESPACIO
		importador_aux = Resources.Load("PrefabIniciales/ParticulasEspacio") as GameObject;
		creador_aux = Instantiate(importador_aux, Vector3.zero, Quaternion.Euler(-90, 0, 0));
		creador_aux.transform.position = new Vector3(0, 0, 56);
		creador_aux.transform.localScale = new Vector3(1, 1, 1);
		////Creacion MAIN CAMERA
		importador_aux = Resources.Load("PrefabIniciales/Main Camera") as GameObject;
		creador_aux_camara = Instantiate(importador_aux, Vector3.zero, Quaternion.identity);
		creador_aux_camara.transform.position = new Vector3(0, 0, -20);
		creador_aux_camara.transform.Rotate(0, 0, 0, Space.Self);
		creador_aux_camara.transform.localScale = new Vector3(1, 1, 1);

		jugador_anim_aux = FindObjectOfType<jugador_control_animacion>();
	}
	private void buscarcomponentesiniciales()
	{
		//Asignacion camara al panel de vidas
		buscador_camara_para_canvas aux_buscador = FindObjectOfType<buscador_camara_para_canvas>();
		aux_buscador.asignar_camara_al_canvas_vidas(creador_aux_camara.GetComponent<Camera>());

		if (GameObject.Find("Panel_Juego"))
		{
			MenuCanvas.gameObject.transform.GetChild(1).gameObject.SetActive(false); //menu pausa
			MenuCanvas.gameObject.transform.GetChild(2).gameObject.SetActive(false); //menu al perder
		}
		if (tutorial && (musica_onoff == 1))
		{
			audiocontroller_aux.reproducir("Nivel_3");
		}
		else
		{
			if (musica_onoff == 1)
			{
				musica_activar(true);
				//audiocontroller_aux.reproducir("Nivel_1");
			}
		}
	}
	private void iniciojuego()
	{
		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
		}
		pause = false;
		dead = false;
		buscarcomponentesiniciales();
		control_activar();
		controlenevento_desactivar();
		resetearpuntos();


		vidas_encript.definir_vidas("z");
		for (int i = 0; i < 5; i++)
		{
			vidas_encript.incre_vida();
		}

		videovistos = 2;

		vidas_gui aux_perder = FindObjectOfType<vidas_gui>();
		aux_perder.vidas_actuales_met(vidas_encript.vidas_actuales());

		cargarnivel(nivelactual);
	}

	public void continuarjuego()
	{

		audiocontroller_aux.detener("Menu_Intro");
		StartCoroutine(delay_continuar());
		//vidas_gui aux_vidas_gui = FindObjectOfType<vidas_gui>();
		//aux_vidas_gui.vidas_actuales_met(vidas_encript.vidas_actuales());
	}
	IEnumerator delay_continuar()
	{
		yield return new WaitForSeconds(1.8f);
		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
		}

		pause = false;
		dead = false;

		creacionelementosprimarios();
		buscarcomponentesiniciales();
		control_activar();
		controlenevento_desactivar();

		vidas_encript.definir_vidas(PlayerPrefs.GetString("continue_vidas", "z"));
		puntos_encript.definir_puntos(PlayerPrefs.GetString("continue_puntaje", "v"));
		puntaje = puntos_encript.decript();
		puntos_numero.text = "" + puntaje;

		nivelactual = PlayerPrefs.GetInt("continue_nivel", 1);

		//vidas_encript.definir_vidas(vidas_continuar);
		vidas_gui aux_vidas_gui = FindObjectOfType<vidas_gui>();
		aux_vidas_gui.vidas_actuales_met(vidas_encript.vidas_actuales());

		cargarnivel(nivelactual);
	}

	public void cargarnivel(int nivelacargar)
	{
		if (actualnivel_aux != null)
		{
			Destroy(actualnivel_aux.gameObject);
		}
		if (Resources.Load("Niveles/Nivel" + nivelactual) != null)
		{
			nivelactual_gameobject = Resources.Load("Niveles/Nivel" + nivelactual) as GameObject;
			actualnivel_aux = Instantiate(nivelactual_gameobject, Vector3.zero, Quaternion.identity);
		}
	}
	public void sumarpuntos(int valor)
	{
		puntaje += valor;
		puntos_numero.text = "" + puntaje;
		sumpunt(valor);
		if (MenuCanvas.gameObject.transform.GetChild(7).GetChild(2) != null)
		{
			GameObject aux_local = MenuCanvas.gameObject.transform.GetChild(7).GetChild(2).gameObject;
			aux_local.gameObject.GetComponent<Text>().text = "" + puntos_encript.decript();
		}
	}
	public void sumpunt(int valor)
	{
		puntos_encript.encript(valor);
	}

	public void restarpuntos(int valor)
	{
		puntaje -= valor;
		puntos_numero.text = "" + puntaje;
	}
	public void resetearpuntos()
	{
		puntaje = 0;
		puntos_numero.text = "" + puntaje;
		puntos_encript.limpiar();
	}
	public int puntosacuales()
	{
		return puntaje;
	}
	public void perder()
	{
		//quito vida en interfaz grafica
		vidas_gui aux_perder = FindObjectOfType<vidas_gui>();
		aux_perder.quitar_vidas(vidas_encript.vidas_actuales());

		//Decremento vidas encriptadas
		vidas_encript.decre_vida();

		dead = true; //estado muerto se setea en TRUE
		control_desactivar(); //desactivo controles
		audiocontroller_aux.reproducir("morir"); //inicio sonido de muerte

		if (vidas_encript.vidas_actuales() > 0)
		{
			guardadovidasypuntajeactual();
			MenuCanvas.gameObject.transform.GetChild(2).gameObject.SetActive(true); //menu al perder
		}
		else
		{
			if (vidas_encript.vidas_actuales() == 0)
			{
				guardadovidasypuntajeactual();
				MenuCanvas.gameObject.transform.GetChild(4).gameObject.SetActive(true); //menu al perder
			}
		}
	}
	public void siguiente_nivel()
	{
		nivelactual++;
		if (nivelactual > 0 && tutorial)
		{
			resetearpuntos();
			tutorial = false;
		}
		//resetear_propiedades_luz_jugador();
		//codigo controlador de niveles
		/*
		if(nivelactual==1&&tutorial)
		{
			tutorial=false;
		}
		*/
		if (nivelactual > 24)//limite de niveles a completar antes de arrancar de nuevo NIVEL MAXIMO
		{
			StartCoroutine(temporizador_para_final());
			StartCoroutine(temporizador_para_nivel_rutina());
			maximo_puntaje_control();
		}
		else
		{
			StartCoroutine(temporizador_para_nivel_rutina());
			guardadovidasypuntajeactual(); //guardo vidas y puntos
		}
	}

	public void restart()
	{
		//Reinicia Desde el nivel 1 y puntaje 0
		nivelactual = 1;
		//Resetear vidas

		//vidas=5;
		while (vidas_encript.vidas_actuales() > 0)
		{
			vidas_encript.decre_vida();
		}
		for (int i = 0; i < 5; i++)
		{
			vidas_encript.incre_vida();
		}

		vidas_gui aux_perder = FindObjectOfType<vidas_gui>();

		StartCoroutine(temporizador_para_nivel_rutina());

		aux_perder.vidas_actuales_met(vidas_encript.vidas_actuales());
		//Ingresar metodo compara con googleplay y local mejor puntaje y reemplaza o sube
		//Utilizando el puntaje encriptado
		resetearpuntos();

		//Destroy(actualnivel_aux);
		//cargarnivel(nivelactual);
		//respawnear en posicion 0
		//mover_jugador_a_centro();
		//dead=false;
		
		//retiro GUI de muerte
		if (MenuCanvas.gameObject.transform.GetChild(2).gameObject.activeSelf)
		{
			MenuCanvas.gameObject.transform.GetChild(2).gameObject.SetActive(false); //menu al perder
		}
		else
		{
			if (MenuCanvas.gameObject.transform.GetChild(4).gameObject.activeSelf)
			{
				MenuCanvas.gameObject.transform.GetChild(4).gameObject.SetActive(false); //menu game over
			}
			else
			{
				if (MenuCanvas.gameObject.transform.GetChild(9).gameObject.activeSelf)
				{
					MenuCanvas.gameObject.transform.GetChild(9).gameObject.SetActive(false); //menu restart
				}
				else
				{
					if (MenuCanvas.gameObject.transform.GetChild(10).gameObject.activeSelf)
					{
						MenuCanvas.gameObject.transform.GetChild(10).gameObject.SetActive(false); //menu restart game over
					}
				}
			}
		}

		//LISTO PARA EMPEZAR A JUGAR
		control_activar();
	}
	public void continue_game()
	{
		//retiro GUI de muerte
		MenuCanvas.gameObject.transform.GetChild(2).gameObject.SetActive(false);
		MenuCanvas.gameObject.transform.GetChild(4).gameObject.SetActive(false);
		//respawnear en posicion 0 
		mover_jugador_a_centro();
		dead = false;
		//LISTO PARA EMPEZAR A JUGAR
		control_activar();
		//Agrego al GUI las vidas actuales
		vidas_gui aux_perder = FindObjectOfType<vidas_gui>();
		aux_perder.vidas_actuales_met(vidas_encript.vidas_actuales());
	}
	public void continue_game_desp_publi()
	{
		//Luego de ver el video publicitario
		if (videovistos == 0)
		{
			//MenuCanvas.gameObject.transform.GetChild(4).gameObject.SetActive(false);
			GameObject auxforchild = MenuCanvas.gameObject.transform.GetChild(4).gameObject;
			auxforchild.gameObject.transform.GetChild(1).gameObject.SetActive(false);
		}
		videovistos--;
		//vidas=1;
		vidas_encript.incre_vida();
		vidas_gui aux_perder = FindObjectOfType<vidas_gui>();
		aux_perder.vidas_actuales_met(vidas_encript.vidas_actuales());
		//retiro GUI de muerte
		MenuCanvas.gameObject.transform.GetChild(4).gameObject.SetActive(false);
		//respawnear en posicion 0 
		mover_jugador_a_centro();
		dead = false;
		//LISTO PARA EMPEZAR A JUGAR
		control_activar();
	}
	//METODOS LOCALES
	private void mover_jugador_a_centro()
	{
		Vector3 origen = jugador_obj.transform.position;
		float actuallerptime = 0;
		float lerptime = 2;
		while (jugador_obj.transform.position != Vector3.zero)
		{
			actuallerptime += Time.deltaTime;
			float perc = actuallerptime / lerptime;
			jugador_obj.transform.position = Vector3.Lerp(origen, Vector3.zero, perc);
		}
		if (!jugador_obj.activeSelf)
		{
			jugador_obj.SetActive(true);
		}
	}
	public void resetear_propiedades_luz_jugador()
	{
		jugador_anim_aux.desactivarluz();
		luz_jugador.spotAngle = 80;
		luz_jugador.intensity = 170;
		luz_jugador.range = 30;
		GameObject aux_limpiador = GameObject.FindGameObjectWithTag("Aura_de_item");
		Destroy(aux_limpiador);
	}
	public void pause_game()
	{
		pause = !pause;
		if (pause)
		{
			Time.timeScale = 0;
			if (controles())
			{
				control_desactivar();
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				MenuCanvas.gameObject.transform.GetChild(1).gameObject.SetActive(true); //menu pausa
			}
		}
		else if (!pause)
		{
			Time.timeScale = 1;
			MenuCanvas.gameObject.transform.GetChild(1).gameObject.SetActive(false); //menu pausa
			control_activar();
		}
	}
	public void tuto(bool booleano)
	{
		tutorial = booleano;

		if (tutorial)
		{
			nivelactual = 0;
		}
		else
		{
			//seleccionar niveles mayores a uno
			nivelactual = 1;
		}
		//SceneManager.LoadScene(1);
	}

	//METODOS Consulta
	public Light obt_luz_jugador()
	{
		return luz_jugador;
	}
	public GameObject jugadoractual()
	{
		return jugador_obj;
	}
	public Camera camara_actual()
	{
		return creador_aux_camara.GetComponent<Camera>();
	}
	public GameObject camara_actual_obj()
	{
		return creador_aux_camara;
	}
	public bool tutorial_estado()
	{
		return tutorial;
	}
	public bool estado_pausa()
	{
		return pause;
	}
	public vidasscrip estado_vidasencript()
	{
		return vidas_encript;
	}

	//Controles en PAUSA
	public void control_activar()
	{
		if (!control && !controles_en_evento)
		{
			control = true;
		}
	}
	public void control_desactivar()
	{
		control = false;
	}
	public bool controles()
	{
		return control;
	}
	//Pausa de Controles durante evento
	public void controlenevento_activar()
	{
		controles_en_evento = true;
		control_desactivar();
	}
	public void controlenevento_desactivar()
	{
		controles_en_evento = false;
		control_activar();
	}
	public bool controlenevento_estado()
	{
		return controles_en_evento;
	}

	///metodos opciones
	public void desplazamiento(bool valor)
	{
		direcciontactil = valor;
	}
	public bool desplazamiento_estado()
	{
		return direcciontactil;
	}
	public void musica_activar(bool valor)
	{
		musica = valor;
		audiocontroller_aux.estado_musica(musica);
		if (SceneManager.GetActiveScene().name == "Menu" && musica)
		{
			audiocontroller_aux.reproducir("Menu_Intro");
		}
		else
		{
			if ((SceneManager.GetActiveScene().name == "Juego" && musica) || SceneManager.GetActiveScene().name == "ZONE_JOBS")
			{
				int int_musica = Random.Range(0, 3);
				switch (int_musica)
				{
					case 0:
						audiocontroller_aux.reproducir("Nivel_1");
						break;
					case 1:
						audiocontroller_aux.reproducir("Nivel_2");
						break;
					case 2:
						audiocontroller_aux.reproducir("Nivel_3");
						break;
				}
			}
		}
	}
	public void sonido_activar(bool valor)
	{
		sonido = valor;
		audiocontroller_aux.estado_sonido(sonido);
	}
	public void cambiar_muerto_estado(bool aux)
	{
		dead = aux;
	}
	public void cambiarscenavalor(int aux)
	{
		escenactual = aux;
	}
	public void cambiarscena()
	{
		StartCoroutine(temporizador_para_cambiar_escena());
	}


	IEnumerator temporizador_para_cambiar_escena()
	{
		yield return new WaitForSeconds(1.7f);
		
		if (SceneManager.GetActiveScene().name == "Juego" || SceneManager.GetActiveScene().name == "ZONE_JOBS") //BORRAR desp de pruebas
		{
			//Previo haber controlado puntajes maximos y subidos a googleplay
			/*
			if(puntos_encript!=null)
			{
				Destroy(puntos_encript);
			}
			if(vidas_encript!=null)
			{
				Destroy(vidas_encript);
			}
			*/
		}
		
		SceneManager.LoadScene(escenactual);
	}
	IEnumerator temporizador_para_nivel_rutina()
	{
		audiocontroller_aux.detener_todo();
		cambiador_fades.anegro();
		yield return new WaitForSeconds(2);
		resetear_propiedades_luz_jugador();
		cargarnivel(nivelactual);
		mover_jugador_a_centro();
		dead = false;
		yield return new WaitForSeconds(1.5f);
		cambiador_fades.ablanco();
		musica_activar(true);
	}


	///al MORIR
	public void resetear_luz()
	{
		creador_aux_luz.GetComponent<Light>().spotAngle = 80;
		creador_aux_luz.GetComponent<Light>().intensity = 170;
		creador_aux_luz.GetComponent<Light>().range = 30;
		GameObject aux_limpiador = GameObject.FindGameObjectWithTag("Aura_de_item");
		Destroy(aux_limpiador);
	}

	IEnumerator temporizador_para_final()
	{
		yield return new WaitForSeconds(2.2f);
		MenuCanvas.gameObject.transform.GetChild(7).gameObject.SetActive(true);
	}

	public void maximo_puntaje_control()
	{
		max_puntaje = PlayerPrefs.GetString("hiscore", "v");
		if (puntos_encript.decript_publico(max_puntaje) < puntos_encript.decript())
		{
			max_puntaje = puntos_encript.encrudo();
			PlayerPrefs.SetString("hiscore", puntos_encript.encrudo());
		}
	}

	public void guardadovidasypuntajeactual()
	{
		if (vidas_encript.vidas_actuales()>0 && nivelactual>22)
		{
			PlayerPrefs.SetString("continue_vidas", vidas_encript.encrudo());
			PlayerPrefs.SetString("continue_puntaje", puntos_encript.encrudo());
			PlayerPrefs.SetInt("continue_nivel", nivelactual);
		}
		else
		{
			PlayerPrefs.SetString("continue_vidas", "z");
			PlayerPrefs.SetString("continue_puntaje", "v");
			PlayerPrefs.SetInt("continue_nivel", 1);
		}
	}
	public void erase_all()
	{
		PlayerPrefs.SetString("continue_vidas", "z");
		PlayerPrefs.SetString("continue_puntaje", "v");
		PlayerPrefs.SetInt("continue_nivel", 1);
		PlayerPrefs.SetString("hiscore", "v");
		
		//Boton continuar
		MenuCanvas_Menu.gameObject.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
		//Canva HISCORE
		MenuCanvas_Menu.gameObject.transform.GetChild(4).gameObject.SetActive(false);
	}
}