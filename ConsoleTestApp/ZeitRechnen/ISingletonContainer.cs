namespace ZeitRechnen
{
    public interface ISingletonContainer
    {
        IViewModel GetViewModelTaeglichArbzeit();
        IViewModel GetViewModelWocheArbzeit();
    }
}
