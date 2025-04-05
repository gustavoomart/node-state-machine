using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public event Action walkTrigger;
    public event Action idleTrigger;
    public event Action groundedTrigger;
    public event Action fallingTrigger;
    Node rootNode;
    public void Awake() {
        Node walkNode = new Node("walkNode");
        Node idleNode = new Node("idleNode");
        Node groundedNode = new Node("groundedNode", idleNode);
        Node fallingNode = new Node("groundedNode", idleNode);
        rootNode = new Node("rootNode", groundedNode);

        walkTrigger += groundedNode.AddSubNodeTransition(walkNode);
        idleTrigger += groundedNode.AddSubNodeTransition(idleNode);

        groundedTrigger += rootNode.AddSubNodeTransition(fallingNode);
        fallingTrigger += rootNode.AddSubNodeTransition(groundedNode);

        walkNode.OnEnter += () => {
            Debug.Log("Entered on walkNode!");
        };
        fallingNode.OnUpdate += () => {
            Debug.Log("Falling node updating!");
        };
        idleNode.OnExit += () => {
            Debug.Log("Idle node exiting!");
        };
    }

    private void Update() {
        rootNode.Update();

        if (Input.GetKeyDown(KeyCode.W)) {
            walkTrigger?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            idleTrigger?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            fallingTrigger?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            groundedTrigger?.Invoke();
        }
        Debug.Log("Current path: " + rootNode.GetNodePath());
    }
}
