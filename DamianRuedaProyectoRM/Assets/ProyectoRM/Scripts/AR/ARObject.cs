using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ARObject : MonoBehaviour {
    
    [Header("Configuración del objeto")]
    [SerializeField] private Collider obj_collider;
    public string object_name = "";
    
    [Header("Eventos del objeto")]
    public UnityEvent on_select;
    public UnityEvent on_deselect;
    
    private GameController gameController;
    
    private bool is_register = false;

    #region MonoBehaviours
        // Al hacer click en la pantalla sobre un objeto con collider se llama a este método
        void OnMouseDown() {
            gameController.SetSelection(this);
        }
    #endregion

    #region Management
        // Inicializa el objeto cuando es creado
        public void Init(string name, GameController gameController) {
            this.object_name = name;
            this.gameController = gameController;
            Register(gameController); // Se registra el objeto en el controlador principal
        }

        // Elimina el objeto
        public void DeInit() {
            Unregister(this.gameController); // Se desregistra el objeto del controlador principal
            Destroy(this.gameObject);
        }

        // Subscribe los eventos para este objeto
        void Register(GameController gameController) {
            if (!is_register) {
                gameController.onInteractionStateChanged += OnInteractionStateChanged; // Se suscribe al evento de cambio de estado del controlador principal
            }
            is_register = true;
        }

        // Elimina el registro de eventos
        void Unregister(GameController gameController) {
            if (is_register) {
                gameController.onInteractionStateChanged -= OnInteractionStateChanged; // Se elimina la suscripción al evento de cambio de estado del controlador principal
            }
            is_register = false;
        }

        // Se llama cuando el estado de GameController cambia según si podemos seleccionar elementos
        public void OnInteractionStateChanged(bool state) {
            if (obj_collider != null) {
                obj_collider.enabled = state;
            }
        }

        // Se llama cuando el objeto es seleccionado
        public void Select() {
            on_select?.Invoke();
        }

        // Se llama cuando el objeto es deseleccionado
        public void Deselect() {
            on_deselect?.Invoke();
        }
    #endregion
}
