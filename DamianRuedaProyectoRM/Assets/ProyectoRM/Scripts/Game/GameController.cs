using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

[System.Serializable]
public class GameAsset
{
    public string name = "";
    public GameObject prefab;
}

[System.Serializable]
public class StateEvent : UnityEvent<int> {}

// Delegado para el cambio de estado de interacción
public delegate void InteractionStateChanged(bool v);

public class GameController : MonoBehaviour
{
    // Enumerador para los estados del juego
    public enum GameState
    {
        Idle = 0,
        Discovering = 1,
        Creation = 2,
        View = 3,
        Edit = 4
    }

    // Configuración de los assets
    [Header("Configuración de los assets")]
    public GameAsset[] gameAssets;

    // Componente para la interacción con rayos AR
    [Header("Componente para la interacción con rayos AR")]
    public ARRaycastInteraction aRRaycastInteraction;

    // Evento que se llama cuando cambia el estado del juego
    [Header("Eventos")]
    public StateEvent onStateChanged;

    // Evento que se llama cuando cambia el estado de interacción
    public event InteractionStateChanged onInteractionStateChanged = null;

    // Variables privadas del controlador de juego
    private GameObject placedObject;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameState currentState = GameState.Idle;
    private bool isPlacementValid = false;
    private int currentAsset = 0;
    private bool stateChanged = false;
    private ARObject selectedObject;
    private bool isOverUI;
    private ARRaycastManager aRRaycastManager;


    #region mono
        // Al arrancar la app nos ponemos en modo reconocimiento del entorno
        void Start(){
            SetState(GameState.Discovering);
        }

        // el resto de updates realizaremos distintas acciones en función del estado en el que estemos 
        void Update(){
            switch (currentState){
                case GameState.Idle:
                    // Nada que hacer
                    break;
                case GameState.Discovering:
                    UpdateDiscovering(stateChanged);
                    break;
                case GameState.Creation:
                    UpdateCreation(stateChanged);
                    break;
                case GameState.View:
                    UpdateView(stateChanged);
                    break;
                case GameState.Edit:
                    UpdateEdit(stateChanged);
                    break;
            }
            stateChanged = false;
        }
    #endregion

    #region custom_actions
        // Actualiza el estado de búsqueda de superficies para la creación de un nuevo objeto
        private void UpdateDiscovering(bool stateChanged){
            //  stateChange se llamara solamente la primera vez que se hace un update de un estado
            //  Lo usamos para inicializar el estado
            if (stateChanged){
                DeselectCurrent();
                aRRaycastInteraction.StopCreating();
                onInteractionStateChanged?.Invoke(false);
            }

            // Comprueba si hay algún plano, para poder comenzar a usar la app
            isPlacementValid = aRRaycastInteraction.DetectEnvironment();
            if (isPlacementValid){
                SetState(GameState.View);
            }
        }

        // Se encarga de la creacion de objetos
        private void UpdateCreation(bool stateChanged){
            if (stateChanged){
                DeselectCurrent();
                currentAsset = -1;
                onInteractionStateChanged?.Invoke(false);
            }

            //  Cuando estemos en este modo y se seleccione el tipo de elemento a crear, instanciamos su prefab donde apuntamos
            if (!aRRaycastInteraction.IsCreating && currentAsset >= 0){
                aRRaycastInteraction.StartCreating(gameAssets[currentAsset].prefab);
            }

            //  Actualizamos la creación hasta que el Raycast nos dice que el objeto se ha fijado y nos lo devuelve
            GameObject placedObject = null;
            if (aRRaycastInteraction.IsCreating && (placedObject = aRRaycastInteraction.UpdateCreation()) != null){

                 // En ese caso inicializamos el objeto AR y lo seleccionamos
                ARObject createdObject = placedObject.GetComponent<ARObject>();
                createdObject.Init(gameAssets[currentAsset].name, this);
                SetSelection(createdObject);
            }

            if (Input.touchCount > 0) 
            {
            Touch TouchOne = Input.GetTouch(0);
            if (TouchOne.phase == TouchPhase.Began) 
            {
                var touchPosition = TouchOne.position;
                isOverUI = isTapOverUI(touchPosition);
            }
            if (TouchOne.phase == TouchPhase.Moved) 
            {
                if (aRRaycastManager.Raycast(TouchOne.position, hits, TrackableType.Planes)) 
                {
                    Pose hitPose = hits[0].pose;
                    if (!isOverUI) 
                    {
                        transform.position = hitPose.position;
                    }
                }
                   

            }

            
            }
        }

    private bool isTapOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touchPosition.x, touchPosition.y);

        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);

        return result.Count > 0;

   
    }

    // Este modo será pasivo, cuando no estemos haciendo nada concreto
    private void UpdateView(bool stateChanged){

            if(stateChanged){
                DeselectCurrent();
                onInteractionStateChanged?.Invoke(true);
                aRRaycastInteraction.StopCreating();
            }

        }

        // Este método requiere un objeto seleccionado
        private void UpdateEdit(bool stateChanged){

            if(stateChanged){
                aRRaycastInteraction.StopCreating();
                onInteractionStateChanged?.Invoke(true);
            }

            // Cuando hayamos terminado volvemos al modo vista
            if(selectedObject == null)  SetState(GameState.View);
        }
    #endregion

    #region ui_feedback
        // Este método establece el estado del juego en el que estamos (como int para llamarlo desde la UI)
        public void SetState(int state){
            SetState((GameState)state);
        }

        // Este método establece el estado del juego en el que estamos
        void SetState(GameState state){
            // Si el estado es igual al estado actual, no hace nada
            if(state == currentState)   return;

            // De lo contrario, establece el nuevo estado, marca que el estado y lanzamos el evento de cambio
            currentState = state;
            stateChanged = true;
            onStateChanged?.Invoke((int)currentState);
        }

        // Este método selecciona el tipo de objeto que queremos crear para el modo creación
        public void SelectAsset(int index){
            if(index < 0 || index >= gameAssets.Length){
                SetState(GameState.View);
            return;
            }
            aRRaycastInteraction.StopCreating();
            currentAsset = index;
        }

        // Este método establece de un objeto AR. Podremos eliminarlo mediante UI
        public void SetSelection(ARObject selected){
            if(selected == null) return;

            DeselectCurrent();
            selectedObject = selected;
            selectedObject.Select();
            SetState(GameState.Edit);
        }

        //  Quita la selección del objeto actual
        void DeselectCurrent(){
            if(selectedObject != null) selectedObject.Deselect();
            selectedObject = null;
        }

        // Elimina el elmento seleccionado desde la UI
        public void DeleteSelection(){
            if(selectedObject == null) return; // no hay nada que eliminar
            selectedObject.DeInit();
            DeselectCurrent();
        }
    #endregion
}
