public enum BTNodeStatus { Success, Failure, Running }

public abstract class BTNode
{
    public abstract BTNodeStatus Execute();
}