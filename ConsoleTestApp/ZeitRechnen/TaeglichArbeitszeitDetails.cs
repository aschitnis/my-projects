using System;
using System.Collections.Generic;

namespace ZeitRechnen
{
    public class TaeglichArbeitszeitDetails : IViewModel
    {
        public string VonStundenMinuten { get; set; }
        public string BisStundenMinuten { get; set; }

        public string VonBisArbeitszeitSummiert { get; set; }

        public TaeglichArbeitszeitDetails()
        {
            VonStundenMinuten = "00:00";
            BisStundenMinuten = "00:00";
        }
    }
    

    public class ViewModelTaeglichArbzeitDetails
    {
        public List<TaeglichArbeitszeitDetails> ListTaeglichArbzeitModel { get; set; }
        public TimeSpan tsWochenArbeitszeit { get; set; }
        public ViewModelTaeglichArbzeitDetails()
        {
            ListTaeglichArbzeitModel = new List<TaeglichArbeitszeitDetails>();
            tsWochenArbeitszeit = new TimeSpan();
        }


        public TaeglichArbeitszeitDetails GeneriereTaeglichArbeitszeitModelData(string _vonminutes, string _bisminutes, string _bishours, string _vonhours)
        {
            TaeglichArbeitszeitDetails obj = new TaeglichArbeitszeitDetails();
            obj.VonBisArbeitszeitSummiert = CommonFunctions.AddHoursAndMinutes(_vonminutes, _bisminutes, _bishours, _vonhours);
            string sVonHours = "00";
            string sBisHours = "00";
            string sVonMinuten = "00";
            string sBisMinuten = "00";

            if (_vonminutes.ToString().Length < 2)
                sVonMinuten = "0" + _vonminutes;
            else
                sVonMinuten = _vonminutes;

            if (_vonhours.ToString().Length < 2)
                sVonHours = "0" + _vonhours;
            else
                sVonHours = _vonhours;

            if (_bisminutes.ToString().Length < 2)
                sBisMinuten = "0" + _bisminutes;
            else
                sBisMinuten = _bisminutes;

            if (_bishours.ToString().Length < 2)
                sBisHours = "0" + _bishours;
            else
                sBisHours = _bishours;

            obj.VonStundenMinuten = sVonHours + ":" + sVonMinuten;
            obj.BisStundenMinuten = sBisHours + ":" + sBisMinuten;
            ListTaeglichArbzeitModel.Add(obj);

            return obj;
        }

        // actual Total number of working hours & minutes, the employee worked on.  
        public string BerechnenAktuelleWochenArbeitszeit()
        {
            int totalHours = 0;
            int totalminutes = 0;
            // loop through List<Model>()
            foreach(TaeglichArbeitszeitDetails model in ListTaeglichArbzeitModel)
            {
                // add the hours and minutes seperately in 2 integer variables.
                string[] sHrsMinutes = model.VonBisArbeitszeitSummiert.Split(':');
                totalHours += int.Parse(sHrsMinutes[0]);
                totalminutes += int.Parse(sHrsMinutes[1]);
            }

            // convert the summedup minutes into hours and minutes
            // add the resulted hours into the total hours and the left over minutes into minutes.
            while (totalminutes >= 60)
            {
                totalminutes = totalminutes - 60;
                totalHours++;
            }

            // format to string "00:00" and return.
            string strTotalHours = "00";
            string strTotalMinutes = "00";

            if (totalHours.ToString().Length < 2)
                strTotalHours = "0" + totalHours.ToString();
            else
                strTotalHours = totalHours.ToString();

            if (totalminutes.ToString().Length < 2)
                strTotalMinutes = "0" + totalminutes.ToString();
            else
                strTotalMinutes = totalminutes.ToString();

            return strTotalHours.ToString() + ":" + strTotalMinutes.ToString();
        }
    }

    public class CommonFunctions
    {
        public static int berechneKalenderwoche(DateTime datum)
        {
            int kalenderwoche = (datum.DayOfYear / 7) + 1;
            if (kalenderwoche == 53) kalenderwoche = 1;
            return kalenderwoche;
        }

