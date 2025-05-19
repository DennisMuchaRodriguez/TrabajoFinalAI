public class BehaviorTree
{
    private BTNode root;
    public BehaviorTree(BTNode rootNode) => root = rootNode;
    public void Update() => root?.Execute();
}