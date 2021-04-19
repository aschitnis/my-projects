using MySql.Data.MySqlClient;
using schnittstelle.mysql.db.baeumer.services.DAO;
using schnittstelle.mysql.db.baeumer.services.DatabaseViewModels.TablesViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace schnittstelle.mysql.db.baeumer.services.DatabaseViewModels
{
    public class DatabaseBusinessViewModel
    {

        public DataAccessModel DataAccessObject { get; set; }

        public DatabaseBusinessViewModel()
        {
            /* gets a object of DataAccessModel.
             * The DataAccessModel class contains all the queries & the connection-object. 
             */
            DataAccessObject = SingletonServices.Instance.GetDataAccessModel();
        }

        /**
         * Get all the Customers
         * **/
        public CustomerViewModel GetCustomers()
        {
            CustomerViewModel customervm = new CustomerViewModel();
            customervm.ResetCustomerModelList();

            DataTable tblCustomer = DataAccessObject.GetCustomerData();
            foreach (DataRow rwCustomer in tblCustomer.Rows)
            {
                int customer_id = (Int32)rwCustomer[0];
                customervm.AddCustomerToList( customer_id, 
                                              rwCustomer["customername"].ToString(),
                                              rwCustomer["customeraccount"].ToString(),
                                              rwCustomer["description"].ToString());
            }
            return customervm;
        }

        /* gets all the data from tblIncoming */
        public ObservableCollection<IncomingViewModel> GetIncomingData()
        {
            ObservableCollection<IncomingViewModel> incomingCollection = new ObservableCollection<IncomingViewModel>();
            /***
            if (DataAccessObject == null)
            {
                DataAccessObject = SingletonServices.Instance.GetDataAccessModel();
            }
            ***/
            /* Get all primary-keys (incoming_id's) from tblIncoming */
            DataTable dtPKIncoming = DataAccessObject.GetDataPrimaryKeysIncomingModel();

            /* loop over the incoming_id's from tblIncoming */
            foreach (DataRow rw in dtPKIncoming.Rows)

            {
                int incoming_id = (Int32)rw[0];
                IncomingViewModel tblIncomingModel = new IncomingViewModel();

                /* gets all the data from tblIncoming for the @incoming_id specified in the parameter.
                 * This calls the Stored-procedure : sp_getIncomingAndCustomerData(@incoming_id).
                 */
                DataTable dtincoming = DataAccessObject.GetCustomerAndIncomingDataTable(incoming_id);
                int customerid = (Int32)dtincoming.Rows[0]["incoming_customer_id"];

                tblIncomingModel.FillIncomingModel(incoming_id,
                                                      (DateTime)dtincoming.Rows[0]["incomingdate"],
                                                      dtincoming.Rows[0]["narration"].ToString(),
                                                      Convert.ToDouble(dtincoming.Rows[0]["depositamount"]),
                                                      Convert.ToDouble(dtincoming.Rows[0]["balance"]),
                                                      (Int32)dtincoming.Rows[0]["incoming_customer_id"]);

                tblIncomingModel.FillCustomerModel((Int32)dtincoming.Rows[0]["customer_id"],
                                                    dtincoming.Rows[0]["customername"].ToString(),
                                                    dtincoming.Rows[0]["customeraccount"].ToString(),
                                                    dtincoming.Rows[0]["description"].ToString());

                DataTable dtoutgoing = DataAccessObject.GetForIncomingModelOutgoingDataTable(incoming_id);

                foreach(DataRow rwoutgoing in dtoutgoing.Rows)
                {
                    tblIncomingModel.AddOutgoingModel((Int32)rwoutgoing["outgoing_id"],
                                                                (Int32)rwoutgoing["out_incoming_id"],
                                                                (DateTime)rwoutgoing["outgoingdate"],
                                                                rwoutgoing["outgoing_narration"].ToString(),
                                                                Convert.ToDecimal(rwoutgoing["withdrawalamount"]));
                }

                DataTable dtcharges = DataAccessObject.GetForIncomingModelChargesDataTable(incoming_id);

                foreach (DataRow rwcharges in dtcharges.Rows)
                {
                    tblIncomingModel.AddToListChargeModel((Int32)rwcharges["charge_id"],
                                                          (Int32)rwcharges["charge_incoming_id"],
                                                          (DateTime)rwcharges["chargedate"],
                                                          rwcharges["charge_narration"].ToString(),
                                                          Convert.ToDecimal(rwcharges["charge"]),
                                                          rwcharges["comments"].ToString()
                                                        );
                }

                DataTable dtclosing = DataAccessObject.GetClosingDataTable(customerid);
                foreach (DataRow rwclosing in dtclosing.Rows)
                {
                    tblIncomingModel.AddClosingModel((Int32)rwclosing["closing_id"],
                                                      (Int32)rwclosing["customer_id"],
                                                      (DateTime)rwclosing["closingdate"],
                                                      Convert.ToDecimal(rwclosing["closingbalance"]),
                                                      rwclosing["description"].ToString()
                                                    );
                }
                incomingCollection.Add(tblIncomingModel);
            }
            return incomingCollection;
        }

        /* gets all the data from tblIncoming */
        //public ObservableCollection<IncomingTableModel> GetIncomingDataOld()
        //{
        //    ObservableCollection<IncomingTableModel> incomingCollection = new ObservableCollection<IncomingTableModel>();

        //    if (DataAccessObject == null)
        //    {
        //        DataAccessObject = SingletonServices.Instance.GetDataAccessModel();
        //    }

        //    /* Get all primary-keys (incoming_id's) from tblIncoming */
        //      DataTable dtPKIncoming = DataAccessObject.GetDataPrimaryKeysIncomingModel();

        //    /* loop over the incoming_id's from tblIncoming */
        //    foreach ( DataRow rw in dtPKIncoming.Rows)

        //    {
        //        int incoming_id = (Int32)rw[0];
        //        IncomingTableModel  tblIncomingModel = new IncomingTableModel();

        //        /* gets all the data from tblIncoming for the @incoming_id specified in the parameter.
        //         * This calls the Stored-procedure : sp_getincomingdata(@incoming_id).
        //         */
        //        DataTable dtincoming = DataAccessObject.GetCustomerAndIncomingData(incoming_id);

        //        int icount = 0;
        //        int iTempOutgoing_Id = 0;
        //        foreach(DataRow rwincoming in dtincoming.Rows)
        //        {
        //            /* 
        //             * check if this is the first record/row in the loop. 
        //             * If so then fill the data for the IncomingModel & the CustomerModel Objects. 
        //             */
        //            if (icount == 0) 
        //            {
        //                 tblIncomingModel.FillIncomingModel(incoming_id,
        //                                                   (DateTime)rwincoming["incomingdate"],
        //                                                   rwincoming["narration"].ToString(),
        //                                                   Convert.ToDouble(rwincoming["depositamount"]),
        //                                                   Convert.ToDouble(rwincoming["balance"]),
        //                                                   (Int32)rwincoming["customer_id"]);
                        
        //                tblIncomingModel.FillCustomerModel(incoming_id, 
        //                                                    rwincoming["customername"].ToString(),
        //                                                    rwincoming["customeraccount"].ToString(),
        //                                                    rwincoming["description"].ToString());

        //                /* 
        //                 * check if the outgoing_id exists. 
        //                 * -1 heißt (unrelevant/nicht vorhanden) Und 1..99 heißt (vorhanden) 
        //                 * Wenn outgoing_id vorhanden ist, dann OutgoingModel zur Liste hinzufügen
        //                 */
                         
        //                iTempOutgoing_Id = (Int32)rwincoming["outgoing_id"];
        //                if ((Int32)rwincoming["outgoing_id"] > 0)
        //                {
        //                    tblIncomingModel.AddToListOutgoingModel((Int32)rwincoming["outgoing_id"],
        //                                                                (Int32)rwincoming["out_incoming_id"],
        //                                                                (DateTime)rwincoming["outgoingdate"],
        //                                                                rwincoming["outgoing_narration"].ToString(),
        //                                                                Convert.ToDecimal(rwincoming["withdrawalamount"]));
        //                }

        //                if ((Int32)rwincoming["charge_id"] > 0)
        //                {
        //                    tblIncomingModel.AddToListChargeModel((Int32)rwincoming["charge_id"],
        //                                                          (Int32)rwincoming["charge_incoming_id"],
        //                                                          (DateTime)rwincoming["chargedate"],
        //                                                          rwincoming["charge_narration"].ToString(),
        //                                                          Convert.ToDecimal(rwincoming["charge"]),
        //                                                          rwincoming["comments"].ToString()
        //                                                         );
        //                }
        //            }
        //            else
        //            {
        //                int current_outgoing_id = (Int32)rwincoming["outgoing_id"];

        //                if (current_outgoing_id > 0 && 
        //                    iTempOutgoing_Id != current_outgoing_id)
        //                {
        //                    tblIncomingModel.AddToListOutgoingModel((Int32)rwincoming["outgoing_id"],
        //                                    (Int32)rwincoming["out_incoming_id"],
        //                                    (DateTime)rwincoming["outgoingdate"],
        //                                    rwincoming["outgoing_narration"].ToString(),
        //                                    Convert.ToDecimal(rwincoming["withdrawalamount"]));

        //                    iTempOutgoing_Id = current_outgoing_id;
        //                }
        //            }
        //            icount++;
        //         }
        //        incomingCollection.Add(tblIncomingModel);
        //    }
        //    return incomingCollection;
        //}
    }
}