        public static string ConvertTimespanToHoursAndMinutes(TimeSpan ts)
        {
            double dFixWochenArbeitszeit = 38.5;
            string sResultArbeitszeit = "00:00";

            if (ts.TotalHours.CompareTo(dFixWochenArbeitszeit) == 0)
            {
                sResultArbeitszeit = "38:30";
            }
            else
            {
                string sWochenArbeitszeit = Convert.ToString(ts.TotalHours);

                string[] sArrayminuten = sWochenArbeitszeit.Replace(",", ":").Split(':');

                if (sArrayminuten.Length < 2)
                {
                    sResultArbeitszeit = sArrayminuten[0] + ":00";
                }
                else
                {
                    switch (sArrayminuten[1])
                    {
                        case "75":
                            sResultArbeitszeit = sArrayminuten[0] + ":45";
                            break;
                        case "25":
                            sResultArbeitszeit = sArrayminuten[0] + ":15";
                            break;
                        case "50":
                            sResultArbeitszeit = sArrayminuten[0] + ":30";
                            break;
                    }
                }
            }

            return sResultArbeitszeit;
        }

        public static string AddHoursAndMinutes(string _vonminutes, string _bisminutes, string _bishours, string _vonhours)
        {
            int totalhours = int.Parse(_vonhours) + int.Parse(_bishours);
            int totalmts = int.Parse(_vonminutes) + int.Parse(_bisminutes);

            string strTotalHours = "00";
            string strTotalMinutes = "00";

            while (totalmts >= 60)
            {
                totalmts = totalmts - 60;
                totalhours++;
            }
                        
            if (totalhours.ToString().Length < 2)
                strTotalHours = "0" + totalhours.ToString();
            else
                strTotalHours = totalhours.ToString();

            if (totalmts.ToString().Length < 2)
                strTotalMinutes = "0" + totalmts.ToString();
            else
                strTotalMinutes = totalmts.ToString();

            return strTotalHours.ToString() + ":" + strTotalMinutes.ToString();
        }

        public static TimeSpan GesetzlicheWochenArbeitszeitMitFeiertageBerechnen(int anzahlFeiertage = 0)
        {
            TimeSpan tsRegelWochenArbeitszeit = new TimeSpan(38, 30, 0);
            TimeSpan tsTagesArbeitszeit = new TimeSpan(7, 45, 0);
            TimeSpan tsAktuellWochenArbeitszeit = new TimeSpan();
            TimeSpan tsFeiertagsZeit = new TimeSpan();

            switch (anzahlFeiertage)
            {
                case 0:
                    tsAktuellWochenArbeitszeit = tsRegelWochenArbeitszeit;
                    break;
                case 1:
                    tsFeiertagsZeit = tsTagesArbeitszeit;
                    tsAktuellWochenArbeitszeit = tsRegelWochenArbeitszeit - tsFeiertagsZeit;
                    break;
                case 2:
                    tsFeiertagsZeit = tsTagesArbeitszeit + tsTagesArbeitszeit;
                    tsAktuellWochenArbeitszeit = tsRegelWochenArbeitszeit - tsFeiertagsZeit;
                    break;
                case 3:
                    tsFeiertagsZeit = tsTagesArbeitszeit + tsTagesArbeitszeit + tsTagesArbeitszeit;
                    tsAktuellWochenArbeitszeit = tsRegelWochenArbeitszeit - tsFeiertagsZeit;
                    break;
                case 4:
                    tsFeiertagsZeit = tsTagesArbeitszeit + tsTagesArbeitszeit + tsTagesArbeitszeit + tsTagesArbeitszeit;
                    tsAktuellWochenArbeitszeit = tsRegelWochenArbeitszeit - tsFeiertagsZeit;
                    break;
                default:
                    tsAktuellWochenArbeitszeit = new TimeSpan(38, 30, 0);
                    break;
            }

            return tsAktuellWochenArbeitszeit;
            //return tsAktuellWochenArbeitszeit.TotalHours;

            //string sWochenArbeitszeit = Convert.ToString(tsAktuellWochenArbeitszeit.TotalHours);
            //string[] sArrayminuten = sWochenArbeitszeit.Replace(",", ":").Split(':');
            //string sResultArbeitszeit = "00:00";

            //if (sArrayminuten.Length<2)
            //{
            //    sResultArbeitszeit = sArrayminuten[0] + ":00";
            //}
            //else
            //{
            //    switch (sArrayminuten[1])
            //    {
            //        case "75":
            //            sResultArbeitszeit = sArrayminuten[0] + ":45";
            //            break;
            //        case "25":
            //            sResultArbeitszeit = sArrayminuten[0] + ":15";
            //            break;
            //        case "50":
            //            sResultArbeitszeit = sArrayminuten[0] + ":30";
            //            break;
            //    }
            //}


            //return string.Format("Wochenstunden {0}", sResultArbeitszeit);
        }
    }
}
