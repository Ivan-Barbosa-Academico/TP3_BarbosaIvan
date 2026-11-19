namespace TPShooter
{
    public interface IInteractable
    {
        string Prompt { get; }
        bool Available { get; }
        void Interact();
    }
}
