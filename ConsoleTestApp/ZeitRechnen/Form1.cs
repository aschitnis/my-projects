using System;
using System.Windows.Forms;
using ZeitRechnen.Patterns;

namespace ZeitRechnen
{
    public partial class Form1 : Form
    {
        private ViewModelTaeglichArbzeitDetails oViewModelTaeglichArbZeitDetails;
        private ViewModelWocheArbzeitDetails oViewModelWocheArbZeitDetails;

        private BindingSource bindingSrcTaeglichArbZeitDetailsModel = new BindingSource();
        private BindingSource bindingSrcWochenArbZeitDetailsModel = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void Zuruecksetzen()
        {
            oViewModelTaeglichArbZeitDetails = SingletonViewModelDataContainer.Instance.GetViewModelTaeglichArbzeit();
            oViewModelWocheArbZeitDetails = SingletonViewModelDataContainer.Instance.GetViewModelWocheArbzeit();

            oViewModelTaeglichArbZeitDetails.ListTaeglichArbzeitModel.Clear();
            oViewModelTaeglichArbZeitDetails.tsWochenArbeitszeit = new TimeSpan();

            oViewModelWocheArbZeitDetails.ListWocheArbZeitDetailsModel.Clear();

            tbBisStunden.Text = "0";
            tbBisMinuten.Text = "0";
            tbVonMinuten.Text = "0";
            tbVonStunden.Text = "0";
            chkFeiertageMitberechnen.Checked = true;
            tbFeiertage.Text = "0";
            bindingSrcWochenArbZeitDetailsModel.Clear();
            bindingSrcTaeglichArbZeitDetailsModel.Clear();
        }
        private void Init()
        {
            oViewModelTaeglichArbZeitDetails = SingletonViewModelDataContainer.Instance.GetViewModelTaeglichArbzeit();
            oViewModelWocheArbZeitDetails = SingletonViewModelDataContainer.Instance.GetViewModelWocheArbzeit();

            tbBisStunden.Text = "0";
            tbBisMinuten.Text = "0";
            tbVonMinuten.Text = "0";
            tbVonStunden.Text = "0";
            chkFeiertageMitberechnen.Checked = true;
            tbFeiertage.Text = "0";
            bindingSrcWochenArbZeitDetailsModel.Clear();
            bindingSrcTaeglichArbZeitDetailsModel.Clear();

            // Initialize the DataGridView.
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSize = true;
            dataGridView1.DataSource = bindingSrcTaeglichArbZeitDetailsModel;

            DataGridViewColumn column = new DataGridViewTextBoxColumn();
            column.DataPropertyName = "VonStundenMinuten";
            column.Name = "VonZeit";
            dataGridView1.Columns.Add(column);

            DataGridViewColumn column2 = new DataGridViewTextBoxColumn();
            column2.DataPropertyName = "BisStundenMinuten";
            column2.Name = "BisZeit";
            dataGridView1.Columns.Add(column2);

            DataGridViewColumn column3 = new DataGridViewTextBoxColumn();
            column3.DataPropertyName = "VonBisArbeitszeitSummiert";
            column3.Name = "Summe";
            dataGridView1.Columns.Add(column3);


            /** ********************************************************************* 
             * Initialize the Second GridView about Weekly Work details
             * ***********************************************************************/
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.AutoSize = true;
            dataGridView2.DataSource = bindingSrcWochenArbZeitDetailsModel;

            DataGridViewColumn colWochenArbzeit = new DataGridViewTextBoxColumn();
            colWochenArbzeit.DataPropertyName = "GesetzlicheWoArbeitszeit";
            colWochenArbzeit.Name = "Woch.Arbeitszeit";
            dataGridView2.Columns.Add(colWochenArbzeit);

            DataGridViewColumn colArbeitszeit = new DataGridViewTextBoxColumn();
            colArbeitszeit.DataPropertyName = "MitarbeiterWoArbeitszeit";
            colArbeitszeit.Name = "Arbeitszeit";
            dataGridView2.Columns.Add(colArbeitszeit);

            DataGridViewColumn colDifferenz = new DataGridViewTextBoxColumn();
            colDifferenz.DataPropertyName = "ArbeitszeitDifferenz";  /** 00:00 **/
            colDifferenz.Name = "Differenz";
            dataGridView2.Columns.Add(colDifferenz);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            tbBisStunden.Text = string.IsNullOrEmpty(tbBisStunden.Text) == true ? "0" : tbBisStunden.Text;
            tbBisMinuten.Text = string.IsNullOrEmpty(tbBisMinuten.Text) == true ? "0" : tbBisMinuten.Text;
            tbVonMinuten.Text = string.IsNullOrEmpty(tbVonMinuten.Text) == true ? "0" : tbVonMinuten.Text;
            tbVonStunden.Text = string.IsNullOrEmpty(tbVonStunden.Text) == true ? "0" : tbVonStunden.Text;

            TaeglichArbeitszeitDetails oTaegArbeitsZeitModel = GetTaeglichArbeitszeitModelObject();

            bindingSrcTaeglichArbZeitDetailsModel.Add(oTaegArbeitsZeitModel);
        }

