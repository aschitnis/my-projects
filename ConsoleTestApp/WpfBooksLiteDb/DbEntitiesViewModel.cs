using LiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfBooksLiteDb.Entities.ViewModels;

namespace WpfBooksLiteDb.Database.Entities.VM
{
    public class DbEntitiesSingletonVM : INotifyPropertyChanged, IDisposable
    {
        private bool isDisposed = false;
        public string databasepath { get; }
        public NewBookEntityVM NewBookViewModel { get; set; }
        public NewBookmarkEntityVM NewBookmarkEntityVM { get; set; }

        #region Event Notification
        private static DbEntitiesSingletonVM instance = new DbEntitiesSingletonVM();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Constructor
        private DbEntitiesSingletonVM()
        {
            databasepath = Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.LastIndexOf(@"\")) + @"\Books.db";
            NewBookViewModel = new NewBookEntityVM(databasepath);
            NewBookmarkEntityVM = new NewBookmarkEntityVM(databasepath);
        }
        #endregion

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static DbEntitiesSingletonVM GetInstance()
        {
            return instance;
        }


        #region Dispose Methods
        public void Dispose()
        {
            Dispose(true);

            /* GC.SuppressFinalize(this) :
             * Objects that implement the IDisposable interface can call this method 
             * from the IDisposable.Dispose method to prevent the garbage collector 
             * from calling Object.Finalize on an object that does not require it.
             */
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // clean up managed objects by calling their Dispose() method.

                    // IsDatabaseConnected = false;
                }
                // clean up unmanaged objects here
            }
            isDisposed = true;
        }
        #endregion

        #region Destructor
        ~DbEntitiesSingletonVM()
        {
            Dispose(false);
        }
        #endregion
    }
}
