using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CustomRenderQueue : MonoBehaviour {

    public UnityEngine.Rendering.CompareFunction comparison = UnityEngine.Rendering.CompareFunction.Always;

    public bool apply = false;

    void Start() {
        Invoke("UpdateMat", 2);
    }

    void Update() {
        if (apply) {
            UpdateMat();
        }
    }

    private void UpdateMat() {
        Debug.Log("Updated material val");
        CanvasRenderer image = GetComponent<CanvasRenderer>();
        Material existingGlobalMat = image.GetMaterial();
        Material updatedMaterial = new Material(existingGlobalMat);
        updatedMaterial.SetInt("unity_GUIZTestMode", (int)comparison);
        image.SetMaterial(updatedMaterial, 0);
        apply = false;
    }
}