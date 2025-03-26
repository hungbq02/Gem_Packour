public interface IInteracable
{
    public string InteractMessage { get; }
    public void Interact(Interactor interactor);

    bool canInteract { get; }

}
