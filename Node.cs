using System;
#nullable enable

public class Node {
    #region DEBUG
    // Name is just for debugging purposes it can be removed
    // highly recommend to remove after finished debugging
    // because strings are expensive in memory
    // remove this region and Name from constructor.
    public string Name;
    public string GetNodePath() {
        if (currentSubNode == null)
            return Name;
        return Name + " > " + currentSubNode.GetNodePath();
    } 
    #endregion

    private Node? currentSubNode;
    public Node(string name, Node? initialSubNode = null) {
        this.Name = name;
        if (initialSubNode != null) {
            this.currentSubNode = initialSubNode;
            currentSubNode.OnEnter?.Invoke();
        }
    }

    public Action AddSubNodeTransition(Node destination) {
        return () => {
            if (destination == currentSubNode) return;
            currentSubNode?.OnExit?.Invoke();
            currentSubNode = destination;
            currentSubNode?.OnEnter?.Invoke();
        };
    }
    public Action? OnEnter;
    public Action? OnUpdate;
    public Action? OnExit;
    public void Update() {
        OnUpdate?.Invoke();
        currentSubNode?.Update();
    }


}
