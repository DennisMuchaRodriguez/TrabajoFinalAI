using System.Collections.Generic;

public class Selector : BTNode
{
    private List<BTNode> children;
    public Selector(List<BTNode> nodes) => children = nodes;

    public override BTNodeStatus Execute()
    {
        foreach (var child in children)
            if (child.Execute() == BTNodeStatus.Success)
                return BTNodeStatus.Success;
        return BTNodeStatus.Failure;
    }
}