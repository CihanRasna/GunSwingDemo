using System;
using System.Collections;
using _Case.Scripts;
using _Case.Scripts.Managers;
using Obi;
using UnityEngine;


public class RopeController : MonoBehaviour
{
    [SerializeField] private float ropeResolution = 1f;
    [SerializeField] private float ropeStretchMultiplier = 1.5f;


    public ObiSolver solver;
    public ObiRope rope;
    public ObiRopeCursor cursor;
    public ObiParticleAttachment gunAttachment;
    public ObiParticleAttachment playerAttachment;

    public UnityEngine.Transform playerAttachmentPosition;
    public UnityEngine.Transform attachedGun;

    private ObiRopeBlueprint blueprint;
    public GameObject tempPinGameObject { private set; get; }

    public enum PinBehaviour
    {
        FollowPlayer = 0,
        FollowRope = 1,
        Nothing = 2
    }

    public PinBehaviour pinBehaviour = PinBehaviour.FollowPlayer;


    #region Initialization

    public IEnumerator InitializeRope(Action completion)
    {
        var level = LevelManager.Instance.currentLevel as Level;

        blueprint = ScriptableObject.CreateInstance<ObiRopeBlueprint>();
        blueprint.thickness = 0.1f;
        blueprint.resolution = ropeResolution;
        blueprint.pooledParticles = 2000;

        var gunPos = gunAttachment.transform.position;
        var modelPos = playerAttachmentPosition.position;
        var direction = (modelPos - gunPos).normalized;
        int filter = ObiUtils.MakeFilter(ObiUtils.CollideWithEverything, 0);

        blueprint.path.Clear();
        blueprint.path.AddControlPoint(gunPos, -direction.normalized, direction.normalized, Vector3.up, 0.1f, 0.1f, 1,
            filter, Color.white, "start");
        blueprint.path.AddControlPoint(modelPos, -direction.normalized, direction.normalized, Vector3.up, 0.1f, 0.1f, 1,
            filter, Color.white, "end");
        blueprint.path.FlushEvents();
        yield return blueprint.Generate();
        rope.ropeBlueprint = blueprint;
        rope.SetFilterCategory(1);

        gunAttachment.attachmentType = ObiParticleAttachment.AttachmentType.Static;
        gunAttachment.particleGroup = rope.blueprint.groups[0];
        gunAttachment.target = attachedGun;

        tempPinGameObject = new GameObject("Pin"); //.CreatePrimitive(PrimitiveType.Cube);
        tempPinGameObject.transform.parent = transform;
        tempPinGameObject.transform.position = transform.position;

        playerAttachment.attachmentType = ObiParticleAttachment.AttachmentType.Static;
        playerAttachment.particleGroup = rope.blueprint.groups[1];
        playerAttachment.target = tempPinGameObject.transform;

        completion?.Invoke();
    }

    #endregion


    #region Cutting

    public void UnpinRope()
    {
        blueprint.path.RemoveControlPoint(blueprint.path.ControlPointCount - 1);
        pinBehaviour = PinBehaviour.Nothing;
    }

    private void SnapPinToRope()
    {
        var element = GetLastElement();
        Vector3 position = rope.GetParticlePosition(element.particle2);
        tempPinGameObject.transform.position = position;
    }

    private ObiStructuralElement GetLastElement()
    {
        var element = rope.GetElementAt(1, out var elementIdx);
        return element;
    }

    public Vector3 GetLastElementPosition(ObiStructuralElement e = null)
    {
        var element = e ?? GetLastElement();
        var elementPos = rope.GetParticlePosition(element.particle2);
        return elementPos;
    }

    #endregion
}