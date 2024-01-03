using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIController : MonoBehaviour{

    GameController.GameState gameState;

    //  Tendremos una UI por estado
    public Canvas discovering_canvas;
    public Canvas creation_canvas;
    public Canvas edit_canvas;
    public Canvas view_canvas;

    #region mono
        void Start(){
            OnStateChange(1);
        }
    #endregion

    #region state
        //  Recibimos el evento de GameController y encendemos la UI que corresponda
        public void OnStateChange(int state){
            gameState = (GameController.GameState)state;

            switch(gameState){
                case GameController.GameState.Discovering:
                    discovering_canvas.enabled = true;
                    creation_canvas.enabled = false;
                    edit_canvas.enabled = false;
                    view_canvas.enabled = false;
                    break;
                case GameController.GameState.Creation:
                    discovering_canvas.enabled = false;
                    creation_canvas.enabled = true;
                    edit_canvas.enabled = false;
                    view_canvas.enabled = false;
                    break;
                case GameController.GameState.Edit:
                    discovering_canvas.enabled = false;
                    creation_canvas.enabled = false;
                    edit_canvas.enabled = true;
                    view_canvas.enabled = false;
                    break;
                case GameController.GameState.View:
                    discovering_canvas.enabled = false;
                    creation_canvas.enabled = false;
                    edit_canvas.enabled = false;
                    view_canvas.enabled = true;
                    break;
                case GameController.GameState.Idle:
                default:
                    discovering_canvas.enabled = false;
                    creation_canvas.enabled = false;
                    edit_canvas.enabled = false;
                    view_canvas.enabled = false;
                    break;
            }
        }
    #endregion
    
}