        private void TbVonStunden_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbVonStunden.Text))
            {
                tbVonStunden.Text = "0";
            }
        }

        private void btnZuruecksetzen_Click(object sender, EventArgs e)
        {
            Zuruecksetzen();
        }

        private void btnGesamtBerechnen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbWocheStart.Text) ||
                string.IsNullOrEmpty(tbWocheEnd.Text))
                return;

            int iAnzahlFeiertage = 0;

            if (chkFeiertageMitberechnen.Checked)
            {
                if(!string.IsNullOrEmpty(tbFeiertage.Text))
                {
                    iAnzahlFeiertage = int.Parse(tbFeiertage.Text.Trim());
                }
                else iAnzahlFeiertage = 0;
            }

            AddWocheArbeitszeitModelToDataView(iAnzahlFeiertage);
            foreach( var obj in oViewModelWocheArbZeitDetails.ListWocheArbZeitDetailsModel)
            {
                bindingSrcWochenArbZeitDetailsModel.Add(obj);
            }
            
           // string whArbeitszeit = CommonFunctions.ConvertTimespanToHoursAndMinutes(tsGesetzlicheWochenarbZeitInklFeiertage);
           // lblberechnetesWert.Text = "Wochenarbeitszeit beträgt " + whArbeitszeit;

            // lblAusWochenArbZeit.Text = "Tatsächliche Arbeitszeit beträgt " + oViewModelTaeglichArbZeitDetails.BerechnenAktuelleWochenArbeitszeit();

        }

        private void AddWocheArbeitszeitModelToDataView(int ifeiertage)
        {
            ModelWocheArbeitszeitDetails objModelWocheArbZeitDetails = oViewModelWocheArbZeitDetails.GetModelWocheArbeitszeitDetails();
            /*
            * d.h. die gesetzliche Wochenarbeitszeit abzüglich Feiertag/e 
            */
            TimeSpan tsGesetzlicheWochenarbZeitInklFeiertage = CommonFunctions.GesetzlicheWochenArbeitszeitMitFeiertageBerechnen(ifeiertage);
            objModelWocheArbZeitDetails.GesetzlicheWoArbeitszeit = CommonFunctions.ConvertTimespanToHoursAndMinutes(tsGesetzlicheWochenarbZeitInklFeiertage);
            objModelWocheArbZeitDetails.MitarbeiterWoArbeitszeit = oViewModelTaeglichArbZeitDetails.BerechnenAktuelleWochenArbeitszeit();

            oViewModelWocheArbZeitDetails.ListWocheArbZeitDetailsModel.Add(objModelWocheArbZeitDetails);
        }

        private TaeglichArbeitszeitDetails GetTaeglichArbeitszeitModelObject()
        {
            return
            oViewModelTaeglichArbZeitDetails.GeneriereTaeglichArbeitszeitModelData(tbVonMinuten.Text.Trim(),
                                                                                   tbBisMinuten.Text.Trim(),
                                                                                   tbBisStunden.Text.Trim(),
                                                                                   tbVonStunden.Text.Trim());
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
