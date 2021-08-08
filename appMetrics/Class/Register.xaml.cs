using System;
using System.Windows;
using System.Windows.Media;

namespace appMetrics.Class
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void ButtonClickOk(object sender, RoutedEventArgs e)
        {
            if (ID.Text != "" && Address.Text != "")
            {
                Metrics metric = new Metrics();

                try
                {
                    int agentId = Convert.ToInt32(ID.Text);
                    metric.AddAgent(agentId, Address.Text);
                    MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                    mainWindow.AddAgentsToList();
                    mainWindow.listBox.SelectedIndex = mainWindow.listBox.Items.Count - 1;
                    Close();
                }
                catch
                {
                    ID.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF99D9D"));
                }
            }
            else Close();
        }

        private void ButtonClickCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
