namespace PlanMyMeal.Api.Interface
{
    public interface IBlobService
    {
        public Task<string> PostImage(string container, IFormFile image);
    }
}
