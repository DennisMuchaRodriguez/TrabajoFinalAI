public class Condition : BTNode
{
    private System.Func<bool> condition;
    public Condition(System.Func<bool> condition) => this.condition = condition;
    public override BTNodeStatus Execute() => condition() ? BTNodeStatus.Success : BTNodeStatus.Failure;
}
