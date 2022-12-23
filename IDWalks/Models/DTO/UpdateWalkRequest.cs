namespace IDWalks.Models.DTO
{
    public class UpdateWalkRequest
    {
        public string name { get; set; }

        public double Length { get; set; }

        public Guid RegionId { get; set; }

        public Guid WalkDeficultyId { get; set; }
    }
}
