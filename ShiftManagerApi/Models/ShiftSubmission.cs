namespace ShiftManagerApi.Models
{
    public class ShiftSubmission
    {
        public string Name {  get; set; } = string.Empty;//Emptyは中身が何もないと分かりやすくするため

        public List<string> Dates { get; set; } = new List<string>();
    }
}
