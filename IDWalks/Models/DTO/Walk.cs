using IDWalks.Models.Domines;

namespace IDWalks.Models.DTO
{
    public class Walk
    {
        public string name { get; set; }

        public double Length { get; set; }

        public Guid RegionId { get; set; }

        public Guid WalkDeficultyId { get; set; }

        public Region region { get; set; }

        public WalkDeficulty walkDeficulty { get; set; }
    }
}
