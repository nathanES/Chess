namespace Chess.UI;

using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Logique d'interaction pour PauseMenu.xaml.
/// </summary>
public partial class PauseMenu : UserControl
{
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
    public PauseMenu() => InitializeComponent();
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.

    public event Action<Option> OptionSelected;

    private void Continue_Click(object sender, RoutedEventArgs e) => OptionSelected?.Invoke(Option.Continue);

    private void Restart_Click(object sender, RoutedEventArgs e) => OptionSelected?.Invoke(Option.Restart);
}
