namespace GameStore.Model
{
    public interface IRepository
    {
        void Add(Game game);
        void Delete(Game game);
        List<Game> ReadAll();
        Game ReadById(int id);
        void Update(Game game);
    }
}
