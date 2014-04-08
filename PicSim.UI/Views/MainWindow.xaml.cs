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

		#region Open

		private void OpenEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			
		}

		private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
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


		#endregion

		#region Compile

		private void CompileEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			
		}

		private void CompileExecuted(object sender, ExecutedRoutedEventArgs e)
		{
			
		}

		#endregion

		#region Run

		private void RunEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			
		}

		private void RunExecuted(object sender, ExecutedRoutedEventArgs e)
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

		#region Stop

		private void StopEnabled(object sender, CanExecuteRoutedEventArgs e)
		{
			
		}

		private void StopExecuted(object sender, ExecutedRoutedEventArgs e)
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

		#region Help

		private void OnShowPdf(object sender, RoutedEventArgs e)
		{
			/*string path = Path.Combine(Directory.GetCurrentDirectory(), "HELP_PDF.pdf");
		
			String openPDFFile = Path.Combine(Directory.GetCurrentDirectory(), "HELP_PDF.pdf"); ;

			System.Diagnostics.Process.Start(openPDFFile);*/
		}
		#endregion

		#endregion
	}
}
