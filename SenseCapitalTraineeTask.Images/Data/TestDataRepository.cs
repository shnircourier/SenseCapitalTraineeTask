using SenseCapitalTraineeTask.Images.Data.Entities;

namespace SenseCapitalTraineeTask.Images.Data;

public class TestDataRepository : IRepository<Image>
{
    private readonly List<Image> _images;

    public TestDataRepository()
    {
        _images = new List<Image>
        {
            new()
            {
                Id = "641d80a8fe5581c7b7f7f304"
            },
            new()
            {
                Id = "641d80a8fe5581c7b7f7f303"
            },
            new()
            {
                Id = "641d80a8fe5581c7b7f7f302"
            }
        };
    }
    public Task<List<Image>> Get()
    {
        return Task.FromResult(_images);
    }

    public Task<Image> Get(string id)
    {
        return Task.FromResult(_images.FirstOrDefault(i => i.Id == id))!;
    }
}