using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LoadLevelScript : MonoBehaviour {
	
	public void LoadMainMenu(){

        UnityEngine.SceneManagement.SceneManager.LoadScene( "MainMenu");
	}
	
	public void LoadJoystickEvent(){
        UnityEngine.SceneManagement.SceneManager.LoadScene( "Joystick-Event-Input");
	}
	
	public void LoadJoysticParameter(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Joystick-Parameter");
	}
	
	public void LoadDPadEvent(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("DPad-Event-Input");
	}
	
	public void LoadDPadClassicalTime(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("DPad-Classical-Time");
	}
	
	public void LoadTouchPad(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("TouchPad-Event-Input");
	}
	
	public void LoadButton(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Button-Event-Input");
	}
	
	public void LoadFPS(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("FPS_Example");
	}
	
	public void LoadThird(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("ThirdPerson+Jump");
	}
	
	public void LoadThirddungeon(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("ThirdPersonDungeon+Jump");
	}
}
