namespace GoFishConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stock = new Deck();
            var player = new Player("Owen");
            player.DrawCard(stock);
        }
    }
}
