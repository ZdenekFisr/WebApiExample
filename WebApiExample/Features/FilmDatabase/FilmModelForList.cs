namespace WebApiExample.Features.FilmDatabase
{
    public class FilmModelForList : Model
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required short YearOfRelease { get; set; }

        public required short LengthInMinutes { get; set; }

        public required byte Rating { get; set; }
    }
}
