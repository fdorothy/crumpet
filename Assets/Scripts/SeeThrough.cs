using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class SeeThrough : MonoBehaviour
{
    public enum AxisType
    {
        X, Y, Z
    }

    protected MeshRenderer meshRenderer;
    protected Player player;
    protected bool seeThrough = false;
    protected bool oldSeeThrough = false;
    public bool isPoint = false;
    protected Tweener tweener;
    public AxisType axisType = AxisType.X;
    protected Material[] originalMaterials;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterials = meshRenderer.materials;
    }

    void Update()
    {
        if (player == null || meshRenderer == null)
        {
            FindPlayer();
            return;
        }
        Vector3 normal = isPoint ? Camera.main.transform.forward : getForwardAxis();
        seeThrough = IsPlaneBetweenPoints(transform.position, normal, Camera.main.transform.position, player.transform.position);
        if (oldSeeThrough != seeThrough)
        {
            if (tweener != null && tweener.IsActive())
                tweener.Kill();
            DoFade();
            oldSeeThrough = seeThrough;
        }
    }

    Vector3 getForwardAxis()
    {
        switch (axisType)
        {
            case AxisType.X: return transform.right;
            case AxisType.Y: return transform.up;
            case AxisType.Z: return transform.forward;
            default: return transform.forward;
        }
    }

    void FindPlayer()
    {
        player = FindObjectOfType<Player>();
    }

    bool IsPlaneBetweenPoints(Vector3 planeCenter, Vector3 planeNormal, Vector3 p1, Vector3 p2)
    {
        float s1 = Vector3.Dot(p1 - planeCenter, planeNormal);
        float s2 = Vector3.Dot(p2 - planeCenter, planeNormal);
        return !(s1 > 0.0f && s2 > 0.0f || s1 < 0.0f && s2 < 0.0f);
    }

    void DoFade()
    {
        if (seeThrough)
        {
            //meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            //meshRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            //tweener = meshRenderer.material.DOFade(0.0f, 1.0f);
            List<Material> newMaterials = new List<Material>();
            for (int i = 0; i < originalMaterials.Length; i++)
                newMaterials.Add(WallManager.singleton.seeThroughMaterial);
            meshRenderer.SetMaterials(newMaterials);
        } else
        {
            //meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            //meshRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            //tweener = meshRenderer.material.DOFade(1.0f, 1.0f);
            List<Material> newMaterials = new List<Material>();
            for (int i = 0; i < originalMaterials.Length; i++)
                newMaterials.Add(originalMaterials[i]);
            meshRenderer.SetMaterials(newMaterials);
        }
    }
}
