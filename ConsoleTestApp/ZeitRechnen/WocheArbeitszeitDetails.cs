using System;
using System.Collections.Generic;

namespace ZeitRechnen
{
    public class ModelWocheArbeitszeitDetails : IViewModel
    {
        public string GesetzlicheWoArbeitszeit { get; set; }
        public string MitarbeiterWoArbeitszeit { get; set; }
        public int FeiertageProWoche { get; set; }
        public string ArbeitszeitDifferenz { get; set; }
        public string KalenderWoche { get; set; }
        public string wocheStartDatum { get; set; }
        public string wocheEndDatum { get; set; }
        public ModelWocheArbeitszeitDetails()
        {
            GesetzlicheWoArbeitszeit = "00:00";
            MitarbeiterWoArbeitszeit = "00:00";
            FeiertageProWoche = 0;
            ArbeitszeitDifferenz = "0";
            wocheStartDatum = "";
            wocheEndDatum = "";
            KalenderWoche = Convert.ToString(CommonFunctions.berechneKalenderwoche(DateTime.Now) );
        }
    }

    public class ViewModelWocheArbzeitDetails
    {
        private List<ModelWocheArbeitszeitDetails> _listWocheArbZeitDetailsModel;

        public List<ModelWocheArbeitszeitDetails> ListWocheArbZeitDetailsModel
        {
            get {
                if (_listWocheArbZeitDetailsModel == null)
                {
                    _listWocheArbZeitDetailsModel = new List<ModelWocheArbeitszeitDetails>();
                }
                    return _listWocheArbZeitDetailsModel;
                }
            set { _listWocheArbZeitDetailsModel = value; }
        }

       public ModelWocheArbeitszeitDetails GetModelWocheArbeitszeitDetails()
        {
            return new ModelWocheArbeitszeitDetails();
        }
    }
}
