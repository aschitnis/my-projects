namespace WpfNestedGridApp.xml
{
    public interface IBundService
    {
        string sOutfile { get; set; }
        bool IstErrorVorhanden { get; set; }

        bool IstDateiVorhanden();
        void Deserialize();
    }
}
