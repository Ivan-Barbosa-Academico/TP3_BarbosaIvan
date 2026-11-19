using UnityEngine;
namespace TPShooter
{
    public sealed class GameMenuPresenter : MonoBehaviour
    {
        [SerializeField] private GameSession _session;
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMPro.TMP_Text _title;
        [SerializeField] private UnityEngine.UI.Button _resume, _restart, _quit;
        private void OnEnable(){_session.Changed+=Refresh;_resume.onClick.AddListener(_session.TogglePause);_restart.onClick.AddListener(_session.Restart);_quit.onClick.AddListener(_session.Quit);}
        private void Start(){Refresh();}
        private void OnDisable(){_session.Changed-=Refresh;_resume.onClick.RemoveListener(_session.TogglePause);_restart.onClick.RemoveListener(_session.Restart);_quit.onClick.RemoveListener(_session.Quit);}
        private void Refresh()
        {
            var state=_session.State;_panel.SetActive(state!=GameSession.SessionState.Running);
            _resume.gameObject.SetActive(state==GameSession.SessionState.Paused);
            _title.text=state==GameSession.SessionState.Won?"ENTRENAMIENTO COMPLETADO":state==GameSession.SessionState.Lost?"ENTRENAMIENTO FALLIDO":"PAUSA";
        }
    }
}
