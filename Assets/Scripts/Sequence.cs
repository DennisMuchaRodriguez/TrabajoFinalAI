using System.Collections.Generic;

public class Sequence : BTNode
{
    private List<BTNode> children;
    public Sequence(List<BTNode> nodes) => children = nodes;

    public override BTNodeStatus Execute()
    {
        foreach (var child in children)
            if (child.Execute() == BTNodeStatus.Failure)
                return BTNodeStatus.Failure;
        return BTNodeStatus.Success;
    }
}