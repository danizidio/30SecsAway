using UnityEngine;
using UnityEngine.UI;
public class Cronometro : MonoBehaviour {

    public static Cronometro instance;
    //private GameBehaviour _gb;

	public Text contador;
	
	public float counter;
	public float counterN;

	public bool relogio;
	public bool timeIsOver;

	private float hour;
	public  float minutes;
	public  float seconds;
	public  float miliseconds;
	private float microseconds;

	public float minutesN;
	public float secondsN;
	public float milisecondsN;
	private float microsecondsN;
		
	// Use this for initialization
	void Start ()
    {
        instance = this;
		Time.timeScale = 1;
		relogio = true;
		timeIsOver = false;

		contador.GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(relogio){
		counterN -= Time.deltaTime;
		microsecondsN = Mathf.RoundToInt (counterN*600);

		if (microsecondsN <= 0){
			microsecondsN = 59;
			milisecondsN--;
		}
		if (milisecondsN <= 0){
			milisecondsN = 59;
			secondsN--;
		}
		if (secondsN <= 0){
			secondsN = 59;
			minutesN--;
		}
		if (minutesN < 0) {

			Gameover();
            }


            /*seconds = Mathf.RoundToInt(counter%60);
            if (seconds == 59){
                seconds = 0;
            }

            minutes = Mathf.Floor(counter/60);
            if (minutes == 59){
                minutes = 0;
                hour++;
            }*/

            contador.text = minutesN + ":" + secondsN + ":" + milisecondsN; 

            counter += Time.deltaTime;
		
		microseconds = Mathf.RoundToInt (counter*600);
		
		if (microseconds >= 59){
			microseconds = 0;
			miliseconds++;
		}
		if (miliseconds >= 59){
			miliseconds = 00;
			seconds++;
		}
		if (seconds >= 59){
			seconds = 00;
			minutes++;
		}
		if (minutes >= 59){
			minutes = 00;
			hour++;
		}
	}

}
	public void Gameover(){
		timeIsOver = true;
		relogio = false;
		Time.timeScale = 0;
	}
	public bool ItsOver(){
		return timeIsOver;
	}


}
