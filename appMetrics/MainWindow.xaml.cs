using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace appMetrics
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int agentID;
        public static string agentAddress = "http://localhost:7993";
        public static List<AgentInfo> agents = new List<AgentInfo>();
        public MainWindow()
        {
            InitializeComponent();

            AddAgentsToList();
            if (listBox.Items.Count != 0)
            {
                agentID = Convert.ToInt32(listBox.SelectedItem.ToString());
            }

            listBox.SelectedIndex = 0;
        }
        public void AddAgentsToList()
        {
            Metrics metrics = new Metrics();
            agents.Clear();
            agents = metrics.GetAgents();

            listBox.Items.Clear();
            for (int i = 0; i < agents.Count; i++)
            {
                listBox.Items.Add($"{agents[i].AgentId}");
            }

        }
        private void Select(object sender, SelectionChangedEventArgs args)
        {
            if (listBox.Items.Count != 0)
            {
                agentID = Convert.ToInt32(listBox.SelectedItem.ToString());
                currentAgent.Text = $" Текущий агент {agentID} {agents[listBox.SelectedIndex].AgentAddress}";

            }
        }
        private void UpdateOnСlickRegister(object sender, RoutedEventArgs args)
        {
            Register registerAgent = new Register();
            registerAgent.Show();
        }
        private void UpdateOnСlickDelete(object sender, RoutedEventArgs e)
        {
            agentID = Convert.ToInt32(listBox.SelectedItem.ToString());
            int indexTmp = listBox.SelectedIndex;
            Metrics metric = new Metrics();
            metric.DeleteAgent(agentID);
            AddAgentsToList();
            listBox.SelectedIndex = indexTmp - 1;
        }
    }
}
