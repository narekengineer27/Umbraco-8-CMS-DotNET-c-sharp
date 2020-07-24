namespace Umbraco.Plugins.Connector
{
    using Umbraco.Core;
    using Umbraco.Core.Composing;
    using Umbraco.Plugins.Connector.Interfaces;
    using Umbraco.Plugins.Connector.Services;
    public class Composer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<ITicketService, TicketService>();
            composition.Register<ITicketFileService, TicketFileService>();
            composition.Register<ICardService, CardService>();
            composition.Register<ITransactionService, TransactionService>();
            composition.Register<ITransactionHistoryService, TransactionHistoryService>();
            composition.Register<IUtilityService, UtilityService>();
            composition.Register<IGameService, GameService>();
        }
    }
}
