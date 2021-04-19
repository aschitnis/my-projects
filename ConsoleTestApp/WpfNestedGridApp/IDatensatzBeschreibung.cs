using WpfNestedGridApp.datensatz;

namespace WpfNestedGridApp.interfaces
{
    public interface IDatensatzBeschreibung
    {
        CHubXmlMapping GetHubMapping();
        CHubstatDatensatzBeschreibung GetDeserializedHubstat();
    }
}
