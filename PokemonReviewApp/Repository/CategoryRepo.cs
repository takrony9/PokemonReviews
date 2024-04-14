using PokemonReviewApp.Data;
using PokemonReviewApp.Entities;
using PokemonReviewApp.Interfaces;
using System.Linq;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepo : ICategory
    {
        private readonly PokemonDbContext _context;
        public CategoryRepo(PokemonDbContext context)
        {
            _context = context;
        }
        public ICollection<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
        public Category GetCategory(int id)
        {
            return _context.Categories.Where(p => p.Id == id).FirstOrDefault(); 
        }
        public ICollection<Pokemon> GetPokemonsByCategory(int CategoryId)
        {

            return _context.PokemonCategories.Where(p => p.CategoryId == CategoryId).Select(p => p.Pokemon).ToList();
        }
        public bool CategoryExists(int id) {
            return _context.Categories.Any(p => p.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);  
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }
    }
}
