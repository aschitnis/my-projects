namespace WpfNestedGridApp
{
    public class CHubXmlMapping
    {
        public CParams HSatzVersandPartner { get; set; }
        public CParams HSatzErstelldatum { get; set; }
        public CParams ASatzSSCC { get; set; }
        public CParams ASatzEmpfangsPartner { get; set; }
        public CParams ASatzDatum { get; set; }
        public CParams ASatzBemerkung { get; set; }
        public CParams ASatzKey { get; set; }

        public CHubXmlMapping()
        {
            HSatzVersandPartner = new CParams();
            HSatzErstelldatum = new CParams();
            ASatzSSCC = new CParams();
            ASatzEmpfangsPartner = new CParams();
            ASatzDatum = new CParams();
            ASatzBemerkung = new CParams();
            ASatzKey = new CParams();
        }
    }

    public class CParams
    {
        public string Start { get; set; }
        public string Length { get; set; }

        public CParams()
        {
            
        }
    }
}
