﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace my.country.sales.models
{
    #region UNUSED CLASSES
    /** currently these classes is not to be used in production code. **/
    //public class RegionDisplayModel : INotifyPropertyChanged
    //{
    //    private string regionnom;
    //    private string regionpointername;

    //    public string RegionName 
    //    {
    //        get { return regionnom; }
    //        set { regionnom = value; OnPropertyChanged(); }
    //    }
    //    public string RegionPointerName
    //    {
    //        get { return regionpointername; }
    //        set { regionpointername = value; OnPropertyChanged(); }
    //    }

    //    #region Event
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //    #endregion
    //}

    //public class CountryModel
    //{
    //    private string country;
    //    private List<SalesModel> countrysalesmodels;
    //    public string Country
    //    {
    //        get { return country; }
    //        set { country = value; }
    //    }
    //    public List<SalesModel> CountrySalesModels
    //    {
    //        get { return countrysalesmodels; }
    //        set { countrysalesmodels = value;  }
    //    }
    //    public CountryModel()
    //    {
    //        CountrySalesModels = new List<SalesModel>();
    //    }
    //}
    #endregion

    public class RegionModel 
    {
        private string region;
        private List<SalesModel> regionalsalesmodels;
        public string Region
        {
            get { return region; }
            set { region = value;  }
        }
        public List<SalesModel> RegionalSalesModels
        {
            get { return regionalsalesmodels; }
            set { regionalsalesmodels = value; }
        }
        public RegionModel()
        {
            RegionalSalesModels = new List<SalesModel>();
        }

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
    public partial class SalesModel //: IEquatable<SalesModel>
    {
        //Region,Country,Item Type, Sales Channel,Order Priority, Order Date,Order ID, 
        // Ship Date,Units Sold, Unit Price,Unit Cost, Total Revenue,Total Cost, Total Profit
        private string region;
        private string country;
        private string itemtype;
        private string saleschannel;
        private string orderpriority;
        private DateTime orderdate;
        private long orderid;
        private DateTime shipdate;
        private string unitssold;
        private string unitprice;
        private string unitcost;
        private string totalrevenue;
        private string totalcost;
        private string totalprofit;

        #region Properties
        public string Region
        {
            get { return region; }
            set { region = value; OnPropertyChanged(); }
        }
        public string Country
        {
            get { return country; }
            set { country = value; OnPropertyChanged(); }
        }
        public string ItemType
        {
            get { return itemtype; }
            set { itemtype = value; OnPropertyChanged(); }
        }
        public string SalesChannel
        {
            get { return saleschannel; }
            set { saleschannel = value; OnPropertyChanged(); }
        }
        public string OrderPriority
        {
            get { return orderpriority; }
            set { orderpriority = value; OnPropertyChanged(); }
        }
        public DateTime OrderDate { get { return orderdate; } set { orderdate = value;OnPropertyChanged(); } }
        public long OrderId
        {
            get { return orderid; }
            set { orderid = value;OnPropertyChanged(); }
        }
        public DateTime ShipDate { get { return shipdate; } set { shipdate = value; OnPropertyChanged(); } }
        #endregion

        #region interface IEquatable methods
        //public override bool Equals(object obj)
        //{
        //    if (obj == null) return false;
        //    SalesModel objAsSalesModel = obj as SalesModel;
        //    if (objAsSalesModel == null) return false;
        //    else return Equals(objAsSalesModel);
        //}
        //public override int GetHashCode()
        //{
        //    return OrderId;
        //}
        //public bool Equals(SalesModel other)
        //{
        //    if (other == null) return false;
        //    return (this.OrderId.Equals(other.OrderId));
        //}
        #endregion

        #region Event
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
