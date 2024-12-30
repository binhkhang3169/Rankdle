namespace APIRanked.DTO
{
    public class CreateQuizDto
    {
        public string Region {  get; set; }
        public string CorrectValue { get; set; }
        public int TypeId {  get; set; }
        public string TokenJWT {  get; set; }

        public string Url {  get; set; }
    }
}
