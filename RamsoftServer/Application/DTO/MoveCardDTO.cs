namespace RamsoftServer.Application.DTO
{
    public class MoveCardDTO
    {
        public int CardId { get; set; }
        public int PreviousColumnId { get; set; }
        public int PreviousIndex { get; set; }
        public int NewColumnId { get; set; }
        public int NewIndex { get; set; }
    }
}
