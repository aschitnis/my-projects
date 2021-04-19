namespace ZeitRechnen.Patterns
{
    public class SingletonViewModelDataContainer 
    {
        private ViewModelTaeglichArbzeitDetails objTaeglichViewModel = new ViewModelTaeglichArbzeitDetails();
        private ViewModelWocheArbzeitDetails objWocheArbeitszeitViewModel = new ViewModelWocheArbzeitDetails();

        private SingletonViewModelDataContainer()
        {

        }

        public ViewModelTaeglichArbzeitDetails GetViewModelTaeglichArbzeit()
        {
            return objTaeglichViewModel ;
        }

        public ViewModelWocheArbzeitDetails GetViewModelWocheArbzeit()
        {
            return objWocheArbeitszeitViewModel;
        }

        private static SingletonViewModelDataContainer instance = new SingletonViewModelDataContainer();
        public static SingletonViewModelDataContainer Instance => instance;
    }
}
