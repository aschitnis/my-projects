//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFBikeSalesLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class stock
    {
        public int store_id { get; set; }
        public int product_id { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual product product { get; set; }
        public virtual store store { get; set; }
    }
}
