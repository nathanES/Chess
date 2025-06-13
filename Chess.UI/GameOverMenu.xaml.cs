namespace Chess.UI;

using Chess.Logic;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Logique d'interaction pour GameOverMenu.xaml.
/// </summary>
public partial class GameOverMenu : UserControl
{
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
    public GameOverMenu(GameState gameState)
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur autre que Null lors de la fermeture du constructeur. Envisagez d’ajouter le modificateur « required » ou de déclarer le champ comme pouvant accepter la valeur Null.
    {
        InitializeComponent();
        Result? result = gameState.Result;
        if (result == null)
            throw new NullReferenceException($"There is no {nameof(result)}");
        WinnerText.Text = GetWinnerText(result.Winner);
        ReasonText.Text = GetReasonText(result.Reason, gameState.CurrentPlayer);
    }

    public event Action<Option> OptionSelected;

    private static string GetWinnerText(Player winner)
    {
        return winner switch
        {
            Player.White => "WHITE WINS!",
            Player.Black => "BLACK WINS!",
            _ => "IT'S A DRAW"
        };
    }

    private static string PlayerString(Player player)
    {
        return player switch
        {
            Player.White => "WHITE",
            Player.Black => "BLACK",
            _ => string.Empty
        };
    }

    private static string GetReasonText(EndReason reason, Player currentPlayer)
    {
        return reason switch
        {
            EndReason.Stalemate => $"STALEMATE - {PlayerString(currentPlayer)} CAN'T MOVE",
            EndReason.Checkmate => $"CHECKMATE - {PlayerString(currentPlayer)} CAN'T MOVE",
            EndReason.FiftyMoveRule => $"FIFTY-MOVE RULE",
            EndReason.InsufficientMaterial => $"INSUFFICIENT MATERIAL",
            EndReason.ThreefoldRepetition => $"THREEFOLD REPETITION",
            _ => string.Empty
        };
    }

    private void Restar_Click(object sender, RoutedEventArgs e) => OptionSelected?.Invoke(Option.Restart);

    private void Exit_CLick(object sender, RoutedEventArgs e) => OptionSelected?.Invoke(Option.Exit);
}
