namespace Chess.UI;

using Chess.Logic;
using System.Windows.Controls;
using System.Windows.Input;

/// <summary>
/// Logique d'interaction pour PromotionMenu.xaml.
/// </summary>
public partial class PromotionMenu : UserControl
{
    public PromotionMenu(Player player)
    {
        InitializeComponent();
        QueenImg.Source = Images.GetImage(player, PieceType.Queen);
        RookImg.Source = Images.GetImage(player, PieceType.Rook);
        BishopImg.Source = Images.GetImage(player, PieceType.Bishop);
        KnightImg.Source = Images.GetImage(player, PieceType.Knight);
    }

    public event Action<PieceType> PieceSelected;

    private void QueenImg_MouseDown(object sender, MouseButtonEventArgs e) => PieceSelected?.Invoke(PieceType.Queen);

    private void RookImg_MouseDown(object sender, MouseButtonEventArgs e) => PieceSelected?.Invoke(PieceType.Rook);

    private void BishopImg_MouseDown(object sender, MouseButtonEventArgs e) => PieceSelected?.Invoke(PieceType.Bishop);

    private void KnightImg_MouseDown(object sender, MouseButtonEventArgs e) => PieceSelected?.Invoke(PieceType.Knight);
}
