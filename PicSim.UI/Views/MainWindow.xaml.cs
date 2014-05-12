using PicSim.UI.ViewModels;
using PicSim.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Waf.Applications;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PicSim.UI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            txtCode.ShowLineNumbers = true;
        }

        #region Event Handler

        #region New

        private void NewEnabled(object sender, CanExecuteRoutedEventArgs e)
        {
            
        }

        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        #endregion

        #region Save

        private void SaveEnabled(object sender, CanExecuteRoutedEventArgs e)
        {
            
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        #endregion

        #region SaveAs

        private void SaveAsExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        #endregion

        #region Close

        private void CloseEnabled(object sender, CanExecuteRoutedEventArgs e)
        {
            
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {

            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            
        }

        #endregion

        #region Compile

        private void CompileEnabled(object sender, CanExecuteRoutedEventArgs e)
        {
            
        }

        private void CompileExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        #endregion

        #region Pause

        private void PauseEnabled(object sender, CanExecuteRoutedEventArgs e)
        {
            
        }

        private void PauseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        #endregion

        #region Continue

        private void ContinueEnabled(object sender, CanExecuteRoutedEventArgs e)
        {
            
        }

        private void ContinueExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        #endregion

        #region Step

        private void StepEnabled(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void StepExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        #endregion

        #region Speed

        private void cbxSpeed_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            /*if (controller != null)
            {
                controller.SimulationSpeed = getSimuSpeedFromComboBox();
            }*/
        }

        #endregion

        #region TextInput
        private void txtCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
        #endregion


        #endregion
    }
}
