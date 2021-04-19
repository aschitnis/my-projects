using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_Bettina_Bauemer_Application.view.root;

namespace Wpf_Bettina_Bauemer_Application
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainTransactions : Window
    {
        // Dummy columns for layers 0 and 1:
        ColumnDefinition colOneCopyForLayer0;
        ColumnDefinition colTwoCopyForLayer0;
        ColumnDefinition colTwoCopyForLayer1;


        private MainDataView mainviewobject;

        public MainDataView MainViewObject
        {
            get { return mainviewobject; }
            set { mainviewobject = value; }
        }

        public MainTransactions()
        {
            InitializeComponent();

            // Initialize the dummy (grouped) columns that are created during docking:
            colOneCopyForLayer0 = new ColumnDefinition();
            colOneCopyForLayer0.SharedSizeGroup = "column1";
            colTwoCopyForLayer0 = new ColumnDefinition();
            colTwoCopyForLayer0.SharedSizeGroup = "column2";
            colTwoCopyForLayer1 = new ColumnDefinition();
            colTwoCopyForLayer1.SharedSizeGroup = "column2";

            Init();
            mainwindow.DataContext = MainViewObject.MaindataContainer;
        }

        private void Init()
        {
            MainViewObject = new MainDataView();
        }

        // Make panel 1 visible when hovering over its button
        private void btnOne_MouseEnter(object sender, MouseEventArgs e)
        {
            /* This is the Panel that appears for Button One */
            gridlayer1.Visibility = Visibility.Visible;

            /*
             *  layer0_panel1 ist die Sicht, die die Datagrids für Button Two beinhaltet.
             *  Diese Sicht wird unsichtbar gemacht.
             */
            layer0_panel1.Visibility = Visibility.Collapsed;

            /*
             *  layer0 ist die Sicht, die die Datagrids für Button One beinhaltet.
             *  Diese Sicht wird sichtbar gemacht.
             */
            layer0.Visibility = Visibility.Visible;

            // gridlayer1 is the Panel that appears for Button One.
            // Adjust ZIndex order to ensure the pane is on top:
            parentGrid.Children.Remove(gridlayer1);
            parentGrid.Children.Add(gridlayer1);

            // Ensure the other pane is hidden if it is undocked
            // gridlayer2 is the Panel that appears for Button Two.
            if (btnTwo.Visibility == Visibility.Visible)
                gridlayer2.Visibility = Visibility.Collapsed;
        }

        private void btnTwo_MouseEnter(object sender, MouseEventArgs e)
        {
            /* This is the Panel with TreeView that appears for Button Two */
            gridlayer2.Visibility = Visibility.Visible;

            /*
             *  layer0_panel1 ist die Sicht, die die Datagrids für Button Two beinhaltet.
             *  layer0 ist die Sicht, die die Datagrids für Button One beinhaltet.
             *  layer0_panel1 wird sichtbar gemacht.
             *  layer0 wird unsichtbar gemacht.
             */
            layer0_panel1.Visibility = Visibility.Visible;
            layer0.Visibility = Visibility.Collapsed;

            // gridlayer2 is the Panel that appears for Button One.
            // Adjust ZIndex order to ensure the pane is on top:
            parentGrid.Children.Remove(gridlayer2);
            parentGrid.Children.Add(gridlayer2);

            // gridlayer1 is the Panel that appears for Button One.
            // Ensure the other pane is hidden if it is undocked
            if (btnOne.Visibility == Visibility.Visible)
                gridlayer1.Visibility = Visibility.Collapsed;
        }

        private void btnThree_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        // Toggle panel 1 between docked and undocked states
        public void panel1Pin_Click(object sender, RoutedEventArgs e)
        {
            if (btnOne.Visibility == Visibility.Collapsed)
             UndockPane(1);
            else
             DockPane(1);
        }

        // Undocks a pane, which reveals the corresponding pane button
        public void UndockPane(int panelNbr)
        {
            if (panelNbr == 1)
            {
                gridlayer1.Visibility = Visibility.Collapsed;
                btnOne.Visibility = Visibility.Visible;
                panel1PinImg.Source = new BitmapImage(new Uri("HorizontalPin.jpg", UriKind.Relative));

                // Remove the cloned columns from layers 0 and 1:
                layer0.ColumnDefinitions.Remove(colOneCopyForLayer0);
                // This won't always be present, but Remove silently ignores bad columns:
                gridlayer1.ColumnDefinitions.Remove(colTwoCopyForLayer1);
            }
            else if (panelNbr == 2)
            {
                gridlayer2.Visibility = Visibility.Collapsed;
                btnTwo.Visibility = Visibility.Visible;
                panel2PinImg.Source = new BitmapImage(new Uri("HorizontalPin.jpg", UriKind.Relative));

                // Remove the cloned columns from layers 0 and 1:
                layer0.ColumnDefinitions.Remove(colTwoCopyForLayer0);
                gridlayer1.ColumnDefinitions.Remove(colTwoCopyForLayer1);
            }
        }

        // Docks a pane and hides its button
        public void DockPane(int paneNumber)
        {
            if (paneNumber == 1)
            {
                btnOne.Visibility = Visibility.Collapsed;
                panel1PinImg.Source = new BitmapImage(new Uri("VerticalPin.jpg", UriKind.Relative));

                // Add the cloned column to layer 0:
                layer0.ColumnDefinitions.Add(colOneCopyForLayer0);
                // Add the cloned column to layer 1, but only if pane 2 is docked:
                if (btnTwo.Visibility == Visibility.Collapsed) gridlayer1.ColumnDefinitions.Add(colTwoCopyForLayer1);
            }
            else if (paneNumber == 2)
            {
                btnTwo.Visibility = Visibility.Collapsed;
                panel2PinImg.Source = new BitmapImage(new Uri("VerticalPin.jpg", UriKind.Relative));

                // Add the cloned column to layer 0:
                layer0.ColumnDefinitions.Add(colTwoCopyForLayer0);
                // Add the cloned column to layer 1, but only if pane 1 is docked:
                if (btnOne.Visibility == Visibility.Collapsed) gridlayer1.ColumnDefinitions.Add(colTwoCopyForLayer1);
            }
        }

        // Toggle panel 2 between docked and undocked states 
        public void panel2Pin_Click(object sender, RoutedEventArgs e)
        {
            if (btnTwo.Visibility == Visibility.Collapsed)
             UndockPane(2);
            else
             DockPane(2);

        }

        // Hide the other pane if undocked when the mouse enters/hovers over Panel-1 Image (image of pin)
        public void panel1PinImg_MouseEnter(object sender, RoutedEventArgs e)
        {
            // Ensure the other pane is hidden if it is undocked
            if (btnTwo.Visibility == Visibility.Visible)
             gridlayer2.Visibility = Visibility.Collapsed;
        }

        // Hide the other pane if undocked when the mouse enters/hovers over Panel-2 Image (image of pin)
        public void panel2PinImg_MouseEnter(object sender, RoutedEventArgs e)
        {
            // Ensure the other pane is hidden if it is undocked
            if (btnOne.Visibility == Visibility.Visible)  
             gridlayer1.Visibility = Visibility.Collapsed;
        }

        #region Events for Layer0
        /* 
         * layer_0 Grid beinhaltet alle 3 Datagrids bzg. Einzahlungsbeträge, Auszahlungsbeträge und Gebührenbeträge. 
         * layer_0 Grid ist assoziert mit Button One.
         */
        private void layer0_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((sender as Grid).Visibility == Visibility.Visible)
            {
                if (btnOne.Visibility == Visibility.Visible)
                    gridlayer1.Visibility = Visibility.Collapsed;
                if (btnTwo.Visibility == Visibility.Visible)
                    gridlayer2.Visibility = Visibility.Collapsed;
            }
        }

        /* 
         * layer0_panel1 Grid beinhaltet alle 3 Datagrids bzg. Einzahlungsbeträge, 
         *  Auszahlungsbeträge und Gebührenbeträge. 
         * layer0_panel1 Grid ist assoziert mit Button Two.
         */
        private void layer0_panel1_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((sender as Grid).Visibility == Visibility.Visible)
            {
                if (btnOne.Visibility == Visibility.Visible)
                    gridlayer1.Visibility = Visibility.Collapsed;
                if (btnTwo.Visibility == Visibility.Visible)
                    gridlayer2.Visibility = Visibility.Collapsed;
            }
        }
        #endregion
    }
}
