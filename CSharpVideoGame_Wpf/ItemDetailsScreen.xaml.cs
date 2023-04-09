using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CSharpVideoGame_Wpf
{
    /// <summary>
    /// Interaction logic for ItemDetailsScreen.xaml
    /// </summary>
    public partial class ItemDetailsScreen : Window
    {
        public ItemDetailsScreen(GameItem gameItem)
        {
            InitializeComponent();

            ItemName.Content = gameItem.Name;
            ItemDescription.Content = gameItem.Description;
        }
    }
}
