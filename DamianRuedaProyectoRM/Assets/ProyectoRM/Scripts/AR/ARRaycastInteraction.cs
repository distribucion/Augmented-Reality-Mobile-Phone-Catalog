using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARRaycastInteraction : MonoBehaviour{
    const float ROTATION_SPEED = 0.5f; // Velocidad de rotación del objeto, ajustable según sea necesario

    [SerializeField]
    private ARRaycastManager arRaycastManager;


    // Variables para el funcionamiento de la clase
    private GameObject prefab;  // El prefab que será colocado
    public GameObject placementIndicator; // La referencia visial de la posición del objeto a colocar
    public Camera arCamera;

    private Pose placementPose; // La posición de colocación del objeto
    private bool placementPoseIsValid = false; // Bandera para indicar si la posición de colocación es válida
    private bool is_creating = false; // Bandera para indicar si se está creando un objeto
    private GameObject placedObject = null; // El objeto colocado

    public bool IsCreating{ // Propiedad para obtener el valor de is_creating
        get{
            return is_creating;
        }
    }

    void Start(){
        if(arRaycastManager == null)    arRaycastManager = GetComponent<ARRaycastManager>();
    }

    //  Método para iniciar la creación de un objeto
    //  Recibimos el prefab y cambiamos de estado
    public void StartCreating(GameObject prefab){
        if(is_creating){
            StopCreating();
        }
        is_creating = true;
        this.prefab = prefab;
        SetIndicatorState(false);
    }

    //  Método para detener la creación de un objeto
    //  Puede llamarse para cancelar la creacion o una vez finalizada
    public void StopCreating(){
        is_creating = false;
        this.prefab = null;
        if(placedObject != null){
            Destroy(placedObject);
        }
        SetIndicatorState(false);
    }

    //  Método para gestionar la colocación de un elemento en curso
    public GameObject UpdateCreation(){
        if(this.prefab == null) return null;
        
        //  result solamente sera valido en el momento que el objeto se fije y no antes.
        GameObject result = null;

        //  Actualizamos la posición del indicador y de la pose en funcion de donde apunta el dispositivo
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        //  Si la posicion es valida y tocamos la pantalla colocamos el objeto *EDITOR usa <SPACE>
#if UNITY_EDITOR
        if (placementPoseIsValid && Input.GetKeyDown(KeyCode.Space)){
#else
        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
#endif
            if (placedObject == null){
                placedObject = Instantiate(prefab, placementPose.position, placementPose.rotation);
            }else{
                placedObject.transform.position = placementPose.position;
            }
        }

        //  Mientras que no deje de tocarse la pantalla podemos recolocar y rotar el objeto * EDITOR usa <SPACE>
#if UNITY_EDITOR
        if (placedObject != null && Input.GetKey(KeyCode.Space) ){
            placedObject.transform.position = placementPose.position;
            float rotateHorizontal = ROTATION_SPEED;
#else
        if (placedObject != null && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved){
            float rotateHorizontal = Input.GetTouch(0).deltaPosition.x * ROTATION_SPEED;
#endif
            placedObject.transform.Rotate(0f, -rotateHorizontal, 0f, Space.World);
        }

        if (placedObject != null && Input.touchCount == 1){
            placedObject.transform.position = placementPose.position;
        }

        //  Al soltar fijamos el objeto y dejamos de "crear"
#if UNITY_EDITOR
        if (placedObject != null && Input.GetKeyUp(KeyCode.Space) ){
#else
        if (placedObject != null && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
#endif
            result = placedObject;
            placedObject = null;
            StopCreating();
        }

        //  si el objeto esta fijado dejamos que el GameController sea quien lo maneje e inicialice
        return result;
    }

    void UpdatePlacementPose(){
        //  Se obtiene el punto central de la pantalla y se realiza un raycast para detectar planos en esa posición
        var screenCenter = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        // Si el plano es válido
        if (placementPoseIsValid){
            // La posición del objeto se establece en el primer plano detectado (por profundidad)
            placementPose = hits[0].pose;

             // La rotación del objeto se establece para que mire hacia la dirección horizontal de la cámara
            var cameraForward = arCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    void UpdatePlacementIndicator(){
        // Actualizamos el indicador cuando estemos sobre un plano
        if (placementPoseIsValid){ 
            SetIndicatorState(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }else{
            SetIndicatorState(false);
        }
    }

    public void SetIndicatorState(bool state){
        placementIndicator.SetActive(state); // Se activa o desactiva el indicador de colocación
    }

    //  Devuelve verdadero si se detecta un plano en el centro de la pantalla
    //  Lo utilizaremos al arrancar la app para reconocer el entorno
    public bool DetectEnvironment(){
        var hits = new List<ARRaycastHit>();
        if (arRaycastManager.Raycast(arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f)), hits, TrackableType.Planes)){
            return true;
        }else{
            return false;
        }
    }
}
