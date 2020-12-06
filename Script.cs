using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class NewBehaviourScript : MonoBehaviour
{
	public List<Sprite> SpriteList;

    // Start is called before the first frame update
    void Start()
    {
        GameObject enunciat = new GameObject("_textGO");
        GameObject boto1 = new GameObject("boto1");
        GameObject boto2 = new GameObject("boto2");
        GameObject boto3 = new GameObject("boto3");

        SpriteList.Add(Resources.Load<Sprite>("1"));

        enunciat.transform.SetParent(this.transform);
        enunciat.transform.position = new Vector3(460,172,0);

        Text _text = enunciat.AddComponent<Text>();

        string[] numeros = {"zero", "u", "dos", "tres", "quatre", "cinc", "sis", "set", "vuit", "nou"};
        int index= prepararText(_text, numeros);
        int resposta = prepararResposta();
        StartCoroutine(sequenciadorPartida(enunciat,boto1,boto2,boto3,index,resposta));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Prepara els atributs del text
    int prepararText(Text _text, string[] numeros)
    {
    	System.Random random = new System.Random();
        int index = random.Next(numeros.Length);
        _text.text = numeros[index];
        _text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        _text.fontSize = 30;
        return index;
    }

    //Prepara quina serà la resposta correcta
    int prepararResposta()
    {
    	System.Random random = new System.Random();
    	int resposta = random.Next(1,3);
    	return resposta;
    }

    //Animacio d'entrada i sortida
    IEnumerator fader(GameObject objecte, bool fadeIn, float duracio)
    {
    	float comptador = 0f;

    	//Valors depenents de inOrOut
    	float a, b;
    	if (fadeIn)
    	{
    		a=0;
    		b=1;
    	}
    	else
    	{
    		a=1;
    		b=0;
    	}

    	Color colorActual = Color.clear;
    	Image image = objecte.GetComponent<Image>();
    	Text text = objecte.GetComponent<Text>();
    	int mode = 0;

    	if (text != null)
    	{
    		colorActual = text.color;
    		mode = 0;
    	}
    	else if (image != null)
    	{
    		colorActual = image.color;
    		mode = 1;
    	}

    	while (comptador < duracio)
    	{
    		comptador += Time.deltaTime;
    		float alpha = Mathf.Lerp(a, b, (comptador/duracio));
    		
    		switch (mode)
    		{
    			case 0:
    				text.color = new Color(colorActual.r, colorActual.g, colorActual.b, alpha);
    				break;
    			case 1:
    				image.color = new Color(colorActual.r, colorActual.g, colorActual.b, alpha);
    				break;
    			default:
    				break;
    		}

    		yield return null;
    	}
    }

    //Espera tants segons com sigui necessari
    IEnumerator waiter(int temps)
    {
    	yield return new WaitForSecondsRealtime(temps);
    }

    //Mostra l'enunciat segons els parametres establerts
    IEnumerator mostradorEnunciat(GameObject enunciat)
    {
    	yield return StartCoroutine(fader(enunciat, true, 2f));
    	Debug.Log("FADED IN");
    	yield return StartCoroutine(waiter(2));
    	Debug.Log("WAITED");
    	yield return StartCoroutine(fader(enunciat, false, 2f));
    	Debug.Log("FADED OUT");
    }

    //Sequencia les subrutines per a l'execució del joc
    IEnumerator sequenciadorPartida(GameObject enunciat, GameObject boto1, 
    	GameObject boto2, GameObject boto3, int index, int resposta)
    {
    	yield return StartCoroutine(mostradorEnunciat(enunciat));
    	yield return StartCoroutine(generadorBotons(boto1, boto2, boto3, index, resposta));
    	yield return StartCoroutine(gestioResposta(boto1, boto2, boto3, resposta));
    }

    //Genera els botons seguint els paràmetres especificats i els dona un valor i determina quina
    //serà la resposta correcta

    // PROBLEMES: Faltaria validar que no es repeteixin els valors randoms de ambdues respostes
    // incorrectes

    IEnumerator generadorBotons(GameObject boto1, GameObject boto2, GameObject boto3, int index, int resposta)
    {
    	Sprite[] sprites = Resources.LoadAll<Sprite>("sprites");
    	Debug.Log(sprites.Length);

        boto1.transform.SetParent(this.transform);
        boto1.transform.position = new Vector3(250, 172, 0);
        
        boto2.transform.SetParent(this.transform);
        boto2.transform.position = new Vector3(450, 172, 0);
        
        boto3.transform.SetParent(this.transform);
        boto3.transform.position = new Vector3(650, 172, 0);

        Button _boto1 = boto1.AddComponent<Button>();
        Image _image1 = boto1.AddComponent<Image>();

        System.Random random = new System.Random();
        int respostaIncorrecta = resposta;

        if (resposta == 1)
        {
        	_image1.sprite = sprites[index];	
        }
        else
        {

    		respostaIncorrecta = random.Next(0,9);
    		_image1.sprite = sprites[respostaIncorrecta];
    		respostaIncorrecta = resposta;
        }
        
        yield return StartCoroutine(fader(boto1, true, 2f));

        Button _boto2 = boto2.AddComponent<Button>();
        Image _image2 = boto2.AddComponent<Image>();
        
        if (resposta == 2)
        {
        	_image2.sprite = sprites[index];	
        }
        else
    	{		
    		respostaIncorrecta = random.Next(0,9);
    		_image2.sprite = sprites[respostaIncorrecta];
    		respostaIncorrecta = resposta;
        }

        yield return StartCoroutine(fader(boto2, true, 2f));

        Button _boto3 = boto3.AddComponent<Button>();
        Image _image3 = boto3.AddComponent<Image>();
        
        if (resposta == 3)
        {
        	_image3.sprite = sprites[index];	
        }
        else
        {
    		respostaIncorrecta = random.Next(0,9);
    		_image3.sprite = sprites[respostaIncorrecta];
    		respostaIncorrecta = resposta;
        }

        yield return StartCoroutine(fader(boto3, true, 2f));
    }

    //Gestiona les respostes que s'introdueixen via click amb el ratolí
    // PROBLEMES: No he pogut acabar aquesta part però caldria trobar la manera de
    // executar una determinada subrutina establerta posteriorment per a tractar els
    // gameobjects dels botons de la manera indicada a l'enunciat

    IEnumerator gestioResposta(GameObject boto1, GameObject boto2, GameObject boto3, int correcta)
    {
    	int counter = 0;
    	string botoCorrecte = String.Format("boto{0}", correcta);

    	Button _boto1 = boto1.GetComponent<Button>();
    	Button _boto2 = boto2.GetComponent<Button>();
    	Button _boto3 = boto3.GetComponent<Button>();

    	string[] noms = {"boto1", "boto2", "boto3"};

    	bool guanyar = false;

    	_boto1.onClick.AddListener(delegate{checkerRespostes(boto1, noms[0], botoCorrecte, counter, guanyar);});
    	_boto2.onClick.AddListener(delegate{checkerRespostes(boto2, noms[1], botoCorrecte, counter, guanyar);});
    	_boto3.onClick.AddListener(delegate{checkerRespostes(boto3, noms[2], botoCorrecte, counter, guanyar);});

    	Debug.Log(guanyar);

    	yield return StartCoroutine(gestioVictoria(boto1, boto2, boto3, counter, guanyar));

    	yield return null;
    }

    // Funcio per als botons que canvia el color i la aparença (presència o no) donat un comptador
    // d'errades

    void checkerRespostes(GameObject boto, string nom, string correcte, int counter, bool guanyar)
    {
    	if (counter == 0)
    	{
    		if (nom == correcte)
    		{
    			Button _boto = boto.GetComponent<Button>();
    			_boto.GetComponent<Image>().color = Color.green;
    			Debug.Log("correcte, comptador 0");
    			guanyar = true;
    			// gestioVictoria(...)

    		}
    		else
    		{
    			Button _boto = boto.GetComponent<Button>();
    			_boto.GetComponent<Image>().color = Color.red;
    			Debug.Log("incorrecte, comptador 0");
    			counter = counter + 1;
    			// gestioVictoria(...)

    		}
    	}
    	else if (counter == 1)
    	{
    		if (nom == correcte)
    		{
    			Button _boto = boto.GetComponent<Button>();
    			_boto.GetComponent<Image>().color = Color.green;
    			Debug.Log("correcte, comptador 1");
    			guanyar = true;
    			// gestioVictoria(...)

    		}
    		else
    		{
    			Button _boto = boto.GetComponent<Button>();
    			_boto.GetComponent<Image>().color = Color.red;
    			Debug.Log("incorrecte, comptador 1");
    			counter = counter + 1;
    			// gestioVictoria(...)
    		}
    	}
    	else
    	{
    		//...
    	}
    }

    // Determinant si s'ha guanyat i el nombre d'errades es faria el fading dels botons
    // adequats i es procediria a seguir amb la partida o a seguir amb un nou intent

    IEnumerator gestioVictoria(GameObject boto1, GameObject boto2, GameObject boto3, int counter, bool victoria)
    {
    	if (victoria == true && counter == 0)
    	{
    		yield return StartCoroutine(fader(boto1, false, 2f));
    		yield return StartCoroutine(fader(boto2, false, 2f));
    		yield return StartCoroutine(fader(boto3, false, 2f));
    	}

    	if (victoria == false && counter == 1)
    	{
    		// ...
    	}

    	if (victoria == false && counter == 2)
    	{
    		// ...
    	}
    }
}



