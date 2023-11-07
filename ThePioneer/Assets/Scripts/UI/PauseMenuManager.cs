using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MV {
    public class PauseMenuManager : MonoBehaviour {
        
        private bool isGamePaused = false;
        public GameObject GamePauseUI;

        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;

        public static PauseMenuManager Instance { get; private set; }

        //   public event EventHandler OnGamePaused;
        //  public event EventHandler OnGameUnpaused;

        private void Awake() {
            resumeButton.onClick.AddListener(() => {
                TogglePauseGame();
            });
            mainMenuButton.onClick.AddListener(() => {
                Loader.Load(Loader.Scene.MainMenuScene);
            });
        }

        private void Start() {
            InputHandler.Instance.OnPauseAction += InputHandler_OnPauseAction;
            Hide();
           
        }

        private void InputHandler_OnPauseAction(object sender, EventArgs e) {
            TogglePauseGame();
        }

        public void TogglePauseGame() {
            isGamePaused = !isGamePaused;
            if(isGamePaused) {
                Time.timeScale = 0f;
                Show();

            } else {
                Time.timeScale = 1f;
                Hide();

            }
            
        }

        private void Show() {
            GamePauseUI.SetActive(true);
        }
        private void Hide() {
            GamePauseUI.SetActive(false);
        }

      
    }
}