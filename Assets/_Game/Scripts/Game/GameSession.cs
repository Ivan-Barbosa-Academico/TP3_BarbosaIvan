using System;
using UnityEngine;
namespace TPShooter
{
    public sealed class GameSession : MonoBehaviour
    {
        public enum SessionState { Running, Paused, Won, Lost }
        [SerializeField] private PlayerInputReader _input;
        [SerializeField] private Health _playerHealth;
        public SessionState State { get; private set; }
        public event Action Changed;
        private void OnEnable(){_input.PausePressed+=TogglePause;_playerHealth.Died+=Lose;}
        private void Start(){Apply();}
        private void OnDisable(){_input.PausePressed-=TogglePause;_playerHealth.Died-=Lose;Time.timeScale=1;Cursor.lockState=CursorLockMode.None;Cursor.visible=true;}
        private void Apply(){bool running=State==SessionState.Running;Time.timeScale=running?1:0;_input.SetBlocked(!running);Cursor.lockState=running?CursorLockMode.Locked:CursorLockMode.None;Cursor.visible=!running;Changed?.Invoke();}
        public void TogglePause(){if(State==SessionState.Running)State=SessionState.Paused;else if(State==SessionState.Paused)State=SessionState.Running;else return;Apply();}
        public void Win(){if(State!=SessionState.Running)return;if(!_playerHealth.IsAlive){Lose();return;}State=SessionState.Won;Apply();}
        public void Lose(){if(State==SessionState.Won||State==SessionState.Lost)return;State=SessionState.Lost;Apply();}
        public void Restart(){Time.timeScale=1;UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);}
        public void Quit(){Application.Quit();}
    }
}
