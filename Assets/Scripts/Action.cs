public class Action : BTNode
{
    private System.Action action;
    public Action(System.Action action) => this.action = action;
    public override BTNodeStatus Execute() { action(); return BTNodeStatus.Success; }
}