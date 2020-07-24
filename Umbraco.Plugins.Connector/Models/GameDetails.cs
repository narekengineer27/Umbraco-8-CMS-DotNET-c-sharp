using System.Collections.Generic;
namespace Umbraco.Plugins.Connector.Models
{
    using System;

    public class GameDetails
    {
        public int GameId { get; set; }
        public string GameIdentifier { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public GameImage[] GameImages { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string GameProvider { get; set; }
        public string Url { get; set; }
        public string GamePageUrl { get; set; }
        public int UrlType { get; set; }
        public UrlType UrlTypeEnum { get { return (UrlType)UrlType; } }
        public bool Favourite { get; set; }
        public bool NewGame { get; set; }
        public GameConfiguration[] Configurations { get; set; }
        public string ExternalId { get; set; }
        public bool DemoEnabled { get; set; }
        public string DemoUrl { get; set; }
        public string GameType { get; set; }
        public string DealerName { get; set; }
        public string Currency_symbol { get; set; }
        public string Limit_min { get; set; }
        public string Limit_max { get; set; }
        public List<GameResult> Results { get; set; }
        public List<Seat> Seats { get; set; }
        public List<BaccaratResult> Roads { get; set; }
    }

    public class GameResult
    {
        public string Value { get; set; }
        public string Color { get; set; }
    }

    public class Seat
    {
        public string Number { get; set; }
        public string Status { get; set; }
    }
    public class BaccaratResult
    {
        public string X { get; set; } 
        public string Y { get; set; }
        public string Color { get; set; }
    }

    public class GameImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ImageType { get; set; }
        public ImageType ImageTypeEnum { get { return (ImageType)ImageType; } }
    }

    public class GameConfiguration
    {
        public string Value { get; set; }
    }

    public enum ImageType
    {
        Thumbnail = 1,
        Full = 2
    }

    public enum UrlType
    {
        iFrame = 1,
        Session = 2
    }

    public enum GameMode
    {
        Play,
        Demo,
        Choose
    }

    public enum GameLoadStatus
    {
        Success,
        Failure
    }
}
