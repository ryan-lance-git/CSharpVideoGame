using System.Windows;
using System.Windows.Documents;
using Engine.ViewModels;
using Engine.EventArgs;
using Engine.Models;
using System.Windows.Controls;
using System.Data;
using System.ComponentModel;
using Engine.Factories;

namespace CSharpVideoGame_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ViewModel
        private GameSession _gameSession = new GameSession();
        public MainWindow()
        {
            InitializeComponent();

            _gameSession.OnMessageRaised += OnGameMessageRaised;

            DataContext = _gameSession; // Provides binding for XAML
        }

        private void OnClick_MoveNorth(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveNorth();
        }
        private void OnClick_MoveWest(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveWest();
        }
        private void OnClick_MoveEast(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveEast();
        }
        private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveSouth();
        }
        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }

        private void OnClick_DisplayTradeScreen(object sender, RoutedEventArgs e)
        {
            TradeScreen tradeScreen = new TradeScreen();
            tradeScreen.Owner = this;
            tradeScreen.DataContext = _gameSession;
            tradeScreen.ShowDialog(); // Makes it a "dialog"--player cannot use main screen
        }

        private void DataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Do something here which involves determining which item was selected
            // and presenting a trade screen for that item.

            if (dgInventory.SelectedItem != null)
            {
                GameItem inventoryItem = ((GroupedInventoryItem)dgInventory.SelectedItem).Item;

                ItemDetailsScreen itemDetailsScreen = new ItemDetailsScreen(inventoryItem);
                itemDetailsScreen.Owner = this;
                itemDetailsScreen.ShowDialog();
            }

        }
    }
}
