namespace DungeonAndDragonsWebsite.Models.Requests
{
    public class UpdateDungeonMasterRequest : DefaultRequest
    {
        public bool RemoveDungeonMaster { get; set; }

        public int noOfPlayer { get; set; }
    }
}
